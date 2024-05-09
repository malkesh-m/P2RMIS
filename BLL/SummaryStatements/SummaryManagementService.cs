using System;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.SummaryStatement;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.ModelBuilders.ClientManagement;
using Sra.P2rmis.Bll.ModelBuilders.SummaryStatement;
using Sra.P2rmis.Bll.SummaryStatements.Blocks;
using Sra.P2rmis.Bll.Rules;
using Sra.P2rmis.Bll.Setup.Blocks;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.CrossCuttingServices.DocumentServices;
using Sra.P2rmis.CrossCuttingServices.FileServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.CrossCuttingServices.Validations;
using Sra.P2rmis.WebModels.Files;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    /// <summary>
    /// Services provided for Summary management.
    /// </summary>
    public partial class SummaryManagementService : ServerBase, ISummaryManagementService
    {
        private const string NotDiscussed = "ND";
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public SummaryManagementService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion
        #region Provided Services
        /// <summary>
        /// Retrieves the applications that are available for summary statement processing based on the user specified
        /// parameters.
        /// </summary>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <returns>Zero or more Applications</returns>
        public Container<IAvailableApplications> GetSummaryApplications(int panelId, int cycle, int? awardTypeId)
        {
            //
            // Set up default return
            // 
            Container<IAvailableApplications> container = new Container<IAvailableApplications>();

            if (panelId > 0)
            {
                //
                // Call the DL and retrieve any programs for this client
                //
                var results = UnitOfWork.SummaryManagementRepository.GetSummaryApplications(panelId, cycle, awardTypeId);
                //
                // Then create the view to return to the PI layer & return
                //
                container.SetModelList(results);
            }

            return container;
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
        public Container<ISummaryStatementApplicationModel> GetSummaryStatementApplications(int programId,
            int yearId, int? cycle, int? panelId, int? awardTypeId)
        {
            //
            // Set up default return
            // 
            var container = new Container<ISummaryStatementApplicationModel>();

            if (IsSummaryManagementPanelRequestParamsValid(programId, yearId, cycle, panelId, awardTypeId))
            {
                //
                // Call the DL and retrieve any programs for this client
                //
                var results = UnitOfWork.SummaryManagementRepository.GetSummaryStatementApplications(programId, yearId, cycle, panelId, awardTypeId);
                //
                // Then create the view to return to the PI layer & return
                //
                container.SetModelList(results);
            }

            return container;
        }
        /// <summary>
        /// Gets the summary statement applications in progress within a summary workflow.
        /// </summary>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="awardTypeId">The award type identifier.</param>
        /// <returns>Container of summary statement progress information</returns>
        public Container<ISummaryStatementProgressModel> GetSummaryStatementApplicationsInProgress(int programId,
            int yearId, int? cycle, int? panelId, int? awardTypeId)
        {
            //
            // Set up default return
            // 
            var container = new Container<ISummaryStatementProgressModel>();

            if (IsSummaryManagementPanelRequestParamsValid(programId, yearId, cycle, panelId, awardTypeId))
            {
                //
                // Call the DL and retrieve any programs for this client
                //
                var results = UnitOfWork.SummaryManagementRepository.GetSummaryStatementApplicationsInProgress(programId, yearId, cycle, panelId, awardTypeId);
                //
                // Then create the view to return to the PI layer & return
                //
                container.SetModelList(results);
            }
            return container;
        }
        /// <summary>
        /// Retrieves the applications that are available for summary statement processing based on the user specified
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award Type (mechanism) identifier (optional)</param>
        /// <returns>Container holding a list of SummaryGroups matching the user specified parameter</returns>
        public Container<ISummaryGroup> GetAllPanelSummaries(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId)
        {
            ///
            /// Set up default return
            /// 
            Container<ISummaryGroup> container = new Container<ISummaryGroup>();

            if (IsSummaryManagementPanelRequestParamsValid(program, fiscalYear, cycle, panelId, awardTypeId))
            {
                //
                // Call the DL and retrieve any programs for this client
                //
                var results = UnitOfWork.SummaryManagementRepository.GetAllPanelSummaries(program, fiscalYear, cycle, panelId, awardTypeId);
                //
                // Then create the view to return to the PI layer & return
                //
                container.SetModelList(results);
            }

            return container;
        }
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
        public Container<ISummaryGroup> GetPanelSummaries(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId, int? userId)
        {
            //
            // Set up the default return
            // 
            Container<ISummaryGroup> container = new Container<ISummaryGroup>();

            if (IsSummaryManagementPanelRequestParamsValid(program, fiscalYear, cycle, panelId, awardTypeId))
            {
                //
                // Call the DL and retrieve the panels for the user's selection
                //
                var results = UnitOfWork.SummaryManagementRepository.GetPanelSummaries(program, fiscalYear, cycle, panelId, awardTypeId, userId);
                //
                // Stuff the results into the container and return to the PL
                //
                container.SetModelList(results);
            }

            return container;
        }
        /// <summary>
        /// Retrieves the grouping for the applications whose summary statements are currently being processed by phase.
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award Type (mechanism) identifier (optional)</param>
        /// <returns>Zero or more Applications</returns>
        public Container<ISummaryGroup> GetPanelSummariesPhases(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId)
        {
            //
            // Set up the default return
            // 
            Container<ISummaryGroup> container = new Container<ISummaryGroup>();

            if (IsSummaryManagementPanelRequestParamsValid(program, fiscalYear, cycle, panelId, awardTypeId))
            {
                //
                // Call the DL and retrieve the panels for the user's selection
                //
                var results = UnitOfWork.SummaryManagementRepository.GetPanelSummariesPhases(program, fiscalYear, cycle, panelId, awardTypeId);
                //
                // Stuff the results into the container and return to the PL
                //
                container.SetModelList(results);
            }

            return container;
        }

        /// <summary>
        /// Retrieves the phase counts for the summary statements currently being processed
        /// </summary>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <param name="userId">the selected user id (optional)</param>
        /// <returns>Zero or more phase counts that are currently being processed</returns>
        public Container<IPhaseCountModel> GetPhaseCounts(int panelId, int cycle, int? awardTypeId, int? userId)
        {
            ///
            /// Set up default return
            /// 
            Container<IPhaseCountModel> container = new Container<IPhaseCountModel>();

            if (panelId > 0)
            {
                //
                // Call the DL and retrieve any programs for this client
                //
                var results = UnitOfWork.SummaryManagementRepository.GetPhaseCounts(panelId, cycle, awardTypeId, userId);
                //
                // Then create the view to return to the PI layer & return
                //
                container.SetModelList(results);
            }

            return container;
        }
        /// <summary>
        /// Retrieves the completed applications.
        /// </summary>
        /// <param name="panelId">the selected panel id</param>
        /// <param name="cycle">The selected receipt cycle</param>
        /// <param name="awardTypeId">the selected award type id (optional)</param>
        /// <param name="userId">the selected user id (optional)</param>
        /// <returns>Zero or more Applications that are currently being processed</returns>
        public Container<IApplicationsProgress> GetCompletedProgressApplications(int panelId, int cycle, int? awardTypeId, int? userId)
        {
            ///
            /// Set up default return
            /// 
            Container<IApplicationsProgress> container = new Container<IApplicationsProgress>();

            if (panelId > 0)
            {
                //
                // Call the DL and retrieve any closed applications
                //
                var results = UnitOfWork.SummaryManagementRepository.GetCompletedProgressApplications(panelId, cycle, awardTypeId, userId);
                //
                // Then populate the Container to return the data to the PI layer
                //
                container.SetModelList(results);
            }

            return container;
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
        public Container<IApplicationsProgress> GetCompletedApplications(int programId, int yearId, int? cycle, int? panelId, int? awardTypeId, int? userId)
        {
            ///
            /// Set up default return
            /// 
            Container<IApplicationsProgress> container = new Container<IApplicationsProgress>();
            //
            // Retrieve any closed applications
            //
            var results = UnitOfWork.SummaryManagementRepository.GetCompletedApplications(programId, yearId, panelId, cycle, awardTypeId, userId);
            //
            // Then populate the Container to return the data to the PI layer
            //
            container.SetModelList(results);
            return container;
        }
        /// <summary>
        /// Retrieves the transaction history for a specified application's workflow.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application's instance of a workflow</param>
        /// <returns>Zero or more transactions of an application workflow</returns>
        public Container<IWorkflowTransactionModel> GetWorkflowTransactionHistory(int applicationWorkflowId)
        {
            ///
            /// Set up default return
            /// 
            Container<IWorkflowTransactionModel> container = new Container<IWorkflowTransactionModel>();

            if (applicationWorkflowId > 0)
            {
                //
                // Call the DL and retrieve the transaction history for this application workflow
                //
                var results = UnitOfWork.SummaryManagementRepository.GetWorkflowTransactionHistory(applicationWorkflowId);
                // 
                // now post process the history returned.  
                //
                container.ModelList = PostProcessWorkflowTransactionHistory(results.ModelList, applicationWorkflowId);
           }

            return container;
        }
        /// <summary>
        /// The ApplicationWorklogTransactionHistory entry only has a entry for the ApplicationWorkflowStep it was checked out
        /// from.  But it also needs the target (or the ApplicationWorkflosStep that it was check into).  So it needs to be
        /// post processed.  The IWorkflowTransactionModels returned from the database are returned in order thus enabling 
        /// a "look ahead" into the next history step to obtain the phase name.
        /// </summary>
        /// <param name="transactionHistoryModels">Collection of IWorkflowTransactionModels</param>
        /// <param name="applicationWorkflowId">ApplicationWorkflow entity identifier</param>
        /// <returns>Post processed list</returns>
        internal virtual List<IWorkflowTransactionModel> PostProcessWorkflowTransactionHistory(IEnumerable<IWorkflowTransactionModel> transactionHistoryModels, int applicationWorkflowId)
        {
            List<IWorkflowTransactionModel> list = transactionHistoryModels.ToList();

            for (int i = 0; i < list.Count(); i++)
            {
                var model = list[i];
                //
                // If we are a check-in then we just look ahead
                //
                if (model.Action == SummaryManagementRepository.CheckIn())
                {
                    //
                    // If there is a log entry after this then retrieve the phase name from there
                    //
                    if (i+1 < list.Count())
                    {
                        model.PhaseName = list[i + 1].PhaseName;
                    }
                    //
                    // Otherwise we need to get if from the workflow (basically whatever is active)
                    // 
                    else
                    {
                        ApplicationWorkflow applicationWorkflowEntity = UnitOfWork.ApplicationWorkflowRepository.GetByID(applicationWorkflowId);
                        ApplicationWorkflowStep applicationWorkflowStepEntity = applicationWorkflowEntity.CurrentStep();
                        model.PhaseName = (applicationWorkflowStepEntity != null) ? applicationWorkflowStepEntity.StepName : string.Empty;
                    }
                }
            }

            return list;
        }
        /// <summary>
        /// Retrieves panel summaries for all closed applications that were processed using P2RMIS-2.
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">Award Type (mechanism) identifier (optional)</param>
        /// <returns>Container holding a list of SummaryGroups matching the user specified parameter</returns>
        public Container<ISummaryGroup> GetAllCompletePanelSummaries(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId)
        {
            ///
            /// Set up default return
            /// 
            Container<ISummaryGroup> container = new Container<ISummaryGroup>();

            if (IsSummaryManagementPanelRequestParamsValid(program, fiscalYear, cycle, panelId, awardTypeId))
            {
                //
                // Call the DL and retrieve any programs for this client
                //
                var results = UnitOfWork.SummaryManagementRepository.GetAllCompletePanelSummaries(program, fiscalYear, cycle, panelId, awardTypeId);
                //
                // Then create the view to return to the PI layer & return
                //
                container.SetModelList(results);
            }

            return container;
        }
        /// <summary>
        /// Retrieve a list of reviewer matching the user entered name substring.
        /// </summary>
        /// <param name="collection">Requesting user's client list</param>
        /// <param name="program">Program filter value</param>
        /// <param name="fiscalYear">Fiscal year filter value</param>
        /// <param name="cycleId">Cycle filter value id</param>
        /// <param name="panelId">Panel filter value id</param>
        /// <param name="awardId">Award filter value id</param>
        /// <param name="reviewerName">String to search</param>
        /// <returns>Container of matching reviewer namesL</returns>
        public Container<IUserModel> GetSearchReviewersList(ICollection<int> collection, int program, int fiscalYear, int? cycleId, int? panelId, int? awardId, string reviewerName)
        {
            //
            // Set up default return
            // 
            Container<IUserModel> container = new Container<IUserModel>();

            if (GetSearchReviewersListParamsValid(collection, reviewerName))
            {
                //
                // Call the DL and retrieve the document types
                //
                var results = UnitOfWork.SummaryManagementRepository.GetSearchReviewersList(collection, program, fiscalYear, cycleId, panelId, awardId, reviewerName);
                //
                // Populate the container to return to the PI layer & return
                //
                container.SetModelList(results);
            }

            return container;
        }
        /// <summary>
        /// Gets available summary statement workflows for a specific client 
        /// </summary>
        /// <param name="clientProgramId">ClientProgram entity identifier</param>
        /// <returns>list of values for available workflows</returns>
        public Container<IMenuItem> GetClientSummaryStatementWorkflows(int clientProgramId)
        {
            ValidateInt(clientProgramId, FullName(nameof(SummaryManagementService), nameof(GetClientSummaryStatementWorkflows)), nameof(clientProgramId));

            Container<IMenuItem> container = new Container<IMenuItem>();
            //
            // Retrieve the ClientProgram.  We get the Client entity identifier from it.
            //
            ClientProgram clientProgramEntity = UnitOfWork.ClientProgramRepository.GetByID(clientProgramId);
            //
            // Now retrieve the Client's summary statement workflows
            //
            var results = UnitOfWork.SummaryManagementRepository.GetClientWorkflowsByStage(clientProgramEntity.ClientId, ReviewStage.Indexes.Summary);
            container.SetModelList(results);
            return container;
        }
        //todo: move to helper classes
        /// <summary>
        /// Validates the parameters for GetSearchReviewersList.
        /// - collection is not null & contains one or more entries
        /// - reviewerName is not null & not white space
        /// </summary>
        /// <param name="collection">Collection of </param>
        /// <param name="reviewerName">Reviewers name</param>
        /// <returns>True if parameters are valid; false otherwise</returns>
        private bool GetSearchReviewersListParamsValid(ICollection<int> collection, string reviewerName)
        {
            return (
                    (collection != null) &&
                    (collection.Count >= 1) &&
                    (!string.IsNullOrWhiteSpace(reviewerName))
                   );
        }
        /// <summary>
        /// query to name of the user who checked out the summary
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for application workflow Step Id</param>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>returns user name</returns>
        public UserName GetSummaryCheckedOutUserName(int AppWorkflowStepId)
        {
            UserName userName = UnitOfWork.SummaryManagementRepository.GetSummaryCheckedOutUserName(AppWorkflowStepId);
            return userName;
        }
        /// <summary>
        /// Determines whether a summary statement is checked out
        /// </summary>
        /// <param name="applicationWorkflowId">the application workflow id</param>
        /// <returns>Boolean true or false</returns>
        public bool IsSsCheckedOut(int applicationWorkflowId)
        {
            bool isCheckedOut = UnitOfWork.SummaryManagementRepository.IsSsCheckedOut(applicationWorkflowId);
            return isCheckedOut;
        }
        /// <summary>
        /// Determines whether [is ss web based] [the specified client program identifier].
        /// </summary>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is ss web based] [the specified client program identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSsWebBased(int clientProgramId)
        {
            ValidateInt(clientProgramId, FullName(nameof(SummaryManagementService), nameof(IsSsWebBased)), nameof(clientProgramId));

            ClientProgramModelBuilder builder = new ClientProgramModelBuilder(this.UnitOfWork, clientProgramId);
            builder.BuildContainer();
            return builder.IsSummaryStatementWebBased;
        }
        /// <summary>
        /// Gets the summary document file.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns></returns>
        public KeyValuePair<ISummaryDocumentFileModel, IServiceState> GetSummaryDocumentFile(int panelApplicationId)
        {
            bool doesSummaryWorkflowExist =
                this.UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId).IsSummaryStarted();
            var file = doesSummaryWorkflowExist ? GetStartedSummaryDocumentFile(panelApplicationId) : GetUnstartedSummaryDocumentFile(panelApplicationId);
            return file;
        }
        /// <summary>
        /// Gets the started summary document file.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns></returns>
        public KeyValuePair<ISummaryDocumentFileModel, IServiceState> GetStartedSummaryDocumentFile(int panelApplicationId)
        {
            ValidateInt(panelApplicationId, FullName(nameof(SummaryManagementService), nameof(GetStartedSummaryDocumentFile)), nameof(panelApplicationId));

            SummaryDocumentRetrievalModelBuilder builder = new SummaryDocumentRetrievalModelBuilder(this.UnitOfWork, panelApplicationId);
            builder.BuildContainer();
            var messages = new List<string>();
            var hasAppTemp = builder.HasApplicationTemplate();
            if (!hasAppTemp)
            {
                messages.Add(MessageService.MissingApplicationTemplate);
            }
            var hasDocTemp = builder.HasDocumentTemplate();
            if (!hasDocTemp)
            {
                messages.Add(MessageService.MissingDocumentTemplate);
            }
            var serviceState = new ServiceState(hasAppTemp, messages);
            return new KeyValuePair<ISummaryDocumentFileModel, IServiceState>(builder.Result, serviceState);
        }
        /// <summary>
        /// Gets the unstarted summary document file.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <returns></returns>
        public KeyValuePair<ISummaryDocumentFileModel, IServiceState> GetUnstartedSummaryDocumentFile(int panelApplicationId)
        {
            ValidateInt(panelApplicationId, FullName(nameof(SummaryManagementService), nameof(GetUnstartedSummaryDocumentFile)), nameof(panelApplicationId));

            SummaryDocumentPreviewModelBuilder builder = new SummaryDocumentPreviewModelBuilder(this.UnitOfWork, panelApplicationId);
            builder.BuildContainer();
            var messages = new List<string>();
            var hasAppTemp = builder.HasApplicationTemplate();
            if (!hasAppTemp)
            {
                messages.Add(MessageService.MissingApplicationTemplate);
            }
            var hasDocTemp = builder.HasDocumentTemplate();
            if (!hasDocTemp)
            {
                messages.Add(MessageService.MissingDocumentTemplate);
            }
            var serviceState = new ServiceState(hasAppTemp, messages);
            return new KeyValuePair<ISummaryDocumentFileModel, IServiceState>(builder.Result, serviceState);
        }
        /// <summary>
        /// Gets the zipped final summary documents location.
        /// </summary>
        /// <param name="panelApplicationIds">The panel application ids.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public string GenerateFinalSummaryDocuments(int[] panelApplicationIds, int userId)
        {
            //Validation panelapplicationids
            panelApplicationIds.ToList().ForEach(
                i => ValidateInt(i, FullName(nameof(SummaryManagementService), nameof(GenerateFinalSummaryDocuments)),
                        nameof(panelApplicationIds)));

            ZipService fileZipper = new ZipService(userId);
            foreach (var panelApplicationId in panelApplicationIds)
            {
                var documentDataWithStatus = GetStartedSummaryDocumentFile(panelApplicationId);
                var documentData = documentDataWithStatus.Key;
                fileZipper.AddWordFile(documentData.ProgramAbbreviation, documentData.FiscalYear, documentData.ReceiptCycle, documentData.LogNumber, WordServices.Process(documentData.FileContent));
            }
            fileZipper.PerformZip();
            return fileZipper.ZipFullPath;
        }
        /// <summary>
        /// Gets the summary document backup file.
        /// </summary>
        /// <param name="applicationWorkflowStepWorkLogId">The application workflow step work log identifier.</param>
        /// <param name="action">The action.</param>
        /// <returns>
        /// FileResult model
        /// </returns>
        public IFileResultModel GetSummaryDocumentBackupFile(int applicationWorkflowStepWorkLogId, string action)
        {
            ValidateInt(applicationWorkflowStepWorkLogId, FullName(nameof(SummaryManagementService), nameof(GetSummaryDocumentBackupFile)), nameof(applicationWorkflowStepWorkLogId));
            ValidateWorkLogAction(action, FullName(nameof(SummaryManagementService), nameof(GetSummaryDocumentBackupFile)), nameof(action));
            SummaryDocumentWorkLogFileModelBuilder builder = new SummaryDocumentWorkLogFileModelBuilder(this.UnitOfWork, applicationWorkflowStepWorkLogId, action);
            builder.BuildContainer();
            return builder.Result;
        }
        /// <summary>
        /// Processes the summary statement upload.
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="inputData">The input data for the file to process.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>failure message if unsuccessful</returns>
        public ServiceState ProcessSummaryStatementUpload(int applicationWorkflowId, int targetApplicationWorkflowStepId, byte[] inputData, int userId)
        {
            // Ignores the "complete" virtual step which has a zero value
            if (targetApplicationWorkflowStepId > 0)
            {
                var isReviewStep = UnitOfWork.ApplicationWorkflowStepRepository.IsWorkflowStepReview(targetApplicationWorkflowStepId);
                if (isReviewStep)
                {
                    inputData = WordServices.AcceptTrackChangesAndRemoveComments(inputData);
                }
            }
            // Extract the data
            var extractedContent = WordServices.Read(inputData);
            // Validate the extracted data
            ServiceState serviceState = ValidateWordContent(applicationWorkflowId, extractedContent);
            // Save the data            
            if (serviceState.IsSuccessful)
                serviceState = SaveSummaryDocument(applicationWorkflowId, inputData, extractedContent, userId).Combine(serviceState);
            return serviceState;
        }

        /// <summary>
        /// Gets the program mechanism summary statement.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        public Container<IProgramMechanismSummaryStatementModel> GetProgramMechanismSummaryStatement(int panelApplicationId, int programMechanismId)
        {
            ValidateInt(panelApplicationId, FullName(nameof(SummaryManagementService), nameof(GetProgramMechanismSummaryStatement)), nameof(panelApplicationId));
            ValidateInt(programMechanismId, FullName(nameof(SummaryManagementService), nameof(GetProgramMechanismSummaryStatement)), nameof(programMechanismId));

            ProgramMechanismSummaryStatementModelBuilder builder = new ProgramMechanismSummaryStatementModelBuilder(this.UnitOfWork, panelApplicationId, programMechanismId);
            builder.BuildContainer();
            return builder.Results;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Checks parameters for validity
        /// </summary>
        /// <param name="program">the selected program abbreviation</param>
        /// <param name="fiscalYear">the selected fiscal year</param>
        /// <param name="cycle">the selected cycle (optional)</param>
        /// <param name="panelId">the selected panel id (optional)</param>
        /// <param name="awardTypeId">the selected award id (optional)</param>
        /// <returns>True is parameters are valid; false otherwise</returns>
        private bool IsSummaryManagementPanelRequestParamsValid(int program, int fiscalYear, int? cycle, int? panelId, int? awardTypeId)
        {
            return ((program > 0) &&
                    (fiscalYear > 0) &&
                    (BllHelper.IdOk(cycle)) &&
                    (BllHelper.IdOk(panelId)) &&
                    ((awardTypeId == null) || (awardTypeId > 0))
                    );
        }

        /// <summary>
        /// Validates the work log action.
        /// </summary>
        /// <param name="action">The action to be validated.</param>
        /// <param name="caller">The caller.</param>
        /// <param name="parameter">The parameter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        private void ValidateWorkLogAction(string action, string caller, string parameter)
        {
            if (action != ApplicationWorkflowStepWorkLog.CheckinAction &&
                action != ApplicationWorkflowStepWorkLog.CheckoutAction)
            {
                string message = string.Format("{0} detected an invalid parameter: {1} was [{2}]", caller, parameter, action);
                throw new ArgumentException(message);
            }
        }
        /// <summary>
        /// Creates the summary documents.
        /// </summary>
        /// <param name="collections">The collections.</param>
        /// <param name="thePanelManagementService">The panel management list service.</param>
        public void CreateSummaryDocuments(ICollection<ChangeToSave> collections, IPanelManagementService thePanelManagementService)
        {
            foreach(var item in collections)
            {
                var app = thePanelManagementService.GetApplicationInformation(item.PanelApplicationId);
                bool isWebBased = IsSsWebBased(app.ClientProgramId);
                if (!isWebBased)
                {
                    var o = GetProgramMechanismSummaryStatement(item.PanelApplicationId, app.ProgramMechanismId).Model;
                    string templateRelativeLocation = o.TemplateLocation;
                    var state = CreateSummaryDocument(item.PanelApplicationId, o.StoredProcedureName, ConfigManager.GetTemplateFullPath(templateRelativeLocation), item.UserId);
                }
            }
        }
        /// <summary>
        /// Creates the summary document.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="templateLocation">The template location.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ServiceState CreateSummaryDocument(int panelApplicationId, string storedProcedureName, string templateLocation, int userId)
        {
            ValidateInt(panelApplicationId, FullName(nameof(SummaryManagementService), nameof(CreateSummaryDocument)), nameof(panelApplicationId));
            ValidateString(storedProcedureName, FullName(nameof(SummaryManagementService), nameof(CreateSummaryDocument)), nameof(storedProcedureName));
            ValidateString(templateLocation, FullName(nameof(SummaryManagementService), nameof(CreateSummaryDocument)), nameof(templateLocation));

            SummaryDocumentModelBuilder builder = new SummaryDocumentModelBuilder(this.UnitOfWork, panelApplicationId,
                storedProcedureName, templateLocation);
            builder.BuildContainer();
            // Save document
            ApplicationWorkflowSummaryStatementBlock block = new ApplicationWorkflowSummaryStatementBlock(builder.ApplicationWorkflowId);
            block.SetDocumentFile(builder.DocumentFileData);
            block.ConfigureAdd();

            return DoCrud(block, null, CrudAction.Add, 0, true, userId);
        }
        /// <summary>
        /// Saves the summary document.
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="inputStream">The input stream.</param>
        /// <param name="extractedContent">Content of the extracted.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        protected ServiceState SaveSummaryDocument(int applicationWorkflowId, byte[] inputData, IList<Tuple<int, string, string>> extractedContent, int userId)
        {
            ServiceState state = null;
            //first lookup the appropriate ApplicationWorkflowStepElementId for each piece of content
            var contentToSave = SetContentIdsFromDatabase(applicationWorkflowId, extractedContent);
            // Save contents
            foreach (var content in contentToSave)
            {
                ApplicationWorkflowStepElementContentBlock block = new ApplicationWorkflowStepElementContentBlock(content.Item1);

                // Check if it's a Word template hint, do not save.
                if (content.Item2.ToLower().Contains(WordServices.TemplatePlaceholderText))
                {
                    continue;
                }

                block.SetContent(content.Item2, content.Item3);
               
                if (content.Item4 != null)
                {
                    block.ConfigureModify();
                    state = DoCrud(block, content.Item4, CrudAction.Modify, content.Item4.ApplicationWorkflowStepElementContentId, false, userId);
                }
                else
                {
                    block.ConfigureAdd();
                    state = DoCrud(block, null, CrudAction.Add, 0, false, userId);
                }
                if (!state.IsSuccessful)
                {
                    break;
                }
            }
            // Save file
            if (state != null && state.IsSuccessful)
            {
                //lookup current ApplicationWorkflowSummaryStatement if it exists
                var summaryStatementEntity =
                    UnitOfWork.ApplicationWorkflowRepository.GetByID(applicationWorkflowId)
                        .ApplicationWorkflowSummaryStatements.FirstOrDefault();
                ApplicationWorkflowSummaryStatementBlock block = new ApplicationWorkflowSummaryStatementBlock(applicationWorkflowId);
                block.SetDocumentFile(inputData);
                if (summaryStatementEntity != null)
                {
                    block.ConfigureModify();
                    state = DoCrud(block, summaryStatementEntity, CrudAction.Modify,
                        summaryStatementEntity.ApplicationWorkflowSummaryStatementId, false, userId);
                }
                else
                {
                    block.ConfigureAdd();
                    state = DoCrud(block, null, CrudAction.Add, 0, false, userId);
                }
            }
            if (state != null && state.IsSuccessful)
            {
                UnitOfWork.Save();
            }

            return state;
        }
        /// <summary>
        /// Validates the content of the word document.
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="extractedContent">Content of the extracted.</param>
        /// <returns>validation message if unsuccessful</returns>
        internal ServiceState ValidateWordContent(int applicationWorkflowId, IList<Tuple<int, string, string>> extractedContent)
        {
            var currentElementList =
                UnitOfWork.ApplicationWorkflowRepository.GetByID(applicationWorkflowId)
                    ?.ApplicationWorkflowSteps.FirstOrDefault()
                    ?.ApplicationWorkflowStepElements
                    .Where(x => x.ApplicationTemplateElement.MechanismTemplateElement.TextFlag)
                    .Select(x => x.ApplicationTemplateElementId)
                    .ToList();
            var extractedElementList = extractedContent.Select(x => x.Item1).ToList();
            var isSuccessful = true;
            IList<string> messageList = isSuccessful
                ? new List<string>()
                : new List<string> {MessageService.CheckInInvalid}; 
            return new ServiceState(isSuccessful, messageList);
        }

        /// <summary>
        /// Sets the content ids from database.
        /// </summary>
        /// <param name="applicationWorkflowId">The application workflow identifier.</param>
        /// <param name="extractedContent">Content of the extracted.</param>
        /// <returns></returns>
        internal IList<Tuple<int, string, string, ApplicationWorkflowStepElementContent>> SetContentIdsFromDatabase(int applicationWorkflowId, IList<Tuple<int, string, string>> extractedContent)
        {
            var currentStepId =
                UnitOfWork.ApplicationWorkflowRepository.GetByID(applicationWorkflowId)
                    .CurrentStep()
                    .ApplicationWorkflowStepId;
            var templateElementIds = extractedContent.Select(x => x.Item1).ToList();
            var idPairing = UnitOfWork.ApplicationWorkflowStepElementRepository.Select()
                .Where(x => templateElementIds.Contains(x.ApplicationTemplateElementId) &&
                        x.ApplicationWorkflowStepId == currentStepId)
                 .Select(x => new
                 {
                     x.ApplicationWorkflowStepElementId,
                     x.ApplicationTemplateElementId,
                     ContentEntity = x.ApplicationWorkflowStepElementContents.FirstOrDefault()
                 }).ToList();
            var returnList = new List<Tuple<int, string, string, ApplicationWorkflowStepElementContent>>();
            foreach (var tuple in extractedContent)
            {
                returnList.Add(new Tuple<int, string, string, ApplicationWorkflowStepElementContent>(idPairing.Where(x => x.ApplicationTemplateElementId == tuple.Item1).Select(x => x.ApplicationWorkflowStepElementId).FirstOrDefault(), tuple.Item2, tuple.Item3, idPairing.Where(x => x.ApplicationTemplateElementId == tuple.Item1).Select(x => x.ContentEntity).FirstOrDefault()));
            }
            return returnList;
        }
        #endregion
        #region CRUD        
        /// <summary>
        /// Does the crud.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="operation">The operation.</param>
        /// <param name="entityId">The entity identifier.</param>
        /// <param name="doUpdate">if set to <c>true</c> [do update].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        internal virtual ServiceState DoCrud(ApplicationWorkflowSummaryStatementBlock block, ApplicationWorkflowSummaryStatement entity, CrudAction operation, int entityId, bool doUpdate, int userId)
        {
            RuleEngine<ApplicationWorkflowSummaryStatement> rules = RuleEngineConstructors.CreateApplicationWorkflowSummaryStatementEngine(UnitOfWork, entity, operation, block);
            //
            // 2) Create the action & execute it
            //
            ApplicationWorkflowSummaryStatementServiceAction action = new ApplicationWorkflowSummaryStatementServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationWorkflowSummaryStatementRepository, doUpdate, entityId, userId);
            action.Populate(block);
            action.Rule(rules);
            action.Execute();
            action.PostProcess();
            //
            // We pull the status off of the rules engine even though the action has ran after.  If
            // there was a failure an exception would have been thrown ignoring us all.
            //
            return new ServiceState(!rules.IsBroken, rules.Messages, action.EntityInfo);
        }
        /// <summary>
        /// Does the crud.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="operation">The operation.</param>
        /// <param name="entityId">The entity identifier.</param>
        /// <param name="doUpdate">if set to <c>true</c> [do update].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        internal virtual ServiceState DoCrud(ApplicationWorkflowStepElementContentBlock block, ApplicationWorkflowStepElementContent entity, CrudAction operation, int entityId, bool doUpdate, int userId)
        {
            RuleEngine<ApplicationWorkflowStepElementContent> rules = RuleEngineConstructors.CreateApplicationWorkflowStepElementContentEngine(UnitOfWork, entity, operation, block);
            //
            // 2) Create the action & execute it
            //
            ApplicationWorkflowStepElementContentServiceAction action = new ApplicationWorkflowStepElementContentServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationWorkflowStepElementContentRepository, doUpdate, entityId, userId);
            action.Populate(block);
            action.Rule(rules);
            action.Execute();
            action.PostProcess();
            //
            // We pull the status off of the rules engine even though the action has ran after.  If
            // there was a failure an exception would have been thrown ignoring us all.
            //
            return new ServiceState(!rules.IsBroken, rules.Messages, action.EntityInfo);
        }

        #endregion
    }
}
