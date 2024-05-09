using System;
using System.Collections.Generic;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Reports;
using Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.Bll.ModelBuilders.SummaryStatement;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Bll.PanelManagement;
using DataLayer = Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    /// <summary>
    /// Services provided for Summary management
    ///     - Modifications to the applications state
    ///     - Pushes the application(s) to the 'processing' queue
    /// </summary>
    public partial class SummaryManagementService : ISummaryManagementService
    {
        #region The services
        /// <summary>
        /// Set or remove a priority of the application.
        /// </summary>
        /// <param name="applicationId">Application identification</param>
        /// <param name="statusToChange">Identifies the priority</param>
        /// <param name="state">Change state</param>
        /// <param name="userId">User requesting the change</param>
        private void SetPriority(DataLayer.Application application, int panelApplicationId, int statusToChange, bool state, int userId)
        {
            DataLayer.PanelApplication a = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            //
            // If the Status indicator (Priority One or Priority Two) is not set and the status indicator is there, it needs to be deleted
            //
            DataLayer.ApplicationReviewStatu b = a.HasReviewStatus(statusToChange);
            if ((!state) & (b != null))
            {
                //
                // There is no review status for the specified value but there is one in the database.
                // So delete it.
                // 
                var editAction = new PriorityChangeServiceAction();
                editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationReviewStatuGenericRepository, ServiceAction<DataLayer.ApplicationReviewStatu>.DoNotUpdate, b.ApplicationReviewStatusId, userId);
                editAction.Execute();
            }
            else if ((state) & (b == null))
            {
                //
                // There is no ReviewStatu entity for this state, but we need one.  So
                // create an editor and add it.
                //
                var editAction = new PriorityChangeServiceAction();
                editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationReviewStatuGenericRepository, ServiceAction<DataLayer.ApplicationReviewStatu>.DoNotUpdate, userId);
                editAction.Populate(a.ApplicationId, statusToChange, panelApplicationId);
                editAction.Execute();
            }
        }
        /// <summary>
        /// Pushes the selected application(s) to the processing queue.
        /// </summary>
        /// <param name="applicationIds">One or more application ids</param>
        /// <param name="userId">User identifier</param>
        public IServiceState StartApplications(ICollection<ChangeToSave> collection, IPanelManagementService thePanelManagementService)
        {
            //
            // Start each application identified in the collection of parameters
            //
            var isSuccessful = false;
            var messages = new List<string>();
            var entityInfo = new List<IEntityInfo>();
            foreach (var item in collection)
            {
                SummaryDocumentPreviewModelBuilder builder = new SummaryDocumentPreviewModelBuilder(this.UnitOfWork, item.PanelApplicationId);
                var app = thePanelManagementService.GetApplicationInformation(item.PanelApplicationId);
                var isSsWebBased = IsSsWebBased(app.ClientProgramId);
                builder.GetTemplatePath();
                if ((!isSsWebBased && !builder.HasDocumentTemplate()) || !builder.HasRequiredApplicationData())
                {
                    messages.Add(String.Format(MessageService.StartProcessFailureMessage, item.LogNumber));
                    entityInfo.Add(new BaseEntityInfo(item.PanelApplicationId));
                }
                else
                {
                    UnitOfWork.ApplicationRepository.StartApplications(item.PanelApplicationId, item.UserId, item.WorkflowId);
                }
            }
            if (messages.Count == 0)
            {
                isSuccessful = true;
            }
            var state = new ServiceState(isSuccessful, messages, entityInfo);
            return state;
        }
        /// <summary>
        /// Retrieves information about the application associated with the panel application.
        /// </summary>
        /// <param name="panelApplicationId">Panel Application identifier</param>
        /// <returns>Application details for a single application</returns>
        public IApplicationDetailModel GetPreviewApplicationInfoDetail(int panelApplicationId)
        {
            ValidateInt(panelApplicationId, FullName(nameof(SummaryManagementService), nameof(GetPreviewApplicationInfoDetail)), nameof(panelApplicationId));

            return UnitOfWork.SummaryManagementRepository.GetPreviewApplicationInfoDetail(panelApplicationId);
        }
        /// <summary>
        /// Retrieves the details of an application workflow step including content and element metadata.
        /// </summary>
        /// <param name="logNumber">Identifier for an application's instance of a workflow</param>
        /// <returns>Zero or more application workflow step elements</returns>
        public Container<IStepContentModel> GetPreviewApplicationStepContent(int panelApplicationId)
        {
            ValidateInt(panelApplicationId, FullName(nameof(SummaryManagementService), nameof(GetPreviewApplicationStepContent)), nameof(panelApplicationId));
            //
            // Set up default return & check the parameter validity
            // 
            Container<IStepContentModel> container = new Container<IStepContentModel>();

            var results = UnitOfWork.SummaryManagementRepository.GetPreviewApplicationStepContent(panelApplicationId);
            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Retrieves application report information for the indicated application ids
        /// </summary>
        /// <param name="panelApplicationIds">The panel applications to retrieve the corresponding report information</param>
        /// <returns>application report information as alist of ReportAppInfo objects</returns>
        public IEnumerable<IReportAppInfo> GetAppReportInfo(int[] panelApplicationIds)
        {
            ValidateGetReportAppInfoParameters(panelApplicationIds);

            var results = UnitOfWork.ApplicationWorkflowRepository.GetReportAppInfoList(panelApplicationIds);

            return results.ModelList;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Tests service parameters for SetApplicationStateParametersOk
        /// </summary>
        /// <param name="aLogNumber">Application identifier</param>
        /// <param name="candidateStatus">Status identifier</param>
        /// <param name="userId">User identifier</param>
        /// <returns>True if the parameters are valid; false otherwise</returns>
        protected bool IsSetApplicationStateParametersValid(DataLayer.Application application, int state, int userId)
        {
            return (
                    (application != null) &&
                    (state > 0) &&
                    (userId > 0)
                   );
        }
        /// <summary>
        /// Returns an application by log number
        /// </summary>
        /// <param name="logNumber">Log number</param>
        /// <returns>Application</returns>
        private DataLayer.Application GetApplication(string logNumber)
        {
            DataLayer.Application result = UnitOfWork.ApplicationRepository.GetByLogNumber(logNumber);
            if (result == null)
            {
                result = new DataLayer.Application { LogNumber = logNumber };
                UnitOfWork.ApplicationRepository.Add(result);
                UnitOfWork.Save();
            }

            return result;
        }
        /// <summary>
        /// Returns an application by application id
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <returns>Application</returns>
        private DataLayer.Application GetApplication(int applicationId)
        {
            return UnitOfWork.ApplicationRepository.GetByID(applicationId);
        }
        /// <summary>
        /// Save the change to an application's default workflow.
        /// </summary>
        /// <param name="applicationId">Application identification</param>
        /// <param name="newWorkflowId">Workflow identifier</param>
        /// <param name="userId">User requesting the change</param>
        private void SaveWorkflowChanges(DataLayer.Application application, int newWorkflowId, int userId)
        {
            DataLayer.ApplicationDefaultWorkflow workflow = application.FindDefaultWorkflow();

            if (workflow != null)
            {
                workflow.Update(newWorkflowId, userId);
                UnitOfWork.ApplicationDefaultWorkflowRepository.Update(workflow);
            }
            else
            {
                workflow = application.AddDefaultWorkflow(newWorkflowId, userId);
                UnitOfWork.ApplicationDefaultWorkflowRepository.Add(workflow);
            }
        }
        /// <summary>
        /// Validates the parameters for GetReportAppInfo().
        /// </summary>
        /// <returns>applicationIds as an array of integers</returns>
        private void ValidateGetReportAppInfoParameters(int[] panelApplicationIds)
        {
            if (panelApplicationIds == null || !Array.TrueForAll(panelApplicationIds, x => x > 0))
            {
                bool isNull = panelApplicationIds == null;
                bool invalidElement = !isNull;
                int theCount = (invalidElement) ? panelApplicationIds.Length : 0;
                string msg = string.Format(ExceptionMessages.GetReportAppInfoParameters, isNull, (isNull) ? 0 : theCount, invalidElement);
                throw new ArgumentException(string.Format(ExceptionMessages.GetReportAppInfoParameters, isNull, (isNull) ? 0 : theCount, invalidElement));
            }
        }
        #endregion
    }
}
