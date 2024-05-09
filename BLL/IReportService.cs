using System;
using System.Collections.Generic;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Bll.Views.Report;
using Sra.P2rmis.WebModels.Reports;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Service providing access to report objects
    /// </summary>
    public interface IReportService : IDisposable
    {
        /// <summary>
        /// Retrieves a list of all reports.
        /// </summary>
        /// <returns>List of ReportListModels representing the available reports</returns>
        Container<IReportListModel> GetMenu(int userId);
    }
}
