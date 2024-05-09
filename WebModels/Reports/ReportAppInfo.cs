namespace Sra.P2rmis.WebModels.Reports
{
    /// <summary>
    /// Definition of data fields required for summary report generation.
    /// </summary>  
    public class ReportAppInfo : IReportAppInfo
    {
        #region attributes
        /// <summary>
        /// The abrevation of the program being reported
        /// </summary>
        public string ProgramAbrv { get; set; }
        /// <summary>
        /// The spplication's fiscal year
        /// </summary>
        public string FiscalYear { get; set; }
        /// <summary>
        /// The application's current cycle
        /// </summary>
        public int Cycle { get; set; }
        /// <summary>
        /// The application's log number
        /// </summary>
        public string AppLogNumber { get; set; }
        /// <summary>
        /// The application workflow id
        /// </summary>
        public int ApplicationWorkflowId { get; set; }
        /// <summary>
        /// File name of the report
        /// </summary>
        public string ReportFileName { get; set; }
        #endregion
    }
}
