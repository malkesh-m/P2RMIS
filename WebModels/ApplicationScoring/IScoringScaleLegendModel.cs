namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Interface to contain the label and min and max value for the scoring legend
    /// </summary>
    public interface IScoringScaleLegendModel
    {
        /// <summary>
        /// The legend item high range value
        /// </summary>
        string HighValue { get; set; }
        /// <summary>
        /// The legend item low range value
        /// </summary>
        string LowValue { get; set; }
        /// <summary>
        /// The item label name
        /// </summary>
        string LegendItemLabel { get; set; }
    }
}
