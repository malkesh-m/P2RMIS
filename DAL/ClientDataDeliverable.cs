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
    
    public partial class ClientDataDeliverable
    {
        public ClientDataDeliverable()
        {
            this.ProgramCycleDeliverables = new HashSet<ProgramCycleDeliverable>();
        }
    
        public int ClientDataDeliverableId { get; set; }
        public int ClientId { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string ApiMethod { get; set; }
        public string FileFormat { get; set; }
        public bool QcRequiredFlag { get; set; }
        public int SortOrder { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual ICollection<ProgramCycleDeliverable> ProgramCycleDeliverables { get; set; }
    }
}