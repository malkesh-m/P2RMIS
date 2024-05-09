using System.Collections.Generic;

namespace Sra.P2rmis.Dal.ResultModels
{
    public interface IResultModel<T>
    {
        /// <summary>
        /// List of model containing the panels for a programs and fiscal
        /// </summary>
        IEnumerable<T> ModelList { get; set; }
    }
}
