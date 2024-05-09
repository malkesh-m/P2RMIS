using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Reports;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// ReportService provides services and business logic specific to the Report Application.
    /// </summary>
    public class ReportService : ServerBase, IReportService
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public ReportService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion

        #region Provided Services
        /// <summary>
        /// Retrieves a list of all reports.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Container holding a list of ReportListModels representing the available reports
        /// </returns>
        public Container<IReportListModel> GetMenu(int userId)
        {
            ///
            /// Set up default return
            /// 
            Container<IReportListModel> container = new Container<IReportListModel>();
            //
            // Call the DL and retrieve any programs for this client
            //
            var results = UnitOfWork.ReportRepository.GetMenu(userId);
            //
            // Set the results into the container
            //
            container.SetModelList(results);

            return container;
        }
        #endregion
    }
}
