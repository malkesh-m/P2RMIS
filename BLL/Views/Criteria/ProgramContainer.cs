using System.Collections.Generic;
using Sra.P2rmis.Dal.ResultModels.Criteria;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Bll.Views.Criteria
{
    /// <summary>
    /// Business Layer representing the programs available for one or more user clients.
    /// </summary>
    public class ProgramContainer: IProgramContainer
    {
        #region constructor
       /// <summary>
        /// Default constructor defines default state.
        /// </summary>
        public ProgramContainer() 
        {
            this.ModelList = new List<ProgramModel>();
        }
        /// <summary>
        /// Constructor.  Populate from the Data Layer.  Always constructs
        /// a list of program descriptions even if none (0 length list)
        /// </summary>
        /// <param name="resultModel">List of programs from the data layer</param>
        public ProgramContainer(IProgramResultModel resultModel): this()
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
        public IEnumerable<ProgramModel> ModelList { get; set; }
        #endregion
    }
}
