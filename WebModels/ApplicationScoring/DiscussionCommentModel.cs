using System;


namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Model representing a comment within a discussion
    /// </summary>
    public interface IDiscussionCommentModel
    {
        /// <summary>
        /// Gets or sets the first name of the author.
        /// </summary>
        /// <value>
        /// The first name of the author.
        /// </value>
        string AuthorFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the author.
        /// </summary>
        /// <value>
        /// The last name of the author.
        /// </value>
        string AuthorLastName { get; set; }

        /// <summary>
        /// Gets or sets the author system role.
        /// </summary>
        /// <value>
        /// The author system role.
        /// </value>
        string AuthorSystemRole { get; set; }

        /// <summary>
        /// Gets or sets the type of the author participant.
        /// </summary>
        /// <value>
        /// The type of the author participant.
        /// </value>
        string AuthorParticipantType { get; set; }

        /// <summary>
        /// Gets or sets the author participant role.
        /// </summary>
        /// <value>
        /// The author participant role.
        /// </value>
        string AuthorParticipantRole { get; set; }

        /// <summary>
        /// Gets or sets the author assignment order.
        /// </summary>
        /// <value>
        /// The author assignment order.
        /// </value>
        int? AuthorAssignmentOrder { get; set; }

        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        /// <value>
        /// The comment text.
        /// </value>
        string CommentText { get; set; }

        /// <summary>
        /// Gets or sets the comment creation date.
        /// </summary>
        /// <value>
        /// The comment creation date.
        /// </value>
        DateTime? CommentCreationDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the [author is  a moderator].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [author is moderator]; otherwise, <c>false</c>.
        /// </value>
        bool AuthorIsModerator { get; set; }
    }

    /// <summary>
    /// Model representing a comment within a discussion
    /// </summary>
    public class DiscussionCommentModel : IDiscussionCommentModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscussionCommentModel"/> class.
        /// </summary>
        /// <param name="authorFirstName">First name of the author.</param>
        /// <param name="authorLastName">Last name of the author.</param>
        /// <param name="authorSystemRole">The author system role.</param>
        /// <param name="authorParticipantType">Type of the author participant.</param>
        /// <param name="authorParticipantRole">The author participant role.</param>
        /// <param name="authorAssignmentOrder">The author assignment order.</param>
        /// <param name="commentText">The comment text.</param>
        /// <param name="commentCreationDate">The comment creation date.</param>
        /// <param name="authorIsModerator">if set to <c>true</c> [author is moderator].</param>
        public DiscussionCommentModel(string authorFirstName, string authorLastName, string authorSystemRole,
            string authorParticipantType, string authorParticipantRole, int? authorAssignmentOrder, string commentText,
            DateTime? commentCreationDate, bool authorIsModerator)
        {
            AuthorFirstName = authorFirstName;
            AuthorLastName = authorLastName;
            AuthorSystemRole = authorSystemRole;
            AuthorParticipantType = authorParticipantType;
            AuthorParticipantRole = authorParticipantRole;
            AuthorAssignmentOrder = authorAssignmentOrder;
            CommentText = commentText;
            CommentCreationDate = commentCreationDate;
            AuthorIsModerator = authorIsModerator;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the first name of the author.
        /// </summary>
        /// <value>
        /// The first name of the author.
        /// </value>
        public string AuthorFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the author.
        /// </summary>
        /// <value>
        /// The last name of the author.
        /// </value>
        public string AuthorLastName { get; set; }

        /// <summary>
        /// Gets or sets the author system role.
        /// </summary>
        /// <value>
        /// The author system role.
        /// </value>
        public string AuthorSystemRole { get; set; }

        /// <summary>
        /// Gets or sets the type of the author participant.
        /// </summary>
        /// <value>
        /// The type of the author participant.
        /// </value>
        public string AuthorParticipantType { get; set; }

        /// <summary>
        /// Gets or sets the author participant role.
        /// </summary>
        /// <value>
        /// The author participant role.
        /// </value>
        public string AuthorParticipantRole { get; set; }

        /// <summary>
        /// Gets or sets the author assignment order.
        /// </summary>
        /// <value>
        /// The author assignment order.
        /// </value>
        public int? AuthorAssignmentOrder { get; set; }

        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        /// <value>
        /// The comment text.
        /// </value>
        public string CommentText { get; set; }

        /// <summary>
        /// Gets or sets the comment creation date.
        /// </summary>
        /// <value>
        /// The comment creation date.
        /// </value>
        public DateTime? CommentCreationDate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the [author is  a moderator].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [author is moderator]; otherwise, <c>false</c>.
        /// </value>
        public bool AuthorIsModerator { get; set; }
        #endregion
    }
}
