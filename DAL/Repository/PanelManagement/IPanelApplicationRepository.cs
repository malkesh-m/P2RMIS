using System.Collections.Generic;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for PanelApplication objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary> 
    public interface IPanelApplicationRepository : IGenericRepository<PanelApplication>
    {
        /// <summary>
        /// Retrieve the PanelApplicationReviewerAssignment entity object with the specified sort order
        /// on the PanelApplication 
        /// </summary>
        /// <param name="presentationOrder">Presentation order</param>
        /// <param name="panelApplicationId">Panel Application identifier</param>
        /// <returns>PanelApplicationReviewerAssignment entity object with the specified presentation order</returns>
        PanelApplicationReviewerAssignment GetPanelApplicationReviewerAssignment(int presentationOrder, int panelApplicationId);
        /// <summary>
        /// Retrieve the PanelApplicationReviewerAssignment entity object for a specific reviewer
        /// on the PanelApplication 
        /// </summary>
        /// <param name="panelApplicationId">Panel Application identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <returns>PanelApplicationReviewerAssignment entity object for the specified reviewer or null if none found</returns>
        PanelApplicationReviewerAssignment GetPanelApplicationReviewerAssignmentForSpecificReviewer(int panelApplicationId, int panelUserAssignmentId);
        /// <summary>
        /// Gets the panel applications for reviewer.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        IEnumerable<PanelApplication> GetPanelApplicationsForScoring(int sessionPanelId);
        /// <summary>
        /// Gets the panel applications for pre assigned.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        IEnumerable<PanelApplication> GetPanelApplicationsForPreAssigned(int sessionPanelId);
        /// <summary>
        /// Gets the panel applications for post assigned.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="assignedPanelApplicationIds">The assigned panel application ids.</param>
        /// <returns></returns>
        IEnumerable<PanelApplication> GetPanelApplicationsForPostAssigned(int sessionPanelId, List<int> assignedPanelApplicationIds);
        /// <summary>
        /// Gets the panel applications for chairs.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="assignedPanelApplicationIds">The assigned panel application ids.</param>
        /// <returns></returns>
        IEnumerable<PanelApplication> GetPanelApplicationsForChairs(int sessionPanelId, List<int> assignedPanelApplicationIds);
        /// <summary>
        /// Determines whether the specified session panel identifier is released.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified session panel identifier is released; otherwise, <c>false</c>.
        /// </returns>
        bool IsReleased(int sessionPanelId);
        /// <summary>
        /// Gets the panel application entity including panel context, workflows, steps, score and reviewer information
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns>Panel application entity with additional data eager loaded</returns>
        PanelApplication GetByIDWithPanelCritiqueInfo(int panelApplicationId);
        
        /// <summary>
        /// Gets the by panel application by identifier with score scale and legend information.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns>Panel application entity with legend information eager loaded</returns>
        PanelApplication GetByIDWithScoreLegend(int panelApplicationId);
        /// <summary>
        /// Gets the with assignments.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns></returns>
        PanelApplication GetWithAssignments(int panelApplicationId);
        /// <summary>
        /// Gets the panel applications.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        IEnumerable<PanelApplication> GetPanelApplications(int programYearId, int receiptCycle);
        /// <summary>
        /// Adds the panel application.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void AddPanelApplication(int sessionPanelId, int applicationId, int userId);
        /// <summary>
        /// Gets the peer review data.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        List<PeerReviewResultModel> GetPeerReviewData(int clientId);
    }
}
