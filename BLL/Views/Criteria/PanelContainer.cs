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
    /// Container returning the panels for a program/fiscal year
    /// </summary>
    public class PanelContainer : IPanelContainer
    {
        #region constructor
       /// <summary>
        /// Default constructor defines default state.
        /// </summary>
        public PanelContainer() 
        {
            this.ModelList = new List<PanelModel>();
        }
        /// <summary>
        /// Constructor.  Populate from the Data Layer.  Always constructs
        /// a list of program descriptions even if none (0 length list)
        /// </summary>
        /// <param name="resultModel">List of programs from the data layer</param>
        public PanelContainer(IPanelResultModel resultModel)
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
        /// The program/fiscal year panels
        /// </summary>
        public IEnumerable<PanelModel> ModelList { get; set; }
        #endregion
    }
}
