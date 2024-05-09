using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.Web.ViewModels.SummaryStatement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebModel = Sra.P2rmis.WebModels.SummaryStatement;
using Newtonsoft.Json;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// SummaryStatement methods for Manage Workflow
    /// </summary>
    public partial class SummaryStatementController
    {
        #region ControllerActions
        /// <summary>
        /// Returns view to manage workflow assignments
        /// </summary>
        /// <returns>manage workflows view</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult ManageWorkflow(int? programId, int? yearId, int? cycle, int? panelId, int? awardTypeId)
        {
            ViewModels.Shared.TabMenuViewModel.HasPermission = HasPermission;

            if (programId > 0)
            {
                SetSsPanelFilterVarsLoad(programId, yearId, cycle, panelId, awardTypeId);
            }

            ManageWorkflowViewModel results = new ManageWorkflowViewModel();
            try
            {
                //
                // Sets the client list for the specific user & get the user's
                // list of programs then populate the view model with this list.
                //
                List<int> clientList = GetUsersClientList();
                var programs = theCriteriaService.GetAllClientPrograms(clientList);
                results.Programs = programs.ModelList.OrderBy(x => x.ProgramAbbreviation).ToList();
                // checks to see if session parameters already exist
                if (IsSessionParametersExisting())
                {
                    results = PopulateManageWorkflowModel(results);
                    GetSsPanelFilterVars(results);
                }
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(results);
        }
        /// <summary>
        /// Returns view to manage workflow assignments
        /// </summary>
        /// <returns>manage workflows view</returns>
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult ManageWorkflowCall(int programId, int yearId, int? cycle, int? panelId, int? awardTypeId)
        {
            if (programId > 0)
            {
                SetSsPanelFilterVars(programId, yearId, cycle, panelId, awardTypeId);
            }
            List<WorkflowViewModel> workflows = new List<WorkflowViewModel>();
            var workflowOptions = new List<KeyValuePair<int, string>>();
            try
            {
                // checks to see if session parameters already exist
                if (IsSessionParametersExisting())
                {
                    var workflowDropdown = theSummaryManagementService.GetClientWorkflowAll(programId, yearId);
                    workflowOptions = workflowDropdown.ModelList.ToList().ConvertAll(x => new KeyValuePair<int, string>(x.WorkflowId, x.WorkflowName));
                    var workflowList = theSummaryManagementService.GetWorkflowAssignmentOrDefault((int)Session[SessionVariables.ClientProgramId], (int)Session[SessionVariables.ProgramYearId], (int?)Session[SessionVariables.Cycle]);
                    workflows = workflowList.ModelList.ToList().ConvertAll(x => new WorkflowViewModel(x));
                }

            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { workflows = workflows, workflowOptions = workflowOptions }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Save workflows
        /// </summary>
        /// <param name="applications">The application data</param>
        /// <returns></returns>
        [HttpPost]
        [Sra.P2rmis.Web.Common.Authorize(Operations = Permissions.SummaryStatement.Manage)]
        public ActionResult SaveWorkflows(string applications)
        {
            var flag = false;
            var saved = false;
            dynamic[] apps = JsonConvert.DeserializeObject<dynamic[]>(applications);
            try
            {
                // Build the list to save
                var workflowsToSave = BuildWorkflowSaveList(apps);
                // Save the workflows
                saved = theSummaryManagementService.AssignWorkflow(workflowsToSave, GetUserId());
                flag = true;
            }
            catch (Exception e)
            {
                //
                // Log the exception 
                //
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return Json(new { flag = flag, saved = saved }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Build workflow save list.
        /// </summary>
        /// <param name="appliations">The application data.</param>
        /// <returns></returns>
        private List<AssignWorkflowToSave> BuildWorkflowSaveList(dynamic[] appliations)
        {
            List<AssignWorkflowToSave> workflowsToSave = new List<AssignWorkflowToSave>();

            for (int i = 0; i < appliations.Length; i++)
            {
                dynamic application = appliations[i];
                AssignWorkflowToSave entry1 = new AssignWorkflowToSave();
                entry1.SetPriorityOneWorkflow((string)application.MechanismId, (string)application.PriorityOneWorkflowId);
                workflowsToSave.Add(entry1);

                AssignWorkflowToSave entry2 = new AssignWorkflowToSave();
                entry2.SetPriorityTwoWorkflow((string)application.MechanismId, (string)application.PriorityTwoWorkflowId);
                workflowsToSave.Add(entry2);

                AssignWorkflowToSave entry3 = new AssignWorkflowToSave();
                entry3.SetNoPriorityWorkflow((string)application.MechanismId, (string)application.NoWorkflowId);
                workflowsToSave.Add(entry3);
            }
            return workflowsToSave;
        }

        /// <summary>
        /// This populates the ManageWorkflowViewModel with data after page refresh or assign action
        /// </summary>
        /// <param name="theModel">the view model</param>
        /// <returns>the view model populated</returns>
        private ManageWorkflowViewModel PopulateManageWorkflowModel(ManageWorkflowViewModel theModel)
        {
            //
            // Sets the client list for the specific user & get the user's
            // list of programs then populate the view model with this list.
            //
            theModel.SelectedProgram = (int)Session[SessionVariables.ClientProgramId];
            List<int> clientList = GetUsersClientList();
            var programs = theCriteriaService.GetAllClientPrograms(clientList);
            theModel.Programs = programs.ModelList.OrderBy(x => x.ProgramAbbreviation).ToList();
            theModel = SetFilterDropdownsFromSession(theModel);
            var workflowDropdown = theSummaryManagementService.GetClientWorkflowAll((int)Session[SessionVariables.ClientProgramId], (int)Session[SessionVariables.ProgramYearId]);
            theModel.WorkflowDropDown = workflowDropdown.ModelList.ToList().ConvertAll(x => new KeyValuePair<int, string>(x.WorkflowId, x.WorkflowName));
            theModel.HideUserSearchCriteria = true;

            // get program and year text
            theModel.ProgramYear = theModel.FiscalYears.Where(x => x.ProgramYearId == (int)Session[SessionVariables.ProgramYearId]).Select(x => x.Year).FirstOrDefault();
            theModel.ProgramAbbreviation = theModel.Programs.Where(x => x.ClientProgramId == (int)Session[SessionVariables.ClientProgramId]).Select(x => x.ProgramAbbreviation).FirstOrDefault();

            return theModel;
        }
        #endregion
    }
}