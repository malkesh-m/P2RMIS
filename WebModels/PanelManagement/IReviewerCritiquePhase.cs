using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Reviewer and critique general information 
    /// </summary>
    public interface IReviewerCritiquePhase
    {
        /// <summary>
        /// Last name of the reviewer
        /// </summary>
        string ReviewerLastName { get; set; }

        /// <summary>
        /// First name of reviewer
        /// </summary>
        string ReviewerFirstName { get; set; }

        /// <summary>
        /// Reviewer identifier
        /// </summary>
        int ReviewerId { get; set; }

        /// <summary>
        /// Client abbreviation for reviewer's assignment type
        /// </summary>
        string AssignmentAbbreviation { get; set; }

        /// <summary>
        /// Client description for reviewer's assignment type
        /// </summary>
        string AssignmentDescription { get; set; }

        /// <summary>
        /// Order in which reviewer is assigned to present
        /// </summary>
        int AssignmentOrder { get; set; }

        /// <summary>
        /// Reviewer's primary email address
        /// </summary>
        string ReviewerEmailAddress { get; set; }

        /// <summary>
        /// Collection of critique phases information
        /// </summary>
        IEnumerable<ICritiquePhaseInformation> CritiquePhases { get; set; }
        /// <summary>
        /// Indicates if the assignment is a COI
        /// </summary>
        bool IsCoi { get; set; }
        /// <summary>
        /// Indicates if the assignment is a Reader
        /// </summary>
        bool IsReader { get; set; }
    }
}