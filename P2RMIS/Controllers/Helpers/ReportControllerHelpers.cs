using System.Collections.Generic;
using System.Text;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.WebModels.Reports;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// Helper Methods for the ReportController
    /// </summary>
    public class ReportControllerHelpers
    {
        /// <summary>
        /// Separator for SQL report parameter values
        /// </summary>
        private static string Seperator = ",";
        /// <summary>
        /// SQLReporting ASPX control
        /// </summary>
        public static string Viewer = "../Reports/ReportViewer.aspx?";

        #region The Helpers
        /// <summary>
        /// Helper method to locate a specific report by report identifier
        /// </summary>
        /// <param name="reportId">Report identifier</param>
        /// <param name="reportList">List of ReportListModels</param>
        /// <returns>ReportListModel if the report is located; null otherwise</returns>
        public static IReportListModel FindReportListModel(int? reportId, IEnumerable<IReportListModel> reportList)
        {
            IReportListModel result = null;

            if ((reportId.HasValue) && (reportList != null))
            {
                List<IReportListModel> list = new List<IReportListModel>(reportList);
                result = list.Find(x => x.ReportId == reportId);
            }
            return result;
        }
        /// <summary>
        /// Constructs the string that is used to pass the selected
        /// application ids to SQL reporting service.
        /// </summary>
        /// <param name="applicationIds">List of one or more application identifier</param>
        /// <returns>Formatted query string</returns>
        internal static string BuildQueryString(string[] applicationIds)
        {
            return string.Format("&reportId=0&ApplicationWorkflowId={0}", string.Join(Seperator, applicationIds));
        }
        /// <summary>
        /// Constructs the string that is used to pass the selected
        /// application ids to SQL reporting service.
        /// </summary>
        /// <param name="reportAppInfo">List of one or more application identifier</param>
        /// <returns>Formatted query string</returns>
        internal static string BuildQueryString(List<IReportAppInfo> reportAppInfo)
        {
            StringBuilder formatedString = new StringBuilder();

            // string for each parameter
            StringBuilder formatedStringRptNum = new StringBuilder(string.Format("&{0}={1}", Invariables.UrlParameters.ReportId, 0));
            StringBuilder formatedStringFlowId = new StringBuilder("&" + Invariables.UrlParameters.ApplicationWorkflowId + "=");
            StringBuilder formatedStringLogNum = new StringBuilder("&" + Invariables.UrlParameters.ApplicationLogNumber + "=");
            StringBuilder formatedStringAbrv = new StringBuilder("&" + Invariables.UrlParameters.ProgramAbrv + "=");
            StringBuilder formatedStringCycle = new StringBuilder("&" + Invariables.UrlParameters.Cycle + "=");
            StringBuilder formatedStringFy = new StringBuilder("&" + Invariables.UrlParameters.FiscalYear + "=");
            StringBuilder formattedStringReportName = new StringBuilder("&" + Invariables.UrlParameters.TemplateName + "=");

            // build each parameter string
            foreach (ReportAppInfo info in reportAppInfo)
            {
                formatedStringFlowId.Append(string.Format("{0},", info.ApplicationWorkflowId));
                formatedStringLogNum.Append(string.Format("{0},", info.AppLogNumber));
                formatedStringAbrv.Append(string.Format("{0},", info.ProgramAbrv));
                formatedStringCycle.Append(string.Format("{0},", info.Cycle));
                formatedStringFy.Append(string.Format("{0},", info.FiscalYear));
                formattedStringReportName.Append(string.Format("{0},", info.ReportFileName));
            }

            formatedString.Append(formatedStringRptNum.ToString());
            formatedString.Append(formatedStringFlowId.ToString().TrimEnd(','));
            formatedString.Append(formatedStringLogNum.ToString().TrimEnd(','));
            formatedString.Append(formatedStringAbrv.ToString().TrimEnd(','));
            formatedString.Append(formatedStringCycle.ToString().TrimEnd(','));
            formatedString.Append(formatedStringFy.ToString().TrimEnd(','));
            formatedString.Append(formattedStringReportName.ToString().TrimEnd(','));
            return formatedString.ToString();
        }

        /// <summary>
        /// Builds the query string for a report such as contract which runs off of a single integer parameter.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="reportName">Name of the report.</param>
        /// <returns></returns>
        internal static string BuildQueryString(int panelUserAssignmentId, string reportName)
        {
            return string.Format("&reportName={0}&panelUserAssignmentId={1}", "Contract_CDMRP", "482");
        }
        #endregion
    }
}