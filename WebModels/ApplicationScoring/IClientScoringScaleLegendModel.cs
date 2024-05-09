using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    public interface IClientScoringScaleLegendModel
    {
        /// <summary>
        /// The overall client scoring scale legend
        /// </summary>
        IEnumerable<ScoringScaleLegendModel> Overall { get; set; }
        /// <summary>
        /// The criterion client scoring scale legend
        /// </summary>
        IEnumerable<ScoringScaleLegendModel> Criterion { get; set; }
    }
}
