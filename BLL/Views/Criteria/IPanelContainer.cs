using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Bll.Views.Criteria
{
    /// <summary>
    /// Container returning the panels for a program/fiscal year
    /// </summary>
    public interface IPanelContainer
    {
        /// <summary>
        /// The program/fiscal year panels
        /// </summary>
        IEnumerable<PanelModel> ModelList { get; set; }
    }
}
