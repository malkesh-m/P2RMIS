using System.Collections.Generic;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.CrossCuttingServices.Models;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for AvailableApplications objects.  Provides access to a database view
    /// and associated functions.
    /// </summary>
    public interface ISummaryManagementRepository
    {
        /// <summary>
        /// Retrieves the applications that are available for summary statement processing based on the user specified
        /// parameters.
        /// </summary>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <returns>Zero or more Applications</returns>
        ResultModel<IAvailableApplications> GetSummaryApplications(int panelId, int cycle, int? awardTypeId);
        /// <summary>
        /// Gets the summary statement applications.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns></returns>
        ResultModel<ISummaryStatementApplicationModel> GetSummaryStatementApplications(int programId,
            int yearId, int? cycle, int? panelId, int? awardTypeId);
        /// <summary>
        /// Retrieves the phase counts for the summary statements currently being processed
        /// </summary>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <param name="userId">the selected user id (optional)</param>
        /// <returns>Zero or more phase counts that are currently being processed</returns>
        ResultModel<IPhaseCountModel> GetPhaseCounts(int panelId, int cycle, int? awardTypeId, int? userId);
        /// <summary>
        /// Retrieves panel summaries for applications that have started a workflow.
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award Type (mechanism) identifier (optional)</param>
        /// <param name="userId">User identifier (optional)</param>
        /// <returns>Zero or more panel summaries</returns>
        ResultModel<ISummaryGroup> GetPanelSummaries(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId, int? userId);
        /// <summary>
        /// Retrieves panel summaries for all applications
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <returns>Zero or more panel summaries</returns>
        ResultModel<ISummaryGroup> GetAllPanelSummaries(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId);
        /// <summary>
        /// Retrieves panel summaries for all applications by phase
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award Type (mechanism) identifier (optional)</param>
        /// <returns>Zero or more panel summaries</returns>
        ResultModel<ISummaryGroup> GetPanelSummariesPhases(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId);
        /// <summary>
        /// Retrieves workflow information for application that user is assigned to one or more child steps.
        /// </summary>
        /// <param name="userId">Identifier for current user</param>
        /// <returns>Zero or more assigned application workflows</returns>
        ResultModel<ISummaryAssignedModel> GetAssignedSummaries(int userId);
        /// <summary>
        /// Retrieves information about workflow progress for each application that a user is assigned.
        /// </summary>
        /// <param name="userId">Identifier for current user</param>
        /// <returns>Zero or more workflow steps progress for an assigned application</returns>
        ResultModel<IWorkflowProgress> GetWorkflowProgress(int userId);
        /// <summary>
        /// Retrieves information about the application a workflow is for.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <returns>Application details for a single application</returns>
        IApplicationDetailModel GetApplicationDetail(int applicationWorkflowId);
        /// <summary>
        /// Retrieves information about an application.
        /// </summary>
        /// <param name="panelApplicationId">Panel Application identifier</param>
        /// <returns>Application details for a single application</returns>
        IApplicationDetailModel GetPreviewApplicationInfoDetail(int panelApplicationId);
        /// <summary>
        /// Retrieves the details of an application workflow step including content and element metadata.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <returns>Zero or more application workflow step elements</returns>
        ResultModel<IStepContentModel> GetApplicationStepContent(int applicationWorkflowId);
        /// <summary>
        /// Retrieves the details of an application workflow before it has been started.
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>Zero or more preview application workflow step elements</returns>
        ResultModel<IStepContentModel> GetPreviewApplicationStepContent(int panelApplicationId);
        /// <summary>
        /// Retrieves the transaction history for a specified application's workflow.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <returns>Zero or more transactions of an application workflow</returns>
        ResultModel<IWorkflowTransactionModel> GetWorkflowTransactionHistory(int applicationWorkflowId);
        /// <summary>
        /// Retrieves the completed applications.
        /// </summary>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <param name="userId">the selected user id (optional)</param>
        /// <returns>Zero or more Applications</returns>
        ResultModel<IApplicationsProgress> GetCompletedProgressApplications(int panelId, int cycle, int? awardTypeId, int? userId);
        /// <summary>
        /// Gets the completed applications.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        ResultModel<IApplicationsProgress> GetCompletedApplications(int programId, int yearId, int? cycle, int? panelId, int? awardTypeId, int? userId);
        /// <summary>
        /// Retrieves panel summaries for all closed applications
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award Type (mechanism) identifier (optional)</param>
        /// <returns>Zero or more panel summaries</returns>
        ResultModel<ISummaryGroup> GetAllCompletePanelSummaries(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId);
        /// <summary>
        /// Retrieves possible file types for a specified client
        /// </summary>
        /// <param name="applicationId">Unique identifier for an application</param>
        /// <returns>Possible file types a client may use</returns>
        ResultModel<IApplicationFileModel> GetClientFileTypes(int applicationId);
        /// <summary>
        /// retrieves the name of the checked out user
        /// </summary>
        /// <param name="AppWorkflowStepId">the applications workflow id</param>
        /// <returns>integer of the active workflow step</returns>
        UserName GetSummaryCheckedOutUserName(int AppWorkflowStepId);
        /// <summary>
        /// Retrieves panel summaries for all closed applications that were processed using P2RMIS-2.
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award Type (mechanism) identifier (optional)</param>
        /// <param name="userId">User identifier (optional)</param>
        /// <returns>Zero or more panel summaries</returns>
        /// 

        /// <summary>
        /// Retrieve a list of summaries available for checkout by the current user.
        /// <param name="UserId">the current user</param>
        /// <paramref name="program">program filter value</paramref>/>
        /// <param name="fiscalYear">Fiscal year filter value</param>
        /// <param name="cycleId">Cycle filter value id</param>
        /// <param name="panelId">Panel filter value id</param>
        /// <param name="awardId">Award filter value id</param>
        /// <param name="canAccessAdminNote">if set to <c>true</c> [can access admin note].</param>
        /// <param name="canAccessDiscussionNote">if set to <c>true</c> [can access discussion note].</param>
        /// <param name="canAccessGeneralNote">if set to <c>true</c> [can access general note].</param>
        /// <param name="canAccessUnassignedReviewerNote">if set to <c>true</c> [can access reviewer note].</param>
        /// <returns>Enumerable list of available summaries.</returns>
        ResultModel<ISummaryAssignedModel> GetDraftSummmariesAvailableForCheckout(int userId, int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId,
            bool canAccessDiscussionNote, bool canAccessGeneralNote, bool canAccessUnassignedReviewerNote);
        /// <summary>
        /// Gets the draft summmaries checkedout.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="canAccessDiscussionNote">if set to <c>true</c> [can access discussion note].</param>
        /// <param name="canAccessGeneralNote">if set to <c>true</c> [can access general note].</param>
        /// <param name="canAccessUnassignedReviewerNote">if set to <c>true</c> [can access reviewer note].</param>
        /// 
        /// <returns></returns>
        ResultModel<ISummaryAssignedModel> GetDraftSummmariesCheckedout(int userId, bool canAccessDiscussionNote, bool canAccessGeneralNote, bool canAccessUnassignedReviewerNote);
        /// <summary>
        /// Retrieve the assigned user for the current phase of a list of applications
        /// </summary>
        /// <param name="applicationIds">List of application ids to find the user assigned (if any)</param>
        /// <returns>Enumerable list of applications and their assigned user for the current phase</returns>
        /// <remarks>This is set up to accept applicationIds, if appWorkflow or Step ids work better it is an easy change</remarks>
        ResultModel<IUserApplicationModel> GetAssignedUsers(ICollection<int> applicationIds);
        /// <summary>
        /// Retrieve the user information based on a partial string to search
        /// </summary>
        /// <param name="searchString">Partial string to search within all user's name</param>
        /// <param name="clientCollection">List of client ids the currently logged in user is assigned to</param>
        /// <returns>Enumerable list of applications and their assigned user for the current phase</returns>
        ResultModel<IUserModel> GetAutoCompleteUsers(string searchString, ICollection<int> clientCollection);
        #region Summary Comments
        /// <summary>
        /// Retrieves the summary comments associated with the application specified by the input appId
        /// parameters.
        /// </summary>
        /// <param name="panelApplicationId">the panel application identifier</param>
        /// <returns>Zero or more Summary comments for this Application</returns>
        ResultModel<ISummaryCommentModel> GetApplicationSummaryComments(int panelApplicationId);
        /// <summary>
        /// Gets available workflows for a specific client and review stage.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="reviewStageId">ReviewStage entity identifier</param>
        /// <returns>List of values for available workflows</returns>
        ResultModel<IMenuItem> GetClientWorkflowsByStage(int clientId, int reviewStageId);
        #endregion

        /// <summary>
        /// Retrieve the user name of users assigned to summary applications based on currently selected search criteria
        /// </summary>
        /// <param name="collection">Collection of user's assigned clients</param>
        /// <param name="program">Research program abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of research program</param>
        /// <param name="cycleId">Receipt cycle in which mechanism was offered (optional)</param>
        /// <param name="panelId">Unique panel identifier (optional)</param>
        /// <param name="awardId">Unique award identifier (optional)</param>
        /// <param name="reviewerName">Partial string to search within user's name</param>
        /// <returns>Enumerable list of user information</returns>
        ResultModel<IUserModel> GetSearchReviewersList(ICollection<int> collection, int program, int fiscalYear, int? cycleId, int? panelId, int? awardId, string reviewerName);

        /// <summary>
        /// Retrieve a matrix representation of awards (y-axis) and priorities (x-axis) and (1)assigned or (2)defaulted workflowIds for each pair 
        /// </summary>
        /// <param name="program">Program abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of program offering</param>
        /// <returns>Enumerable list of awards and their assigned or defaulted workflow</returns>
        IEnumerable<IAwardWorkflowPriorityModel> GetWorkflowAssignmentOrDefault(int program, int fiscalYear, int? cycle);

        /// <summary>
        /// Retrieve workflow information for all workflows belonging to a client
        /// </summary>
        /// <param name="program">Program abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of program offering</param>
        /// <returns>Enumerable list of workflows</returns>
        IEnumerable<IWorkflowTemplateModel> GetClientWorkflowAll(int program, int fiscalYear);
        /// <summary>
        /// Gets the request review applications.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns></returns>
        ResultModel<ISummaryStatementRequestReview> GetRequestReviewApplications(int programId, int yearId, int? cycle, int? panelId, int? awardTypeId);
        /// <summary>
        /// Determines whether a summary statement is checked out
        /// </summary>
        /// <param name="applicationWorkflowId">the application workflow id</param>
        /// <returns>boolean true or false</returns>
        bool IsSsCheckedOut(int applicationWorkflowId);
        /// <summary>
        /// Retrieves the descriptive information describing the abstract file
        /// </summary>
        /// <param name="applicationId">Application entity identifier</param>
        /// <returns>Enumeration of ApplicationFileModels (should only be one)</returns>
        IEnumerable<ApplicationFileModel> GetClientAbstractFileType(int applicationId);

        /// <summary>
        /// Gets the summary data.
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <returns></returns>
        ISummaryDocumentModel GetSummaryDocumentData(int applicationWorkflowId, int panelApplicationId,
            string procedureName);
        /// <summary>
        /// Gets the summary preview document data.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns>SummaryDocumentModel containing data needed to populate a summary statement</returns>
        ISummaryDocumentModel GetSummaryPreviewDocumentData(int panelApplicationId);

        /// <summary>
        /// Gets the summary statement applications currently in progress.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns>Web model containing SummaryStatement information related to its progress in the workflow</returns>
        ResultModel<ISummaryStatementProgressModel> GetSummaryStatementApplicationsInProgress(int programId,
            int yearId, int? cycle, int? panelId, int? awardTypeId);
    }
}
