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
    
    public partial class ClientApplicationInfoType
    {
        public ClientApplicationInfoType()
        {
            this.ApplicationInfoes = new HashSet<ApplicationInfo>();
        }
    
        public int ClientApplicationInfoTypeId { get; set; }
        public int ClientId { get; set; }
        public string InfoTypeDescription { get; set; }
    
        public virtual ICollection<ApplicationInfo> ApplicationInfoes { get; set; }
    }
}
