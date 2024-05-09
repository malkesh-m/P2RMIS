using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the tabs in user profile management
    /// </summary
    public abstract class UserProfileTabsViewModel : PrivilegedTabsViewModel
    {
        #region Constants

        public const string TabController = "/UserProfileManagement/";
        public const string TabController2 = "/MyWorkspace/";
        public const string Tab1 = "Profile";
        public const string Tab2 = "Manage Password";
        public const string Tab3 = "Program Participation";
        public const string Tab4 = "Current Assignments";
        public const string Tab5 = "Hotel and Travel";
        public const string Tab6 = "Manage Application Scoring";
        public const string Tab1Route = "ViewUser";
        public const string Tab2Route = "PasswordManagement";
        public const string Tab3Route = "ViewParticipationHistory";
        public const string Tab4Route = "Index";
        public const string Tab5Route = "HotelAndTravel";
        public const string Tab6Route = "ManageApplicationScoring";
        public const string UserInfoParam = "?userInfoId=";
        public const string Tab1Link = TabController + Tab1Route;
        public const string Tab2Link = TabController + Tab2Route;
        public const string Tab3Link = TabController + Tab3Route;
        public const string Tab4Link = TabController2 + Tab4Route;
        public const string Tab5Link = TabController2 + Tab5Route;
        public const string Tab6Link = TabController2 + Tab6Route;
        public const string MyWorkspaceTitle = "My Workspace";
        public const string AccountManagementTitle = "My Account";
        public const string AccountManagementTitleOther = "Account Management";


        #endregion

        #region Constructor
        /// <summary>
        /// Constructor that defines tabs
        /// </summary>
        public UserProfileTabsViewModel()
        {
            ///instantiate tab list
            List<PrivilegedTabItem> theTabList = new List<PrivilegedTabItem>();
            ///add items to list
            /// 
            theTabList.Add(new PrivilegedTabItem() { TabOrder = 1, TabName = Tab1, TabLink = Tab1Link });
            theTabList.Add(new PrivilegedTabItem() { TabOrder = 2, TabName = Tab2, TabLink = Tab2Link });
            theTabList.Add(new PrivilegedTabItem() { TabOrder = 3, TabName = Tab3, TabLink = Tab3Link, RequiredPermission = Permissions.UserProfileManagement.ManageUserAccounts });
            theTabList.Add(new PrivilegedTabItem() { TabOrder = 4, TabName = Tab4, TabLink = Tab4Link, RequiredPermission = Permissions.MyWorkspace.AccessMyWorkspace });
            theTabList.Add(new PrivilegedTabItem() { TabOrder = 5, TabName = Tab5, TabLink = Tab5Link, RequiredPermission = Permissions.MyWorkspace.AccessMyWorkspace });

            ///set property to the tab list
            this.Tabs = theTabList;
        }

        
        /// <summary>
        /// Appends userInfoId parameter to all available tabs
        /// </summary>
        /// <param name="userInfoId">Identifier for a user profile</param>
        internal void AppendProfileTabs(int userInfoId)
        {
            foreach (var tab in this.Tabs)
            {
                tab.TabLink = AppendUserInfoId(tab.TabLink, userInfoId);
            }
            
        }

        internal void AppendCpritChairTabs()
        {
            this.Tabs.Add(new PrivilegedTabItem() { TabOrder = 6, TabName = Tab6, TabLink = Tab6Link, RequiredPermission = Permissions.MyWorkspace.AccessMyWorkspace });
        }
        /// <summary>
        /// Gets or sets the menu title.
        /// </summary>
        /// <value>
        /// The title that displays above the profile/my workspace tabs.
        /// </value>
        public string MenuTitle { get; set; }

        /// <summary>
        /// Sets the profile/workspace tab context.
        /// </summary>
        /// <param name="currentUserId">The current user identifier.</param>
        /// <param name="userId">The user identifier for the page context (if known or applicable).</param>
        /// <param name="userInfoId">The user information identifier for the page context (if known or applicable).</param>
        /// <param name="permCredentials">if set to <c>true</c> user has permanent credentials.</param>
        /// <param name="profileVerified">if set to <c>true</c> user's profile has been verified.</param>
        /// <param name="hasWorkspacePermission">if set to <c>true</c> user has permission to view my workspace.</param>
        /// <remarks>This method must be called for each request that includes profile/workspace sub tabs</remarks>
        public void SetTabContext(int currentUserId, int? userId, int? userInfoId, bool permCredentials, bool profileVerified, bool hasWorkspacePermission)
        {
            //Filter tab visibility
            FilterTabs(currentUserId, userId, userInfoId, permCredentials, profileVerified, hasWorkspacePermission);
            //Append userInfoId if currentUser is not the same as the user in profile context
            if (currentUserId != userId && userInfoId != null)
            {
                AppendProfileTabs(userInfoId ?? 0);
            }
        }
        /// <summary>
        /// Overload of the SetTabContext for the ViewUser() method.  In addition to the normal
        /// SetTabContext() processing,  will remove the Program Participation tab if the user is a client.
        /// </summary>
        /// <param name="currentUserId">The current user identifier.</param>
        /// <param name="userId">The user identifier for the page context (if known or applicable).</param>
        /// <param name="userInfoId">The user information identifier for the page context (if known or applicable).</param>
        /// <param name="permCredentials">if set to <c>true</c> user has permanent credentials.</param>
        /// <param name="profileVerified">if set to <c>true</c> user's profile has been verified.</param>
        /// <param name="hasWorkspacePermission">if set to <c>true</c> user has permission to view my workspace.</param>
        /// <param name="isClient">Indicates if the user is a client user</param>
        public void SetTabContext(int currentUserId, int? userId, int? userInfoId, bool permCredentials, bool profileVerified, bool hasWorkspacePermission, bool isClient)
        {
            SetTabContext(currentUserId, userId, userInfoId, permCredentials, profileVerified, hasWorkspacePermission);
            //
            // now we deal with the client issue
            //
            FilterTabs(isClient);
        }
        /// <summary>
        /// Sets the menu title.
        /// </summary>
        /// <param name="hasWorkspacePermission">if set to <c>true</c> user has my workspace permission.</param>
        internal void SetMenuTitle(int currentUserId, int? userId, bool hasWorkspacePermission)
        {
            if (currentUserId == userId)
            {
                this.MenuTitle = hasWorkspacePermission ? MyWorkspaceTitle : AccountManagementTitle;
            }
            else {
                this.MenuTitle = hasWorkspacePermission ? MyWorkspaceTitle : AccountManagementTitleOther;
            }
        }
        // Set menu title for account releated pages
        internal void SetMenuTitle(int currentUserId, int? userId)
        {
            this.MenuTitle = (currentUserId == userId) ? AccountManagementTitle : AccountManagementTitleOther;            
        }
        /// <summary>
        /// Sets the profile/workspace tab context.
        /// </summary>
        /// <param name="currentUserId">The current user identifier.</param>
        /// <param name="userId">The user identifier for the page context (if known or applicable).</param>
        /// <param name="userInfoId">The user information identifier for the page context (if known or applicable).</param>
        /// <param name="permCredentials">if set to <c>true</c> user has permanent credentials.</param>
        /// <param name="profileVerified">if set to <c>true</c> user's profile has been verified.</param>
        /// <param name="hasWorkspacePermission">if set to <c>true</c> user has permission to view my workspace.</param>
        /// <remarks>This method must be called for each request that includes profile/workspace sub tabs</remarks>
        internal void FilterTabs(int currentUserId, int? userId, int? userInfoId, bool permCredentials, bool profileVerified,
            bool hasWorkspacePermission)
        {
            //This method runs through a series of checks to set it's tab context correctly
            //First we check whether user is in a create context (userId and userInfoId don't exist)
            //or their profile is not verified in which case they only have tab 1
            if (((userId ?? 0) == 0 && (userInfoId ?? 0) == 0) || !profileVerified)
            {
                this.Tabs = this.Tabs.Where(x => x.TabName == Tab1).ToList();
            }
            //Users without permanent credentials can only set permanent credentials
            if (!permCredentials)
            {
                this.Tabs = this.Tabs.Where(x => x.TabName == Tab2).ToList();
            }
            //User who is not the current user cannot see the manage password tab
            if (currentUserId != userId)
            {
                this.Tabs = this.Tabs.Where(x => x.TabName != Tab2).ToList();
            }
            //User who does not have the view workspace permission cannot see the current assignments tab
            if (!hasWorkspacePermission)
            {
                this.Tabs = this.Tabs.Where(x => x.TabName != Tab4 && x.TabName != Tab5).ToList();
            }
        }
        /// <summary>
        /// Filter the tabs if the user is a client to remove the Program Participation tab.
        /// </summary>
        /// <param name="isClient"></param>
        internal void FilterTabs(bool isClient)
        {
            if (isClient)
            {
                this.Tabs = this.Tabs.Where(x => x.TabName != Tab3).ToList();
            }
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Appends UserInfoId parameter onto a string
        /// </summary>
        /// <param name="tabLink">Link to append</param>
        /// <param name="userInfoId">Id to append</param>
        private string AppendUserInfoId(string tabLink, int userInfoId)
        {
            StringBuilder sb = new StringBuilder();
            return sb.AppendFormat("{0}{1}{2}", tabLink, UserInfoParam, userInfoId).ToString();
        }
        #endregion
    }
}