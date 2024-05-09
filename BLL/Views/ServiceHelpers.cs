using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Bll.Views
{
    internal class ServiceHelpers
    {
        /// <summary>
        /// Create a data layer object when a comment is added.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="comments">Comments</param>
        /// <returns>UserApplicationComment to be added to the database</returns>
        internal static UserApplicationComment CreateComment(int userId, int panelApplicationId, string comments, int commentType)
        {
            UserApplicationComment newComment = new UserApplicationComment();

            newComment.UserID = userId;
            newComment.PanelApplicationId = panelApplicationId;
            newComment.Comments = comments;
            newComment.CommentTypeID = commentType;
            Helper.UpdateCreatedFields(newComment, userId);
            Helper.UpdateModifiedFields(newComment, userId);

            return newComment;
        }
        /// <summary>
        /// Locate a comment within the context.  Once located the comment is modified with the 
        /// provided comment.  The user that modified the comment & modified date & time are 
        /// updated.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="comments">User comments</param>
        /// <returns>Updated UserApplicationComment</returns>
        internal static UserApplicationComment EditComment(int userId, int commentId, string comments, int commentType, IUserApplicationCommentRepository repository)
        {
            //
            // Retrieve the current object.  If one is located then check
            // that the comments have been changed.  If it has then update it in 
            // the context & save it.
            // 
            UserApplicationComment result = repository.GetByID(commentId);
            //
            // Update the appropriate filed.  If one cannot be located just let it error out
            // and be caught by the try/catch block in the controller.
            // 
            result.Comments = comments;
            result.ModifiedBy = userId;
            result.ModifiedDate = GlobalProperties.P2rmisDateTimeNow;
            result.CommentTypeID = commentType;

            return result;
        }
    }
}
