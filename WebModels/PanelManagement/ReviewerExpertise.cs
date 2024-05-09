
namespace Sra.P2rmis.WebModels.PanelManagement
{
    public delegate string ExpertiseFormatter(string rating);
    public delegate string ReviewerColorFormatter(bool scientistFlag, bool specialistFlag, bool consumerFlag);
    /// <summary>
    /// Data model for reviewer's expertise.
    /// </summary>
    public class ReviewerExpertise : IReviewerExpertise
    {
        /// <summary>
        /// Unique identifier for the application
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Applications log number
        /// </summary>
        public string LogNumber { get; set; }
        /// <summary>
        /// Award Abbreviation
        /// </summary>
        public string AwardAbbrev { get; set; }
        /// <summary>
        /// Reviewer's first name
        /// </summary>
        public string ReviewerLastName { get; set; }
        /// <summary>
        /// Reviewer's last name
        /// </summary>
        public string ReviewerFirstName { get; set; }
        /// <summary>
        /// User assignment identifier
        /// </summary>
        public int ParticipantId { get; set; }
        /// <summary>
        /// Participant type
        /// </summary>
        public string ParticipantType { get; set; }
        /// <summary>
        /// Participant type identifier
        /// </summary>
        public int ParticipantTypeId { get; set; }
        /// <summary>
        /// Participant type name
        /// </summary>
        public string ParticipantTypeName { get; set; }
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
        /// <summary>
        /// Whether the reviewer is an elevated chairperson
        /// </summary>
        public bool ElevatedChairpersonFlag { get; set; }
        /// <summary>
        /// Client role identifier
        /// </summary>
        public int ClientRoleId { get; set; }
        /// <summary>
        /// Expertise rating, such as High, Medium, Low, COI, or empty
        /// </summary>
        public string Rating { get; set; }
        /// <summary>
        /// Expertise rating identifier
        /// </summary>
        public int RatingId { get; set; }
        /// <summary>
        /// the ratings color
        /// </summary>
        public static ExpertiseFormatter RatingColor { get; set; }
        /// <summary>
        /// the formatted reviewer expertise rating color
        /// </summary>
        public string FormattedExpertise
        {
            get { return (RatingColor != null) ? RatingColor(Rating) : RatingColor.ToString(); }
        }
        /// <summary>
        /// the reviewers view color
        /// </summary>
        public static ReviewerColorFormatter ReviewerColor { get; set; }
        /// <summary>
        /// the formatted reviewer color in the view
        /// </summary>
        public string FormattedReviewerColor
        {
            get { return (ReviewerColor != null) ? ReviewerColor(ScientistFlag, SpecialistFlag, ConsumerFlag) : ReviewerColor.ToString(); }
        }
        /// <summary>
        /// Reviewer's review order on the Application
        /// </summary>
        public int? ReviewOrder { get; set; }
        /// <summary>
        /// the reviewers participation role abbreviation
        /// </summary>
        public string ParticipationRoleAbbrev { get; set; }
        /// <summary>
        /// Reviewer's user identifier
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Numeric value of the review order.  Useful for calculation.
        /// </summary>
        public int ReviewOrderValue { get { return (ReviewOrder.HasValue) ? ReviewOrder.Value : 0; } }
        /// <summary>
        /// Panel application identifier
        /// </summary>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// the assignment to the panel application
        /// </summary>
        public int? AssignmentTypeId { get; set; }

        /// <summary>
        /// Whether the current logged in user is a COI on the application
        /// </summary>
        public bool IsCurrentUserCoi { get; set; }
        /// <summary>
        /// Reviewer's expertise list
        /// </summary>
        public string ReviewerExpertiseText { get; set; }

    }
}
