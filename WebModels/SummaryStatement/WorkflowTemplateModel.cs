using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Represents information for a workflow template
    /// </summary>
    public class WorkflowTemplateModel : IWorkflowTemplateModel
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public WorkflowTemplateModel ()
        {
            //
            // Ensure that there is always a container for the workflow steps
            //
            Steps = new List<WorkflowStepModel>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Workflow identifier
        /// </summary>
        public int WorkflowId { get; set; }
        /// <summary>
        /// Client identifier
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// Workflow name
        /// </summary>
        public string WorkflowName { get; set; }
        /// <summary>
        /// Workflow description
        /// </summary>
        public string WorkflowDescription { get; set; }
        /// <summary>
        /// This workflow's steps
        /// </summary>
        public ICollection<WorkflowStepModel> Steps { get; set; }
        #endregion
    }
}
