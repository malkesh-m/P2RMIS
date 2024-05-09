using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.ViewModels.PanelManagement
{
    /// <summary>
    /// Discussion board view model
    /// </summary>
    public class DiscussionBoardViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscussionBoardViewModel"/> class.
        /// </summary>
        public DiscussionBoardViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscussionBoardViewModel"/> class.
        /// </summary>
        /// <param name="discussionBoard">The discussion board.</param>
        public DiscussionBoardViewModel(IDiscussionBoardModel discussionBoard)
        {
            ApplicationStageStepId = discussionBoard.ApplicationStageStepId;
            ApplicationStageStepDiscussionId = discussionBoard.ApplicationStageStepDiscussionId;
            LogNumber = discussionBoard.LogNumber;
            DiscussionExists = discussionBoard.DiscussionExists;
            PanelApplicationId = discussionBoard.PanelApplicationId;
            SessionPanelId = discussionBoard.SessionPanelId;
            DiscussionComments = discussionBoard.DiscussionComments.ToList<IDiscussionCommentModel>().ConvertAll(x => new DiscussionComment(x));
            Participants = discussionBoard.Participants.ToList<IDiscussionParticipantModel>().ConvertAll(x => new DiscussionParticipant(x));
            Participants = discussionBoard.Participants.OrderBy(x => x.LastName).ToList().OrderByDescending(x => x.IsModerator).ThenByDescending(x => x.IsChair).ToList<IDiscussionParticipantModel>().ConvertAll(x => new DiscussionParticipant(x));
            IsModDone = discussionBoard.IsModDone;
        }

        /// <summary>
        /// Gets or sets the application stage step identifier.
        /// </summary>
        /// <value>
        /// The application stage step identifier.
        /// </value>
        public int ApplicationStageStepId { get; set; }

        /// <summary>
        /// Gets or sets the application stage step discussion identifier.
        /// </summary>
        /// <value>
        /// The application stage step discussion identifier.
        /// </value>
        public int? ApplicationStageStepDiscussionId { get; set; }

        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number that identifies the application.
        /// </value>
        public string LogNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [discussion exists].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [discussion exists]; otherwise, <c>false</c>.
        /// </value>
        public bool DiscussionExists { get; set; }

        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        public int PanelApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the session panel identifier.
        /// </summary>
        /// <value>
        /// The session panel identifier.
        /// </value>
        public int SessionPanelId { get; set; }

        /// <summary>
        /// Gets or sets the discussion comments.
        /// </summary>
        /// <value>
        /// The discussion comments.
        /// </value>
        public List<DiscussionComment> DiscussionComments { get; set; }
        /// <summary>
        /// Returns the number of discussion comments.
        /// </summary>
        public int DiscussionCommentsCount
        {
            get { return DiscussionComments != null? DiscussionComments.Count(): 0; }
        }
        /// <summary>
        /// Gets or sets the participants of the discussion.
        /// </summary>
        /// <value>
        /// The participants available to take part in the discussion.
        /// </value>
        public List<DiscussionParticipant> Participants { get; set; }
        /// <summary>
        /// Indicates if the MOD has completed
        /// </summary>
        public bool IsModDone { get; set; }
        /// <summary>
        /// The last page URL
        /// </summary>
        public string LastPageUrl { get; set; }

        /// <summary>
        /// Discussion comment
        /// </summary>
        public class DiscussionComment
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DiscussionComment"/> class.
            /// </summary>
            /// <param name="comment">The comment.</param>
            public DiscussionComment(IDiscussionCommentModel comment)
            {
                AuthorFirstName = comment.AuthorFirstName;
                AuthorLastName = comment.AuthorLastName;
                AuthorSystemRole = comment.AuthorSystemRole;
                AuthorParticipantType = comment.AuthorParticipantType;
                AuthorParticipantRole = comment.AuthorParticipantRole;
                AuthorAssignmentOrder = comment.AuthorAssignmentOrder?.ToString() ?? string.Empty;
                CommentText = comment.CommentText;
                CommentCreationDate = ViewHelpers.FormatDateTime2(comment.CommentCreationDate);
                AuthorIsModerator = comment.AuthorIsModerator;
            }

            /// <summary>
            /// Gets or sets the first name of the author.
            /// </summary>
            /// <value>
            /// The first name of the author.
            /// </value>
            [JsonProperty(PropertyName = "authorFName")]
            public string AuthorFirstName { get; set; }

            /// <summary>
            /// Gets or sets the last name of the author.
            /// </summary>
            /// <value>
            /// The last name of the author.
            /// </value>
            [JsonProperty(PropertyName = "authorLName")]
            public string AuthorLastName { get; set; }

            /// <summary>
            /// Gets or sets the author system role.
            /// </summary>
            /// <value>
            /// The author system role.
            /// </value>
            [JsonProperty(PropertyName = "authorRole")]
            public string AuthorSystemRole { get; set; }

            /// <summary>
            /// Gets or sets the type of the author participant.
            /// </summary>
            /// <value>
            /// The type of the author participant.
            /// </value>
            [JsonProperty(PropertyName = "authorPartType")]
            public string AuthorParticipantType { get; set; }

            /// <summary>
            /// Gets or sets the author participant role.
            /// </summary>
            /// <value>
            /// The author participant role.
            /// </value>
            [JsonProperty(PropertyName = "authorPartRole")]
            public string AuthorParticipantRole { get; set; }

            /// <summary>
            /// Gets the formatted author name and role.
            /// </summary>
            /// <value>
            /// The formatted author name and role.
            /// </value>
            [JsonProperty(PropertyName = "authorNameAndRole")]
            public string FormattedAuthorNameAndRole
            {
                get
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(String.Format("{0} {1}", AuthorFirstName, AuthorLastName));
                    if (AuthorParticipantType != null)
                    {
                        sb.Append(String.Format(" {0} {1}", AuthorParticipantType, AuthorAssignmentOrder));
                    }
                    if (AuthorParticipantRole != null)
                    {
                        sb.Append(String.Format(" - {0}", AuthorParticipantRole));
                    }
                    return sb.ToString();
                }
            }

            /// <summary>
            /// Gets or sets the author assignment order.
            /// </summary>
            /// <value>
            /// The author assignment order.
            /// </value>
            [JsonProperty(PropertyName = "authorOrderNo")]
            public string AuthorAssignmentOrder { get; set; }

            /// <summary>
            /// Gets or sets the comment text.
            /// </summary>
            /// <value>
            /// The comment text.
            /// </value>
            [JsonProperty(PropertyName = "comment")]
            public string CommentText { get; set; }

            /// <summary>
            /// Gets or sets the comment creation date.
            /// </summary>
            /// <value>
            /// The comment creation date.
            /// </value>
            [JsonProperty(PropertyName = "authorDate")]
            public string CommentCreationDate { get; set; }
            /// <summary>
            /// Returns the comment creation date in standard P2RMIS format.
            /// </summary>
            /// <value>
            /// The comment creation date.
            /// </value>
            [JsonProperty(PropertyName = "formatedAuthorDate")]
            public string FormattedCreationDate { get { return CommentCreationDate; } }
            /// <summary>
            /// Gets or sets a value indicating whether the [author is  a moderator].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [author is moderator]; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "authorModerator")]
            public bool AuthorIsModerator { get; set; }
        }

        /// <summary>
        /// Discussion participant
        /// </summary>
        public class DiscussionParticipant
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DiscussionParticipant"/> class.
            /// </summary>
            /// <param name="participant">The participant.</param>
            public DiscussionParticipant(IDiscussionParticipantModel participant)
            {
                FirstName = participant.FirstName;
                LastName = participant.LastName;
                ParticipantType = participant.ParticipantType;
                AssignmentOrder = participant.AssignmentOrder;
                ParticipantRole = participant.ParticipantRole;
                PhoneNumber = participant.PhoneNumber;
                IsModerator = participant.IsModerator;
                IsChair = participant.IsChair;
            }

            /// <summary>
            /// Gets or sets the first name.
            /// </summary>
            /// <value>
            /// The first name.
            /// </value>
            [JsonProperty(PropertyName = "authorFName")]
            public string FirstName { get; set; }

            /// <summary>
            /// Gets or sets the last name.
            /// </summary>
            /// <value>
            /// The last name.
            /// </value>
            [JsonProperty(PropertyName = "authorLName")]
            public string LastName { get; set; }

            /// <summary>
            /// Gets or sets the type of the participant.
            /// </summary>
            /// <value>
            /// The type of the participant.
            /// </value>
            [JsonProperty(PropertyName = "authorPartType")]
            public string ParticipantType { get; set; }

            /// <summary>
            /// Gets or sets the assignment order.
            /// </summary>
            /// <value>
            /// The assignment order.
            /// </value>
            [JsonProperty(PropertyName = "authorOrderNo")]
            public int? AssignmentOrder { get; set; }

            /// <summary>
            /// Gets or sets the participant role.
            /// </summary>
            /// <value>
            /// The participant role.
            /// </value>
            [JsonProperty(PropertyName = "authorRole")]
            public string ParticipantRole { get; set; }

            /// <summary>
            /// Gets or sets the phone number.
            /// </summary>
            /// <value>
            /// The phone number.
            /// </value>
            [JsonProperty(PropertyName = "authorPhone")]
            public string PhoneNumber { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this instance is moderator.
            /// </summary>
            /// <value>
            /// <c>true</c> if this instance is moderator; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "authorModerator")]
            public bool IsModerator { get; set; }
            
            /// <summary>
            /// Gets or sets a value indicating whether this instance is a chairperson.
            /// </summary>
            /// <value>
            /// <c>true</c> if this instance is chairperson; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty(PropertyName = "chairPerson")]
            public bool IsChair { get; set; }
        }
    }
}