using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Sra.P2rmis.Web.Models;

namespace Sra.P2rmis.Web.Common
{
    /// <summary>
    /// Extend the AuthorizeAttribute class to handle unauthorized access to pages and redirect to appropriate error page
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        private string _operations;   
        private string[] _operationsSplit = new string[0];
        public string Operations {
            get { return _operations ?? String.Empty; }   
            set   
            {   
                _operations = value;   
                _operationsSplit = value.Split(',');   
            }

        }
        /// <summary>
        /// Provides access for derived classes to the list of individual permissions.
        /// </summary>
        protected string[] OperationsSplit { get { return _operationsSplit; } }
        protected void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        /// <summary>
        /// If the user is not authorized, then send to NoAccess page
        /// </summary>
        /// <param name="filterContext">event handler</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                                    new RouteValueDictionary {
                                            { "controller", "ErrorPage" },    
                                            { "action", "NoAccess" }                                       
                                        });
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)    
        {    
            if (httpContext == null)    
            {    
                throw new ArgumentNullException("httpContext");    
            }    
            IPrincipal user = httpContext.User;
            
            CustomIdentity ident = user.Identity as CustomIdentity;
            //if the user has not logged in or their session has expired fail the authorization
            if (ident == null || !ident.IsAuthenticated)   
            {   
                return false;   
            }
            //if the user does not have permission fail the authorization
            if (_operationsSplit.Length > 0 && !SecurityHelpers.CheckValidPermissionFromSession(httpContext.Session, _operations))
            {
                return false;
            }
            return base.AuthorizeCore(httpContext);   
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                SetCachePolicy(filterContext);
            }
            else if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // auth failed, redirect to login page
                filterContext.Result = CreateNoAuthenticationResult(filterContext);
            }
            else
            {
                filterContext.Result = CreateNoAccessResult(filterContext);
            }
        }

        protected void SetCachePolicy(AuthorizationContext filterContext)
        {
            // ** IMPORTANT **
            // Since we're performing authorization at the action level, the authorization code runs
            // after the output caching module. In the worst case this could allow an authorized user
            // to cause the page to be cached, then an unauthorized user would later be served the
            // cached page. We work around this by telling proxies not to cache the sensitive page,
            // then we hook our custom authorization code into the caching mechanism so that we have
            // the final say on whether a page should be served from the cache.
            HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
            cachePolicy.SetProxyMaxAge(new TimeSpan(0));
            cachePolicy.SetNoStore();
            cachePolicy.AppendCacheExtension("no-cache");
            cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
        }
        /// <summary>
        /// Constructs a RedirectToRouteResult object routing the user to the "No Access" page.
        /// </summary>
        /// <returns>RedirectToRouteResult to the "No Access" page</returns>
        protected RedirectToRouteResult CreateNoAccessResult()
        {
            return new RedirectToRouteResult(new RouteValueDictionary {
                                        { "controller", "ErrorPage" },
                                        { "action", "NoAccess" }
                                            });
        }
        /// <summary>
        /// Constructs a RedirectToRouteResult object routing the user to the "No Access" page.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <returns>RedirectToRouteResult to the "No Access" page</returns>
        protected RedirectToRouteResult CreateNoAccessResult(AuthorizationContext filterContext)
        {
            var actionName = filterContext.HttpContext.Request.IsAjaxRequest() ? "NoAjaxAccess" : "NoAccess";
            return new RedirectToRouteResult(new RouteValueDictionary {
                                        { "controller", "ErrorPage" },
                                        { "action", actionName } });
        }
        /// <summary>
        /// Creates the no authentication result.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <returns>ActionResult to the "No Authentication" page</returns>
        protected ActionResult CreateNoAuthenticationResult(AuthorizationContext filterContext)
        {
            return filterContext.HttpContext.Request.IsAjaxRequest() ? 
                    (ActionResult)(new RedirectToRouteResult(new RouteValueDictionary {
                                        { "controller", "ErrorPage" },
                                        { "action", "NoAjaxAuthentication" } })) : new HttpUnauthorizedResult();
        }
        /// <summary>
        /// Converts a string into an integer.
        /// </summary>
        /// <param name="value">Integer value as a string</param>
        /// <returns>If successful value as an integer; null otherwise</returns>
        protected int? TryConvertToInt(string value)
        {
            int result;
            return (int.TryParse(value, out result)) ? (int?)result : null;
        }

        /// <summary>
        /// Gets the operations list from the appropriate session variable.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns>List of string operations; empty is session or variable has not been set</returns>
        protected List<string> GetOperationsListFromSession(HttpContextBase httpContext)
        {
            return (httpContext.Session == null || httpContext.Session[SessionVariables.AuthorizedActionList] == null) ? new List<string>() : (List<string>)httpContext.Session[SessionVariables.AuthorizedActionList];
        }

    }
}
