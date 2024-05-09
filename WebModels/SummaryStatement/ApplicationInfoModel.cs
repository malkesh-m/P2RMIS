using System.Collections.Generic;
using Sra.P2rmis.WebModels.Files;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    public class ApplicationInfoModel : IApplicationInfoModel
    {
        public ApplicationInfoModel()
        {
            theApplicationDetail = new ApplicationDetailModel();
            theApplicationFileInfo = new List<IFileInfoModel>();
        }
        ///// <summary>
        ///// theApplicationDetail - Application details
        ///// </summary>
        public IApplicationDetailModel theApplicationDetail { get; set; }

        public List<IFileInfoModel> theApplicationFileInfo { get; set; }


    }
}
