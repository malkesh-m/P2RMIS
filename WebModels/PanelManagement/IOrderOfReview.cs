using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for OrderOfReview tab in PanelManagement
    /// </summary>
    public interface IOrderOfReview
    {
        /// <summary>
        /// Review order
        /// </summary>
        string Order { get; set; }
        /// <summary>
        /// Indicates if the application is triaged
        /// </summary>
        bool IsTriaged { get; set; }
        /// <summary>
        /// Applications log number
        /// </summary>
        string LogNumber { get; set; }
        /// <summary>
        /// PI's First Name
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// PI's Last Name
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// the application's Award Mechanism (abbreviation)
        /// </summary>        
        string AwardMechanism { get; set; }
        /// <summary>
        /// the application's reviewers with presentation order
        /// </summary>        
        IEnumerable<IApplicationPanelReviewers> ApplicationReviewers { get; set; }
        /// <summary>
        /// List of Participate type ordered by presentation order
        /// </summary>
        IEnumerable<string> PartType { get; set; }
        /// <summary>
        /// List of COI's first name
        /// </summary>
        IEnumerable<string> CoiFirstName { get; set; }
        /// <summary>
        /// List of COI's last name
        /// </summary>
        IEnumerable<string> CoiLastName { get; set; }
        /// <summary>
        /// Pre Meeting Scores ordered by presentation order
        /// </summary>
        IEnumerable<IReviewerScoreModel> PreMeetingScores { get; set; }
    }
}
