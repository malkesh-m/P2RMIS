namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Container returning a client program description and abbreviations.
    /// </summary>
    public interface IReportClientProgramModel
    {
        /// <summary>
        /// Client program abbreviation
        /// </summary>
        string ClientProgramAbbreviation { get; set; }
        /// <summary>
        /// Client program description
        /// </summary>
        string ClientProgramDescription { get; set; }
    }
}
