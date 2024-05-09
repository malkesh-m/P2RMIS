namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Container returning a client program description and abbreviations.
    /// </summary>
    /// <remarks>No unit are needed for this class (just wrapper for data)</remarks>
    public class ReportClientProgramModel : IReportClientProgramModel
    {
        /// <summary>
        /// Client program abbreviation
        /// </summary>
        public string ClientProgramAbbreviation { get; set; }
        /// <summary>
        /// Client program description
        /// </summary>
        public string ClientProgramDescription { get; set; }
    }
}
