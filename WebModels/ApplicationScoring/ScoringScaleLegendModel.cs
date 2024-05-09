namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Class to contain the label and min and max value for the scoring legend
    /// </summary>
    public class ScoringScaleLegendModel : IScoringScaleLegendModel
    {
        /// <summary>
        /// The legend item high range value
        /// </summary>
        public string HighValue { get; set; }
        /// <summary>
        /// The legend item low range value
        /// </summary>
        public string LowValue { get; set; }
        /// <summary>
        /// The item label name
        /// </summary>
        public string LegendItemLabel { get; set; }
    }
}
