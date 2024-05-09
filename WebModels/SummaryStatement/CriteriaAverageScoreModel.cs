
namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Criteria score data
    /// </summary>
    public class CriteriaAverageScoreModel : ICriteriaAverageScoreModel
    {
        /// <summary>
        /// Score for an evaluation criteria
        /// </summary>
        public decimal? CriteriaScore { get; set; }
        /// <summary>
        /// Description for an evaluation criteria
        /// </summary>
        public string CriteriaDescription { get; set; }
        /// <summary>
        /// Order in which criteria is displayed
        /// </summary>
        public int CriteriaSortOrder { get; set; }
    }
}
