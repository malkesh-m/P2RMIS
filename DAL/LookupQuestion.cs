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
    
    public partial class LookupQuestion
    {
        public LookupQuestion()
        {
            this.Users = new HashSet<User>();
            this.Users1 = new HashSet<User>();
            this.Users2 = new HashSet<User>();
        }
    
        public int QID { get; set; }
        public string QuestionText { get; set; }
    
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<User> Users1 { get; set; }
        public virtual ICollection<User> Users2 { get; set; }
    }
}