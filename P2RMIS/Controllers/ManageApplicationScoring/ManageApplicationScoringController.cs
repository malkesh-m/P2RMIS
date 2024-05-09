using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.ApplicationScoring;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.Web.ViewModels.ManageApplicationScoring;

namespace Sra.P2rmis.Web.Controllers.ManageApplicationScoring
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ManageApplicationScoringController : ManageApplicationScoringBaseController
    {
        #region Construction & Setup
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        public ManageApplicationScoringController()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="thePanelManagementService">The panel management service</param>
        /// <param name="theSessionService">The session service</param>
        /// <param name="theApplicationManagementService">The application management service</param>
        public ManageApplicationScoringController(IPanelManagementService thePanelManagementService,
                                                  ISessionService theSessionService,
                                                  IApplicationManagementService theApplicationManagementService,
                                                  IApplicationScoringService applicationScoringService)
        {
            this.thePanelManagementService = thePanelManagementService;
            this.theSessionService = theSessionService;
            this.theApplicationManagementService = theApplicationManagementService;
            this.theApplicationScoringService = applicationScoringService;
        }
        #endregion
        public class Constants
        {
            /// <summary>
            /// Contains error messages
            /// </summary>
            public class Messages
            {
                public const string FailedRetrievalOfApplicationDetails = "An error occurred retrieving the application details.  Please try again.";
                public const string FailedRetrievalOfCritiqueDetails = "An error occurred retrieving the application reviewer's critiques.  Please try again.";
                public const string FailedToSaveComment = "An error occurred that prevented the comment from being saved.  Please try again.";
                public const string FailedToEditComment = "An error occurred that prevented the comment from being modified.  Please try again.";
                public const string FailedToDeleteComment = "An error occurred that prevented the comment from being deleted.  Please try again.";
            }
        }
        #region Controller Actions
        /// <summary>
        /// Manage application scoring
        /// </summary>
        /// <returns>The view of the Manage Application Scoring page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ManageApplicationScoring.AccessManageApplicationScoring)]
        public ActionResult Index()
        {
            int? sessionProgramYearId = null;
            int? sessionSessionId = null;
            int? sessionPanelId = null;
            ManageApplicationScoringViewModel model = new ManageApplicationScoringViewModel();
            try
            {
                int userId = GetUserId();
                model = SetFilterMenu(model);
                GetFromSession(ref sessionProgramYearId, ref sessionSessionId, ref sessionPanelId);

                if (sessionProgramYearId != null)
                {
                    model.SelectedProgramId = sessionProgramYearId;
                    if (model.CanViewAllPanels)
                    {
                        model.Sessions = GetSessionList((int)sessionProgramYearId);
                    }
                    if (sessionSessionId != null && model.Sessions.Exists(x => x.SessionId == (int)sessionSessionId))
                    {
                        model.SelectedSessionId = sessionSessionId;
                        if (model.CanViewAllPanels)
                        {
                            model.Panels = GetPanelList((int)model.SelectedProgramId, (int)sessionSessionId);
                        }
                    }
                }
                CheckPanelVisibility(model);
                model.SelectedPanelId = sessionPanelId;
                // Reset CanViewScoreboard based on meeting status
                model.CanViewScoreboard = model.CanViewScoreboard && model.SelectedPanelId != null &&
                    thePanelManagementService.IsMeetingCurrent((int)model.SelectedPanelId);
                model = PopulateManageAppScoringGrids(sessionProgramYearId.GetValueOrDefault(), sessionSessionId.GetValueOrDefault(), sessionPanelId.GetValueOrDefault(), userId, model);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(model);
        }
        /// <summary>
        /// Manage application scoring submission
        /// </summary>
        /// <param name="SelectedProgramId">The selected program identifier</param>
        /// <param name="SelectedSessionId">The selected session identifier</param>
        /// <param name="SelectedPanelId">The selected panel identifier</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ManageApplicationScoring.AccessManageApplicationScoring)]
        public ActionResult Index(int? SelectedProgramId, int? SelectedSessionId, int? SelectedPanelId)
        {
            int programId = SelectedProgramId.GetValueOrDefault();
            int sessionId = SelectedSessionId.GetValueOrDefault();
            int panelId = SelectedPanelId.GetValueOrDefault();

            ManageApplicationScoringViewModel model = new ManageApplicationScoringViewModel();
            try
            {
                int userId = GetUserId();
                SetSessionVariables(SelectedProgramId, SelectedSessionId, SelectedPanelId);
                model = SetFilterMenu(model);
                model.SetSelectionIdentifiers(SelectedProgramId, SelectedSessionId, SelectedPanelId);
                if (SelectedProgramId != null)
                {
                    model.Sessions = GetSessionList((int)model.SelectedProgramId);
                    if (SelectedSessionId != null)
                    {
                        model.Panels = GetPanelList((int)model.SelectedProgramId, (int)model.SelectedSessionId);
                    }
                }
                CheckPanelVisibility(model);
                // Reset CanViewScoreboard based on meeting status
                model.CanViewScoreboard = model.CanViewScoreboard && model.SelectedPanelId != null &&
                    thePanelManagementService.IsMeetingCurrent((int)model.SelectedPanelId);
                model = PopulateManageAppScoringGrids(programId, sessionId, panelId, userId, model);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(model);
        }

        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetSessionsAndPanels(int selectedProgramId)
        {
            var sessions = new List<SessionView>();
            var panels = new List<PanelView>();
            try
            {
                sessions = GetSessionList(selectedProgramId);
                foreach (var session in sessions)
                {
                    var sessionPanels = GetPanelList(selectedProgramId, session.SessionId);
                    panels.AddRange(sessionPanels);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { sessions = sessions, panels = panels }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get panels from selected program/session identifiers
        /// </summary>
        /// <param name="selectedProgramId">The selected program identifier</param>
        /// <param name="selectedSessionId">The selected session identifier</param>
        /// <returns>The panel data in JSON format</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetPanels(int selectedProgramId, int selectedSessionId)
        {
            var panels = new List<PanelView>();
            try
            {
                panels = GetPanelList(selectedProgramId, selectedSessionId);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(panels, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Application details
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>The view of the Application Details page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ManageApplicationScoring.AccessManageApplicationScoring + "," + Permissions.ManageApplicationScoring.ViewCritique)]
        public ActionResult ApplicationDetails(int sessionPanelId, int panelApplicationId)
        {
            ApplicationDetailsViewModel model = new ApplicationDetailsViewModel();
            try
            {
                int q = ControllerHelpers.GetCurrentUserId(User.Identity);
                CustomIdentity ident = User.Identity as CustomIdentity;
                List<int> commentTypes = new List<int>();
                if (IsValidPermission(Permissions.Note.AccessDiscussion))
                {
                    commentTypes.Add(1);
                }
                if (IsValidPermission(Permissions.Note.AccessAdmin))
                {
                    commentTypes.Add(2);
                }
                if (IsValidPermission(Permissions.Note.AccessGeneral))
                {
                    commentTypes.Add(3);
                }
                // Populate grid data
                var container = theApplicationManagementService.GetApplicationDetails(panelApplicationId, sessionPanelId, commentTypes);
                model = new ApplicationDetailsViewModel(container);
                // Application information
                IApplicationInformationModel appInfo = thePanelManagementService.ListApplicationInformation(sessionPanelId, panelApplicationId);
                model.ApplicationInformationModel = appInfo;
                // Key/legend 
                ClientScoringScaleLegendModel legend = theApplicationManagementService.GetScoringLegendSyncStage(panelApplicationId);
                model.ClientScoringScaleLegend = legend;
                model.IsFromMyWorkspace = !IsValidPermission(Permissions.ManageApplicationScoring.AccessManageApplicationScoring);
                // Back button - due to modal submitting a form, need logic to ensure that the back button is set to the referring url for the page that opened the modal
                model.LastPageUrl = GetBackButtonUrl();
                model.UserId = q;
                // Permissions
                var canEditAssignmentType = IsValidPermission(Permissions.ManageApplicationScoring.EditAssignmentType);
                var canEditScoreCard = IsValidPermission(Permissions.ManageApplicationScoring.EditScoreCard);
                model.CanEditAssignmentType = canEditAssignmentType;
                model.CanEditScoreCard = canEditScoreCard;
                model.CanOnlyViewCritique = IsValidPermission(Permissions.MyWorkspace.ViewCritique);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(model);
        }
        /// <summary>
        /// Opens modal window for user to select the new app status
        /// </summary>
        /// <param name="panelAppId">the panel application id</param>
        /// <param name="currentAppStatus">the panel application id's current status</param>
        /// <param name="applicationId">the application id</param>
        /// <returns>opens the app status modal window</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ManageApplicationScoring.EditScoreStatus)]
        public ActionResult AppStatus(int panelAppId, int currentAppStatus, string applicationId)
        {
            AppStatusViewModel theViewModel = new AppStatusViewModel();
            try
            {
                theViewModel.PanelAppId = panelAppId;
                theViewModel.CurrentStatus = currentAppStatus;
                theViewModel.ApplicationId = applicationId;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.AppStatus, theViewModel);
        }
        /// <summary>
        /// sets the applications status based on the users selection
        /// </summary>
        /// <param name="panelAppId">the panel application id</param>
        /// <param name="appStatusAction">the action the user wants to perform on the application</param>
        /// <returns>the user to the manage application scoring screen</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ManageApplicationScoring.EditScoreStatus)]
        public ActionResult SetAppStatus(int panelAppId, string logNumber, string appStatusAction)
        {
            try
            {
                // Set Status Logic
                if (appStatusAction == AppStatusViewModel.AppStatusActions.Activate)
                {
                    //
                    // If there is no application actively being scored then we can push
                    // the application to "ready to score"
                    //
                    if (theApplicationScoringService.IsAnyApplicationBeingScored(panelAppId) == null)
                    {
                        this.theApplicationScoringService.Active(panelAppId, GetUserId());
                    }
                    else
                    {
                        AlreadyActiveAppViewModel theSubViewModel = new AlreadyActiveAppViewModel();

                        theSubViewModel.CurrentActiveAppId = theApplicationScoringService.IsAnyApplicationBeingScored(panelAppId).PanelApplicationId;
                        theSubViewModel.CurrentActiveAppLogNumber = theApplicationScoringService.IsAnyApplicationBeingScored(panelAppId).LogNumber;
                        theSubViewModel.CurrentActiveAppStatusDescription = theApplicationScoringService.GetApplicationStatus(theSubViewModel.CurrentActiveAppId.GetValueOrDefault(0));
                        theSubViewModel.NewAppId = panelAppId;
                        theSubViewModel.NewAppLogNumber = logNumber;

                        return View(Routing.ManageApplicationScoring.AlreadyActiveApp, theSubViewModel);
                    }
                }
                //Deactivate the application
                else if (appStatusAction == AppStatusViewModel.AppStatusActions.Deactivate)
                {
                    this.theApplicationScoringService.Deactivate(panelAppId, GetUserId());
                }
                //Triage the application
                else if (appStatusAction == AppStatusViewModel.AppStatusActions.Triage)
                {
                    this.theApplicationScoringService.Triage(panelAppId, GetUserId());
                }
                //Champion the application back into discussion
                else if (appStatusAction == AppStatusViewModel.AppStatusActions.Champion)
                {
                    this.theApplicationScoringService.ReadyToScore(panelAppId, GetUserId());
                }
                //Disapprove the application
                else if (appStatusAction == AppStatusViewModel.AppStatusActions.Disapprove)
                {
                    this.theApplicationScoringService.Disapproved(panelAppId, GetUserId());
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            //redirect back to the manage app scoring page
            return RedirectToAction(ActionNames.ManageAppScoring);
        }

        /// <summary>
        /// Opens modal window for user to select a new review status action
        /// </summary>
        /// <param name="panelAppId">the panel application id</param>
        /// <param name="panelUserAssignmentId">the panel user assignment id for the user to change status</param>
        /// <param name="sessionPanelId">Identifier for the current panel</param>
        /// <param name="reviewDiscussionComplete">Whether the discussion has been completed for this application</param>
        /// <returns>opens the rev status modal window</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ManageApplicationScoring.EditAssignmentType)]
        public ActionResult RevStatus(int panelAppId, int panelUserAssignmentId, int sessionPanelId, bool reviewDiscussionComplete)
        {
            RevStatusViewModel theViewModel = new RevStatusViewModel();
            try
            {
                theViewModel.PanelAppId = panelAppId;
                theViewModel.PanelUserAssignmentId = panelUserAssignmentId;
                theViewModel.SessionPanelId = sessionPanelId;
                theViewModel.ReviewDiscussionComplete = reviewDiscussionComplete;
                // Set the COI type dropdown list
                theViewModel.CoiDropdown = thePanelManagementService.GetPanelSessionCoiTypeList(sessionPanelId).ModelList.ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.RevStatus, theViewModel);
        }
        //
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ManageApplicationScoring.EditAssignmentType)]
        public ActionResult SetRevStatus(RevStatusViewModel model)
        {
            try
            {
                //If passes validation call the appropriate service to make the update
                if (model.RevStatusAction == RevStatusViewModel.RevStatusActions.Abstain)
                    theApplicationScoringService.MarkReviewierScoresAsAbstain(model.PanelAppId, model.PanelUserAssignmentId, GetUserId());
                else
                    thePanelManagementService.AssignAsCoi(model.PanelAppId, model.PanelUserAssignmentId, model.ClientCoiTypeId, model.CoiComment, GetUserId());
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            //redirect back to the application details page
            return RedirectToAction(ActionNames.ApplicationDetails, new { panelApplicationId = model.PanelAppId, sessionPanelId = model.SessionPanelId });
        }
        /// <summary>
        /// Deactivating the current app and activating the new app
        /// </summary>
        /// <param name="currentActiveApp">the current app to be deactivated</param>
        /// <param name="newActiveApp">the new app to be activated</param>
        /// <returns>the user to the manage app scoring page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ManageApplicationScoring.ViewScoreboard)]
        public ActionResult DeactivateActivate(int currentActiveApp, int newActiveApp)
        {
            try
            {
                // deactivate the current application
                this.theApplicationScoringService.Deactivate(currentActiveApp, GetUserId());
                //activate the new application
                this.theApplicationScoringService.Active(newActiveApp, GetUserId());
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return RedirectToAction(ActionNames.ManageAppScoring);
        }
        /// <summary>
        /// the scoreboard for a specific panel
        /// </summary>
        /// <param name="panelId">the SessionPanel entity identifier</param>
        /// <returns>the user to the scoreboard page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ManageApplicationScoring.ViewScoreboard)]
        public ActionResult Scoreboard(int panelId)
        {
            ScoreboardViewModelNew theViewModel = new ScoreboardViewModelNew();
            try
            {
                theViewModel.PanelId = panelId;

                if (theApplicationScoringService.IsAnyApplicationBeingScoredByPanel(panelId) == null)
                {
                    theViewModel.PanelAppId = 0;
                    theViewModel.DisplayContent = ScoreboardViewModelNew.Constants.NoActiveApp;
                }
                else
                {
                    theViewModel.PanelAppId = theApplicationScoringService.IsAnyApplicationBeingScoredByPanel(panelId).PanelApplicationId.GetValueOrDefault(0);
                    theViewModel.AppLogNumber = theApplicationScoringService.IsAnyApplicationBeingScoredByPanel(panelId).LogNumber;

                    theViewModel.CurrentAppStatus = theApplicationScoringService.GetApplicationStatus(theViewModel.PanelAppId);
                    if (theViewModel.CurrentAppStatus == "Active" || theViewModel.CurrentAppStatus == "Scoring")
                    {
                        //get COI data
                        theViewModel.DisplayContent = (theViewModel.CurrentAppStatus == "Active") ? ScoreboardViewModelNew.Constants.ActiveApp :
                                ScoreboardViewModelNew.Constants.ScoringApp;
                        theViewModel.ApplicationInformation = thePanelManagementService.GetApplicationInformation(theViewModel.PanelAppId);
                        theViewModel.CoiList = theApplicationScoringService.ListApplicationCoi(theViewModel.PanelAppId).ModelList.ToList();
                        //get scoreboard data
                        theViewModel.ScoreboardScores = theApplicationScoringService.GetApplicationPreMtgScoresForReviewer(theViewModel.PanelAppId);
                        theViewModel.PiName = theViewModel.ApplicationInformation.Blinded ? ViewHelpers.Constants.Blinded :
                            ViewHelpers.ConstructNameWithComma(theViewModel.ApplicationInformation.PiFirstName,
                            theViewModel.ApplicationInformation.PiLastName);
                    }
                    else
                    {
                        theViewModel.DisplayContent = ScoreboardViewModelNew.Constants.NoActiveApp;
                    }
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(theViewModel);
        }
        /// <summary>
        /// moves user to the scoreboard but moves app to scoring first
        /// </summary>
        /// <param name="panelAppId">the active panel app id</param>
        /// <returns>user to the scoreboard page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ManageApplicationScoring.ViewScoreboard)]
        public ActionResult ProceedToScoring(int panelAppId)
        {
            bool result = false;
            try
            {
                theApplicationScoringService.Scoring(panelAppId, GetUserId());
                result = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// ends scoring for current application and moves the user back to scoreboard
        /// </summary>
        /// <param name="panelId">the panel id</param>
        /// <param name="panelAppId">the active panel app id</param>
        /// <returns>user to the scoreboard page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.ManageApplicationScoring.ViewScoreboard)]
        public ActionResult EndScoring(int panelId, int panelAppId)
        {
            try
            {
                theApplicationScoringService.Deactivate(panelAppId, GetUserId());
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return RedirectToAction(Routing.MyWorkspace.Scoreboard, new { panelId = panelId, panelAppId = panelAppId });
        }

        /// <summary>
        /// The Admin Notes modal
        /// </summary>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Note.AccessAdmin)]
        public ActionResult AdminNotes(int applicationId)
        {
            var model = new AdminNoteViewModel();
            try
            {
                var adminBudgetNote = theApplicationManagementService.GetApplicationAdminBudgetNote(applicationId);
                model = new AdminNoteViewModel(adminBudgetNote, applicationId);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.AdminNotes, model);
        }
        /// <summary>
        /// Saves the Adminastration note.
        /// </summary>
        /// <param name="applicationBudgetId">ApplicationBudget entity identifier.  (0 if none exists)</param>
        /// <param name="applicationId">Containing Application entity container identifier</param>
        /// <param name="note">The "note" to save</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.Note.AccessAdmin)]
        public ActionResult SaveAdminNote(int applicationBudgetId, int applicationId, string note)
        {
            note = note.Trim();
            bool isSuccessful = false;
            try
            {
                theApplicationManagementService.AddModifyAdminNote(applicationBudgetId, applicationId, note, GetUserId());
                isSuccessful = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(isSuccessful);
        }
        /// <summary>
        /// Add a user comment
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="comments">User's comments</param>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>Redirected to Application Details controller action</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult AddComment(int userId, string comments, int commentType, int sessionPanelId, int panelApplicationId)
        {
            try
            {
                theApplicationManagementService.AddComment(userId, panelApplicationId, comments, commentType);
            }
            catch (Exception e)
            {
                ///
                /// Mark the model as invalid.
                /// 
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                ModelState.AddModelError(string.Empty, Constants.Messages.FailedToSaveComment);
            }
            return RedirectToAction(Routing.ManageApplicationScoring.ApplicationDetails, Routing.P2rmisControllers.ManageApplicationScoring, new { sessionPanelId = sessionPanelId, panelApplicationId = panelApplicationId });
        }
        /// <summary>
        /// Edit a user's comment
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="commentId">Comment identifier</param>
        /// <param name="comments">User's comments</param>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>Redirected to Application Details controller action</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult EditComment(int userId, int commentId, string comments, string applicationId, int commentType, int sessionPanelId, int panelApplicationId)
        {
            try
            {
                theApplicationManagementService.EditComment(userId, commentId, comments, commentType);
            }
            catch (Exception e)
            {
                ///
                /// Mark the model as invalid.
                /// 
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                ModelState.AddModelError(string.Empty, Constants.Messages.FailedToEditComment);
            }
            return RedirectToAction(Routing.ManageApplicationScoring.ApplicationDetails, Routing.P2rmisControllers.ManageApplicationScoring, new { sessionPanelId = sessionPanelId, panelApplicationId = panelApplicationId });
        }
        /// <summary>
        /// Delete a user's comment
        /// </summary>
        /// <param name="commentId">Comment identifier</param>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>Redirected to Application Details controller action</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult DeleteComment(int commentId, int sessionPanelId, int panelApplicationId)
        {
            try
            {
                theApplicationScoringService.DeleteComment(commentId, GetUserId());
            }
            catch (Exception e)
            {
                ///
                /// Mark the model as invalid.
                /// 
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                ModelState.AddModelError(string.Empty, Constants.Messages.FailedToDeleteComment);
            }
            return RedirectToAction(Routing.ManageApplicationScoring.ApplicationDetails, Routing.P2rmisControllers.ManageApplicationScoring, new { sessionPanelId = sessionPanelId, panelApplicationId = panelApplicationId });
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Set filter menu data to the view model
        /// </summary>
        /// <param name="model">The ManageApplicationScoringViewModel model</param>
        /// <returns></returns>
        private ManageApplicationScoringViewModel SetFilterMenu(ManageApplicationScoringViewModel model)
        {
            CustomIdentity ident = User.Identity as CustomIdentity;
            int userId = ident.UserID;
            List<int> clientList = GetUsersClientList();
            var canViewAssignedPanels = IsValidPermission(Permissions.ManageApplicationScoring.ViewOnlineScoringAssignedPanels);
            var canViewAllPanels = IsValidPermission(Permissions.ManageApplicationScoring.ViewOnlineScoringAllPanels);
            var canViewScoreboard = IsValidPermission(Permissions.ManageApplicationScoring.ViewScoreboard);
            var canEditScoreStatus = IsValidPermission(Permissions.ManageApplicationScoring.EditScoreStatus);
            if (canViewAssignedPanels)
            {
                // One drop-down
                var panels = theApplicationManagementService.ListAdminUsersOpenPanels(userId);
                model = new ManageApplicationScoringViewModel(panels.ModelList.ToList());
                model.CanViewAssignedPanels = canViewAssignedPanels;
            }
            else if (canViewAllPanels)
            {
                // Three drop-downs
                var op = theApplicationManagementService.GetOpenProgramsList(clientList);
                model = new ManageApplicationScoringViewModel(op);
                model.CanViewAllPanels = canViewAllPanels;
            }
            model.CanViewScoreboard = canViewScoreboard;
            model.CanEditScoreStatus = canEditScoreStatus;
            return model;
        }
        /// <summary>
        /// Get session list
        /// </summary>
        /// <param name="selectedProgramId">The selected program identifier</param>
        /// <returns>A list of sessions</returns>
        private List<SessionView> GetSessionList(int selectedProgramId) {
            int userId = GetUserId();
            CustomIdentity ident = User.Identity as CustomIdentity;
            var hasAllPanelsPermission = IsValidPermission(Permissions.ManageApplicationScoring.ViewOnlineScoringAllPanels);
            SessionDetailView view = theSessionService.GetAllSessionsDetails(selectedProgramId, userId, hasAllPanelsPermission);
            var sessions = view.Sessions.ToList<SessionView>();
            return sessions;
        }
        /// <summary>
        /// Get panel list
        /// </summary>
        /// <param name="selectedProgramId">The selected program identifier</param>
        /// <param name="selectedSessionId">The selected session identifier</param>
        /// <returns>A list of panels</returns>
        private List<PanelView> GetPanelList(int selectedProgramId, int selectedSessionId)
        {
            int userId = GetUserId();
            CustomIdentity ident = User.Identity as CustomIdentity;
            var hasAllPanelsPermission = IsValidPermission(Permissions.ManageApplicationScoring.ViewOnlineScoringAllPanels);
            SessionDetailView view = theSessionService.GetPanelAndMechanismDetailsForSelectedSession(selectedProgramId, selectedSessionId, userId, hasAllPanelsPermission);
            var panels = view.Panels.ToList<PanelView>();
            return panels;
        }

        /// <summary>
        /// Populates model grids based on ids
        /// </summary>
        /// <param name="programId">the program id</param>
        /// <param name="sessionId">the session id</param>
        /// <param name="panelId">the panel id</param>
        /// <param name="userId">the user id</param>
        /// <param name="model">the view model</param>
        /// <returns>the view model populated with grid data</returns>
        private ManageApplicationScoringViewModel PopulateManageAppScoringGrids(int programId, int sessionId, int panelId, int userId, ManageApplicationScoringViewModel model)
        {
            CustomIdentity ident = User.Identity as CustomIdentity;
            if (panelId > 0)
            {
                var serviceData = theSessionService.GetSessionDetailsForPanel(programId, panelId, model.CanViewAllPanels, userId);
                model.MechanismCounts = serviceData.Mechanisms.ToList();
                model.PanelDetails = serviceData.ViewPanelDetails.ToList();
            }
            else if (sessionId > 0)
            {
                model.MechanismCounts = theSessionService.GetPanelAndMechanismDetailsForSelectedSession(programId, sessionId, userId,
                    model.CanViewAllPanels).Mechanisms.ToList();
            }
            else if (programId > 0)
            {
                model.MechanismCounts = theSessionService.GetAllSessionsDetails(programId, userId, model.CanViewAllPanels).Mechanisms.ToList();
            }
            model.CanAccessAdminNote = IsValidPermission(Permissions.Note.AccessAdmin);
            model.CanShowLinks = !theApplicationScoringService.IsSessionPanelOver(panelId);
            return model;
        }
        /// <summary>
        /// Retrieves values from session
        /// </summary>
        /// <param name="programYearId">Program identifier</param>
        /// <param name="sessionId">Meeting identifier</param>
        /// <param name="panelId">SessionPanel entity identifier</param>
        internal override void GetFromSession(ref int? programYearId, ref int? sessionId, ref int? panelId)
        {
            int? thePanelId = (int?)(Session[SessionVariables.PanelId]);

            if ((thePanelId.HasValue) && (!thePanelManagementService.IsOnline(thePanelId.Value)))
            {
                programYearId = (int?)Session[SessionVariables.ProgramYearId];
                sessionId = (int?)(Session[SessionVariables.SessionId]);
                panelId = (int?)(Session[SessionVariables.PanelId]);
            }
        }
        /// <summary>
        /// Checks panel visibility.
        /// </summary>
        /// <param name="model">ManageApplicationScoringViewModel model</param>
        internal virtual void CheckPanelVisibility(ManageApplicationScoringViewModel model)
        {
            if (!model.CanViewAssignedPanels && !model.CanViewAllPanels)
            {
                throw new NotSupportedException();
            }
        }
        #endregion
    }
}