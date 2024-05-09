using System;
using Sra.P2rmis.WebModels.Reports;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Service providing access to report objects
    /// </summary>
    public interface IReportViewerService : IDisposable
    {
        /// <summary>
        /// Retrieves report metadata for a single report.
        /// </summary>
        /// <param name="reportId">Unique identifier for a report</param>
        /// <returns>Report metadata for a single report</returns>
        IReportModel GetReportInfo(int reportId);
        /// <summary>
        /// Logs report information for a single report.
        /// </summary>
        /// <param name="userId"><User entity identifier/param>
        /// <param name="applicationWorkflowId">Workflow entity identifier</param>
        void LogReportInfo(int userId, int applicationWorkflowId);
    }
}
