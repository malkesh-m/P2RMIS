using System.Collections.Generic;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.Criteria;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Database access methods for report view server requests.
    /// </summary>
    public interface IReportViewerRepository
    {
        /// <summary>
        /// Retrieves details of a single report.
        /// </summary>
        /// <param name="reportId">Unique identifier for a single report</param>
        /// <returns>Details for a given report id</returns>
        IReportResultModel GetReportInfo(int reportId);
        IEnumerable<ClientMeetingModel> GetMeetings(List<string> fiscalYear);
    }
}
