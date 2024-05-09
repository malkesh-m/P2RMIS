using System;
using Sra.P2rmis.Bll.Views;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Service providing access to Session information.  Services provided are:
    ///      - TODO: list
    /// </summary>
    public interface ISessionService : IDisposable
    {
        /// <summary>
        /// Retrieves Sessions; Panels & Mechanisms for a specific program.
        /// </summary>
        /// <param name="programId">Program identifier</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="elevatedPerms">Elevated permissions</param>
        /// <returns>Service results for retrieval of Mechanism data by a specific Program.</returns>
        SessionDetailView GetAllSessionsDetails(int programId, int? userId, bool elevatedPerms);
    
        /// <summary>
        /// Retrieves TODO:: document me
        /// </summary>
        /// <param name="programId">Program identifier</param>
        /// <param name="panelId">Panel identifier</param>
        /// <param name="elevatedPerms">Elevated permissions</param>
        /// <param name="userId">The user identifier</param>
        /// /// <returns>ViewPanelDetailsView object</returns>
        ViewPanelDetailsView GetSessionDetailsForPanel(int programId, int panelId, bool elevatedPerms, int userId);
        
        /// <summary>
        /// Returns Mechanism details for a selected session.  Session details is retrieved from the database.
        /// </summary>
        /// <param name="programId">Program identifier</param>
        /// <param name="sessionId">Session identifier</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="elevatedPerms">Elevated permissions</param>
        /// <returns>SessionDetailView object</returns>
        SessionDetailView GetPanelAndMechanismDetailsForSelectedSession(int programId, int sessionId, int? userId, bool elevatedPerms);
    }
}
