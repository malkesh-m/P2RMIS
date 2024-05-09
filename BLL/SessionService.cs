using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// SessionService provides services and business logic specific to the Application Scoring Application.
    /// </summary>
    public class SessionService: ServerBase,ISessionService
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public SessionService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion
        #region Provided Services
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="programId">Identifies specific program to retrieve the session details for.</param>
        /// <returnsSessionDetailView object<returns>
        public SessionDetailView GetAllSessionsDetails(int programId, int? userId, bool elevatedPerms)
        {
            ///
            /// Retrieve the data we are supposed to retrieve & shove it into the 
            /// our view
            ///  
            SessionsResultModel getResults = UnitOfWork.SessionDetailRepository.GetAllSessionDetails(programId, userId, elevatedPerms);
            var view = new SessionDetailView(getResults);
            ///
            /// return the view to the controller
            /// 
            return view;
        }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="programId">Program identifier</param>
        /// <param name="panelId">Panel identifier</param>
        /// <param name="elevatedPerms">Elevated permissions</param>
        /// <param name="userId">The user identifier</param>
        /// <returns>ViewPanelDetailsView object</returns>
        public ViewPanelDetailsView GetSessionDetailsForPanel(int programId, int panelId, bool elevatedPerms, int userId)
        {
            ///
            /// Retrieve the data we are supposed to retrieve & shove it into the 
            /// our view
            ///  
            ViewPanelDetailsResultModel getResults = UnitOfWork.ViewPanelDetailsRepository.GetSessionDetailsForPanel(programId, panelId, elevatedPerms, userId);
            var view = new ViewPanelDetailsView(getResults);
            ///
            /// return the view to the controller
            /// 
            return view;
        }
        
        
        /// <summary>
        /// Returns Mechanism details for a selected session.  Session details is retrieved from the database.
        /// </summary>
        /// <param name="programId">Open program identifier</param>
        /// <param name="sessionId">Session identifier</param>
        /// <param name="elevatedPerms">Elevated permissions</param>
        /// <param name="userId">The user identifier</param>
        /// <returns>SessionDetailView object</returns>
        public SessionDetailView GetPanelAndMechanismDetailsForSelectedSession(int programId, int sessionId, int? userId, bool elevatedPerms)
        {
            ///
            /// Retrieve the data we are supposed to retrieve & shove it into our view
            ///  
            SessionsResultModel results = UnitOfWork.SessionDetailRepository.GetPanelAndMechanismDetailsForSelectedSession(programId, sessionId, userId, elevatedPerms);
            var view = new SessionDetailView(results);
            ///
            /// return the view to the controller
            /// 
            return view;
        }
        #endregion
    }
}
