
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing a selection of application properties to be assigned to a workflow
    /// </summary>
    public class WorkflowMechanismModel : IWorkflowMechanismModel
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public WorkflowMechanismModel()
        {
            //
            // Ensure that there is always a container for the workflow steps and mechanisms
            // 
            Steps = new List<WorkflowStepModel>();
            Mechanisms = new List<MechanismCycleModel>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Workflow identifier
        /// </summary>
        public int WorkflowId { get; set; }
        /// <summary>
        /// Workflow name
        /// </summary>
        public string WorkflowName { get; set; }
        /// <summary>
        /// Label of priority for the workflow's applications
        /// </summary>
        public string Priority { get; set; }
        /// <summary>
        /// Whether the particular workflow template is editable (it is not considered a default workflow for it's assigned client
        /// </summary>
        public bool IsEditable { get; set; }
        /// <summary>
        /// This workflow's steps
        /// </summary>
        public ICollection<WorkflowStepModel> Steps { get; set; }
        /// <summary>
        /// Mechanism information for which the workflow is assigned
        /// </summary>
        public ICollection<MechanismCycleModel> Mechanisms { get; set; }
        #endregion
    }
}
