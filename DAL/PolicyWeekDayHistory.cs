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
    
    public partial class PolicyWeekDayHistory
    {
        public int PolicyWeekDayHistoryId { get; set; }
        public Nullable<int> PolicyHistoryId { get; set; }
        public int PolicyId { get; set; }
        public int VersionId { get; set; }
        public int WeekDayId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool DeletedFlag { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        public virtual PolicyHistory PolicyHistory { get; set; }
    }
}
