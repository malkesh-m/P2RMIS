namespace Sra.P2rmis.Dal.ResultModels.Reports
{
    /// <summary>
    /// comment:rdl
    /// </summary>
    public class ReportDescriptionModel : IReportDescriptionModel
    {
        /// ReportGroup unique identifier
        /// </summary>
        public int ReportGroupId { get; set; }
        /// <summary>
        /// Group name of the report selected.
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// Report description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Report group presentation order
        /// </summary>
        public int SortOrder { get; set; }
    }
}
