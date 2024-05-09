using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Database access methods for the View Panel Details view.
    /// </summary>
    public interface IViewPanelDetailsRepository
    {
        /// <summary>
        /// Returns the session details for a specific panel identified by the program Id.
        /// </summary>
        /// <param name="programId">Open program identifier</param>
        /// <param name="panelId">Panel identifier</param>
        /// <param name="elevatedPerms">Elevated permissions</param>
        /// <param name="userId">The user identifier</param>
        /// <returns>ViewPanelDetailsResultModel container containing results</returns>
        ViewPanelDetailsResultModel GetSessionDetailsForPanel(int programId, int panelId, bool elevatedPerms, int userId);
        /// <summary>
        /// Returns the application details for all applications on a specific panel.
        /// </summary>
        /// <param name="panelId">Panel identifier</param>
        /// <param name="userId">The user identifier</param>
        /// <returns>Enumeration of ViewPanelDetails_Results</returns>
        IEnumerable<uspViewPanelDetails_Result> GetApplicationDetailsForPanel(int panelId, int userId); 
    }
    /// <summary>
    /// Database access methods for the View Panel Details view.
    /// </summary>
    public class ViewPanelDetailsRepository : IViewPanelDetailsRepository
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
        public ViewPanelDetailsRepository(P2RMISNETEntities context)
        {
            this.Context = context;
        }
        #endregion
        #region Repository Methods
        /// <summary>
        /// Returns the session details for a specific panel identified by the program Id.
        /// </summary>
        /// <param name="programId">Open program identifier</param>
        /// <param name="panelId">Panel identifier</param>
        /// <param name="elevatedPerms">Elevated permissions</param>
        /// <param name="userId">The user identifier</param>
        /// <returns>ViewPanelDetailsResultModel container containing results</returns>
        public ViewPanelDetailsResultModel GetSessionDetailsForPanel(int programId, int panelId, bool elevatedPerms, int userId)
        {
            ViewPanelDetailsResultModel results = new ViewPanelDetailsResultModel();
            results.ApplicationCounts = RepositoryHelpers.GetMechanismApplicationCount(Context, panelId);
            ///
            /// Get a list of Sessions that belong to the program Id
            /// and then retrieve the details for the sessions &
            /// create a return container.
            /// 
            results.Sessions = RepositoryHelpers.GetMeetingSessionsByProgramId(Context, programId, userId, elevatedPerms);
            ///
            /// Get a list of the session ids from the returned sessions then 
            /// get a list of the panels for these sessions.
            /// 
            IEnumerable<int> ss = from s in results.Sessions select s.MeetingSessionId;
            results.Panels = RepositoryHelpers.GetPanelsBySessionIds(Context, programId, ss, userId, elevatedPerms); 
            ///
            /// Get the application details
            /// 
            results.ViewPanelDetail = RepositoryHelpers.GetApplicationDetailsByPanelId(Context, panelId, userId);

            return results;
        }
        /// <summary>
        /// Returns the application details for all applications on a specific panel.
        /// </summary>
        /// <param name="panelId">Panel identifier</param>
        /// <param name="userId">The user identifier</param>
        /// <returns>Enumeration of ViewPanelDetails_Results</returns>
        public IEnumerable<uspViewPanelDetails_Result> GetApplicationDetailsForPanel(int panelId, int userId)
        {
            return RepositoryHelpers.GetApplicationDetailsByPanelId(Context, panelId, userId);
        }
        #endregion
    }
}
