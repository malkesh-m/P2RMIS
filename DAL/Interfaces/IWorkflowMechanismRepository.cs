namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository definition for WorkflowMechanism objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public interface IWorkflowMechanismRepository : IGenericRepository<WorkflowMechanism>
    {
        /// <summary>
        /// Retrieves the entity object by the mechanism identifier and review status identifier
        /// </summary>
        /// <param name="mechanismId">Mechanism identifier</param>
        /// <param name="reviewStatusId">Review Status identifier</param>
        /// <returns>Entity object if located; null otherwise</returns>
        WorkflowMechanism GetByMechanismIdAndReviewStatusId(int mechanismId, int? reviewStatusId);
    }
}
