namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Container returning a client program description and abbreviations.
    /// </summary>
    public interface IReportClientModel
    {
        /// <summary>
        /// Client unique identifier
        /// </summary>
        int ClientIdentifier { get; set; }
        /// <summary>
        /// Client description
        /// </summary>
        string ClientDescription { get; set; }
        /// <summary>
        /// Client abbreviation
        /// </summary>
        string ClientAbbreviation { get; set; }
    }
}
