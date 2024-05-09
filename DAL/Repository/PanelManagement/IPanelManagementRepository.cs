using System.Collections.Generic;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for PanelManagement objects.  This repository provides only
    /// list retrieval services.  No CRUD methods are supported
    /// </summary> 
    public interface IPanelManagementRepository
    {
        #region View Applications Services
        /// <summary>
        /// Retrieves lists of application information for the specified panel.
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <returns>ResultModel of IApplicationInformationModel models</returns>
        /// </summary>
        ResultModel<IApplicationInformationModel> ListApplicationInformation(int sessionPanelId);
        /// <summary>
        /// Retrieves lists of panel significations for the specified user.
        /// Return empty list if the user is not an SRO
        /// <param name="userId">user identifier</param>
        /// <returns>ResultModel of IPanelSignificationsModel models</returns>
        /// </summary>
        ResultModel<IPanelSignificationsModel> ListPanelSignifications(int userId);
        /// <summary>
        /// Retrieves list of Program/Years for the specified user.
        /// <param name="userId">user identifier</param>
        /// <returns>ResultModel of IProgramYearModel models</returns>
        /// </summary>
        ResultModel<IProgramYearModel> ListProgramYears(int userId);
        /// <summary>
        /// Retrieves list of panel significations for the specific user and Program/Year.
        /// <param name="userId">user identifier</param>
        /// <param name="programYearId">ProgramYear identifier</param>
        /// <returns>ResultModel of IPanelSignificationsModel models</returns>
        /// </summary>
        /// <remarks>When this method is reused from another function (i.e. User Management, Criteria etc.) this and any other methods should be refactored to the CriteriaService.</remarks>
        ResultModel<IPanelSignificationsModel> ListPanelSignifications(int userId, int programYearId);
        /// <summary>
        /// Retrieves list of personnels with conflict of interest.
        /// <param name="applicationId">the application identifier</param>
        /// <returns>ResultModel of IPersonnelWithCoi models</returns>
        /// </summary>
        ResultModel<IPersonnelWithCoi> ListPersonnelWithCoi(int applicationId);

        /// <summary>
        /// Retrieves list of panel names that the application could be transferred to.
        /// <param name="applicationId">Application identifier</param>
        /// <param name="currentPanelId">Panel identifier of the applications current panel</param>
        /// <returns>ResultModel of ITransferPanelModel models</returns>
        /// </summary>
        ResultModel<ITransferPanelModel> ListPanelNames(int applicationId, int currentPanelId);
        /// <summary>
        /// Retrieves list of panel names that the application could be transferred to.
        /// </summary>
        /// <param name="currentPanelId">Panel identifier of the applications current panel</param>
        /// <returns>Container of ITransferPanelModel models</returns>
        ResultModel<ITransferPanelModel> ListSiblingPanelNames(int currentPanelId);
        /// <summary>
        /// Retrieves list of application transfer reasons.
        /// <returns>ResultModel of ITransferPanelModel models</returns>
        /// </summary>
        ResultModel<IReasonModel> ListApplicationTransferReasons();
        /// <summary>
        /// Retrieves list of reviewer expertise.
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="userId">User identifier for the logged in user</param>
        /// <returns>ResultModel of IReviewerExpertise models</returns>
        /// </summary>
        ResultModel<IReviewerExpertise> ListReviewerExpertise(int sessionPanelId, int userId);
        /// <summary>
        /// Retrieves PI information with list of parters.
        /// <param name="applicationId">Application identifier</param>
        /// <returns>ResultModel of IApplicationPIInformation models</returns>
        /// </summary>
        ResultModel<IApplicationPIInformation> ApplicationPIInformation(int applicationId);
        /// <summary>
        /// Retrieves an application's abstract text from the database.
        /// <param name="applicationId">Application identifier</param>
        /// <returns>ResultModel of IApplicationAbstractDocumentModel models</returns>
        /// </summary>
        ResultModel<IApplicationAbstractDocumentModel> ApplicationAbstract(int applicationId);

        /// <summary>
        /// Gets the panel stage information.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        IPanelStageResultModel GetPanelStageInformation(int sessionPanelId);
        /// <summary>
        /// Is application's abstract text from the database.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <returns>bool, true if abstract in database, false otherwise</returns>
        /// </Summary>
        bool IsAbstractInDatabase(int applicationId);
        /// <summary>
        /// Retrieves the reviewer evaluation provided by the current user for this panel.
        /// <param name="sessionPanelId">Session Panel identifier</param>
        /// <param name="curUserId">current user identifier</param>
        /// <returns>ResultModel of IReviewerEvaluation models</returns>
        /// </summary>
        ResultModel<IReviewerEvaluation> ListUserPanelReviewerEvaluations(int sessionPanelId, int curUserId);
        /// <summary>
        /// Retrieves the rating guidance by which the evaluator will rate the reviewers.
        /// <param name="panelId">panel identifier</param>
        /// <returns>Container of IRatingGuidance models</returns>
        /// </summary>
        ResultModel<IRatingGuidance> ListReviewerRatingGuidance(int panelId);
        /// <summary>
        /// Retrieves reviewer assignment information for the indicated assignement and application.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="panelUserAssignmentId">Panel User Assignment identifier</param>
        /// <param name="applicationId">Application identifier</param>
        /// <returns> IPanelReviewerApplicationAssignmentInformation</returns>        
        IPanelReviewerApplicationAssignmentInformation GetCurrentPanelReviewerApplicationAssignmentInformation(int panelUserAssignmentId, int applicationId);

        /// <summary>
        /// Removes the application from panel.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void RemoveApplicationFromPanel(int panelApplicationId, int userId);

        #endregion
        #region Order of Review Services
        /// <summary>
        /// Retrieves the panel's applications to display their review order.
        /// <param name="currentPanelId">SessionPanel identifier</param>
        /// <returns>ResultModel of IOrderOfReview models</returns>
        /// </summary>
        ResultModel<IOrderOfReview> ListOrderOfReview(int currentPanelId);
        #endregion
        #region Communication Services
        /// <summary>
        /// Retrieves lists of panel administrator names and Email addresses
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <returns>ResultModel of IPanelAdministrators models</returns>
        ResultModel<IPanelAdministrators> GetPanelAdministrators(int sessionPanelId);
        #endregion
        #region Dropdown Services
        /// <summary>
        /// Retrieves the list of applications types for the specified session panel.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>ResultModel of IAssignmentTypeDropdownList</returns>        
        ResultModel<IAssignmentTypeDropdownList> GetPanelSessionAssignmentTypeList(int sessionPanelId);
        /// <summary>
        /// Retrieves the list of Coi types for the specified session panel.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>ResultModel of IClientCoiDropdownList</returns>        
        ResultModel<IClientCoiDropdownList> GetPanelSessionCoiTypeList(int sessionPanelId);
        /// <summary>
        /// Retrieves the list of expertise types for the specified session panel.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Enumerable collection of ClientExpertiseRatingDropdownList</returns>        
        ResultModel<IClientExpertiseRatingDropdownList> GetPanelSessionClientExpertiseRatingList(int sessionPanelId);

        #endregion
        #region Application Release Services
        /// <summary>
        /// Retrieves a list of ApplicationReleasableStatus objects 
        /// which indicate whether or not the application is ready for release to the reviewers
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>ResultModel<IApplicationReleasableStatus> collection of ApplicationReleasableStatus objects indicating what applications are ready for release to reviewers</returns>
        ResultModel<IApplicationReleasableStatus> AreApplicationsReadyForRelease(int sessionPanelId);
        #endregion
        #region Critique Management Services
        /// <summary>
        /// Retrieves an application's critique information
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>ApplicationCritiqueModel containing information about Reviewer Critiques for a single application</returns>
        IApplicationCritiqueModel GetApplicationCritiques(int panelApplicationId);

        /// <summary>
        /// Gets the application critiques for  a panel.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>ApplicationCritiqueModel collection containing information about Reviewer Critiques for a panel</returns>
        IEnumerable<IApplicationCritiqueModel> GetApplicationCritiquesForPanel(int sessionPanelId);

        /// <summary>
        /// Retrieves contents of the reviewers critique
        /// </summary>
        /// <param name="applicationWorkflowStepId">ApplicationWorkflowStep identifier</param>
        /// <returns>IMeetingCritiqueModel containing the current contents of the reviewer's critique</returns>
        IApplicationCritiqueDetailsModel GetApplicationCritiqueDetails(int applicationWorkflowStepId);
        /// <summary>
        /// Retrieves lists of application information for the specified panel.
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <returns>Container of IApplicationInformationModel models</returns>
        /// </summary>
        ResultModel<IApplicationInformationModel> ListApplicationInformation(int sessionPanelId, int panelApplicationId);
        #endregion
        #region Reviewer Search Services
        /// <summary>
        /// Searches for reviewers.
        /// </summary>
        /// <param name="searchModel">The search criteria model.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <returns>Collection of search results matching search criteria</returns>
        ResultModel<IReviewerSearchResultModel> SearchForReviewers(ISearchForReviewersModel searchModel, int panelId);
		/// <summary>
        /// Searches for staffs.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <returns></returns>
        ResultModel<IStaffSearchResultModel> SearchForStaffs(ISearchForStaffsModel searchModel, int panelId);
        #endregion
        #region Reviewer Services
        /// <summary>
        /// Setups the new registration for a specified panel user assignment id.
        /// </summary>
        /// <param name="panelUserAssignmentId">The new assignment identifier.</param>
        /// <param name="userId">The user identifier for the user making the change.</param>
        void SetupNewRegistration(int panelUserAssignmentId, int userId);
        /// <summary>
        /// Removes the user from panel.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void RemoveUserFromPanel(int panelUserAssignmentId, int userId);
        /// <summary>
        /// Gets the participant type of the supplied participant information.
        /// </summary>
        /// <param name="assignedUserId">The user identifier to be assigned.</param>
        /// <param name="sessionPanelId">The session panel identifier to be assigned.</param>
        /// <returns>The mapped participant type</returns>
        ClientParticipantType GetMappedParticipantType(int assignedUserId, int sessionPanelId);
        /// <summary>
        /// get applications by param
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="fiscalYear"></param>
        /// <param name="programYearId"></param>
        /// <param name="panelId"></param>
        /// <param name="receiptCycle"></param>
        /// <param name="awardId"></param>
        /// <param name="logNumber"></param>
        /// <returns></returns>
        ResultModel<IApplicationsManagementModel> GetApplications(int? clientId, string fiscalYear, int? programYearId, int? panelId, int? receiptCycle, int? awardId, string logNumber, int userId);

        #endregion
    }
}
