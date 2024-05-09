namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing an application's current progress within a workflow
    /// </summary>
    public interface IWorkflowProgress
    {
        /// <summary>
        /// Unique identifier for an application workflow
        /// </summary>
        int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// Name of an application's workflow step
        /// </summary>
        string StepName { get; set; }
        /// <summary>
        /// Order an application's workflow step is scheduled to occur
        /// </summary>
        int StepOrder { get; set; }
        /// <summary>
        /// Whether an application's workflow step was completed
        /// </summary>
        bool IsCompleted { get; set; }
        /// <summary>
        /// Whether an application's workflow step is assigned to the current user
        /// </summary>
        bool IsAssigned { get; set; }

    }
}
