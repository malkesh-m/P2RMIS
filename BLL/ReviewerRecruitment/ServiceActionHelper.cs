using Sra.P2rmis.Dal;
using System;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Bll.ReviewerRecruitment
{
    /// <summary>
    /// Helper methods for ServiceAction classes.
    /// </summary>
    public partial class ServiceActionHelper
    {
        /// <summary>
        /// Determine the RecruitedDate value based on the state of
        /// RecruitedFlag & RecruitedDate
        /// </summary>
        /// <param name="RecruitedFlag">Reviewer recruited state</param>
        /// <param name="recruitedDate">Reviewer recruited date/time</param>
        /// <param name="resolvedDate">Property setter for RecruitedDate</param>
        public static void ResolveRecruited(bool RecruitedFlag, DateTime? recruitedDate, Action<DateTime?> resolvedDate)
        {
            //
            // If RecruitedFlag is set to true & the RecruitedDate has not been
            // set then set the RecruitedDate to now.  (It can only be set once.)
            //
            if ((RecruitedFlag) && (!recruitedDate.HasValue))
            {
                resolvedDate(GlobalProperties.P2rmisDateTimeNow);
            }
        }
        /// <summary>
        /// Determine the ClientApproval ApprovalDate ^& ApprovedBy values
        /// based on the change in the ClientApprovalFlag value.
        /// </summary>
        /// <remarks>
        /// Used by PanelUserPotentialAssignmentServiceAction & PanelUserAssignment ServiceActions.
        /// </remarks>
        /// <param name="entity">IPanelReviewer (PanelUserAssignment or PanelUserPotentialAssignment) entity</param>
        /// <param name="clientApprovedBy">Property setter for ClientApprovedBy</param>
        /// <param name="clientApprovalDate">Property setter for ClientApprovalDate</param>
        /// <param name="clientApprovalFlag">ClientApproval state</param>
        /// <param name="userId">User entity identifier of user changing the ClientApproval</param>
        public static void ResolveClientApproval(IPanelReviewer entity, Action<int> clientApprovedBy, Action<DateTime?> clientApprovalDate, bool? clientApprovalFlag, int userId)
        {
            //
            // Case: (N/A --> T) & (N/T --> F)
            //
            if (!entity.ClientApprovalFlag.HasValue)
            {
                if (clientApprovalFlag.HasValue)
                {
                    clientApprovalDate(GlobalProperties.P2rmisDateToday);
                    clientApprovedBy(userId);
                }
            }
            //
            // Case: (T --> N/A) && (T --> F)
            //
            else if (entity.ClientApprovalFlag.Value)
            {
                if (
                    (!clientApprovalFlag.HasValue) ||
                    ((clientApprovalFlag.HasValue) && (!clientApprovalFlag.Value))
                    )
                {
                    clientApprovalDate(GlobalProperties.P2rmisDateToday);
                    clientApprovedBy(userId);
                }
            }
            //
            // Case: (F --> N/A) &&  (F --> T)
            //
            else
            {
                if (
                    (!clientApprovalFlag.HasValue) ||
                    ((clientApprovalFlag.HasValue) && (clientApprovalFlag.Value))
                    )
                {
                    clientApprovalDate(GlobalProperties.P2rmisDateToday);
                    clientApprovedBy(userId);
                }
            }
        }
    }
}
