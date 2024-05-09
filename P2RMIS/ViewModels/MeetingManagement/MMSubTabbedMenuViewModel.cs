using System.Collections.Generic;

namespace Sra.P2rmis.Web.UI.Models
{    
    /// <summary>
    /// The view model for the tabbed list on the meeting management pages.
    /// </summary>
    public class MMSubTabbedMenuViewModel
    {
        #region Constants

        public const string SubTabController = "/MeetingManagement/";
        public const string SubTab1 = "Hotel";
        public const string SubTab2 = "Travel";
        public const string SubTab3 = "Comments";
        public const string SubTab1Route = "EditHotel";
        public const string SubTab2Route = "EditTravel";
        public const string SubTab3Route = "EditComments";
        public const string SubTab1Link = SubTabController + SubTab1Route;
        public const string SubTab2Link = SubTabController + SubTab2Route;
        public const string SubTab3Link = SubTabController + SubTab3Route;

        #endregion

        #region Constructor

        public MMSubTabbedMenuViewModel() 
        {
            ///instantiate tab list
            List<MMTabItem> theTabList = new List<MMTabItem>();
            ///add items to list
            theTabList.Add(new MMTabItem() { TabOrder = 1, TabName = SubTab1, TabLink = SubTab1Link});
            theTabList.Add(new MMTabItem() { TabOrder = 2, TabName = SubTab2, TabLink = SubTab2Link });
            theTabList.Add(new MMTabItem() { TabOrder = 3, TabName = SubTab3, TabLink = SubTab3Link });

            ///set property to the tab list
            this.SubTabs = theTabList;
        }

        #endregion
        
        #region Properties
        /// <summary>
        /// the list of tabs in summary statement management.
        /// </summary>
        public List<MMTabItem> SubTabs { get; set; }
            
        #endregion
    }
}