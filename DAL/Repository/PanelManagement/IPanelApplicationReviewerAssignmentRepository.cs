using System.Linq;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository interface for PanelApplicationReviewerAssignment objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary> 
    public interface IPanelApplicationReviewerAssignmentRepository : IGenericRepository<PanelApplicationReviewerAssignment>
    {
        /// <summary>
        /// Get reviewer assignment
        /// </summary>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>Panel application reviewer assignment</returns>
        PanelApplicationReviewerAssignment GetReviewerAssignment(int panelUserAssignmentId, int panelApplicationId);
        /// <summary>
        /// Gets the reviewer assignments.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        IQueryable<PanelApplicationReviewerAssignment> GetReviewerAssignments(int panelUserAssignmentId);
        /// <summary>
        /// Sets up all the necessary information for a reviewer to evaluate an application
        /// </summary>
        /// <param name="reviewerAssignmentId">An assignemnt of a reviewer to an individual application</param>
        /// <param name="userId">User identifier</param>
        void StartReviewerWorkflow(int reviewerAssignmentId, int userId);
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
        void Delete(PanelApplicationReviewerAssignment entity, int userId);

        /// <summary>
        /// Gets the reviewer assignments who are expected to provide a critique.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns></returns>
        PanelApplicationReviewerAssignment GetReviewerAssignmentForCritique(int panelUserAssignmentId,
            int panelApplicationId);

        /// <summary>
        /// Starts the reviewer workflow for panel.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void StartReviewerWorkflowForPanel(int sessionPanelId, int userId);
    } 
}
