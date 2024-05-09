using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for an application's Critique summary in panel management
    /// </summary>
    public interface ICritiqueSummaryModel
    {
        /// <summary>
        /// PanelApplication identifier
        /// </summary>
        int PanelApplicationId { get; set; }
        /// <summary>
        /// Application log number
        /// </summary>
        string LogNumber { get; set; }
        /// <summary>
        /// Number of reviewers on the panel
        /// </summary>
        int NumberOfReviewers { get; set; }
        /// <summary>
        /// Gets or sets the application critique.
        /// </summary>
        /// <value>
        /// The application critique.
        /// </value>
        IApplicationCritiqueModel ApplicationCritique { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is meeting phase started.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is meeting phase started; otherwise, <c>false</c>.
        /// </value>
        bool IsMeetingPhaseStarted { get; set; }
        /// <summary>
        /// Collection of data describing the phases
        /// </summary>
        ICollection<PanelStageStepModel> Phases { get; set; }
        /// <summary>
        /// Return the panel application's phases in step order.
        /// </summary>
        ICollection<PanelStageStepModel> OrderedPhases { get; }
    }
}
