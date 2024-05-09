using System.Collections.Generic;
using Sra.P2rmis.WebModels.DocumentManagement;

namespace Sra.P2rmis.Dal.ResultModels.DocManagement
{
    /// <summary>
    /// Interface defining the result of a document list request for
    /// a specific program; fiscal year and panel.
    /// </summary>
    public class DocListResultModel : IDocListResultModel
    {
        #region Constructor
        /// <summary>
        /// Default constructor. 
        /// </summary>
        public DocListResultModel()
        {
            //
            // By default we always return a list, even if it is 0 in length
            //
            this.ModelList = new List<IDocListModel>();
        }
        #endregion
        /// <summary>
        /// List of models containing the document list
        /// </summary>
        public IEnumerable<IDocListModel> ModelList { get; set; }
    }
}
