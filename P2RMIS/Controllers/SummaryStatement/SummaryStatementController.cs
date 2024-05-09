using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.Reports;
using Sra.P2rmis.Web.ViewModels;
using Sra.P2rmis.Web.ViewModels.SummaryStatement;
using WebModel = Sra.P2rmis.WebModels.SummaryStatement;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices.FileServices;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// Controller class for the Summary Management views.
    /// </summary>
    public partial class SummaryStatementController : SummaryStatementBaseController
    {
        #region Constants
        private const int stringLength = 3;
        /// <summary>
        /// Class identifies the views controlled by this controller.
        /// </summary>
        internal class ViewNames
        {
            public const string Index = "Index";
            public const string Progress = "Progress";
            public const string WorkflowHistory = "_WorkflowHistory";
            public const string Completed = "Completed";
            public const string Preview = "_SummaryPreview";
            public const string Notes = "_Notes";
            public const string ViewApplicationPartial = "_ViewApplication";
            public const string UploadFile = "_UploadFile";
            public const string ViewApplicationModal = "_ViewApplicationModal";
        }
        /// <summary>
        /// class identifies the success messages displayed to the user
        /// </summary>
        private class SummaryStatementMessages
        {
            public const string SucessMessage = "You have successfully updated user and/or phase for these applications.";
            public const string AssignFailureMessage = "There was an error updating assignments for the selected applications. Please try again.";
        }
        #endregion

        #region Construction; Setup & Disposal

        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private SummaryStatementController()
        {
        }

        /// <summary>
        /// Summary Statement constructor.  Controller controls the following views:
        ///     - Summary Statement 
        /// </summary>
        /// <param name="theSummaryManagementService">The summary management service.</param>
        /// <param name="theCriteriaService">The criteria service.</param>
        /// <param name="theWorkflowService">The workflow service.</param>
        /// <param name="theSummaryProcessingService">The summary processing service.</param>
        /// <param name="theFileService">The file service.</param>
        /// <param name="theApplicationManagementService">The application management service.</param>
        /// <param name="thePanelManagementService">The panel management list service.</param>
        /// <param name="theReportViewerService">The report viewer service.</param>
        /// <param name="theClientSummaryService">The client summary service.</param>
        public SummaryStatementController(ISummaryManagementService theSummaryManagementService, ICriteriaService theCriteriaService, 
            IWorkflowService theWorkflowService, ISummaryProcessingService theSummaryProcessingService, 
            IFileService theFileService, IApplicationManagementService theApplicationManagementService,
            IPanelManagementService thePanelManagementService, IReportViewerService theReportViewerService, IClientSummaryService theClientSummaryService)
        {
            this.theSummaryManagementService = theSummaryManagementService;
            this.theCriteriaService = theCriteriaService;
            this.theWorkflowService = theWorkflowService;
            this.theSummaryProcessingService = theSummaryProcessingService;
            this.theFileService = theFileService;
            this.theApplicationManagementService = theApplicationManagementService;
            this.thePanelManagementService = thePanelManagementService;
            this.theReportViewerService = theReportViewerService;
            this.theClientSummaryService = theClientSummaryService;
        }

        /// <summary>
        /// Dispose of the controller
        /// </summary>
        /// <param name="disposing">Indicates if the object should be disposed</param>
        protected override void Dispose(bool disposing)
        {
            //
            // if the object has not been disposed & we should dispose the object
            // 
            if ((!this._disposed) && (disposing))
            {
                // RDL 
                // need to dispose of services here.
                //
                base.Dispose(disposing);
                this._disposed = true;
            }
        }
        #endregion

        #region Controller Actions
        /// <summary>
        /// Retrieves all available summary statements to move to next stage
        /// </summary>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult Index()
        {
            ViewModels.Shared.TabMenuViewModel.HasPermission = HasPermission;
            var vm = new SummaryStatementViewModel();
            try
            {
                //
                // Sets the client list for the specific user & get the user's
                // list of programs then populate the view model with this list.
                //
                List<int> clientList = GetUsersClientList();
                var programs = theCriteriaService.GetAllClientPrograms(clientList);
                vm.Programs = programs.ModelList.OrderBy(x => x.ProgramAbbreviation).ToList();
                //
                if (IsSessionParametersExisting())
                {
                    vm = SetFilterDropdownsFromSession(vm);
                    GetSsPanelFilterVars(vm);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            // send view model to view page
            return View(vm);
        }
        /// <summary>
        /// Gets the applications json.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult GetStagedApplicationsJson(int programId, int yearId, int? cycle, int? panelId, int? awardTypeId)
        {
            //Dictionary<int, string> listOverviewOrder = new Dictionary<int, string>();
            StagedApplicationsViewModel results = new StagedApplicationsViewModel();
            try
            {
                if (programId >= 0)
                {

                    SetSsPanelFilterVars(programId, yearId, cycle, panelId, awardTypeId);
                    var apps = theSummaryManagementService.GetSummaryStatementApplications(programId, yearId, cycle, panelId, awardTypeId).ModelList.ToList();
                    foreach(var ap in apps)
                    {
                        if (!string.IsNullOrEmpty(ap.OverallScore))
                        {
                            ap.OverallScore = ap.OverallScore.Substring(0, stringLength);
                            if (ap.Order == "ND") ap.OverallScore = ap.Order;
                        }                        
                        
                    }
                    var workflowOptions = this.theSummaryManagementService.GetClientSummaryStatementWorkflows(programId).ModelList.ToList();
                    results = new StagedApplicationsViewModel(apps, workflowOptions);
                    results.RefreshTime = ViewHelpers.FormatDateTime(GlobalProperties.P2rmisDateTimeNow);
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
            // and format the results into Json format.
            //
            return Json(results, JsonRequestBehavior.AllowGet);
        }     
        /// <summary>
        /// Retrieves all available summary statements to move to next stage
        /// </summary>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult Progress()
        {
            ViewModels.Shared.TabMenuViewModel.HasPermission = HasPermission;
            var vm = new ProgressViewModel();
            try
            {
                // Sets the client list for the specific user
                List<int> clientList = GetUsersClientList();
                // Get users program list based off of their client list
                var programs = theCriteriaService.GetAllClientPrograms(clientList);
                vm.Programs = programs.ModelList.OrderBy(x => x.ProgramAbbreviation).ToList();

                if (IsSessionParametersExisting())
                {
                    vm = SetFilterDropdownsFromSession(vm);

                    var summaryGroups = this.theSummaryManagementService.GetPanelSummaries((int)Session[SessionVariables.ClientProgramId], (int)Session[SessionVariables.ProgramYearId], (int?)Session[SessionVariables.Cycle], (int?)Session[SessionVariables.PanelId], (int?)Session[SessionVariables.AwardTypeId], (int?)Session[Constants.UserIdSession]);
                    vm.SummaryGroup = new List<WebModel.ISummaryGroup>(summaryGroups.ModelList.Cast<WebModel.ISummaryGroup>());
                    GetSsPanelFilterVars(vm);
                    vm.IsWebBased = theSummaryManagementService.IsSsWebBased((int)Session[SessionVariables.ClientProgramId]);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(vm);
        }
        /// <summary>
        /// Retrieves all completed summary statements
        /// </summary>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult Completed()
        {
            ViewModels.Shared.TabMenuViewModel.HasPermission = HasPermission;
            var vm = new ProgressViewModel();
            try
            {
                // Gets users current permissions
                CustomIdentity ident = User.Identity as CustomIdentity;
                // Sets the client list for the specific user
                List<int> clientList = GetUsersClientList();
                // Get users program list based off of their client list
                var programs = theCriteriaService.GetAllClientPrograms(clientList);
                vm.Programs = programs.ModelList.OrderBy(x => x.ProgramAbbreviation).ToList();
                vm.HideUserSearchCriteria = true;

                if (IsSessionParametersExisting())
                {
                    vm = SetFilterDropdownsFromSession(vm);
                    GetSsPanelFilterVars(vm);
                    vm.IsWebBased = theSummaryManagementService.IsSsWebBased((int)Session[SessionVariables.ClientProgramId]);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(vm);
        }
        /// <summary>
        /// Gets the completed applications json.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult GetCompletedApplicationsJson(int programId, int yearId, int? cycle, int? panelId, int? awardTypeId)
        {
            CompletedApplicationsViewModel results = new CompletedApplicationsViewModel();
            try
            {
                if (programId >= 0)
                {
                    SetSsPanelFilterVars(programId, yearId, cycle, panelId, awardTypeId);
                    var completedSs = this.theSummaryManagementService.GetCompletedApplications(programId, yearId, panelId, cycle, awardTypeId, GetUserId())
                            .ModelList.ToList();
                    results = new CompletedApplicationsViewModel(completedSs);
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
        /// Generates the reports.
        /// </summary>
        /// <param name="panelApplicationIds">The panel application ids.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult GenerateWebBasedReports(string panelApplicationIds)
        {
            bool flag = false;
            string[] idArray = JsonConvert.DeserializeObject<string[]>(panelApplicationIds);
            string queryString = string.Empty;
            try
            {
                int[] ids = Array.ConvertAll(idArray, int.Parse);

                List<IReportAppInfo> rptInfo = theSummaryManagementService.GetAppReportInfo(ids).ToList();
                queryString = ReportControllerHelpers.BuildQueryString(rptInfo);
                flag = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, queryString = queryString } );
        }
        /// <summary>
        /// Generates the document based reports.
        /// </summary>
        /// <param name="panelApplicationIds">The panel application ids.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult GenerateDocumentBasedReports(string panelApplicationIds)
        {
            bool flag = false;
            string[] idArray = JsonConvert.DeserializeObject<string[]>(panelApplicationIds);
            string zippedFileLocation = String.Empty;
            try
            {
                int[] ids = Array.ConvertAll(idArray, int.Parse);

                zippedFileLocation = theSummaryManagementService.GenerateFinalSummaryDocuments(ids, GetUserId());
                List<IReportAppInfo> rptInfo = theSummaryManagementService.GetAppReportInfo(ids).ToList();
                // Loop and AddSummaryLog
                foreach (ReportAppInfo info in rptInfo)
                {
                    theReportViewerService.LogReportInfo(GetUserId(), info.ApplicationWorkflowId);
                }
                flag = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, filePath = zippedFileLocation });
        }
        #region Workflow History
        /// <summary>
        /// Retrieves the application history for a specified application workflow.
        /// </summary>
        /// <param name="applicationWorkflowId">Application Workflow identifier</param>
        /// <returns>Application workflow transaction history as partial view</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrProcessOrReview)]
        public ActionResult WorkflowHistory(int applicationWorkflowId)
        {
            var workflowHistoryViewModel = new WorkflowHistoryViewModel();
            try
            {
                if (applicationWorkflowId > 0)
                {
                    // get the application workflow transactions
                    var workflowHistory = theSummaryManagementService.GetWorkflowTransactionHistory(applicationWorkflowId);
                    // get the application details
                    var applicationDetail = theSummaryProcessingService.GetApplicationDetail(applicationWorkflowId);

                    workflowHistoryViewModel.WorkflowTransactions = new List<WebModel.IWorkflowTransactionModel>(workflowHistory.ModelList);
                    workflowHistoryViewModel.ApplicationDetail = applicationDetail;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            // send view model to view page
            return PartialView(ViewNames.WorkflowHistory, workflowHistoryViewModel);
        }
        #endregion
        #endregion
        #region Helpers
        /// <summary>
        /// Retrieve the fiscal years for the selected program.
        /// </summary>
        /// <param name="selectedProgram">Selected program abbreviation</param>
        /// <returns>List of fiscal years in Json format</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrProcessOrReview)]
        public ActionResult GetFiscalYearsJson(int selectedProgram)
        {
            var list = theCriteriaService.GetAllProgramYears(selectedProgram, HasPermission(Permissions.SummaryStatement.DisplayAssignedPanels), GetUserId()); 
            var result = list.ModelList.OrderByDescending(x => x.Year);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Retrieve the panels for the selected program and fiscal years.
        /// </summary>
        /// <param name="selectedFY">Fiscal year selected (ex "2014")</param>
        /// <param name="selectedProgram">Selected program abbreviation</param>
        /// <returns>List of panels in Json format</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrProcessOrReview)]
        public ActionResult GetPanelsJson(int selectedFY)
        {
            var list = theCriteriaService.GetSessionPanels(selectedFY, HasPermission(Permissions.SummaryStatement.DisplayAssignedPanels), GetUserId());
            var result = list.ModelList.OrderBy(x => x.PanelAbbreviation);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Retrieve the cycles for the selected program and fiscal years.
        /// </summary>
        /// <param name="selectedFY">Fiscal year selected (ex "2014")</param>
        /// <param name="selectedProgram">Selected program abbreviation</param>
        /// <returns>List of cycles in Json format</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrProcessOrReview)]
        public ActionResult GetProgramCyclesJson(int selectedFY)
        {
            var list = theCriteriaService.GetProgramYearCycles(selectedFY, HasPermission(Permissions.SummaryStatement.DisplayAssignedPanels), GetUserId());  
            var result = list.ModelList.OrderBy(x => x);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Retrieve the awards for the selected program/fiscal years/cycle and or panelId.
        /// </summary>
        /// <param name="theCriteriaService">Criteria service</param>
        /// <param name="selectedProgram">the Program abbreviation selected</param>
        /// <param name="selectedFy">the Fiscal year selected</param>
        /// <param name="selectedCycle">the selected cycle</param>
        /// <param name="selectedPanel">the selected panel id</param>
        /// <returns>List of awards in Json format</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrProcessOrReview)]
        public ActionResult GetAwardsJson(int selectedFY, int? selectedCycle, int? selectedPanel)
        {
            var list = theCriteriaService.GetAwards(selectedFY, selectedCycle, selectedPanel, HasPermission(Permissions.SummaryStatement.DisplayAssignedPanels), GetUserId());
            var result = list.ModelList.OrderBy(x => x.AwardAbbreviation);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Retrieves list of assigned reviewers 
        /// </summary>
        /// <param name="selectedProgram">the selected program</param>
        /// <param name="selectedFy">the selected fiscal year</param>
        /// <param name="selectedCycle">the selected cycle</param>
        /// <param name="selectedPanel">the selected panel</param>
        /// <param name="selectedAward">the selected award</param>
        /// <param name="substring">the string being typed in by the user</param>
        /// <returns>list of assigned reviewers</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult GetReviewerNames(int selectedProgram, int selectedFy, int? selectedCycle, int? selectedPanel, int? selectedAward, string substring)
        {
            return Json(GetSearchReviewersList(selectedProgram, selectedFy, selectedCycle, selectedPanel, selectedAward, substring), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Blank action result for Menu item to control display
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ManageOrProcessOrReview)]
        public ActionResult Menu()
        {
            return View();
        }
        /// <summary>
        /// Sets the filter dropdown menus from the session variables
        /// </summary>
        /// <param name="theViewModel">the view model</param>
        /// <returns>SummaryStatement view model with populated selection values</returns>
        private SummaryStatementViewModel SetFilterDropdownsFromSession(SummaryStatementViewModel theViewModel)
        {
            //
            // Test for a null session.  This is necessary to support unit testing
            //
            if (Session != null)
            {
                theViewModel.SelectedProgram = (int) Session[SessionVariables.ClientProgramId];
                //populate the fiscal years list from the programs selected in the session
                var fys = theCriteriaService.GetAllProgramYears((int)Session[SessionVariables.ClientProgramId]);
                theViewModel.FiscalYears = fys.ModelList.OrderByDescending(o => o.Year).ToList();
                //populate the panel list from the fiscal years selected in the session
                var panels = this.theCriteriaService.GetSessionPanels((int)Session[SessionVariables.ProgramYearId]);
                theViewModel.Panels = panels.ModelList.OrderBy(x => x.PanelAbbreviation).ToList();
                //populate the cycle list from the program and fiscal year selected in the session
                var cycles = this.theCriteriaService.GetProgramYearCycles((int)Session[SessionVariables.ProgramYearId]);
                theViewModel.Cycles = cycles.ModelList.OrderBy(x => x).ToList();
                if ((Session[SessionVariables.Cycle] != null) || (Session[SessionVariables.PanelId] != null))
                {
                    //populate the award list from the program/fiscal year/cycle/panel in the session
                    var awards = this.theCriteriaService.GetAwards((int)Session[SessionVariables.ProgramYearId], (int?)Session[SessionVariables.Cycle], (int?)Session[SessionVariables.PanelId]);
                    theViewModel.Awards = awards.ModelList.OrderBy(x => x.AwardAbbreviation).ToList();
                }
            }
            return theViewModel;
        }
        /// <summary>
        /// Sets the filter dropdown menus from the session variables
        /// </summary>
        /// <param name="theViewModel">the view model</param>
        /// <returns></returns>
        private ProgressViewModel SetFilterDropdownsFromSession(ProgressViewModel theViewModel)
        {
            if (Session != null)
            {
                theViewModel.SelectedProgram = (int)Session[SessionVariables.ClientProgramId];
                //populate the fiscal years list from the programs selected in the session
                var fys = theCriteriaService.GetAllProgramYears((int)Session[SessionVariables.ClientProgramId]);
                theViewModel.FiscalYears = fys.ModelList.OrderByDescending(o => o.Year).ToList();
                //populate the panel list from the fiscal years selected in the session
                var panels = this.theCriteriaService.GetSessionPanels((int)Session[SessionVariables.ProgramYearId]);
                theViewModel.Panels = panels.ModelList.OrderBy(x => x.PanelAbbreviation).ToList();
                //populate the cycle list from the program and fiscal year selected in the session
                var cycles = this.theCriteriaService.GetProgramYearCycles((int)Session[SessionVariables.ProgramYearId]);
                theViewModel.Cycles = cycles.ModelList.OrderBy(x => x).ToList();
                if ((Session[SessionVariables.Cycle] != null) || (Session[SessionVariables.PanelId] != null))
                {
                    //populate the award list from the program/fiscal year/cycle/panel in the session
                    var awards = this.theCriteriaService.GetAwards((int)Session[SessionVariables.ProgramYearId], (int?)Session[SessionVariables.Cycle], (int?)Session[SessionVariables.PanelId]);
                    theViewModel.Awards = awards.ModelList.OrderBy(x => x.AwardAbbreviation).ToList();
                }
            }
            return theViewModel;
        }
        /// <summary>
        /// Sets the filter dropdown menus from the session variables
        /// </summary>
        /// <param name="theViewModel">the view model</param>
        /// <returns>the view model</returns>
        private ManageWorkflowViewModel SetFilterDropdownsFromSession(ManageWorkflowViewModel theViewModel)
        {
            if (Session != null)
            {
                theViewModel.SelectedProgram = (int)Session[SessionVariables.ClientProgramId];
                //populate the fiscal years list from the programs selected in the session
                var fys = theCriteriaService.GetAllProgramYears((int)Session[SessionVariables.ClientProgramId]);
                theViewModel.FiscalYears = fys.ModelList.OrderByDescending(o => o.Year).ToList();
                //populate the panel list from the fiscal years selected in the session
                var panels = this.theCriteriaService.GetSessionPanels((int)Session[SessionVariables.ProgramYearId]);
                theViewModel.Panels = panels.ModelList.OrderBy(x => x.PanelAbbreviation).ToList();
                //populate the cycle list from the program and fiscal year selected in the session
                var cycles = this.theCriteriaService.GetProgramYearCycles((int)Session[SessionVariables.ProgramYearId]);
                theViewModel.Cycles = cycles.ModelList.OrderBy(x => x).ToList();
                if ((Session[SessionVariables.Cycle] != null) || (Session[SessionVariables.PanelId] != null))
                {
                    //populate the award list from the program/fiscal year/cycle/panel in the session
                    var awards = this.theCriteriaService.GetAwards((int)Session[SessionVariables.ProgramYearId], (int?)Session[SessionVariables.Cycle], (int?)Session[SessionVariables.PanelId]);
                    theViewModel.Awards = awards.ModelList.OrderBy(x => x.AwardAbbreviation).ToList();
                }
            }
            return theViewModel;
        }
        /// <summary>
        /// Retrieve a list of reviewers for a given reviewer substring limited to the available filter values.
        /// </summary>
        /// <param name="program">Program filter value</param>
        /// <param name="fiscalYear">Fiscal year filter value</param>
        /// <param name="cycleId">Cycle filter value id</param>
        /// <param name="panelId">Panel filter value id</param>
        /// <param name="awardId">Award filter value id</param>
        /// <param name="reviewerName">String to search</param>
        /// <returns>Enumerable list of reviewer names.  If there are no reviewers or no parameter is passed an empty list is returned</returns>
        private IEnumerable<WebModel.IUserModel> GetSearchReviewersList(int program, int fiscalYear, int? cycleId, int? panelId, int? awardId, string reviewerName)
        {
            //
            // Create a default return & retrieve the user's client(s)
            //
            IEnumerable<WebModel.IUserModel> list = new List<WebModel.IUserModel>();
            CustomIdentity ident = User.Identity as CustomIdentity;

            if (SearchReviewersListParametersValid(reviewerName, ident))
            {
                // get list of programs from BLL
                var container = theSummaryManagementService.GetSearchReviewersList(GetUsersClientList(), program, fiscalYear, cycleId, panelId, awardId, reviewerName);
                list = container.ModelList;
                WebModel.UserModel.NameFormatter = ViewHelpers.ConstructNameWithComma;
             }

            return list;
        }
        /// <summary>
        /// Checks if the parameters are valid for the SearchReviewersList().  To be valid:
        ///  - reviewerName is not null
        ///  - reviewerName does not containWhitespace & greater than 0 length
        ///  - CustomIdentity is not null
        ///  - CustomIdentity.UserClientList is not null & greater than 0 
        /// </summary>
        /// <param name="reviewerName">Reviewer name substring</param>
        /// <param name="identity">Users custom identity</param>
        /// <returns>True if parameters valid; false otherwise</returns>
        private bool SearchReviewersListParametersValid(string reviewerName, CustomIdentity identity)
        {
            return ((!string.IsNullOrWhiteSpace(reviewerName)) && (identity != null) && (GetUsersClientList() != null) && (GetUsersClientList().Count > 0));
        }
        /// <summary>
        /// Views the application files as a modal
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>the partial page of the log numbers files</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ViewApplicationModal(int panelApplicationId)
        {
            WebModel.ApplicationInfoModel theViewModel = new WebModel.ApplicationInfoModel();

            var theApplicationDetail = theSummaryManagementService.GetPreviewApplicationInfoDetail(panelApplicationId);
            var theFile = theFileService.GetFileInfo(theApplicationDetail.ApplicationId);

            theViewModel.theApplicationFileInfo = theFile.ModelList.ToList();
            theViewModel.theApplicationDetail = theApplicationDetail;

            // return the populated view model
            return PartialView(ViewNames.ViewApplicationPartial, theViewModel);
        }
        /// <summary>
        /// Gets the URL for viewing the application file.
        /// </summary>
        /// <param name="logNumber">The log number.</param>
        /// <param name="fileSuffix">The file suffix (including file extension).</param>
        /// <returns>File on success, redirect if failed.</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ViewApplicationFile(string logNumber, string fileSuffix)
        {
            var fileUrl = $"/SummaryStatement/ApplicationFile?logNumber={logNumber}&fileSuffix={fileSuffix}";
            //file and download URL is same as this is PDF document
            return PdfViewer(fileUrl, fileUrl);            
        }
        /// <summary>
        /// Gets the URL for viewing the application file.
        /// </summary>
        /// <param name="logNumber">The log number.</param>
        /// <param name="fileSuffix">The file suffix (including file extension).</param>
        /// <returns>File on success, redirect if failed.</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ApplicationFile(string logNumber, string fileSuffix)
        {
            string fileName = $"{logNumber}{fileSuffix}";
            try
            {
                var fileContent = theFileService.GetApplicationFile(logNumber, fileSuffix);
                return File(fileContent, FileConstants.MimeTypes.Pdf, fileName);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                //
                // Redirect to file not found error page
                //
                return RedirectToAction("FileNotFound", "ErrorPage");
            }
        }
        /// <summary>
        /// Gets the abstract file from the legacy server and returns to the user.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="logNumber">The log number.</param>
        /// <returns>PDF version of the abstract</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetAbstractFile(int applicationId, string logNumber)
        {
            var fileUrl = $"/SummaryStatement/GetAbstract?applicationId={applicationId}&logNumber={logNumber}";
            //file and download URL is same as this is PDF document
            return PdfViewer(fileUrl, fileUrl);           
        }
        /// <summary>
        /// Gets the abstract file from the legacy server and returns to the user.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="logNumber">The log number.</param>
        /// <returns>PDF version of the abstract</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetAbstract(int applicationId, string logNumber)
        {            
            byte[] fileContent;
            string fileName = String.Format("{0}_abstract.pdf", logNumber);
            try
            {
                var abstractData = theFileService.RetrieveAbstractFile(applicationId, thePanelManagementService);
                if (abstractData.Type == WebModel.AbstractFileType.TextType)
                {
                    //if text we need to do work to convert to pdf before sending to user
                    fileContent = PdfServices.CreatePdf(Encoding.Default.GetString(abstractData.AbstractText), string.Empty, BaseUrl,
                        DepPath);
                }
                else
                {
                    fileContent = abstractData.AbstractText;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                //
                // Redirect to file not found error page
                //
                return RedirectToAction("FileNotFound", "ErrorPage");

            }
            return File(fileContent, "application/pdf", fileName);
        }
        #endregion        
        /// <summary>
        /// Gets the progress applications json.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult GetProgressApplicationsJson(int programId, int yearId, int? cycle, int? panelId, int? awardTypeId)
        {
            SummaryStatementNewProgressViewModel results = new SummaryStatementNewProgressViewModel();
            try
            {
                if (programId >= 0)
                {
                    SetSsPanelFilterVars(programId, yearId, cycle, panelId, awardTypeId);
                    var apps = theSummaryManagementService.GetSummaryStatementApplicationsInProgress(programId, yearId, cycle, panelId, awardTypeId).ModelList.ToList();
                    results = new SummaryStatementNewProgressViewModel(apps);
                    SummaryStatementProgressViewModel.ScoreFormatter = ViewHelpers.ScoreFormatter;
                    results.RefreshTime = ViewHelpers.FormatDateTime(GlobalProperties.P2rmisDateTimeNow);
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
            // and format the results into Json format.
            //
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}