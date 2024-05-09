using System.Linq;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.PanelManagement
{
    /// <summary>
    /// PanelManagement functionality for ApplicationManagement functions
    /// </summary>
    public partial class PanelManagementService
    {
        /// <summary>
        /// Mark a user as a COI.  This defaults several values & is primarily used by the Manage Application Scoring.
        /// </summary>
        /// <param name="panelApplicationId">The application identifier</param>
        /// <param name="panelUserAssignmentId">The panel user assignment</param>
        /// <param name="clientCoiTypeId">ClientCoiType entity identifier</param>
        /// <param name="comment">The comment associated with the assignment</param>
        /// <param name="userId">The user identifier making the assignment</param>
        public void AssignAsCoi(int panelApplicationId, int panelUserAssignmentId, int? clientCoiTypeId, string comment, int userId)
        {
            ValidateParametersAssignAsCoi(panelApplicationId, panelUserAssignmentId, comment, userId);
            //
            // We need the Client entity identifier.  We can get that through the PanelApplication.
            //
            PanelApplication p = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            int clientId = p.ClientId();
            //
            // Now that we have that we need the ClientAssignmetType and ClientExpertiseRating.  These we default but there should only 
            // be a single one.
            //
            ClientAssignmentType clientAssignmentTypeEntity = UnitOfWork.ClientAssignmentTypeRepository.Get(x => x.ClientId == clientId & x.AssignmentTypeId == AssignmentType.COI).ElementAt(0);
            ClientExpertiseRating clientExpertiseRatingEntity = UnitOfWork.ClientExpertiseRatingRepository.Get(x => x.ClientId == clientId & x.ConflictFlag == true).ElementAt(0);
            //
            // Now we can update it
            //
            AssignReviewerAsCoi(panelApplicationId, panelUserAssignmentId, clientAssignmentTypeEntity.ClientAssignmentTypeId, clientCoiTypeId, clientExpertiseRatingEntity.ClientExpertiseRatingId, comment, userId);
        }
        #region Helpers
        /// <summary>
        /// Validate the parameters for AssignAsCoi
        /// </summary>
        /// <param name="panelApplicationId">The application identifier</param>
        /// <param name="panelUserAssignmentId">The panel user assignment</param>
        /// <param name="comment">The comment associated with the assignment</param>
        /// <param name="userId">The user identifier making the assignment</param>
        private void ValidateParametersAssignAsCoi(int panelApplicationId, int panelUserAssignmentId, string comment, int userId)
        {
            ValidateInteger(panelApplicationId, "PanelManagementService.AssignAsCoi", "panelApplicationId");
            ValidateInteger(panelUserAssignmentId, "PanelManagementService.AssignAsCoi", "panelUserAssignmentId");
            ValidateString(comment, "PanelManagementService.AssignAsCoi", "comment");
            ValidateInteger(userId, "PanelManagementService.AssignAsCoi", "userId");
        }
        #endregion
    }
}
