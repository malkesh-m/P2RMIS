using System.Collections.Generic;
using Sra.P2rmis.WebModels.Files;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    public interface IApplicationInfoModel
    {
        /// <summary>
        /// theApplicationDetail - Application details
        /// </summary>
        IApplicationDetailModel theApplicationDetail { get; set; }

        List<IFileInfoModel> theApplicationFileInfo { get; set; }
    }
}
