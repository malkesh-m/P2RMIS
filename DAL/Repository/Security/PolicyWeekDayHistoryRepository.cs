namespace Sra.P2rmis.Dal.Repository.Security
{
    public interface IPolicyWeekDayHistoryRepository : IGenericRepository<PolicyWeekDayHistory>
    {        
    }
    public class PolicyWeekDayHistoryRepository : GenericRepository<PolicyWeekDayHistory>,IPolicyWeekDayHistoryRepository
    {
        public PolicyWeekDayHistoryRepository(P2RMISNETEntities context) : base(context) { }        
    }
}
