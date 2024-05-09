namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the ListApplicationInformation requests.
    /// </summary>
    public interface IApplicationPanelReviewers
    {
        /// <summary>
        /// Reviewer Order
        /// </summary>
        int Order { get; set; }
        /// <summary>
        /// ReviewerInitial
        /// </summary>
        string ReviewerFirstName { get; set; }
        /// <summary>
        /// Reviewer last name
        /// </summary>
        string ReviewerLastName { get; set; }
        /// <summary>
        /// Panel user assignment identifier
        /// </summary>
        int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Panel user role identifier
        /// </summary>
        int? PartRoleId { get; set; }
        /// <summary>
        /// Panel user role name identifier
        /// </summary>
        string ParticipantRoleName { get; set; }
        /// <summary>
        /// Panel user assignment
        /// </summary>
        string ParticipantAssignment { get; set; }

    }
}
