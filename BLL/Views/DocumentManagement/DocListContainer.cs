using System;
using System.Collections.Generic;
using Sra.P2rmis.Dal.ResultModels.DocManagement;
using Sra.P2rmis.WebModels.DocumentManagement;

namespace Sra.P2rmis.Bll.Views.DocumentManagement
{
    /// <summary>
    /// Container returning the document list for a specific program; fiscal year & panel.
    /// </summary>
    public class DocListContainer: IDocListContainer
    {
        #region Constructors
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private DocListContainer() { }
        /// <summary>
        /// Constructor.  Populated from the Data Layers DocListResultModel
        /// </summary>
        public DocListContainer(IDocListResultModel resultModel)
        {
            this.modelList = new List<IDocListModel>();

            if ((resultModel != null) && (resultModel.ModelList != null))
            {
                this.modelList = new List<IDocListModel>(resultModel.ModelList);
            }

            Console.WriteLine("my size " + this.modelList.Count);
        }
        #endregion
        #region Properties
        /// <summary>
        /// List of models describing a document.
        /// </summary>
        public IList<IDocListModel> modelList { get; private set; }
        #endregion
    }
}
