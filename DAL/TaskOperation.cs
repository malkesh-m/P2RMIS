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
    
    public partial class TaskOperation
    {
        public int TaskOperationId { get; set; }
        public int SystemTaskId { get; set; }
        public int SystemOperationId { get; set; }
    
        public virtual SystemOperation SystemOperation { get; set; }
        public virtual SystemTask SystemTask { get; set; }
    }
}