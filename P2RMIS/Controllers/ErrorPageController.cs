using System.Web.Mvc;
using System.Web.Security;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Models;

namespace Sra.P2rmis.Web.Controllers
{
    public class ErrorPageController : Controller
    {
        public class ViewNames
        {
            public const string LogOn = "LogOn";
            public const string NoAjaxAuthentication = "NoAjaxAuthentication";
            public const string NoAjaxAccess = "NoAjaxAccess";
        }
        //
        // GET: /ErrorPage/
        [HandleError()]
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult NoAccess()
        {
            CustomIdentity ident = User.Identity as CustomIdentity;
            if (ident == null || !ident.IsAuthenticated)
                ViewBag.isLoggedIn = false;
            return View();
        }
        public ActionResult ReturnToLogin()
        {
            Cache.ClearCacheForSession(Session.SessionID);
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult FileNotFound()
        {
            return View();
        }
        /// <summary>
        /// Partial view for No Ajax Authentication.
        /// </summary>
        /// <returns>Partial view for No Ajax Authentication</returns>
        public ActionResult NoAjaxAuthentication()
        {
            return View(ViewNames.NoAjaxAuthentication);
        }
        /// <summary>
        /// Partial view for No Ajax Access.
        /// </summary>
        /// <returns>Partial view for No Ajax Authentication</returns>
        public ActionResult NoAjaxAccess()
        {
            return View(ViewNames.NoAjaxAccess);
        }
    }
}
