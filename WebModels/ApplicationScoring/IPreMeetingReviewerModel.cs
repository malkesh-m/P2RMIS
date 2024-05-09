

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    public interface IPreMeetingReviewerModel
    {
        /// <summary>
        /// The reviewers first name
        /// </summary>
        string ReviewerFirstName { get; set; }
        /// <summary>
        /// The reviewers last name
        /// </summary>
        string ReviewerLastName { get; set; }
        /// <summary>
        /// The reviewers assignment type
        /// </summary>
        string AssignmentType { get; set; }
        /// <summary>
        /// The application log identifer
        /// </summary>
        int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// The panel abbreviation
        /// </summary>
        int? AssignmentOrder { get; set; }

    }
}
