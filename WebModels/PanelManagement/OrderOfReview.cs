using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for OrderOfReview tab in PanelManagement
    /// </summary>
    /// <remarks>Types are only my initial estimation.  Subject to the realities of the database</remarks>
    public class OrderOfReview : IOrderOfReview
    {
        /// <summary>
        /// Review order
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// Indicates if the application is triaged
        /// </summary>
        public bool IsTriaged { get; set; }
        /// <summary>
        /// Applications log number
        /// </summary>
        public string LogNumber { get; set; }
        /// <summary>
        /// PI's First Name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// PI's Last Name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// the application's Award Mechanism (abbreviation)
        /// </summary>        
        public string AwardMechanism { get; set; }
        /// <summary>
        /// the application's reviewers with presentation order
        /// </summary>        
        public IEnumerable<IApplicationPanelReviewers> ApplicationReviewers { get; set; }
        /// <summary>
        /// Participate type ordered by presentation order
        /// </summary>
        public IEnumerable<string> PartType { get; set; }
        /// <summary>
        /// List of COI's first name
        /// </summary>
        public IEnumerable<string> CoiFirstName { get; set; }
        /// <summary>
        /// List of COI's last name
        /// </summary>
        public IEnumerable<string> CoiLastName { get; set; }
        /// <summary>
        /// Pre Meeting Scores ordered by presentation order
        /// </summary>
        public IEnumerable<IReviewerScoreModel> PreMeetingScores { get; set; }
    }
}
