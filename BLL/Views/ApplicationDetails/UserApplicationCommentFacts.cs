using System;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll.Views.ApplicationDetails
{
    public class UserApplicationCommentFacts : IUserApplicationCommentFacts
    {
        
        #region Constructor
        /// <summary>
        /// Constructor creates & populates the business layer representation of a Reviewer comments.
        /// <remarks>
        /// There are other data elements returned from the data layer but did not appear to be required
        /// in the business layer or presentation layer.
        /// </remarks>
        /// </summary>
        /// <param name="item">Data layer's User Application comment object</param>
        internal UserApplicationCommentFacts(IUserApplicationComments item)
        {
            this.CommentID = item.CommentID;
            this.UserID = item.UserID;
            this.ApplicationID  = item.ApplicationID;
            this.UserFullName = ViewHelpers.ConstructNameWithPrefix(item.UserPrefix, item.UserFirstName, item.UserLastName);
            this.Comments = item.Comments;
            this.ModifiedDate = String.Format("{0:g}", item.ModifiedDate);
            this.CommentLkpId = item.CommentLkpId;
            this.CommentLkpDescription = item.CommentLkpDescription;
        }

        public UserApplicationCommentFacts()
        {
            this.CommentID = 1;
            this.UserID = 10;
            this.ApplicationID = "BC120633";
            this.UserFullName = "Dr. Full Name";
            this.Comments = "I ADDED THIS COMMENT: MIMSY WERE THE BOROGROVES!!!";
            this.ModifiedDate = String.Format("{0:g}", GlobalProperties.P2rmisDateToday);
        }
        #endregion
        #region Properties
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int CommentID { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string ApplicationID { get; set; }
        /// <summary>
        /// Returns string of comment authors full name
        /// </summary>
        public string UserFullName { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public string ModifiedDate { get; set; }
        /// <summary>
        /// Comment lookup ID
        /// </summary>
        public int CommentLkpId { get; set; }
        /// <summary>
        /// Comment lookup description
        /// </summary>
        public string CommentLkpDescription { get; set; }
        #endregion
    }
    
}
