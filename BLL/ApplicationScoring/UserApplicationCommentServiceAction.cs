using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete UserApplicationComment.
    /// </summary>
    public class UserApplicationCommentServiceAction: ServiceAction<UserApplicationComment>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public UserApplicationCommentServiceAction()
        {
            //
            // By default all actions are valid.  Only Modify may be invalid.
            //
            this.WasActionValid = true;
        }
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        public void Populate(int panelApplicationId, string comment, int commentType)
        {
            this.PanelApplicationId = panelApplicationId;
            this.Comment = comment;
            this.CommentType = commentType;
        }
        /// <summary>
        /// Initialize the action's data values for a modify.
        /// </summary>
        /// <param name="comment"></param>
        public void Populate(string comment)
        {
            this.Comment = comment;
        }
        #endregion
        #region Properties
        /// <summary>
        /// User's comment
        /// </summary>
        public string Comment { get; private set; }
        /// <summary>
        /// Comment type index
        /// </summary>
        public int CommentType { get; private set; }
        /// <summary>
        /// Associated PanelApplication entity identifier
        /// </summary>
        public int PanelApplicationId { get; private set; }
        /// <summary>
        /// Indicates the ServiceAction successfully completed the action requited.
        /// </summary>
        public bool WasActionValid { get; private set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the UserApplicationComment entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">UserApplicationComment entity</param>
        protected override void Populate(UserApplicationComment entity)
        {
            entity.Populate(this.UserId, this.PanelApplicationId, this.Comment, this.CommentType);
        }
        /// <summary>
        /// Indicates if the UserApplicationComment has data.
        /// </summary>
        protected override bool HasData
        {
            get
            {
                //
                // By definition we are expecting that it will have data.  Even
                // null or the empty string should be stored.
                //
                return true;
            }
        }
        /// <summary>
        /// Indicates if the data represents a delete.
        /// </summary>
        protected override bool IsDelete
        {
            get { return ((this.EntityIdentifier != 0) & (string.IsNullOrWhiteSpace(this.Comment))); }
        }
        #endregion
        #region Optional Overrides
        /// <summary>
        /// Override the PreModify method.  Basically we retrieve the data from the entity that does
        /// not change so we can reuse Populate.
        /// </summary>
        /// <param name="entity">UserApplicationComment entity</param>
        protected override void PreModify(UserApplicationComment entity)
        {
            this.PanelApplicationId = entity.PanelApplicationId;
            this.CommentType = entity.CommentTypeID;
        }
        /// <summary>
        /// Validate the user modifying a comment.  Verify only that the original owner 
        /// can make changes to the comment.
        /// </summary>
        /// <param name="entity">UserApplicationComment entity</param>
        /// <returns>True changes originated with the owner; false otherwise</returns>
        protected override bool IsValidModify(UserApplicationComment entity)
        {
            WasActionValid = (this.UserId == entity.UserID);
            return WasActionValid;
        }
        #endregion
    }
}
