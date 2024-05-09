namespace Sra.P2rmis.WebModels.Reports
{
    /// <summary>
    /// Report specific menu item information
    /// </summary>
    public class ReportMenuItem : MenuItem, IReportMenuItem
    {
        #region Attributes
        /// <summary>
        /// Name of the SQL Reporting Service report file
        /// </summary>
        public string ReportFileName { get; set; }
        #endregion 

    }
}
