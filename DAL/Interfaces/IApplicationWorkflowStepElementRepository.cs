namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for ApplicationWorkflowStepElementRepository objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary> 
    public interface IApplicationWorkflowStepElementRepository : IGenericRepository<ApplicationWorkflowStepElement>
    {
        /// <summary>
        /// Delete application workflow step elements
        /// </summary>
        /// <param name="appWorkflowId">Application workflow identifier</param>
        /// <param name="userId">User identifier</param>
        void DeleteByWorkflowId(int appWorkflowId, int userId);
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <param name="userId">User identifier</param>
        void Delete(int id, int userId);
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <param name="userId">User identifier</param>
        void Delete(ApplicationWorkflowStepElement entity, int userId);
    }
}
