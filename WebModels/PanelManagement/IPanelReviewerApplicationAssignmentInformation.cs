namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for Panel Reviewer Application Assignment in PanelManagement
    /// </summary>
    public interface IPanelReviewerApplicationAssignmentInformation
    {
        /// <summary>
        /// The client assignment type identifier
        /// </summary>
        int ClientAssignmentTypeId { get; set; }
        /// <summary>
        /// The client assignment type abbreviation
        /// </summary>
        string AssignmentTypeAbbreviation { get; set; }
        /// <summary>
        /// The reviewer's position in the review presentation order
        /// </summary>
        int ReviewerPresentationPosition { get; set; }
        /// <summary>
        /// Does the reviewer have a COI?
        /// </summary>
        bool IsCoi { get; set; }
        /// <summary>
        /// The Panel application reviewer expertise rating identifier for this reviewer
        /// </summary>
        int PanelApplicationReviewerExpertiseRatingId { get; set; }
        /// <summary>
        /// The Client's experience rating identifier
        /// </summary>
        int ClientExperienceRatingId { get; set; }
        /// <summary>
        /// The client's experience rating abbreviation
        /// </summary>
        string RatingAbbreviation { get; set; }
        /// <summary>
        /// The Panel application reviewer coi detail identifier for this reviewer
        /// </summary>
        int? PanelApplicationReviewerCoiDetailId { get; set; }
        /// <summary>
        /// The client coi type identifier associated with the reviewer's coi
        /// </summary>
        int? ClientCoiTypeId { get; set; }
        /// <summary>
        /// The name of the coi type
        /// </summary>
        string CoiTypeName { get; set; }
        /// <summary>
        /// Comments associated with this reviewer's assignment
        /// </summary>
        string Comments { get; set; }
    }
}
