using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.ViewModels.PanelManagement;

namespace Sra.P2rmis.Web.Controllers.PanelManagement
{
    public partial class PanelManagementController
    {
        #region Controller Actions
        /// <summary>
        /// action result for the Reviewers page
        /// </summary>
        /// <returns>the view of the reviews</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ViewPanelReviewers)]
        public ActionResult Reviewers()
        {
            ReviewersViewModel theViewModel = new ReviewersViewModel();
            try
            {
                // Set data for panel menu(s)
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                if (theViewModel.SelectedPanel > 0)
                {
                    var assignedStaffs = thePanelManagementService.GetAssignedStaffs(theViewModel.SelectedPanel).ModelList.ToList();
                    var reviewers = thePanelManagementService.GetPanelReviewers(theViewModel.SelectedPanel).ModelList.ToList();
                    theViewModel.SetData(assignedStaffs, reviewers, HasPermission(Permissions.ReviewerRecruitment.ModifyParticipantPostAssignment), 
                        HasPermission(Permissions.ReviewerRecruitment.ModifyParticipantPostAssignmentLimited), HasPermission(Permissions.PanelManagement.ProcessStaffs), HasPermission(Permissions.PanelManagement.ManageApplicationReviewer), HasPermission(Permissions.UserProfileManagement.ManageUserAccounts), HasPermission(Permissions.PanelManagement.EvaluateReviewers));
                }
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new ReviewersViewModel();
                HandleExceptionViaElmah(e);
            }
            return View(theViewModel);
        }
        /// <summary>
        /// action result for the Reviewers page
        /// </summary>
        /// <param name="SelectedProgramYear">The selected program year.</param>
        /// <param name="SelectedPanel">The selected panel.</param>
        /// <returns>
        /// the view of the reviews
        /// </returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ViewPanelReviewers)]
        public ActionResult Reviewers(int? SelectedProgramYear, int SelectedPanel)
        {
            ReviewersViewModel theViewModel = new ReviewersViewModel();
            try
            {
                // Set data for panel menu(s)
                SetSessionVariables(SelectedProgramYear, SelectedPanel);
                // Set data for panel menu(s)
                SetPanelMenu(theViewModel);
                SetTabs(theViewModel);
                if (theViewModel.SelectedPanel > 0)
                {
                    var assignedStaffs = thePanelManagementService.GetAssignedStaffs(theViewModel.SelectedPanel).ModelList.ToList();
                    var reviewers = thePanelManagementService.GetPanelReviewers(theViewModel.SelectedPanel).ModelList.ToList();
                    theViewModel.SetData(assignedStaffs, reviewers, HasPermission(Permissions.ReviewerRecruitment.ModifyParticipantPostAssignment), 
                        HasPermission(Permissions.ReviewerRecruitment.ModifyParticipantPostAssignmentLimited), HasPermission(Permissions.PanelManagement.ProcessStaffs), HasPermission(Permissions.PanelManagement.ManageApplicationReviewer), HasPermission(Permissions.UserProfileManagement.ManageUserAccounts), HasPermission(Permissions.PanelManagement.EvaluateReviewers));
                }
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new ReviewersViewModel();
                HandleExceptionViaElmah(e);
            }
            return View(theViewModel);
        }
        /// <summary>
        /// Gets the assigned staffs.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ProcessStaffs)]
        public ActionResult GetAssignedStaffs(int sessionPanelId)
        {
            var result = new List<StaffViewModel>();
            try
            {
                var assignedStaffs = thePanelManagementService.GetAssignedStaffs(sessionPanelId).ModelList.ToList();
                result = assignedStaffs.ConvertAll(x => new StaffViewModel(x));
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// The method called when the user loads the panel assignments modal
        /// </summary>
        /// <param name="context">the context</param>
        /// <returns>the modal for searching a reviewer</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageApplicationReviewer)]
        public ActionResult PanelAssignment(int participantUserId, bool isBlocked, bool status)
        {
            var theViewModel = new PanelAssignmentViewModel();
            bool canProcessPanel = HasPermission(Permissions.PanelManagement.Process);
            bool canManageApplicationReviewer = HasPermission(Permissions.PanelManagement.ManageApplicationReviewer);
            try
            {
                int? clientId = GetClientId();
                if (clientId != null)
                {
                    var sessionPanelId = IsSessionVariableNull(SessionVariables.PanelId) ? 0 : (int)Session[SessionVariables.PanelId];
                    var model = theRecruitmentService.RetrieveReviewerParticipation(sessionPanelId, participantUserId);
                    var participantTypeList = theLookupService.ListParticipantType((int)clientId).ModelList.ToList();
                    var participantRoleList = theLookupService.ListParticipantRoleAbbreviation((int)clientId).ModelList.ToList();
                    var participationMethodList = theLookupService.ListParticipationMethods().ModelList.ToList();
                    var participationLevelList = theLookupService.ListParticipationLevels().ModelList.ToList();
                    theViewModel = new PanelAssignmentViewModel(model, participantUserId, isBlocked, status, canProcessPanel,
                        canManageApplicationReviewer);
                    theViewModel.SetLists(participantTypeList, participantRoleList, participationMethodList, participationLevelList);
                }
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new PanelAssignmentViewModel();
                HandleExceptionViaElmah(e);
            }

            return PartialView(ViewNames.PanelAssignment, theViewModel);
        }
        /// <summary>
        /// The method called 
        /// </summary>
        /// <param name="participantUserId">Reviewer user identifier</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.Manage)]
        public ActionResult IsContractSigned(int ParticipantUserId)
        {
            var result = false;
            try
            {
                int sessionPanelId = IsSessionVariableNull(SessionVariables.PanelId) ? 0 : (int)Session[SessionVariables.PanelId];
                result = theRecruitmentService.IsContractSigned(ParticipantUserId, sessionPanelId);                
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                HandleExceptionViaElmah(e);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Panels the assignment.
        /// </summary>
        /// <param name="model">The PanelAssignmentViewModel model.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageApplicationReviewer)]
        public ActionResult PanelAssignment(PanelAssignmentViewModel model )
        {
            var success = false;
            try
            {
                model.SetClientApproval();
                var panelUserId = model.PanelUserPotentialAssignmentId;
                if (!model.IsAssigned)
                {
                    if (model.StatusValue == "Assign")
                    {
                        var shouldSendEmail = theRecruitmentService.AssignReviewerToPanel(model.PanelUserPotentialAssignmentId ?? 0, model.PanelUserAssignmentId ?? 0, model.SessionPanelId,
                            model.ParticipantUserId, model.ParticipantTypeId, model.ParticipantRoleId,
                            model.ParticipationMethodId, model.ClientApproval, model.IsParticipationRestricted,
                            !model.IsAssigned, model.PanelUserAssignmentId.HasValue, true, GetUserId());
                        if (shouldSendEmail)
                        {
                            var isFirstTimeReviewer = theRecruitmentService.IsNewProfile(model.ParticipantUserId);
                            if (isFirstTimeReviewer)
                            {
                                theRecruitmentService.ToggleReviewerProfile(model.ParticipantUserId, GetUserId());
                                var newEmailStatus = theUserProfileManagementService.SendNewCredentials(theMailService, model.ParticipantUserId, GetUserId());
                            }
                            var isEmailSent = theRecruitmentService.SendPanelAssignmentEmail(theMailService, model.ParticipantUserId, model.SessionPanelId, GetUserId());
                        }
                    }
                    else if (model.StatusValue == "Remove")
                    {
                        theRecruitmentService.DeletePanelUserPotentialAssignment((int)panelUserId, GetUserId());
                        success = true;
                    }
                    else if (model.StatusValue == "Change Status")
                    {
                        var noStatus = false;
                        var shouldSendEmailTwo = theRecruitmentService.AssignReviewerToPanel(model.PanelUserPotentialAssignmentId ?? 0, model.PanelUserAssignmentId ?? 0, model.SessionPanelId,
                                model.ParticipantUserId, model.ParticipantTypeId, model.ParticipantRoleId,
                                model.ParticipationMethodId, model.ClientApproval, model.IsParticipationRestricted,
                                model.IsAssigned, model.PanelUserAssignmentId.HasValue, noStatus, GetUserId());
                        success = true;
                    }
                }
                else
                {
                    if(model.StatusValue == "Remove")
                    {
                        var panelUserAssignmentId = model.PanelUserAssignmentId;
                        thePanelManagementService.RemoveUserFromPanel((int)panelUserAssignmentId, GetUserId());
                    }
                    else
                    {
                        var shouldSendEmailThree = theRecruitmentService.AssignReviewerToPanel(model.PanelUserPotentialAssignmentId ?? 0, model.PanelUserAssignmentId ?? 0, model.SessionPanelId,
                            model.ParticipantUserId, model.ParticipantTypeId, model.ParticipantRoleId,
                            model.ParticipationMethodId, model.ClientApproval, model.IsParticipationRestricted,
                            !model.IsAssigned, model.PanelUserAssignmentId.HasValue, true, GetUserId());
                    }

                }
                success = true;
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { success = success }, JsonRequestBehavior.AllowGet);
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageApplicationReviewer)]
        public ActionResult TransferToPanel(int? PanelUserAssignmentId, int? newSessionPanelId, int? reviewerUserId, int? clientParticipantTypeId,
                                      int? clientRoleId, int? participantMethodId, bool? clientApprovalFlag, bool? restrictedAccessFlag)
        {
            var success = false;
            try
            {
                theRecruitmentService.TransferReviewerToPanel((int)PanelUserAssignmentId, (int)newSessionPanelId, (int)reviewerUserId, clientParticipantTypeId,
                  clientRoleId, participantMethodId, clientApprovalFlag, (bool)restrictedAccessFlag, GetUserId());
                success = true;
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(new { success = success }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Commmunication log
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.SendPanelCommunication)]
        public ActionResult CommLog(int userInfoId)
        {
            var theViewModel = new ReviewerCommunicationLogViewModel();
            try
            {
                var log = theRecruitmentService.GetRecruitCommunicationLog(userInfoId, GetUserId());
                var methods = theLookupService.ListCommunicationMethod().ModelList.ToList();
                theViewModel = new ReviewerCommunicationLogViewModel(log, methods);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new ReviewerCommunicationLogViewModel();
                HandleExceptionViaElmah(e);
            }
            return PartialView(ViewNames.CommunicationLog, theViewModel);
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.EvaluateReviewers)]
        public ActionResult RatingEvaluation(int userId)
        {
            ViewReviewerEvaluationViewModel theViewModel = new ViewReviewerEvaluationViewModel();
            try
            {
                //Call the service
                var evalData = thePanelManagementService.RetrieveEvaluationDetails(userId).ModelList.ToList();
                theViewModel.SetData(evalData);
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                theViewModel = new ViewReviewerEvaluationViewModel();
                HandleExceptionViaElmah(e);
            }
            return PartialView(ViewNames.RatingEvaluation, theViewModel);
        }
        /// <summary>
        /// Adds a potential assignment
        /// </summary>
        /// <param name="userId">The user identifier of the potential reviewer</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageApplicationReviewer)]
        public ActionResult AddPotentialAssignment(int userId)
        {
            var successful = false;
            try
            {
                //Call the service
                var sessionPanelId = IsSessionVariableNull(SessionVariables.PanelId) ? 0 : (int)Session[SessionVariables.PanelId];
                theRecruitmentService.SavePanelUserAssignPotentialReviewer(null, sessionPanelId, userId, null, null, null,
                    null, false, false, GetUserId());
                successful = true;
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(successful, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Removes the potential assignment.
        /// </summary>
        /// <param name="userId">The user identifier of the reviewer.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageApplicationReviewer)]
        public ActionResult RemovePotentialAssignment(int panelUserAssignmentId)
        {
            var successful = false;
            try
            {
                //Call the service
                var sessionPanelId = IsSessionVariableNull(SessionVariables.PanelId) ? 0 : (int)Session[SessionVariables.PanelId];
                theRecruitmentService.DeletePanelUserPotentialAssignment(panelUserAssignmentId, GetUserId());
                successful = true;
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(successful, JsonRequestBehavior.AllowGet);
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.Manage)]
        public ActionResult FindApplications(int PanelUserAssignementId)
        {
            var hasApplications = thePanelManagementService.HasAssignedApplications(PanelUserAssignementId);
            return Json(new { success = hasApplications }, JsonRequestBehavior.AllowGet);
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageApplicationReviewer)]
        public ActionResult RemoveUser(int panelUserAssignmentId)
        {
            var successful = false;
            try
            {
                thePanelManagementService.RemoveUserFromPanel(panelUserAssignmentId, GetUserId());
                successful = true;
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(successful, JsonRequestBehavior.AllowGet);
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ProcessStaffs)]
        public ActionResult AddStaff(int assignedUserId, int sessionPanelId)
        {
            var success = false;
            try
            {
                thePanelManagementService.AddStaffToPanel(assignedUserId, sessionPanelId, GetUserId());
                success = true;
            }
            catch (Exception e)
            {
                HandleExceptionViaElmah(e);
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

}