using Sra.P2rmis.WebModels;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Linq implementation of search result methods
    /// </summary>
    internal partial class RepositoryHelpers
    {

        /// <summary>
        /// The internal constants for Report Parameter Id
        /// </summary>
        internal const int ParameterPanelId = 1;
        internal const int ParameterCycleId = 2;
        internal const int ParameterMeetingId = 3;
        internal const int ParameterProgramId = 5;
        internal const int ParameterYearId = 6;
        internal const int ParameterMeetingTypeId = 7;

        /// <summary>
        /// Retrieves a list of all reports.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Enumeration of ReportListModels
        /// </returns>
        internal static IEnumerable<ReportListModel> GetMenu(P2RMISNETEntities context, int userId)
        {
            
                var reportParameters = from reportParameter in context.ReportParameters
                join panelParam in context.ReportParameters on new { p1 = reportParameter.ReportId, p2 = ParameterPanelId } equals new { p1 = panelParam.ReportId, p2 = panelParam.ParameterId } into panelParamMap
                from panelParam in panelParamMap.DefaultIfEmpty()
                join cycleParam in context.ReportParameters on new { p1 = reportParameter.ReportId, p2 = ParameterCycleId } equals new { p1 = cycleParam.ReportId, p2 = cycleParam.ParameterId } into CycleParamMap
                from cycleParam in CycleParamMap.DefaultIfEmpty()
                join meetingTypeParam in context.ReportParameters on new { p1 = reportParameter.ReportId, p2 = ParameterMeetingTypeId } equals new { p1 = meetingTypeParam.ReportId, p2 = meetingTypeParam.ParameterId } into MeetingTypeParamMap
                from meetingTypeParam in MeetingTypeParamMap.DefaultIfEmpty()
                join meetingParam in context.ReportParameters on new { p1 = reportParameter.ReportId, p2 = ParameterMeetingId } equals new { p1 = meetingParam.ReportId, p2 = meetingParam.ParameterId } into MeetingParamMap
                from meetingParam in MeetingParamMap.DefaultIfEmpty()
                join programParam in context.ReportParameters on new { p1 = reportParameter.ReportId, p2 = ParameterProgramId } equals new { p1 = programParam.ReportId, p2 = programParam.ParameterId } into ProgramParamMap
                from programParam in ProgramParamMap.DefaultIfEmpty()
                join yearParam in context.ReportParameters on new { p1 = reportParameter.ReportId, p2 = ParameterYearId } equals new { p1 = yearParam.ReportId, p2 = yearParam.ParameterId } into YearParamMap
                from yearParam in YearParamMap.DefaultIfEmpty()
                select new
                {
                    ReportId = reportParameter.ReportId,
                    ShowPanel = panelParam.ParameterId > 0 ? true : false,
                    isPanelRequired = panelParam.ParameterId > 0 ? panelParam.Required : false,
                    isPanelMultiSelect = panelParam.ParameterId > 0 ? panelParam.MultiSelect : false,

                    ShowCycle = cycleParam.ParameterId > 0 ? true : false,
                    isCycleRequired = cycleParam.ParameterId > 0 ? cycleParam.Required : false,
                    isCycleMultiSelect = cycleParam.ParameterId > 0 ? cycleParam.MultiSelect : false,

                    ShowMeetingType = meetingTypeParam.ParameterId > 0 ? true : false,
                    isMeetingTypeRequired = meetingTypeParam.ParameterId > 0 ? meetingTypeParam.Required : false,
                    isMeetingTypeMultiSelect = meetingTypeParam.ParameterId > 0 ? meetingTypeParam.MultiSelect : false,

                    ShowMeeting = meetingParam.ParameterId > 0 ? true : false,
                    isMeetingRequired = meetingParam.ParameterId > 0 ? meetingParam.Required : false,
                    isMeetingMultiSelect = meetingParam.ParameterId > 0 ? meetingParam.MultiSelect : false,

                    ShowProgram = programParam.ParameterId > 0 ? true : false,
                    isProgramRequired = programParam.ParameterId > 0 ? programParam.Required : false,
                    isProgramMultiSelect = programParam.ParameterId > 0 ? programParam.MultiSelect : false,

                    ShowYear = yearParam.ParameterId > 0 ? true : false,
                    isYearRequired = yearParam.ParameterId > 0 ? yearParam.Required : false,
                    isYearMultiSelect = yearParam.ParameterId > 0 ? yearParam.MultiSelect : false,
                                
                };

            var result = from reportGroup in context.LookupReportGroups
                         join report in context.Reports on reportGroup.ReportGroupId equals report.ReportGroupId
                         join permission in context.ReportPermissions on report.ReportId equals permission.ReportId
                         join parameter in reportParameters on report.ReportId equals parameter.ReportId
                         join systemOperation in context.SystemOperations on permission.OperationName equals systemOperation.OperationName
                         join taskOperation in context.TaskOperations on systemOperation.SystemOperationId equals taskOperation.SystemOperationId
                         join roleTask in context.RoleTasks on taskOperation.SystemTaskId equals roleTask.SystemTaskId
                         join userRole in context.UserSystemRoles on roleTask.SystemRoleId equals userRole.SystemRoleId
                         where userRole.UserID == userId && report.Active
                         orderby reportGroup.SortOrder descending, report.ReportName
                         select new ReportListModel
                         {
                             ReportGroupId = reportGroup.ReportGroupId,
                             ReportGroupName = reportGroup.ReportGroupName,
                             ReportGroupDescription = reportGroup.ReportDescription,
                             ReportGroupSortOrder = reportGroup.SortOrder,
                             ReportId = report.ReportId,
                             ReportName = report.ReportName,
                             ReportDescription = report.ReportDescription,
                             RequiredPermission = permission.OperationName,

                             ShowPanel = parameter.ShowPanel,
                             IsPanelRequired = parameter.isPanelRequired,
                             IsPanelMultiSelect = parameter.isPanelMultiSelect,

                             ShowCycle = parameter.ShowCycle,
                             IsCycleRequired = parameter.isCycleRequired,
                             IsCycleMultiSelect = parameter.isCycleMultiSelect,

                             ShowMeetingType = parameter.ShowMeetingType,
                             IsMeetingTypeRequired = parameter.isMeetingTypeRequired,
                             IsMeetingTypeMultiSelect = parameter.isMeetingTypeMultiSelect,

                             ShowMeeting = parameter.ShowMeeting,
                             IsMeetingRequired = parameter.isMeetingRequired,
                             IsMeetingMultiSelect = parameter.isMeetingMultiSelect,

                             ShowProgram = parameter.ShowProgram,
                             IsProgramRequired = parameter.isProgramRequired,
                             IsProgramMultiSelect = parameter.isProgramMultiSelect,

                             ShowYear = parameter.ShowYear,
                             IsYearRequired = parameter.isYearRequired,
                             IsYearMultiSelect = parameter.isYearMultiSelect,

                             ReportParameterGroupId = report.ReportParameterGroupId
                         };

            return result.Distinct();
        }
    }
}
