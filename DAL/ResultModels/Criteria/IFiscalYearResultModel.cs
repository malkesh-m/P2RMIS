using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Dal.ResultModels.Criteria
{
    /// <summary>
    /// Data to identify a single program fiscal year
    /// </summary>
    public interface IFiscalYearResultModel
    {
        /// <summary>
        /// List of model containing the fiscal years for one or more programs
        /// </summary>
        IEnumerable<FyModel> ModelList { get; set; }
    }
}
