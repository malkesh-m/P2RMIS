using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.Web.ViewModels.SummaryStatement;

namespace Sra.P2rmis.Web.ViewModels.SummaryStatementReview
{
    public class RequestReviewViewModel : SsTabMenuViewModel, ISSFilterMenuModel
    {
        #region constructor
        /// <summary>
        /// View model for displaying the client's applications
        /// </summary>
        public RequestReviewViewModel()
            : base()
        {
            ///Initializing the programs list
            List<IClientProgramModel> programs = new List<IClientProgramModel>();
            this.Programs = programs;
            List<IProgramYearModel> fys = new List<IProgramYearModel>();
            this.FiscalYears = fys;
            List<int> cycles = new List<int>();
            this.Cycles = cycles;
            List<ISessionPanelModel> panels = new List<ISessionPanelModel>();
            this.Panels = panels;
            List<IAwardModel> awards = new List<IAwardModel>();
            this.Awards = awards;
            //
            // Hide user search criteria
            //
            HideUserSearchCriteria = true;
            //
            // Hide cycle search criteria
            //
            HideCycleCriteria = false;

            //
            // do not hide panel criteria search criteria
            //
            HidePanelCriteria = false;

            //
            // do not hide award search criteria
            //
            HideAwardCriteria = false;

        }
        #endregion

        #region properties
        /// <summary>
        /// List of Programs
        /// </summary>
        public List<IClientProgramModel> Programs { get; set; }
        /// <summary>
        /// List of Fiscal Years
        /// </summary>
        public List<IProgramYearModel> FiscalYears { get; set; }
        /// <summary>
        /// List of Panels
        /// </summary>
        public List<ISessionPanelModel> Panels { get; set; }
        /// <summary>
        /// List of cycles
        /// </summary>
        public List<int> Cycles { get; set; }
        /// <summary>
        /// List of Awards
        /// </summary>
        public List<IAwardModel> Awards { get; set; }
        /// <summary>
        /// The selected program (required)
        /// </summary>
        [Required(ErrorMessage = "Please select program(s)")]
        public int SelectedProgram { get; set; }
        /// <summary>
        /// Selected fiscal year (required)
        /// </summary>
        [Required(ErrorMessage = "Please select fiscal year(s)")]
        public int SelectedFy { get; set; }
        /// <summary>
        /// Selected Panel (optional)
        /// </summary>
        public int? SelectedPanel { get; set; }
        /// <summary>
        /// Selected Cycle (optional)
        /// </summary>
        public int? SelectedCycle { get; set; }
        /// <summary>
        /// Selected Award (optional)
        /// </summary>
        public int? SelectedAward { get; set; }
        /// <summary>
        /// the selected reviewer name
        /// </summary>
        public string SelectedReviewerName { get; set; }
        /// <summary>
        /// the selected reviewer id
        /// </summary>
        public int? SelectedReviewerId { get; set; }
        /// <summary>
        /// whether the user search criteria should be hidden
        /// </summary>
        public bool HideUserSearchCriteria { get; set; }
        /// <summary>
        /// whether the panel criteria  search criteria should be hidden
        /// </summary>
        public bool HidePanelCriteria { get; set; }
        /// <summary>
        /// whether the award criteria  search criteria should be hidden
        /// </summary>
        public bool HideAwardCriteria { get; set; }
        /// <summary>
        /// whether the cycle search criteria should be hidden
        /// </summary>
        public bool HideCycleCriteria { get; set; }
        /// <summary>
        /// Filter menu title
        /// </summary>
        public string FilterMenuTitle
        {
            get { return "Select Filters"; }
        }
        /// <summary>
        /// Indicates if the Panel selection is required for this user
        /// </summary>
        public bool IsPanelRequired { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is web based.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is web based; otherwise, <c>false</c>.
        /// </value>
        public bool IsWebBased { get; set; }
        #endregion
    }
}