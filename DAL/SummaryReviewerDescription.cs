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
    
    public partial class SummaryReviewerDescription
    {
        public int SummaryReviewerDescriptionId { get; set; }
        public Nullable<int> ProgramMechanismId { get; set; }
        public Nullable<int> ClientParticipantTypeId { get; set; }
        public Nullable<int> ClientRoleId { get; set; }
        public Nullable<int> AssignmentOrder { get; set; }
        public int CustomOrder { get; set; }
        public string DisplayName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        public virtual ClientParticipantType ClientParticipantType { get; set; }
        public virtual ClientRole ClientRole { get; set; }
        public virtual ProgramMechanism ProgramMechanism { get; set; }
    }
}