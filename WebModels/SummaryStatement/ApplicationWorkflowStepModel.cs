using System;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing the active steps in a workflow
    /// </summary>
    public class ApplicationWorkflowStepModel : IApplicationWorkflowStepModel
    {
            /// <summary>
            /// Unique identifier for a workflow's step instance
            /// </summary>
            public int ApplicationWorkflowStepId { get; set; }
            /// <summary>
            /// Identifier for a workflow's application instance
            /// </summary>
            public int ApplicationWorkflowId { get; set; }
            /// <summary>
            /// The system type of the step
            /// </summary>
            public int StepTypeId { get; set; }
            /// <summary>
            /// The name of the step
            /// </summary>
            public string StepName { get; set; }
            /// <summary>
            /// Whether the workflow step is active
            /// </summary>
            public bool Active { get; set; }
            /// <summary>
            /// The chronological order of the step
            /// </summary>
            public int StepOrder { get; set; }
            /// <summary>
            /// Whether the step has been completed
            /// </summary>
            public bool Resolution { get; set; }
            /// <summary>
            /// The date the step was completed
            /// </summary>
            public Nullable<DateTime> ResolutionDate { get; set; }
            /// <summary>
            /// Whether the step is the current step
            /// </summary>
            public bool IsCurrentStep { get; set; }


    }
}
