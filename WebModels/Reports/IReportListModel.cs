namespace Sra.P2rmis.WebModels.Reports
{
    /// <summary>
    /// Definition of data fields returned from the GetMenu Linq query.
    /// </summary>  
    public interface IReportListModel
    {
        /// <summary>
        /// the unique identifier of the report group the report belongs to
        /// </summary>
        int ReportGroupId { get; set; }
        /// <summary>
        /// the report group name that the report belongs to
        /// </summary>
        string ReportGroupName { get; set; }
        /// <summary>
        /// the reports group description
        /// </summary>
        string ReportGroupDescription { get; set; }
        /// <summary>
        /// Report Group Sort order
        /// </summary>
        int ReportGroupSortOrder { get; set; }
        /// <summary>
        /// Report unique identifier
        /// </summary>
        int ReportId { get; set; }

        /// <summary>
        /// Name of the report
        /// </summary>
        string ReportName { get; set; }
        /// <summary>
        /// Name of the SQL Reporting Service report file
        /// </summary>
        string ReportFileName { get; set; }
        /// <summary>
        /// the report description
        /// </summary>
        string ReportDescription { get; set; }
        /// <summary>
        /// Permission (if any) required to execute the report
        /// </summary>
        string RequiredPermission { get; set; }
        /// <summary>
        /// the breadcrumb of the report that will be search-able in the search for reports functionality
        /// </summary>
        string ReportBreadcrumb { get; }

        /// <summary>
        /// the bool value for displaying program parameter 
        /// </summary>
        bool ShowProgram { get; set; }

        /// <summary>
        /// the bool value true if program is required 
        /// </summary>
        bool IsProgramRequired { get; set; }

        /// <summary>
        /// the bool value true if program is multiselect
        /// </summary>
        bool IsProgramMultiSelect { get; set; }

        /// <summary>
        /// the bool value for displaying year parameter 
        /// </summary>
        bool ShowYear { get; set; }

        /// <summary>
        /// the bool value true if year is required 
        /// </summary>
        bool IsYearRequired { get; set; }

        /// <summary>
        /// the bool value true if year is multiselect
        /// </summary>
        bool IsYearMultiSelect { get; set; }

        /// <summary>
        /// the bool value for displaying Panel parameter 
        /// </summary>
        bool ShowPanel { get; set; }

        /// <summary>
        /// the bool value true if Panel is required 
        /// </summary>
        bool IsPanelRequired { get; set; }

        /// <summary>
        /// the bool value true if Panel is multiselect
        /// </summary>
        bool IsPanelMultiSelect { get; set; }

        /// <summary>
        /// the bool value for displaying Cycle parameter 
        /// </summary>
        bool ShowCycle { get; set; }

        /// <summary>
        /// the bool value true if Cycle is required 
        /// </summary>
        bool IsCycleRequired { get; set; }

        /// <summary>
        /// the bool value true if Cycle is multiselect
        /// </summary>
        bool IsCycleMultiSelect { get; set; }

        /// <summary>
        /// the bool value for displaying Meeting type parameter 
        /// </summary>
        bool ShowMeetingType { get; set; }

        /// <summary>
        /// the bool value true if Meeting type is required 
        /// </summary>
        bool IsMeetingTypeRequired { get; set; }

        /// <summary>
        /// the bool value true if Meeting type is multiselect
        /// </summary>
        bool IsMeetingTypeMultiSelect { get; set; }

        /// <summary>
        /// the bool value for displaying Meeting parameter 
        /// </summary>
        bool ShowMeeting { get; set; }

        /// <summary>
        /// the bool value true if Meeting is required 
        /// </summary>
        bool IsMeetingRequired { get; set; }

        /// <summary>
        /// the bool value true if Meeting is multiselect
        /// </summary>
        bool IsMeetingMultiSelect { get; set; }

        /// <summary>
        /// the report paramater group identifier
        /// </summary>
        int? ReportParameterGroupId { get; set; }
    }
}
