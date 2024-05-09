
namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Common services provided by all servers.
    /// </summary>
    public interface IServerBase
    {
        /// <summary>
        /// Checks whether the user can access their panel assignment due to having incomplete assignments
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <returns>True if access is permitted; otherwise false;</returns>
        bool NewCanUserAccessPanelAssignment(int panelUserAssignmentId);
        /// <summary>
        /// Checks whether the user can access their panel assignment due to having incomplete assignments
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sessionPanelId"></param>
        /// <returns>True if access is permitted; otherwise false;</returns>
        bool NewCanUserAccessPanelAssignment(int userId, int sessionPanelId);
    }
}
