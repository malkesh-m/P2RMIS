using System.Collections.Generic;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for IApplicationWorkflowStepElementContentHistory objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public interface IApplicationWorkflowStepElementContentHistoryRepository : IGenericRepository<ApplicationWorkflowStepElementContentHistory>
    {
        /// <summary>
        /// Adds each collection entry to the context.
        /// </summary>
        /// <param name="collection">Collection of ApplicationWorkflowStepElementContentHistory entity objects</param>
        void AddRange(ICollection<ApplicationWorkflowStepElementContentHistory> collection);
    }
}
