namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Container returning a client's identifier, description and abbreviations.
    /// </summary>
    /// <remarks>No unit tests are needed for this class (just wrapper for data)</remarks>
    public class ReportClientModel : IReportClientModel
    {
        /// <summary>
        /// Client identifier
        /// </summary>
        public int ClientIdentifier { get; set; }
        /// <summary>
        /// Client abbreviation
        /// </summary>
        public string ClientAbbreviation { get; set; }
        /// <summary>
        /// Client description
        /// </summary>
        public string ClientDescription { get; set; }
    }
}
