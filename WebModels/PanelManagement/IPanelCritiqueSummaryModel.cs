using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// web model for data displayed on panel management critique page
    /// </summary>
    public interface IPanelCritiqueSummaryModel
    {
        /// <summary>
        /// Collection of the critique summary information for the applications on the panel
        /// </summary>
        ICollection<ICritiqueSummaryModel> PanelCritiques { get; set; }
        /// <summary>
        /// Collection of general phase information
        /// </summary>
        IDictionary<int, IPanelStageStepModel> PhaseHeaders { get; set; }
        /// <summary>
        /// The meeting session identifier containing the session panel.
        /// </summary>
        int? MeetingSessionId { get; set; }
    }
}
