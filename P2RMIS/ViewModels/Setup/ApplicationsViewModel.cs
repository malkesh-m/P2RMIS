using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.Web.UI.Models;

namespace Sra.P2rmis.Web.ViewModels.Setup
{
    public class ApplicationsViewModel : TabbedMenuViewModel
    {
        public ApplicationsViewModel()
        {
            ///instantiate tab list
            List<TabItem> theTabList = new List<TabItem>();
            ///add items to list
            theTabList.Add(new TabItem() { TabOrder = 1, TabName = "Applications", TabLink = "/Setup/Applications" });
            theTabList.Add(new TabItem() { TabOrder = 2, TabName = "Referral Assignments", TabLink = "/Setup/ReferralMapping" });

            ///set property to the tab list
            this.Tabs = theTabList;
        }
    }
}