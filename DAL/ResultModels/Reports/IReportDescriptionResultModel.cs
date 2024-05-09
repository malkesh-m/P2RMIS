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
    public interface IReportDescriptionResultModel
    {
        /// <summary>
        /// List of model containing the fiscal years for one or more programs
        /// </summary>
        IEnumerable<IReportDescriptionModel> ModelList { get; set; }
    }
}
