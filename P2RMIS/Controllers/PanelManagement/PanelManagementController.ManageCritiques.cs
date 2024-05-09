using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.Web.ViewModels.PanelManagement;

namespace Sra.P2rmis.Web.Controllers.PanelManagement
{
    /// <summary>
    /// Controller actions for panel management Critique tab are contained in this partial file.
    /// </summary>
    public partial class PanelManagementController
    {
        /// <summary>
        /// Identifies actions available.
        /// </summary>
        public partial class PrmisAction
        {
            public const string GetApplicationCritiquesOverviewAction = "GetApplicationCritiquesOverview";
            public const string ManageCritiquesAction = "ManageCritiques";
            public const string ResetToEditAction = "ResetToEditAction";
            public const string SubmitCritiqueAction = "SubmitCritique";
            public const string ViewCritiqueAction = "ViewCritique";
            public const string SubmitPhaseAction = "SubmitPhase";
            public const string UpdatePanelStageDates = "UpdatePanelStageDates";
        }
        #region Controller Actions
        /// <summary>
        /// The Manage Critique page
        /// </summary>
        /// <returns>the view model to the page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ViewPanelCritiques)]
        public ActionResult ManageCritiques()
        {
            ManageCritiquesViewModel theViewModel = new ManageCritiquesViewModel();

            try
            {
                CustomIdentity identity = User.Identity as CustomIdentity;

                // Set data for panel menu(s)
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                if (theViewModel.HasSelectedPanel)
                {
                    //
                    // Do all of the heavy lifting of retrieving the the data.
                    //
                    ManageCritiques(theViewModel, theViewModel.SelectedPanel, identity);
                }
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new ManageCritiquesViewModel();
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                HandleExceptionViaElmah(e);
            }
             return View(theViewModel);
        }
        /// <summary>
        /// The Manage Critique page from the HTTP post
        /// </summary>
        /// <param name="SelectedProgramYear">Program/Year identifier</param>
        /// <param name="SelectedPanel">Session panel identifier</param>
        /// <returns>the view model to the page</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ViewPanelCritiques)]
        public ActionResult ManageCritiques(int? SelectedProgramYear, int SelectedPanel)
        {
            ManageCritiquesViewModel theViewModel = new ManageCritiquesViewModel();

            try
            {
                //
                // If a panel was selected then we need to determine the session.
                //
                if (SelectedPanel > 0)
                {
                    CustomIdentity identity = User.Identity as CustomIdentity;
                    //
                    // Set the selected value so the drop down displays the selected value
                    //
                    int? meetingSessionId = this.thePanelManagementService.GetMeetingSessionId(SelectedPanel);
                    SetSessionVariables(SelectedProgramYear, meetingSessionId, SelectedPanel);
                    //
                    // Do all of the heavy lifting of retrieving the the data.
                    //
                    ManageCritiques(theViewModel, SelectedPanel, identity);
                }
            }
            catch(Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new ManageCritiquesViewModel();
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                HandleExceptionViaElmah(e);
            }

            return View(theViewModel);
        }
        /// <summary>
        /// Wrapper method to do all of the heavy lifting since there are two action methods that
        /// could retrieve critique data.
        /// </summary>
        /// <param name="theViewModel">View model to populate</param>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Populated view model</returns>
        private ManageCritiquesViewModel ManageCritiques(ManageCritiquesViewModel theViewModel, int sessionPanelId, CustomIdentity identity)
        {
            //
            // Set the selected value so the drop down displays the selected value
            //
            SetPanelSession(sessionPanelId);
            SetPanelMenu(theViewModel);
            SetTabs(theViewModel);
            //
            // Check if the user can modify the phase dates and manage critiques
            //
            theViewModel.HasModifyDatePermission = IsValidPermission(Permissions.PanelManagement.ModifyReopenDates);
            theViewModel.HasManageCritiquePermission = IsValidPermission(Permissions.PanelManagement.ManagePanelCritiques);
            //
            // Off into the magic land of business rules 
            //
            var critiques = thePanelManagementService.ManageCritiques(sessionPanelId);
            PanelCritiqueSummaryViewModel.ScoreFormatter = ViewHelpers.ScoreFormatterNotCalculatedNoStatus;
            theViewModel.Critiques = new PanelCritiqueSummariesViewModel(critiques, theViewModel.HasManageCritiquePermission, GetUserId());
            //
            // Active log number
            //
            if (theViewModel.Critiques.PanelCritiques != null && theViewModel.Critiques.PanelCritiques.Count > 0)
            {
                theViewModel.ActiveLogNumber = GetActiveLogNumber() ??
                    theViewModel.Critiques.PanelCritiques.ToList<IPanelCritiqueSummaryViewModel>()[0].LogNumber;
            }            

            return theViewModel;
        }
        /// <summary>
        /// The View Critique page.
        /// </summary>
        /// <param name="applicationWorkflowStepId">Application workflow step identifier</param>
        /// <returns>the view model to the page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ViewPanelCritiques)]
        public ActionResult ViewCritique(int applicationWorkflowStepId)
        {
            ViewCritiqueViewModel theViewModel = new ViewCritiqueViewModel();
            try
            {
                // Set data for panel menu(s)
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                theViewModel.CritiqueDetails = thePanelManagementService.GetApplicationCritiqueDetails(applicationWorkflowStepId);
                //Delegate to format score
                ApplicationCritiqueDetailsModel.ScoreFormatter = ViewHelpers.ScoreFormatterNotCalculatedNoStatus;
                SetActiveLogNumber(theViewModel.CritiqueDetails.ApplicationLogNumber);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new ViewCritiqueViewModel();
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                HandleExceptionViaElmah(e);
            }

            return View(theViewModel);
        }
        /// <summary>
        /// Submit the critique & complete the workflow step.
        /// </summary>
        /// <param name="applicationWorkflowId">ApplicationWorkflow identifier for critique workflow</param>
        /// <returns>The ManageCritiques view</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManagePanelCritiques)]
        public ActionResult SubmitCritique(int applicationWorkflowId, int applicationWorkflowStepId)
        {
            ManageCritiquesViewModel theViewModel = new ManageCritiquesViewModel();
            try
            {
                ViewCritiqueViewModel theViewCritModel = new ViewCritiqueViewModel();             
                theViewCritModel.CritiqueDetails = thePanelManagementService.GetApplicationCritiqueDetails(applicationWorkflowStepId);

                if (thePanelManagementService.IsCritiqueSubmittable(applicationWorkflowId)  && !theViewCritModel.CritiqueDetails.IsSubmitted)
                {
                    theWorkflowService.ExecuteSubmitWorkflow(applicationWorkflowId, GetUserId());
                }
                //
                // Set data for panel menu(s). Then do all of the heavy 
                // lifting of retrieving the data.
                //
                CustomIdentity identity = User.Identity as CustomIdentity;

                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                ManageCritiques(theViewModel, theViewModel.SelectedPanel, identity);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new ManageCritiquesViewModel();
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                HandleExceptionViaElmah(e);
            }

            return View(ViewNames.ManageCritiques, theViewModel);
        }
        /// <summary>
        /// Submit all submittable critiques in the phase.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier identifing the current panel</param>
        /// <param name="stepTypeId">StepType identifier identifing the phase</param>
        /// <returns>True if phase submitted; false otherwise </returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManagePanelCritiques)]
        public ActionResult SubmitPhase(int sessionPanelId, int stepTypeId)
        {
            bool status = false;
            try
            {
                thePanelManagementService.FinalizeCritique(this.theWorkflowService, sessionPanelId, stepTypeId, GetUserId());
                status = true;
            }
            catch (Exception e)
            {
                //
                // Something went wrong log the error
                //
                HandleExceptionViaElmah(e);
            }

            return Json(status);
        }


        /// <summary>
        /// Get application critique data
        /// </summary>
        /// <param name="panelApplicationId">the panel application identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <returns>the partial view for a single application's critiques</returns>
        [Common.Authorize(Operations = Permissions.PanelManagement.ManagePanelCritiques)]
        public ActionResult GetApplicationCritiquesOverview(int panelApplicationId, int sessionPanelId)
        {
            ApplicationCritiquesOverviewViewModel theViewModel = new ApplicationCritiquesOverviewViewModel(panelApplicationId, sessionPanelId);
            try
            {
                if (panelApplicationId > 0)
                {
                    // Meeting phase has already been started (Scored/Scoring)
                    bool isMeetingPhaseStarted = thePanelManagementService.IsMeetingPhaseStarted(panelApplicationId);
                    theViewModel.IsMeetingPhaseStarted = isMeetingPhaseStarted;
                    var applicationCritiqueObject = thePanelManagementService.GetApplicationCritiques(panelApplicationId);
                    theViewModel.ApplicationCritiques = applicationCritiqueObject;
                    //Delegate to format score
                    CritiquePhaseInformation.ScoreFormatter = ViewHelpers.ScoreFormatterNotCalculatedNoStatus;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                theViewModel = new ApplicationCritiquesOverviewViewModel(panelApplicationId, sessionPanelId);
                HandleExceptionViaElmah(e);
            }
            // send view model to view page
            return PartialView(ViewNames.ApplicationCritiquesOverview, theViewModel);
        }

        /// <summary>
        /// Action to reset to edit
        /// </summary>
        /// <param name="applicationWorkflowId">Application workflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="targetWorkflowStepId">Application workflow step identifier</param>
        /// <returns>True in JSON if successful, otherwise return false.</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManagePanelCritiques)]
        public ActionResult ResetToEditAction(int applicationWorkflowId, int userId, int targetWorkflowStepId)

        {
            bool status = false;
            try
            {
                theWorkflowService.ExecuteResetToEditWorkflow(applicationWorkflowId, targetWorkflowStepId, userId);
                status = true;
                thePanelManagementService.SendCritiqueResetEmail(applicationWorkflowId, theMailService, GetUserId());
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(status);
        }
        /// <summary>
        /// Update the panel's re-open & close date/time with the specified values.
        /// </summary>
        /// <param name="endDate">Panel phase end date</param>
        /// <param name="reopenDateTime">Panel phase ReOpen date</param>
        /// <param name="closeDateTime">Panel phase Close date</param>
        /// <param name="meetingSessionId">Meeting session identifier</param>
        /// <param name="stageTypeId">StageType identifier indicates the phase.</param>
        /// <returns>ReturnStatus object indicating the success status and message</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageReviewerAssignmentExpertise)]
        public ActionResult UpdatePanelStageDates(string endDate, string reopenDateTime, string closeDateTime, int meetingSessionId, int stageTypeId)
        {
            PanelStageDateUpdateStatus status = PanelStageDateUpdateStatus.Default;
            
            try
            {
                DateTime theEndDate = DateTime.Parse(endDate);
                //Since reopen dates are being converted to UTC on the front end and passed as UTC string, explicitly cast to Eastern Time Zone otherwise they will be stored in db as timezone of server rather than eastern
                DateTime theReopenDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTimeOffset.Parse(reopenDateTime), "Eastern Standard Time").DateTime;
                DateTime theCloseDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTimeOffset.Parse(closeDateTime), "Eastern Standard Time").DateTime;

                status = thePanelManagementService.UpdatePanelStageDates(theEndDate, theReopenDateTime, theCloseDateTime, meetingSessionId, stageTypeId, GetUserId());
            }
            catch (Exception e)
            {
                status = PanelStageDateUpdateStatus.SomethingBadHappened;
                HandleExceptionViaElmah(e);
            }
            //
            // Set up the return & send it
            //
            var returnResult = new ReturnStatus { IsSuccessful = (status == PanelStageDateUpdateStatus.Success), Message = GetMessage(status) };  
            var result = JsonConvert.SerializeObject(returnResult);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Edit comments on the discussion board during the MOD phase
        /// </summary>
        /// <param name="applicationStageStepId">Application stage step identifier</param>
        /// <returns>Returns the Discussion Board View</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult DiscussionBoard(int applicationStageStepId)
        {
            var model = new DiscussionBoardViewModel();
            try
            {
                var discussionBoard = theApplicationScoringService.RetreiveDiscussionInfo(applicationStageStepId);
                model = new DiscussionBoardViewModel(discussionBoard);
                model.LastPageUrl = GetBackButtonUrl();
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return View(model);
        }

        [Sra.P2rmis.Web.Common.Authorize] //(Operations = Permissions.PanelManagement.Manage)]
        public ActionResult AddDBComment()
        {
            return PartialView(ViewNames.AddDBComment);
        }

        /// <summary>
        /// Warning that the application cannot start MOD.
        /// </summary>
        /// <returns>Returns the warning view</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult CanNotStartModWarning()
        {
            return PartialView(ViewNames.CanNotStartModWarning);
        }

        /// <summary>
        /// Saves the mod comment.
        /// </summary>
        /// <param name="applicationStageStepEntityId">The application stage step entity identifier.</param>
        /// <param name="applicationDicussionEntityId">The application discussion entity identifier.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>Status value.  True the on-line discussion session was successfully start; false otherwise.  Status is returned in JSON format.</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult SaveModComment(int applicationStageStepEntityId, int? applicationDiscussionEntityId, string comment)
        {
            bool result = false;
            try
            {
                bool isNew = false;
                int userId = GetUserId();
                var commentEntityId = thePanelManagementService.SaveModComment(applicationStageStepEntityId, applicationDiscussionEntityId, comment, userId, isNew);
                theApplicationScoringService.SendDiscussionNotification(theMailService, commentEntityId.CommentId, commentEntityId.CommentType);
                result = true;
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Refresh the on line discussion comment grid in response to the Refresh button.
        /// </summary>
        /// <param name="applicationStageStepEntityId">ApplicationStageStep entity identifier of the on line discussion</param>
        /// <returns>Discussion comments in JSON format</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult RefreshComments(int applicationStageStepEntityId)
        {
            var model = new DiscussionBoardViewModel();
            string results = string.Empty;
            
            try
            {
                var discussionBoard = theApplicationScoringService.RetreiveDiscussionInfo(applicationStageStepEntityId);
                model = new DiscussionBoardViewModel(discussionBoard);
                results = JsonConvert.SerializeObject(model.DiscussionComments);
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets message for updating panel stage dates.
        /// </summary>
        /// <param name="status">The PanelStageDateUpdateStatus value</param>
        /// <returns>The message for the specified status. Otherwise return an empty string.</returns>
        protected string GetMessage(PanelStageDateUpdateStatus status)
        {
            string message = string.Empty;
            switch (status)
            {
                case PanelStageDateUpdateStatus.Success:
                    message = Messages.DatesUpdated;
                    break;
                case PanelStageDateUpdateStatus.ReOpenDateInvalid:
                    message = Messages.ReOpenDateInvalid;
                    break;
                case PanelStageDateUpdateStatus.CloseDateInvalid:
                    message = Messages.CloseDateInvalid;
                    break;
                case PanelStageDateUpdateStatus.BothDatesInvalid:
                    message = Messages.BothDatesInvalid;
                    break;
                case PanelStageDateUpdateStatus.SomethingBadHappened:
                    message = Messages.SomethingBadHappened;
                    break;
                case PanelStageDateUpdateStatus.SameDates:
                    message = Messages.SameDates;
                    break;
            }
            return message;
        }
        #endregion
    }
}