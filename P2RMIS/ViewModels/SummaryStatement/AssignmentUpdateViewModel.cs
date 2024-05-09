using System.Collections.Generic;
using Sra.P2rmis.WebModels.SummaryStatement;
using System.Linq;
namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for assign users (SSM-310).  Additionally it contains functionality
    /// to roll back a summary statement to a previous workflow phase.
    /// </summary>
    public class AssignmentUpdateViewModel : SSFilterMenuViewModel
    {
        #region Constants
        /// <summary>
        /// Defines symbols used in the view and referenced in the controller.
        /// </summary>
        public class Labels
        {
            public const string FormWorkflowIds = "workflowIds";
            public const string FormUserIds = "userIds";
            public const string FormApplicationIds = "appIds";
            public const string FormTargetWorkflowStepIds = "targetStepIds";
            public const string FormCurrentWorkflowStepIds = "currentStepIds";
            public const string FormPanelApplicationIds = "panelApplicationId";
            public const string FormUnassignedWorkflowIds = "unassignedWorkflowIds";
            public const string FormWorkflowAction = "formAction";
            public const string FormNewAssignee = "newAssigneeId";
            public const string FormActionAssign = "Assign";
            public const string FormActionUnassign = "Unassign";
        }
        #endregion

        #region Properties
        /// <summary>
        /// the selected application IDs.
        /// </summary>
        public List<int> ApplicationIds { get; set; }
        /// <summary>
        /// the assignments selected by the user
        /// </summary>
        public List<IUserApplicationModel> Assignments { get; set; }
        /// <summary>
        /// Workflow phases available for assignment of the selected applications
        /// </summary>
        public List<WorkflowStepViewModel> AvailableWorkflowPhases => this.Assignments.SelectMany(x => x.Steps.Where(w => w.Active))
                                                                                    .GroupBy(g => new { g.StepName, g.StepOrder, g.StepTypeId })
                                                                                    .Select(y => new WorkflowStepViewModel
                                                                                    {
                                                                                        StepName = y.Key.StepName,
                                                                                        StepOrder = y.Key.StepOrder,
                                                                                        StepTypeId = y.Key.StepTypeId
                                                                                    })
                                                                                    .OrderBy(o => o.StepOrder)
                                                                                    .ToList();
        #endregion
        #region Constructor & Setup
        /// <summary>
        /// Constructor.  Initialize contents to safe values
        /// </summary>
        public AssignmentUpdateViewModel()
        {
            this.ApplicationIds = new List<int>();
            this.Assignments = new List<IUserApplicationModel>();
        }
        #endregion
        /// <summary>
        /// ViewModel information for workflow phases/steps
        /// </summary>
        public class WorkflowStepViewModel
        {
            #region Properties
            /// <summary>
            /// Name of a Step
            /// </summary>
            public string StepName { get; set; }

            /// <summary>
            /// Order in which the step occurs
            /// </summary>
            public int StepOrder { get; set; }

            /// <summary>
            /// Step type identifier
            /// </summary>
            public int StepTypeId { get; set; }
            #endregion
        }
    }
}