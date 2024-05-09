namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing an application's current progress within a workflow
    /// </summary>
    public class WorkflowProgress : IWorkflowProgress
    {
        /// <summary>
        /// Unique identifier for an application workflow
        /// </summary>
        public int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// Name of an application's workflow step
        /// </summary>
        public string StepName { get; set; }
        /// <summary>
        /// Order an application's workflow step is scheduled to occur
        /// </summary>
        public int StepOrder { get; set; }
        /// <summary>
        /// Whether an application's workflow step was completed
        /// </summary>
        public bool IsCompleted { get; set; }
        /// <summary>
        /// Whether an application's workflow step is assigned to the current user
        /// </summary>
        public bool IsAssigned { get; set; }
    }
}
