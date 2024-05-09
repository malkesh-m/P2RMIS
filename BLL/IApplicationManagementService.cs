using System;
using System.Collections.Generic;
using Sra.P2rmis.Bll.ApplicationManagement;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Bll.Views.ApplicationDetails;
using Sra.P2rmis.Bll.Views.CritiqueDetails;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.Lists;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Service providing access to Open Program.  Services provided are:
    ///      - GetOpenProgramList - generates a list of Open Programs
    /// </summary>
    public interface IApplicationManagementService: IDisposable
    {
        /// <summary>
        /// Generate a list of open Programs.
        /// </summary>
        ApplicationManagementView GetOpenProgramsList(List<int> clientList);
        /// <summary>
        /// Generate a list of assigned open Programs.
        /// </summary>
        ApplicationManagementView GetAssignedOpenProgramsList(int userId);
        /// <summary>
        /// Retrieve the application's details.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="panelId">Panel identifier</param>
        /// <returns>-----</returns>
        ApplicationDetailsContainer GetApplicationDetails(int panelApplicationId, int panelId, List<int> commentTypes);
        /// <summary>
        /// Add a comment
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="comments">Comments</param>
        /// <param name="commentType">CommentType entity identifier</param>
        /// <remarks>TODO: consolidate with the ApplicationScoringService.AddComment</remarks>
        void AddComment(int userId, int panelApplicationId, string comments, int commentType);
        /// <summary>
        /// Edit a user comment.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="commentId">TODO:: document</param>
        /// <param name="comments">User comments</param>
        /// <param name="commentType">TODO:: document</param>
        /// <remarks>TODO: consolidate with the ApplicationScoringService.EditComment</remarks>
        void EditComment(int userId, int commentId, string comments, int commentType);
        /// <summary>
        /// Delete a user's comment
        /// </summary>
        /// <param name="commentId">Comment identifier</param>
        void DeleteComment(int commentId);
        /// <summary>
        /// List the user's panels in any open session.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of IListEntry entities</returns>
        Container<IListEntry> ListUsersOpenPanels(int userId);
        /// <summary>
        /// Get the scoring legend by mechanism template identifier.
        /// </summary>
        /// <param name="mechanismTemplateId">Mechanism template identifier.</param>
        /// <returns></returns>
        ClientScoringScaleLegendModel GetScoringLegendByMechanismTemplateId(int mechanismTemplateId);
        /// <summary>
        /// Get the scoring legend for this panel application for the synchronous (online scoring) stage
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>ClientScoringScaleLegendModel</returns>
        ClientScoringScaleLegendModel GetScoringLegendSyncStage(int panelApplicationId);
        /// <summary>
        /// Get the scoring legend for this panel application's current review stage
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>ClientScoringScaleLegendModel</returns>
        ClientScoringScaleLegendModel GetScoringLegendCurrentStage(int panelApplicationId);
        /// <summary>
        /// Retrieves the status of the specified SessionPanel
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>PanelStatus entity describing the SessionPanel's status</returns>
        PanelStatus OpenPanelStatus(int sessionPanelId, int userId);
        /// <summary>
        /// List the user's panels in any open session for a non-reviewer (admin) user.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of IListEntry entities</returns>
        Container<IListEntry> ListAdminUsersOpenPanels(int userId);
        /// <summary>
        /// Retrieves a specific application budget note
        /// </summary>
        /// <param name="applicationBudgetId">The application budget identifier</param>
        /// <returns>AdminBudgetNoteModel</returns>
        IAdminBudgetNoteModel GetSpecificAdminBudgetNote(int applicationBudgetId);
        /// <summary>
        /// Retrieves all the application budget notes for a specific 
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns>AdminBudgetNoteModel</returns>
        IAdminBudgetNoteModel GetApplicationAdminBudgetNote(int applicationId);

        void DeleteAdminNote(int applicationBudgetId, int userId);
        /// <summary>
        /// Create or add's an admin note.  If the containing ApplicationBudget entity does
        /// not exist, it will be created.
        /// </summary>
        /// <param name="applicationBudgetId">Application entity identifier</param>
        /// <param name="applicationId">Application entity identifier</param>
        /// <param name="note">Administration note text</param>
        /// <param name="userId">User entity identifier of user making change</param>
        void AddModifyAdminNote(int applicationBudgetId, int applicationId, string note, int userId);
        /// <summary>
        /// Modify withdraw status
        /// </summary>
        /// <param name="logNumber"></param>
        /// <returns></returns>
        void ModifyWithdrawStatus(int applicationId, int? withdrawnBy, bool withdrawnFlag, DateTime? withdrawnDate);
        /// <summary>
        /// find application by log number
        /// </summary>
        /// <param name="logNumber"></param>
        /// <returns></returns>
        Sra.P2rmis.Dal.Application FindApplicationById(int applicationId);
    }
}
