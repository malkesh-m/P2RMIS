namespace Sra.P2rmis.Dal.ResultModels.Reports
{
    /// <summary>
    /// comment:rdl
    /// </summary>
    public interface IReportDescriptionModel
    {
        /// <summary>
        /// ReportGroup unique identifier
        /// </summary>
        int ReportGroupId { get; set; }
        /// <summary>
        /// Group name of the report selected.
        /// </summary>
        string GroupName { get; set; }
        /// <summary>
        /// Report description
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// Report group presentation order
        /// </summary>
        int SortOrder { get; set; }
    }
}
