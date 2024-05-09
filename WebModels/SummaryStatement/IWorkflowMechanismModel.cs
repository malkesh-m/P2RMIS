using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing a selection of application properties to be assigned to a workflow
    /// </summary>
    public interface IWorkflowMechanismModel
    {
        /// <summary>
        /// Workflow identifier
        /// </summary>
        int WorkflowId { get; set; }
        /// <summary>
        /// Workflow name
        /// </summary>
        string WorkflowName { get; set; }
        /// <summary>
        /// Label of priority for the applications to be assigned
        /// </summary>
        string Priority { get; set; }
        /// <summary>
        /// Whether the particular workflow template is editable (it is not considered a default workflow for it's assigned client
        /// </summary>
        bool IsEditable { get; set; }
        /// <summary>
        /// This workflow's steps
        /// </summary>
        ICollection<WorkflowStepModel> Steps { get; set; }
        /// <summary>
        /// The mechanisms for which a workflow is assigned
        /// </summary>
        ICollection<MechanismCycleModel> Mechanisms { get; set; }

    }
}
