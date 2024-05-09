using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct one model for the Evaluation Criteria setup modal.
    /// </summary>
    internal class EvaluationCriteriaModalModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        public EvaluationCriteriaModalModelBuilder(IUnitOfWork unitOfWork, int mechanismTemplateElementId)
            : base(unitOfWork)
        {
            this.MechanismTemplateElementId = mechanismTemplateElementId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// MechanismTemplate entity being modified
        /// </summary>
        protected int MechanismTemplateElementId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<MechanismTemplateElement> SearchResults { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IEvaluationCriteriaModalModel> Results { get; private set; } = new Container<IEvaluationCriteriaModalModel>();
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
        /// Does all the heavy lifting for retrieving the Award/Mechanism Setup grid data.
        /// </summary>
        internal virtual void Search()
        {
            this.SearchResults = UnitOfWork.MechanismTemplateElementRepository.Select()
                    //
                    // Go and get the specific MechanismTemplateElement
                    //
                    .Where(x => x.MechanismTemplateElementId == this.MechanismTemplateElementId);
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
            IQueryable<IEvaluationCriteriaModalModel> modelResults = SearchResults.Select(x => new EvaluationCriteriaModalModel
            {
                //
                // Pull out the data for the model
                //
                OverallFlag = x.OverallFlag,
                ScoreFlag = x.ScoreFlag,
                TextFlag = x.TextFlag,
                RecommendedWordCount = x.RecommendedWordCount,
                ShowAbbreviationOnScoreboard = x.ShowAbbreviationOnScoreboard,
                SortOrder = x.SortOrder,
                SummarySortOrder = x.SummarySortOrder,
                SummaryIncludeFlag = x.SummaryIncludeFlag,
                InstructionText = x.InstructionText,
                EvaluationCriteria = x.MechanismTemplate.MechanismTemplateElements.Select(y => y.MechanismTemplateElementId),
                ElementDescription = x.ClientElement.ElementDescription,
                //
                // and now pull out the identifiers
                //
                ClientElementId = x.ClientElementId,
                MechanismTemplateElementId = x.MechanismTemplateElementId
            });
            //
            // And finally we execute it.

            this.Results.ModelList = modelResults.ToList();
        }
        #endregion
    }
}
