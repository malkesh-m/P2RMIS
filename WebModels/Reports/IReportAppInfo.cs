namespace Sra.P2rmis.WebModels.Reports
{
    /// <summary>
    /// Definition of data fields required for summary report generation.
    /// </summary>  
    public interface IReportAppInfo
    {
        #region attributes
        /// <summary>
        /// The abrevation of the program being reported
        /// </summary>
        string ProgramAbrv { get; set; }
        /// <summary>
        /// The spplication's fiscal year
        /// </summary>
        string FiscalYear { get; set; }
        /// <summary>
        /// The application's current cycle
        /// </summary>
        int Cycle { get; set; }
        /// <summary>
        /// The application's log number
        /// </summary>
        string AppLogNumber { get; set; }
        /// <summary>
        /// The application workflow id
        /// </summary>
        int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// File name of the report
        /// </summary>
        string ReportFileName { get; set; }
        #endregion
    }
}
