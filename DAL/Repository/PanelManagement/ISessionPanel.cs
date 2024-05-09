using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.PanelManagement;
using System.Collections.Generic;


namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for SessionPanel objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public interface ISessionPanelRepository : IGenericRepository<SessionPanel>
    {
        /// <summary>
        /// Retrieves the list of expertise types for the specified session panel.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param
        /// <param name="panelApplicationId"></param
        /// <param name="panelUserAssignmentId"></param
        /// <returns></returns>        
        ResultModel<IExpertiseAssignments> GetExpertiseAssignments(int sessionPanelId, int panelApplicationId, int panelUserAssignmentId);
        
        /// <summary>
        /// Gets the Session Panel by identifier with panel application information.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>Session panel instance with panel and application data eager loaded</returns>
        SessionPanel GetByIDWithPanelApplicationInfo(int sessionPanelId);

        /// <summary>
        /// Retrieves lists of primary email addresses.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Container of IEmailAddress models</returns>
        ResultModel<IEmailAddress> ListPanelUserEmailAddresses(int sessionPanelId);
        /// <summary>
        /// Get a session panel's program year information
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>IProgramYearModel model</returns>
        IProgramYearModel GetProgramYear(int sessionPanelId);

        /// <summary>
        /// Gets the panel stage steps status.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        List<PanelStageStepModel> GetPanelStageStepsStatus(int sessionPanelId);
        /// <summary>
        /// Gets the panel by identifier with panel application statuses.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>Session panel instance with panel application status info eager loaded</returns>
        SessionPanel GetByIDWithPanelApplicationStatus(int sessionPanelId);

        SessionPanel GetSessionPanelId(int programYearId, string referralMappingPanelAbbrv);

    }
}
