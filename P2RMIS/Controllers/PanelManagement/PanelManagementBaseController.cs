using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.ReviewerRecruitment;
using Sra.P2rmis.Bll.ApplicationScoring;
using Sra.P2rmis.Web.UI.Models;
using System.Linq;
using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.CrossCuttingServices;
namespace Sra.P2rmis.Web.Controllers.PanelManagement
{
    /// <summary>
    /// Base controller for P2RMIS Panel Management controller.  
    /// Basically a container for Panel Management controller common functionality.
    /// </summary>
    public class PanelManagementBaseController : BaseController
    {
        #region Properties
        /// <summary>
        /// Service providing access to the Summary management services.
        /// </summary>
        protected ISummaryProcessingService theSummaryProcessingService { get; set; }
        protected IMailService theMailService { get; set; }
        protected IPanelManagementService thePanelManagementService { get; set; }
        protected IWorkflowService theWorkflowService { get; set; }
        protected IUserProfileManagementService theUserProfileManagementService { get; set; }
        protected IFileService theFileService { get; set; }
        protected ILookupService theLookupService { get; set; }
        protected ICriteriaService theCriteriaService { get; set; }
        protected IReviewerRecruitmentService theRecruitmentService { get; set; }
        protected IApplicationScoringService theApplicationScoringService { get; set; }
        #endregion
        #region Constants
        public const string ControllerName = "PanelManagement";
        /// <summary>
        /// Class identifies session variables used by the PanelManagement controller.
        /// </summary>
        public class Constants
        {
            public const string ProgramYearSession = "PmProgramYear";
            public const string PanelSession = "PmPanel";
            public const string SearchInstruction = "Please select a Panel to start.";
            public const string ZeroSearchResults = "There are no matching results for the selected panel.";
        }
        public class ViewNames
        {
            public const string ApplicationAbstracts = "Index";
            public const string EditOverview = "_EditOverview";
            public const string CoiList = "_CoiList";
            public const string RequestTransfer = "_RequestTransfer";
            public const string RequestReviewerTransfer = "_RequestReviewerTransfer";
            public const string NewRequestReviewerTransfer = "_NewRequestReviewerTransfer";
            public const string PIInformation = "_PIInformation";
            public const string ReviewerAssignment = "_ReviewerAssignment";
            public const string ElevatedChairpersonAssignment = "_ElevatedChairpersonAssignment";
            public const string ManageCritiques = "ManageCritiques";
            public static string ApplicationCritiquesOverview = "_ApplicationCritiquesOverview";
            public static string ViewCommunication = "_ViewCommunication";
            public static string SelectReviewersEmailAddress = "_SelectReviewersEmailAddress";
            public static string Communication = "Communication";
            public static string PanelAssignment = "_PanelAssignment";
            public static string CommunicationLog = "_CommunicationLog";
            public static string RatingEvaluation = "_RatingEvaluation";
            public static string PersonSearchNoRecordsFound = "_PersonSearchNoRecordsFound";
            public static string PersonSearchNotEnoughChars = "_PersonSearchNotEnoughChars";
            public static string PanelListRemoveReviewerWarning = "_PanelListRemoveReviewerWarning";
            public static string AssignWarning = "_AssignWarning";
            public static string AddDBComment = "_AddDBComment";
            public static string CanNotStartModWarning = "_CanNotStartModWarning";
            public static string ManageCritiqueModal = "_ManageCritiqueModal";
            public static string ApplicationModal = "_ApplicationModal";
            public static string Threshold = "_ThresholdModal";

        }
        #endregion
        #region Supporting Classes
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

        /// <summary>
        /// Get tab menu filtered by what the logged in user can access
        /// </summary>
        /// <param name="viewModel">The ProfileManagement view model</param>
        protected virtual void SetTabs(PanelTabsViewModel viewModel)
        {
            viewModel.Tabs = viewModel.Tabs.Where(o => HasPermission(o.RequiredPermission)).ToList();
        }


        /// <summary>
        /// Checks if the current user can access the panel
        /// </summary>
        /// <param name="sessionPanelId"></param>
        /// <returns>List of Messages for outstanding requirements. Empty list means they can access</returns>
        protected virtual List<string> CanUserAccessManagePanel(int sessionPanelId)
        {
            int userId = GetUserId();
            bool registrationComplete = theApplicationScoringService.CanUserAccessPanelAssignment(userId, sessionPanelId);
            bool expertiseRatingComplete = thePanelManagementService.IsUserExpertiseComplete(sessionPanelId, userId, HasPermission(Permissions.PanelManagement.ManagePanelCritiques));
            return PopulatePanelAccessMessages(registrationComplete, expertiseRatingComplete);
        }

        private List<string> PopulatePanelAccessMessages(bool registrationComplete, bool expertiseRatingComplete)
        {
            List<string> messages = new List<string>();
            if (!registrationComplete) messages.Add(MessageService.RegistrationNotComplete);
            if (!expertiseRatingComplete) messages.Add(MessageService.ExpertiseNotComplete);

            return messages;
        }
        #endregion
    }
}