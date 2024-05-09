using System.Collections.Generic;
using System.IO;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Files;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.Reports;
using Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.Bll.Views.ApplicationDetails;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    /// <summary>
    /// Services provided by SummaryManagemenService:
    ///  - GetSummaryApplications - retrieves the requested available applications
    ///  - GetProgressApplications - retrieves the requested applications that are currently generating summary statements
    /// </summary>
    public interface ISummaryManagementService
    {
        /// <summary>
        /// Retrieves the applications that are available for summary statement processing based on the user specified
        /// parameters.
        /// </summary>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <returns>Container containing zero or more Applications</returns>
        Container<IAvailableApplications> GetSummaryApplications(int panelId, int cycle, int? awardTypeId);
        /// <summary>
        /// Gets the summary statement applications.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns></returns>
        Container<ISummaryStatementApplicationModel> GetSummaryStatementApplications(int programId,
            int yearId, int? cycle, int? panelId, int? awardTypeId);
        /// <summary>
        /// Retrieves the applications that are available for summary statement processing based on the user specified
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award Type (mechanism) identifier (optional)</param>
        /// <returns>Summary group object representing all of the panels for the specified panels</returns>
        Container<ISummaryGroup> GetAllPanelSummaries(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId); 
        /// <summary>
        /// Retrieves the applications whose summary statements are currently being processed.
        /// parameters.
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award Type (mechanism) identifier (optional)</param>
        /// <param name="userId">User identifier (optional)</param>
        /// <returns>Zero or more Applications</returns>
        Container<ISummaryGroup> GetPanelSummaries(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId, int? userId);
        /// <summary>
        /// Retrieves the grouping for the applications whose summary statements are currently being processed by phase.
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award Type (mechanism) identifier (optional)</param>
        /// <returns>Zero or more Applications</returns>
        Container<ISummaryGroup> GetPanelSummariesPhases(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId);
        /// <summary>
        /// Retrieves the phase counts for the summary statements currently being processed
        /// </summary>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <param name="userId">the selected user id (optional)</param>
        /// <returns>Zero or more phase counts that are currently being processed</returns>
        Container<IPhaseCountModel> GetPhaseCounts(int panelId, int cycle, int? awardTypeId, int? userId);
        /// <summary>
        /// Retrieves the transaction history for a specified application's workflow.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <returns>Zero or more transactions of an application workflow</returns>
        Container<IWorkflowTransactionModel> GetWorkflowTransactionHistory(int applicationWorkflowId);
        /// <summary>
        /// Pushes the selected application(s) to the processing queue.
        /// </summary>
        /// <param name="collection">Collection of application specifications tos start.</param>
        /// <param name="thePanelManagementService">The panel management list service.</param>
        IServiceState StartApplications(ICollection<ChangeToSave> collection, IPanelManagementService thePanelManagementService);
        /// <summary>
        /// Retrieves the completed applications.
        /// </summary>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <param name="userId">the selected user id (optional)</param>
        /// <returns>Zero or more Applications that are currently being processed</returns>
        Container<IApplicationsProgress> GetCompletedProgressApplications(int panelId, int cycle, int? awardTypeId, int? userId);
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
        Container<IApplicationsProgress> GetCompletedApplications(int programId, int yearId, int? cycle, int? panelId, int? awardTypeId, int? userId);
        /// <summary>
        /// Retrieves panel summaries for all closed applications that were processed using P2RMIS-2.
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award Type (mechanism) identifier (optional)</param>
        /// <returns>Container holding a list of SummaryGroups matching the user specified parameter</returns>
        Container<ISummaryGroup> GetAllCompletePanelSummaries(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId);
        /// <param name="collection">Requesting user's client list</param>
        /// <param name="program">Program filter value</param>
        /// <param name="fiscalYear">Fiscal year filter value</param>
        /// <param name="cycleId">Cycle filter value id</param>
        /// <param name="panelId">Panel filter value id</param>
        /// <param name="awardId">Award filter value id</param>
        /// <param name="reviewerName">String to search</param>
        /// <returns>Container of matching reviewer namesL</returns>
        Container<IUserModel> GetSearchReviewersList(ICollection<int> collection, int program, int fiscalYear, int? cycleId, int? panelId, int? awardId, string reviewerName);
        /// <summary>
        /// Retrieves information about the application.
        /// </summary>
        /// <param name="panelApplicationId">Identifier for a panel application</param>
        /// <returns>Application details for a single application</returns>
        IApplicationDetailModel GetPreviewApplicationInfoDetail(int panelApplicationId);
        /// <summary>
        /// Retrieves the details of an application workflow step including content and element metadata.
        /// </summary>
        /// <param name="panelApplicationId">Identifier for a panel application</param>
        /// <returns>Zero or more application workflow step elements</returns>
        Container<IStepContentModel> GetPreviewApplicationStepContent(int panelApplicationId);
        /// <summary>
        /// query to name of the user who checked out the summary
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for application workflow Step Id</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>name of the user who checked out the summary statement</returns>
        /// <exception cref="ArgumentException">Exception thrown if invalid application workflow Id detected</exception>
        UserName GetSummaryCheckedOutUserName(int AppWorkflowStepId);
        /// <summary>
        /// Retrieves the application id associated with the log number
        /// </summary>
        /// <param name="logNum">application logNumber Identifier</param>
        /// <returns>associated applicationId or null if none</returns>
        /// <summary>
        /// Retrieves the applicationid associated with the input paramter logNumber.
        /// </summary>
        /// <param name="logNumber">LogNumber identifier for an application</param>
        /// <returns>the associated application id</returns>
        //int? GetApplicationIdByLogNumber(string logNumber);

        #region Summary Comments
        /// <summary>
        /// Retrieves this application's Summary comments.
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>Summary comments for  single application</returns>
        Container<ISummaryCommentModel> GetApplicationSummaryComments(int panelApplicationId);
        /// <summary>
        /// Gets the user application comments.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="canAccessAdminNote">if set to <c>true</c> [can access admin note].</param>
        /// <param name="canAccessDiscussionNote">if set to <c>true</c> [can access discussion note].</param>
        /// <param name="canAccessGeneralNote">if set to <c>true</c> [can access general note].</param>
        /// <param name="canAccessUnassignedReviewerNote">if set to <c>true</c> [can access unassigned reviewer note].</param>
        /// <returns></returns>
        List<IUserApplicationCommentFacts> GetUserApplicationComments(int panelApplicationId, bool canAccessAdminNote, bool canAccessDiscussionNote,
                bool canAccessGeneralNote, bool canAccessUnassignedReviewerNote);
        /// <summary>
        /// Gets available workflows
        /// </summary>
        /// <param name="clientProgramId">ClientProgram entity identifier</param>
        /// <returns>list of values for available workflows</returns>
        Container<IMenuItem> GetClientSummaryStatementWorkflows(int clientProgramId);
        /// <summary>
        /// Adds an application summary comment.
        /// </summary>
        /// <param name="comment">the text of the comment</param>
        /// <param name="applicationId">the id of the associated application</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">the id of the user adding the comment</param>
        /// <returns></returns>
        void AddApplicationSummaryComment(string comment, string applicationId, int panelApplicationId, int userId);
        /// <summary>
        /// Edits an application summary comment.
        /// </summary>
        /// <param name="comment">the text of the comment</param>
        /// <param name="userApplicationCommentId">the id of the associated application</param>
        /// <param name="userId">the id of the user adding the comment</param>
        /// <returns></returns>
        void EditApplicationSummaryComment(string comment, int userApplicationCommentId, int userId);
        /// <summary>
        /// Deletes an application summary comment.
        /// </summary>
        /// <param name="userApplicationCommentId">the id of the associated application</param>
        /// <param name="userId">the id of the user adding the comment</param>
        /// <returns></returns>
        void DeleteApplicationSummaryComment(int userApplicationCommentId, int userId);
        #endregion
        /// <summary>
        /// Saves changes (priority; workflow and templates) for one or Applications.
        /// </summary>
        /// <param name="theWorkflowService">The WorkflowService </param>
        /// <param name="collection">Collection of changes to save</param>
        //void SaveChanges(ICollection<ChangeToSaveApplication> collection);
        /// <summary>
        /// TODO:: rdl document me
        /// </summary>
        /// <param name="collection">Collection of changes to save</param>
        //void SaveChanges(ICollection<ChangeToSave> collection);
        /// <summary>
        /// Retrieve the assigned user for the current phase for one or more applications.
        /// </summary>
        /// <param name="applicationIds">Collection of application identifiers</param>
        /// <returns>Container of applications & their assigned users.</returns>
        Container<IUserApplicationModel> GetAssignedUsers(ICollection<int> applicationIds);
        /// <summary>
        /// Retrieve the user information based on a partial string to search
        /// </summary>
        /// <param name="program">Program abbreviation</param>
        /// <param name="fiscalYear">Fiscal Year</param>
        /// <returns>Container holding zero or more workflow assignments</returns>
        Container<IUserModel> GetAutoCompleteUsers(string searchString, ICollection<int> clientCollection);
        /// <summary>
        /// Retrieve a matrix representation of awards (y-axis) and priorities (x-axis) and (1)assigned or (2)defaulted workflowIds for each pair 
        /// </summary>
        /// <param name="program">Program abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of program offering</param>
        /// <returns>Enumerable list of awards and their assigned or defaulted workflow</returns>
        Container<IAwardWorkflowPriorityModel> GetWorkflowAssignmentOrDefault(int program, int fiscalYear, int? cycle);
        /// <summary>
        /// Retrieve workflow information for all workflows belonging to a client
        /// </summary>
        /// <param name="program">Program abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of program offering</param>
        /// <returns>Enumerable list of workflows</returns>
        Container<IWorkflowTemplateModel> GetClientWorkflowAll(int program, int fiscalYear);
        /// <summary>
        /// Save or updates as appropriate the workflow assigned to a mechanism.
        /// </summary>
        /// <param name="collection">Collection of mechanismId/workflowId pairs</param>
        bool AssignWorkflow(ICollection<AssignWorkflowToSave> collection, int userId);
        /// <summary>
        /// Determines whether a summary statement is checked out
        /// </summary>
        /// <param name="applicationWorkflowId">the application workflow id</param>
        /// <returns>Boolean true or false</returns>
        bool IsSsCheckedOut(int applicationWorkflowId);
        /// <summary>
        /// Determines whether [is ss web based] [the specified client program identifier].
        /// </summary>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is ss web based] [the specified client program identifier]; otherwise, <c>false</c>.
        /// </returns>
        bool IsSsWebBased(int clientProgramId);
        /// <summary>
        /// Gets the program mechanism summary statement.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        Container<IProgramMechanismSummaryStatementModel> GetProgramMechanismSummaryStatement(int panelApplicationId, int programMechanismId);
        /// <summary>
        /// Retrieves application report information for the indicated application ids
        /// </summary>
        /// <param name="panelApplicationIds">The panel applications to retrieve the corresponding report information</param>
        /// <returns>application report information as a list of ReportAppInfo objects</returns>
        IEnumerable<IReportAppInfo> GetAppReportInfo(int[] panelApplicationIds);
        /// <summary>
        /// Creates the summary documents.
        /// </summary>
        /// <param name="collections">The collections.</param>
        /// <param name="thePanelManagementService">The panel management list service.</param>
        void CreateSummaryDocuments(ICollection<ChangeToSave> collections, IPanelManagementService thePanelManagementService);
        /// <summary>
        /// Creates the summary document.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="templateLocation">The template location.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        ServiceState CreateSummaryDocument(int panelApplicationId, string storedProcedureName, string templateLocation, int userId);
        /// <summary>
        /// Gets the summary document file.
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        KeyValuePair<ISummaryDocumentFileModel, IServiceState> GetSummaryDocumentFile(int panelApplicationId);
        /// <summary>
        /// Gets the started summary document file.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns></returns>
        KeyValuePair<ISummaryDocumentFileModel, IServiceState> GetStartedSummaryDocumentFile(int panelApplicationId);
        /// <summary>
        /// Gets the unstarted summary document file.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns></returns>
        KeyValuePair<ISummaryDocumentFileModel, IServiceState> GetUnstartedSummaryDocumentFile(int panelApplicationId);
        /// <summary>
        /// Generates the final summary documents.
        /// </summary>
        /// <param name="panelApplicationIds">The panel application ids to be generated.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Zipped collection of summary document files
        /// </returns>
        string GenerateFinalSummaryDocuments(int[] panelApplicationIds, int userId);

        /// <summary>
        /// Gets the summary document backup file.
        /// </summary>
        /// <param name="applicationWorkflowStepWorkLogId">The application workflow step work log identifier.</param>
        /// <param name="action">The action.</param>
        /// <returns>FileResult model</returns>
        IFileResultModel GetSummaryDocumentBackupFile(int applicationWorkflowStepWorkLogId, string action);

        /// <summary>
        /// Processes the summary statement upload.
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="inputData">The input data for the file to process.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>failure message if unsuccessful</returns>
        ServiceState ProcessSummaryStatementUpload(int applicationWorkflowId, int targetApplicationWorkflowStepId, byte[] inputData, int userId);
        /// <summary>
        /// Gets the summary statement applications in progress within a summary workflow.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns>Container of summary statement progress information</returns>
        Container<ISummaryStatementProgressModel> GetSummaryStatementApplicationsInProgress(int programId,
            int yearId, int? cycle, int? panelId, int? awardTypeId);
    }
}
