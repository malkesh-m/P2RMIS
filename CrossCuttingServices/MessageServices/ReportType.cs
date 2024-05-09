using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.CrossCuttingServices.MessageServices
{
    /// <summary>
    /// report parameter group
    /// </summary>
    /// <remarks></remarks>
    public enum ReportParameterGroup
    {
        /// <summary>
        /// Panel paramter group
        /// </summary>
        PanelReportGroupId = 1,
        /// <summary>
        /// Cycle parameter group
        /// </summary>
        CycleReportGroupId = 2,
        /// <summary>
        /// Meeting parameter group
        /// </summary>
        MeetingReportGroupId = 3,
        /// <summary>
        /// Meeting type parameter group
        /// </summary>
        MeetingTypeReportGroupId = 7,
        /// <summary>
        /// Default parameter group
        /// </summary>
        DefaultReportGroupId = 0
    }

   
}
