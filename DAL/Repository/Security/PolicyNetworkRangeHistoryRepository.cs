namespace Sra.P2rmis.Dal.Repository.Security
{
    public interface IPolicyNetworkRangeHistoryRepository : IGenericRepository<PolicyNetworkRangeHistory>
    {        
    }
    public class PolicyNetworkRangeHistoryRepository : GenericRepository<PolicyNetworkRangeHistory>, IPolicyNetworkRangeHistoryRepository
    {
        public PolicyNetworkRangeHistoryRepository(P2RMISNETEntities context) : base(context) { }        
    }
}
