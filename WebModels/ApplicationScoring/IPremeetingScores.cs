namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Model object containing the criterion score.
    /// </summary>
    public interface IPremeetingScores
    {
        /// <summary>
        /// The premeeting score
        /// </summary>
        decimal? Score { get; set; }
        /// <summary>
        /// Is the score an abstaination 
        /// </summary>
        bool Abstain { get; set; }
        /// <summary>
        /// The Criterion Abbreviation
        /// </summary>
        string ElementAbbreviation { get; set; }
        /// <summary>
        /// The client element identifier
        /// </summary>
        int ClientElementId { get; set; }
        /// <summary>
        /// The application workfolw step element identifieer
        /// </summary>
        int ApplicationWorkflowStepElementId { get; set; }
    }
}
