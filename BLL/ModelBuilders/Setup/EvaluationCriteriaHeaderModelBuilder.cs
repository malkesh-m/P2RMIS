using Sra.P2rmis.Dal;
using System.Linq;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct a model for the Evaluation Criteria header.
    /// </summary>
    internal class EvaluationCriteriaHeaderModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        public EvaluationCriteriaHeaderModelBuilder(IUnitOfWork unitOfWork, int programMechanismId)
            : base(unitOfWork)
        {
            this.ProgramMechanismId = programMechanismId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// The ProgramMechanism we desire.
        /// </summary>
        protected int ProgramMechanismId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<ProgramMechanism> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IEvaluationCriteriaHeaderModel> Results { get; private set; } = new Container<IEvaluationCriteriaHeaderModel>();
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            Search();
            MakeModels();
            SetIsReleased();
            SetTemplateIds();
        }
        /// <summary>
        /// Does all the heavy lifting for retrieving the Evaluation Criteria header data.
        /// </summary>
        protected virtual void Search()
        {
            SearchResults = UnitOfWork.ProgramMechanismRepository.Select()
                //
                // We want this ProgramMechanism
                //
                .Where(x => x.ProgramMechanismId == this.ProgramMechanismId);
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
            IQueryable<IEvaluationCriteriaHeaderModel> modelResults = SearchResults.Select(x => new EvaluationCriteriaHeaderModel
            {
                //
                // Pull out the data for the model
                //
                ClientAbrv = x.ClientAwardType.Client.ClientAbrv,
                ProgramDescription = x.ProgramYear.ClientProgram.ProgramDescription,
                Year = x.ProgramYear.Year,
                ProgramAbbreviation = x.ProgramYear.ClientProgram.ProgramAbbreviation,
                ReceiptCycle = x.ReceiptCycle,
                FundingOpportunityId = x.FundingOpportunityId,
                AwardAbbreviation = x.ClientAwardType.AwardAbbreviation,
                Blinded = x.BlindedFlag,
                PartneringPiAllowedFlag = x.PartneringPiAllowedFlag,
                //
                // and now pull out the identifiers
                //
                ClientId = x.ClientAwardType.ClientId,
                ProgramMechanismId = this.ProgramMechanismId,
                ProgramYearId = x.ProgramYearId
            });
            //
            // Finally we execute it.
            //
            this.Results.ModelList = modelResults.ToList();
        }
        /// <summary>
        /// Set the model indicator telling if applications have been released.
        /// </summary>
        protected virtual void SetIsReleased()
        {
            IEvaluationCriteriaHeaderModel model = this.Results.Model;
            //
            // We have a program mechanism then we need to follow the trail of bread crumbs to
            // see if any of the applications have been release.   Then we check for any ApplicationStage
            // that has an AssignmentVisibilityFlag set to true.
            //
            model.HasApplicationsBeenReleased = SearchResults.SelectMany(x => x.Applications)
                                                    .SelectMany(x => x.PanelApplications)
                                                    .SelectMany(x => x.ApplicationStages)
                                                    .Any(x => x.AssignmentVisibilityFlag);
        }
        /// <summary>
        /// Set the MechanismTemplateId for the Asynchronous stage
        /// </summary>
        protected virtual void SetTemplateIds()
        {
            MechanismTemplate entity = SearchResults.Select(x => x.MechanismTemplates
                                                     //
                                                     // We only want the Asynchronous stage
                                                     //
                                                     .FirstOrDefault(y => y.ReviewStageId == ReviewStage.Indexes.Asynchronous))
                                                     //
                                                     // There is only one or none.
                                                     //
                                                     .FirstOrDefault();
            //
            // And we set the model with both identifier
            //
            int? scoreTemplateId =
                SearchResults.SelectMany(x => x.MechanismScoringTemplates)
                    .Select(x => x.ScoringTemplateId)
                    .FirstOrDefault();
            IEvaluationCriteriaHeaderModel model = this.Results.Model;
            model.MechanismTemplateId = entity?.MechanismTemplateId;
            model.ScoringTemplateId = scoreTemplateId != 0 ? scoreTemplateId : null;
            int? mechanismScoringTemplateId = SearchResults.SelectMany(x => x.MechanismScoringTemplates).Select(x => x.MechanismScoringTemplateId).FirstOrDefault();
            model.MechanismScoringTemplateId = mechanismScoringTemplateId != 0 ? mechanismScoringTemplateId : null;
        }
        #endregion
    }

}
