namespace Sra.P2rmis.WebModels.Reports
{
    /// <summary>
    /// Report specific menu item information
    /// </summary>
    public interface IReportMenuItem
    {
        #region Attributes
        /// <summary>
        /// Name of the SQL Reporting Service report file
        /// </summary>
        string ReportFileName { get; set; }
        #endregion
    }
}
