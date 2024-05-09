using Sra.P2rmis.Dal;
using System;

namespace Sra.P2rmis.Bll.Security
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete PolicyWeekDayHistory.
    /// </summary>
    public class PolicyWeekDayHistoryServiceAction : ServiceAction<PolicyWeekDayHistory>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public PolicyWeekDayHistoryServiceAction()
        {
        }

        public void Populate(int policyWeekDayHistoryId, int? policyHistoryId, int policyId, int versionId, int weekDayId)
        {
            this.PolicyWeekDayHistoryId = policyWeekDayHistoryId;
            this.PolicyHistoryId = policyHistoryId;
            this.PolicyId = policyId;
            this.VersionId = versionId;
            this.WeekDayId = weekDayId;
        }
        #endregion
        #region Properties
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
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the PolicyWeekDayHistory entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">PolicyWeekDayHistory entity</param>
        protected override void Populate(PolicyWeekDayHistory entity)
        {
            entity.Populate(this.PolicyWeekDayHistoryId,this.PolicyHistoryId,this.PolicyId,this.VersionId,this.WeekDayId);
        }
        /// <summary>
        /// Indicates if the PolicyWeekDayHistory has data.
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
        #endregion
        #region Optional Overrides
        #endregion
    }
}
