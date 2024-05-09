using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.MeetingManagement;
using Sra.P2rmis.WebModels.SummaryStatement;
using System.Collections.Generic;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository interface for PanelUserAssignment objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary> 
    public interface IPanelUserAssignmentRepository : IGenericRepository<PanelUserAssignment>
    {
        /// <summary>
        /// Retrieves list of reviewer names that are associated with a session panel.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Container of IUserModel models</returns>
        ResultModel<IUserModel> ListReviewerNames(int sessionPanelId);

        /// <summary>
        /// Gets the panel user assignments for open panels.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IEnumerable<PanelUserAssignment> GetPanelUserAssignmentsForOpenPanels(int userId);
        /// <summary>
        /// Gets the assigned application ids.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        List<int> GetAssignedPanelApplicationIds(int panelUserAssignmentId);
        /// <summary>
        /// Gets the client identifier.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        int GetClientId(int panelUserAssignmentId);
    }
}
