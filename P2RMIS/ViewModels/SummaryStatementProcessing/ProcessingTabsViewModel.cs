using System.Collections.Generic;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the processing tabs in summary statement processing
    /// </summary
    public abstract class ProcessingTabsViewModel : TabbedMenuViewModel
    {
        #region Constants

        public const string TabController = "/SummaryStatementProcessing/";
        public const string Tab1 = "Available Draft Summary Statements";
        public const string Tab2 = "My Draft Summary Statements";
        public const string Tab1Route = "Index";
        public const string Tab2Route = "Assignments";
        public const string Tab1Link = TabController + Tab1Route;
        public const string Tab2Link = TabController + Tab2Route;

        #endregion

        #region Constructor

        public ProcessingTabsViewModel() 
        {
            ///instantiate tab list
            List<TabItem> theTabList = new List<TabItem>();
            ///add items to list
            theTabList.Add(new TabItem() { TabOrder = 1, TabName = Tab1, TabLink = Tab1Link});
            theTabList.Add(new TabItem() { TabOrder = 2, TabName = Tab2, TabLink = Tab2Link });

            ///set property to the tab list
            this.Tabs = theTabList;
        }

        #endregion
    }
}