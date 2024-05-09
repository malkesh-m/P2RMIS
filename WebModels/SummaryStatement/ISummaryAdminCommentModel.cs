using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Data model for the administrative comments for a specific application.
    /// </summary>
    public interface ISummaryAdminCommentModel
    {
        /// <summary>
        /// CommentID unique identifier
        /// </summary>
        int CommentID { get; set; }
        /// <summary>
        /// AppID identifier
        /// </summary>
        string AppID { get; set; }
        /// <summary
        /// First name of most recent user to affect contents of the comment
        /// </summary>
        string FirstName { get; set; }
        /// <summary
        /// Last name of most recent user to affect contents of the comment
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// Date of most recent action affecting the contents of the comment
        /// source table, UserApplicationComment, allows null for date values
        /// </summary>
        DateTime? CommentDate { get; set; }
        /// <summary>
        /// Text Of Comment
        /// </summary>
        string Comment { get; set; }
    }
}
