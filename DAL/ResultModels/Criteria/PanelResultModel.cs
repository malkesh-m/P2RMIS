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
    public class PanelResultModel : IPanelResultModel
    {
        #region Constructor
        /// <summary>
        /// Default constructor. 
        /// </summary>
        public PanelResultModel()
        {
            //
            // By default we always return a list, even if it is 0 in length
            //
            this.ModelList = new List<PanelModel>();
        }
        #endregion
        /// <summary>
        /// List of model containing the panels for a programs and fiscal
        /// </summary>
        public IEnumerable<PanelModel> ModelList { get; set; }
    }
}
