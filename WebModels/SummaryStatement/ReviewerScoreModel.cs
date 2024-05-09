namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Model representing an individual reviewers scores
    /// </summary>
    public class ReviewerScoreModel : IReviewerScoreModel
    {
        /// <summary>
        /// Order in which the reviewer is displayed
        /// </summary>
        public int? ReviewerSortOrder { get; set; }
        /// <summary>
        /// Score value provided by the reviewer
        /// </summary>
        public decimal? Score { get; set; }
        /// <summary>
        /// Adjectival equivalent of the score value
        /// </summary>
        public string AdjectivalValue { get; set; }
        /// <summary>
        /// The type of scoring of the ScoreType
        /// </summary>
        public string ScoreType { get; set; }
    }
}
