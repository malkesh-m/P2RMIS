using System.Collections.Generic;
using Sra.P2rmis.WebModels.DocumentManagement;

namespace Sra.P2rmis.Bll.Views.DocumentManagement
{
    /// <summary>
    /// Container returning the document list for a specific program; fiscal year & panel.
    /// </summary>
    public interface IDocListContainer
    {
        #region Properties
        /// <summary>
        /// List of models describing a document.
        /// </summary>
        IList<IDocListModel> modelList { get; }
        #endregion
    }
}
