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
    
    public partial class Gender
    {
        public Gender()
        {
            this.UserInfoes = new HashSet<UserInfo>();
            this.Nominees = new HashSet<Nominee>();
        }
    
        public int GenderId { get; set; }
        public string Gender1 { get; set; }
    
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
        public virtual ICollection<Nominee> Nominees { get; set; }
    }
}