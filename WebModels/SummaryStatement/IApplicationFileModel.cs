namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Data model for retrieving a client's possible files.
    /// </summary>
    public interface IApplicationFileModel
    {
        /// <summary>
        /// Display label for file
        /// </summary>
        string DisplayLabel { get; set; }
        /// <summary>
        /// Log number of the file
        /// </summary>
        string LogNumber { get; set; }
        /// <summary>
        /// The appended suffix for a specific file
        /// </summary>
        string FileSuffix { get; set; }
        /// <summary>
        /// The file folder the application is located in on the legacy server
        /// </summary>
        string Folder { get; set; }
    }
}
