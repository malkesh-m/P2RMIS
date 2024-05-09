using System;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.AccessAccountManagement;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.Web.Common;

namespace Sra.P2rmis.Web.Controllers.UserProfileManagement
{
    /// <summary>
    /// Base controller for P2RMIS User Profile Management controller.  
    /// Basically a container for User Profile Management controller common functionality.
    /// </summary>
    public class UserProfileManagementBaseController : BaseController
    {
        #region Constants
        /// <summary>
        /// The View names in user profile management
        /// </summary>
        public class ViewNames
        {
            public const string ParticipationHistory = "ParticipationHistory";
            public const string SearchUser = "_SearchUser";
            public const string Profile = "Profile";
            public const string SearchResults = "SearchResults";
            public const string ViewUser = "ViewUser";
            public const string AddResume = "_AddResume";
            public const string ManageBlock = "_ManageBlock";
            public const string VendorManagement = "_VendorManagement";
            public const string VendorManagementModal = "_VendorManagementModal";
        }
        /// <summary>
        /// the types of search contexts a user can have
        /// </summary>
        public class SearchContexts
        {
            public const string CreateContext = "create";
            public const string UpdateContext = "update";
        }
        /// <summary>
        /// The session key for profile management context
        /// </summary>
        public const string ProfileManagementSessionContext = "ProfileManagementContext";

        #endregion
        #region Properties
        /// <summary>
        /// Service providing access to the lookup services for dropdown contents
        /// </summary>
        protected ILookupService theLookupService { get; set; }
        /// <summary>
        /// Service providing access to the file resources
        /// </summary>
        protected IFileService theFileService { get; set; }
        /// <summary>
        /// Service providing access to the user profile management service
        /// </summary>
        protected IUserProfileManagementService theProfileService { get; set; }
        /// <summary>
        /// Service provideing mail services.
        /// </summary>
        protected IMailService theMailService { get; set;  }
        /// <summary>
        /// Service providing access to the user access account management service
        /// </summary>
        protected IAccessAccountManagementService theAccessAccountService { get; set; }
        /// <summary>
        /// Service providing access to PanelManagement functions.
        /// </summary>
        protected IPanelManagementService thePanelManagementService { get; set; }
        #endregion
        /// <summary>
        /// Get tab menu
        /// </summary>
        /// <param name="viewModel">The UserProfileTabsViewModel view model</param>
        /// <remarks>Visibility should have "protected" but because RhinoMocks was used as the testing framework the unit test would not have worked otherwise.</remarks>
        public virtual void SetTabs(UserProfileTabsViewModel viewModel)
        {
            viewModel.Tabs = viewModel.Tabs.Where(o => HasPermission(o.RequiredPermission)).ToList();
        }
        /// <summary>
        /// Determines if the user can manage the password for the retrieved profile
        /// </summary>
        /// <param name="userInfoId">UserInfo entity identifier of the profile</param>
        /// <param name="userId">User entity identifier of the person modifying the profile.</param>
        /// <returns>True if the user can manage the password; false otherwise</returns>
        protected bool CanManagePassword(int userProfileId, int userId)
        {
            return (userProfileId == userId);
        }
        /// <summary>
        /// Does the user have active, permanent credentials
        /// </summary>
        /// <param name="userProfileId">The user identifier</param>
        /// <returns>true if the credentials are active and permanent, false otherwise</returns>
        //public bool IsPermanentCredentials(int userProfileId)
        //{
        //    return theProfileService.IsPermanentCredentials(userProfileId);
        //}
        /// <summary>
        /// Does the user have sufficient permissions to display the Role drop down.
        /// </summary>
        /// <returns>true if the Role drop down is displayed, false otherwise</returns>
        public bool AreRolesDisplayed()
        {
            return HasPermission(Permissions.UserProfileManagement.ManageUserAccounts);
        }

        /// <summary>
        /// Determines whether user has my workspace permission.
        /// </summary>
        /// <returns>true if user has my workspace permission</returns>
        protected bool HasMyWorkspacePermission()
        {
            return HasPermission(Permissions.MyWorkspace.AccessMyWorkspace);
        }

        //////////////////////
        /// <summary>
        /// Gets the back button URL.
        /// </summary>
        /// <returns></returns>
        protected override string GetBackButtonUrl()
        {
            string controllerName = ControllerContext.RouteData.Values["Controller"].ToString();

            string loginPage = Url.Action(Routing.Account.LogOn, Routing.P2rmisControllers.Account, null, Request.Url.Scheme);
            string referrerPage = (Request.UrlReferrer != null) ? Request.UrlReferrer.AbsoluteUri : string.Empty;
            string currentPage = (Request.Url != null) ? Request.Url.AbsoluteUri : string.Empty;

            bool sameController = referrerPage.Contains("/" + controllerName + "/");

            if (referrerPage.Equals(loginPage, StringComparison.OrdinalIgnoreCase))
            {
                Session[SessionVariables.BackButton] = string.Empty;
            }
            else if (!referrerPage.Equals(currentPage, StringComparison.OrdinalIgnoreCase))
            {
                Session[SessionVariables.BackButton] = referrerPage;
            }
            return Session[SessionVariables.BackButton]?.ToString();
        }

    }
}