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
    
    public partial class CommentType
    {
        public CommentType()
        {
            this.UserApplicationComments = new HashSet<UserApplicationComment>();
        }
    
        public int CommentTypeID { get; set; }
        public string CommentTypeName { get; set; }
    
        public virtual ICollection<UserApplicationComment> UserApplicationComments { get; set; }
    }
}