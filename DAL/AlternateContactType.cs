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
    
    public partial class AlternateContactType
    {
        public AlternateContactType()
        {
            this.UserAlternateContacts = new HashSet<UserAlternateContact>();
        }
    
        public int AlternateContactTypeId { get; set; }
        public string AlternateContactType1 { get; set; }
        public int SortOrder { get; set; }
    
        public virtual ICollection<UserAlternateContact> UserAlternateContacts { get; set; }
    }
}