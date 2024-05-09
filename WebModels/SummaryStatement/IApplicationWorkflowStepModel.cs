using System;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing the active steps in a workflow
    /// </summary>
    public interface IApplicationWorkflowStepModel
    {
        /// <summary>
        /// Unique identifier for a workflow's step instance
        /// </summary>
        int ApplicationWorkflowStepId { get; set; }
        /// <summary>
        /// Identifier for a workflow's application instance
        /// </summary>
        int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// The system type of the step
        /// </summary>
        int StepTypeId { get; set; }
        /// <summary>
        /// The name of the step
        /// </summary>
        string StepName { get; set; }
        /// <summary>
        /// Whether the workflow step is active
        /// </summary>
        bool Active { get; set; }
        /// <summary>
        /// The chronological order of the step
        /// </summary>
        int StepOrder { get; set; }
        /// <summary>
        /// Whether the step has been completed
        /// </summary>
        bool Resolution { get; set; }
        /// <summary>
        /// The date the step was completed
        /// </summary>
        Nullable<DateTime> ResolutionDate { get; set; }
        /// <summary>
        /// Whether the step is the current step
        /// </summary>
        bool IsCurrentStep { get; set; }
    }
}
