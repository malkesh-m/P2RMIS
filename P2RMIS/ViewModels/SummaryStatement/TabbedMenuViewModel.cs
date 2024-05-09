using System.Collections.Generic;

namespace Sra.P2rmis.Web.UI.Models
{    
    /// <summary>
    /// The view model for the tabbed list on the summary statement pages.
    /// </summary>
    public class TabbedMenuViewModel : SubTabbedMenuViewModel
    {       
        #region Properties
        /// <summary>
        /// the list of tabs in summary statement management.
        /// </summary>
        public List<TabItem> Tabs { get; set; }            
        #endregion
    }

    public class TabItem 
    {
        /// <summary>
        /// the order of the tabs
        /// </summary>
        public int TabOrder { get; set; }
        /// <summary>
        /// the name of the tab
        /// </summary>
        public string TabName { get; set; }
        /// <summary>
        /// the action link for the tab
        /// </summary>
        public string TabLink { get; set; }
    }
}