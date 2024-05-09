namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for ApplicationWorkflowStepWorkLogRepository objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary> 
    public interface IApplicationWorkflowStepWorkLogRepository : IGenericRepository<ApplicationWorkflowStepWorkLog>
    {
        /// <summary>
        /// Find the last matching log entry for this workflow.
        /// </summary>
        /// <param name="workflowStepId">WorkflowStep identifier</param>
        /// <returns>Last matching log entry for the workflow with a CheckInDate of null.</returns>
        ApplicationWorkflowStepWorkLog FindInCompleteWorkLogEntryByWorkflowStep(int workflowStepId);
        /// <summary>
        /// Delete application workflow step work logs
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
        void Delete(ApplicationWorkflowStepWorkLog entity, int userId);
     }
}
