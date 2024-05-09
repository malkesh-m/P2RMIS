namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Represents a single workflow step
    /// </summary>
    public interface IWorkflowStepModel
    {
        /// <summary>
        /// Workflow step identifier
        /// </summary>
        int WorkflowStepId { get; set; }
        /// <summary>
        /// Workflow identifier that this is related to
        /// </summary>
        int WorkflowId { get; set; }
        /// <summary>
        /// Whether or not the Step is a client review step
        /// </summary>
        bool IsClientReview { get; set; }
        /// <summary>
        /// Step name
        /// </summary>
        string StepName { get; set; }
        /// <summary>
        /// Step order
        /// </summary>
        int StepOrder { get; set; }
        /// <summary>
        /// Enabled indicator
        /// </summary>
        bool ActiveDefault { get; set; }
    }
}
