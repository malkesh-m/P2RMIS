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
    
    public partial class ClientConfiguration
    {
        public int ClientConfigurationId { get; set; }
        public int ClientId { get; set; }
        public int SystemConfigurationId { get; set; }
        public bool ConfigurationValue { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual SystemConfiguration SystemConfiguration { get; set; }
    }
}