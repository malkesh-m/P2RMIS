using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Individual score data for summary section
    /// </summary>
    public interface ISummaryIndividualScoreModel
    {
        /// <summary>
        /// The highest value possible for a criteria
        /// </summary>
        decimal? ScaleHighValue { get; set; }

        /// <summary>
        /// The lowest value possible for a criteria
        /// </summary>
        decimal? ScaleLowValue { get; set; }

        /// <summary>
        /// The name of the criteria
        /// </summary>
        string CriteriaName { get; set; }

        /// <summary>
        /// Collection of reviewer scores
        /// </summary>
        IEnumerable<ReviewerScoreModel> ReviewerScores { get; set; }

        /// <summary>
        /// Order in which criteria is to be displayed
        /// </summary>
        int CriteriaSortOrder { get; set; }

        /// <summary>
        /// Whether the criteria is considered an overall rating
        /// </summary>
        bool IsOverall { get; set; }

        /// <summary>
        /// The type of scoring the criteria is represented by
        /// </summary>
        string ScoreType { get; set; }
    }
}