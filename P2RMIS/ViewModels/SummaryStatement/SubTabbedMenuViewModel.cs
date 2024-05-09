using System.Collections.Generic;

namespace Sra.P2rmis.Web.UI.Models
{    
    /// <summary>
    /// The view model for the tabbed list on the summary statement pages.
    /// </summary>
    public class SubTabbedMenuViewModel
    {
        #region Constants

        public const string SubTabController = "/SummaryStatement/";
        public const string SubTab1 = "Overall Progress";
        public const string SubTab2 = "Phases";
        public const string SubTab3 = "Deliverables";
        public const string SubTab1Route = "Progress";
        public const string SubTab2Route = "Phases";
        public const string SubTab3Route = "Completed";
        public const string SubTab1Link = SubTabController + SubTab1Route;
        public const string SubTab2Link = SubTabController + SubTab2Route;
        public const string SubTab3Link = SubTabController + SubTab3Route;

        #endregion

        #region Constructor

        public SubTabbedMenuViewModel() 
        {
            ///instantiate tab list
            List<TabItem> theTabList = new List<TabItem>();
            ///add items to list
            theTabList.Add(new TabItem() { TabOrder = 1, TabName = SubTab1, TabLink = SubTab1Link});
            theTabList.Add(new TabItem() { TabOrder = 2, TabName = SubTab2, TabLink = SubTab2Link });
            theTabList.Add(new TabItem() { TabOrder = 3, TabName = SubTab3, TabLink = SubTab3Link });

            ///set property to the tab list
            this.SubTabs = theTabList;
        }

        #endregion
        
        #region Properties
        /// <summary>
        /// the list of tabs in summary statement management.
        /// </summary>
        public List<TabItem> SubTabs { get; set; }
            
        #endregion
    }
}