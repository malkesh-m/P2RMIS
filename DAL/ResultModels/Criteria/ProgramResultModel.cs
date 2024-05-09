using System.Collections.Generic;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Dal.ResultModels.Criteria
{
    /// <summary>
    /// Result of a request for criteria search for open programs.
    /// </summary>
    public class ProgramResultModel : IProgramResultModel
    {
         #region Constructor
        /// <summary>
        /// Default constructor. 
        /// </summary>
        public ProgramResultModel()
        {
            //
            // By default we always return a list, even if it is 0 in length
            //
            this.ModelList = new List<ProgramModel>();
        }
        #endregion
        /// <summary>
        /// List of models containing the document list
        /// </summary>
        public IEnumerable<ProgramModel> ModelList { get; set; }
   }
}
