namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for Panel Reviewer Application Assignment in PanelManagement
    /// </summary>
    public class PanelReviewerApplicationAssignmentInformation : IPanelReviewerApplicationAssignmentInformation
    {
        /// <summary>
        /// The client assignment type identifier
        /// </summary>
        public int ClientAssignmentTypeId { get; set; }
        /// <summary>
        /// The client assignment type abbreviation
        /// </summary>
        public string AssignmentTypeAbbreviation { get; set; }
        /// <summary>
        /// The reviewer's position in the review presentation order
        /// </summary>
        public int ReviewerPresentationPosition { get; set; }
        /// <summary>
        /// Does the reviewer have a COI?
        /// </summary>
        public bool IsCoi { get; set; }
        /// <summary>
        /// The Panel application reviewer expertise rating identifier for this reviewer
        /// </summary>
        public int PanelApplicationReviewerExpertiseRatingId { get; set; }
        /// <summary>
        /// The Client's experience rating identifier
        /// </summary>
        public int ClientExperienceRatingId { get; set; }
        /// <summary>
        /// The client's experience rating abbreviation
        /// </summary>
        public string RatingAbbreviation { get; set; }
        /// <summary>
        /// The Panel application reviewer coi detail identifier for this reviewer
        /// </summary>
        public int? PanelApplicationReviewerCoiDetailId { get; set; }
        /// <summary>
        /// The client coi type identifier associated with the reviewer's coi
        /// </summary>
        public int? ClientCoiTypeId { get; set; }
        /// <summary>
        /// The name of the coi type
        /// </summary>
        public string CoiTypeName { get; set; }
        /// <summary>
        /// Comments associated with this reviewer's assignment
        /// </summary>
        public string Comments { get; set; }
    }
}
