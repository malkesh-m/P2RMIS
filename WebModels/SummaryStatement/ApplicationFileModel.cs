namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Data model for retrieving a client's possible files.
    /// </summary>
    public class ApplicationFileModel : IApplicationFileModel
    {
        /// <summary>
        /// Display label for file
        /// </summary>
        public string DisplayLabel { get; set; }
        /// <summary>
        /// Log number of the file
        /// </summary>
        public string LogNumber { get; set; }
        /// <summary>
        /// The file suffix to identify a physical file as a particular type
        /// </summary>
        public string FileSuffix { get; set; }
        /// <summary>
        /// The file folder the application is located in on the legacy server
        /// </summary>
        public string Folder { get; set; }

    }
}
