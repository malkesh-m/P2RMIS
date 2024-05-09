using Sra.P2rmis.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal
{
    public partial class SessionUserAssignment : IStandardDateFields
    {
        /// <summary>
        /// the primary contact phone number for a user.
        /// </summary>
        /// <returns>phone number as string</returns>
        public string PrimaryPhoneNumber()
        {
            return this.User.UserInfoEntity().GetPrimaryPhoneNumber();
        }
    }
}
