using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.WebModels.Criteria;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct one or more models for the ProgramSetup grid
    /// </summary>
    internal class ProgramSetupModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="clientIds">List of users Client entity identifiers</param>
        public ProgramSetupModelBuilder(IUnitOfWork unitOfWork, IList<int> clientIds)
            : base(unitOfWork)
        {
            this.ClientIds = clientIds;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        protected ProgramSetupModelBuilder(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion
        #region Attributes
        /// <summary>
        /// Search parameters
        /// </summary>
        protected virtual IList<int> ClientIds { get; set; } = new List<int>();
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<ProgramYear> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IProgramSetupModel> Results { get; private set; } = new Container<IProgramSetupModel>();
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            Search();
            MakeModels();
        }
        /// <summary>
        /// Does all the heavy lifting for retrieving the Program Setup grid data.
        /// </summary>
        internal virtual void Search()
        {
            this.SearchResults = UnitOfWork.ClientProgramRepository.ProgramSetup(ClientIds);
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        internal virtual void MakeModels()
        {
            //
            // Now that we have the records from the database that will populate the grid
            // we can model the data & return it.
            //
            IQueryable<ProgramSetupModel> modelResults = SearchResults.Select(x => new ProgramSetupModel
                                                    {
                                                        // information from Client entity
                                                        ClientAbrv = x.ClientProgram.Client.ClientAbrv,
                                                        ClientDesc = x.ClientProgram.Client.ClientDesc,
                                                        ClientMeetingCount = x.ProgramMeetings.Count(),
                                                        ClientId = x.ClientProgram.ClientId,
                                                        // information from ClientProgram entity
                                                        ProgramDescription = x.ClientProgram.ProgramDescription,
                                                        ProgramAbbreviation = x.ClientProgram.ProgramAbbreviation,
                                                        ClientProgramId = x.ClientProgramId,
                                                        // information from ProgramYear entity
                                                        Active = !x.DateClosed.HasValue,
                                                        Year = x.Year,
                                                        ProgramMechanismCount = x.ProgramMechanism.Count(),
                                                        ProgramYearId = x.ProgramYearId,
                                                        IsApplicationsReleased = x.ProgramPanels
                                                                                .Select(y => y.SessionPanel)
                                                                                .SelectMany(y => y.ProgramPanels)
                                                                                .Select(y => y.SessionPanel)
                                                                                .SelectMany(y => y.PanelApplications)
                                                                                .SelectMany(y => y.ApplicationStages).Where(y => y.ReviewStageId == ReviewStage.Indexes.Asynchronous)
                                                                                .Any(y => y.AssignmentVisibilityFlag)
            })
            //
            //  And now we order the results as required
            //
            .OrderBy(x => x.ClientAbrv)
            .ThenByDescending(x => x.Year)
            .ThenBy(x => x.ProgramAbbreviation);
            //
            // And finally we order (if any) & execute it
            //
            this.Results.ModelList = modelResults.ToList();
        }
        #endregion
    }
    /// <summary>
    /// Model builder to construct one a model for the ProgramSetup Wizard.
    /// </summary>
    internal class ProgramSetupWizardModelBuilder : ProgramSetupModelBuilder
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="clientIds">List of users Client entity identifiers</param>
        public ProgramSetupWizardModelBuilder(IUnitOfWork unitOfWork, int clientId, int programYearId)
            : base(unitOfWork)
        {
            this.ClientIds = new List<int>() { clientId };
            this.ProgramYearId = programYearId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Identifies the specific ProgramYear to retrieve.
        /// </summary>
        private int ProgramYearId { get; set; }
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
        /// Filter the base search results for a specific Program
        /// </summary>
        protected virtual void FilterSearchResults()
        {
            this.SearchResults = this.SearchResults.Where(y => y.ProgramYearId == ProgramYearId);
        }
        #endregion
    }
    /// <summary>
    /// Model builder to construct one a model for the award/mechanism physical year drop down
    /// </summary>
    internal class ProgramYearModelBuilder : ProgramSetupModelBuilder
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="clientIds">Client entity identifier to search for physical years for</param>
        public ProgramYearModelBuilder(IUnitOfWork unitOfWork, int clientId)
            : base(unitOfWork)
        {
            this.ClientIds = new List<int>() { clientId };
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Builder results.
        /// </summary>
        internal new Container<IFilterableProgramYearModel> Results { get; private set; } = new Container<IFilterableProgramYearModel>();
        #endregion
        #region Services
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        internal override void MakeModels()
        {
            //
            // Now that we have the records from the database that will populate the grid
            // we can model the data & return it.
            //
            IQueryable<IFilterableProgramYearModel> modelResults = SearchResults.Select(x => new FilterableProgramYearModel
            {
                                                        Year = x.Year,
                                                        IsActive = !x.DateClosed.HasValue,
                                                        ProgramYearId = x.ProgramYearId,
                                                        ClientProgramId = x.ClientProgramId
                                                    });
            this.Results.ModelList = modelResults;
        }
        #endregion
    }
    /// <summary>
    /// Model builder to construct a container of model for the award/mechanism program drop down
    /// </summary>
    internal class FilterableProgramModelBuilder : ProgramSetupWizardModelBuilder
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// We know we are not going to filter on ProgramYearId so call the base constructor with a 0.
        /// </remarks>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="clientIds">Client entity identifier to search for physical years for</param>
        public FilterableProgramModelBuilder(IUnitOfWork unitOfWork, int clientId, string year)
            : base(unitOfWork, clientId, 0)
        {
            this.Year = year;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Program Year (e.g. 2013, 2014 ..)
        /// </summary>
        protected string Year { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal new Container<IFilterableProgramModel> Results { get; private set; } = new Container<IFilterableProgramModel>();
        #endregion
        #region Services
        protected override void FilterSearchResults()
        {
            this.SearchResults = this.SearchResults.Where(y => y.Year == Year);
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        internal override void MakeModels()
        {
            //
            // Now that we have the records from the database that will populate the grid
            // we can model the data & return it.
            //
            IQueryable<IFilterableProgramModel> modelResults = SearchResults.Select(x => new FilterableProgramModel
            {
                //
                // The Attribute data
                //
                ProgramAbbreviation = x.ClientProgram.ProgramAbbreviation,
                ProgramDescription = x.ClientProgram.ProgramDescription,
                IsActive = !x.DateClosed.HasValue,
                //
                // The indexes
                ClientProgramId = x.ClientProgramId,
                ProgramYearId = x.ProgramYearId
            });
            this.Results.ModelList = modelResults;
        }
        #endregion
    }

}

