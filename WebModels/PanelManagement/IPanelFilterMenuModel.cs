using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for the panel selection menu
    /// </summary>
    public interface IPanelFilterMenuModel
    {
        /// <summary>
        /// List of Program/Years
        /// </summary>
        List<IProgramYearModel> ProgramYears { get; set; }
        /// <summary>
        /// List of Panels
        /// </summary>
        List<IPanelSignificationsModel> Panels { get; set; }
        /// <summary>
        /// List of cycles
        /// </summary>
        List<int> Cycles { get; set; }
        /// <summary>
        /// Selected Program/Year (optional)
        /// </summary>
        int? SelectedProgramYear { get; set; }
        /// <summary>
        /// Selected Panel (optional)
        /// </summary>
        int SelectedPanel { get; set; }
        /// <summary>
        /// Whether the current user has SelectProgramPanel permission
        /// </summary>
        bool HasSelectProgramPanelPermission { get; set; }
    }
}
