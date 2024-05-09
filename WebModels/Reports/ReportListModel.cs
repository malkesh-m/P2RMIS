using Sra.P2rmis.WebModels.Reports;

// RDL: FIX THIS !! IT IS IN THE WRONG NAME SPACE !!
namespace Sra.P2rmis.WebModels
{
    /// <summary>
    /// Definition of data fields returned from the GetMenu Linq query.
    /// </summary>
    public class ReportListModel : IReportListModel
    {
        /// <summary>
        /// the unique identifier of the report group the report belongs to
        /// </summary>
        public int ReportGroupId { get; set; }
        /// <summary>
        /// the report group name that the report belongs to
        /// </summary>
        public string ReportGroupName { get; set; }
        /// <summary>
        /// the reports group description
        /// </summary>
        public string ReportGroupDescription { get; set; }
        /// <summary>
        /// Report Group Sort order
        /// </summary>
        public int ReportGroupSortOrder { get; set; }
        /// <summary>
        /// Report unique identifier
        /// </summary>
        public int ReportId { get; set; }

        /// <summary>
        /// Name of the report
        /// </summary>
        public string ReportName { get; set; }
        /// <summary>
        /// Name of the SQL Reporting Service report file
        /// </summary>
        public string ReportFileName { get; set; }
        /// <summary>
        /// the report description
        /// </summary>
        public string ReportDescription { get; set; }
        /// <summary>
        /// Permission (if any) required to execute the report
        /// </summary>
        public string RequiredPermission { get; set; }
        /// <summary>
        /// the breadcrumb of the report that will be search-able in the search for reports functionality
        /// </summary>
        public string ReportBreadcrumb { get { return ReportGroupName + " > " + ReportName; } }

        /// <summary>
        /// the bool value for displaying program parameter 
        /// </summary>
        public bool ShowProgram { get; set; }

        /// <summary>
        /// the bool value true if program is required 
        /// </summary>
        public bool IsProgramRequired { get; set; }

        /// <summary>
        /// the bool value true if program is multiselect
        /// </summary>
        public bool IsProgramMultiSelect { get; set; }

        /// <summary>
        /// the bool value for displaying year parameter 
        /// </summary>
        public bool ShowYear { get; set; }

        /// <summary>
        /// the bool value true if year is required 
        /// </summary>
        public bool IsYearRequired { get; set; }

        /// <summary>
        /// the bool value true if year is multiselect
        /// </summary>
        public bool IsYearMultiSelect { get; set; }

        /// <summary>
        /// the bool value for displaying Panel parameter 
        /// </summary>
        public bool ShowPanel { get; set; }

        /// <summary>
        /// the bool value true if Panel is required 
        /// </summary>
        public bool IsPanelRequired { get; set; }

        /// <summary>
        /// the bool value true if Panel is multiselect
        /// </summary>
        public bool IsPanelMultiSelect { get; set; }

        /// <summary>
        /// the bool value for displaying Cycle parameter 
        /// </summary>
        public bool ShowCycle { get; set; }

        /// <summary>
        /// the bool value true if Cycle is required 
        /// </summary>
        public bool IsCycleRequired { get; set; }

        /// <summary>
        /// the bool value true if Cycle is multiselect
        /// </summary>
        public bool IsCycleMultiSelect { get; set; }

        /// <summary>
        /// the bool value for displaying Meeting type parameter 
        /// </summary>
        public bool ShowMeetingType { get; set; }

        /// <summary>
        /// the bool value true if Meeting type is required 
        /// </summary>
        public bool IsMeetingTypeRequired { get; set; }

        /// <summary>
        /// the bool value true if Meeting type is multiselect
        /// </summary>
        public bool IsMeetingTypeMultiSelect { get; set; }
        
        /// <summary>
        /// the bool value for displaying Meeting parameter 
        /// </summary>
        public bool ShowMeeting { get; set; }

        /// <summary>
        /// the bool value true if Meeting is required 
        /// </summary>
        public bool IsMeetingRequired { get; set; }

        /// <summary>
        /// the bool value true if Meeting is multiselect
        /// </summary>
        public bool IsMeetingMultiSelect { get; set; }

        /// <summary>
        /// the report paramater group identifier
        /// </summary>
        public int? ReportParameterGroupId { get; set; }
        

    }
}
