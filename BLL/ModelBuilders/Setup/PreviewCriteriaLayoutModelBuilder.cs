using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    /// <summary>
    /// Model builder to construct a model for the Evaluation Preview Criteria Layout model.
    /// </summary>
    internal class PreviewCriteriaLayoutModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        /// <param name="mechanismTemplateId"></param>
        public PreviewCriteriaLayoutModelBuilder(IUnitOfWork unitOfWork, int mechanismTemplateId)
            : base(unitOfWork)
        {
            this.MechanismTemplateId = mechanismTemplateId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// MechanismTemplate entity identifier
        /// </summary>
        protected int MechanismTemplateId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<MechanismTemplateElement> SearchResults { get; set; } 
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IPreviewCriteriaLayoutModel> Results { get; private set; } = new Container<IPreviewCriteriaLayoutModel>();
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            Search();
            MakeModels();
            MakeModelAdjectivalScores();
        }
        /// <summary>
        /// Does all the heavy lifting for retrieving the Award/Mechanism Setup grid data.
        /// </summary>
        internal virtual void Search()
        {
            this.SearchResults = UnitOfWork.MechanismTemplateRepository.Select()
                                 .Where(x => x.MechanismTemplateId == this.MechanismTemplateId)
                                 .SelectMany(x => x.MechanismTemplateElements);
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        internal virtual void MakeModels()
        {
            var a = new List<string>();
            //
            // Now that we have the records from the database that will populate the grid
            // we can model the data & return it.
            //
            IQueryable<IPreviewCriteriaLayoutModel> modelResults = SearchResults.Select(x => new PreviewCriteriaLayoutModel
            {
                //
                // Pull out the data for the model
                //
                ElementAbbreviation = x.ClientElement.ElementAbbreviation,
                ElementDescription = x.ClientElement.ElementDescription,
                InstructionText = x.InstructionText,
                RecommendedWordCount = x.RecommendedWordCount,
                SortOrder = x.SortOrder,
                IsScoringInteger = x.MechanismTemplateElementScorings.Any(y => (y.ClientScoringScale.ScoreType == ClientScoringScale.ScoringType.Integer) & (y.StepTypeId == StepType.Indexes.Preliminary)),
                IsScoringAdjectival = x.MechanismTemplateElementScorings.Any(y => (y.ClientScoringScale.ScoreType == ClientScoringScale.ScoringType.Adjectival) & (y.StepTypeId == StepType.Indexes.Preliminary)),
                IsScoringDecimal = x.MechanismTemplateElementScorings.Any(y => (y.ClientScoringScale.ScoreType == ClientScoringScale.ScoringType.Decimal) & (y.StepTypeId == StepType.Indexes.Preliminary)),
                LowValue = (x.MechanismTemplateElementScorings.Count() > 0) ? x.MechanismTemplateElementScorings.FirstOrDefault().ClientScoringScale.LowValue : (decimal?)null,
                HighValue = (x.MechanismTemplateElementScorings.Count() > 0) ? x.MechanismTemplateElementScorings.FirstOrDefault().ClientScoringScale.HighValue : (decimal?)null,
                //
                // and now pull out the identifiers
                //
                MechanismTemplateElementId = x.MechanismTemplateElementId,
                MechanismTemplateId = x.MechanismTemplateId,
                IsCriteriaText = x.TextFlag,
                IsOverall = x.OverallFlag,
                ClientScoringScaleId = (x.MechanismTemplateElementScorings.Count() > 0) ? x.MechanismTemplateElementScorings.FirstOrDefault().ClientScoringScale.ClientScoringId: 0,
            });
            //
            // And finally we order it (by the sort order) & execute.
            //
            this.Results.ModelList = modelResults.OrderBy(x => x.SortOrder).ToList();
        }
        /// <summary>
        /// Populate the Adjectival scores if there are any.
        /// </summary>
        protected void MakeModelAdjectivalScores()
        {
            //
            // Tried to use a non supported function in Linq2Entities.  Seems that this is not possible:
            //
            //  AdjectivalValues = (x.MechanismTemplateElementScorings.Count() > 0) ? x.MechanismTemplateElementScorings.FirstOrDefault().ClientScoringScale.ClientScoringScaleAdjectivals.Select(y => y.ScoreLabel).ToList() : new List<string>(),
            //
            // because Linq2Entities does not support nested queries.  Silly me.  So do it by brute force.  So
            // for each model that is adjectival we need to populate their scale.
            //
            foreach (var model in this.Results.ModelList.Where(x => x.IsScoringAdjectival))
            {
                //
                // First we get the client's scoring scale
                //
                model.AdjectivalValues = this.UnitOfWork.ClientScoringScaleRepository.GetByID(model.ClientScoringScaleId)
                                        //
                                        // then we pull out the adjectival values.  It seems odd that there is no order among the adjectivals.
                                        //
                                        .ClientScoringScaleAdjectivals
                                        //
                                        // but we only want their labels and we make a list of them.
                                        //
                                        .Select(y => y.ScoreLabel).ToList();
            }
        }
        #endregion
    }

}
