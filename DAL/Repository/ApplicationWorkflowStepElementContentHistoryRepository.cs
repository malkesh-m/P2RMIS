using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for ApplicationWorkflowStepElementContentHistory objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public class ApplicationWorkflowStepElementContentHistoryRepository : GenericRepository<ApplicationWorkflowStepElementContentHistory>, IApplicationWorkflowStepElementContentHistoryRepository
    {
         #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationWorkflowStepElementContentHistoryRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion 
        #region Services provided
        /// <summary>
        /// Adds each collection entry to the context.
        /// </summary>
        /// <param name="collection">Collection of ApplicationWorkflowStepElementContentHistory entity objects</param>
        public void AddRange(ICollection<ApplicationWorkflowStepElementContentHistory> collection)
        {
            collection.ToList().ForEach(delegate(ApplicationWorkflowStepElementContentHistory entity) { Add(entity); });
        }
        #endregion
    }
}
