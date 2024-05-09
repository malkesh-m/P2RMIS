using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    public partial class UserPassword : IStandardDateFields
    {
        public void Populate(int userId, string password, DateTime? passwordDate, string passwordSalt)
        {
            this.UserID = userId;
            this.Password = password;
            this.PasswordDate = passwordDate;
            this.PasswordSalt = passwordSalt;

            Helper.UpdateModifiedFields(this, userId);
            Helper.UpdateCreatedFields(this, userId);
        }
    }
}
