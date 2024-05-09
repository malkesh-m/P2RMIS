namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for ApplicationWorkflowStepAssignment objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public interface IApplicationWorkflowStepAssignmentRepository: IGenericRepository<ApplicationWorkflowStepAssignment>
    {
        /// <summary>
        /// Retrieves the ApplicationWorkflowStepAssignment for the specified workflow step
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="thisWorkflowStepId">Workflow step identifier</param>
        ApplicationWorkflowStepAssignment GetStepAssignment(int thisWorkflowStepId);
        /// <summary>
        /// Delete application workflow step assignments
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
        void Delete(ApplicationWorkflowStepAssignment entity, int userId);
    }
}
