using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Individual score data for summary section
    /// </summary>
    public class SummaryIndividualScoreModel : ISummaryIndividualScoreModel
    {
        /// <summary>
        /// The highest value possible for a criteria
        /// </summary>
        public decimal? ScaleHighValue { get; set; }
        /// <summary>
        /// The lowest value possible for a criteria
        /// </summary>
        public decimal? ScaleLowValue { get; set; }
        /// <summary>
        /// The name of the criteria
        /// </summary>
        public string CriteriaName { get; set; }
        /// <summary>
        /// Collection of reviewer scores
        /// </summary>
        public IEnumerable<ReviewerScoreModel> ReviewerScores { get; set; }
        /// <summary>
        /// Order in which criteria is to be displayed
        /// </summary>
        public int CriteriaSortOrder { get; set; }
        /// <summary>
        /// Whether the criteria is considered an overall rating
        /// </summary>
        public bool IsOverall { get; set; }
        /// <summary>
        /// The type of scoring the criteria is represented by
        /// </summary>
        public string ScoreType { get; set; }
    }
    
}
