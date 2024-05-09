using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for getting the contents of a critique from the database.
    /// </summary>
    public class ApplicationCritiqueDetailsModel : IApplicationCritiqueDetailsModel
    {
        /// <summary>
        /// The Meeting description
        /// </summary>
        public string MeetingDescription { get; set; }
        /// <summary>
        /// The program year
        /// </summary>
        public string ProgramYear { get; set; }
        /// <summary>
        /// The Award mechanism title
        /// </summary>
        public string AwardTitle { get; set; }
        /// <summary>
        /// Reviewer's last name
        /// </summary>
        public string ReviewerLastName { get; set; }
        /// <summary>
        /// Reviewer's first name
        /// </summary>
        public string ReviewerFirstName { get; set; }
        /// <summary>
        /// PI last name
        /// </summary>
        public string PiLastName { get; set; }
        /// <summary>
        /// Application LogNumber
        /// </summary>
        public string ApplicationLogNumber { get; set; }
        /// <summary>
        /// Application Title
        /// </summary>
        public string ApplicationTitle { get; set; }
        /// <summary>
        /// Is the critique submitted
        /// </summary>
        public bool IsSubmitted { get; set; }
        /// <summary>
        /// Numeric overall rating provided by reviewer
        /// </summary>
        public decimal ScoreRating { get; set; }
        /// <summary>
        /// Adjectival overall rating provided by reviewer (if exists)
        /// </summary>
        public string AdjectivalRating { get; set; }
        /// <summary>
        /// The type of scoring used for the application
        /// </summary>
        public string ScoreType { get; set; }
        /// <summary>
        /// delegate used to format the score
        /// </summary>
        public static ScoreFormatter ScoreFormatter { get; set; }
        /// <summary>
        /// String representation of a formatted score for presentation
        /// </summary>
        public string FormattedScore
        {
            get { return ScoreFormatter(ScoreRating, ScoreType, AdjectivalRating, IsSubmitted); }
        }
        /// <summary>
        /// A list of the contents and instructions for each section of the critique
        /// </summary>
        public IEnumerable<ICritiqueSection> ReviewerCritiques { get; set; }
    }
}
