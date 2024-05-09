using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for  ApplicationWorkflowStep objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public class ApplicationWorkflowStepRepository : GenericRepository<ApplicationWorkflowStep>, IApplicationWorkflowStepRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationWorkflowStepRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
        #region Services provided
        /// <summary>
        /// Deletes ApplicationWorkflowStep associated with an application workflow
        /// </summary>
        /// <param name="appWorkflowId">Application workflow identifier</param>
        /// <param name="userId">User identifier</param>
        public void DeleteByWorkflowId(int appWorkflowId, int userId)
        {
            var contentToDelete =
                from awfs in context.ApplicationWorkflowSteps
                where awfs.ApplicationWorkflowId == appWorkflowId
                select awfs;

            foreach (ApplicationWorkflowStep item in contentToDelete.ToList())
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
        public void Delete(ApplicationWorkflowStep entity, int userId)
        {
            Helper.UpdateDeletedFields(entity, userId);
            Delete(entity);
        }
        /// <summary>
        /// List all of the ApplicationWorkFlowSteps that have submittable critiques.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier (identifies the panel)</param>
        /// <param name="stepTypeId">StepType identifier (identifies the phase)</param>
        /// <returns>Enumerable collection of workflow steps with workflow steps that could have submittable critiques</returns>
        public IEnumerable<ApplicationWorkflowStep> ListSessionPanelSubmittableWorkflowSteps(int sessionPanelId, int stepTypeId)
        {
            var results = RepositoryHelpers.ListSessionPanelSubmittableWorkflowSteps(context.PanelUserAssignments, sessionPanelId, stepTypeId);
            return results;
        }
        /// <summary>
        /// Is the summary statement checked out
        /// </summary>
        /// <param name="appliciationWorkflowId">Application Workflow identifier</param>
        public bool IsWorkflowStepCheckedOut(int appliciationWorkflowId)
        {
            var entity = GetByID(appliciationWorkflowId);
            return entity.ApplicationWorkflowStepWorkLogs.FirstOrDefault().CheckOutDate == null;
        }
        /// <summary>
        /// Determines whether [is workflow step review] [the specified application workflow step identifier].
        /// </summary>
        /// <param name="applicationWorkflowStepId">The application workflow step identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is workflow step review] [the specified application workflow step identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsWorkflowStepReview(int applicationWorkflowStepId)
        {
            var entity = GetByID(applicationWorkflowStepId);
            return entity.StepTypeId == StepType.Indexes.Review;
        }
        #endregion

    }
}
