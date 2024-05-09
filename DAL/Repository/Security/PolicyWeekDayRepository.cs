namespace Sra.P2rmis.Dal.Repository.Security
{
    public interface IPolicyWeekDayRepository : IGenericRepository<PolicyWeekDay>
    {        
    }
    public class PolicyWeekDayRepository : GenericRepository<PolicyWeekDay>,IPolicyWeekDayRepository
    {
        public PolicyWeekDayRepository(P2RMISNETEntities context) : base(context) { }        
    }
}
