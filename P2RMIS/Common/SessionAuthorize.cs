using System.Web;
using System.Web.Mvc;

namespace Sra.P2rmis.Web.Common
{
    /// <summary>
    /// Authorization attribute for polling methods.  Poolling methods are set to call the controller
    /// everyr X seconds.  This handles the case were the controller is restarted and all session data
    /// is lost.  Consequently the page that is activelly polling does not have the necessary security
    /// information or identity information.
    /// </summary>
    public class SessionAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return (httpContext.Session[SessionVariables.Verified] != null || httpContext.Session[SessionVariables.CredentialPermanent] != null);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/ErrorPage/NoAjaxAccess");
        }
    }
}