using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Provides access to and methods to support ApplicationScoring
    /// </summary> 
    public interface IApplicationScoringRepository
    {
        /// <summary>
        /// Retrieves the application comments associated with the application specified by the input appId
        /// parameters.
        /// </summary>
        /// <param name="panelApplicationId">the selected panel application id</param>
        /// <returns>Zero or more Summary comments for this Application</returns>
        ResultModel<ISummaryCommentModel> GetApplicationComments(int panelApplicationId, int commentTypeId);

        /// <summary>
        /// Retrieves the pre-meeting scores for the reviewer for a given session panel and application
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>PreMeetingReviewerModel</returns>
        ResultModel<IPreMeetingReviewerModel> GetPreMtgReviewers(int panelApplicationId);

        /// <summary>
        /// Retrieves the pre-meeting scores for the reviewer for a given session panel and application
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>PreMeetingCriteriaModel</returns>
        ResultModel<IPreMeetingCriteriaModel> GetPreMtgCriteria(int panelApplicationId);
        /// <summary>
        /// Initiate the panel application workflow.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplicaiton entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        void BeginScoringWorkflow(int? panelApplicationId, int userId);
    }
    /// <summary>
    /// Provides access to and methods to support ApplicationScoring
    /// </summary>
    public class ApplicationScoringRepository: IApplicationScoringRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// The database context.
        /// </summary>
        internal P2RMISNETEntities context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationScoringRepository(P2RMISNETEntities context)
        {
            this.context = context;
        }
        #endregion
        #region Services Provided
        /// <summary>
        /// Retrieves the application comments associated with the application specified by the input appId
        /// parameters.
        /// </summary>
        /// <param name="panelApplicationId">the selected panel application id</param>
        /// <returns>Zero or more Summary comments for this Application</returns>
        public ResultModel<ISummaryCommentModel> GetApplicationComments(int panelApplicationId, int commentTypeId)
        {
            ResultModel<ISummaryCommentModel> result = new ResultModel<ISummaryCommentModel>();
            result.ModelList = RepositoryHelpers.GetApplicationComments(context, panelApplicationId, commentTypeId);
            return result;
        }
        /// <summary>
        /// Retrieves the assigned reviewers for the application
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>PreMeetingReviewerModel</returns>
        public ResultModel<IPreMeetingReviewerModel> GetPreMtgReviewers(int panelApplicationId)
        {
            ResultModel<IPreMeetingReviewerModel> result = new ResultModel<IPreMeetingReviewerModel>();

            result.ModelList = RepositoryHelpers.GetPreMtgReviewers(context, panelApplicationId);

            return result;
        }
        /// <summary>
        /// Retrieves the pre-meeting scores for the reviewer for a given session panel and application
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>PreMeetingCriteriaModel</returns>
        public ResultModel<IPreMeetingCriteriaModel> GetPreMtgCriteria(int panelApplicationId)
        {
            ResultModel<IPreMeetingCriteriaModel> result = new ResultModel<IPreMeetingCriteriaModel>();
            result.ModelList = RepositoryHelpers.GetPreMtgReviewerScores(context, panelApplicationId);
            return result;
        }
        /// <summary>
        /// Initiate the panel application workflow.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplicaiton entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        public void BeginScoringWorkflow(int? panelApplicationId, int userId)
        {
            context.uspBeginScoringWorkflow(panelApplicationId, userId);
        }
        #endregion
    }
}
