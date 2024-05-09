using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Controllers.UserProfileManagement;
using Sra.P2rmis.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace Sra.P2rmis.Web.Common
{

    #region Sandbox
    /// <summary>
    /// Extend the AuthorizeAttribute class to handle unauthorized access to specific resources on the pages.
    /// If the user does not have access to the resource they are redirected to an appropriate error page.
    /// </summary>
    /// <remarks>
    /// There is an implied precedence order when this Attribute is used.  If a method is decorated with multiple
    /// permissions with this Attribute then if authorization was due to a permission that appears before the 
    /// LimitedProfileManagement permission than the limitation check is not performed.
    /// </remarks>
    public class RestrictedManageUserAccountsAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Determines if a permission with a higher precedence (implied) triggered the authorization.
        /// </summary>
        /// <param name="httpContext">HTTP-specific information about an individual HTTP request</param>
        /// <returns>True if the user is authorized to access the resource by permission; false otherwise</returns>
        protected bool IfAuthorizationWasDueToHigherPermission(HttpContextBase httpContext)
        {
            //
            // We have authorize using the base rules. When using
            // this attribute, permissions have an implied precedence.  The precedence is based
            // on the order they are concatenated within the attribute parameter.
            // We need to determine if a permission before the limited access permission triggered
            // the authentication.  So take the permissions until we get to the limited access 
            // permission and just revalidate with those.
            //
            IEnumerable<string> aa = OperationsSplit.TakeWhile(x => x != Permissions.UserProfileManagement.RestrictedManageUserAccounts);
            return aa.Any(x => SecurityHelpers.CheckValidPermissionFromSession(httpContext.Session, x));
        }
        /// <summary>
        /// Override of the AuthorizeAttribute OnAuthorization method implementing an SRO's manage access to  
        /// users profile information.  The user must be on an SRO panel for successful authorization.
        /// </summary>
        /// <param name="filterContext">The filter context, which encapsulates information for using AuthorizeAttribute</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            IPrincipal principal = filterContext.HttpContext.User;
            //
            // First let's just do the default authentication.  Then if the user's
            // authenticated there then we can get to the specifics of this secondary
            // authentication.
            //
            base.OnAuthorization(filterContext);
            if ((filterContext.Result == null) & (!IfAuthorizationWasDueToHigherPermission(filterContext.HttpContext)))
            {
                //
                // Well the user has the permissions.  Now get the information we need to determine
                // if the user has the permission to access the specific resource.
                //
                UserProfileManagementController profileManagementController = filterContext.Controller as UserProfileManagementController;
                IPanelManagementService service = profileManagementController.GetPanelManagementService;
                //
                // And the individual data values
                //
                string parameter = filterContext.Controller.ValueProvider.GetValue(Routing.UserProfileManagement.ViewUserParameters.UserInfoId).AttemptedValue;
                int? parameterValue = TryConvertToInt(parameter);
                int userId = profileManagementController.GetUserId();

                if (
                    (parameterValue.HasValue) &&
                    (service.IsUserAssignedToSroPanel(userId, parameterValue.Value))
                   )
                {
                    //
                    // We have successfully converted the parameter and the SRO has 
                    // asked for a resource that they are allowed to access then
                    // just pass them on.
                    //
                }
                else
                {
                    filterContext.Result = CreateNoAccessResult();
                }                
            }
        }
    }

    #endregion
}