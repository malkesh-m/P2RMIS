using System;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.WebModels.SummaryStatement;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Bll.PanelManagement
{
    /// <summary>
    /// PanelManagementService provides services to return collections of model
    /// data specific to the PanelManagement Application.
    /// </summary>
    public partial interface IPanelManagementService
    {
        /// <summary>
        /// Retrieves lists of application information for the specified panel.

        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <returns>Container of IApplicationInformationModel models</returns>
        /// </summary>
        Container<IApplicationInformationModel> ListApplicationInformation(int sessionPanelId);
        /// <summary>
        /// Gets the applications by panel.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        List<KeyValuePair<int, string>> GetApplicationsByPanel(int sessionPanelId);
        /// <summary>
        /// Retrieves list of panel significations (FY, Panel Name andId, Application Id, Program Abbreviation and role).
        /// Returns an empty lit if the user is not an SRO
        /// <returns></returns>
        /// </summary>
        Container<IPanelSignificationsModel> ListPanelSignifications(int userId);
        /// <summary>
        /// Retrieves list of Program/Years for the specified user.
        /// <param name="userId">user identifier</param>
        /// <returns>Container of IProgramYearModel models</returns>
        /// </summary>
        /// <remarks>When this method is reused from another function (i.e. User Management, Criteria etc.) this and any other methods should be refactored to the CriteriaService.</remarks>
        Container<IProgramYearModel> ListProgramYears(int userId);
        /// <summary>
        /// Retrieves list of panel significations for the specific user and Program/Year.
        /// <param name="userId">user identifier</param>
        /// <param name="programYearId">ProgramYear identifier</param>
        /// <returns>Container of IPanelSignificationsModel models</returns>
        /// </summary>
        Container<IPanelSignificationsModel> ListPanelSignifications(int userId, int programYearId);
        /// <summary>
        /// Lists the panels with meeting types.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        IEnumerable<Tuple<int, string, string>> ListPanelsWithMeetingTypes(int userId, int programYearId);
        /// <summary>
        /// Retrieves list of personnels with conflict of interest.
        /// <param name="applicationId">the application identifier</param>
        /// <returns>Container of IPersonnelWithCoi models</returns>
        /// </summary>
        Container<IPersonnelWithCoi> ListPersonnelWithCoi(int applicationId);
        /// <summary>
        /// Retrieves list of panel names that the application could be transferred to.
        /// <param name="applicationId">Application identifier</param>
        /// <param name="currentPanelId">Panel identifier of the applications current panel</param>
        /// <returns>Container of ITransferPanelModel models</returns>
        /// </summary>
        Container<ITransferPanelModel> ListPanelNames(int applicationId, int currentPanelId);
        /// <summary>
        /// Retrieves list of panel names that the application could be transferred to.
        /// </summary>
        /// <param name="currentPanelId">Panel identifier of the applications current panel</param>
        /// <returns>Container of ITransferPanelModel models</returns>
        Container<ITransferPanelModel> ListSiblingPanelNames(int currentPanelId);
        /// <summary>
        /// Retrieves list of reviewer names that are associated with a session panel.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Container of IUserModel models</returns>
        Container<IUserModel> ListReviewerNames(int sessionPanelId);
        /// <summary>
        /// Retrieves list of reasons that the application could be transferred for.
        /// <returns>Container of IApplicationInformationModel models</returns>
        /// </summary>
        Container<IReasonModel> ListTransferReasons();
        /// <summary>
        /// Retrieves list of reviewer expertise.
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="userId">User identifier for the logged in user</param>
        /// <returns>Container of IReviewerExpertise models</returns>
        /// </summary>
        Container<IReviewerExpertise> ListReviewerExpertise(int sessionPanelId, int userId);
        /// <summary>
        /// Retrieves PI information with lists of documents and partners.
        /// <param name="applicationId">Application identifier</param>
        /// <returns>ResultModel of IApplicationPIInformation models containing PI information and lists of documents and partners</returns>
        /// </summary>
        Container<IApplicationPIInformation> ApplicationPIInformation(int applicationId);
        /// <summary>
        /// Retrieves PI information with lists of documents and partners with blinding.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <returns>ResultModel of IApplicationPIInformation models containing PI information and lists of documents and partners</returns>
        Container<IApplicationPIInformation> ApplicationPIInformationWithBlinding(int applicationId);
        /// <summary>
        /// Retrieves list of abstracts contained in the database for the specified application.
        /// <param name="applicationId">Application identifier</param>
        /// <returns>ResultModel of IApplicationAbstractDocumentModel models application abstracts</returns>
        /// </summary>  
        Container<IApplicationAbstractDocumentModel> ApplicationAbstract(int applicationId);
        /// <summary>
        /// IsAbstractInDatabase.
        /// <param name="appId">the application to determine if the abstract is in the database</param>
        /// <returns>true if the abstract is in the database, false otherwise.</returns>
        /// </summary>
        bool IsAbstractInDatabase(int appId);
        /// <summary>
        /// Create a log message and insert it into the ActionLog for an application transfer
        /// request between panel.
        /// </summary>
        /// <param name="message">Text of message to log</param>
        /// <param name="userId">User identifier</param>
        void LogApplicationTranferRequest(string message, int userId);
        /// <summary>
        /// Create a log message and insert it into the ActionLog for an application reviewer transfer
        /// request between panel.
        /// </summary>
        /// <param name="message">Text of message to log</param>
        /// <param name="userId">User identifier</param>
        void LogReviewerTranferRequest(string message, int userId);
        /// <summary>
        /// Retrieves the panel's applications to display their review order.
        /// <param name="sessionPanelId">Selected session panel identifier </param>
        /// <returns>Container of IOrderOfReview models</returns>
        /// </summary>
        Container<IOrderOfReview> ListOrderOfReview(int sessionPanelId);
        /// <summary>
        /// Retrieves the reviewer evaluation provided by the current user for this panel.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="sessionPanelId">Session Panel identifier</param>
        /// <param name="userId">current user identifier</param>
        /// <returns>Container of IReviewerEvaluation models</returns>
        Container<IReviewerEvaluation> ListUserPanelReviewerEvaluations(int sessionPanelId, int userId);
        /// <summary>
 	    /// Retrieves the rating guidance by which the evaluator will rate the reviewers.
 	    /// <param name="context">P2RMIS database context</param>
 	    /// <param name="panelId">panel identifier</param>
 	    /// <returns>Container of IRatingGuidance models</returns>
 	    /// </summary>
 	    Container<IRatingGuidance> ListReviewerRatingGuidance(int panelId);
        /// <summary>
        /// Retrieves lists of panel administrator names and Email addresses
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <returns>Container of IPanelAdministrators models</returns>
        Container<IPanelAdministrators> GetPanelAdministrators(int sessionPanelId);

        Container<IAssignmentTypeDropdownList> GetPanelSessionAssignmentTypeList(int sessionPanelId);

        Container<IClientCoiDropdownList> GetPanelSessionCoiTypeList(int sessionPanelId);

        Container<IClientExpertiseRatingDropdownList> GetPanelSessionClientExpertiseRatingList(int sessionPanelId);
        /// <summary>
        /// Retrieves lists of presentation order values
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>Container of webModelInterfaceType models</returns>
        Container<int> ListPresentationOrder(int panelApplicationId);
        /// <summary>
        /// Retrieves the data for the modal pop-up on the Panel Management Expertise/Assignments tab.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier for the reviewer</param>
        /// <returns>Container of webModelInterfaceType models</returns>
        Container<IExpertiseAssignments> GetExpertiseAssignments(int sessionPanelId, int panelApplicationId, int panelUserAssignmentId);
        /// <summary>
        /// Retrieves lists of critiques for the specified panel
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Container of webModelInterfaceType models</returns>
        PanelCritiqueSummaryModel ManageCritiques(int sessionPanelId);
        /// <summary>
        /// Gets application's reviewer and critique information
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>IApplicationCritiqueModel model</returns>
        IApplicationCritiqueModel GetApplicationCritiques(int panelApplicationId);
        /// <summary>
        /// Retrieves lists of application information for the specified panel.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="paneApplicationInformation">PanelApplication entity identifier</param>
        /// <returns>Container of IApplicationInformationModel models</returns>
        IApplicationInformationModel ListApplicationInformation(int sessionPanelId, int paneApplicationInformation);
        /// <summary>
        /// get existing application in panel
        /// </summary>
        /// <param name="programYearId"></param>
        /// <param name="receiptCycle"></param>
        /// <returns></returns>
        List<ReferralMappingModel> GetReferralMappingApplications(int programYearId, int receiptCycle);

        /// <summary>
        /// Retrieves the application information for the specified application.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="paneApplicationInformation">PanelApplication entity identifier</param>
        /// <returns>Container of IApplicationInformationModel models</returns>
        IApplicationInformationModel GetApplicationInformation(int paneApplicationId);
        /// <summary>
        /// Retrieves the application information for the specified application with blinding.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="paneApplicationInformation">PanelApplication entity identifier</param>
        /// <returns>Container of IApplicationInformationModel models</returns>
        IApplicationInformationModel GetApplicationInformationWithBlinding(int paneApplicationId);
        /// <summary>
        /// Retrieves list of Active Program/Years for the specified user.
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <returns>Container of IProgramYearModel models of active programs</returns>
        Container<IProgramYearModel> ListActiveProgramYears(int userId);
    /// <summary>
        /// Gets the assigned staffs.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        Container<IAssignedStaffModel> GetAssignedStaffs(int sessionPanelId);
        /// <summary>
        /// Gets the panel reviewers.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        Container<IPanelReviewerModel> GetPanelReviewers(int sessionPanelId);
        /// <summary>
        /// Gets the search results.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <returns>List of users fullfilling supplied search criteria</returns>
        Container<IReviewerSearchResultModel> GetSearchReviewerResults(ISearchForReviewersModel searchModel, int panelId);
        /// <summary>
        /// Gets the search staff results.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <returns></returns>
        Container<IStaffSearchResultModel> GetSearchStaffResults(ISearchForStaffsModel searchModel, int panelId);
        /// <summary>
        /// Gets the applications.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <returns></returns>
        List<KeyValuePair<int, string>> GetApplications(int programYearId, int cycle);
        /// <summary>
        /// Get referral mapping error message
        /// </summary>
        /// <param name="referrals">The referrals.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="userId">The logged-in user identifier.</param>
        /// <returns></returns>
        KeyValuePair<List<string>, int> SaveReferralMapping(List<KeyValuePair<string,string>> referrals, int programYearId, int receiptCycle, int userId);
        /// <summary>
        /// Validates the referral mapping.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <param name="sessionPanelIds">The session panel ids.</param>
        /// <returns></returns>
        List<string> ValidateReferralMapping(int referralMappingId, List<int> sessionPanelIds);
    }
}
