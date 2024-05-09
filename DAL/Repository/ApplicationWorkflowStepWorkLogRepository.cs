using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for ApplicationWorkflowStepWorkLogRepository objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>   
    public class ApplicationWorkflowStepWorkLogRepository : GenericRepository<ApplicationWorkflowStepWorkLog>, IApplicationWorkflowStepWorkLogRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationWorkflowStepWorkLogRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
        #region Services provided
        /// <summary>
        /// Find the last matching log entry for this workflow.
        /// </summary>
        /// <param name="workflowStepId">WorkflowStep identifier</param>
        /// <returns>Last matching log entry for the workflow with a CheckInDate of null.</returns>
        public ApplicationWorkflowStepWorkLog FindInCompleteWorkLogEntryByWorkflowStep(int workflowStepId)
        {
            var result = from wl in context.ApplicationWorkflowStepWorkLogs where ((wl.ApplicationWorkflowStepId == workflowStepId) && (wl.CheckInDate == null)) select wl;
            if (result.Any())
                return result.FirstOrDefault();
            else
                return null;
        }
        /// <summary>
        /// Deletes ApplicationWorkflowStepWorkLog associated with an application workflow
        /// </summary>
        /// <param name="appWorkflowId">Application workflow identifier</param>
        /// <param name="userId">User identifier</param>
        public void DeleteByWorkflowId(int appWorkflowId, int userId)
        {
            var contentToDelete =
                from awfsw in context.ApplicationWorkflowStepWorkLogs
                join awfs in context.ApplicationWorkflowSteps
                on awfsw.ApplicationWorkflowStepId equals awfs.ApplicationWorkflowStepId
                where awfs.ApplicationWorkflowId == appWorkflowId
                select awfsw;

            foreach (ApplicationWorkflowStepWorkLog item in contentToDelete.ToList())
            {
                Delete(item, userId);
            }
        }
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <param name="userId">User identifier</param>
        public void Delete(int id, int userId)
        {
            var entity = GetByID(id);
            Helper.UpdateDeletedFields(entity, userId);
            Delete(id);
        }
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <param name="userId">User identifier</param>
        public void Delete(ApplicationWorkflowStepWorkLog entity, int userId)
        {
            Helper.UpdateDeletedFields(entity, userId);
            Delete(entity);
        }
        #endregion
        #region Services Not Provided
        #endregion
    }
}
