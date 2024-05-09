using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.Reports
{
    public interface IReportModel
    {
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
        /// The group of the permission associated with the report
        /// </summary>
        int ReportParameterGroupId { get; }

    }
    public class ReportModel : IReportModel
    {
        public ReportModel(int reportId, string reportName, string reportFileName, string reportPermissionName, int reportParameterGroupId)
        {
            ReportId = reportId;
            ReportName = reportName;
            ReportFileName = reportFileName;
            ReportPermissionName = reportPermissionName;
            ReportParameterGroupId = reportParameterGroupId;
        }
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
        /// The group of the permission associated with the report
        /// </summary>
        public int ReportParameterGroupId { get; internal set; }
    }
}
