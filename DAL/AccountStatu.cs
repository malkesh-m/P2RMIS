//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sra.P2rmis.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccountStatu
    {
        public AccountStatu()
        {
            this.UserAccountStatus = new HashSet<UserAccountStatu>();
            this.AccountStatusReasons = new HashSet<AccountStatusReason>();
            this.UserAccountStatusChangeLogs = new HashSet<UserAccountStatusChangeLog>();
            this.UserAccountStatusChangeLogs1 = new HashSet<UserAccountStatusChangeLog>();
        }
    
        public int AccountStatusId { get; set; }
        public string AccountStatusName { get; set; }
    
        public virtual ICollection<UserAccountStatu> UserAccountStatus { get; set; }
        public virtual ICollection<AccountStatusReason> AccountStatusReasons { get; set; }
        public virtual ICollection<UserAccountStatusChangeLog> UserAccountStatusChangeLogs { get; set; }
        public virtual ICollection<UserAccountStatusChangeLog> UserAccountStatusChangeLogs1 { get; set; }
    }
}