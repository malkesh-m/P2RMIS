namespace Sra.P2rmis.Dal.Repository.Security
{
    public interface IPolicyNetworkRangeRepository : IGenericRepository<PolicyNetworkRange>
    {        
    }
    public class PolicyNetworkRangeRepository : GenericRepository<PolicyNetworkRange>, IPolicyNetworkRangeRepository
    {
        public PolicyNetworkRangeRepository(P2RMISNETEntities context) : base(context) { }        
    }
}
