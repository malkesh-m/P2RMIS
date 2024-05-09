namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Represents a single workflow step
    /// </summary>
    public class WorkflowStepModel : IWorkflowStepModel
    {
        /// <summary>
        /// Workflow step identifier
        /// </summary>
        public int WorkflowStepId { get; set; }
        /// <summary>
        /// Workflow identifier that this is related to
        /// </summary>
        public int WorkflowId { get; set; }
        /// <summary>
        /// Whether or not the Step is a client review step
        /// </summary>
        public bool IsClientReview { get; set; }
        /// <summary>
        /// Step name
        /// </summary>
        public string StepName { get; set; }
        /// <summary>
        /// Step order
        /// </summary>
        public int StepOrder { get; set; }
        /// <summary>
        /// Enabled indicator
        /// </summary>
        public bool ActiveDefault { get; set; }
    }
}
