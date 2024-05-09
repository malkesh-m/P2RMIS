using System.Collections.Generic;
using System.Web.Security;

namespace Sra.P2rmis.Web.Common.Interfaces
{
    /// <summary>
    /// P2RMIS identity methods
    /// </summary>
    public interface ICustomIdentity
    {
        /// <summary>
        /// NEEDS COMMENT
        /// </summary>
        FormsAuthenticationTicket Ticket { get; }
        /// <summary>
        /// NEEDS COMMENT
        /// </summary>
        int UserID { get; }
        /// <summary>
        /// NEEDS COMMENT
        /// </summary>
        string FullUserName { get; }
        /// <summary>
        /// NEEDS COMMENT
        /// </summary>
        string DBUserName { get; }
    }

}
