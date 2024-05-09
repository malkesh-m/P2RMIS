using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.ApplicationScoring;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll.HotelAndTravel;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.Web.ViewModels;
using System;

namespace Sra.P2rmis.Web.Controllers.MyWorkspace
{
    /// <summary>
    /// Base controller for P2RMIS My Workspace controller.  
    /// Basically a container for My Workspace controller common functionality.
    /// </summary>
    public class MyWorkspaceBaseController : BaseController
    {
        #region Properties
        /// <summary>
        /// Service providing access to the My Workspace services.
        /// </summary>
        protected IApplicationManagementService theApplicationManagementService { get; set; }
        protected IApplicationScoringService theApplicationScoringService { get; set; }
        protected IPanelManagementService thePanelManagementService { get; set; }
        protected IUserProfileManagementService theUserProfileManagementService { get; set; }
        protected IWorkflowService theWorkflowService { get; set; }
        protected IFileService theFileService { get; set; }
        protected IMailService theMailService { get; set; }            
        protected ILookupService theLookupService { get; set; }    
        protected ISessionService theSessionService { get; set; }
        /// <summary>
        /// The HotelAndTravelService.
        /// </summary>
        protected IHotelAndTravelService theHotelAndTravelService { get; set; }
        #endregion
        #region Constants
        public const string ControllerName = "MyWorkspace";
        /// <summary>
        /// MyWorkspace view names
        /// </summary>
        public class ViewNames
        {
            public const string AssignedReviewersScores = "_AssignedReviewersScores";
            public const string Scorecard = "Scorecard";
            public const string ReviewerAssignment = "_ReviewerAssignment";
            public const string ReviewerAssignmentCPRIT = "_ReviewerAssignmentCPRIT";
            public const string IncompleteOverallWarningModal = "_IncompleteOverallWarningModal";
            public const string IncompleteCritiqueWarningModal = "_IncompleteCritiqueWarningModal";
            public const string NotificationOfSubmitModal = "_NotificationOfSubmitModal";
            public const string ConfirmationOfSuccessModal = "_ConfirmationOfSuccessModal";
            public const string CritiquePanel = "CritiquePanel";
            public const string NoDataAvailable = "_NoDataAvailable";
            public const string ScorecardModal = "_ScorecardModal";
            public const string AlreadySubmittedCritique = "_AlreadySubmittedCritique";
        }
        /// <summary>
        /// Status messages shown to the user based on server status values
        /// </summary>
        protected class Messages
        {
            public readonly static string AttachmentsExceedSize = "The total size of all attachments exceed the maximum size to email.";
            public readonly static string FailedToSend = "The Email message was not sent.  Please try again.";
            public readonly static string UnspecifiedErrorReturned = "An unspecified error occurred.";
            public readonly static string Failure = "The request was not performed";
            //
            // Messages for PanelStageDateUpdateStatus values
            //
            public readonly static string DatesUpdated = "The Re-Open and Close Date/Times were successfully updated.";
            public readonly static string ReOpenDateInvalid = "The Re-Open Date/Time is invalid.";
            public readonly static string CloseDateInvalid = "The Close Date/Time is invalid.";
            public readonly static string BothDatesInvalid = "The Re-Open and Close Date/Time are invalid.";
            public readonly static string SomethingBadHappened = "An unexpected error has occurred.  Please try again.";
            public readonly static string SameDates = "The Re-Open and Close Date/Time are the same.";
            public readonly static string PanelOverviewSaved = "The overview for this application was saved.";
            public readonly static string PanelOverviewNotSaved = "The Panel Overview was not saved.";
            public readonly static string SummaryStatementProcessingStarted = "The Panel Overview was not saved because Summary Statement processing has started.";
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Sets the panel id in the session.
        /// </summary>
        /// <param name="panelId">the panel id to set in the session</param>
        public virtual void SetPanelSession(int panelId)
        {
            Session[Invariables.SessionKey.PanelSession] = panelId;
        }
        /// <summary>
        /// returns the panelId stored in the session.
        /// </summary>
        /// <returns>panelId</returns>
        public virtual int GetPanelSession()
        {
            int panelId = Convert.ToInt32(Session[Invariables.SessionKey.PanelSession]);
            return panelId;
        }
        /// <summary>
        /// Returns whether the current user can access the specified panel due to registration or other requirements
        /// </summary>
        /// <param name="sessionPanelId">Identifier for a panel</param>
        /// <returns>True if user can access; otherwise false</returns>
        public virtual bool CanUserAccessPanel(int sessionPanelId)
        {
            int userId = GetUserId();
            return theApplicationScoringService.CanUserAccessPanelAssignment(userId, sessionPanelId);
        }
        #endregion
    }
}