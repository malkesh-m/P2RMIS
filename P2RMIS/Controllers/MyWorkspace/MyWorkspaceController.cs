using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.ApplicationManagement;
using Sra.P2rmis.Bll.ApplicationScoring;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Bll.HotelAndTravel;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.Web.ViewModels.MyWorkspace;
using Sra.P2rmis.Web.ViewModels.SummaryStatement;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.Web.ViewModels;
using Newtonsoft.Json;

namespace Sra.P2rmis.Web.Controllers.MyWorkspace
{
    /// <summary>
    /// Controller for P2RMIS My Workspace controller.  
    /// </summary>
    public partial class MyWorkspaceController : MyWorkspaceBaseController
    {
        /// <summary>
        /// Enable/Disable the CurrentAssignment link
        /// </summary>
        private const bool EnableCurrentAssignment = true;
        private const bool DisableCurrentAssignment = false;
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private MyWorkspaceController()
        {
        }
        /// <summary>
        /// My Workspace constructor.
        /// </summary>
        /// <param name="theApplicationManagementService">Application Management service</param>
        /// <param name="theApplicationScoringService">Application Scoring Service</param>
        /// <param name="thePanelManagementService">Panel Management service</param>
        /// <param name="theUserProfileManagementService">User Profile Management service</param>
        /// <param name="theFileService">The file service.</param>
        /// <param name="theWorkflowService">The Workflow service</param>
        /// <param name="theMailService">The mail service</param>
        /// <param name="theHotelAndTravelService">The Hotel/Travel service</param>
        /// <param name="theLookupService">The lookup service</param>
        /// <param name="theSessionService">The session service</param>
        public MyWorkspaceController(IApplicationManagementService theApplicationManagementService, IApplicationScoringService theApplicationScoringService,
            IPanelManagementService thePanelManagementService,
            IUserProfileManagementService theUserProfileManagementService, IFileService theFileService, IWorkflowService theWorkflowService,
            IMailService theMailService, IHotelAndTravelService theHotelAndTravelService, ILookupService theLookupService, ISessionService theSessionService)
        {
            this.theApplicationManagementService = theApplicationManagementService;
            this.theApplicationScoringService = theApplicationScoringService;
            this.thePanelManagementService = thePanelManagementService;
            this.theUserProfileManagementService = theUserProfileManagementService;
            this.theFileService = theFileService;
            this.theWorkflowService = theWorkflowService;
            this.theMailService = theMailService;
            this.theHotelAndTravelService = theHotelAndTravelService;
            this.theLookupService = theLookupService;
            this.theSessionService = theSessionService;
        }
        /// <summary>
        /// Get My Workspace view
        /// </summary>
        /// <returns>The view of the application scores</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.AccessMyWorkspace)]
        public ActionResult Index()
        {
            MyWorkspaceViewModel model = new MyWorkspaceViewModel();
            try
            {
                int userId = GetUserId();
                var sessionPanels = theApplicationManagementService.ListUsersOpenPanels(userId).ModelList.ToList();

                int sessionPanelId = GetPanelSession();
                if (sessionPanelId <= 0 && sessionPanels.Count == 1)
                {
                    sessionPanelId = sessionPanels[0].Index;
                }
                if (sessionPanelId > 0)
                {
                    SetPanelSession(sessionPanelId);

                    var panelStatus = theApplicationManagementService.OpenPanelStatus(sessionPanelId, userId);
                    var isClientChair = theApplicationScoringService.IsCpritChairPerson(sessionPanelId, userId);

                    if (isClientChair)
                    {
                        var applications = GetChairApplications(sessionPanelId, userId);
                        model = new MyWorkspaceViewModel(applications, panelStatus, isClientChair);
                    }
                    else
                    {
                        var applications = GetApplications(panelStatus, sessionPanelId, userId);
                        model = new MyWorkspaceViewModel(applications, panelStatus, isClientChair);
                    }
                    model.SessionPanelId = sessionPanelId;
                    //Check that user's registration is complete
                    if (!CanUserAccessPanel(sessionPanelId))
                    {
                        model.NotRegistered = true;
                    }
                    model.ClientId = panelStatus.ClientId;
                }

                model.SessionPanels = sessionPanels;
                model.SessionPanelId = sessionPanelId;
                model.UserId = userId;
                model.LastPageUrl = GetBackButtonUrl();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                throw e;
            }
            //Set the tab navigation
            model.SetTabContext(GetUserId(), model.UserId, null, true, true, HasPermission(Permissions.MyWorkspace.AccessMyWorkspace));
            model.SetMenuTitle(GetUserId(), model.UserId, HasPermission(Permissions.MyWorkspace.AccessMyWorkspace));
            return View(model);
        }
        /// <summary>
        /// View or edit critique
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <param name="sessionPanelId">The session panel identifier</param>
        /// <returns>The view of the Critique page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.ScoreApplications + "," + Permissions.MyWorkspace.ViewCritique)]
        public ActionResult Critique(int panelApplicationId, int sessionPanelId)
        {
            CritiqueViewModel model = new CritiqueViewModel();
            if (!CanUserAccessPanel(sessionPanelId))
            {
                return RedirectToAction("ViewParticipationHistory", "UserProfileManagement", new { sessionPanelId = sessionPanelId });
            }
            try
            {
                var userId = GetUserId();
                var showCritiqueIcons = theApplicationScoringService.ShowCritiqueIcons(panelApplicationId, sessionPanelId, userId);
                var reviewerCritiques = theApplicationScoringService.ListApplicationCritiquesForApplicationEvaluation(panelApplicationId, userId, false).ModelList.ToList();
                model = ConstructCritiqueViewModel(showCritiqueIcons, panelApplicationId, sessionPanelId, userId, reviewerCritiques, EnableCurrentAssignment);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                throw e;
            }
            return View(model);
        }
        /// <summary>
        /// View or edit critique
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <param name="sessionPanelId">The session panel identifier</param>
        /// <returns>The view of the Critique page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.ScoreApplications + "," + Permissions.MyWorkspace.ViewCritique)]
        public ActionResult ViewCritiqueFromApplicationScoring(int panelApplicationId, int sessionPanelId, int targetUserId)
        {
            CritiqueViewModel model = new CritiqueViewModel();
            if (!CanUserAccessPanel(sessionPanelId))
            {
                return RedirectToAction("ViewParticipationHistory", "UserProfileManagement", new { sessionPanelId = sessionPanelId });
            }
            try
            {
                var userId = GetUserId();
                var reviewerCritiques = theApplicationScoringService.ListApplicationCritiquesForApplicationEvaluation(panelApplicationId, userId, IsValidPermission(Permissions.ManageApplicationScoring.EditScoreCard)).ModelList.ToList();
                var showCritiqueIcons = theApplicationScoringService.ShowCritiqueIcons(panelApplicationId, sessionPanelId, userId);
                model = ConstructCritiqueViewModel(showCritiqueIcons, panelApplicationId, sessionPanelId, userId, reviewerCritiques, EnableCurrentAssignment);
                model.SelectThisReviewer(targetUserId);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                throw e;
            }
            return View(Routing.MyWorkspace.Critique, model);
        }
        /// <summary>
        /// View or edit critique
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <param name="sessionPanelId">The session panel identifier</param>
        /// <param name="targetUserId">Reviewer User entity identifier</param>
        /// <returns>The view of the Critique page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.ScoreApplications + "," + Permissions.MyWorkspace.ViewCritique)]
        public ActionResult ProcessIncompleteCritiqueFromApplicationScoring(int panelApplicationId, int sessionPanelId, int targetUserId)
        {
            CritiqueViewModel model = new CritiqueViewModel();
            if (!CanUserAccessPanel(sessionPanelId))
            {
                return RedirectToAction(Routing.UserProfileManagement.ViewParticipationHistory, Routing.P2rmisControllers.UserProfileManagement, new { sessionPanelId = sessionPanelId });
            }
            try
            {
                var userId = GetUserId();
                var reviewerCritiques = theApplicationScoringService.ListApplicationCritiquesForApplicationEvaluation(panelApplicationId, targetUserId, IsValidPermission(Permissions.ManageApplicationScoring.EditScoreCard)).ModelList.ToList();
                var showCritiqueIcons = theApplicationScoringService.ShowCritiqueIcons(panelApplicationId, sessionPanelId, userId);
                model = ConstructCritiqueViewModel(showCritiqueIcons, panelApplicationId, sessionPanelId, userId, reviewerCritiques, EnableCurrentAssignment);
                model.SelectThisReviewer(targetUserId);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                throw e;
            }
            return View(Routing.MyWorkspace.Critique, model);
        }
        /// <summary>
        /// View a specific users critique.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <param name="applicationWorkflowStepId">ApplicationWorkflowStep entity identifier</param>
        /// <param name="targetController">Destination controller</param>
        /// <param name="targetAction">Destination controller action</param>
        /// <returns>The view of the Critique page for the specified user.</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.ScoreApplications)]
        public ActionResult ViewUsersCritique(int panelApplicationId, int sessionPanelId, int userId, int applicationWorkflowStepId, string targetController, string targetAction)
        {
            CritiqueViewModel model = new CritiqueViewModel();

            try
            {
                int currentUser = GetUserId();
                bool isCurrentUser = currentUser == userId;
                bool hasManageCritiquesPermission = HasPermission(Permissions.PanelManagement.ManagePanelCritiques);
                bool hasViewCritiquesPermission = HasPermission(Permissions.PanelManagement.ViewPanelCritiques);
                var reviewerCritiques = theApplicationScoringService.ListApplicationCritiqueForPanelManagement(panelApplicationId, userId, applicationWorkflowStepId, isCurrentUser, hasManageCritiquesPermission, hasViewCritiquesPermission).ModelList.ToList();
                var showCritiqueIcons = theApplicationScoringService.ShowCritiqueIcons(panelApplicationId, sessionPanelId, currentUser);
                model = ConstructCritiqueViewModel(showCritiqueIcons, panelApplicationId, sessionPanelId, userId, reviewerCritiques, DisableCurrentAssignment, targetController, targetAction);
                SetActiveLogNumber(model.ApplicationInformation.LogNumber);
                //
                // The Critique view is displayed from several pages, from management pages & user pages.
                // Several links are shown only when the Critique is shown for the user.  The links are controlled
                // by the IsReviewer flag which is set based on a retrieval from the service layer.  Since this controller
                // method is only called from the the Critiques tab in PanelManagement, the links should not be displayed.
                // we reset it to ensure the links are not displayed.
                // 
                model.SetIsReviewerForPanelmanagement();

                for (int i = 0; i < model.ReviewerCritiquesList.Count; i++)
                {
                    if (model.ReviewerCritiquesList[i].IsCurrentUser)
                    {
                        model.CanSubmit = model.ReviewerCritiquesList[i].CanEdit;
                        break;
                    }

                }

            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                throw e;
            }
            return View(Routing.MyWorkspace.Views.Critique, model);
        }
        /// <summary>
        /// Populates a CritiqueViewModel.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <param name="reviewerCritiques">List of reviewer's critiques</param>
        /// <param name="isCurrentAssignmentEnabled"></param>
        /// <param name="targetController">Destination controller</param>
        /// <param name="targetAction">Destination controller action</param>
        /// <returns>Populated CritiqueViewModel</returns>
        internal virtual CritiqueViewModel ConstructCritiqueViewModel(CritiqueIconControlModel showCritiqueIcons, int panelApplicationId, int sessionPanelId, int userId, List<IReviewerCritiques> reviewerCritiques, bool isCurrentAssignmentEnabled, string targetController, string targetAction)
        {
            CritiqueViewModel model = ConstructCritiqueViewModel(showCritiqueIcons, panelApplicationId, sessionPanelId, userId, reviewerCritiques, isCurrentAssignmentEnabled);
            model.LastPageUrl = Url.Action(targetAction, targetController);
            return model;
        }
        /// <summary>
        /// Populates a CritiqueViewModel.
        /// </summary>
        /// <param name="showCritiqueIcons">Model for enabling/disabling the critique icons</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <param name="reviewerCritiques">List of reviewer's critiques</param>
        /// <param name="isCurrentAssignmentEnabled"></param>
        /// <returns>Populated CritiqueViewModel</returns>
        internal virtual CritiqueViewModel ConstructCritiqueViewModel(CritiqueIconControlModel showCritiqueIcons, int panelApplicationId, int sessionPanelId, int userId, List<IReviewerCritiques> reviewerCritiques, bool isCurrentAssignmentEnabled)
        {
            CritiqueViewModel model = new CritiqueViewModel();
            var appInfo = thePanelManagementService.ListApplicationInformation(sessionPanelId, panelApplicationId);
            var critiquePhase = theApplicationScoringService.GetCritiqueStageStep(panelApplicationId);
            var critiqueReviewerOrder = theApplicationScoringService.GetCritiqueReviewerOrder(panelApplicationId);
            var status = theApplicationScoringService.GetApplicationStatus(panelApplicationId);
            var modStatus = theApplicationScoringService.PanelApplicationMODStatus(panelApplicationId);

            var userDiscussionAccess = theApplicationScoringService.IsUserDiscussionParticipant(modStatus.ApplicationStageStepId, userId);
            ClientScoringScaleLegendModel clientScoringScaleLegend = theApplicationManagementService.GetScoringLegendCurrentStage(panelApplicationId);

            model = new CritiqueViewModel(reviewerCritiques, appInfo, critiquePhase, critiqueReviewerOrder, showCritiqueIcons,
                isCurrentAssignmentEnabled, modStatus, clientScoringScaleLegend, panelApplicationId, sessionPanelId);
            model.CanAccessDiscussionBoard =
                OnLineDiscussions.AccessDiscussionBoard(userDiscussionAccess, modStatus.IsModActive, modStatus.IsModClosed, modStatus.IsModReady, modStatus.IsModPhase);
            model.ApplicationStatus = status;
            model.PanelApplicationId = panelApplicationId;
            model.SessionPanelId = sessionPanelId;
            model.UserId = userId;
            model.LastPageUrl = (Request.UrlReferrer != null) ? Request.UrlReferrer.AbsoluteUri :
                Url.Action(Routing.MyWorkspace.Index, Routing.P2rmisControllers.MyWorkspace);
            return model;
        }

        /// <summary>
        /// Set session panel identifier into the application session
        /// </summary>
        /// <param name="sessionPanelId">the SessionPanel identifier</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.AccessMyWorkspace)]
        public JsonResult SetSessionPanel(int sessionPanelId)
        {
            bool isSuccessful = false;
            try
            {
                SetPanelSession(sessionPanelId);
                isSuccessful = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(isSuccessful, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Score card
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <param name="sessionPanelId">The session panel identifier</param>
        /// <param name="reviewerId">The reviewer's user identifier</param>
        /// <returns>The view of the Scorecard page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.ScoreApplications)]
        public ActionResult Scorecard(int panelApplicationId, int sessionPanelId, int? reviewerId)
        {
            //TODO: Another check should be added to verify users are not submitting scores for behalf of another user on the same panel unless they have the approrpriate system permissions
            if (!CanUserAccessPanel(sessionPanelId))
                return RedirectToAction("ViewParticipationHistory", "UserProfileManagement", new { sessionPanelId = sessionPanelId });
            ScorecardViewModel model = new ScorecardViewModel();
            try
            {
                var userId = (reviewerId != null) ? (int)reviewerId : GetUserId();
                model = PopulateScoreCardModel(model, sessionPanelId, panelApplicationId, userId);
                model.LastPageUrl = Request.UrlReferrer.AbsoluteUri;
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
        /// Saves the reviewers critique.
        /// </summary>
        /// <param name="applicationWorkflowStepElementContentId"></param>
        /// <param name="contentText">Critique text.  Can be empty string or null.</param>
        /// <returns>True if critique was saved; false otherwise</returns>
        [ValidateInput(false)]//this line is so that the application wont throw an error when trying to submit html back as a variable        
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.ScoreApplications)]
        public JsonResult SaveReviewersCritique(int applicationWorkflowStepElementContentId, string contentText)
        {
            bool isSuccessful = false;
            try
            {
                this.theApplicationScoringService.SaveReviewersCritique(applicationWorkflowStepElementContentId, contentText, GetUserId());
                isSuccessful = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(isSuccessful, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Saves the reviewer's critique and score.
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">Application workflow step element identifier</param>
        /// <param name="applicationWorkflowStepElementContentId">Application workflow step element content identifier</param>
        /// <param name="contentText">Critique text.  Can be empty string or null.</param>
        /// <param name="score">Score</param>
        /// <param name="abstain">Flag to indicate if it is abstain</param>
        /// <returns>Result in JSON format.</returns>
        [ValidateInput(false)]//this line is so that the application wont throw an error when trying to submit html back as a variable   
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult SaveReviewersCritiqueAndScore(int applicationWorkflowStepElementId, int applicationWorkflowStepElementContentId, string contentText,
                string score, string scoreType, string adjectival, bool abstain, bool isPanelStarted, string errorMessage)
        {
            object returnData;
            try
            {
                score = (abstain || scoreType == string.Empty) ? null : score;
                decimal? scoreDecimal = string.IsNullOrEmpty(score) ? null : (decimal?)Convert.ToDecimal(score);
                string scoreDisplay = ViewHelpers.ScoreFormatterNotCalculatedWithAbstain(scoreDecimal, scoreType, adjectival, abstain);
                // Validate score
                // First, make sure it's not submitted
                var isResolved = this.theApplicationScoringService.IsResolved(applicationWorkflowStepElementId);

                if (isResolved)
                {
                    errorMessage = MessageService.FailedToSaveCritiqueBecauseCritiqueWasSubmitted(applicationWorkflowStepElementContentId, GetUserId());
                    returnData = new { success = false, message = errorMessage, issubmitted = true };
                    return Json(returnData, JsonRequestBehavior.AllowGet);

                }

                var scoreStatus = theApplicationScoringService.IsRatingValid(applicationWorkflowStepElementId, scoreDecimal, isPanelStarted);
                var isScoreValid = abstain ? true : scoreStatus.IsRatingValid;
                if (isScoreValid)
                {
                    var saveResult = this.theApplicationScoringService.SaveReviewersCritiquePostAssignment(applicationWorkflowStepElementId, applicationWorkflowStepElementContentId, contentText,
                        scoreDecimal, abstain, isPanelStarted, GetUserId(), errorMessage);
                    errorMessage = saveResult.ErrorMessage;
                    returnData = new
                    {
                        success = true,
                        content = contentText,
                        elementId = saveResult.ApplicationWorkflowStepElementId,
                        score = score,
                        scoreDisplay = scoreDisplay,
                        abstain = abstain,
                        contentId = saveResult.ApplicationWorkflowStepElementContentId
                    };
                }
                else
                {
                    returnData = new { success = false, message = scoreStatus.ErrorMessage };
                }
                if (errorMessage != null)
                {
                    returnData = new { success = false, message = errorMessage};
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                returnData = new { success = false };
            }
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Checks if the reviewer's critique already submitted.
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">Application workflow step element identifier</param>
        /// <param name="applicationWorkflowStepElementContentId">Application workflow step element content identifier</param>
        /// <returns>Result in JSON format.</returns>
        [ValidateInput(false)]  
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult IsCritiquesAlreadySubmitted(int applicationWorkflowStepElementId, int applicationWorkflowStepElementContentId)
        {
            object returnData = new { success = true, message = String.Empty, issubmitted = false }; ;
            try
            {
                // First, make sure it's not submitted
                var isResolved = this.theApplicationScoringService.IsResolved(applicationWorkflowStepElementId);

                if (isResolved)
                {
                    string errorMessage = MessageService.FailedToSaveCritiqueBecauseCritiqueWasSubmitted(applicationWorkflowStepElementContentId, GetUserId());
                    returnData = new { success = true, message = errorMessage, issubmitted = true };
                    return Json(returnData, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                returnData = new { success = false };
            }
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Submits the critique.
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="applicationWorkflowElementId">The application workflow step element identifier.</param>
        /// <returns>Result in JSON format.</returns>
        /// <remarks>TODO: permission</remarks>
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult SubmitCritique(int applicationWorkflowId, int applicationWorkflowStepElementId)
        {
            object returnData;
            try
            {
                    theWorkflowService.ExecuteSubmitWorkflow(applicationWorkflowId, GetUserId());
                    returnData = new { success = true };
                
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                returnData = new { success = false };
            }
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Submits the critique.
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier</param>
        /// <param name="applicationWorkflowElementId">The application workflow step element identifier.</param>
        /// <returns>Result in JSON format.</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult CanSubmitCritique(int applicationWorkflowId, int applicationWorkflowStepElementId)
        {
            object returnData;
            try
            {
                var hasOverall = theApplicationScoringService.HasOverall(applicationWorkflowStepElementId);
                if (hasOverall)
                {
                    var incompleteCritiqueModels = theApplicationScoringService.CanSubmit(applicationWorkflowStepElementId).ModelList.ToList();
                    var hasIncompleteCritiqueModels = incompleteCritiqueModels.Count > 0;
                    if (!hasIncompleteCritiqueModels)
                    {
                        var isResolved = this.theApplicationScoringService.IsResolved(applicationWorkflowStepElementId);
                        if (isResolved == true)
                        {
                            //include code = 1 in the return result if critiques are already submitted 
                            returnData = new { success = false, code = 1 };
                        }
                        else
                        {
                            returnData = new { success = true };
                        }
                    }
                    else
                    {
                        List<IncompleteCritiqueViewModel> incompleteCritiques = incompleteCritiqueModels.ConvertAll(o => new IncompleteCritiqueViewModel(o));
                        returnData = new { success = false, isCritiqueIncomplete = true, incompleteCritiques = incompleteCritiques };
                    }
                }
                else
                {
                    returnData = new { success = false, isOverallIncomplete = true };
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                returnData = new { success = false };
            }
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Set abstain to elements
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="applicationWorkflowElementId">The application workflow step element identifier.</param>
        /// <param name="elementsToAbstain">The identifiers of elements to abstain</param>
        /// <returns>Result in JSON format.</returns>
        /// <remarks>TODO: permission</remarks>
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult SetAbstains(int applicationWorkflowId, int applicationWorkflowStepElementId, List<int> elementsToAbstain)
        {
            object returnData;
            try
            {
                var hasElementsToAbstain = elementsToAbstain != null && elementsToAbstain.Count > 0;
                if (hasElementsToAbstain)
                {
                    var abstainResult = theApplicationScoringService.SaveReviewersIncompleteCritiquePostAssignmentAsAbstains(elementsToAbstain, GetUserId()).ModelList.ToList();
                    var elementsAbstained = abstainResult.ConvertAll(o => new AbstainedElementViewModel(o));
                    returnData = new { success = true, elementsAbstained = elementsAbstained };
                }
                else
                {
                    returnData = new { success = false, isParameterMissing = true };
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                returnData = new { success = false };
            }
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// View application comments
        /// </summary>
        /// <param name="applicationId">The application identifier</param>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>The comments in JSON format</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ViewComments(int applicationId, int panelApplicationId)
        {
            NotesViewModel theViewModel = new NotesViewModel();
            try
            {
                CritiqueIconControlModel iconControlModel = theApplicationScoringService.ShowCritiqueIcons(panelApplicationId, GetUserId());

                var results = theApplicationScoringService.GetApplicationComments(panelApplicationId);
                theViewModel.Notes = new List<ISummaryCommentModel>(results.ModelList);
                theViewModel.CanAddEditComments = iconControlModel.CanAddEditComments;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            //
            // return the populated view model
            //
            return Json(theViewModel, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Adds the supplied comment to the indicated application for the current user
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="panelApplicationID">The panel application identifier.</param>
        /// <returns>The result message.</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult AddComment(string comment, int panelApplicationID)
        {
            var result = Routing.MyWorkspace.SaveUnsuccessful;
            try
            {
                theApplicationScoringService.AddComment(panelApplicationID, comment, GetUserId());
                result = Routing.MyWorkspace.SaveSuccessful;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Content(result);
        }
        /// <summary>
        /// Edits the supplied comment for the current user
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns>The result message.</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult EditComment(string comment, int commentId)
        {
            var result = Routing.MyWorkspace.SaveUnsuccessful;
            try
            {
                theApplicationScoringService.EditComment(commentId, comment, GetUserId());
                result = Routing.MyWorkspace.SaveSuccessful;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Content(result);
        }
        /// <summary>
        /// Deleted the comment for the current user
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns>The result message.</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult DeleteComment(int commentId)
        {
            var result = Routing.MyWorkspace.SaveUnsuccessful;
            try
            {
                theApplicationScoringService.DeleteComment(commentId, GetUserId());
                result = Routing.MyWorkspace.SaveSuccessful;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Content(result);
        }
        /// <summary>
        /// the reviewer scoreboard action
        /// </summary>
        /// <param name="panelApplicationId">the panel application id</param>
        /// <returns>the reviewer scoreboard view</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.ScoreApplications)]
        public ActionResult ReviewerScoreboard(int panelApplicationId)
        {
            return View();
        }
        /// <summary>
        ///  Save the scorecard data.
        /// </summary>
        /// <param name="model">ScorecardViewModel containing user entered data</param>
        /// <returns>To ScoreCard view if an invalid data entered; Redirected to Index view if successful</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult SaveScore(ScorecardViewModel model)
        {
            ActionResult result = null;
            if (ModelState.IsValid)
            {
                try
                {
                    //
                    // Let the service layer save the scores
                    //
                    theApplicationScoringService.SaveScore(model.CriterionScores, model.OverallScores, model.PanelApplicationId, model.UserId, IsValidPermission(Permissions.ManageApplicationScoring.EditScoreCard));
                    result = (string.IsNullOrWhiteSpace(model.LastPageUrl)) ? (ActionResult)RedirectToAction(Routing.MyWorkspace.Index, Routing.P2rmisControllers.MyWorkspace)
                                                                            : Redirect(model.LastPageUrl);
                }
                catch (ScoringException e)
                {
                    HandleExceptionViaElmah(e);
                    model = ResetModel(model);
                    result = View(ViewNames.Scorecard, model);
                    ModelState.AddModelError(string.Empty, MessageService.ScoringClosed());
                }
                catch (Exception e)
                {
                    HandleExceptionViaElmah(e);
                    model = ResetModel(model);
                    result = View(ViewNames.Scorecard, model);
                }
            }
            else
            {
                //
                // Repopulate the view model with the data that was non hidden
                //
                model = ResetModel(model);
                result = View(ViewNames.Scorecard, model);
            }
            //
            // Return to where ever ....
            //
            return result;
        }
        /// <summary>
        /// Retrieve any updated scores and return it to the requesting view
        /// </summary>
        /// <param name="applicationId">Application entity identifier</param>
        /// <returns>List of ScoreCacheEntries that have been updated since the last pool request</returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult PollScore(int applicationId, List<ReviewerScoringViewModel.ScoreEntryViewModel> currentScores)
        {
            ReviewerScoringViewModel model = new ReviewerScoringViewModel();
            try
            {
                List<ScoreCacheEntry> reviewerScores = theApplicationScoringService.PollScore(applicationId);
                model = new ReviewerScoringViewModel(reviewerScores, currentScores);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Retrieve the active application identifier by a session panel identifier
        /// </summary>
        /// <param name="applicationId">Session panel identifier</param>
        /// <returns>The active application identifier</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult PollActiveApplicationId(int? sessionPanelId)
        {
            List<IApplicationStatusModel> list = new List<IApplicationStatusModel>();
            try
            {
                if ((sessionPanelId.HasValue) && (sessionPanelId.Value > 0))
                {
                    var results = theApplicationScoringService.RetrieveSessionApplicationScoringStatuses(sessionPanelId.Value);
                    list = results.ModelList.ToList();
                    MakeUrlsForStatus(list);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            //
            // All done.  Just send back to the UI
            //
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets score-able application
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetScorableApplications(int? sessionPanelId)
        {
            string results = String.Empty;
            try
            {
                if ((sessionPanelId.HasValue) && (sessionPanelId.Value > 0))
                {
                    int userId = GetUserId();
                    int theSessionPanelId = sessionPanelId.Value;
                    MyWorkspaceViewModel model = new MyWorkspaceViewModel();

                    var panelStatus = theApplicationManagementService.OpenPanelStatus(theSessionPanelId, userId);
                    var isClientChair = theApplicationScoringService.IsCpritChairPerson((int)sessionPanelId, userId);
                    var applications = GetApplications(panelStatus, theSessionPanelId, userId);

                    model = new MyWorkspaceViewModel(applications, panelStatus, isClientChair);
                    //model.isClientChair = isClientChair;
                    results = JsonConvert.SerializeObject(model.ScorableApplications);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Construct the scorecard URL's for the status changes
        /// </summary>
        /// <param name="list">List of ApplicationStatusModels</param>
        internal void MakeUrlsForStatus(List<IApplicationStatusModel> list)
        {
            foreach (var model in list)
            {
                model.ScoreCardUrl = Url.Action(Routing.MyWorkspace.Scorecard, Routing.P2rmisControllers.MyWorkspace, new { panelapplicationid = model.PanelApplicationId, sessionpanelid = model.SessionPanelId });
            }
        }
        /// <summary>
        /// Returns Index page with Assignment grid
        /// </summary>
        /// <param name="panelApplicationId"></param>
        /// <param name="panelUserAssignmentId"></param>
        /// <param name="isCprit"></param>
        /// <param name="isCpritChair"></param>
        /// <returns>Returns Index page with Assignment grid</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetReviewerAssignment(int panelApplicationId, int panelUserAssignmentId, bool isCprit, bool isCpritChair)
        {
            ExpertiseCoiViewModel theViewModel = new ExpertiseCoiViewModel();
            try
            {
                theViewModel.PanelApplicationId = panelApplicationId;
                theViewModel.PanelUserAssignmentId = panelUserAssignmentId;

                // Get panel session identifier
                int panelId = GetPanelSession();
                if (panelId > 0)
                {
                    var applicationInfo = thePanelManagementService.GetApplicationInformationWithBlinding(panelApplicationId);
                    theViewModel.ApplicationInfo = applicationInfo;
                    var collaborators = thePanelManagementService.ListPersonnelWithCoi(applicationInfo.ApplicationId).ModelList.ToList();
                    theViewModel.Collaborators = collaborators;

                    var assignmentTypeListContainer = thePanelManagementService.GetPanelSessionAssignmentTypeList(panelId);
                    theViewModel.ClientAssignmentTypeList = assignmentTypeListContainer.ModelList.ToList();

                    var clientCoiTypeContainer = thePanelManagementService.GetPanelSessionCoiTypeList(panelId);
                    theViewModel.ClientCoiList = clientCoiTypeContainer.ModelList.ToList();

                    var clientExpertiseRatingContainer = thePanelManagementService.GetPanelSessionClientExpertiseRatingList(panelId);
                    theViewModel.ClientExpertiseRatingList = clientExpertiseRatingContainer.ModelList.ToList();
                    // Remove "COI" from the Expertise drop-down list
                    var coiExpertise = theViewModel.ClientExpertiseRatingList.FirstOrDefault(x => x.ClientExpertiseRatingAbbreviation == Invariables.ReviewerExpertise.CoiExpetise);
                    if (coiExpertise != null)
                    {
                        theViewModel.ClientExpertiseRatingList.Remove(coiExpertise);
                        theViewModel.CurrSessionCoiExpertiseRatingId = coiExpertise.ClientExpertiseRatingId;
                    }

                    if (panelUserAssignmentId > 0)
                    {
                        var assignmentContainer = thePanelManagementService.GetExpertiseAssignments(panelId, panelApplicationId, panelUserAssignmentId);
                        var assignmentList = assignmentContainer.ModelList.ToList();
                        if (assignmentList.Count > 0)
                        {
                            theViewModel.AssignmentTypeId = assignmentList[0].AssignmentTypeId;
                            theViewModel.ClientAssignmentTypeId = assignmentList[0].ClientAssignmentTypeId;
                            theViewModel.ClientCoiTypeId = assignmentList[0].CoiTypelId;
                            theViewModel.ClientExpertiseRatingId = assignmentList[0].ClientExpertiseRatingId;
                            theViewModel.Comment = assignmentList[0].ExpertiseComments;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                theViewModel = new ExpertiseCoiViewModel();
                HandleExceptionViaElmah(e);
            }
            return (isCpritChair) ? PartialView(ViewNames.ReviewerAssignmentCPRIT, theViewModel) : PartialView(ViewNames.ReviewerAssignment, theViewModel);
        }

        /// <summary>
        /// Gets the incomplete overall warning modal.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetIncompleteOverallWarningModal()
        {
            return PartialView(ViewNames.IncompleteOverallWarningModal);
        }
        /// <summary>
        /// Gets the incomplete critique warning modal.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetIncompleteCritiqueWarningModal(List<IncompleteCritiqueViewModel> incompleteCritiques)
        {
            return PartialView(ViewNames.IncompleteCritiqueWarningModal, incompleteCritiques);
        }
        /// <summary>
        /// Gets the notification of submit modal.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetNotificationOfSubmitModal()
        {
            return PartialView(ViewNames.NotificationOfSubmitModal);
        }
        /// <summary>
        /// Gets the confirmation of success modal.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetConfirmationOfSuccessModal()
        {
            return PartialView(ViewNames.ConfirmationOfSuccessModal);
        }

        /// <summary>
        /// Gets the no scores modal.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetNoScoresModal()
        {
            return PartialView(ViewNames.NoDataAvailable);
        }
        /// <summary>
        /// Get the submitted critique modal
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult AlreadySubmittedCritique()
        {
            return PartialView(ViewNames.AlreadySubmittedCritique);
        }
        //public ActionResult GetSubmittedModal()
        //{
        //    //return PartialView(ViewNames.Alrea)
        //}
        /// <summary>
        /// Saves the assignment (assign/unassign/expertise/coi)
        /// </summary>
        /// <param name="clientExpertiseRatingId">Expertise reviewer identifier</param>
        /// <param name="clientCoiTypeId">Client COI type identifier</param>
        /// <param name="clientAssignmentTypeId">Client Type identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="comment">COI comment</param>
        /// <param name="forceUnAssignAction">Whether to force UnAssign action even if there are workflow(s)/critique(s) associated</param>
        /// <returns>the updated expertise model back to the expertise page</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult SaveAssignment(int? clientExpertiseRatingId, int? clientCoiTypeId, int? clientAssignmentTypeId, int panelApplicationId, int panelUserAssignmentId, string comment, bool forceUnAssignAction)
        {
            string message = string.Empty;
            try
            {
                //call the bl service to decide what to do kick back error/assign/un-assign
                ReviewerAssignmentStatus status = thePanelManagementService.SaveAssignmentWithCurrentPresentationOrder(clientExpertiseRatingId, clientCoiTypeId, clientAssignmentTypeId, panelApplicationId,
                        panelUserAssignmentId, comment, forceUnAssignAction, GetUserId(), false);
                message = GetAssignmentMessage(status);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                HandleExceptionViaElmah(e);
            }
            return string.IsNullOrEmpty(message) ? Json(true) : Json(message);
        }

        /// <summary>
        /// the assigned reviewers scores screen.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns>Assigned reviewers scores modal content</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult AssignedReviewersScores(int panelApplicationId)
        {
            var model = theApplicationScoringService.RetrieveAssignedReviewersScores(panelApplicationId, GetUserId());
            var appInfo = thePanelManagementService.GetApplicationInformationWithBlinding(panelApplicationId);
            AssignedReviewersScoresViewModel viewModel = new AssignedReviewersScoresViewModel(model, appInfo);
            viewModel.CriteriaGridData = GroupScoresForGrid(model, false);
            viewModel.OverallGridData = GroupScoresForGrid(model, true);
            return PartialView(ViewNames.AssignedReviewersScores, viewModel);
        }

        /// <summary>
        /// Critiques the panel.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="applicationWorkflowStepId">The application workflow step identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult CritiquePanel(int panelApplicationId, int sessionPanelId, int userId, int applicationWorkflowStepId)
        {
            var model = new CritiquePanelViewModel();
            try
            {
                bool isCurrentUser = GetUserId() == userId;
                bool hasManageCritiquesPermission = HasPermission(Permissions.PanelManagement.ManagePanelCritiques);
                bool hasViewCritiquesPermission = HasPermission(Permissions.PanelManagement.ViewPanelCritiques);
                var reviewerCritiques = theApplicationScoringService.ListApplicationCritiqueForPanelManagement(panelApplicationId, userId, applicationWorkflowStepId, isCurrentUser, hasManageCritiquesPermission, hasViewCritiquesPermission).ModelList.ToList();
                var appInfo = thePanelManagementService.ListApplicationInformation(sessionPanelId, panelApplicationId);
                var critiquePhase = theApplicationScoringService.GetCritiqueStageStep(panelApplicationId);
                var critiqueReviewerOrder = theApplicationScoringService.GetCritiqueReviewerOrder(panelApplicationId);
                ClientScoringScaleLegendModel clientScoringScaleLegend = theApplicationManagementService.GetScoringLegendCurrentStage(panelApplicationId);
                if (reviewerCritiques.Count > 0)
                {
                    int count = 0;
                    foreach (var critique in reviewerCritiques)
                    {
                        if (critique.ReviewerId == userId)
                        {
                            model = new CritiquePanelViewModel(reviewerCritiques[count], appInfo, critiquePhase, critiqueReviewerOrder, clientScoringScaleLegend);
                        }
                        count++;
                    }
                }
                else
                {
                    HandleExceptionViaElmah(new Exception(ExceptionMessage.MissingReviewerCritiques));
                }
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                HandleExceptionViaElmah(e);
            }
            return View(ViewNames.CritiquePanel, model);
        }

        /// <summary>
        /// Groups the scores for grid. The outer join and sorting forces alignment between header and grid data
        /// </summary>
        /// <param name="model">The populated model.</param>
        /// <param name="overallFlag">if set to true overall elements are populated; otherwise non-overall.</param>
        /// <returns></returns>
        internal List<CriteriaReviewerScoreViewModel> GroupScoresForGrid(IReviewerCritiqueSummary model, bool overallFlag)
        {
            var groupedData = (from rev in model.ReviewerList
                               from phase in model.PhaseList
                               join criteria in model.CriteriaList on new { rev.PanelUserAssignmentId, phase.StepTypeId } equals
                                   new { criteria.PanelUserAssignmentId, criteria.StepTypeId }
                               where criteria.OverallFlag == overallFlag
                               group new { criteria, phase, rev } by new { criteria.CriteriaName, criteria.CriteriaSortOrder } into group1
                               select new CriteriaReviewerScoreViewModel
                               {
                                   CriteriaName = group1.Key.CriteriaName,
                                   CriteriaSortOrder = group1.Key.CriteriaSortOrder,
                                   OverallFlag = overallFlag,
                                   ReviewerPhaseScores = (from data1 in group1
                                                          orderby data1.rev.SortOrder, data1.phase.SortOrder
                                                          select new ReviewerPhaseScoreViewModel
                                                          {
                                                              PanelUserAssignmentId = data1.rev.PanelUserAssignmentId,
                                                              StepTypeId = data1.phase.StepTypeId,
                                                              Score = data1.criteria.Score,
                                                              ApplicationWorkflowStepId = data1.criteria.ApplicationWorkflowStepId,
                                                              UserId = data1.rev.UserId
                                                          }).ToList()
                               }).OrderBy(x => x.CriteriaSortOrder).ToList();
            return groupedData;
        }
        /// <summary>
        /// Gets the incomplete overall warning modal.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ScorecardModal()
        {
            return PartialView(ViewNames.ScorecardModal);
        }
        #region Helpers
        /// <summary>
        /// Resets the ScorecardViewModel if the user entered an error or an exception occurred.
        /// </summary>
        /// <param name="model">ScorecardViewModel containing user entered data</param>
        /// <returns>Updated ScorecardViewModel</returns>
        private ScorecardViewModel ResetModel(ScorecardViewModel model)
        {
            model = PopulateScoreCardModel(model, model.SessionPanelId, model.PanelApplicationId, model.UserId);
            return model;
        }
        /// <summary>
        /// Populates the ScorecardViewModel with data.
        /// </summary>
        /// <param name="model">ScorecardViewModel containing user entered data</param>
        /// <param name="sessionPanelId">Identifier for a session panel</param>
        /// <param name="panelApplicationId">Identifier for a panel application being scored</param>
        /// <param name="userId">Identifier for the user whom scores are being submitted for</param>
        /// <returns>Updated ScorecardViewModel</returns>
        private ScorecardViewModel PopulateScoreCardModel(ScorecardViewModel model, int sessionPanelId, int panelApplicationId, int userId)
        {
            var appInfo = thePanelManagementService.ListApplicationInformation(sessionPanelId, panelApplicationId);
            model.ApplicationInformation = appInfo;
            var revScores = theApplicationScoringService.ListApplicationScores(panelApplicationId, userId);
            model.ReviewerScores = revScores.ModelList.ToList();
            model.OverallScores = GetOverallScores(model.ReviewerScores);
            model.CriterionScores = GetCriterionScores(model.ReviewerScores);
            model.UserName = theUserProfileManagementService.GetuUserName((int)userId).FullName;
            model.UserId = userId;
            model.PanelApplicationId = panelApplicationId;
            model.SessionPanelId = sessionPanelId;
            CritiqueIconControlModel iconControlModel = theApplicationScoringService.ShowCritiqueIcons(panelApplicationId, sessionPanelId, userId);
            model.CanUserEnterComments = iconControlModel.CanAViewComments;
            model.CanUserEditScores = (IsValidPermission(Permissions.ManageApplicationScoring.EditScoreCard) || iconControlModel.IsScoreCardEnabled);
            model.DetermineScoringTypes();
            return model;
        }

        /// <summary>
        /// Generates a list consisting of only criterion scores
        /// </summary>
        /// <param name="reviewerScores"></param>
        /// <returns>List of criterion scores</returns>
        internal List<ReviewerScores> GetCriterionScores(List<ReviewerScores> reviewerScores)
        {
            return reviewerScores.Where(x => x.OverallFlag == false).ToList();
        }
        /// <summary>
        /// Generates a list consisting of only overall scores
        /// </summary>
        /// <param name="reviewerScores"></param>
        /// <returns>List of overall scores</returns>
        internal List<ReviewerScores> GetOverallScores(List<ReviewerScores> reviewerScores)
        {
            return reviewerScores.Where(x => x.OverallFlag).ToList();
        }
        /// <summary>
        /// Get applications
        /// </summary>
        /// <param name="panelStatus">PanelStatus object</param>
        /// <param name="sessionPanelId">Session Panel identifier</param>
        /// <param name="userId">User identifier</param>
        /// <returns>List of IBaseAssignmentModel applications</returns>
        private List<IBaseAssignmentModel> GetApplications(PanelStatus panelStatus, int sessionPanelId, int userId)
        {
            var apps = new List<IBaseAssignmentModel>();
            if (panelStatus.IsReleased && !panelStatus.IsPostAssigned)
            {
                apps = theApplicationScoringService.ListReviewerReadyForReview(sessionPanelId, userId).ModelList.ToList().ConvertAll(o => (IBaseAssignmentModel)o);
            }
            else if (panelStatus.IsPostAssigned)
            {
                apps = theApplicationScoringService.RetrievePostAssignmentApplications(sessionPanelId, userId).ModelList.ToList().ConvertAll(o => (IBaseAssignmentModel)o);
            }
            else
            {
                apps = theApplicationScoringService.RetrievePreAssignmentApplications(sessionPanelId, userId).ModelList.ToList().ConvertAll(o => (IBaseAssignmentModel)o);
            }
            return apps;
        }
        /// <summary>
        /// Gets active or scroing application.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.AccessMyWorkspace)]
        public JsonResult GetActiveOrScoringApplication(int sessionPanelId)
        {
            var app = new KeyValuePair<int, string>();
            try
            {
                var cachedApp = ScorableApplicationCacheService.GetActiveOrScoringApplication(sessionPanelId.ToString());
                if (cachedApp == null)
                {
                    app = theApplicationScoringService.GetActiveOrScoringApplication(sessionPanelId);
                    ScorableApplicationCacheService.AddActiveOrScoringApplication(app, sessionPanelId.ToString());
                } else 
                    app = (KeyValuePair<int, string>)cachedApp;
            }
            catch (Exception e)
            {
                //
                // Log the exception
                //
                HandleExceptionViaElmah(e);
            }
            return Json(new { applicationId = app.Key, status = app.Value }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the chair applications.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private List<IBaseAssignmentModel> GetChairApplications(int sessionPanelId, int userId)
        {
            var apps = new List<IBaseAssignmentModel>();
            apps = theApplicationScoringService.RetrieveChairApplications(sessionPanelId, userId).ModelList.ToList().ConvertAll(o => (IBaseAssignmentModel)o);
            return apps;
        }
        #endregion
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult EditOverview(int panelApplicationId)
        {
            //
            // Create the view model & set what information we need for display
            //
            string overview = String.Empty;
            try
            {
                // call BLL service to get overview data
                overview = this.thePanelManagementService.GetPanelSummary(panelApplicationId);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                HandleExceptionViaElmah(e);
            }
            return Json(overview, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Controller action method to save the panel overview
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="panelOverview">Overview content</param>
        /// <remarks>
        /// Since the controller method was developed before the view ticket was worked, this method was
        /// put in this controller file.  When the link is located the method should be re-factored into 
        /// the correct file.
        /// </remarks>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult SaveOverview(int panelApplicationId, string panelOverview)
        {
            string message = Messages.PanelOverviewNotSaved;
            bool success = false;
            try
            {
                string decodedOverview = System.Web.HttpUtility.HtmlDecode(panelOverview);
                decodedOverview = System.Web.HttpUtility.HtmlDecode(decodedOverview);
                // call BLL service to save/insert data
                success = this.thePanelManagementService.SavePanelOverview(panelApplicationId, decodedOverview, GetUserId());
                message = (success) ? Messages.PanelOverviewSaved : Messages.SummaryStatementProcessingStarted;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                message = Messages.PanelOverviewNotSaved;
            }
            return Json(new { success = success, message = message });
        }
        /// <summary>
        /// Manage application scoring
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.AccessMyWorkspace)]
        public ActionResult ManageApplicationScoring()
        {
            ManageApplicationScoringViewModel model = new ManageApplicationScoringViewModel();
            try
            {
                int userId = GetUserId();
                int? sessionPanelId = GetPanelSession();
                var panel = thePanelManagementService.GetSessionPanel((int)sessionPanelId);
                int? programYearId = panel.ProgramYearId;
                int? sessionId = panel.MeetingSessionId;

                var isClientChair = theApplicationScoringService.IsCpritChairPerson((int)sessionPanelId, GetUserId());
                if (isClientChair)
                {
                    var applications = GetChairApplications((int)sessionPanelId, userId);
                    model = PopulateManageAppScoringGrids(programYearId.GetValueOrDefault(), sessionId.GetValueOrDefault(), sessionPanelId.GetValueOrDefault(), userId, model);
                    model.SelectedPanelId = sessionPanelId;
                    model.SelectedProgramId = programYearId;
                    model.SelectedSessionId = sessionId;
                    model.AppendTabs(applications);
                }
                else
                {
                    return RedirectToAction("NoAccess", "ErrorPage");
                }
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
        /// Populates the manage application scoring grids.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        private ManageApplicationScoringViewModel PopulateManageAppScoringGrids(int programId, int sessionId, int panelId, int userId, ManageApplicationScoringViewModel model)
        {
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
        /// Get My Workspace view
        /// </summary>
        /// <returns>The view of the application scores</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.MyWorkspace.AccessMyWorkspace)]
        public ActionResult GetPreAssignmentApplications(int sessionPanelId)
        {
            MyWorkspaceViewModel model = new MyWorkspaceViewModel();
            try
            {
                SetPanelSession(sessionPanelId);
                var userId = GetUserId();
                var panelStatus = theApplicationManagementService.OpenPanelStatus(sessionPanelId, userId);
                var isClientChair = theApplicationScoringService.IsCpritChairPerson(sessionPanelId, userId);

                if (isClientChair)
                {
                    var applications = GetChairApplications(sessionPanelId, userId);
                    model = new MyWorkspaceViewModel(applications, panelStatus, isClientChair);
                }
                else
                {
                    var applications = GetApplications(panelStatus, sessionPanelId, userId);
                    model = new MyWorkspaceViewModel(applications, panelStatus, isClientChair);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return Content(JsonConvert.SerializeObject(model));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="panelApplicationId"></param>
        /// <param name="panelUserAssignmentId"></param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetCollaborators(int panelApplicationId, int panelUserAssignmentId)
        {
            ExpertiseCoiViewModel theViewModel = new ExpertiseCoiViewModel();
            try
            {
                theViewModel.PanelApplicationId = panelApplicationId;
                theViewModel.PanelUserAssignmentId = panelUserAssignmentId;

                // Get panel session identifier
                int panelId = GetPanelSession();
                if (panelId > 0)
                {
                    var applicationInfo = thePanelManagementService.GetApplicationInformationWithBlinding(panelApplicationId);
                    theViewModel.ApplicationInfo = applicationInfo;
                    var collaborators = thePanelManagementService.ListPersonnelWithCoi(applicationInfo.ApplicationId).ModelList.ToList();
                    theViewModel.Collaborators = collaborators;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return Content(JsonConvert.SerializeObject(theViewModel.Collaborators));
        }
    }
}