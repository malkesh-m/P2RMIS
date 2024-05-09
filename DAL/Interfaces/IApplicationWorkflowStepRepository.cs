using System.Collections.Generic;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for  ApplicationWorkflowStep objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public interface IApplicationWorkflowStepRepository : IGenericRepository<ApplicationWorkflowStep>
    {
        /// <summary>
        /// Delete application workflow steps
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
        void Delete(ApplicationWorkflowStep entity, int userId);
        /// <summary>
        /// List all of the ApplicationWorkFlowSteps that have submittable critiques.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier (identifies the panel)</param>
        /// <param name="stepTypeId">StepType identifier (identifies the phase)</param>
        /// <returns>Enumerable collection of workflow steps with workflow steps that could have submittable critiques</returns>
        IEnumerable<ApplicationWorkflowStep> ListSessionPanelSubmittableWorkflowSteps(int sessionPanelId, int stepTypeId);
        /// <summary>
        /// Determines whether [is workflow step review] [the specified application workflow step identifier].
        /// </summary>
        /// <param name="applicationWorkflowStepId">The application workflow step identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is workflow step review] [the specified application workflow step identifier]; otherwise, <c>false</c>.
        /// </returns>
        bool IsWorkflowStepReview(int applicationWorkflowStepId);
    }
}
