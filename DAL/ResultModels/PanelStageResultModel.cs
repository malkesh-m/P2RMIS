
using System.Collections.Generic;

namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Interface for Panel stage setup information
    /// </summary>
    /// <seealso cref="Sra.P2rmis.Dal.ResultModels.IPanelStageResultModel" />
    public class PanelStageResultModel : IPanelStageResultModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether assignments have been released to reviewers.
        /// </summary>
        /// <value>
        ///   true if assignments have been released; otherwise, false.
        /// </value>
        public bool AssignmentsReleased { get; set; }

        /// <summary>
        /// Gets or sets the panel stage and steps.
        /// </summary>
        /// <value>
        /// The panel stage and steps.
        /// </value>
        public IList<PanelStage> PanelStageAndSteps { get; set; }
    }
}
