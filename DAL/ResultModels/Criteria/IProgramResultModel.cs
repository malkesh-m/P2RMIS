using System.Collections.Generic;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Dal.ResultModels.Criteria
{
    /// <summary>
    /// Interface defining the result of a request for criteria search for open programs.
    /// </summary>
    public interface IProgramResultModel
    {
        /// <summary>
        /// Database operation results
        /// </summary>
        IEnumerable<ProgramModel> ModelList { get; set; }
    }
}
