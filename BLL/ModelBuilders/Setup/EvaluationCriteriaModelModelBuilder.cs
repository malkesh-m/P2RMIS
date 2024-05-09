using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct one or more models for the Evaluation Criteria grid.
    /// </summary>
    internal class EvaluationCriteriaModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        public EvaluationCriteriaModelBuilder(IUnitOfWork unitOfWork, int programMechanismId)
            : base(unitOfWork)
        {
            this.ProgramMechanismId = programMechanismId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ProgramMechanism entity identifier.  Specifies the source of the 
        /// evaluation criteria
        /// </summary>
        protected int ProgramMechanismId {get; set;}
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<MechanismTemplateElement> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IEvaluationCriteriaModel> Results { get; private set; } = new Container<IEvaluationCriteriaModel>();
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
        /// Does all the heavy lifting for retrieving the Evaluation Criteria grid data.
        /// </summary>
        internal virtual void Search()
        {
            SearchResults = UnitOfWork.ProgramMechanismRepository.Select()
                //
                // We want this ProgramMechanism
                //
                .Where(x => x.ProgramMechanismId == this.ProgramMechanismId)
                //
                // There can be multiple MechanismTemplates.  We want only the Asynchronous template and there is only one of those
                //
                .SelectMany(x => x.MechanismTemplates.Where(y => y.ReviewStageId == ReviewStage.Indexes.Asynchronous))
                //
                // finally we get the MechanismTemplateElements
                //
                .SelectMany(x => x.MechanismTemplateElements);
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
            IQueryable<IEvaluationCriteriaModel> modelResults = SearchResults.Select(x => new EvaluationCriteriaModel
            {
                //
                // Pull out the data for the model
                //
                ElementAbbreviation = x.ClientElement.ElementAbbreviation,
                ElementDescription = x.ClientElement.ElementDescription,
                OverallFlag = x.OverallFlag,
                InstructionText = x.InstructionText,
                RecommendedWordCount = x.RecommendedWordCount,
                ScoreFlag = x.ScoreFlag,
                TextFlag = x.TextFlag,
                SummarySortOrder = x.SummarySortOrder,
                SummaryIncludeFlag = x.SummaryIncludeFlag,
                SortOrder = x.SortOrder,
                //
                // and now pull out the identifiers
                //
                MechanismTemplateElementId = x.MechanismTemplateElementId,
                MechanismTemplateId = x.MechanismTemplateId,
                ProgramMechanismid = this.ProgramMechanismId
                
            });
            //
            // we order it:
            //   
            //
            modelResults = modelResults.OrderBy(x => x.SortOrder);
            //
            // and we execute it
            //
            this.Results.ModelList = modelResults.ToList();
        }
        #endregion
    }
}
