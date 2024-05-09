using System.Collections.Generic;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    public interface ISSFilterMenuModel
    {
        #region Properties

        /// <summary>
        /// List of Programs
        /// </summary>
        List<IClientProgramModel> Programs { get; set; }
        /// <summary>
        /// List of Fiscal Years
        /// </summary>
        List<IProgramYearModel> FiscalYears { get; set; }
        /// <summary>
        /// List of Panels
        /// </summary>
        List<ISessionPanelModel> Panels { get; set; }
        /// <summary>
        /// List of cycles
        /// </summary>
        List<int> Cycles { get; set; }
        /// <summary>
        /// List of Awards
        /// </summary>
        List<IAwardModel> Awards { get; set; }
        /// <summary>
        /// The selected program (required)
        /// </summary>
        int SelectedProgram { get; set; }
        /// <summary>
        /// Selected fiscal year (required)
        /// </summary>
        int SelectedFy { get; set; }
        /// <summary>
        /// Selected Panel (optional)
        /// </summary>
        int? SelectedPanel { get; set; }
        /// <summary>
        /// Selected Cycle (optional)
        /// </summary>
        int? SelectedCycle { get; set; }
        /// <summary>
        /// Selected Award (optional)
        /// </summary>
        int? SelectedAward { get; set; }
        /// <summary>
        /// the selected reviewer name
        /// </summary>
        string SelectedReviewerName { get; set; }
        /// <summary>
        /// the selected reviewer id
        /// </summary>
        int? SelectedReviewerId { get; set; }
        /// <summary>
        /// whether the user search criteria should be hidden
        /// </summary>
        bool HideUserSearchCriteria { get; set; }
        /// <summary>
        /// whether the cycle criteria should be hidden
        /// </summary>
        bool HideCycleCriteria { get; set; }
        /// <summary>
        /// whether the panel search criteria should be hidden
        /// </summary>
        bool HidePanelCriteria { get; set; }
        /// <summary>
        /// whether the award search criteria should be hidden
        /// </summary>
        bool HideAwardCriteria { get; set; }
        /// <summary>
        /// Filter menu title
        /// </summary>
        string FilterMenuTitle { get; }
        /// <summary>
        /// Indicates if the Panel selection is required for this user
        /// </summary>
        bool IsPanelRequired { get; set; }
        #endregion
    }
}
