namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for reviewer's expertise
    /// </summary>
    public interface IReviewerExpertise
    {
        /// <summary>
        /// Unique identifier for the application
        /// </summary>
        int ApplicationId { get; set; }
        /// <summary>
        /// Applications log number
        /// </summary>
        string LogNumber { get; set; }
        /// <summary>
        /// Award Abbreviation
        /// </summary>
        string AwardAbbrev { get; set; }
        /// <summary>
        /// Reviewer's first name
        /// </summary>
        string ReviewerFirstName { get; set; }
        /// <summary>
        /// Reviewer's last name
        /// </summary>
        string ReviewerLastName { get; set; }
        /// <summary>
        /// User assignment identifier
        /// </summary>
        int ParticipantId { get; set; }
        /// <summary>
        /// Participant type
        /// </summary>
        string ParticipantType { get; set; }
        /// <summary>
        /// Participant type identifier
        /// </summary>
        int ParticipantTypeId { get; set; }
        /// <summary>
        /// Participant type name
        /// </summary>
        string ParticipantTypeName { get; set; }
        /// <summary>
        /// Whether the reviewer is a scientist
        /// </summary>
        bool ScientistFlag { get; set; }
        /// <summary>
        /// Whether the reviewer is a specialist
        /// </summary>
        bool SpecialistFlag { get; set; }
        /// <summary>
        /// Whether the reviewer is a consumer
        /// </summary>
        bool ConsumerFlag { get; set; }
        /// <summary>
        /// Whether the reviewer is an elevated chairperson
        /// </summary>
        bool ElevatedChairpersonFlag { get; set; }
        /// <summary>
        /// Client role identifier
        /// </summary>
        int ClientRoleId { get; set; }
        /// <summary>
        /// Expertise rating, such as High, Medium, Low, COI, or empty
        /// </summary>
        string Rating { get; set; }
        /// <summary>
        /// Expertise rating identifier
        /// </summary>
        int RatingId { get; set; }
        /// <summary>
        /// the formatted reviewer expertise rating color
        /// </summary>
        string FormattedExpertise { get; }
        /// <summary>
        /// the formatted reviewer color in the view
        /// </summary>
        string FormattedReviewerColor { get; }
        /// <summary>
        /// Reviewer's review order on the Application
        /// </summary>
        int? ReviewOrder { get; set; }
        /// the reviewers participation role abbreviation
        /// </summary>
        string ParticipationRoleAbbrev { get; set; }
        /// <summary>
        /// Reviewer's user identifier
        /// </summary>
        int UserId { get; set; }
        /// <summary>
        /// Numeric value of the review order.  Useful for calculation.
        /// </summary>
        int ReviewOrderValue { get; }
        /// <summary>
        /// Panel application identifier
        /// </summary>
        int PanelApplicationId { get; set; }
        /// <summary>
        /// the assignment to the panel application
        /// </summary>
        int? AssignmentTypeId { get; set; }
        /// <summary>
        /// Whether the current logged in user is a COI on the application
        /// </summary>
        bool IsCurrentUserCoi { get; set; }
        /// <summary>
        /// Reviewer's expertise list
        /// </summary>
        string ReviewerExpertiseText { get; set; }


    }
}
