using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Bll.Views.Criteria
{
    /// <summary>
    /// Container returning the fiscal years for one or more programs
    /// </summary>
    public interface IFiscalYearContainer
    {
        /// <summary>
        /// The client's programs
        /// </summary>
        IEnumerable<FyModel> ModelList { get; set; }
    }
}
