using System.Collections.Generic;

namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Interface for Panel stage setup information
    /// </summary>
    public interface IPanelStageResultModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether assignments have been released to reviewers.
        /// </summary>
        /// <value>
        ///   true if assignments have been released; otherwise, false.
        /// </value>
        bool AssignmentsReleased { get; set; }
        /// <summary>
        /// Gets or sets the panel stage and steps.
        /// </summary>
        /// <value>
        /// The panel stage and steps.
        /// </value>
        IList<PanelStage> PanelStageAndSteps { get; set; }
    }
}