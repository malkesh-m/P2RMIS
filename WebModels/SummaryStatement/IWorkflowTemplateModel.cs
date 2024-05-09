using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Represents information for a workflow template
    /// </summary>
    public interface IWorkflowTemplateModel
    {
        /// <summary>
        /// Workflow identifier
        /// </summary>
        int WorkflowId { get; set; }
        /// <summary>
        /// Client identifier
        /// </summary>
        int ClientId { get; set; }
        /// <summary>
        /// Workflow name
        /// </summary>
        string WorkflowName { get; set; }
        /// <summary>
        /// Workflow description
        /// </summary>
        string WorkflowDescription { get; set; }
        /// <summary>
        /// This workflow's steps
        /// </summary>
        ICollection<WorkflowStepModel> Steps { get; set; }
    }
}
