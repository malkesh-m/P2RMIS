using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web.Security;
using System.Web.SessionState;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Common.Interfaces;

namespace Sra.P2rmis.Web.Models
{
    /// <summary>
    /// NEEDS COMMENT
    /// </summary>
    public class CustomIdentity : IIdentity, ICustomIdentity
    {
        /// <summary>
        /// NEEDS COMMENT
        /// </summary>
        private FormsAuthenticationTicket _ticket;
        /// <summary>
        /// NEEDS COMMENT
        /// </summary>
        public CustomIdentity(FormsAuthenticationTicket ticket)
        {
            _ticket = ticket;
        }
        /// <summary>
        /// NEEDS COMMENT
        /// </summary>
        public string AuthenticationType
        {
            get { return "Custom"; }
        }
        /// <summary>
        /// NEEDS COMMENT
        /// </summary>
        public bool IsAuthenticated
        {
            get { return true; }
        }
        /// <summary>
        /// NEEDS COMMENT
        /// </summary>
        public string Name
        {
            get { return _ticket.Name; }
        }
        /// <summary>
        /// NEEDS COMMENT
        /// </summary>
        public FormsAuthenticationTicket Ticket
        {
            get { return _ticket; }
        }
        /// <summary>
        /// NEEDS COMMENT
        /// </summary>
        public int UserID
        {
            get
            {
                string[] userDataPieces = _ticket.UserData.Split("|".ToCharArray());
                return Convert.ToInt32(userDataPieces[0]);
            }
        }
        /// <summary>
        /// Full User's Name = Prefix + FirstName + LastName + Suffix
        /// </summary>
        public string FullUserName
        {
            get
            {
                string[] userDataPieces = _ticket.UserData.Split("|".ToCharArray());
                return userDataPieces[1];
            }

        }
        /// <summary>
        /// NEEDS COMMENT
        /// </summary>
        public string DBUserName
        {
            get
            {
                string[] userDataPieces = _ticket.UserData.Split("|".ToCharArray());
                return (userDataPieces[1]);
            }
        }
        /// <summary>
        /// Retrieves the users last login date
        /// </summary>
        public string LastLoginDate
        {
            get 
            {
                string[] userDataPieces = _ticket.UserData.Split("|".ToCharArray());
                return Convert.ToDateTime(userDataPieces[2]).ToString("MM/dd/yyyy hh:mm tt");
            }
        }

        /// <summary>
        /// The user's system role
        /// </summary>
        public string SystemRole
        {
            get
            {
                string[] userDataPieces = _ticket.UserData.Split("|".ToCharArray());
                return userDataPieces[3];
            }
        }
    }
}