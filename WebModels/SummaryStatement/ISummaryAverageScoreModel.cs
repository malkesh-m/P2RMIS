using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Average score data for summary section
    /// </summary>
    public interface ISummaryAverageScoreModel
    {
        /// <summary>
        /// Overall average score for an application
        /// </summary>
        decimal? OverallScore { get; set; }

        /// <summary>
        /// Standard deviation of overall score for an application
        /// </summary>
        decimal? OverallStandardDeviation { get; set; }

        /// <summary>
        /// Lowest score possible for the overall score
        /// </summary>
        decimal? OverallScaleLow { get; set; }

        /// <summary>
        /// Highest score possible for the overall score
        /// </summary>
        decimal? OverallScaleHigh { get; set; }

        /// <summary>
        /// Collection of criteria scores for the application
        /// </summary>
        IEnumerable<CriteriaAverageScoreModel> CriteriaScores { get; set; }
    }
}