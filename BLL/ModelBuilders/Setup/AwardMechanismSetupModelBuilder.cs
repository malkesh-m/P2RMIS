using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using System.Linq;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct a models for the Award/Mechanism Setup grid
    /// </summary>
    internal class AwardMechanismSetupModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="programYearId">ProgramYear entity identifiers</param>
        public AwardMechanismSetupModelBuilder(IUnitOfWork unitOfWork, int programYearId)
            : base(unitOfWork)
        {
            this.ProgramYearId = programYearId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        protected int ProgramYearId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<ProgramMechanism> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IAwardMechanismModel> Results { get; private set; } = new Container<IAwardMechanismModel>();
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            Search();
            MakeModels();
            SetPreAppValues();
        }
        /// <summary>
        /// Does all the heavy lifting for retrieving the Award/Mechanism Setup grid data.
        /// </summary>
        protected virtual void Search()
        {
            this.SearchResults = UnitOfWork.ProgramMechanismRepository.RetrieveProgramYearProgramMechanisms(this.ProgramYearId);
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        protected virtual void MakeModels()
        {
            //
            // Now that we have the records from the database that will populate the grid
            // we can model the data & return it.
            //
            IQueryable<IAwardMechanismModel> modelResults = SearchResults.Select(x => new AwardMechanismModel
            {
                //
                // Pull out the data for the model
                //
                FiscalYear = x.ProgramYear.Year,
                ClientAbbreviation = x.ProgramYear.ClientProgram.Client.ClientAbrv,
                ProgramAbbreviation = x.ProgramYear.ClientProgram.ProgramAbbreviation,
                LegacyAtmId = x.LegacyAtmId,
                ReceiptCycle = x.ReceiptCycle,
                FundingOpportunityId = x.FundingOpportunityId,
                AwardAbbreviation = x.ClientAwardType.AwardAbbreviation,
                LegacyAwardTypeId = x.ClientAwardType.LegacyAwardTypeId,
                AwardDescription = x.ClientAwardType.AwardDescription,
                PartneringPiAllowedFlag = x.PartneringPiAllowedFlag,
                BlindedFlag = x.BlindedFlag,
                ReceiptDeadline = x.ReceiptDeadline,
                IsChild = x.ParentProgramMechanismId.HasValue,
                CriteriaCount = x.MechanismTemplates.Count() == 0 ? 0 : x.MechanismTemplates.FirstOrDefault().MechanismTemplateElements.Count(),
                HasApplications = x.Applications.Count() > 0,
                //
                // and now pull out the identifiers
                //
                ClientId = x.ProgramYear.ClientProgram.ClientId,
                ClientProgramId = x.ProgramYear.ClientProgramId,
                ProgramYearId = this.ProgramYearId,
                ProgramMechanismId = x.ProgramMechanismId,
                ParentProgramMechanismId = x.ParentProgramMechanismId
            });
            //
            // And finally we order it:
            //  1) Cycle (descending)
            //  2) Funding opportunity identifier
            //  3) Award (descending)
            //
            modelResults = modelResults.OrderBy(x => x.ReceiptCycle)
                .ThenBy(x => x.FundingOpportunityId)
                .ThenBy(x => x.AwardDescription);

            this.Results.ModelList = modelResults.ToList();
        }
        /// <summary>
        /// Populate the pre-app values in the grid.  
        /// </summary>
        protected virtual void SetPreAppValues()
        {
            List<IAwardMechanismModel> result = this.Results.ModelList.ToList();
            //
            // First thing is to locate any entry that has a parent.
            //
            var list = result.Where(x => x.IsChild).
                //
                // then we pull out 
                //  - the ProgramMechanismId (which is the pre-app)
                //  - the ParentProgramMechanismid
                //  - the ReceiptCycle
                //
                Select(x => new { ThePreAppProgramMechanismId = x.ProgramMechanismId, TheParentProgramMechanismId = x.ParentProgramMechanismId, ParentReceiptCycle = x.ReceiptCycle }).ToList();
            //
            // then match the results & updated the pre-app values in the grid 
            //
            foreach (var preApp in list)
            {
                IAwardMechanismModel parentAward = result.First(x => x.ProgramMechanismId == preApp.TheParentProgramMechanismId);
                parentAward.PreAppReceiptCycle = preApp.ParentReceiptCycle;
            }
        }
        #endregion
    }
    /// <summary>
    /// Model builder to construct a model for the Add/Edit Award modal
    /// </summary>
    internal class AwardSetupWizardModelBuilder : AwardMechanismSetupModelBuilder
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <param name="programMechanismId">ProgramMechanism entity identifier</param>
        public AwardSetupWizardModelBuilder(IUnitOfWork unitOfWork, int programYearId, int programMechanismId)
            : base(unitOfWork, programYearId)
        {
            this.ProgramMechanismId = programMechanismId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        protected int ProgramMechanismId { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal new Container<IAwardSetupWizardModel> Results { get; private set; } = new Container<IAwardSetupWizardModel>();
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            Search();
            FilterSearchResults();
            MakeModels();
        }
        /// <summary>
        /// Filter the search results for a specific ProgramMechanism
        /// </summary>
        protected virtual void FilterSearchResults()
        {
            this.SearchResults = this.SearchResults.Where(y => y.ProgramMechanismId == this.ProgramMechanismId);
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        protected override void MakeModels()
        {
            //
            // Now that we have the records from the database that will populate the grid
            // we can model the data & return it.
            //
            IQueryable<IAwardSetupWizardModel> modelResults = SearchResults.Select(x => new AwardSetupWizardModel
            {
                //
                // Pull out the data for the model
                //
                Active = x.ProgramYear.DateClosed == null,
                Client = x.ProgramYear.ClientProgram.Client.ClientAbrv,
                Program = x.ProgramYear.ClientProgram.ProgramAbbreviation,
                Year = x.ProgramYear.Year,
                ReceiptCycle = x.ReceiptCycle,
                FundingOpportunityId = x.FundingOpportunityId,
                PartneringPiAllowedFlag = x.PartneringPiAllowedFlag,
                BlindedFlag = x.BlindedFlag,
                ReceiptDeadline = x.ReceiptDeadline,
                //
                // and now pull out the identifiers
                //
                ClientId = x.ProgramYear.ClientProgram.ClientId,
                ClientProgramId = x.ProgramYear.ClientProgramId,
                ProgramYearId = this.ProgramYearId,
                ProgramMechanismId = x.ProgramMechanismId,
                ClientAwardTypeId = x.ClientAwardTypeId,
                ParentProgramMechanismId = x.ParentProgramMechanismId
            });
            //
            // And finally we order it:
            this.Results.ModelList = modelResults;
        }
        #endregion
    }

}
