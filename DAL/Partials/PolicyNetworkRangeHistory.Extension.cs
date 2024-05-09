using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.CrossCuttingServices;
using System;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PolicyHistory object. 
    /// </summary>
    public partial class PolicyNetworkRangeHistory : IStandardDateFields
    {
        public void Populate()
        {
        }

        public void Populate(int policyWeekDayHistoryId, int? policyHistoryId, int policyId, int versionId, string startAddress, string endAddress)
        {
            this.PolicyNetworkRangeHistoryId = policyWeekDayHistoryId;
            this.PolicyHistoryId = policyHistoryId;
            this.PolicyId = policyId;
            this.VersionId = versionId;
            this.StartAddress = startAddress;
            this.EndAddress = endAddress;
        }

    }
}
