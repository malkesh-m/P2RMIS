using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;

namespace Sra.P2rmis.Web.Common
{
    public class BasicAuthenticationAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        /// <summary>
        /// The basic authentication scheme
        /// </summary>
        public const string Basic = "basic";

        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var isAuthorized = false;
            if (actionContext.Request.Headers.Authorization != null)
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string username = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                string password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);

                if (username == ConfigManager.BasicAuthenticationUserName && password == ConfigManager.BasicAuthenticationPassword)
                {
                    isAuthorized = true;
                    base.OnActionExecuting(actionContext);
                }
            }
            if (!isAuthorized)
                actionContext.Response = GetUnauthroizedResponseWithBasicHeader();
        }

        /// <summary>
        /// Gets the unauthroized response with basic header.
        /// </summary>
        /// <returns></returns>
        private HttpResponseMessage GetUnauthroizedResponseWithBasicHeader()
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(Basic));
            return response;
        }
    }
}