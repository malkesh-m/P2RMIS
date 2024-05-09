namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Result Model that represents a Report object
    /// </summary>
    public class ReportResultModel : IReportResultModel
    {
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
        /// <summary>
        /// The name of the permission associated with the report
        /// </summary>
        public string ReportPermissionName { get; internal set; }
        /// <summary>
        /// The group id of the parameter used to run the report
        /// </summary>
        public int ReportParameterGroupId { get; internal set; }

        #endregion
    }
}
