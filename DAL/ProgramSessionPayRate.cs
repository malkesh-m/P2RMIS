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
    
    public partial class ProgramSessionPayRate
    {
        public int ProgramSessionPayRateId { get; set; }
        public int ProgramYearId { get; set; }
        public Nullable<int> MeetingSessionId { get; set; }
        public Nullable<int> MeetingTypeId { get; set; }
        public int ClientParticipantTypeId { get; set; }
        public int ParticipantMethodId { get; set; }
        public bool RestrictedAssignedFlag { get; set; }
        public Nullable<int> EmploymentCategoryId { get; set; }
        public string HonorariumAccepted { get; set; }
        public string ConsultantFeeText { get; set; }
        public decimal ConsultantFee { get; set; }
        public System.DateTime PeriodStartDate { get; set; }
        public System.DateTime PeriodEndDate { get; set; }
        public string ManagerList { get; set; }
        public string DescriptionOfWork { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        public virtual ClientParticipantType ClientParticipantType { get; set; }
        public virtual EmploymentCategory EmploymentCategory { get; set; }
        public virtual MeetingSession MeetingSession { get; set; }
        public virtual MeetingType MeetingType { get; set; }
        public virtual ProgramYear ProgramYear { get; set; }
    }
}
