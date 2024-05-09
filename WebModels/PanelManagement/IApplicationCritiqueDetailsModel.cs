
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for getting the contents of a critique from the database.
    /// </summary>
    public interface IApplicationCritiqueDetailsModel
    {
        /// <summary>
        /// The Meeting description
        /// </summary>
        string MeetingDescription { get; set; }
        /// <summary>
        /// The program year
        /// </summary>
        string ProgramYear { get; set; }
        /// <summary>
        /// The Award mechanism title
        /// </summary>
        string AwardTitle { get; set; }
        /// <summary>
        /// Reviewer's last name
        /// </summary>
        string ReviewerLastName { get; set; }
        /// <summary>
        /// Reviewer's first name
        /// </summary>
        string ReviewerFirstName { get; set; }
        /// <summary>
        /// PI last name
        /// </summary>
        string PiLastName { get; set; }
        /// <summary>
        /// Application LogNumber
        /// </summary>
        string ApplicationLogNumber { get; set; }
        /// <summary>
        /// Application Title
        /// </summary>
        string ApplicationTitle { get; set; }
        /// <summary>
        /// Is the critique submitted
        /// </summary>
        bool IsSubmitted { get; set; }
        /// <summary>
        /// Numeric overall rating provided by reviewer
        /// </summary>
        decimal ScoreRating { get; set; }
        /// <summary>
        /// Adjectival overall rating provided by reviewer (if exists)
        /// </summary>
        string AdjectivalRating { get; set; }
        /// <summary>
        /// The type of scoring used for the application
        /// </summary>
        string ScoreType { get; set; }
        /// <summary>
        /// String representation of a formatted score for presentation
        /// </summary>
        string FormattedScore { get; }
        /// <summary>
        /// A list of the contents and instructions for each section of the critique
        /// </summary>
        IEnumerable<ICritiqueSection> ReviewerCritiques { get; set; }
    }
}
