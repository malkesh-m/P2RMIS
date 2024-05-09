namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// web model for reviewer evaluation data
    /// </summary>
    public interface IReviewerEvaluation
    {
        /// <summary>
        /// the reviewer evaluation id
        /// </summary>
        int? ReviewerEvaluationId { get; set; }
        /// <summary>
        /// the users panel assignment id
        /// </summary>
        int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// the reviewers first name
        /// </summary>
        string ReviewerFirstName { get; set; }
        /// <summary>
        /// the reviewers last name
        /// </summary>
        string ReviewerLastName { get; set; }
        /// <summary>
        /// the reviewers participant type name
        /// </summary>
        string ParticipantTypeName { get; set; }
        /// <summary>
        /// determines whether the reviewer is already a chair
        /// </summary>
        bool? ChairFlag { get; set; }
        /// <summary>
        /// the reviewers rating
        /// </summary>
        int? Rating { get; set; }
        /// <summary>
        /// whether or not the rater thinks the reviewer can be a chair or not
        /// </summary>
        bool? PotentialChairFlag { get; set; }
        /// <summary>
        /// the rating comments
        /// </summary>
        string RatingComments { get; set; }
        /// <summary>
        /// whether or not the reviewer is a consumer or not
        /// </summary>
        bool ConsumerFlag { get; set; }
        /// <summary>
        /// the panel name
        /// </summary>
        string PanelName { get; set; }
    }
}
