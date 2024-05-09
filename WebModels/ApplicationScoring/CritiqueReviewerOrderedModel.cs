

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Interface for the critique reviewer order
    /// </summary>
    public interface ICritiqueReviewerOrderedModel
    {
        /// <summary>
        /// The Panel application reviewer assignment identifier
        /// </summary>
        int PanelApplicationReviewerAssignmentId { get; set; }
        /// <summary>
        /// The reviewers user identifier
        /// </summary>
        int ReviewerId { get; set; }
        /// <summary>
        /// The reviewer's first name
        /// </summary>
        string ReviewerFirstName { get; set; }
        /// <summary>
        /// The reviewer's last name
        /// </summary>
        string ReviewerLastName { get; set; }
        /// <summary>
        /// The client assignment type abbreviation
        /// </summary>
        string clientAssignmentTypeAbbreviation { get; set; }
    }
    /// <summary>
    /// Model object containing the critique reviewer order
    /// </summary>
    public class CritiqueReviewerOrderedModel : ICritiqueReviewerOrderedModel
    {
        /// <summary>
        /// The Panel application reviewer assignment identifier
        /// </summary>
        public int PanelApplicationReviewerAssignmentId { get; set; }
        /// <summary>
        /// The reviewers user identifier
        /// </summary>
        public int ReviewerId { get; set; }
        /// <summary>
        /// The reviewer's first name
        /// </summary>
        public string ReviewerFirstName { get; set; }
        /// <summary>
        /// The reviewer's last name
        /// </summary>
        public string ReviewerLastName { get; set; }
        /// <summary>
        /// The client assignment type abbreviation
        /// </summary>
        public string clientAssignmentTypeAbbreviation { get; set; }
    }
}
