using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.Reports;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Database access methods for report server requests.
    /// </summary>
    public class ReportRepository :  IReportRepository
    {
        #region Attributes
        /// <summary>
        /// P2RMIS database context
        /// </summary>
        private P2RMISNETEntities Context { get; set; }
        #endregion
        #region Constructor; Setup and Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ReportRepository(P2RMISNETEntities context)
        {
            this.Context = context;
        }
        #endregion
        #region Repository Methods
        /// <summary>
        /// Retrieves a list of all reports.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of ReportListModels representing the available reports
        /// </returns>
        public ResultModel<IReportListModel> GetMenu(int userId)
        {
            ResultModel<IReportListModel> result = new ResultModel<IReportListModel>();
            var menu = RepositoryHelpers.GetMenu(Context, userId);
            if (menu != null)
            {
                result.ModelList = menu;
            }
            return result;
        }
        #endregion
    }
}
