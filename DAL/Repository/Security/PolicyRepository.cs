namespace Sra.P2rmis.Dal.Repository.Security
{
    public interface IPolicyRepository : IGenericRepository<Policy>
    {        
    }
    public class  PolicyRepository : GenericRepository<Policy>, IPolicyRepository
    {
        public PolicyRepository(P2RMISNETEntities context) : base(context) { }        
    }
}
