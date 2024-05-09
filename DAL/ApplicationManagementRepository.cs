using System.Collections.Generic;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Repository object used to retrieve the open programs 
    /// assignment list for a specific user & client.
    /// </summary>
    public class ProgramFYRepository : GenericRepository<ProgramYear>
    {
        #region Construction; Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ProgramFYRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion
        #region Repository Methods
        /// <summary>
        /// Returns the open programs assignment list for a specific
        /// user & client.
        /// </summary>
        /// <returns>Enumerable list of ProgramListResultModels</returns>
        public IEnumerable<ProgramListResultModel> GetOpenProgramsList(List<int> clientList)
        {
            return RepositoryHelpers.GetOpenProgramsList(clientList, context);
        }

        /// <summary>
        /// Returns the open programs assignment list for a specific assigned user
        /// </summary>
        /// <returns>Enumerable list of ProgramListResultModels</returns>
        public IEnumerable<ProgramListResultModel> GetAssignedOpenProgramsList(int? userId)
        {
            return RepositoryHelpers.GetAssignedOpenProgramsList(userId, context);
        }
        /// <summary>
        /// Returns the Applications details (details, reviewers & scores). 
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="panelId">Panel identifier</param>
        /// <returns>ResultModel containing the applications detail</returns>
        public ApplicationDetailResultModel GetApplicationDetail(int panelApplicationId, int panelId, List<int> commentTypes)
        {
            ApplicationDetailResultModel results = new ApplicationDetailResultModel();

            ///
            /// Get the application's reviewers
            ///
            results.ReviewerDetails = RepositoryHelpers.GetApplicationReviewersByApplicationId(context, panelApplicationId, panelId);
            ///
            /// And now the scores
            /// 
            results.ReviewerScoreDetails = RepositoryHelpers.GetApplicationScoresByApplicationId(context, panelApplicationId);
            ///
            /// Retrieve the reviewer comments for the application
            /// 
            results.ReviewerComments = RepositoryHelpers.GetReviewerCommentsByApplicationId(context, panelApplicationId);
            ///
            /// Finally retrieve the user comments
            /// 
            results.UserApplicationComments = RepositoryHelpers.GetUserCommentsByApplicationId(context, panelApplicationId, commentTypes);
            ///
            ///Retrieve comment lookup values
            ///
            results.CommentLookupTypes = RepositoryHelpers.GetCommentExceptAdminLookupValues(context, commentTypes);
            return results;
        }
        /// <summary>
        /// Gets the user application comments.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns></returns>
        public IEnumerable<UserApplicationComments> GetUserApplicationComments(int panelApplicationId, bool canAccessAdminNote, bool canAccessDiscussionNote,
                bool canAccessGeneralNote, bool canAccessUnassignedReviewerNote)
        {
            var commentTypes = new List<int>();
            if (canAccessAdminNote)
                commentTypes.Add(CommentType.Indexes.AdminNote);
            if (canAccessDiscussionNote)
                commentTypes.Add(CommentType.Indexes.DiscussionNote);
            if (canAccessGeneralNote)
                commentTypes.Add(CommentType.Indexes.GeneralNote);
            if (canAccessUnassignedReviewerNote)
                commentTypes.Add(CommentType.Indexes.ReviewerNote);
            var comments = RepositoryHelpers.GetUserCommentsByApplicationId(context, panelApplicationId, commentTypes);
            return comments;
        }
        #endregion
    }
}
