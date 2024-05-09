
namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// The PremeetingRviewerModel object
    /// </summary>
    public class PreMeetingReviewerModel : IPreMeetingReviewerModel
    {
        /// <summary>
        /// The reviewers first name
        /// </summary>
        public string ReviewerFirstName { get; set; }
        /// <summary>
        /// The reviewers last name
        /// </summary>
        public string ReviewerLastName { get; set; }
        /// <summary>
        /// The reviewers assignment type
        /// </summary>
        public string AssignmentType { get; set; }
        /// <summary>
        /// The application log identifer
        /// </summary>
        public int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// The panel abbreviation
        /// </summary>
        public int? AssignmentOrder { get; set; }
    }
}

