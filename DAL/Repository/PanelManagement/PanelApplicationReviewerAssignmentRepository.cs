using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    /// 
    /// Repository for PanelApplicationReviewerAssignment objects.  Provides CRUD methods and 
    /// associated database services.
    ///
    public partial class PanelApplicationReviewerAssignmentRepository : GenericRepository<PanelApplicationReviewerAssignment>, IPanelApplicationReviewerAssignmentRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public PanelApplicationReviewerAssignmentRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
		
		#region Services provided
        /// <summary>
        /// Get reviewer assignment
        /// </summary>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>Panel application reviewer assignment</returns>
        public PanelApplicationReviewerAssignment GetReviewerAssignment(int panelUserAssignmentId, int panelApplicationId)
        {
            var result = context.PanelApplicationReviewerAssignments.Where(u => u.PanelApplicationId == panelApplicationId)
                .Where(v => v.PanelUserAssignment.PanelUserAssignmentId == panelUserAssignmentId).FirstOrDefault();
            return result;
        }
        /// <summary>
        /// Gets the reviewer assignments.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns></returns>
        public IQueryable<PanelApplicationReviewerAssignment> GetReviewerAssignments(int panelUserAssignmentId)
        {
            var result = context.PanelApplicationReviewerAssignments.Where(v => v.PanelUserAssignment.PanelUserAssignmentId == panelUserAssignmentId);
            return result;
        }

        /// <summary>
        /// Gets the reviewer assignments who are expected to provide a critique.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns></returns>
        public PanelApplicationReviewerAssignment GetReviewerAssignmentForCritique(int panelUserAssignmentId,
            int panelApplicationId)
        {
            var result = context.PanelApplicationReviewerAssignments.Where(u => u.PanelApplicationId == panelApplicationId 
                && u.PanelUserAssignment.PanelUserAssignmentId == panelUserAssignmentId
                && AssignmentType.CritiqueAssignments.Contains(u.ClientAssignmentType.AssignmentTypeId)).FirstOrDefault();
            return result;
        }
        /// <summary>
        /// Sets up all the necessary information for a reviewer to evaluate an application
        /// </summary>
        /// <param name="reviewerAssignmentId">An assignemnt of a reviewer to an individual application</param>
        /// <param name="userId">User identifier</param>
        public void StartReviewerWorkflow(int reviewerAssignmentId, int userId)
        {
            context.uspBeginReviewerWorkflow(reviewerAssignmentId, userId);
        }
        /// <summary>
        /// Sets up all the necessary information for a reviewer to evaluate an application
        /// </summary>
        /// <param name="sessionPanelId">An assignemnt of a reviewer to an individual application</param>
        /// <param name="userId">User identifier</param>
        public void StartReviewerWorkflowForPanel(int sessionPanelId, int userId)
        {
            context.uspBeginReviewerWorkflowPanel(sessionPanelId, userId);
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
        public void Delete(PanelApplicationReviewerAssignment entity, int userId)
        {
            Helper.UpdateDeletedFields(entity, userId);
            Delete(entity);
        }
        #endregion
        #region Services not provided
        #endregion
        #region Overwritten services provided
        #endregion
    }
}
