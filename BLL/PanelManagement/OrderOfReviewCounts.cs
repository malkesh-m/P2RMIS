
namespace Sra.P2rmis.Bll.PanelManagement
{
    /// <summary>
    /// Individual reviewer's presentation order counts
    /// </summary>
    public class OrderOfReviewCounts
    {
        /// <summary>
        /// Reviewer's user identifier
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Number of times this reviewer has been first presenter
        /// </summary>
        public int FirstReviewerCount { get; set; }
        /// <summary>
        /// Number of times this reviewer has been all other presenters
        /// </summary>
        public int SecondReviewerCount { get; set; }
        /// <summary>
        /// Number of times this reviewer has been a presenter in any position.
        /// </summary>
        public int AllPositionsCount { get; set; }
        /// <summary>
        /// Reviewer's first name
        /// </summary>
        public string ReviewerFirstName { get; set; }
        /// <summary>
        /// Reviewer's last name
        /// </summary>
        public string ReviewerLastName { get; set; }
        /// <summary>
        /// Whether the reviewer is a scientist
        /// </summary>
        public bool ScientistFlag { get; set; }
        /// <summary>
        /// Whether the reviewer is a specialist
        /// </summary>
        public bool SpecialistFlag { get; set; }
        /// <summary>
        /// Whether the reviewer is a consumer
        /// </summary>
        public bool ConsumerFlag { get; set; }
    }
}
