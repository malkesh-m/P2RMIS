using System.Collections.Generic;

namespace Sra.P2rmis.Dal.ResultModels 
{
    public class ReportDetailResultModel : IReportDetailResultModel
    {
        public IEnumerable<ReportResultModel> Reports { get; internal set; } 
    }
}
