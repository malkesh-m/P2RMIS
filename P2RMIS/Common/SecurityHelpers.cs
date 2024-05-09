using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sra.P2rmis.Web.Common
{
    /// <summary>
    /// Helper methods for checking permissions from httpContext.
    /// </summary>
    public static class SecurityHelpers
    {
        /// <summary>
        /// Checks the valid permission from session.
        /// </summary>
        /// <param name="httpSession">The session context of the calling thread.</param>
        /// <param name="operationNames">The operation names. Can be comma delimited to specify multiple</param>
        /// <returns>true if permission exists in session; otherwise false</returns>
        public static bool CheckValidPermissionFromSession(HttpSessionStateBase httpSession, string operationNames)
        {
            List<string> operationsToCheckList = operationNames?.Split(',').ToList() ?? new List<string>();
            List<string> userOperationsList = (httpSession?[SessionVariables.AuthorizedActionList] == null)
                ? new List<string>()
                : (List<string>) httpSession[SessionVariables.AuthorizedActionList];
            return userOperationsList.Any(x => operationsToCheckList.Contains(x));
        }
        /// <summary>
        /// Check if passsword age is set to expired in session
        /// </summary>
        /// <param name="httpSession">The session context of the calling thread.</param>
        /// <returns></returns>
        public static bool CheckPasswordAgeExpiredFromSession(HttpSessionStateBase httpSession)
        {
            return (httpSession?[SessionVariables.PasswordAgeExpired] == null)?false : httpSession[SessionVariables.PasswordAgeExpired].Equals(1);

        }
    }
}