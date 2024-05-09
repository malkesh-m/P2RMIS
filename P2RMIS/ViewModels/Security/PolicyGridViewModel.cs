using System;
using System.Collections.Generic;

namespace Sra.P2rmis.Web.UI.Models
{
    public class PolicyGridViewModel
    {
        /// <summary>
        /// Policy Id
        /// </summary>
        public int Id { get; set;}
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
        public string CreatedDateTime { get; set; }
    }
}