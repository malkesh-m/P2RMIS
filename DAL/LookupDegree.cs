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
    
    public partial class LookupDegree
    {
        public LookupDegree()
        {
            this.UserInfoes = new HashSet<UserInfo>();
        }
    
        public int DegreeID { get; set; }
        public string DegreeName { get; set; }
    
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
    }
}
