using System.Collections.Generic;

namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// The results when retrieving the client program list for a client.
    /// </summary>
    public class ReportClientListResultModel : IReportClientListResultModel
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public ReportClientListResultModel()
        {
            //
            // Return list of client data
            //
            this.ClientList = new List<ReportClientModel>();
        }
        #endregion
        /// <summary>
        /// List of all clients
        /// </summary>
        public IEnumerable<ReportClientModel> ClientList { get; set; }
    }
}
