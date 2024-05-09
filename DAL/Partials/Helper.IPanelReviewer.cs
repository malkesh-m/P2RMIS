using System;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Helper methods for IPanelReviewer entities.
    /// </summary>
    public partial class Helper
    {
        /// <summary>
        /// Populate common properties in IPanelReviewer entities
        /// </summary>
        /// <param name="entity">IPanelReviewer entity</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">Reviewer User entity identifier</param>
        /// <param name="clinetRoleId">ClientRole entity identifier</param>
        /// <param name="clientApprovalFlag">Client approval state</param>
        /// <param name="restrictedAssignedFlag">Restricted access state</param>
        /// <param name="clientApprovedBy">User entity identifier who approved the reviewer for the client</param>
        /// <param name="clientApprovalDate">Date/Time reviewer was approved by the client</param>
        public static void Populate(IPanelReviewer entity, int sessionPanelId, int userId,
                                    int? clientRoleId, bool? clientApprovalFlag, bool restrictedAssignedFlag,
                                    int? clientApprovedBy,  DateTime? clientApprovalDate)
        {
            entity.SessionPanelId = sessionPanelId;
            entity.UserId = userId;

            entity.ClientRoleId = clientRoleId;
            entity.ClientApprovalFlag = clientApprovalFlag;
            entity.RestrictedAssignedFlag = restrictedAssignedFlag;

            entity.ClientApprovalBy = clientApprovedBy;
            entity.ClientApprovalDate = clientApprovalDate;
        }
    }
}
