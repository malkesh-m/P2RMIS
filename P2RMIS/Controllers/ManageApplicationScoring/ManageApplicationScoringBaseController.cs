using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.ApplicationScoring;
using Sra.P2rmis.Bll.PanelManagement;

namespace Sra.P2rmis.Web.Controllers.ManageApplicationScoring
{
    /// <summary>
    /// Base controller for P2RMIS Manage Application Scoring controller.  
    /// Basically a container for Manage Application Scoring controller common functionality.
    /// </summary>
    public class ManageApplicationScoringBaseController : BaseController
    {
        #region Properties
        /// <summary>
        /// Service providing access to the Panel Management services.
        /// </summary>
        protected IPanelManagementService thePanelManagementService { get; set; }
        /// <summary>
        /// Service providing access to the Application Scoring services.
        /// </summary>
        protected IApplicationScoringService theApplicationScoringService { get; set; }
        /// <summary>
        /// Service providing access to the session services.
        /// </summary>
        protected ISessionService theSessionService { get;  set; }
        /// <summary>
        /// Service providing access to the application management services.
        /// </summary>
        protected IApplicationManagementService theApplicationManagementService { get;  set; }
        /// <summary>
        /// The View names in user profile management
        /// </summary>
        public class ViewNames
        {
            public const string AppStatus = "_AppStatus";
            public const string AlreadyActiveApp = "_AlreadyActiveApp";
            public const string RevStatus = "_RevStatus";
            public const string AdminNotes = "_AdminNotes";
        }

        public class ActionNames
        {
            public const string ManageAppScoring = "Index";
            public const string ApplicationDetails = "ApplicationDetails";
        }
        #endregion
    }
}