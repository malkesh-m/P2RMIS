namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Interface for the criteriaEvaluation object
    /// </summary>
    public interface ICriteriaEvaluation
    {
        /// <summary>
        /// The scores
        /// </summary>
        decimal? Score { get; set; }
        /// <summary>
        /// The panel user assignment identifier
        /// </summary>
        int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Did the reviewer abstain
        /// </summary>
        bool Abstain { get; set; }
        /// <summary>
        /// Gets or sets the type of the score.
        /// </summary>
        /// <value>
        /// The type of the score.
        /// </value>
        bool IntegerFlag { get; set; }
        /// <summary>
        /// Gets the score value.
        /// </summary>
        /// <value>
        /// The score value.
        /// </value>
        string ScoreValue { get; }
    }
}
