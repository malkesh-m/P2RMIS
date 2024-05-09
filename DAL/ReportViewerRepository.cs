using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.Criteria;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Database access methods for report view server requests.
    /// </summary>
    public class ReportViewerRepository : GenericRepository<ReportResultModel>, IReportViewerRepository
    {
        #region Constructor; Setup and Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ReportViewerRepository(P2RMISNETEntities context)
            : base(context)
        {

        }

        public IEnumerable<ClientMeetingModel> GetMeetings(List<string> fiscalYear)
        {
            return RepositoryHelpers.GetMeetingsByYear(context,fiscalYear);
        }


        #endregion

        #region Repository Methods
        /// <summary>
        /// Retrieves details of a single report.
        /// </summary>
        /// <param name="reportId">Unique identifier for a single report</param>
        /// <returns>Details of a supplied report id</returns>
        public IReportResultModel GetReportInfo(int reportId)
        {
            IReportResultModel result = RepositoryHelpers.GetReportInfo(context, reportId);
            return result;
        }
        #endregion
    }
}
