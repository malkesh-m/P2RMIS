using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Views.CritiqueDetails
{
    public interface ICritiqueDetailsContainer
    {
        IEnumerable<CritiqueFacts> CritiqueDetails { get; }
        bool IsCritiqueDeadlinePassed { get; }
    }
}
