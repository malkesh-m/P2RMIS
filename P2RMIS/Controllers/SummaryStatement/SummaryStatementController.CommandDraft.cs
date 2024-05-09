using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.FileServices;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.Reports;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// SummaryStatement methods for marking the application as CommandDraft
    /// </summary>
    public partial class SummaryStatementController
    {
        public class CommandDraftConstants
        {
            public const string PushSelected = "Start Process";
            public const string SaveChanges = "Save Changes";
            public const string Assign = "Assign";
            public const string Generate = "Generate";

            public const string AssignmentSessionVar = "AssignmentUpdateDataContainer";
        }
        /// <summary>
        /// Assigns the specified panel application ids.
        /// </summary>
        /// <param name="panelApplicationIds">The panel application ids.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult Assign(string panelApplicationIds)
        {
            bool flag = false;
            string[] idArray = JsonConvert.DeserializeObject<string[]>(panelApplicationIds);
            string queryString = string.Empty;
            try
            {
                int[] ids = Array.ConvertAll(idArray, int.Parse);
                if (ids != null)
                {
                    // Put view model into TempData to work with redirection
                    Session[CommandDraftConstants.AssignmentSessionVar] = ids.ToList();
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
            }
            return Json(new { flag = flag });
        }
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult EditPriorities()
        {
            EditPrioritiesViewModel viewModel = new EditPrioritiesViewModel();
            SSFilterMenuViewModel filterModel = new SSFilterMenuViewModel();
            // Retrieve list of applications being edited from Session
            List<int> panelApplicationsToEdit = Session[CommandDraftConstants.AssignmentSessionVar] as List<int>;
            try
            {
                //set filter model options from session
                GetSsPanelFilterVars(filterModel);
                var applications = theClientSummaryService.GetRequestReviewApplications(filterModel.SelectedProgram, filterModel.SelectedFy,
                filterModel.SelectedCycle, filterModel.SelectedPanel, filterModel.SelectedAward).ModelList;
                //filter applications based on selected IDs
                var filteredApplications = applications.Where(x => panelApplicationsToEdit.Contains(x.PanelApplicationId));
                viewModel.PopulateApplications(filteredApplications);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View(viewModel);
        }
        /// <summary>
        /// Starts the applications.
        /// </summary>
        /// <param name="applications">The applications.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult StartApplications(string applications)
        {
            bool flag = false;
            dynamic[] apps = JsonConvert.DeserializeObject<dynamic[]>(applications);
            List<string> messages = new List<string>();
            List<int> ids = new List<int>();
            IServiceState serviceState;
            try
            {
                // Moves applications into processing
                var collection = BuildPriorityChangeList(apps, GetUserId());
                serviceState = theSummaryManagementService.StartApplications(collection, thePanelManagementService);
                if (serviceState.IsSuccessful)
                {
                    theSummaryManagementService.CreateSummaryDocuments(collection, thePanelManagementService);
                    flag = true;
                }
                else
                {
                    var msgs = serviceState.Messages.ToList();
                    var entities = serviceState.EntityInfo.ToList();
                    for (var i = 0; i < msgs.Count; i++)
                    {
                        messages.Add(msgs[i]);
                        ids.Add(entities[i].EntityId);
                    }
                }
            }
            catch (Exception exception)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
                messages.Add(MessageService.UnexpectedException);
            }
            return Json(new { flag = flag, messages = messages, ids = ids });
        }
        /// <summary>
        /// Saves the priority changes.
        /// </summary>
        /// <param name="applications">The applications.</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult SavePriorityChanges(EditPrioritiesViewModel vm)
        {
            try
            {

                var collection = BuildPriorityChangeListFromModel(vm, GetUserId());
                theWorkflowService.ProgressSaveChanges(collection);
                TempData["SuccessMessage"] = MessageService.PrioritySaveSuccess;
            }
            catch (Exception exception)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
                TempData["FailureMessage"] = MessageService.PrioritySaveFailure;
            }
            return RedirectToAction("EditPriorities");
        }
        #region Helpers
        /// <summary>
        /// Build priority change list.
        /// </summary>
        /// <param name="applications">The applications.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private ICollection<ChangeToSave> BuildPriorityChangeList(dynamic[] applications, int userId)
        {
            List<ChangeToSave> result = new List<ChangeToSave>();

            for (int i = 0; i < applications.Length; i++)
            {
                result.Add(new ChangeToSave((string)applications[i].panelApplicationId, (string)applications[i].logNumber,
                    (string)applications[i].priority1, (string)applications[i].priority2,
                    (string)applications[i].workflowId, userId));
            }
            return result;
        }
        /// <summary>
        /// Build priority change list.
        /// </summary>
        /// <param name="applications">The applications.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private ICollection<ChangeToSave> BuildPriorityChangeListFromModel(EditPrioritiesViewModel model, int userId)
        {
            List<ChangeToSave> result = new List<ChangeToSave>();

            foreach (var item in model.SummaryApplications)
            {
                result.Add(new ChangeToSave(item.PanelApplicationId, item.LogNumber,
                    model.PriorityOneSelection ?? ConvertYesNoToBooleanString(item.Priority1), model.PriorityTwoSelection ?? ConvertYesNoToBooleanString(item.Priority2), item.ApplicationWorkflowId, userId));
            }
            return result;
        }

        /// <summary>
        /// Converts a yes/no string to boolean true/false string equivalent
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns>string equivalent</returns>
        private string ConvertYesNoToBooleanString(string s)
        {
            return (s.ToLower() == "yes") ? "true" : "false";
        }
        #endregion
    }
}