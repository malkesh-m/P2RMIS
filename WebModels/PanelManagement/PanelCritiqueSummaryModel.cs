using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// web model for data displayed on panel management critique page
    /// </summary>
    public class PanelCritiqueSummaryModel : IPanelCritiqueSummaryModel
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public PanelCritiqueSummaryModel()
        {
            this.PanelCritiques = new List<ICritiqueSummaryModel>();
            this.PhaseHeaders = new SortedDictionary<int, IPanelStageStepModel>();
        }
        #endregion
        /// <summary>
        /// Collection of the critique summary information for the applications on the panel
        /// </summary>
        public ICollection<ICritiqueSummaryModel> PanelCritiques { get; set; }
        /// <summary>
        /// Collection of general phase information
        /// </summary>
        public IDictionary<int, IPanelStageStepModel> PhaseHeaders { get; set; }
        /// <summary>
        /// Indicates if the web model has any critiques.
        /// </summary>
        public bool HasCritiques
        {
            get
            {
                return (this.PanelCritiques.Count > 0);
            }
        }
        /// <summary>
        /// The meeting session identifier containing the session panel.
        /// </summary>
        public int? MeetingSessionId { get; set; }
    }
}
