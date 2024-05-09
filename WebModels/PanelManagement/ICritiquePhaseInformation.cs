using System;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model for information about an individual critique phase
    /// </summary>
    public interface ICritiquePhaseInformation
    {
        /// <summary>
        /// Identifier for ApplicationWorkflow
        /// </summary>
        int ApplicationWorkflowId { get; set; }

        /// <summary>
        /// Identifier for ApplicationWorkflowStep
        /// </summary>
        int ApplicationWorkflowStepId { get; set; }

        /// <summary>
        /// Order in which Step takes place within a workflow
        /// </summary>
        int StepOrder { get; set; }

        /// <summary>
        /// Identifier for the type of step or phase
        /// </summary>
        int StepTypeId { get; set; }

        /// <summary>
        /// Numeric overall rating provided by reviewer
        /// </summary>
        decimal ScoreRating { get; set; }

        /// <summary>
        /// Adjectival overall rating provided by reviewer (if exists)
        /// </summary>
        string AdjectivalRating { get; set; }

        /// <summary>
        /// The type of scoring used for the application
        /// </summary>
        string ScoreType { get; set; }

        /// <summary>
        /// Whether the critique was submitted as final
        /// </summary>
        bool IsSubmitted { get; set; }

        /// <summary>
        /// Date critique was submitted as final
        /// </summary>
        DateTime? DateSubmitted { get; set; }

        /// <summary>
        /// Date phase begins
        /// </summary>
        DateTime PhaseStartDate { get; set; }

        /// <summary>
        /// Date phase ends
        /// </summary>
        DateTime PhaseEndDate { get; set; }

        /// <summary>
        /// Date phase re-opens for edits
        /// </summary>
        DateTime? ReOpenStartDate { get; set; }

        /// <summary>
        /// Date phase re-open period ends
        /// </summary>
        DateTime? ReOpenEndDate { get; set; }

        /// <summary>
        /// The largest step order with critiques from the current reviewer
        /// </summary>
        int MaxStepOrder { get; set; }        
        
        /// <summary>
        /// Whether content exists for the reviewer's critique
        /// </summary>
        bool ContentExists { get; set; }

        /// <summary>
        /// String representation of a formatted score for presentation
        /// </summary>
        string FormattedScore { get;  }
        /// <summary>
        /// The largest step order with critiques from the current reviewer that have been submitted
        /// </summary>
        int MaxSubmittedStepOrder { get; set; }
    }
}