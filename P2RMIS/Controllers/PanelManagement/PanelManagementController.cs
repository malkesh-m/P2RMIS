using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Web.ViewModels.PanelManagement;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.Bll.ReviewerRecruitment;
using Sra.P2rmis.WebModels.ReviewerRecruitment;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sra.P2rmis.Bll.ApplicationScoring;

namespace Sra.P2rmis.Web.Controllers.PanelManagement
{
    public partial class PanelManagementController : PanelManagementBaseController
    {
        #region Properties
        /// <summary>
        /// Controller Name
        /// </summary>
        public static string Name { get { return "PanelManagement"; } }
        public static string MethodRequestTransfer { get { return "RequestTransfer"; } }
        #endregion
        #region Construction; Setup & Disposal

        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private PanelManagementController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelManagementController"/> class.
        /// </summary>
        /// <param name="theSummaryProcessingService">The summary processing service.</param>
        /// <param name="theMailService">The mail service.</param>
        /// <param name="thePanelManagementService">The panel management service.</param>
        /// <param name="theWorkflowService">The workflow service.</param>
        /// <param name="theUserProfileManagementService">The user profile management service.</param>
        /// <param name="theFileService">The file service.</param>
        /// <param name="theLookupService">The lookup service.</param>
        /// <param name="theCriteriaService">The criteria service.</param>
        /// <param name="theRecruitmentService">The recruitment service.</param>
        /// <param name="theApplicationScoringService">The application scoring service</param>
        public PanelManagementController(ISummaryProcessingService theSummaryProcessingService, 
                                        IMailService theMailService, 
                                        IPanelManagementService thePanelManagementService, 
                                        IWorkflowService theWorkflowService, 
                                        IUserProfileManagementService theUserProfileManagementService, 
                                        IFileService theFileService, 
                                        ILookupService theLookupService,
                                        ICriteriaService theCriteriaService,
                                        IReviewerRecruitmentService theRecruitmentService,
                                        IApplicationScoringService theApplicationScoringService)
        {
            this.theSummaryProcessingService = theSummaryProcessingService;
            this.theMailService = theMailService;
            this.thePanelManagementService = thePanelManagementService;
            this.theWorkflowService = theWorkflowService;
            this.theUserProfileManagementService = theUserProfileManagementService;
            this.theFileService = theFileService;
            this.theLookupService = theLookupService;
            this.theCriteriaService = theCriteriaService;
            this.theRecruitmentService = theRecruitmentService;
            this.theApplicationScoringService = theApplicationScoringService;
        }
        #endregion
        #region Helpers
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManagePanelOrDocuments)]
        public ActionResult Menu()
        {
            return RedirectToAction("Expertise");
        }
        /// <summary>
        /// Retrieves the panels for the selected Program/Year.
        /// </summary>
        /// <param name="selectedProgramYear">Selected program year identifier</param>
        /// <returns>List of panels in Json format</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetPanelsJson(int selectedProgramYear)
        {
            var panels = new List<IPanelSignificationsModel>();
            try
            {
                int userId = GetUserId();
                Session[SessionVariables.ProgramYearId] = selectedProgramYear;
                var container = thePanelManagementService.ListPanelSignifications(userId, selectedProgramYear);
                panels = new List<IPanelSignificationsModel>(container.ModelList);
                var cycles = theCriteriaService.GetProgramYearCycles(selectedProgramYear).ModelList;
                var cyclesModel = cycles.ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                HandleExceptionViaElmah(e);
            }
            return Json(panels, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Sets the Program/Year id in the session.
        /// </summary>
        /// <param name="programYearId"></param>
        public virtual void SetProgramYearSession(int programYearId)
        {
            Session[SessionVariables.ProgramYearId] = programYearId;
        }
        /// <summary>
        /// Gets the Program/Year id in the session.
        /// </summary>
        /// <returns></returns>
        public virtual int GetProgramYearSession()
        {
            int programYearId = Convert.ToInt32(Session[SessionVariables.ProgramYearId]);
            return programYearId;
        }
        /// <summary>
        /// Sets the panel id in the session.
        /// </summary>
        /// <param name="panelId">the panel id to set in the session</param>
        public virtual void SetPanelSession(int panelId)
        {
            Session[SessionVariables.PanelId] = panelId;
        }
        /// <summary>
        /// returns the panelId stored in the session.
        /// </summary>
        /// <returns>panelId</returns>
        public virtual int GetPanelSession()
        {
            int panelId = Convert.ToInt32(Session[SessionVariables.PanelId]);
            return panelId;
        }
        /// <summary>
        /// Sets panel menu in the view model.
        /// </summary>
        /// <param name="theViewModel">the panel management view model</param>
        internal void SetPanelMenu(PanelManagementViewModel theViewModel)
        {
            int userId = GetUserId();
            bool hasSelectProgramPanelPermission = HasSelectProgramPanelPermission();

            theViewModel.HasSelectProgramPanelPermission = hasSelectProgramPanelPermission;
            theViewModel.HasManageReviewerAssignmentExpertisePermission = HasManageReviewerAssignmentExpertisePermission();

            if (!hasSelectProgramPanelPermission)
            {

                // For assigned users who have ProgramYear menu only
                var container = thePanelManagementService.ListPanelSignifications(userId);
                theViewModel.Panels = container.ModelList.ToList();
                // Build display text for Panels
                BuildPanelDropdownDisplayText(theViewModel.Panels);

                int panelId = GetSessionVariableId(SessionVariables.PanelId);
                if (panelId > 0)
                    theViewModel.SelectedPanel = panelId;
                if (theViewModel.SelectedPanel > 0)
                {
                    //Finally check if user is missing any panel management access requirements
                    var accessMessages = CanUserAccessManagePanel(theViewModel.SelectedPanel);
                    if (accessMessages.Any())
                    {
                        theViewModel.CanAccessPanel = false;
                        theViewModel.PanelAccessErrors = accessMessages;
                    }
                    else
                    {
                        theViewModel.CanAccessPanel = true;
                        theViewModel.PanelAccessErrors = new List<string>();
                    }
                }
            }
            else
            {
                // For SRM who has both Program/Year and Panel menus
                var programYearContainer = thePanelManagementService.ListProgramYears(userId);
                theViewModel.ProgramYears = programYearContainer.ModelList.ToList();
               
                // Build display text for Program/Years
                BuildProgramYearDropdownDisplayText(theViewModel.ProgramYears);

                // use session programId to populate panel dropdown list
                int programYearId = GetSessionVariableId(SessionVariables.ProgramYearId);
                if (programYearId > 0)
                {
                    theViewModel.SelectedProgramYear = programYearId;
                    // Get Panels after a Program/Year is selected
                    var panelContainer = thePanelManagementService.ListPanelSignifications(userId, programYearId);
                    theViewModel.Panels = panelContainer.ModelList.ToList();
                    // Build display text for Panels
                    BuildPanelAbbrDropdownDisplayText(theViewModel.Panels);

                    int panelIdWhenProgramYearExists = GetPanelSession();
                    if (panelIdWhenProgramYearExists > 0)
                        theViewModel.SelectedPanel = panelIdWhenProgramYearExists;
                }
            }
        }
        /// <summary>
        /// Whether the current user has SelectProgramPanel permission.
        /// </summary>
        /// <returns>True if the current user has SelectProgramPanel permission; false otherwise.</returns>
        public virtual bool HasSelectProgramPanelPermission()
        {
            CustomIdentity ident = User.Identity as CustomIdentity;
            var hasPermission = IsValidPermission(Permissions.PanelManagement.ManageUnassignedPanel);
            return hasPermission;
        }
        /// <summary>
        /// Whether the current user has ManageReviewerAssignmentExpertise permission.
        /// </summary>
        /// <returns>True if the current user has ManageReviewerAssignmentExpertise permission; false otherwise.</returns>
        public virtual bool HasManageReviewerAssignmentExpertisePermission()
        {
            var hasPermission = IsValidPermission(Permissions.PanelManagement.ManageReviewerAssignmentExpertise);
            return hasPermission;
        }
        /// <summary>
        /// Formats the panel dropdown display text.
        /// </summary>
        /// <param name="panels">IPanelSignifications list</param>
        private void BuildPanelDropdownDisplayText(List<IPanelSignificationsModel> panels)
        {
            foreach (IPanelSignificationsModel panel in panels)
            {
                panel.DisplayText = panel.FY + " " + panel.ProgramAbbreviation + ", " + panel.Role + ", " + panel.PanelName;
            }
        }
        /// <summary>
        /// Formats the Program/Year dropdown display text.
        /// </summary>
        /// <param name="programYears">IProgramYearModel list</param>
        private void BuildProgramYearDropdownDisplayText(List<IProgramYearModel> programYears)
        {
            foreach (IProgramYearModel programYear in programYears)
            {
                programYear.DisplayText = programYear.FY + " " + programYear.ProgramDescription + " (" + programYear.ProgramAbbreviation + ")";
            }
        }
        /// <summary>
        /// Formats the panel dropdown display text in the short format.
        /// </summary>
        /// <param name="panels">IPanelSignifications list</param>
        private void BuildPanelAbbrDropdownDisplayText(List<IPanelSignificationsModel> panels)
        {
            foreach (IPanelSignificationsModel panel in panels)
            {
                panel.DisplayText = panel.PanelAbbreviation;
            }
        }
        #endregion
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
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManagePanelApplication)]
        public ActionResult SaveOverview(FormCollection form)       
        {

            string message = Messages.PanelOverviewNotSaved;
            bool success = false;
            try
            {
                //gets the appIds values to work with from the submitted form
                string panelOverview = form.GetValues("PanelOverview")[0].ToString();
                int panelApplicationId = Convert.ToInt32(form.GetValues("PanelApplicationId")[0]);

                // call BLL service to save/insert data
                // First, need to decode Html data, and because it seems to be encoded twice ( with some special Html chars we get something
                // like this: &amp;ldquo;DEAD CODE&amp;rdquo; ) need to decode it twice just in case.
                string decodedOverview = System.Web.HttpUtility.HtmlDecode(panelOverview);
                decodedOverview = System.Web.HttpUtility.HtmlDecode(decodedOverview);
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
            return RedirectToAction(ViewNames.ApplicationAbstracts);       
        }

        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.Manage)]
        public ActionResult NotEnoughChars()
        {
            return PartialView(ViewNames.PersonSearchNotEnoughChars);
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.Manage)]
        public ActionResult NoRecordsFound()
        {
            return PartialView(ViewNames.PersonSearchNoRecordsFound);
        }
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult LaunchRemoveReviewerWarning()
        {
            return PartialView(ViewNames.PanelListRemoveReviewerWarning);
        }
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ManageCritiqueModal()
        {
            return PartialView(ViewNames.ManageCritiqueModal);
        }

        /// <summary>
        /// Searches for reviewers.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ManageApplicationReviewer)]
        public ActionResult SearchForReviewers(SearchForReviewersViewModel model)
        {
            //
            // Set the dropdowns for the "Other" section
            //
            int? clientId = GetClientId();
            // Set data for panel menu(s)
            SetPanelMenu(model);
            SetTabs(model);
            if (clientId != null)
            {
                //
                // Initialize the view model with the values for the static drop downs
                //
                model.SetOtherDropdowns(
                                theLookupService.ListEthnicity().ModelList.ToList(),
                                theLookupService.ListStateByName().ModelList.ToList(),
                                theLookupService.ListGender().ModelList.ToList(),
                                theLookupService.ListAcademicRankAbbreviation().ModelList.ToList(),
                                theLookupService.ListParticipantType((int)clientId).ModelList.ToList(),
                                theLookupService.ListParticipantRoleAbbreviation((int)clientId).ModelList.ToList()
                                        );
                //
                // Now lets do client specific.
                ;                //
                List<int> clientIds = new List<int>();
                clientIds.Add((int)clientId);
                var programs = theCriteriaService.GetAllClientPrograms(clientIds).ModelList.OrderBy(x => x.ProgramAbbreviation).ToList();
                model.SetClientSpecific((int)clientId, programs);
                
                if (model.ProgramId > 0)
                {
                    var fiscalYears = theCriteriaService.GetOpenProgramYears((int)model.ProgramId).ModelList.ToList();
                    model.SetFiscalYearList(model.YearId, fiscalYears);
                }

                if (model.SessionPanelAbbreviation != null)
                {
                    var panelList = theCriteriaService.GetSessionPanels((int)model.YearId).ModelList.ToList();                    
                    model.SetPanelList(model.SessionPanelAbbreviation, panelList);
                }

                if (model.PersonKey != null)
                {
                    var restrictedManageUserAccountsPermission = HasPermission(Permissions.UserProfileManagement.RestrictedManageUserAccounts);
                    // Run search
                    var searchForReviewers = model.GetSearchForReviewersModel();
                    var searchResults = thePanelManagementService.GetSearchReviewerResults(searchForReviewers, GetPanelSession()).ModelList.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
                    model.SetReviewerResults(searchResults, restrictedManageUserAccountsPermission);
                }
            }

            return View(model);
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ProcessStaffs)]
        public ActionResult SearchForStaff(SearchForStaffsViewModel model)
        {
            SetPanelMenu(model);
            SetTabs(model);
            return View(model);
        }
        /// <summary>
        /// Searches for staffs.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.ProcessStaffs)]
        public ActionResult SearchForStaffs(SearchForStaffsViewModel model)
        {
            var results = new List<SearchForStaffViewModel>();            
            try
            {
                    // Run search
                    var searchForStaffs = model.GetSearchForStaffsModel();
                    var searchResults = thePanelManagementService.GetSearchStaffResults(searchForStaffs, GetPanelSession()).ModelList.ToList();
                    model.SetStaffResults(searchResults);
                    results = model.StaffResults;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                throw e;
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Saves the communication logs.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="logs">The logs.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.PanelManagement.SendPanelCommunication)]
        public JsonResult SaveCommunicationLogs(int userId, List<ReviewerCommunicationLogViewModel.Log> logs)
        {
            var isSuccessful = false;
            try
            {
                var vm = new ReviewerCommunicationLogViewModel();
                var log = vm.GetUserCommunicationLogModel(userId, logs);
                theRecruitmentService.SaveRecruitCommunicationLog(log, GetUserId());
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
        /// Returns the program for a given client
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <returns>JSON list of ProgramYearModel web models</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult GetProgramPanelsForProgramYear(int programYearId)
        {
            List<IListEntry> panelList = new List<IListEntry>();
            try
            {
                panelList = theLookupService.ListDistinctPanelForProgramYear(programYearId).ModelList.ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);

                throw;
            }
            return Json(panelList, JsonRequestBehavior.AllowGet);
           
        }
        /// <summary>
        /// Returns the program for a given client
        /// </summary>
        /// <param name="clientProgramId">Client entity identifier</param>
        /// <returns>JSON list of ProgramYearModel web models</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public JsonResult GetProgramPanelsForSpecificClient(int clientProgramId)
        {
            List<IListEntry> panelList = new List<IListEntry>();
            try
            {
                panelList = theLookupService.ListDistinctPanelForProgram(clientProgramId).ModelList.ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);

                throw;
            }
            return Json(panelList, JsonRequestBehavior.AllowGet);

        }

        #region Helpers
        /// <summary>
        /// Returns the selected Client entity identifier in a list.
        /// </summary>
        /// <returns>Client entity identifier</returns>
        protected int? GetClientId()
        {
            var sessionPanelId = IsSessionVariableNull(SessionVariables.PanelId) ? 0 : (int)Session[SessionVariables.PanelId];
            return ClientId((ServerBase)thePanelManagementService, sessionPanelId);
        }
        #endregion
    }
}