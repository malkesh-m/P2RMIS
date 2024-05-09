using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sra.P2rmis.Web.ViewModels.PanelManagement
{
    /// <summary>
    /// View model for editing the application overview
    /// </summary>
    public class EditOverviewViewModel
    {
        #region Properties
        /// <summary>
        /// The panel overview text
        /// </summary>
        public string PanelOverview { get; set; }
        /// <summary>
        /// The panel application identifier
        /// </summary>
        public int PanelApplicationId { get; set; }
        #endregion
    }
}