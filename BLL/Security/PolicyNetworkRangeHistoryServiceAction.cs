using Sra.P2rmis.Dal;
using System;

namespace Sra.P2rmis.Bll.Security
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete PolicyNetworkRangeHistory.
    /// </summary>
    public class PolicyNetworkRangeHistoryServiceAction : ServiceAction<PolicyNetworkRangeHistory>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public PolicyNetworkRangeHistoryServiceAction()
        {
        }

        public void Populate(int policyNetworkRangeHistoryId, int? policyHistoryId, int policyId, int versionId, string startAddress, string endAddress)
        {
            this.PolicyNetworkRangeHistoryId = policyNetworkRangeHistoryId;
            this.PolicyHistoryId = policyHistoryId;
            this.PolicyId = policyId;
            this.VersionId = versionId;
            this.StartAddress = startAddress;
            this.EndAddress = endAddress;
        }
        #endregion
        #region Properties
        public int PolicyNetworkRangeHistoryId { get; set; }
        public Nullable<int> PolicyHistoryId { get; set; }
        public int PolicyId { get; set; }
        public int VersionId { get; set; }
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }
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
        /// Populate the PolicyNetworkRangeHistory entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">PolicyNetworkRangeHistory entity</param>
        protected override void Populate(PolicyNetworkRangeHistory entity)
        {
            entity.Populate(this.PolicyNetworkRangeHistoryId,this.PolicyHistoryId,this.PolicyId,this.VersionId,this.StartAddress,this.EndAddress);
        }
        /// <summary>
        /// Indicates if the PolicyNetworkRangeHistory has data.
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
            get { return ((this.EntityIdentifier != 0) & (string.IsNullOrWhiteSpace(this.StartAddress))); }
        }
        #endregion
        #region Optional Overrides
        #endregion
    }
}
