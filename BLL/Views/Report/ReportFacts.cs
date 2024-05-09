using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll.Views
{
    public class ReportFacts : IReportFacts
    {
        #region Constructor
        /// <summary>
        /// TODO: Document
        /// </summary>
        /// <param name="item"></param>
        internal ReportFacts(IReportResultModel item)
        {
            this.ReportId = item.ReportId;
            this.ReportName = item.ReportName;
            this.ReportFileName = item.ReportFileName;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Report unique identifier
        /// </summary>
        public int ReportId { get; internal set; }
        /// <summary>
        ///  Report Name
        /// </summary>
        public string ReportName { get; internal set; }
        /// <summary>
        /// Report File name
        /// </summary>
        public string ReportFileName { get; internal set; }
        #endregion
    }
}
