using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.CrossCuttingServices.Models;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Provides access to and methods returning AvailableApplications objects.
    /// </summary>
    public class SummaryManagementRepository:  GenericViewRepository<AvailableApplications>, ISummaryManagementRepository
    {
        #region Construction; Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public SummaryManagementRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion
        #region Constants
        /// <summary>
        /// The preview stored procedure name
        /// </summary>
        internal const string PreviewStoredProcedureName = "uspPreviewSummaryStatement";
        #endregion
        #region the services
        /// <summary>
        /// Retrieves the applications that are available for summary statement processing based on the user specified
        /// parameters.
        /// </summary>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <returns>Zero or more Applications</returns>
        public ResultModel<IAvailableApplications> GetSummaryApplications(int panelId, int cycle, int? awardTypeId)
        {
            ResultModel<IAvailableApplications> result = new ResultModel<IAvailableApplications>();
            result.ModelList = RepositoryHelpers.GetSummaryApplications(context, panelId, cycle, awardTypeId);

            return result;
        }
        /// <summary>
        /// Gets the summary statement applications.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns></returns>
        public ResultModel<ISummaryStatementApplicationModel> GetSummaryStatementApplications(int programId,
            int yearId, int? cycle, int? panelId, int? awardTypeId)
        {
            var result = new ResultModel<ISummaryStatementApplicationModel>();
            result.ModelList = RepositoryHelpers.GetSummaryStatementApplications(context, 
                programId, yearId, cycle, panelId, awardTypeId);            

            return result;
        }

        /// <summary>
        /// Gets the summary statement applications currently in progress.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns>Web model containing SummaryStatement information related to its progress in the workflow</returns>
        public ResultModel<ISummaryStatementProgressModel> GetSummaryStatementApplicationsInProgress(int programId,
            int yearId, int? cycle, int? panelId, int? awardTypeId)
        {
            var result = new ResultModel<ISummaryStatementProgressModel>();
            result.ModelList = RepositoryHelpers.GetSummaryStatementApplicationsInProgress(context,
                programId, yearId, cycle, panelId, awardTypeId);
            return result;
        }
        /// <summary>
        /// Retrieves the completed applications.
        /// </summary>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <param name="userId">the selected user id (optional)</param>
        /// <returns>Zero or more Applications</returns>
        public ResultModel<IApplicationsProgress> GetCompletedProgressApplications(int panelId, int cycle, int? awardTypeId, int? userId)
        {
            ResultModel<IApplicationsProgress> result = new ResultModel<IApplicationsProgress>();
            result.ModelList = RepositoryHelpers.GetCompletedProgressApplications(context, panelId, cycle, awardTypeId, userId);
            return result;
        }
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
        public ResultModel<IApplicationsProgress> GetCompletedApplications(int programId, int yearId, int? cycle, int? panelId, int? awardTypeId, int? userId)
        {
            ResultModel<IApplicationsProgress> result = new ResultModel<IApplicationsProgress>();
            result.ModelList = RepositoryHelpers.GetCompletedApplications(context, programId, yearId, panelId, cycle, awardTypeId, userId);
            return result;
        }
        /// <summary>
        /// Retrieves the phase counts for the summary statements currently being processed
        /// </summary>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <param name="userId">the selected user id (optional)</param>
        /// <returns>Zero or more phase counts that are currently being processed</returns>
        public ResultModel<IPhaseCountModel> GetPhaseCounts(int panelId, int cycle, int? awardTypeId, int? userId)
        {
            ResultModel<IPhaseCountModel> result = new ResultModel<IPhaseCountModel>();
            result.ModelList = RepositoryHelpers.GetPhaseCounts(context, panelId, cycle, awardTypeId, userId);
            return result;
        }
        /// <summary>
        /// Retrieves panel summaries for applications that have started a workflow.
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <returns>Zero or more panel summaries</returns>
        public ResultModel<ISummaryGroup> GetPanelSummaries(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId, int? userId)
        {
            ResultModel<ISummaryGroup> result = new ResultModel<ISummaryGroup>();
            result.ModelList = RepositoryHelpers.GetPanelSummaries(context, program, fiscalYear, cycle, panelId, awardTypeId, userId);
            return result;
        }
        /// <summary>
        /// Retrieves panel summaries for all applications
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award Type (mechanism) identifier (optional)</param>
        /// <returns>Zero or more panel summaries</returns>
        public ResultModel<ISummaryGroup> GetAllPanelSummaries(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId)
        {
            ResultModel<ISummaryGroup> result = new ResultModel<ISummaryGroup>();
            result.ModelList = RepositoryHelpers.GetPanelSummaryAll(context, program, fiscalYear, cycle, panelId, awardTypeId);
            return result;
        }
        /// <summary>
        /// Retrieves panel summaries for all applications by phase
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award Type (mechanism) identifier (optional)</param>
        /// <returns>Zero or more panel summaries</returns>
        public ResultModel<ISummaryGroup> GetPanelSummariesPhases(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId)
        {
            ResultModel<ISummaryGroup> result = new ResultModel<ISummaryGroup>();
            result.ModelList = RepositoryHelpers.GetPanelSummariesPhases(context, program, fiscalYear, cycle, panelId, awardTypeId);
            return result;
        }
        /// <summary>
        /// Retrieves workflow information for application that user is assigned to one or more child steps.
        /// </summary>
        /// <param name="userId">Identifier for current user</param>
        /// <returns>Zero or more assigned application workflows</returns>
        public ResultModel<ISummaryAssignedModel> GetAssignedSummaries(int userId)
        {
            ResultModel<ISummaryAssignedModel> result = new ResultModel<ISummaryAssignedModel>();
            result.ModelList = RepositoryHelpers.GetAssignedSummaries(context, userId);
            return result;
        }
        /// <summary>
        /// Retrieves information about workflow progress for each application that a user is assigned.
        /// </summary>
        /// <param name="userId">Identifier for current user</param>
        /// <returns>Zero or more workflow steps progress for an assigned application</returns>
        public ResultModel<IWorkflowProgress> GetWorkflowProgress(int userId)
        {
            ResultModel<IWorkflowProgress> result = new ResultModel<IWorkflowProgress>();
            result.ModelList = RepositoryHelpers.GetWorkflowProgress(context, userId);
            return result;
        }
        /// <summary>
        /// Retrieves information about the application a workflow is for.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <returns>Application details for a single application</returns>
        public IApplicationDetailModel GetApplicationDetail(int applicationWorkflowId)
        {
            IApplicationDetailModel result = new ApplicationDetailModel();
            result = RepositoryHelpers.GetApplicationDetail(context, applicationWorkflowId);
            return result;
        }
        /// <summary>
        /// Retrieves information about an application.
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>Application details for a single application</returns>
        public IApplicationDetailModel GetPreviewApplicationInfoDetail(int panelApplicationId)
        {
            IApplicationDetailModel result = new ApplicationDetailModel();
            result = RepositoryHelpers.GetPreviewApplicationInfoDetail(context, panelApplicationId);
            return result;
        }
        /// <summary>
        /// Retrieves the details of an application workflow step including content and element metadata.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <returns>Zero or more application workflow step elements</returns>
        public ResultModel<IStepContentModel> GetApplicationStepContent(int applicationWorkflowId)
        {
            ResultModel<IStepContentModel> result = new ResultModel<IStepContentModel>();
            result.ModelList = RepositoryHelpers.GetApplicationStepContent(context, applicationWorkflowId);
            return result;
        }
        /// <summary>
        /// Retrieves the details of an application workflow before it has been started.
        /// </summary>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>Zero or more preview application workflow step elements</returns>
        public ResultModel<IStepContentModel> GetPreviewApplicationStepContent(int panelApplicationId)
        {
            ResultModel<IStepContentModel> result = new ResultModel<IStepContentModel>();
            result.ModelList = RepositoryHelpers.GetPreviewApplicationStepContent(context, panelApplicationId);
            return result;
        }
        /// <summary>
        /// Retrieves the transaction history for a specified application's workflow.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <returns>Zero or more transactions of an application workflow</returns>
        public ResultModel<IWorkflowTransactionModel> GetWorkflowTransactionHistory(int applicationWorkflowId)
        {
            ResultModel<IWorkflowTransactionModel> result = new ResultModel<IWorkflowTransactionModel>();
            result.ModelList = RepositoryHelpers.GetWorkflowTransactionHistory(context, applicationWorkflowId);
            return result;
        }
        /// <summary>
        /// Retrieves panel summaries for all closed applications that were processed using P2RMIS-2.
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award Type (mechanism) identifier (optional)</param>
        /// <returns>Zero or more panel summaries</returns>
        public ResultModel<ISummaryGroup> GetAllCompletePanelSummaries(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId)
        {
            ResultModel<ISummaryGroup> result = new ResultModel<ISummaryGroup>();
            result.ModelList = RepositoryHelpers.GetAllCompletePanelSummaries(context, program, fiscalYear, cycle, panelId, awardTypeId);
            return result;
        }
        /// <summary>
        /// Retrieve the user name of users assigned to summary applications based on currently selected search criteria
        /// </summary>
        /// <param name="collection">Collection of user's assigned clients</param>
        /// <param name="program">Research program abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of research program</param>
        /// <param name="cycleId">Receipt cycle in which mechanism was offered (optional)</param>
        /// <param name="panelId">Unique panel identifier (optional)</param>
        /// <param name="awardId">Unique panel identifier (optional)</param>
        /// <param name="reviewerName">Partial string to search within user's name</param>
        /// <returns>Enumerable list of user information</returns>
        public ResultModel<IUserModel> GetSearchReviewersList(ICollection<int> collection, int program, int fiscalYear, int? cycleId, int? panelId, int? awardId, string reviewerName)
        {
            var result = new ResultModel<IUserModel>
            {
                ModelList =
                    RepositoryHelpers.GetAutoCompleteUsersAssignedActiveStep(context, reviewerName, program, fiscalYear,
                        panelId, cycleId, awardId)
            };
            return result;
        }
        /// <summary>
        /// query to name of the user who checked out the summary
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for application workflow Step Id</param>
        /// <returns>string of the user who checked out the summary statement</returns>
        public UserName GetSummaryCheckedOutUserName(int AppWorkflowStepId)
        {
            UserName userName = RepositoryHelpers.GetSummaryCheckedOutUserName(context, AppWorkflowStepId);
            return userName;
        }

        /// <summary>
        /// Retrieve a list of summaries available for checkout by the current user.
        /// <param name="userId">the current user</param>
        /// <paran name="program">program filter value</paran>
        /// <param name="fiscalYear">Fiscal year filter value</param>
        /// <param name="cycleId">Cycle filter value id</param>
        /// <param name="panelId">Panel filter value id</param>
        /// <param name="awardTypeId">Award filter value id</param>
        /// <param name="canAccessDiscussionNote">if set to <c>true</c> [can access discussion note].</param>
        /// <param name="canAccessGeneralNote">if set to <c>true</c> [can access general note].</param>
        /// <param name="canAccessUnassignedReviewerNote">if set to <c>true</c> [can access unassigned reviewer note].</param>
        /// <returns>Enumerable list of available summaries.</returns>
        public ResultModel<ISummaryAssignedModel> GetDraftSummmariesAvailableForCheckout(int userId, int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId,
            bool canAccessDiscussionNote, bool canAccessGeneralNote,
            bool canAccessUnassignedReviewerNote)
        {
            ResultModel<ISummaryAssignedModel> result = new ResultModel<ISummaryAssignedModel>();
            var commentTypes = new List<int>();
            if (canAccessDiscussionNote)
                commentTypes.Add(CommentType.Indexes.DiscussionNote);
            if (canAccessGeneralNote)
                commentTypes.Add(CommentType.Indexes.GeneralNote);
            if (canAccessUnassignedReviewerNote)
                commentTypes.Add(CommentType.Indexes.ReviewerNote);
            result.ModelList = RepositoryHelpers.GetDraftSummmariesAvailableForCheckout(context, userId, program, fiscalYear, cycle, panelId, awardTypeId, commentTypes);
            return result;
        }
        /// <summary>
        /// Gets the draft summmaries checkedout.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="canAccessDiscussionNote">if set to <c>true</c> [can access discussion note].</param>
        /// <param name="canAccessGeneralNote">if set to <c>true</c> [can access general note].</param>
        /// <param name="canAccessUnassignedReviewerNote">if set to <c>true</c> [can access unassigned reviewer note].</param>
        /// 
        /// <returns></returns>
        public ResultModel<ISummaryAssignedModel> GetDraftSummmariesCheckedout(int userId, bool canAccessDiscussionNote, bool canAccessGeneralNote,
            bool canAccessUnassignedReviewerNote)
        {
            ResultModel<ISummaryAssignedModel> result = new ResultModel<ISummaryAssignedModel>();
            var commentTypes = new List<int>();
            if (canAccessDiscussionNote)
                commentTypes.Add(CommentType.Indexes.DiscussionNote);
            if (canAccessGeneralNote)
                commentTypes.Add(CommentType.Indexes.GeneralNote);
            if (canAccessUnassignedReviewerNote)
                commentTypes.Add(CommentType.Indexes.ReviewerNote);
            result.ModelList = RepositoryHelpers.GetDraftSummmariesCheckedout(context, userId, commentTypes);
            return result;

        }
        /// <summary>
        /// Retrieve the assigned user for the current phase of a list of applications
        /// </summary>
        /// <param name="applicationIds">List of application ids to find the user assigned (if any)</param>
        /// <returns>Enumerable list of applications and their assigned user for the current phase</returns>
        /// <remarks>This is set up to accept applicationIds, if appWorkflow or Step ids work better it is an easy change</remarks>
        public ResultModel<IUserApplicationModel> GetAssignedUsers(ICollection<int> applicationIds)
        {
            var result = new ResultModel<IUserApplicationModel>
            {
                ModelList = RepositoryHelpers.GetAssignedUsers(context, applicationIds)
            };
            return result;
        }
        /// <summary>
        /// Retrieve the user information based on a partial string to search
        /// </summary>
        /// <param name="searchString">Partial string to search within all user's name</param>
        /// <param name="clientCollection">List of client ids the currently logged in user is assigned to</param>
        /// <returns>Enumerable list of applications and their assigned user for the current phase</returns>
        public ResultModel<IUserModel> GetAutoCompleteUsers(string searchString, ICollection<int> clientCollection)
        {
            var result = new ResultModel<IUserModel>
            {
                ModelList = RepositoryHelpers.GetAutoCompleteUsers(context, searchString, clientCollection)
            };
            return result;
        }
        #region Summary Comments
        /// <summary>
        /// Retrieves the summary comments associated with the application specified by the input appId
        /// parameters.
        /// </summary>
        /// <param name="panelApplicationId">the panel application identifier</param>
        /// <returns>Zero or more Summary comments for this Application</returns>
        public ResultModel<ISummaryCommentModel> GetApplicationSummaryComments(int panelApplicationId)
        {

            ResultModel<ISummaryCommentModel> result = new ResultModel<ISummaryCommentModel>();
            result.ModelList = RepositoryHelpers.GetApplicationSummaryComments(context, panelApplicationId);

            return result;
        }
        /// <summary>
        /// Gets available workflows for a specific client and review stage.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="reviewStageId">ReviewStage entity identifier</param>
        /// <returns>List of values for available workflows</returns>
        public ResultModel<IMenuItem> GetClientWorkflowsByStage(int clientId, int reviewStageId)
        {
            ResultModel<IMenuItem> result = new ResultModel<IMenuItem>();
            result.ModelList = RepositoryHelpers.GetClientWorkflowsByStage(context, clientId, reviewStageId);

            return result;
        }
        /// <summary>
        /// Retrieves possible file types for a specified client
        /// </summary>
        /// <param name="applicationId">Unique identifier for an application</param>
        /// <returns>Possible file types a client may use</returns>
        public ResultModel<IApplicationFileModel> GetClientFileTypes(int applicationId)
        {
            ResultModel<IApplicationFileModel> result = new ResultModel<IApplicationFileModel>();
            result.ModelList = RepositoryHelpers.GetClientFileTypes(context, applicationId);
            return result;
        }
        /// <summary>
        /// Retrieves the descriptive information describing the abstract file
        /// </summary>
        /// <param name="applicationId">Application entity identifier</param>
        /// <returns>Enumeration of ApplicationFileModels (should only be one)</returns>
        public IEnumerable<ApplicationFileModel> GetClientAbstractFileType(int applicationId)
        {
            return RepositoryHelpers.GetClientAbstractFileType(context, applicationId);
        }
        /// <summary>
        /// Retrieve a matrix representation of awards (y-axis) and priorities (x-axis) and (1)assigned or (2)defaulted workflowIds for each pair 
        /// </summary>
        /// <param name="program">Program abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of program offering</param>
        /// <returns>Enumerable list of awards and their assigned or defaulted workflow</returns>
        public IEnumerable<IAwardWorkflowPriorityModel> GetWorkflowAssignmentOrDefault(int program, int fiscalYear, int? cycle)
        {
            var results = RepositoryHelpers.GetWorkflowAssignmentOrDefault(context, program, fiscalYear, cycle);
            return results;
        }

        /// <summary>
        /// Retrieve workflow information for all workflows belonging to a client
        /// </summary>
        /// <param name="program">Program abbreviation</param>
        /// <param name="fiscalYear">Fiscal year of program offering</param>
        /// <returns>Enumerable list of workflows</returns>
        public IEnumerable<IWorkflowTemplateModel> GetClientWorkflowAll(int program, int fiscalYear)
        {
            var results = RepositoryHelpers.GetClientWorkflowAll(context, program, fiscalYear);
            return results;
        }

        #endregion
        /// <summary>
        /// Gets the request review applications.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns></returns>
        public ResultModel<ISummaryStatementRequestReview> GetRequestReviewApplications(int programId, int yearId, int? cycle, int? panelId, int? awardTypeId)
        {
            var result = new ResultModel<ISummaryStatementRequestReview>
            {
                ModelList = RepositoryHelpers.GetRequestReviewApplications(context, programId, yearId, cycle, panelId, awardTypeId)
            };
            return result;
        }
        /// <summary>
        /// Indicates if the summary statement is checked out.
        /// </summary>
        /// <param name="applicationWorkflowId">ApplicationWorkflow entity identifier</param>
        /// <returns>True summary statemen is check out; false otherwise</returns>
        public bool IsSsCheckedOut(int applicationWorkflowId)
        {
            bool isCheckedOut = RepositoryHelpers.IsSsCheckedOut(context, applicationWorkflowId);
            return isCheckedOut;
        }
        /// <summary>
        /// Specific value return in IWorkflowTransactionModels for an action of "Check-In".
        /// </summary>
        /// <returns>String return for a Check In action</returns>
        public static string CheckIn()
        {
            return RepositoryHelpers.TransactionFinalized;
        }
        /// <summary>
        /// Gets the summary document data.
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <returns>SummaryDocumentModel containing data needed to populate a summary statement</returns>
        public ISummaryDocumentModel GetSummaryDocumentData(int applicationWorkflowId, int panelApplicationId, string procedureName)
        {
            string sqlStatement = $"exec {procedureName} {applicationWorkflowId}, {panelApplicationId}";
            var result = PopulateSummaryDocumentModel(sqlStatement);
            return result;
        }
        /// <summary>
        /// Gets the summary preview document data.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns>SummaryDocumentModel containing data needed to populate a summary statement</returns>
        public ISummaryDocumentModel GetSummaryPreviewDocumentData(int panelApplicationId)
        {
            string sqlStatement = $"exec {PreviewStoredProcedureName} {panelApplicationId}";
            var result = PopulateSummaryDocumentModel(sqlStatement);
            return result;
        }
        /// <summary>
        /// Populates the summary document model.
        /// </summary>
        /// <param name="sqlStatement">The SQL statement.</param>
        /// <returns></returns>
        internal ISummaryDocumentModel PopulateSummaryDocumentModel(string sqlStatement)
        {
            var result = new SummaryDocumentModel();
            //multiple result sets are not supported through dbContext
            //instead we use ado.net and use translate method of objectContext to map to domain model
            using (var connection = new SqlConnection(context.Database.Connection.ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = sqlStatement;
                    var reader = command.ExecuteReader();
                    var objectContext = ((IObjectContextAdapter) context).ObjectContext;
                    result.App = objectContext.Translate<SummaryDocumentModel.ApplicationInfo>(reader).FirstOrDefault();
                    reader.NextResult();
                    result.Critiques = objectContext.Translate<SummaryDocumentModel.CritiqueData>(reader).ToList();
                    reader.NextResult();
                    result.OtherData1 = objectContext.Translate<SummaryDocumentModel.OtherData>(reader).ToList();
                    reader.NextResult();
                    result.OtherData2 = objectContext.Translate<SummaryDocumentModel.OtherData>(reader).ToList();
                    reader.NextResult();
                    result.OtherData3 = objectContext.Translate<SummaryDocumentModel.OtherData>(reader).ToList();
                }
            }
            return result;
        }
        #endregion
    }
}
