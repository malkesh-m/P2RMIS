using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.ApplicationScoring;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Provides retrieval functions for the Application Scoring application.
    /// </summary>
    public interface ISessionDetailRepository
    {
        /// <summary>
        /// Returns the session details for a specific program identified by the program Id.
        /// </summary>
        /// <param name="programId">Identifies the session to retrieve</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="elevatedPerms">Elevated Permissions</param>
        /// <returns>Enumerable list of SessionResultModel</returns>
        SessionsResultModel GetAllSessionDetails(int programId, int? userId, bool elevatedPerms);
        /// <summary>
        /// Returns the panels & mechanism details for a specific session identified by the program Id.
        /// </summary>
        /// <param name="programId">Open program identifier</param>
        /// <param name="sessionId">Session identifier</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="elevatedPerms">Elevated Permissions</param>
        /// <returns>ResultModel containing Panels & Mechanism form the selected Session</returns>
        SessionsResultModel GetPanelAndMechanismDetailsForSelectedSession(int programId, int sessionId, int? userId, bool elevatedPerms);
    }
    /// <summary>
    /// Provides retrieval functions for the Application Scoring application.
    /// </summary>
    public class SessionDetailRepository: ISessionDetailRepository
    {
        #region Attributes
        /// <summary>
        /// P2RMIS database context
        /// </summary>
        private P2RMISNETEntities Context { get; set; }
        #endregion
        #region Construction; Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public SessionDetailRepository(P2RMISNETEntities context)
        {
            this.Context = context;
        }
        #endregion
        #region Repository Methods
        /// <summary>
        /// Returns the session details for a specific program identified by the program Id.
        /// </summary>
        /// <param name="programId">Identifies the session to retrieve</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="elevatedPerms">Elevated Permissions</param>
        /// <returns>Enumerable list of SessionResultModels</returns>
        public SessionsResultModel GetAllSessionDetails(int programId, int? userId, bool elevatedPerms)
        {
            SessionsResultModel results = new SessionsResultModel(); 

            results.Sessions = RepositoryHelpers.GetMeetingSessionsByProgramId(Context, programId, userId, elevatedPerms);

            IEnumerable<int> ss = from s in results.Sessions select s.MeetingSessionId;

            results.Panels = RepositoryHelpers.GetPanelsBySessionIds(Context, programId, ss, userId, elevatedPerms);

            IEnumerable<int> pp = from p in results.Panels select p.SessionPanelId;
            results.ApplicationCounts = RepositoryHelpers.GetMechanismApplicationCount(Context, pp) ?? new List<ApplicationCount>();

            return results;
        }

        /// <summary>
        /// Returns the panels & mechanism details for a specific session identified by the program Id.
        /// </summary>
        /// <param name="programId">Open program identifier</param>
        /// <param name="sessionId">Session identifier</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="elevatedPerms">Elevated Permissions</param>
        /// <returns>ResultModel containing Panels & Mechanism for the selected Session</returns>
        public SessionsResultModel GetPanelAndMechanismDetailsForSelectedSession(int programId, int sessionId, int? userId, bool elevatedPerms)
        {
            SessionsResultModel results = new SessionsResultModel();
            ///
            /// First get the session details.  Then get the panels for this session.
            /// 
            results.Sessions = RepositoryHelpers.GetMeetingSessionsByProgramId(Context, programId, userId, elevatedPerms);
            

            List<int> sessionList = new List<int> {sessionId};

            results.Panels = RepositoryHelpers.GetPanelsBySessionIds(Context, programId, sessionList, userId, elevatedPerms);

            IEnumerable<int> pp = from p in results.Panels select p.SessionPanelId;
            results.ApplicationCounts = RepositoryHelpers.GetMechanismApplicationCount(Context, pp) ?? new List<ApplicationCount>();

            return results;
        }
        #endregion
   }
}
