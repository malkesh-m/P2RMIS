using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal.Interfaces;


namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ApplicationBudget object. 
    /// </summary>
    public partial class ApplicationBudget : IStandardDateFields
    {
        /// <summary>
        /// Updates the comment field and comment modified fields
        /// </summary>
        /// <param name="comment">The comment</param>
        /// <param name="userId">The user identifier of the user updating the comment</param>
        public void UpdateComment(string comment, int userId)
        {
            // do not want to trigger an update, based on modified comment by and date fields if comment did not change
            if (this.DidNoteChange(comment))
            {
                this.Comments = !string.IsNullOrWhiteSpace(comment) ? comment : null;
                this.UpdateCommentModified(userId);
                Helper.UpdateModifiedFields(this, userId);
            }        
        }
        /// <summary>
        /// Deletes the contents of comment field and updates the comment modified fields
        /// </summary>
        /// <param name="userId">The user identifier of the user updating the comment</param>
        public void DeleteComment(int userId)
        {
            // do not want to trigger an update, based on modified comment by and date fields, if comment field did not change
            if (this.DidNoteChange(null))
            {
                this.Comments = null;
                this.UpdateCommentModified(userId);
                Helper.UpdateModifiedFields(this, userId);
            }
        }
        /// <summary>
        /// Updates the comment modified fields
        /// </summary>
        /// <param name="userId">The user identifier of the user updating the comment</param>
        private void UpdateCommentModified(int userId)
        {
            this.CommentModifiedBy = userId;
            this.CommentModifiedDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Determines if the comment is different from the present value
        /// </summary>
        /// <param name="comments">The new comments</param>
        /// <returns>True if the new value is not the same as the present value, false otherwise</returns>
        private bool DidNoteChange(string comments)
        {
            return (!string.IsNullOrEmpty(this.Comments) && !string.IsNullOrEmpty(comments) && this.Comments != comments) ||
                string.IsNullOrEmpty(this.Comments) && !string.IsNullOrEmpty(comments) ||
                !string.IsNullOrEmpty(this.Comments) && string.IsNullOrEmpty(comments);
        }
    }
}
