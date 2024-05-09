using System.Collections.Generic;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for viewing application/abstracts information
    /// </summary>
    public class ViewApplicationViewModel : PanelManagementViewModel
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ViewApplicationViewModel()
            : base()
        {
            this.Applications = new List<IApplicationInformationModel>();
        }
        #endregion

        #region Properties        
        /// <summary>
        /// the list of application
        /// </summary>
        public List<IApplicationInformationModel> Applications { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can add application.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can add application; otherwise, <c>false</c>.
        /// </value>
        public bool CanAddApplication { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is sro.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sro; otherwise, <c>false</c>.
        /// </value>
        public bool IsSro { get; set; }
        #endregion
    }
}