using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Sra.P2rmis.Bll.Workflow;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.UI.Models;
using WebModel = Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.Bll.SummaryStatements;

namespace Sra.P2rmis.Web.Controllers
{
	public partial class SummaryStatementController
    {
        #region Controller Actions

        /// <summary>
        /// Displays Assignment Update view
        /// </summary>
        /// <returns></returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult AssignmentUpdate()
        {
            AssignmentUpdateViewModel viewModel = new AssignmentUpdateViewModel();
            // Retrieve view model from TempData if exists
            viewModel.ApplicationIds = Session[CommandDraftConstants.AssignmentSessionVar] as List<int>;
            try
            {
                GetAssignments(viewModel);
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(Constants.ViewNames.AssignmentUpdate, viewModel);
        }
        /// <summary>
        /// Assigns the applications to the specified users
        /// </summary>
        /// <param name="form">all form values from the form</param>
        /// <returns>view model to the page</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult AssignAppsToUsers(FormCollection form)
        {
            AssignmentUpdateViewModel viewModel = new AssignmentUpdateViewModel();

            try
            {
                //gets the appIds values to work with from the submitted form
                var workflowIds = form.GetValues(AssignmentUpdateViewModel.Labels.FormWorkflowIds);
                var assigneeId = form.GetValues(AssignmentUpdateViewModel.Labels.FormNewAssignee)[0];
                var assigneeIds = form.GetValues(AssignmentUpdateViewModel.Labels.FormUserIds);
                var targetWorkflowSteps = form.GetValues(AssignmentUpdateViewModel.Labels.FormTargetWorkflowStepIds);
                var currentWorkflowStepIds = form.GetValues(AssignmentUpdateViewModel.Labels.FormCurrentWorkflowStepIds);
                var panelApplicationIds = form.GetValues(AssignmentUpdateViewModel.Labels.FormPanelApplicationIds);
                var isUnassign = form.GetValues(AssignmentUpdateViewModel.Labels.FormWorkflowAction)[0] == AssignmentUpdateViewModel.Labels.FormActionUnassign;

                int userId = GetUserId();


                //
                // build the parameter to feed into the service to perform the assignment
                //
                var assignUserCollection = BuildAssignUserToApplicationCollection(userId, isUnassign, assigneeId, assigneeIds, workflowIds);
                var assignWorkflowCollection = BuildAssignWorkflowStepCollection(workflowIds, targetWorkflowSteps, currentWorkflowStepIds);
                //
                // The Rollback should be done first.
                // 
                if (assignWorkflowCollection.Count > 0)
                    theWorkflowService.ExecuteAssignWorkflow(assignWorkflowCollection, userId);
                if (assignUserCollection.Count > 0) 
                    theWorkflowService.ExecuteAssignUser(assignUserCollection);
                //
                // Repopulate the view model and redisplay the page
                //
                viewModel.ApplicationIds = BuildAssignList(panelApplicationIds);
                GetAssignments(viewModel);
                // populate success message
                if (assignWorkflowCollection.Count + assignUserCollection.Count > 0)
                    ViewBag.SuccessMessage = SummaryStatementMessages.SucessMessage;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                ViewBag.FailureMessage = SummaryStatementMessages.AssignFailureMessage;
            }

            return View(Constants.ViewNames.AssignmentUpdate, viewModel);
        }
        /// <summary>
        /// Gets the drop down list for assigning users to an application
        /// </summary>
        /// <param name="substring">the substring typed in by the user</param>
        /// <returns>user drop down list</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult GetUserListSsAssignments(string substring)
        {
            // get users client list
            List<int> clientList = GetUsersClientList();
            // instantiate user model
            List<WebModel.IUserModel> userList = new List<WebModel.IUserModel>();
            // get available users to assign the SS to
            var users = theSummaryManagementService.GetAutoCompleteUsers(substring, clientList);
            userList = new List<WebModel.IUserModel>(users.ModelList);
            WebModel.UserModel.NameFormatter = ViewHelpers.ConstructNameWithComma;

            //pass user list to the page through Json call
            return Json(userList, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Constructs a collection of AssignUserToApplication objects from two matched arrays.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="unassignedWorkflowIds">Workflow identifiers of unassigned applications</param>
        /// <param name="assigneeId">User identifier of a new assignee</param>
        /// <param name="assigneeIds">Array of user identifier of the assignees</param>
        /// <param name="workflowIds">Array of workflow identifiers</param>
        /// <returns>Collection of AssignUserToApplication objects each representing a Assign User request.</returns>
        private ICollection<AssignUserToApplication> BuildAssignUserToApplicationCollection(int userId, bool isUnassign, string assigneeId, string[] assigneeIds, string[] workflowIds)
        {
            List<AssignUserToApplication> result = new List<AssignUserToApplication>(workflowIds.Length);

            for (int i = 0; i < workflowIds.Length; i++)
            {
                if (isUnassign)
                {
                    // Add an unassignment
                    AssignUserToApplication anAssignment = new AssignUserToApplication(userId, null, workflowIds[i]);
                    result.Add(anAssignment);
                }
                else if (!string.IsNullOrWhiteSpace(assigneeId))
                {
                    // Add an assignment
                    AssignUserToApplication anAssignment = new AssignUserToApplication(userId, assigneeId, workflowIds[i]);
                    result.Add(anAssignment);
                }
                else
                {
                    AssignUserToApplication anAssignment = new AssignUserToApplication(userId, assigneeIds[i], workflowIds[i]);
                    result.Add(anAssignment);
                }
            }
            //return the list to save
            return result;
        }
        /// <summary>
        /// Constructs a list of AssignWorkflowStep objects based on the user's input.  Each AssignWorkflowStep
        /// describes a rollback request.
        /// </summary>
        /// <param name="targetWorkflowStepId">Target workflow step identifiers</param>
        /// <param name="currentWorkflowStepId">Current workflow step identifiers</param>
        /// <returns>Collection of AssignWorkflowStep objects</returns>
        private ICollection<AssignWorkflowStep> BuildAssignWorkflowStepCollection(string[] workflowId, string[] targetWorkflowStepId, string[] currentWorkflowStepId)
        {
            ICollection<AssignWorkflowStep> result = new List<AssignWorkflowStep>();
            if (targetWorkflowStepId != null)
            {
                for (int i = 0; i < targetWorkflowStepId.Length; i++)
                {
                    var item = AssignWorkflowStep.Create(workflowId[i], currentWorkflowStepId[i], targetWorkflowStepId[i]);
                    if (item != null)
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Converts list of strings into list of integers
        /// </summary>
        /// <param name="appIds">list of strings of application ids</param>
        /// <returns>list of application ids</returns>
        private List<int> BuildAssignList(string[] appIds)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < appIds.Length; i++)
            {
                result.Add(Convert.ToInt32(appIds[i]));
            }
            return result;
        }
        /// <summary>
        /// Gets current application assignments
        /// </summary>
        /// <param name="viewModel">the view model</param>
        /// <returns>the view model with application assignments</returns>
        private AssignmentUpdateViewModel GetAssignments(AssignmentUpdateViewModel viewModel)
        {
            var assignments = theSummaryManagementService.GetAssignedUsers(viewModel.ApplicationIds);
            viewModel.Assignments = new List<WebModel.IUserApplicationModel>(assignments.ModelList);
            //
            // We need to add an additional workflow step to signify they want to do completion.
            //
            SummaryManagementService.CompleteWorkflowsSteps(viewModel.Assignments);

            return viewModel;
        }
        #endregion
    }
}