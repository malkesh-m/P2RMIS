using System.Collections.Generic;

namespace Sra.P2rmis.Web.ViewModels.Shared
{
    public delegate bool PermissionHandler(string permission);
    /// <summary>
    /// The view model for the tabbed list on the summary statement pages.
    /// </summary>
    public abstract class TabMenuViewModel
    {    
        #region Constructor
        public TabMenuViewModel()
        {
        }
        #endregion
        
        #region Properties   
        /// <summary>
        /// The tab names
        /// </summary>
        public string[] TabNames { get; set; }
        /// <summary>
        /// The tab links
        /// </summary>
        public string[] TabLinks { get; set; }
        /// <summary>
        /// Gets or sets the tab required permissions.
        /// </summary>
        /// <value>
        /// The tab required permissions.
        /// </value>
        public string[] TabRequiredPermissions { get; set; }
        /// <summary>
        /// Gets or sets the permission handler.
        /// </summary>
        /// <value>
        /// The permission handler.
        /// </value>
        public static PermissionHandler HasPermission { get; set; }
        /// <summary>
        /// the list of tabs in summary statement management.
        /// </summary>
        public List<TabItem> Tabs { get; set; }
        #endregion
        /// <summary>
        /// Sets the tabs.
        /// </summary>
        public void SetTabs()
        {
            // Instantiate tab list
            List<TabItem> theTabList = new List<TabItem>();
            // Add items to list
            for (var i = 0; i < TabNames.Length; i++)
            {
                if (HasPermission(TabRequiredPermissions[i]))
                {
                    theTabList.Add(new TabItem() { TabOrder = i, TabName = TabNames[i], TabLink = TabLinks[i]});
                }
            }
            // Set property to the tabe list
            this.Tabs = theTabList;
        }
    }
    /// <summary>
    /// Tab item
    /// </summary>
    public class TabItem
    {
        /// <summary>
        /// Order of the tabs
        /// </summary>
        public int TabOrder { get; set; }
        /// <summary>
        /// Name of the tab
        /// </summary>
        public string TabName { get; set; }
        /// <summary>
        /// Action link for the tab
        /// </summary>
        public string TabLink { get; set; }
    }
}