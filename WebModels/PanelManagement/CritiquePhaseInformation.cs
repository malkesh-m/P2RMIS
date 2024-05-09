using System;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model for information about an individual critique phase
    /// </summary>
    public class CritiquePhaseInformation : ICritiquePhaseInformation
    {
        /// <summary>
        /// Identifier for ApplicationWorkflow
        /// </summary>
        public int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// Identifier for ApplicationWorkflowStep
        /// </summary>
        public int ApplicationWorkflowStepId { get; set; }
        /// <summary>
        /// Order in which Step takes place within a workflow
        /// </summary>
        public int StepOrder { get; set; }
        /// <summary>
        /// Identifier for the type of step or phase
        /// </summary>
        public int StepTypeId { get; set; }
        /// <summary>
        /// Numeric overall rating provided by reviewer
        /// </summary>
        public decimal ScoreRating { get; set; }
        /// <summary>
        /// Adjectival overall rating provided by reviewer (if exists)
        /// </summary>
        public string AdjectivalRating { get; set; }
        /// <summary>
        /// The type of scoring used for the application
        /// </summary>
        public string ScoreType { get; set; }
        /// <summary>
        /// Whether the critique was submitted as final
        /// </summary>
        public bool IsSubmitted { get; set; }
        /// <summary>
        /// Date critique was submitted as final
        /// </summary>
        public DateTime? DateSubmitted { get; set; }
        /// <summary>
        /// Date phase begins
        /// </summary>
        public DateTime PhaseStartDate { get; set; }
        /// <summary>
        /// Date phase ends
        /// </summary>
        public DateTime PhaseEndDate { get; set; }
        /// <summary>
        /// Date phase re-opens for edits
        /// </summary>
        public DateTime? ReOpenStartDate { get; set; }
        /// <summary>
        /// Date phase re-open period ends
        /// </summary>
        public DateTime? ReOpenEndDate { get; set; }
        /// <summary>
        /// The largest step order with critiques from the current reviewer
        /// </summary>
        public int MaxStepOrder { get; set; }
        /// <summary>
        /// Whether content exists for the reviewer's critique
        /// </summary>
        public bool ContentExists { get; set; }
        /// <summary>
        /// delegate used to format the score
        /// </summary>
        public static ScoreFormatter ScoreFormatter { get; set; }
        /// <summary>
        /// String representation of a formatted score for presentation
        /// </summary>
        public string FormattedScore
        {
            get { return PhaseStartDate <= GlobalProperties.P2rmisDateTimeNow ? ScoreFormatter(ScoreRating, ScoreType, AdjectivalRating, IsSubmitted) : null; }
        }
        /// <summary>
        /// The largest step order with critiques from the current reviewer that have been submitted
        /// </summary>
        public int MaxSubmittedStepOrder { get; set; }
    }
}
