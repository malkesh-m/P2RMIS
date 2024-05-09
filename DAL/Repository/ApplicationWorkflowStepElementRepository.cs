using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for ApplicationWorkflowStepElementRepository objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>   
    public class ApplicationWorkflowStepElementRepository : GenericRepository<ApplicationWorkflowStepElement>, IApplicationWorkflowStepElementRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationWorkflowStepElementRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion
        #region Services provided
        /// <summary>
        /// Deletes ApplicationWorkflowStepElement associated with an application workflow
        /// </summary>
        /// <param name="appWorkflowId">Application workflow identifier</param>
        /// <param name="userId">User identifier</param>
        public void DeleteByWorkflowId(int appWorkflowId, int userId)
        {
            var contentToDelete =
                from awfse in context.ApplicationWorkflowStepElements
                join awfs in context.ApplicationWorkflowSteps
                on awfse.ApplicationWorkflowStepId equals awfs.ApplicationWorkflowStepId
                where awfs.ApplicationWorkflowId == appWorkflowId
                select awfse;

            foreach (ApplicationWorkflowStepElement item in contentToDelete.ToList())
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
        public void Delete(ApplicationWorkflowStepElement entity, int userId)
        {
            Helper.UpdateDeletedFields(entity, userId);
            Delete(entity);
        }
        #endregion
        #region Services Not Provided
        #endregion
    }
}
