
namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Signature of delegate used to format a score whether decimal or adjectival.
    /// </summary>
    /// <param name="number">Number to format</param>
    /// <param name="scoreType">The type of score the value represents</param>
    /// <param name="adjectival">Text to replace number (if applicable)</param>
    /// <param name="submitted">Whether the critique has been submitted</param>
    /// <returns>Number formatted as string in standard way</returns>
    public delegate string ScoreFormatter(decimal number, string scoreType, string adjectival, bool submitted);
    /// <summary>
    /// Data model for individual reviewer score information
    /// </summary>
    public class ReviewerScoreModel : IReviewerScoreModel
    {
        /// <summary>
        /// Decimal representation of a score rating
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// String representation of an adjectival score rating (if exists)
        /// </summary>
        public string AdjectivalRating { get; set; }
        /// <summary>
        /// Whether Critique was submitted by reviewer
        /// </summary>
        public bool IsCritiqueSubmitted { get; set; }
        /// <summary>
        /// The type of score the value represents
        /// </summary>
        public string ScoreType { get; set; }

        /// <summary>
        /// Panel user assignment identifier
        /// </summary>
        public int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// delegate used to format the score
        /// </summary>
        public static ScoreFormatter ScoreFormatter { get; set; }
        /// <summary>
        /// String representation of a formatted score for presentation
        /// </summary>
        public string FormattedScore
        {
            get { return ScoreFormatter(Score, ScoreType, AdjectivalRating, IsCritiqueSubmitted); }
        }
    }
}
