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
    
    public partial class CommunicationMethod
    {
        public CommunicationMethod()
        {
            this.UserCommunicationLogs = new HashSet<UserCommunicationLog>();
        }
    
        public int CommunicationMethodId { get; set; }
        public string MethodName { get; set; }
    
        public virtual ICollection<UserCommunicationLog> UserCommunicationLogs { get; set; }
    }
}
