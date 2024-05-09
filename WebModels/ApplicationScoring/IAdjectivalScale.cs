namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// The interface for the AdjectivalScale object
    /// </summary>
    interface IAdjectivalScale
    {
        /// <summary>
        /// The Abjectival score
        /// </summary>
        string ScoringLabel { get; set; }
        /// <summary>
        /// The numeric equalivant score
        /// </summary>
        int NumericEquivalent { get; set; }
    }
}
