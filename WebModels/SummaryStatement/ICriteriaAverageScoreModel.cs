namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Criteria score data
    /// </summary>
    public interface ICriteriaAverageScoreModel
    {
        /// <summary>
        /// Score for an evaluation criteria
        /// </summary>
        decimal? CriteriaScore { get; set; }

        /// <summary>
        /// Description for an evaluation criteria
        /// </summary>
        string CriteriaDescription { get; set; }

        /// <summary>
        /// Order in which criteria is displayed
        /// </summary>
        int CriteriaSortOrder { get; set; }
    }
}