using System;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for panel stage step/phase
    /// </summary>
    public interface IPanelStageStepModel
    {
        /// <summary>
        /// Panel stage step identifier
        /// </summary>
        int StageStepId { get; set; }
        /// <summary>
        /// Stage type Id
        /// </summary>
        int StageTypeId { get; set; }
        /// <summary>
        /// Panel stage step order
        /// </summary>
        int StepOrder { get; set; }
        /// <summary>
        /// Start date
        /// </summary>
        DateTime? StartDate { get; set; }
        /// <summary>
        /// End date
        /// </summary>
        DateTime? EndDate { get; set; }
        /// <summary>
        /// Re-open date
        /// </summary>
        DateTime? ReOpenDate { get; set; }
        /// <summary>
        /// Re-close date
        /// </summary>
        DateTime? ReCloseDate { get; set; }
        /// <summary>
        /// Count of critique assignments
        /// </summary>
        int CritiqueAssignmentCount { get; set; }
        /// <summary>
        /// Count of submitted critique assignments
        /// </summary>
        int CritiqueAssignmentSubmittedCount { get; set; }
        /// <summary>
        /// Stage step name
        /// </summary>
        string StepName { get; set; }
        /// <summary>
        /// Count of the submitted applications (CritiqueAssignmentCount == CritiqueAssignmentSubmittedCount)
        /// </summary>
        int ApplicationSubmittedCount { get; set; }
        /// <summary>
        /// Indicates if the phase is an MOD (moderated on-line discussion)
        /// </summary>
        bool IsModPhase { get; set; }
        /// <summary>
        /// Indicates if the MOD can be started.
        /// </summary>
        bool IsModReady { get; set; }
        /// <summary>
        /// Indicates if the MOD is active
        /// </summary>
        bool IsModActive { get; set; }
        /// <summary>
        /// Indicates if the MOD is closed
        /// </summary>
        bool IsModClosed { get; set; }
        /// <summary>
        /// The ApplicationStageStep entity identifier
        /// </summary>
        int? ApplicationStageStepId { get; set; }
        /// <summary>
        /// Gets or sets the panel application identifier.
        /// </summary>
        /// <value>
        /// The panel application identifier.
        /// </value>
        int PanelApplicationId { get; set; }
    }

}
