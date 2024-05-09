using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.Criteria;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Linq implementation of search result methods
    /// </summary>
    internal partial class RepositoryHelpers
    {
        /// <summary>
        /// Retrieves report information for a single report.
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="reportId">Unique identifier for a report</param>
        /// <returns></returns>
        internal static ReportResultModel GetReportInfo(P2RMISNETEntities context, int reportId)
        {
            var result = from report in context.Reports
                         join reportParam in context.ReportParameters on report.ReportId equals reportParam.ReportId

                         join parameterLkup in context.LookupReportParameters on reportParam.ParameterId equals parameterLkup.ParameterId
                         where report.ReportId == reportId
                         select new
                         {
                             ReportId = report.ReportId,
                             ReportName = report.ReportName,
                             ReportFileName = report.ReportFileName,
                             ReportPermissionName = report.ReportPermissions1.Select(x => x.OperationName),
                             ReportParameterGroupId =  report.ReportParameterGroupId,
                         };
            //Since we can't concatenate the operation names in linq to sql, we pull back as an enumerable first
            var resultFormatted = result.AsEnumerable().Select(x => new ReportResultModel
            {
                ReportId = x.ReportId,
                ReportName = x.ReportName,
                ReportFileName = x.ReportFileName,
                ReportPermissionName = string.Join(",", x.ReportPermissionName),
                ReportParameterGroupId = (int)x.ReportParameterGroupId
            }).FirstOrDefault();
            return resultFormatted;
        }

        /// <summary>
        /// get meeting type by year
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fiscalYear"></param>
        /// <returns></returns>
        internal static IEnumerable<ClientMeetingModel> GetMeetingsByYear(P2RMISNETEntities context, List<string> fiscalYear)
        {
            var result = from ClientMeeting in context.ClientMeetings
                         join ProgramMeeting in context.ProgramMeetings on ClientMeeting.ClientMeetingId equals ProgramMeeting
                             .ClientMeetingId into pm
                         from ProgramMeeting2 in pm.DefaultIfEmpty()
                         where fiscalYear.Contains(ProgramMeeting2.ProgramYear.Year)
                         select new ClientMeetingModel
                         {
                             ClientMeetingId = ClientMeeting.ClientMeetingId,
                             MeetingDescription = ClientMeeting.MeetingDescription,
                             Year = ProgramMeeting2.ProgramYear.Year,
                             MeetingTypeId = ClientMeeting.MeetingTypeId

                         };
            return result;
        }


        /// <summary>
        /// get all meeting by fiscal year
        /// </summary>
        /// <param name="fiscalYear"></param>
        /// <returns></returns>
        internal static IEnumerable<WebModels.Criteria.IClientMeetingModel> GetMeetings(P2RMISNETEntities context, List<string> fiscalYear)
        {
            var result = from ClientMeeting in context.ClientMeetings
                         select new
                         {
                             ClientMeetingId = ClientMeeting.ClientMeetingId

                         };

            return null;




        }
    }

}
