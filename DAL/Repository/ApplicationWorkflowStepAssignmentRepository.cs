using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    public class ApplicationWorkflowStepAssignmentRepository : GenericRepository<ApplicationWorkflowStepAssignment>, IApplicationWorkflowStepAssignmentRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationWorkflowStepAssignmentRepository(P2RMISNETEntities context) : base(context)
        {     
        }
        #endregion
        #region Services

        /// <summary>
        /// Retrieves the ApplicationWorkflowStepAssignment for the specified workflow step
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="thisWorkflowStepId">Workflow step identifier</param>
        public ApplicationWorkflowStepAssignment GetStepAssignment(int thisWorkflowStepId)
        {
            var results = context.ApplicationWorkflowStepAssignments.Where(x => x.ApplicationWorkflowStepId == thisWorkflowStepId).FirstOrDefault();
            return results;
        }
        /// <summary>
        /// Deletes ApplicationWorkflowStepAssignment associated with an application workflow
        /// </summary>
        /// <param name="appWorkflowId">Application workflow identifier</param>
        /// <param name="userId">User identifier</param>
        public void DeleteByWorkflowId(int appWorkflowId, int userId)
        {
            var contentToDelete =
                from awfsa in context.ApplicationWorkflowStepAssignments
                join awfs in context.ApplicationWorkflowSteps
                on awfsa.ApplicationWorkflowStepId equals awfs.ApplicationWorkflowStepId
                where awfs.ApplicationWorkflowId == appWorkflowId
                select awfsa;

            foreach (ApplicationWorkflowStepAssignment item in contentToDelete.ToList())
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
        public void Delete(ApplicationWorkflowStepAssignment entity, int userId)
        {
            Helper.UpdateDeletedFields(entity, userId);
            Delete(entity);
        }
        #endregion
    }
}
