using System.Collections.Generic;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for ApplicationWorkflowStepElementContent objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public interface IApplicationWorkflowStepElementContentRepository : IGenericRepository<ApplicationWorkflowStepElementContent>
    {
        /// <summary>
        /// Add multiple ApplicationWorkflowStepElementContent entity objects
        /// to the context.
        /// </summary>
        /// <param name="list">List of ApplicationWorkflowStepElementContent to add</param>
        void Add(List<ApplicationWorkflowStepElementContent> list);
        /// <summary>
        /// Deletes an ApplicationWorkflowStepElementContent associated with an application workflow id
        /// </summary>
        /// <param name="appWorkflowId">Application workflow identifier</param>
        /// <param name="userId">User identifier</param>
        void DeleteByWorkflowId(int appWorkflowId, int userId);
        /// <summary>
        /// Deletes ApplicationWorkflowStepElementContent of the target step and post-target steps 
        /// associated with an application workflow id        /// 
        /// </summary>
        void DeletePostTargetStepElementContent(int appWorkflowId, int targetWorkflowStepId, int userId);
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
        void Delete(ApplicationWorkflowStepElementContent entity, int userId);
        /// <summary>
        /// Gets the by element identifier.
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">The application workflow step element identifier.</param>
        /// <returns></returns>
        ApplicationWorkflowStepElementContent GetByElementId(int applicationWorkflowStepElementId);
    }
}
