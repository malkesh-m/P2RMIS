using System;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Data model for the administrative comments for a specific application.
    /// </summary>
    public class SummaryCommentModel : ISummaryCommentModel
    {
        /// <summary>
        /// CommentID unique identifier
        /// </summary>
        public int CommentID { get; set; }
        /// <summary>
        /// The identity of most recent user to affect contents of the comment
        /// </summary>
        public int UserId { get; set; }
        /// <summary
        /// First name of most recent user to affect contents of the comment
        /// </summary>
        public string FirstName { get; set; }
        /// <summary
        /// Last name of most recent user to affect contents of the comment
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Date of most recent action affecting the contents of the comment
        /// source table, UserApplicationComment, allows null for date values
        /// </summary>
        public DateTime? CommentDate { get; set; }
        /// <summary>
        /// Text Of Comment
        /// </summary>
        public string Comment { get; set; }
    }
}
