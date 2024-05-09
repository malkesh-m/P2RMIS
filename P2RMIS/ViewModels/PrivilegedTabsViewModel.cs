using System.Collections.Generic;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the privileged tabs
    /// </summary
    public abstract class PrivilegedTabsViewModel : TabbedMenuViewModel
    {
        #region Properties
        /// <summary>
        /// the list of tabs in user profile management.
        /// </summary>
        public new List<PrivilegedTabItem> Tabs { get; set; }
        /// <summary>
        /// Gets or sets the tab group label.
        /// </summary>
        /// <value>
        /// The tab group label.
        /// </value>
        public string TabGroupLabel { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor that defines tabs
        /// </summary>
        public PrivilegedTabsViewModel()
        {
            ///set property to the tab list
            this.Tabs = new List<PrivilegedTabItem>();
        }
        #endregion
    }
}