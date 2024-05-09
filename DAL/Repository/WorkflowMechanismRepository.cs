using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for WorkflowMechanism objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public class WorkflowMechanismRepository: GenericRepository<WorkflowMechanism>, IWorkflowMechanismRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public WorkflowMechanismRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion
        #region Services
        /// <summary>
        /// Retrieves the entity object by the mechanism identifier and review status identifier
        /// </summary>
        /// <param name="mechanismId">Mechanism identifier</param>
        /// <param name="reviewStatusId">Review Status identifier</param>
        /// <returns>Entity object if located; null otherwise</returns>
        public virtual WorkflowMechanism GetByMechanismIdAndReviewStatusId(int mechanismId, int? reviewStatusId)
        {
            return dbSet.FirstOrDefault(x => (x.MechanismId == mechanismId) & (x.ReviewStatusId == reviewStatusId));
        }
        #endregion
    }
}
