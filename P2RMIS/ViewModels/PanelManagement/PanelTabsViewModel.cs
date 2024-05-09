using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices;
namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the tabs in panel management
    /// </summary
    public abstract class PanelTabsViewModel : PrivilegedTabsViewModel
    {
        #region Constants

        public const string TabController = "/PanelManagement/";
        public const string Tab1 = "Applications/Abstracts";
        public const string Tab2 = "Reviewers";
        public const string Tab3 = "Communication";
        public const string Tab4 = "Expertise/Assignments";
        public const string Tab5 = "Critiques";
        public const string Tab6 = "Order of Discussion";
        public const string Tab7 = "Reviewer Evaluation";
        public const string Tab1Route = "Index";
        public const string Tab2Route = "Reviewers";
        public const string Tab3Route = "Communication";
        public const string Tab4Route = "Expertise";
        public const string Tab5Route = "ManageCritiques";
        public const string Tab6Route = "OrderOfReview";
        public const string Tab7Route = "ReviewerEvaluation";
        public const string Tab1Link = TabController + Tab1Route;
        public const string Tab2Link = TabController + Tab2Route;
        public const string Tab3Link = TabController + Tab3Route;
        public const string Tab4Link = TabController + Tab4Route;
        public const string Tab5Link = TabController + Tab5Route;
        public const string Tab6Link = TabController + Tab6Route;
        public const string Tab7Link = TabController + Tab7Route;

        #endregion

        #region Constructor

        public PanelTabsViewModel()
        {
            ///instantiate tab list
            List<PrivilegedTabItem> theTabList = new List<PrivilegedTabItem>();
            ///add items to list
            theTabList.Add(new PrivilegedTabItem() { TabOrder = 1, TabName = Tab1, TabLink = Tab1Link, RequiredPermission = Permissions.PanelManagement.ManagePanelApplication });
            theTabList.Add(new PrivilegedTabItem() { TabOrder = 2, TabName = Tab2, TabLink = Tab2Link, RequiredPermission = Permissions.PanelManagement.ViewPanelReviewers });
            theTabList.Add(new PrivilegedTabItem() { TabOrder = 3, TabName = Tab3, TabLink = Tab3Link, RequiredPermission = Permissions.PanelManagement.SendPanelCommunication });
            theTabList.Add(new PrivilegedTabItem() { TabOrder = 4, TabName = Tab4, TabLink = Tab4Link, RequiredPermission = Permissions.PanelManagement.ViewReviewerAssignmentExpertise });
            theTabList.Add(new PrivilegedTabItem() { TabOrder = 5, TabName = Tab5, TabLink = Tab5Link, RequiredPermission = Permissions.PanelManagement.ViewPanelCritiques });
            theTabList.Add(new PrivilegedTabItem() { TabOrder = 6, TabName = Tab6, TabLink = Tab6Link, RequiredPermission = Permissions.PanelManagement.ManageDiscussionOrder });
            theTabList.Add(new PrivilegedTabItem() { TabOrder = 7, TabName = Tab7, TabLink = Tab7Link, RequiredPermission = Permissions.PanelManagement.EvaluateReviewers });

            ///set property to the tab list
            this.Tabs = theTabList;
        }

        #endregion
    }
}