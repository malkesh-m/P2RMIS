using System;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.Reports;

namespace Sra.P2rmis.Bll
{
    public class ReportViewerService : IReportViewerService
    {
        #region Properties
        /// <summary>
        /// Indicates if the object has been disposed but not garbage collected.
        /// </summary>
        private bool _disposed;
        protected IUnitOfWork UnitOfWork { get; set; }
        #endregion

        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public ReportViewerService()
        {
            UnitOfWork = new UnitOfWork();
        }
        /// <summary>
        /// Dispose of the service.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Dispose of the service
        /// </summary>
        /// <param name="disposing">Indicates if the object should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            ///
            /// if the object has not been disposed & we should dispose the object
            /// 
            if ((!this._disposed) && (disposing))
            {
                if (UnitOfWork != null)
                {
                    ((IDisposable)UnitOfWork).Dispose();
                    this._disposed = true;
                }
            }
        }
        #endregion

        #region Provided Services
        /// <summary>
        /// Retrieves report metadata for a single report.
        /// </summary>
        /// <param name="reportId">Unique identifier for a report</param>
        /// <returns>Report metadata for a single report</returns>
        public IReportModel GetReportInfo(int clientId)
        {            
            //
            // Call the DL and retrieve report information
            //
            var result = UnitOfWork.ReportViewerRepository.GetReportInfo(clientId);
            var o = new ReportModel(result.ReportId, result.ReportName, result.ReportFileName, result.ReportPermissionName, result.ReportParameterGroupId);
            return o;
        }
        /// <summary>
        /// Create a log entry for the summary report generation and add it to the table.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="applicationWorkflowId">Application workflow identifier</param>
        public void LogReportInfo(int userId, int applicationWorkflowId)
        {
            //
            // Get the information needed for the log entity
            //
            ApplicationWorkflow workflow = UnitOfWork.ApplicationWorkflowRepository.GetByID(applicationWorkflowId);
            bool isComplete = workflow.IsComplete();
            int applicationWorkflowStepEntityId = (!isComplete) ? workflow.CurrentStep().ApplicationWorkflowStepId : workflow.LastStep().ApplicationWorkflowStepId;
            //
            // Create the log entity and populate it.
            //
            var applicationSummaryLogEntity = new ApplicationSummaryLog();
            applicationSummaryLogEntity.Populate(userId, applicationWorkflowStepEntityId, isComplete);

            Helper.UpdateCreatedFields(applicationSummaryLogEntity, userId);
            Helper.UpdateModifiedFields(applicationSummaryLogEntity, userId);
            //
            // Now add the entry to the log file and save it to the database.
            //
            UnitOfWork.ApplicationSummaryLogRepository.Add(applicationSummaryLogEntity);
            UnitOfWork.Save();
        }
        #endregion
    }
}
