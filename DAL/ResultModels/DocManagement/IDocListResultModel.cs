using System.Collections.Generic;
using Sra.P2rmis.WebModels.DocumentManagement;

namespace Sra.P2rmis.Dal.ResultModels.DocManagement
{
    /// <summary>
    /// Interface defining the result of a document list request for
    /// a specific program; fiscal year and panel.
    /// </summary>
    public interface IDocListResultModel
    {
        IEnumerable<IDocListModel> ModelList { get; set; }
    }
}
