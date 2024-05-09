using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.FileServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.Web.ViewModels.SummaryStatement;
using Sra.P2rmis.Web.ViewModels.SummaryStatementProcessing;
using Sra.P2rmis.WebModels.SummaryStatement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// Controller Summary Statement Processing page tabs:
    ///   - SSM-600 - Summary Statement Editor 
    ///   - SSM-700 - Available Assignments Editor
    ///   - SSM-710 - My Assignments Editor
    ///   - SSM-201 - View Notes
    /// </summary>
    public class SummaryStatementProcessingController : SummaryStatementBaseController
    {
        #region Constants
        /// <summary>
        /// Class identifies the views controlled by this controller.
        /// </summary>
        public class ViewNames
        {
            public const string Index = "Index";
            public const string checkoutBtn = "checkout";
            public const string checkinBtn = "checkin";
            public const string Notes = @"..\SummaryStatement\_Notes";
            public const string Preview = @"..\SummaryStatement\_SummaryPreview";
            public const string WorkflowStepList = "_WorkflowStepSelection";
            public const string UploadFile = "_UploadFile";
            public const string ViewApplicationModal = "_ViewApplicationModal";
        }
        
        /// <summary>
        /// action text for editing a summary statement
        /// </summary>
        public const string editSummaryAction = "EditSummaryStatement";
        /// <summary>
        /// action text for my draft summary statement
        /// </summary>
        public const string assignmentsAction = "Assignments";

        public const string saveSuccessful = "Saved Successfully";
        public const string saveUnsuccessful = "Error while Saving";
        #endregion

        #region Construction; Setup & Disposal

        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private SummaryStatementProcessingController()
        {
        }

        /// <summary>
        /// Summary processing constructor.  Controller controls the following views:
        /// </summary>
        /// <param name="theSummaryManagementService">The summary management service.</param>
        /// <param name="theCriteriaService">The criteria service.</param>
        /// <param name="theWorkflowService">The workflow service.</param>
        /// <param name="theSummaryProcessingService">The summary processing service.</param>
        /// <param name="theLookupService">The lookup service.</param>
        /// <param name="theFileService">The file service.</param>
        /// <param name="theApplicationManagementService">The application management service.</param>
        public SummaryStatementProcessingController(ISummaryManagementService theSummaryManagementService, ICriteriaService theCriteriaService, IWorkflowService theWorkflowService, 
            ISummaryProcessingService theSummaryProcessingService, ILookupService theLookupService, IFileService theFileService,
            IApplicationManagementService theApplicationManagementService)
        {
            this.theSummaryManagementService = theSummaryManagementService;
            this.theCriteriaService = theCriteriaService;
            this.theWorkflowService = theWorkflowService;
            this.theSummaryProcessingService = theSummaryProcessingService;
            this.theLookupService = theLookupService;
            this.theFileService = theFileService;
            this.theApplicationManagementService = theApplicationManagementService;
        }
        /// <summary>
        /// Dispose of the SummaryStatementProcessing Controller's unmanaged resources.  
        /// </summary>
        public override void DisposeUnmanagedResources()
        {
            // 
            // Service disposal
            //
            this.theSummaryManagementService = null;
            this.theWorkflowService = null;
            this.theSummaryProcessingService = null;
        }
        #endregion

        #region Controller Actions
        /// <summary>
        /// Retrieves all available summary statements to move to next stage
        /// </summary>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ProcessOrReview)]
        public ActionResult Index()
        {
            ViewModels.Shared.TabMenuViewModel.HasPermission = HasPermission;
            var vm = new ProcessingViewModel();
            try
            {
                // Get users current permissions
                int userId = GetUserId();
                bool hasPermission = HasPermission(Permissions.SummaryStatement.DisplayAssignedPanels);
                //
                // Sets the client list for the specific user & get the user's
                // list of programs then populate the view model with this list.
                //
                List<int> clientList = GetUsersClientList();
                var programs = theCriteriaService.GetAllClientPrograms(clientList, hasPermission, userId);  
                vm.Programs = programs.ModelList.OrderBy(x => x.ProgramAbbreviation).ToList();
                var isClient = !IsValidPermission(Permissions.SummaryStatement.ManageOrProcess);
                vm.IsClient = isClient;
                vm.IsPanelRequired = hasPermission;
                vm.RefreshTime = ViewHelpers.FormatDateTime(GlobalProperties.P2rmisDateTimeNow);

                if (IsSessionParametersExisting(hasPermission))
                {
                    SetProcessingViewModelWithSessionInformation(vm, hasPermission, userId);
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
        /// Does the heavy lifting to set up the view model.
        /// </summary>
        /// <param name="processingVM">ProcessingViewModel</param>
        /// <param name="isFiltered">Indicates if the list of programs should be filtered</param>
        /// <param name="userId">User identifier</param>
        internal virtual void SetProcessingViewModelWithSessionInformation(ProcessingViewModel processingVM, bool isFiltered, int userId)
        {
            processingVM = SetFilterDropdownsFromSession(processingVM, isFiltered, userId);
            GetSsPanelFilterVars(processingVM);

        }
        /// <summary>
        /// Gets available summary statements
        /// </summary>
        /// <param name="processingVM">SummaryStatementProcessing View Model</param>
        /// <returns>Available application view populated with grid populated</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetAvailableApplicationsJson(int clientProgramId, int programYearId, 
                int? cycle, int? panelId, int? awardTypeId)
        {
            var vm = new AssignmentsViewModel();
            try
            {
                int userId = GetUserId();
                bool hasPermission = HasPermission(Permissions.SummaryStatement.DisplayAssignedPanels);
                
                // Initialize the client client list; 
                List<int> clientList = GetUsersClientList();
                // Get users program list based off of their client list
                var programs = theCriteriaService.GetAllClientPrograms(clientList, hasPermission, userId);
                // Get list of available applications
                if (IsGetSummaryApplicationsParametersValid(clientProgramId, programYearId, panelId, hasPermission))
                {
                    SetSsPanelFilterVars(clientProgramId, programYearId, cycle, panelId, awardTypeId);
                    var canAccessAdminNote = IsValidPermission(Permissions.SummaryStatement.AccessAdminNote);
                    var canAccessGeneralNote = IsValidPermission(Permissions.SummaryStatement.AccessGeneralNote);
                    var canAccessDiscussionNote = IsValidPermission(Permissions.SummaryStatement.AccessDiscussionNote);
                    var canAccessUnassignedReviewerNote = IsValidPermission(Permissions.SummaryStatement.AccessUnassignedReviewerNote);
                    var results = theSummaryProcessingService.GetDraftSummmariesAvailableForCheckout(userId,
                        clientProgramId, programYearId, cycle, panelId, awardTypeId,
                        canAccessDiscussionNote, canAccessGeneralNote, canAccessUnassignedReviewerNote).ModelList;
                    var reviewOnly = IsValidPermission(Permissions.SummaryStatement.Review);
                    var editingOnly = IsValidPermission(Permissions.SummaryStatement.EditingOnly);
                    var writingOnly = IsValidPermission(Permissions.SummaryStatement.WritingOnly);
                    var manageOrProcess = IsValidPermission(Permissions.SummaryStatement.ManageOrProcess);
                    if (reviewOnly && !manageOrProcess)
                    {
                        results = results.Where(x => x.IsClientReviewStepType);
                    }
                    else if (editingOnly && !writingOnly) 
                    {
                        results = results.Where(x => x.IsEditingStepType);
                    }
                    else if (writingOnly && !editingOnly)
                    {
                        results = results.Where(x => x.IsWritingStepType);
                    }
                    vm.Statements = results.ToList().ConvertAll(x => new AssignmentViewModel((SummaryAssignedModel)x))
                        .Select((item, index) => { item.IsClient = reviewOnly && !manageOrProcess; return item; }).ToList();
                    vm.RefreshTime = ViewHelpers.FormatDateTime(GlobalProperties.P2rmisDateTimeNow);

                    var webBased = theSummaryManagementService.IsSsWebBased(clientProgramId);
                    vm.IsWebBased = webBased;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Sets the filter dropdown menus from the session variables
        /// </summary>
        /// <param name="theViewModel">the view model</param>
        /// <param name="isFiltered">Indicates if the list of programs should be filtered</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>ProcessingViewModel view model with populated selection values</returns>
        private ProcessingViewModel SetFilterDropdownsFromSession(ProcessingViewModel theViewModel, bool isFiltered, int userId)
        {
            //
            // Test for a null session.  This is necessary to support unit testing
            //
            if (Session != null)
            {
                //
                // Since the program year is used multiple times, pull it out & cast only once.
                //
                int programYearId = (int)Session[SessionVariables.ProgramYearId];

                theViewModel.SelectedProgram = (int)Session[SessionVariables.ClientProgramId];
                //populate the fiscal years list from the programs selected in the session
                var fys = theCriteriaService.GetAllProgramYears((int)Session[SessionVariables.ClientProgramId], isFiltered, userId);
                theViewModel.FiscalYears = fys.ModelList.OrderByDescending(o => o.Year).ToList();
                //populate the panel list from the fiscal years selected in the session
                var panels = this.theCriteriaService.GetSessionPanels(programYearId, isFiltered, userId);
                theViewModel.Panels = panels.ModelList.OrderBy(x => x.PanelAbbreviation).ToList();
                //populate the cycle list from the program and fiscal year selected in the session
                var cycles = this.theCriteriaService.GetProgramYearCycles(programYearId, isFiltered, userId);
                theViewModel.Cycles = cycles.ModelList.OrderBy(x => x).ToList();
                if ((Session[SessionVariables.Cycle] != null) || (Session[SessionVariables.PanelId] != null))
                {
                    //populate the award list from the program/fiscal year/cycle/panel in the session
                    var awards = this.theCriteriaService.GetAwards(programYearId, (int?)Session[SessionVariables.Cycle], (int?)Session[SessionVariables.PanelId], isFiltered, userId);
                    theViewModel.Awards = awards.ModelList.OrderBy(x => x.AwardAbbreviation).ToList();
                }
            }
            return theViewModel;
        }
        /// <summary>
        /// Retrieves the assignments for a given user
        /// </summary>
        /// <returns>Assignments view model to the assignments page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ProcessOrReview)]
        public ActionResult Assignments()
        {
            ViewModels.Shared.TabMenuViewModel.HasPermission = HasPermission;
            AssignmentsViewModel vm = new AssignmentsViewModel();
            return View(vm);
        }
        /// <summary>
        /// Gets my draft applications.
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetMyDraftApplicationsJson()
        {
            // Instantiate view model
            AssignmentsViewModel vm = new AssignmentsViewModel();
            try
            {
                vm.RefreshTime = ViewHelpers.FormatDateTime(GlobalProperties.P2rmisDateTimeNow);
                var canAccessAdminNote = IsValidPermission(Permissions.SummaryStatement.AccessAdminNote);
                var canAccessGeneralNote = IsValidPermission(Permissions.SummaryStatement.AccessGeneralNote);
                var canAccessDiscussionNote = IsValidPermission(Permissions.SummaryStatement.AccessDiscussionNote);
                var canAccessUnassignedReviewerNote = IsValidPermission(Permissions.SummaryStatement.AccessUnassignedReviewerNote);
                var isClient = !IsValidPermission(Permissions.SummaryStatement.ManageOrProcess);
                // Get assignments from BLL and set it to view model   
                var results = this.theSummaryProcessingService.GetDraftSummmariesCheckedout(GetUserId(),
                    canAccessDiscussionNote, canAccessGeneralNote, canAccessUnassignedReviewerNote).ModelList.ToList();
                if (results.Count > 0)
                {
                    var clientProgramId = results[0].ClientProgramId;
                    var webBased = theSummaryManagementService.IsSsWebBased(clientProgramId);
                    vm.IsWebBased = webBased;
                }
                vm.Statements = results.ConvertAll(x => new AssignmentViewModel((SummaryAssignedModel)x))
                    .Select((item, index) => { item.Index = index + 1; item.IsClient = isClient; return item; }).ToList();
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Changes the workflow's active workflow step id
        /// </summary>
        /// <param name="workflowId">the applications workflow id</param>
        /// <param name="workflowStepId">the new active workflow step id</param>
        /// <returns>the partial view model to the workflow section on summary statement editing page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Process)]
        public ActionResult AssignWorkflow(int workflowId, int workflowStepId)
        {
            // instantiate view model
            EditSummaryStatementViewModel editSummaryStatementVM = new EditSummaryStatementViewModel();

            try
            {
                // gets users current permissions
                int userId = GetUserId();

                // Change active workflow step
                theWorkflowService.ExecuteAssignWorkflow(workflowId, userId, workflowStepId);

                // go get the data to populate the view model and put it into the view model
                editSummaryStatementVM = GetDataForEditing(editSummaryStatementVM, workflowId);

                CustomIdentity ident = User.Identity as CustomIdentity;
                // this method is only accessible for accounts with a Manage permission
                editSummaryStatementVM.HasManagePermission = true;
                editSummaryStatementVM.SetCommentPermissions(false);
                // get previous workflow steps
                editSummaryStatementVM.PreviousWorkflowSteps = GetPreviousWorkflowSteps(editSummaryStatementVM.TheWorkflow, editSummaryStatementVM.ActiveWorkflowStep);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            //return back to the workflow section at editing the summary
            return PartialView("_WorkflowWidget", editSummaryStatementVM);
        }

        /// <summary>
        /// performs the check out action for the summary
        /// </summary>
        /// <param name="applicationWorkflowId">the application workflow id.</param>
        /// <returns>the Boolean indicator of the checkout action status</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult CheckoutAction(int applicationWorkflowId)
        {
            //
            // Call the base method to checkout the workflow.
            //
            return Checkout(applicationWorkflowId);
        }
        /// <summary>
        /// performs the check out action for the summary
        /// </summary>
        /// <param name="workflowId">the workflow id</param>
        /// <returns>the Boolean indicator of the check-in action status</returns>
        /// <remarks>
        ///   The indicator may seem to be the opposite of what was intended.  The service layer returns
        ///   an indication if the summary statement is currently checked out (ie. true means it is; false 
        ///   it is not).
        /// </remarks>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult CheckinAction(int workflowId)
        {
            return Checkin(workflowId);
        }
        /// <summary>
        /// Directs user to a specific summary statement to edit
        /// </summary>
        /// <param name="workflowId">the applications workflow id</param>
        /// <returns>the view model to summary statement edit page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Process)]
        public ActionResult EditSummaryStatement(int workflowId)
        {
            bool isReviewContext = false;
            //
            // instantiate view model & return it to the page
            //
            EditSummaryStatementViewModel viewModel = CreateSummaryStatementViewModel(workflowId, isReviewContext);

            return View(viewModel);
        }
        /// <summary>
        /// gets previous workflow steps
        /// </summary>
        /// <param name="workflow">the workflow</param>
        /// <param name="activeWorkflowStep">the active workflow step</param>
        /// <returns>a list of previous workflow steps</returns>
        private List<KeyValuePair<int, string>> GetPreviousWorkflowSteps(IEnumerable<ApplicationWorkflowStepModel> workflow, int activeWorkflowStep)
        {
            List<KeyValuePair<int, string>> previousWorkflowSteps = new List<KeyValuePair<int, string>>();
            foreach (var item in workflow)
            {
                if (item.ApplicationWorkflowStepId != activeWorkflowStep)
                {
                    previousWorkflowSteps.Add(new KeyValuePair<int, string>(item.ApplicationWorkflowStepId, item.StepName));
                }
                else
                {
                    // the current step and following steps are not previous steps
                    break;
                }
            }
            return previousWorkflowSteps;
        }

        /// <summary>
        /// performs a user defined action for the summary
        /// </summary>
        /// <param name="submit">the name of the action chosen by the user</param>
        /// <param name="workflowId">the workflow id of the SS being edited</param>
        /// <returns>view model back to page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Process)]
        public ActionResult SummaryAction(string submit, int workflowId)
        {
            try
            {
                // gets users current permissions
                int userId = GetUserId();

                //determines the action to perform on the summary
                switch (submit)
                {
                    case ViewNames.checkoutBtn:
                        theWorkflowService.ExecuteCheckoutWorkflow(workflowId, userId);
                        break;
                    case ViewNames.checkinBtn:
                        theWorkflowService.ExecuteCheckinWorkflow(workflowId, userId);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            //return back to my draft summary statement
            return RedirectToAction(assignmentsAction);
        }

        /// <summary>
        /// controller action to save specific content while editing a summary statement
        /// </summary>
        /// <param name="workflowId">workflow id of the SS</param>
        /// <param name="contentId">the contents Id (could be null if it doesn't exist for discussion notes and overview)</param>
        /// <param name="stepElementId">the step element of the workflow</param>
        /// <param name="content">the content to be updated/inserted</param>
        /// <returns>whether the save was successful or not</returns>
        [ValidateInput(false)]//this line is so that the application wont throw an error when trying to submit html back as a variable
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ProcessOrReview)]
        public ActionResult SaveContent(int workflowId, int? contentId, int stepElementId, string content)
        {
            try
            {
                // gets users current permissions
                int userId = GetUserId();
                // call BLL service to save/insert data
                theWorkflowService.ExecuteSaveWorkflow(workflowId, userId, contentId.GetValueOrDefault(0), content, stepElementId);
                // return that save was successful
                return Content(saveSuccessful);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                // return that the save was unsuccessful
                return Content(saveUnsuccessful);
            }
        }

        /// <summary>
        /// Checks in the summary statement.
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="targetApplicationWorkflowStepId">The target application workflow step identifier.</param>
        /// <param name="summaryStatement">The summary statement.</param>
        /// <returns>
        /// Post upload redirect, as necessary
        /// </returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ProcessOrReview)]
        public ActionResult CheckInSummaryStatement(int applicationWorkflowId, int targetApplicationWorkflowStepId, HttpPostedFileBase summaryStatement)
        {
            string result = String.Empty;
            try
            {
                // Validate and process form data
                var inputData = FileServices.GetBinary(summaryStatement.InputStream);
                var uploadResult = theSummaryManagementService.ProcessSummaryStatementUpload(applicationWorkflowId, targetApplicationWorkflowStepId, inputData, GetUserId());
                // Execute the assign action to transfer content to next step
                if (uploadResult.IsSuccessful)
                {
                    theWorkflowService.ExecuteAssignWorkflowWithFile(applicationWorkflowId, GetUserId(),
                            targetApplicationWorkflowStepId, inputData);
                    // return that save was successful (tentative may use redirect)
                    result = string.Empty;
                }
                else
                {
                    var msgs = uploadResult.Messages.ToList();
                    if (msgs.Count> 0)
                    {
                        result = msgs[0];
                    }
                    else
                    {
                        result = uploadResult.Messages.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                // return that the save was unsuccessful
                result = saveUnsuccessful;
            }
            return Content(result);
        }
        /// <summary>
        /// View general comments
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>The comments in JSON format</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.ProcessOrReview)]
        public ActionResult ViewComments(int panelApplicationId)
        {
            NotesViewModel theViewModel = new NotesViewModel();
            var results = theSummaryManagementService.GetApplicationSummaryComments(panelApplicationId);
            theViewModel.Notes = new List<ISummaryCommentModel>(results.ModelList);
            //
            // return the populated view model
            //
            return Json(theViewModel, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Adds the supplied comment to the indicated application for the current user
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="applicationID">The application identifier.</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <returns>The result message.</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Review)]
        public ActionResult AddComment(string comment, string applicationID, int panelApplicationId)
        {
            try
            {
                // get the current user
                int userId = GetUserId();
                // add comment
                theSummaryManagementService.AddApplicationSummaryComment(comment, applicationID,panelApplicationId, userId);
                // return that save was successful
                return Content(saveSuccessful);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                // return that the save was unsuccessful
                return Content(saveUnsuccessful);
            }
        }
        /// <summary>
        /// Edits the supplied comment for the current user
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns>The result message.</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Review)]
        public ActionResult EditComment(string comment, int commentId)
        {
            try
            {
                // get the current user
                int userId = GetUserId();
                // add comment
                theSummaryManagementService.EditApplicationSummaryComment(comment, commentId, userId);
                // return that save was successful
                return Content(saveSuccessful);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                // return that the save was unsuccessful
                return Content(saveUnsuccessful);
            }
        }
        /// <summary>
        /// Deleted the comment for the current user
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns>The result message.</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Review)]
        public ActionResult DeleteComment(int commentId)
        {
            try
            {
                // get the current user
                int userId = GetUserId();
                // add comment
                theSummaryManagementService.DeleteApplicationSummaryComment(commentId, userId);
                // return that save was successful
                return Content(saveSuccessful);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                // return that the save was unsuccessful
                return Content(saveUnsuccessful);
            }
        }
        /// <summary>
        /// Retrieves application information model associated with the panel application identifier
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>Partial view with application info model populated.</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ViewApplicationModal(int panelApplicationId)
        {
            ApplicationInfoModel theViewModel = new ApplicationInfoModel();
            var theApplicationDetail = theSummaryManagementService.GetPreviewApplicationInfoDetail(panelApplicationId);

            var theFile = theFileService.GetFileInfo(theApplicationDetail.ApplicationId);

            theViewModel.theApplicationFileInfo = theFile.ModelList.OrderBy(x => x.FileInfo.DisplayLabel).ToList();
            theViewModel.theApplicationDetail = theApplicationDetail;

            return PartialView(ViewNames.ViewApplicationModal, theViewModel);
        }
        /// <summary>
        /// Retrieves a list of workflow steps for the identified application workflow
        /// and populates a partial view.
        /// </summary>
        /// <param name="workflowId">ApplicationWorkflow entity identifier</param>
        /// <returns>Partial view with list of application workflow steps populated.</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult ListWorkflowSteps(int workflowId, int workflowStepId)
        {
            WorkflowStepListViewModel model = new WorkflowStepListViewModel();

            try
            {
                var list = WorkflowStepListViewModel.FormatList(theLookupService.ListWorkflowSteps(workflowId).ModelList.ToList());
                bool isClient = !IsValidPermission(Permissions.SummaryStatement.ManageOrProcess);
                model = new WorkflowStepListViewModel(list, workflowId, workflowStepId, isClient);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return PartialView(ViewNames.WorkflowStepList, model);
        }
        /// <summary>
        /// Check in the summary statement 
        /// </summary>
        /// <param name="workflowId">ApplicationWorkflow entity identifier</param>
        /// <param name="workflowStepId">ApplicationWorkflowStep entity identifier</param>
        /// <returns>True if the workflow was successfully assigned to the workflow step; false otherwise</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult CheckInToStep(int workflowId, int workflowStepId)
        {
            bool result = false;
            try
            {
                theWorkflowService.ExecuteAssignWorkflow(workflowId, GetUserId(), workflowStepId);
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
        /// View action for uploading a summary statement
        /// </summary>
        /// <param name="workflowId">ApplicationWorkflow entity identifier</param>
        /// <param name="workflowStepId">The workflow step identifier.</param>
        /// <returns>
        /// Modal window for uploading a summary statement
        /// </returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult UploadFile(int workflowId, int workflowStepId)
        {
            //instatiate view model
            WorkflowStepListViewModel model = new WorkflowStepListViewModel();
            try
            {
                var list = WorkflowStepListViewModel.FormatList(theLookupService.ListWorkflowSteps(workflowId).ModelList.ToList());
                bool isClient = !IsValidPermission(Permissions.SummaryStatement.ManageOrProcess);
                model = new WorkflowStepListViewModel(list, workflowId, workflowStepId, isClient);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            //populate view model
            return PartialView(ViewNames.UploadFile, model);
        }
        #endregion
    }
}