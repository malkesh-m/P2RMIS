using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Linq implementation of Online Scoring methods.
    /// </summary>
    internal partial class RepositoryHelpers
    {
        /// <summary>
        /// Retrieves Application Comment for the indicated application and type
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="appId">The applicationId for the comment(s) to be retrieved</param>
        /// <param name="commentTypeId">CommentType Index value to retrieve</param>
        /// <returns>Zero or more application summary comments</returns>
        /// <remarks>
        ///      This is a more generic version of the query to retrieve comments.  The code that useds the other method (GetApplicationSummaryComments) 
        ///      should be refactored.
        /// </remarks>
        internal static IEnumerable<ISummaryCommentModel> GetApplicationComments(P2RMISNETEntities context, int panelApplicationId, int commentTypeId)
        {
            // gets the summary comments for the specified application. Associates the comment with the last user to affect the contents of the comment
            var results = (from sumCmts in context.UserApplicationComments
                           join u in context.UserInfoes on sumCmts.ModifiedBy equals u.UserID
                           where
                               ((sumCmts.PanelApplicationId == panelApplicationId) && (sumCmts.CommentTypeID == commentTypeId))
                           select new SummaryCommentModel
                           {
                               CommentID = sumCmts.UserApplicationCommentID,
                               UserId = u.UserID,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               CommentDate = sumCmts.ModifiedDate,
                               Comment = sumCmts.Comments
                           });

            return results;
        }
    }
}
