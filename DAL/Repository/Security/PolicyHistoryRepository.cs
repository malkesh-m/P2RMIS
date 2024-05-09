
using System.Linq;

namespace Sra.P2rmis.Dal.Repository.Security
{
    /// <summary>
    /// Database access methods for the PolicyHistories.
    /// </summary>
    public interface IPolicyHistoryRepository : IGenericRepository<PolicyHistory>
    {
        /// <summary>
        /// Retrieves the entity object by policyId.
        /// </summary>
        /// <param name="policyId">Policy identifier</param>
        /// <returns>Entity object if located; null otherwise</returns>
        PolicyHistory GetLastByPolicyId(int policyId);
    }
    public class PolicyHistoryRepository : GenericRepository<PolicyHistory>, IPolicyHistoryRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public PolicyHistoryRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion
        #region Services
        /// <summary>
        /// Retrieves the entity object by policyId.
        /// </summary>
        /// <param name="policyId">Policy identifier</param>
        /// <returns>Entity object if located; null otherwise</returns>
        public virtual PolicyHistory GetLastByPolicyId(int policyId)
        {
            return dbSet.OrderByDescending(x=>x.PolicyHistoryId)
                .FirstOrDefault(x => x.PolicyId == policyId);
            //if(policyHistory==null)
            //{
            //    return policyHistory;
            //}
            //return 
        }
        #endregion
    }
}
