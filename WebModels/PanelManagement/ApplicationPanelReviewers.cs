namespace Sra.P2rmis.WebModels.PanelManagement
{
    public class ApplicationPanelReviewers : IApplicationPanelReviewers
    {
        /// <summary>
        /// Reviewer Order
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// ReviewerInitial
        /// </summary>
        public string ReviewerFirstName { get; set; }
        /// <summary>
        /// Reviewer last name
        /// </summary>
        public string ReviewerLastName { get; set; }
        /// <summary>
        /// Panel user assignment identifier
        /// </summary>
        public int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Panel user role identifier
        /// </summary>
        public int? PartRoleId { get; set; }
        /// <summary>
        /// Panel user role identifier
        /// </summary>
        public string ParticipantRoleName { get; set; }
        /// <summary>
        /// Panel user assignment
        /// </summary>
        public string ParticipantAssignment { get; set; }
       

    }

}
