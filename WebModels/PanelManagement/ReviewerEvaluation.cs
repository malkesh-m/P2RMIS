namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// web model for reviewer evaluation data
    /// </summary>
    public class ReviewerEvaluation : IReviewerEvaluation
    {
        /// <summary>
        /// the reviewer evaluation id
        /// </summary>
        public int? ReviewerEvaluationId { get; set; }
        /// <summary>
        /// the users panel assignment id
        /// </summary>
        public int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// the reviewers first name
        /// </summary>
        public string ReviewerFirstName { get; set; }
        /// <summary>
        /// the reviewers last name
        /// </summary>
        public string ReviewerLastName { get; set; }
        /// <summary>
        /// the reviewers participant type name
        /// </summary>
        public string ParticipantTypeName { get; set; }
        /// <summary>
        /// determines whether the reviewer is already a chair
        /// </summary>
        public bool? ChairFlag { get; set; }
        /// <summary>
        /// the reviewers rating
        /// </summary>
        public int? Rating { get; set; }
        /// <summary>
        /// whether or not the rater thinks the reviewer can be a chair or not
        /// </summary>
        public bool? PotentialChairFlag { get; set; }
        /// <summary>
        /// the rating comments
        /// </summary>
        public string RatingComments { get; set; }
        /// <summary>
        /// whether or not the reviewer is a consumer or not
        /// </summary>
        public bool ConsumerFlag { get; set; }
        /// <summary>
        /// the panel name
        /// </summary>
        public string PanelName { get; set; }
    }
}
