namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Model object containing the criterion score.
    /// </summary>
    public class PremeetingScores : IPremeetingScores
    {
        /// <summary>
        /// The premeeting score
        /// </summary>
        public decimal? Score { get; set; }
        /// <summary>
        /// Is the score an abstaination 
        /// </summary>
        public bool Abstain { get; set; }
        /// <summary>
        /// The Criterion Abbreviation
        /// </summary>
        public string ElementAbbreviation { get; set; }
        /// <summary>
        /// The client element identifier
        /// </summary>
        public int ClientElementId { get; set; }
        /// <summary>
        /// The application workfolw step element identifieer
        /// </summary>
        public int ApplicationWorkflowStepElementId { get; set; } 
    }
}
