using System.Collections.Generic;

namespace Sra.P2rmis.Web.UI.Models
{
    public class RunReportViewModel
    {
        #region Properties

        public int ReportGroupId { get; set; }
        public int ReportId { get; set; }
        public List<string> ProgramList { get; set; }
        public List<string> FyList { get; set; }
        public List<int> PanelsList { get; set; }

        #endregion
    }
}