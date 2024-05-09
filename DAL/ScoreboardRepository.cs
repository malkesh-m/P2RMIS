using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Database access methods for the Scoreboard view.
    /// </summary>
    public class ScoreboardRepository : GenericRepository<ViewPanelDetails_Result>  // TODO:: change T type
    {
        #region Construction; Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ScoreboardRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion
        #region Repository Methods
        /// <summary>
        /// Retrieves the application's scoreboard from the P2rmis database context.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="panelId">Panel identifier</param>
        /// <returns>ResultModel containing containing the scoreboard detail</returns>
        public ScoreboardResultModel GetScoreboardByApplicationIdPanelId(int panelApplicationId, int panelId)
        {
            ///
            /// Create the result object
            /// 
            ScoreboardResultModel result = new ScoreboardResultModel();
            ///
            /// Retrieve the scoreboard data
            ///

            result.ReviewerDetails = RepositoryHelpers.GetScoreboardReviewersByApplicationId(context, panelApplicationId, panelId);

            return result;
        }
        #endregion
    }
}
