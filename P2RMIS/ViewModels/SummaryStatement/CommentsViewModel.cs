using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.Bll.Views.ApplicationDetails;
using Newtonsoft.Json;

namespace Sra.P2rmis.Web.ViewModels.SummaryStatement
{
    public class CommentsViewModel
    {
        public CommentsViewModel() { }

        public CommentsViewModel(List<IUserApplicationCommentFacts> comments)
        {
            Comments = comments.ConvertAll(x => new CommentViewModel(x));
        }
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public List<CommentViewModel> Comments { get; set; }

        public class CommentViewModel {

            public CommentViewModel(IUserApplicationCommentFacts comment)
            {
                CommentId = comment.CommentID;
                UserId = comment.UserID;
                ApplicationId = comment.ApplicationID;
                UserFullName = comment.UserFullName;
                Comments = comment.Comments;
                ModifiedDate = comment.ModifiedDate;
                CommentLkpId = comment.CommentLkpId;
                CommentLkpDescription = comment.CommentLkpDescription;
            }
            /// <summary>
            /// Gets the comment identifier.
            /// </summary>
            /// <value>
            /// The comment identifier.
            /// </value>
            [JsonProperty("commentId")]
            public int CommentId { get; private set; }
            /// <summary>
            /// Gets the user identifier.
            /// </summary>
            /// <value>
            /// The user identifier.
            /// </value>
            [JsonProperty("userId")]
            public int UserId { get; private set; }
            /// <summary>
            /// Gets the application identifier.
            /// </summary>
            /// <value>
            /// The application identifier.
            /// </value>
            [JsonProperty("applicationId")]
            public string ApplicationId { get; private set; }
            /// <summary>
            /// Gets the full name of the user.
            /// </summary>
            /// <value>
            /// The full name of the user.
            /// </value>
            [JsonProperty("userFullName")]
            public string UserFullName { get; private set; }
            /// <summary>
            /// Gets the comments.
            /// </summary>
            /// <value>
            /// The comments.
            /// </value>
            [JsonProperty("comments")]
            public string Comments { get; private set; }
            /// <summary>
            /// Gets the modified date.
            /// </summary>
            /// <value>
            /// The modified date.
            /// </value>
            [JsonProperty("modifiedDate")]
            public string ModifiedDate { get; private set; }
            /// <summary>
            /// Gets the comment LKP identifier.
            /// </summary>
            /// <value>
            /// The comment LKP identifier.
            /// </value>
            [JsonProperty("commentLkpId")]
            public int CommentLkpId { get; private set; }
            /// <summary>
            /// Gets the comment LKP description.
            /// </summary>
            /// <value>
            /// The comment LKP description.
            /// </value>
            [JsonProperty("commentLkpDescription")]
            public string CommentLkpDescription { get; private set; }
        }
    }
}