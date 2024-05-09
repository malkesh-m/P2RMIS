using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for ApplicationWorkflowStepElementContent objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public class ApplicationWorkflowStepElementContentRepository : GenericRepository<ApplicationWorkflowStepElementContent>, IApplicationWorkflowStepElementContentRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationWorkflowStepElementContentRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
        #region Services provided
        /// <summary>
        /// Add multiple ApplicationWorkflowStepElementContent entity objects
        /// to the context.
        /// </summary>
        /// <param name="list">List of ApplicationWorkflowStepElementContent to add</param>
        public void Add(List<ApplicationWorkflowStepElementContent> list)
        {
            if (list != null)
            {
                list.ForEach(x => this.Add(x));
            }
        }
        /// <summary>
        /// Deletes ApplicationWorkflowStepElementContent associated with an application workflow id
        /// </summary>
        /// <param name="appWorkflowId">Application workflow identifier</param>
        /// <param name="userId">User identifier</param>
        public void DeleteByWorkflowId(int appWorkflowId, int userId)
        {
            var contentToDelete =
                from awfs in context.ApplicationWorkflowSteps
                join awse in context.ApplicationWorkflowStepElements
                on awfs.ApplicationWorkflowStepId equals awse.ApplicationWorkflowStepId
                join awsec in context.ApplicationWorkflowStepElementContents
                on awse.ApplicationWorkflowStepElementId equals awsec.ApplicationWorkflowStepElementId
                where awfs.ApplicationWorkflowId == appWorkflowId
                select awsec;

            foreach (ApplicationWorkflowStepElementContent item in contentToDelete)
            {
                Delete(item, userId);
            }
        }
        /// <summary>
        /// Deletes ApplicationWorkflowStepElementContent of the target step and post-target steps 
        /// associated with an application workflow id        /// 
        /// </summary>
        /// <param name="appWorkflowId"></param>
        /// <param name="targetWorkflowStepId"></param>
        /// <param name="userId"></param>
        public void DeletePostTargetStepElementContent(int appWorkflowId, int targetWorkflowStepId, int userId)
        {
            var contentToDelete =
                from awfs in context.ApplicationWorkflowSteps
                join awse in context.ApplicationWorkflowStepElements
                on awfs.ApplicationWorkflowStepId equals awse.ApplicationWorkflowStepId
                join awsec in context.ApplicationWorkflowStepElementContents
                on awse.ApplicationWorkflowStepElementId equals awsec.ApplicationWorkflowStepElementId
                join awfsTarget in context.ApplicationWorkflowSteps
                on awfs.ApplicationWorkflowId equals awfsTarget.ApplicationWorkflowId
                where awfs.ApplicationWorkflowId == appWorkflowId && awfsTarget.ApplicationWorkflowStepId == targetWorkflowStepId 
                && awfs.StepOrder >= awfsTarget.StepOrder
                select awsec;

            foreach (ApplicationWorkflowStepElementContent item in contentToDelete)
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
        public void Delete(ApplicationWorkflowStepElementContent entity, int userId)
        {
            Helper.UpdateDeletedFields(entity, userId);
            Delete(entity);
        }
        /// <summary>
        /// Gets the by element identifier.
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">The application workflow step element identifier.</param>
        /// <returns></returns>
        public ApplicationWorkflowStepElementContent GetByElementId(int applicationWorkflowStepElementId)
        {
            return context.ApplicationWorkflowStepElementContents.FirstOrDefault(x => x.ApplicationWorkflowStepElementId == applicationWorkflowStepElementId);
        }
        #endregion
    }
}
