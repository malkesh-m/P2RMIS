using System.Collections.Generic;

namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// TODO: document me
    /// </summary>
    public interface ICritiqueDetailResultModel
    {
        IApplicationDetail ApplicationDetails { get; }
        IEnumerable<ReviewerCritiques_Result> CritiqueDetails { get; }
    }
}
