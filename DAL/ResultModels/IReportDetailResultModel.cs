using System.Collections.Generic;

namespace Sra.P2rmis.Dal.ResultModels 
{
    public interface IReportDetailResultModel 
    {
        IEnumerable<ReportResultModel> Reports { get; } 
    }
}
