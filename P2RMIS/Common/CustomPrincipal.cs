using System;
using System.Security.Principal;
using System.Web.Security;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.Models
{
    public class CustomPrincipal : IPrincipal
    {

        private CustomIdentity _identity;

        public CustomPrincipal(CustomIdentity identity)
        {
            _identity = identity;
        }

        public System.Security.Principal.IIdentity Identity
        {
            get { return _identity; }
        }

        /// <summary>
        /// Determines whether [is in role] [the specified role].
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>true if user is in the specified role; otherwise false</returns>
        public bool IsInRole(string role)
        {
            return _identity.SystemRole == role;
        }
    }

}
