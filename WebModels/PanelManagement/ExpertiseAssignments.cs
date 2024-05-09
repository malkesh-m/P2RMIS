
namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the GetExpertiseAssignments() repository helper.
    /// This populates the reviewer assignment-update modal window on the Expertise/Assignments tab.
    /// </summary>
    public class ExpertiseAssignments : IExpertiseAssignments
    {
        /// <summary>
        /// SessionPanel identifier
        /// </summary>
        public int SessionPanelId { get; set; }
        /// <summary>
        /// Panel application identifier
        /// </summary>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// PanelApplicationReviewerExpertise identifier
        /// </summary>
        public int PanelApplicationReviewerExpertiseId { get; set; }
        /// <summary>
        /// 1 Assignment type abbreviation (Assignment drop down)
        /// </summary>
        public int? ClientParticipantTypeId { get; set; }
        /// <summary>
        /// 2 Presentation order  (Sort order drop down)
        /// </summary>
        public int? SortOrder { get; set; }
        /// <summary>
        /// Client expertise rating identifier.  (Expertise drop down)
        /// </summary>
        public int? ClientExpertiseRatingId { get; set; }
        /// <summary>
        /// 4 COI type identifier  (COI Type drop down)
        /// </summary>
        public int? CoiTypelId { get; set; }
        /// <summary>
        ///  5 Expertise comments  (Comments text box)
        /// </summary>
        public string ExpertiseComments { get; set; }
        /// <summary>
        ///  Client assignment type identifier
        /// </summary>
        public int? ClientAssignmentTypeId { get; set; }
        /// <summary>
        /// the main assignment type id
        /// </summary>
        public int? AssignmentTypeId { get; set; }
    }
}
