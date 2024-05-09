using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.ResultModels.Reports
{
    /// <summary>
    /// comment:rdl
    /// </summary>
    public class ReportDescriptionResultModel : IReportDescriptionResultModel
    {
        #region Constructor
        /// <summary>
        /// Default constructor. 
        /// </summary>
        public ReportDescriptionResultModel()
        {
            //
            // By default we always return a list, even if it is 0 in length
            //
            this.ModelList = new List<IReportDescriptionModel>();
        }
        #endregion
        /// <summary>
        /// List of model containing the fiscal years for one or more programs
        /// </summary>
        public IEnumerable<IReportDescriptionModel> ModelList { get; set; }
    }
}
