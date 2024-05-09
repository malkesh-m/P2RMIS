using System.Collections.Generic;

namespace Sra.P2rmis.Web.UI.Models
{
    public class MMTabsViewModel : MMTabbedMenuViewModel
    {
        #region Constants

        public const string TabController = "/MeetingManagement/";
        public const string Tab1 = "Hotel & Travel";
        public const string Tab2 = "Non-Reviewer Attendees";
        public const string Tab3 = "Upload";

        public const string Tab1Route = "Index";
        public const string Tab2Route = "NonReviewerAttendees";
        public const string Tab3Route = "Upload";

        public const string Tab1Link = TabController + Tab1Route;
        public const string Tab2Link = TabController + Tab2Route;
        public const string Tab3Link = TabController + Tab3Route;
        #endregion

        #region Constructor
        public MMTabsViewModel()
        {
            ///instantiate tab list
            List<MMTabItem> theTabList = new List<MMTabItem>();
            ///add items to list
            theTabList.Add(new MMTabItem() { TabOrder = 1, TabName = Tab1, TabLink = Tab1Link });
            theTabList.Add(new MMTabItem() { TabOrder = 2, TabName = Tab2, TabLink = Tab2Link });
            theTabList.Add(new MMTabItem() { TabOrder = 3, TabName = Tab3, TabLink = Tab3Link });

            ///set property to the tab list
            this.Tabs = theTabList;
        }
        #endregion
    }
}