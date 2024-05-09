using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.Reports;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Database access methods for report server requests.
    /// </summary>
    public interface IReportRepository
    {
        /// <summary>
        /// Retrieves a list of all reports.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of ReportListModels representing the available reports
        /// </returns>
        ResultModel<IReportListModel> GetMenu(int userId);
    }
}
