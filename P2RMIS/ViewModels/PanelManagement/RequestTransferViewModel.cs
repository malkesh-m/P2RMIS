using System.Collections.Generic;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Web.ViewModels.PanelManagement
{
    /// <summary>
    /// View model for requesting an application be transferred from a panel
    /// </summary>
    public class RequestTransferViewModel
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor - initialize so model is usable.
        /// </summary>
        public RequestTransferViewModel()
        {
            this.AvailablePanels = new List<ITransferPanelModel>();
            this.AvailableReviewers = new List<IUserModel>();
            this.TransferReasons = new List<IReasonModel>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// The application log number 
        /// </summary>
        public string ApplicationLogNumber { get; set; }
        /// <summary>
        /// Current panel name
        /// </summary>
        public string CurrentPanel { get; set; }
        /// <summary>
        /// List of available panels where the application can be transferred
        /// </summary>
        public List<ITransferPanelModel> AvailablePanels { get; set; }
        /// <summary>
        /// List of reviewer that are associated with the current panel
        /// </summary>
        public List<IUserModel> AvailableReviewers { get; set; }
        /// <summary>
        /// List of transfer reasons
        /// </summary>
        public List<IReasonModel> TransferReasons { get; set; }
        /// <summary>
        /// User supplied comments
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Selected panel
        /// </summary>
        public int SelectedPanel { get; set; }
        /// <summary>
        /// Selected reviewer
        /// </summary>
        public int SelectedReviewer { get; set; }
        /// <summary>
        /// Selected reason
        /// </summary>
        public int SelectedReason { get; set; }
        #endregion
    }
}