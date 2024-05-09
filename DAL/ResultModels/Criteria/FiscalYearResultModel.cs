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
    public class FiscalYearResultModel : IFiscalYearResultModel
    {
        #region Constructor
        /// <summary>
        /// Default constructor. 
        /// </summary>
        public FiscalYearResultModel()
        {
            //
            // By default we always return a list, even if it is 0 in length
            //
            this.ModelList = new List<FyModel>();
        }
        #endregion
        /// <summary>
        /// List of model containing the fiscal years for one or more programs
        /// </summary>
        public IEnumerable<FyModel> ModelList { get; set; }
    }
}
