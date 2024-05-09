using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    public partial class UserApplicationComment : IStandardDateFields
    {
        /// <summary>
        /// Extension to modify a user application comment
        /// </summary>
        /// <param name="comment">Text of the comment</param>
        /// <param name="commentTypeId">Identifier what type the comment is</param>
        /// <param name="userId">Identifier for a user</param>
        public UserApplicationComment Modify(string comment, int commentTypeId, int userId)
        {
            this.Comments = comment;
            this.CommentTypeID = commentTypeId;
            Helper.UpdateModifiedFields(this, userId);

            return this;
        }
        /// <summary>
        /// Extension to populate an empty user application comment
        /// </summary>
        /// <param name="comment">Text of the comment</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="commentTypeId">Identifier what type the comment is</param>
        /// <param name="userId">Identifier for a user</param>
        /// 
        /// <returns>UserApplicationComment entity</returns>
        public UserApplicationComment Populate(string comment, int panelApplicationId, int commentTypeId, int userId)
        {
            this.UserID = userId;
            this.PanelApplicationId = panelApplicationId;
            this.Comments = comment;
            this.CommentTypeID = commentTypeId;
            Helper.UpdateCreatedFields(this, userId);
            Helper.UpdateModifiedFields(this, userId);

            return this;
        }
        /// <summary>
        /// Extension to populate a UserApplicationComment
        /// </summary>
        /// <param name="userId">Identifier for a user</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="comment">Text of the comment</param>
        /// <param name="commentTypeId">Identifier what type the comment is</param>
        public UserApplicationComment Populate(int userId, int panelApplicationId, string comment, int commentTypeId)
        {
            this.UserID = userId;
            this.PanelApplicationId = panelApplicationId;
            this.Comments = comment;
            this.CommentTypeID = commentTypeId;
            return this;
        } 
        /// <summary>
        /// Extension to remove a user application comment
        /// </summary>
        /// <param name="userId">Identifier for a user</param>
        public UserApplicationComment Remove(int userId)
        {
            Helper.UpdateDeletedFields(this, userId);
            return this;
        }
    }
}
