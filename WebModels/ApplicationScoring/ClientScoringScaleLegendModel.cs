using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Class representing the client scoring legend values
    /// </summary>
    public class ClientScoringScaleLegendModel
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public ClientScoringScaleLegendModel()
        {
            Overall = new List<IScoringScaleLegendModel>();
            Criterion = new List<IScoringScaleLegendModel>();
        }
        #endregion
        /// <summary>
        /// The overall client scoring scale legend
        /// </summary>
        public IEnumerable<IScoringScaleLegendModel> Overall { get; set; }
        /// <summary>
        /// Overall scale label
        /// </summary>
        public string OverallScaleLabel { get; set; }
        /// <summary>
        /// The criterion client scoring scale legend
        /// </summary>
        public IEnumerable<IScoringScaleLegendModel> Criterion { get; set; }
        /// <summary>
        /// Criterion scale label
        /// </summary>
        public string CriterionScaleLabel { get; set; }
    }
}
