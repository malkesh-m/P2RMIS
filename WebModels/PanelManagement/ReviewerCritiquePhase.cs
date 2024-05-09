using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Reviewer and critique general information 
    /// </summary>
    public class ReviewerCritiquePhase : IReviewerCritiquePhase
    {
        /// <summary>
        /// Last name of the reviewer
        /// </summary>
        public string ReviewerLastName { get; set; }
        /// <summary>
        /// First name of reviewer
        /// </summary>
        public string ReviewerFirstName { get; set; }
        /// <summary>
        /// Reviewer identifier
        /// </summary>
        public int ReviewerId { get; set; }
        /// <summary>
        /// Client abbreviation for reviewer's assignment type
        /// </summary>
        public string AssignmentAbbreviation { get; set; }
        /// <summary>
        /// Client description for reviewer's assignment type
        /// </summary>
        public string AssignmentDescription { get; set; }
        /// <summary>
        /// Order in which reviewer is assigned to present
        /// </summary>
        public int AssignmentOrder { get; set; }
        /// <summary>
        /// Reviewer's primary email address
        /// </summary>
        public string ReviewerEmailAddress { get; set; }
        /// <summary>
        /// Collection of critique phases information
        /// </summary>
        public IEnumerable<ICritiquePhaseInformation> CritiquePhases { get; set; }
        /// <summary>
        /// Indicates if the assignment is a COI
        /// </summary>
        public bool IsCoi { get; set; }
        /// <summary>
        /// Indicates if the assignment is a Reader
        /// </summary>
        public bool IsReader { get; set; }
    }
}
