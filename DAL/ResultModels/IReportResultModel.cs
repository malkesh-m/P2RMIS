namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Result Model that represents a Report object
    /// </summary>
    public interface IReportResultModel 
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
        /// <summary>
        /// The name of the permission associated with the report
        /// </summary>
        string ReportPermissionName { get; }
        /// <summary>
        /// The group of the parameter used to run the report
        /// </summary>
        int ReportParameterGroupId { get; }

         #endregion
    }
}
