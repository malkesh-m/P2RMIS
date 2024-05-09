using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.CrossCuttingServices;
using System;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PolicyHistory object. 
    /// </summary>
    public partial class PolicyHistory : IStandardDateFields
    {
        /// <summary>
        /// Populates a PolicyHistory when the PolicyHistory has been reviewed.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        public void Populate()
        {
            //this.ReviewedFlag = true;
            //this.ReviewedDate = GlobalProperties.P2rmisDateTimeNow;
            //this.ReviewedBy = userId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="entityFieldName"></param>
        /// <param name="entityTableName"></param>
        /// <param name="identifer"></param>
        public void Populate(int policyHistoryId, int policyId, int versionId, int clientId, int typeId, string name, string details, DateTime startDateTime, DateTime? endDateTime, int? restrictionTypeId, TimeSpan? restrictionStartTime, TimeSpan? restrictionEndTime, bool active, int policyStatusId)
        {
            this.PolicyHistoryId = policyHistoryId;
            this.PolicyId = policyId;
            this.VersionId = versionId;
            this.ClientId = clientId;
            this.TypeId = typeId;
            this.Name = name;
            this.Details = details;
            this.StartDateTime = startDateTime;
            this.EndDateTime = endDateTime;
            this.RestrictionTypeId = restrictionTypeId;
            this.RestrictionStartTime = restrictionStartTime;
            this.RestrictionEndTime = restrictionEndTime;
            this.Active = active;
            this.PolicyStatusId = policyStatusId;
        }

    }
}
