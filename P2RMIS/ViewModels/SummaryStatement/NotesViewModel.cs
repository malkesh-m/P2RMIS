using System.Collections.Generic;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Web.ViewModels.SummaryStatement
{
    /// <summary>
    /// View model for the summary statement notes model
    /// </summary>
    public class NotesViewModel
    {
        public NotesViewModel()
        {
            Notes = new List<ISummaryCommentModel>();
        }
        /// <summary>
        /// Collection of notes for the application
        /// </summary>
        public ICollection<ISummaryCommentModel> Notes { get; set; }
        /// <summary>
        /// Indicates if the user can add & edit comments.
        /// </summary>
        public bool CanAddEditComments { get; set; }
    }
}