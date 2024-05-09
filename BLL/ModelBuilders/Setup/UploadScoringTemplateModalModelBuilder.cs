using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Setup
{
    internal class UploadScoringTemplateModalModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param> 
        public UploadScoringTemplateModalModelBuilder(IUnitOfWork unitOfWork, int scoringTemplateId)
            : base(unitOfWork)
        {
            this.ScoringTemplateId = scoringTemplateId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Scoring Template entity identifier
        /// </summary>
        protected int ScoringTemplateId { get; set; }
        /// <summary>
        /// Search results
        /// </summary>
        protected IQueryable<ScoringTemplatePhase> SearchResults { get; set; }
        /// <summary>
        /// Constructed models to populate the grid.
        /// </summary>
        protected IQueryable<IUploadScoringTemplateGridModalModel> ModelResults { get; set; }
        /// <summary>
        /// The specific ScoringTemplate
        /// </summary>
        protected ScoringTemplate SearchScoringTemplate { get; set; }
        /// <summary>
        /// Builder results.
        /// </summary>
        internal Container<IUploadScoringTemplateModalModel> Results { get; private set; } = new Container<IUploadScoringTemplateModalModel>();
        #endregion
        #region Services
        /// <summary>
        /// Build & Construct the Container.  
        /// </summary>
        public override void BuildContainer()
        {
            Search();
            MakeGridModels();
            MakeModel();
        }
        /// <summary>
        /// Does all the heavy lifting for retrieving the Award/Mechanism Setup grid data.
        /// </summary>
        internal virtual void Search()
        {
            //
            // Locate the Scoring Template & pull it's phases.  We retrieve the data from their.
            //
            this.SearchResults = UnitOfWork.ScoringTemplate.Select()
                                //
                                // Get the meeting identified by the ClientMeetingId
                                //
                                .Where(x => x.ScoringTemplateId == this.ScoringTemplateId)
                                //
                                // then for each of the ProgramMeeting's
                                //
                                .SelectMany(x => x.ScoringTemplatePhases)
                                //
                                // and now we order them as assigned
                                //
                                .OrderBy(x => x.StepOrder);
            //
            // Also need the specific scoring template to pick up the template name
            //
            this.SearchScoringTemplate = UnitOfWork.ScoringTemplate.GetByID(this.ScoringTemplateId);
        }
        /// <summary>
        /// Does all the heavy lifting for modeling the search results.
        /// </summary>
        internal virtual void MakeGridModels()
        {
            //
            // Now that we have the records from the database that will populate the grid
            // we can model the data & return it.
            //
            this.ModelResults = SearchResults.Select(x => new UploadScoringTemplateGridModalModel
            {
                //
                // Pull out the data for the model
                //
                StepTypeName = x.StepType.StepTypeName,
                //
                // This describes the overall scoring information
                //
                OverallType = x.ClientScoringScale1.ScoreType,
                OverallAdjectivalScale = x.ClientScoringScale1.ClientScoringScaleAdjectivals.Select(y => y.ScoreLabel).ToList(),
                OverallHighValue = x.ClientScoringScale1.HighValue,
                OverallLowValue = x.ClientScoringScale1.LowValue,
                //
                // This describes the criteria scoring information
                //
                CriteriaType = x.ClientScoringScale.ScoreType,
                CriteriaAdjectivalScale = x.ClientScoringScale.ClientScoringScaleAdjectivals.Select(y => y.ScoreLabel).ToList(),
                CriteriaHighValue = x.ClientScoringScale.HighValue,
                CriteriaLowValue = x.ClientScoringScale.LowValue
            });
        }
        /// <summary>
        /// Construct the final model.
        /// </summary>
        protected void MakeModel()
        {
            //
            // We execute the query to get the grid results & check the number
            //
            var gridSearchResults = ModelResults.ToList();
            //
            // The assumption here is that
            //  - there are at least 2 phases
            //  - the last phase is implicitly ordered to be the last
            //
            int count = gridSearchResults.Count() - 1;
            IUploadScoringTemplateModalModel model = new UploadScoringTemplateModalModel(
                                                                                        this.SearchScoringTemplate.TemplateName,
                                                                                        gridSearchResults.Take(count),
                                                                                        gridSearchResults.Skip(count),
                                                                                        this.ScoringTemplateId
                                                                                        );
            //
            // All that is left to do is to just set up what to return
            //
            this.Results.ModelList = new List<IUploadScoringTemplateModalModel>() { model };
        }
        #endregion
    }
}
