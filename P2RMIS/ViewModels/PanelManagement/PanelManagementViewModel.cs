using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.Web.Common;
using System;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The base view model for Panel Management.
    /// </summary>
    public class PanelManagementViewModel : PanelTabsViewModel, IPanelFilterMenuModel
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public PanelManagementViewModel()
            : base()
        {
            this.ProgramYears = new List<IProgramYearModel>();
            this.Panels = new List<IPanelSignificationsModel>();
            this.Cycles = new List<int>();
            this.PanelAccessErrors = new List<string>();
            this.CanAccessPanel = true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// List of Program/Years
        /// </summary>
        public List<IProgramYearModel> ProgramYears { get; set; }
        /// <summary>
        /// List of Panels
        /// </summary>
        public List<IPanelSignificationsModel> Panels { get; set; }
        /// <summary>
        /// List of cycles
        /// </summary>
        public List<int> Cycles { get; set; }
        /// <summary>
        /// Selected Program/Year (optional)
        /// </summary>
        public int? SelectedProgramYear { get; set; }
        /// <summary>
        /// Selected Panel (optional)
        /// </summary>
        [Required(ErrorMessage = "Panel Selection is required.")]
        public int SelectedPanel { get; set; }
        /// <summary>
        /// Whether the current user has SelectProgramPanel permission
        /// </summary>
        public bool HasSelectProgramPanelPermission { get; set; }
        /// <summary>
        /// Whether the current user has ManageReviewerAssignmentExpertise permission.
        /// </summary>
        public bool HasManageReviewerAssignmentExpertisePermission { get; set; }
        /// <summary>
        /// Is a panel selected?
        /// </summary>
        public bool HasSelectedPanel
        {
            get
            {
                return (this.SelectedPanel > 0);
            }
        }
        /// <summary>
        /// Gets the no results message.
        /// </summary>
        /// <value>
        /// The no results message.
        /// </value>
        public string NoResultsMessage
        {
            get
            {
                return HasSelectedPanel ? Invariables.Labels.PanelManagement.Messages.NoResultsFound :
                    Invariables.Labels.PanelManagement.Messages.SelectPanel;
            }
        }

        /// <summary>
        /// List of messages that display when the user cannot access the panel
        /// </summary>
        public List<string> PanelAccessErrors { get; set; }

        /// <summary>
        /// Whether the user can access the current panel
        /// </summary>
        public bool CanAccessPanel { get; set; }
        #endregion
    }
}