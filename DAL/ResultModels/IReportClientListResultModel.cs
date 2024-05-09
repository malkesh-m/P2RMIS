using System.Collections.Generic;

namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Interface defining the results when retrieving the client details of a list of clients.
    /// </summary>
    public interface IReportClientListResultModel
    {
        /// <summary>
        /// List of a clients programs
        /// </summary>
        IEnumerable<ReportClientModel> ClientList { get; set; }
    }
}
