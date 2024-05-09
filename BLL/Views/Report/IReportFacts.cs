namespace Sra.P2rmis.Bll.Views
{
    public interface IReportFacts
    {
        #region Properties
        /// <summary>
        /// Report unique identifier
        /// </summary>
        int ReportId { get; }
        /// <summary>
        ///  Report Name
        /// </summary>
        string ReportName { get; }
        /// <summary>
        /// Report File name
        /// </summary>
        string ReportFileName { get; }
        #endregion
    }
}
