using Newtonsoft.Json;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.SummaryStatement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// Base controller for P2RMIS Summary Statement controller.  
    /// Basically a container for Summary Statement controller common functionality.
    /// </summary>
    public class SummaryStatementBaseController : BaseController
    {
        #region Constants
        /// <summary>
        /// Class identifies session variables used by the SummaryManagement controller.
        /// </summary>
        public class Constants
        {
            public const string ProgramSession = "SsProgram";
            public const string FySession = "SsFy";
            public const string CycleSession = "SsCycle";
            public const string PanelSession = "SsPanel";
            public const string AwardSession = "SsAward";
            public const string UserIdSession = "SsUserId";
            public const string UserNameSession = "SsUserName";
            public const string NO_KEY = "";
            public const string SearchInstruction = "Please select at least a Program and Fiscal Year from the filter menu above.";
            public const string ZeroSearchResults = "There are no results matching the searched criteria.";
            public const string PreviewAffix = "_preview";
            /// <summary>
            /// Names of views in Summary Management Application
            /// </summary>
            public class ViewNames
            {
                public const string AssignmentUpdate = "AssignmentUpdate";
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// Service providing access to the Summary management services.
        /// </summary>
        protected ISummaryManagementService theSummaryManagementService { get; set; }
        /// <summary>
        /// Service providing access to the common search criteria services.
        /// </summary>
        protected ICriteriaService theCriteriaService { get; set; }
        /// <summary>
        /// Service providing access to the common workflow services.
        /// </summary>
        protected IWorkflowService theWorkflowService { get; set; }
        /// <summary>
        /// Indicates if the object has been disposed but not garbage collected.
        /// </summary>
        protected ISummaryProcessingService theSummaryProcessingService { get; set; }
        /// <summary>
        /// Service providing access to the file services
        /// </summary>
        protected IFileService theFileService { get; set; }
        /// <summary>
        /// Service providing access to the Client Summary services.
        /// </summary>
        protected IClientSummaryService theClientSummaryService { get; set; }
        /// <summary>
        /// Service providing access to list retrieval.
        /// </summary>
        protected ILookupService theLookupService { get; set; }
        /// <summary>
        /// Gets or sets the application management service.
        /// </summary>
        /// <value>
        /// The application management service.
        /// </value>
        protected IApplicationManagementService theApplicationManagementService { get; set; }
        /// <summary>
        /// Gets or sets the panel management list service.
        /// </summary>
        /// <value>
        /// The panel management list service.
        /// </value>
        protected IPanelManagementService thePanelManagementService { get; set; }
        /// <summary>
        /// Gets or sets the report viewer service.
        /// </summary>
        /// <value>
        /// The report viewer service.
        /// </value>
        protected IReportViewerService theReportViewerService { get; set; }
        #endregion
        #region Helpers
        /// /// <summary>
        /// Sets the summary statement filter session variables
        /// </summary>
        /// <param name="program">program selected by user</param>
        /// <param name="fy">fiscal year selected by user</param>
        /// <param name="cycle">cycle selected by user</param>
        /// <param name="panel">Panel selected by the user</param>
        /// <param name="award">Award (Mechanism) selected by the user</param>
        /// <remarks>
        /// Setting the session variables is surrounded by an if block that 
        /// checks for the existence of Session.  This is necessary to support unit testing of the 
        /// controller methods.
        /// </remarks>
        protected void SetSsPanelFilterVars(int program, int fy, int? cycle, int? panel, int? award)
        {
            if (Session != null)
            {
                Session[SessionVariables.ClientProgramId] = program;
                Session[SessionVariables.ProgramYearId] = fy;
                Session[SessionVariables.Cycle] = cycle;
                Session[SessionVariables.PanelId] = panel;
                Session[SessionVariables.AwardTypeId] = award;
            }
        }

        protected void SetSsPanelFilterVarsLoad(int? program, int? fy, int? cycle, int? panel, int? award)
        {
            if (Session != null)
            {
                Session[SessionVariables.ClientProgramId] = program;
                Session[SessionVariables.ProgramYearId] = fy;
                Session[SessionVariables.Cycle] = cycle;
                Session[SessionVariables.PanelId] = panel;
                Session[SessionVariables.AwardTypeId] = award;
            }
        }
        /// <summary>
        /// Retrieves the summary statement filter session variables and puts them into model
        /// </summary>
        /// <param name="model">the ISSFilterMenuModel model</param>
        protected void GetSsPanelFilterVars(ISSFilterMenuModel model)
        {
            if (Session != null)
            {
                model.SelectedProgram = (int)Session[SessionVariables.ClientProgramId];
                model.SelectedFy = (int)Session[SessionVariables.ProgramYearId];
                model.SelectedCycle = (int?)Session[SessionVariables.Cycle];
                model.SelectedPanel = (int?)Session[SessionVariables.PanelId];
                model.SelectedAward = (int?)Session[SessionVariables.AwardTypeId];
            }
        }
        /// <summary>
        /// Verifies we have valid parameters for GetSummaryApplications
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <returns>True if parameters are valid; false otherwise</returns>
        protected bool IsGetSummaryApplicationsParametersValid(int program, int fiscalYear)
        {
            return (
                    (program > 0) &&
                    (fiscalYear > 0)
                    );
        }
        /// <summary>
        /// Verifies we have valid parameters for GetSummaryApplications.
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="panelId">ProgramPanel entity identifier</param>
        /// <param name="isFiltered">Indicates if the list of programs should be filtered</param>
        /// <returns>True if the parameters are valid.  False otherwise</returns>
        protected bool IsGetSummaryApplicationsParametersValid(int program, int fiscalYear, int? panelId, bool isFiltered)
        {
            bool result = IsGetSummaryApplicationsParametersValid(program, fiscalYear);
            if (isFiltered)
            {
                result &= (panelId.HasValue) && (panelId > 0);
            }

            return result;
        }

        /// <summary>
        /// Verifies to see if Session parameters existing
        /// </summary>
        /// <returns>True if session parameters exist; false otherwise</returns>
        protected bool IsSessionParametersExisting()
        {
            return (
                    (Session[SessionVariables.ClientProgramId] != null) &&
                    (Session[SessionVariables.ProgramYearId] != null)
                   );
        }
        /// <summary>
        /// Verifies that the Session parameters exist.  This version checks
        /// if a panel is required and if so a panel selection is in session.
        /// </summary>
        /// <param name="isFiltered">Indicates if the list of programs should be filtered</param>
        /// <returns></returns>
        protected bool IsSessionParametersExisting(bool isFiltered)
        {
            bool result = IsSessionParametersExisting();
            if (isFiltered)
            {
                result &= (Session[SessionVariables.PanelId] != null);
            }
            return result;
        }
        /// <summary>
        /// Sets view model information for editing a summary statement
        /// </summary>
        /// <param name="theViewModel">the edit view model</param>
        /// <param name="applicationWorkflowId">the application workflow id</param>
        /// <returns>the populated edit summary statement view model</returns>
        protected EditSummaryStatementViewModel GetDataForEditing(EditSummaryStatementViewModel theViewModel, int applicationWorkflowId)
        {
            // calls to the BLL to get DL content for step content and app details
            var stepContent = theSummaryProcessingService.GetApplicationStepContent(applicationWorkflowId);
            var appDetail = theSummaryProcessingService.GetApplicationDetail(applicationWorkflowId);
            var workflow = theWorkflowService.GetSingleWorkflowProgress(applicationWorkflowId);
            theViewModel.ActiveWorkflowStep = theWorkflowService.GetActiveApplicationWorkflowStep(applicationWorkflowId);

            // sort and order the content for presentation
            List<IStepContentModel> criteria = new List<IStepContentModel>(stepContent.ModelList);
            var sortedCriteria = ControllerHelpers.OrderContentForPresentation(criteria);
            var newSortedCriteria = ControllerHelpers.NewOrderContentForPresentation(criteria);
            theViewModel.OverallCriteria = newSortedCriteria.Item1;
            theViewModel.ScoredCriteria = newSortedCriteria.Item2;
            theViewModel.UnScoredCriteria = newSortedCriteria.Item3;
            theViewModel.DoDisplayAverageScoreTable = DetermineIfAverageScoreTableShouldDisplay(appDetail.IsTriaged, criteria);
            //if displaying average score table, individual score table can be empty, else average score table can be empty
            if (theViewModel.DoDisplayAverageScoreTable)
            {
                theViewModel.AverageScoreTableData = ExtractScoresForAverageScoreTable(criteria);
                theViewModel.IndividualScoreTableData = new List<SummaryIndividualScoreModel>();
            }
            else
            {
                theViewModel.IndividualScoreTableData = ExtractScoresForIndividualScoreTable(criteria);
                theViewModel.AverageScoreTableData = new SummaryAverageScoreModel();
            }
            // set the properties in the view model
            theViewModel.Criteria = sortedCriteria;
            theViewModel.ApplicationDetails = appDetail;
            theViewModel.TheWorkflow = workflow.ModelList;
            UserName name = theSummaryManagementService.GetSummaryCheckedOutUserName(theViewModel.ActiveWorkflowStep);

            // set user identifier and user name
            theViewModel.CheckoutUserId = GetUserId();
            theViewModel.CheckoutName = (name != null) ? ViewHelpers.ConstructNameWithComma(name.FirstName, name.LastName) : string.Empty;
            theViewModel.CheckoutUsername = (name != null) ? name.UsersName : string.Empty;
            //
            // The size of the in line comment & general comments are configurable.  Set the configured size here.
            //
            theViewModel.InlineCommentMaximum = ConfigManager.CommentInlineMaximum;
            theViewModel.GeneralCommentMaximum = ConfigManager.CommentGeneralMaximum;

            var adminBudgetNote = theApplicationManagementService.GetApplicationAdminBudgetNote(appDetail.ApplicationId);
            theViewModel.AdminNote = adminBudgetNote.Comment;

            // return the populated view model
            return theViewModel;
        }
        /// <summary>
        /// Determines whether average score table should display on the page
        /// </summary>
        /// <param name="isTriaged">Whether the application is triaged</param>
        /// <param name="criteria">Collection of SS Criteria</param>
        /// <returns>Boolean whether the table should display</returns>
        internal bool DetermineIfAverageScoreTableShouldDisplay(bool isTriaged, List<IStepContentModel> criteria)
        {
            //Currently the only we know if this should display is if the app is not triaged and it has meeting scores
            return !isTriaged && criteria.Any(x => x.ElementContentAverageScore != null);
        }

        /// <summary>
        /// Extracts scores in format suitable for individual score table
        /// </summary>
        /// <param name="criteria">List of criteria</param>
        /// <returns>List of scores for individual score table</returns>
        internal IList<SummaryIndividualScoreModel> ExtractScoresForIndividualScoreTable(List<IStepContentModel> criteria)
        {
            var tableData = criteria
                .Where(w => w.ElementScoreFlag && !w.DiscussionNoteFlag)
                .DefaultIfEmpty(new StepContentModel())
                .GroupBy(g => new
                {
                    g.ElementName,
                    g.ElementSortOrder,
                    g.ElementOverallFlag,
                    g.ElementScoreType,
                    g.ElementScaleLowValue,
                    g.ElementScaleHighValue
                })
                .OrderByDescending(o => o.Key.ElementOverallFlag)
                .ThenBy(o => o.Key.ElementSortOrder)
                .Select(x => new SummaryIndividualScoreModel
            {
                CriteriaName = x.Key.ElementName,
                CriteriaSortOrder = x.Key.ElementSortOrder,
                IsOverall = x.Key.ElementOverallFlag,
                ScoreType = x.Key.ElementScoreType,
                ScaleHighValue = x.Key.ElementScaleHighValue,
                ScaleLowValue = x.Key.ElementScaleLowValue,
                ReviewerScores = x.Select(s => new ReviewerScoreModel
                {
                    ReviewerSortOrder = s.ReviewerAssignmentOrder,
                    Score = s.ElementContentScore,
                    AdjectivalValue = s.ElementContentAdjectivalLabel,
                    ScoreType = s.ElementScoreType
                }).OrderBy(o => o.ReviewerSortOrder)
            }).ToList();
            return tableData;
        }
        /// <summary>
        /// Extracts scores in format suitable for average score table
        /// </summary>
        /// <param name="criteria">List of criteria</param>
        /// <returns>List of score for average score table</returns>
        internal SummaryAverageScoreModel ExtractScoresForAverageScoreTable(List<IStepContentModel> criteria)
        {
            var tableData = new SummaryAverageScoreModel
            {
                OverallScore = criteria.Where(w => w.ElementOverallFlag).DefaultIfEmpty(new StepContentModel()).First().ElementContentAverageScore,
                OverallStandardDeviation = criteria.Where(w => w.ElementOverallFlag).DefaultIfEmpty(new StepContentModel()).First().ElementScoreStandardDeviation,
                OverallScaleHigh = criteria.Where(w => w.ElementOverallFlag).DefaultIfEmpty(new StepContentModel()).First().ElementScaleHighValue,
                OverallScaleLow = criteria.Where(w => w.ElementOverallFlag).DefaultIfEmpty(new StepContentModel()).First().ElementScaleLowValue,
                CriteriaScores = criteria.Where(w => !w.ElementOverallFlag && w.ElementScoreFlag).DefaultIfEmpty(new StepContentModel()).GroupBy(g => new
                {
                    g.ElementName,
                    g.ElementContentAverageScore,
                    g.ElementSortOrder
                }).Select(s => new CriteriaAverageScoreModel
                {
                    CriteriaScore = s.Key.ElementContentAverageScore,
                    CriteriaSortOrder = s.Key.ElementSortOrder,
                    CriteriaDescription = s.Key.ElementName
                }).OrderBy(o => o.CriteriaSortOrder)
            };
            return tableData;
        }
        /// <summary>
        /// Check-in the summary statement.
        /// </summary>
        /// <param name="theWorkflowService">The workflow service</param>
        /// <param name="workflowId">The ApplicationWorkflow entity identifier</param>
        /// <returns>Status of the summary statement check-in as a JSON result</returns>
        protected JsonResult Checkin(int applicationWorkflowId)
        {
            bool result = false;
            try
            {
                theWorkflowService.ExecuteCheckinWorkflow(applicationWorkflowId, GetUserId());
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
        /// Directs user to a specific summary statement to edit
        /// </summary>
        /// <param name="workflowId">the applications workflow id</param>
        /// <returns>the view model to summary statement edit page</returns>
        protected EditSummaryStatementViewModel CreateSummaryStatementViewModel(int workflowId, bool isReviewContext)
        {
            // instantiate view model
            EditSummaryStatementViewModel editSummaryStatementVM = new EditSummaryStatementViewModel();

            try
            {
                // go to the BLL and get the data for editing
                editSummaryStatementVM = GetDataForEditing(editSummaryStatementVM, workflowId);

                CustomIdentity ident = User.Identity as CustomIdentity;
                // check if the current user has a Manage permission
                editSummaryStatementVM.HasManagePermission = IsValidPermission(Permissions.SummaryStatement.Manage);
                editSummaryStatementVM.SetCommentPermissions(isReviewContext);
                editSummaryStatementVM.CanAcceptTrackChanges = IsValidPermission(Permissions.SummaryStatement.AcceptTrackChanges);
                editSummaryStatementVM.SetCheckinDidplayIndicator(IsValidPermission(Permissions.SummaryStatement.CheckIntoAnyPhase));
                // get previous workflow steps
                editSummaryStatementVM.PreviousWorkflowSteps = GetPreviousWorkflowSteps(editSummaryStatementVM.TheWorkflow, editSummaryStatementVM.ActiveWorkflowStep);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                editSummaryStatementVM = new EditSummaryStatementViewModel();
            }

            //return the view model
            return editSummaryStatementVM;
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
        /// performs the check out action for the summary
        /// </summary>
        /// <param name="applicationWorkflowId">the application workflow id.</param>
        /// <returns>the Boolean indicator of the checkout action status</returns>
        protected JsonResult Checkout(int applicationWorkflowId)
        {
            bool isSuccessful = false;
            string msg = string.Empty;
            try
            {
                bool isCheckedOut = this.theSummaryManagementService.IsSsCheckedOut(applicationWorkflowId);
                if (!isCheckedOut)
                {
                    // gets users current permissions
                    int userId = GetUserId();
                    isSuccessful = theWorkflowService.ExecuteCheckoutWorkflow(applicationWorkflowId, userId);
                }
                else
                {
                    msg = MessageService.CheckOutFailure;
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            var returnResult = new { IsSuccessful = isSuccessful, Message = msg };
            var returnResultInJson = JsonConvert.SerializeObject(returnResult);
            return Json(returnResultInJson, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}