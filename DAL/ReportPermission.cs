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
    
    public partial class ReportPermission
    {
        public int ReportPermId { get; set; }
        public int ReportId { get; set; }
        public string OperationName { get; set; }
    
        public virtual Report Report1 { get; set; }
    }
}
