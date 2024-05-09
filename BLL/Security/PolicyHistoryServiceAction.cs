using Sra.P2rmis.Dal;
using System;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Security
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete PolicyHistory.
    /// </summary>
    public class PolicyHistoryServiceAction : ServiceAction<PolicyHistory>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public PolicyHistoryServiceAction()
        {
            //
            // By default all actions are valid.  Only Modify may be invalid.
            //
            //this.WasActionValid = true;
        }
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        /// <param name="policyHistoryId"></param>
        /// <param name="policyId"></param>
        /// <param name="versionId"></param>
        /// <param name="clientId"></param>
        /// <param name="typeId"></param>
        /// <param name="name"></param>
        /// <param name="details"></param>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="restrictionTypeId"></param>
        /// <param name="restrictionStartTime"></param>
        /// <param name="restrictionEndTime"></param>
        /// <param name="active"></param>
        /// <param name="policyStatus"></param>
        public void Populate(int policyHistoryId, int policyId, int versionId, int clientId, int typeId, string name, string details, DateTime startDateTime, DateTime? endDateTime, int? restrictionTypeId, TimeSpan? restrictionStartTime, TimeSpan? restrictionEndTime, bool active, PolicyStatus policyStatus)
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
            this.PolicyStatusId = (int)policyStatus;
        }
        #endregion
        #region Properties
        public int PolicyHistoryId { get; set; }
        public int PolicyId { get; set; }
        public int VersionId { get; set; }
        public int ClientId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public System.DateTime StartDateTime { get; set; }
        public Nullable<System.DateTime> EndDateTime { get; set; }
        public Nullable<int> RestrictionTypeId { get; set; }
        public Nullable<System.TimeSpan> RestrictionStartTime { get; set; }
        public Nullable<System.TimeSpan> RestrictionEndTime { get; set; }
        public bool Active { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool DeletedFlag { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public int PolicyStatusId { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the PolicyHistory entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">PolicyHistory entity</param>
        protected override void Populate(PolicyHistory entity)
        {
            entity.Populate(this.PolicyHistoryId,this.PolicyId,this.VersionId,this.ClientId,this.TypeId,this.Name,this.Details,this.StartDateTime,this.EndDateTime,this.RestrictionTypeId,this.RestrictionStartTime,this.RestrictionEndTime,this.Active, this.PolicyStatusId);
        }
        /// <summary>
        /// Indicates if the PolicyHistory has data.
        /// </summary>
        protected override bool HasData
        {
            get
            {
                //
                // By definition we are expecting that it will have data.  Even
                // null or the empty string should be stored.
                //
                return true;
            }
        }
        /// <summary>
        /// Indicates if the data represents a delete.
        /// </summary>
        protected override bool IsDelete
        {
            get { return ((this.EntityIdentifier != 0) & (string.IsNullOrWhiteSpace(this.Name))); }
        }
        #endregion
        #region Optional Overrides
        #endregion
    }
}
