using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Dal.ResultModels.Criteria
{
    /// <summary>
    /// Container holding the results of a panel query
    /// </summary>
    public interface IPanelResultModel
    {
        /// <summary>
        /// List of model containing the panels for a programs and fiscal
        /// </summary>
        IEnumerable<PanelModel> ModelList { get; set; }
    }
}
