using System;
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.Security
{
    public partial interface IPolicyWebModel
    {
        /// <summary>
        /// Policy Id
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Policy name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Policy type
        /// </summary>
        string Type { get; }
        /// <summary>
        /// Policy start date & time
        /// </summary>
        DateTime StartDateTime { get; }
        /// <summary>
        /// Policy end date & time
        /// </summary>
        DateTime? EndDateTime { get; }
        /// <summary>
        /// Policy access restriction start time
        /// </summary>
        string RestrictionStartTime { get; }
        /// <summary>
        /// Policy access restriction end time
        /// </summary>
        string RestrictionEndTime { get; }
        /// <summary>
        /// Days of the week that the policy is applied
        /// </summary>
        string DaysApplied { get; }
        /// <summary>
        /// Policy network address ranges
        /// </summary>
        string NetworkRanges { get; }
        /// <summary>
        /// Enabled or disabled status
        /// </summary>
        string Status { get; }
        /// <summary>
        /// User that created the policy
        /// </summary>
        string CreatedBy { get; }
        /// <summary>
        /// Date the policy was created
        /// </summary>
        DateTime? CreatedDateTime { get; }
    }
    public class PolicyWebModel : IPolicyWebModel
    {
        /// <summary>
        /// Policy Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Policy name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Policy type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Policy start date & time
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// Policy end date & time
        /// </summary>
        public DateTime? EndDateTime { get; set; }
        /// <summary>
        /// Policy access restriction start time
        /// </summary>
        public string RestrictionStartTime { get; set; }
        /// <summary>
        /// Policy access restriction end time
        /// </summary>
        public string RestrictionEndTime { get; set; }
        /// <summary>
        /// Days of the week that the policy is applied
        /// </summary>
        public string DaysApplied { get; set; }
        /// <summary>
        /// Policy network address ranges
        /// </summary>
        public string NetworkRanges { get; set; }
        /// <summary>
        /// Enabled or disabled status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// User that created the policy
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Date the policy was created
        /// </summary>
        public DateTime? CreatedDateTime { get; set; }
        public string Details { get; set; }
        public int PolicyTypeId { get; set; }
        public Nullable<int> RestrictionTypeId { get; set; }
        public string RestrictionType { get; set; }
        public Nullable<System.TimeSpan> RestrictionStartTimeSpan { get; set; }
        public Nullable<System.TimeSpan> RestrictionEndTimeSpan { get; set; }
        public virtual ICollection<PolicyNetworkRangeWebModel> PolicyNetworkRanges { get; set; }
        public virtual ICollection<PolicyWeekDayWebModel> PolicyWeekDays { get; set; }
    }
}
