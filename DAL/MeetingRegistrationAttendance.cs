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
    
    public partial class MeetingRegistrationAttendance
    {
        public int MeetingRegistrationAttendanceId { get; set; }
        public int MeetingRegistrationId { get; set; }
        public Nullable<System.DateTime> AttendanceStartDate { get; set; }
        public Nullable<System.DateTime> AttendanceEndDate { get; set; }
        public string MealRequestComments { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        public virtual MeetingRegistration MeetingRegistration { get; set; }
    }
}