namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for individual reviewer score information
    /// </summary>
    public interface IReviewerScoreModel
    {
        /// <summary>
        /// Decimal representation of a score rating
        /// </summary>
        decimal Score { get; set; }

        /// <summary>
        /// String representation of an adjectival score rating (if exists)
        /// </summary>
        string AdjectivalRating { get; set; }

        /// <summary>
        /// Whether Critique was submitted by reviewer
        /// </summary>
        bool IsCritiqueSubmitted { get; set; }

        /// <summary>
        /// The type of score the value represents
        /// </summary>
        string ScoreType { get; set; }

        /// <summary>
        /// Panel user assignment identifier
        /// </summary>
        int PanelUserAssignmentId { get; set; }

        /// <summary>
        /// String representation of a formatted score for presentation
        /// </summary>
        string FormattedScore { get; }

    }
}