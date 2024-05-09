using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.Dal.ResultModels.Criteria;

namespace Sra.P2rmis.Bll.Views.Criteria
{
    /// <summary>
    /// Container returning the fiscal years for one or more programs
    /// </summary>
    public class FiscalYearContainer: IFiscalYearContainer
    {
        #region constructor
       /// <summary>
        /// Default constructor defines default state.
        /// </summary>
        public FiscalYearContainer() 
        {
            this.ModelList = new List<FyModel>();
        }
        /// <summary>
        /// Constructor.  Populate from the Data Layer.  Always constructs
        /// a list of program descriptions even if none (0 length list)
        /// </summary>
        /// <param name="resultModel">List of programs from the data layer</param>
        public FiscalYearContainer(IFiscalYearResultModel resultModel)
            : this()
        {
            if ((resultModel != null) && (resultModel.ModelList != null))
            {
                this.ModelList = resultModel.ModelList;
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// The client's programs
        /// </summary>
        public IEnumerable<FyModel> ModelList { get; set; }
        #endregion
    }
}
