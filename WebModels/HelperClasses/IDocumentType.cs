namespace Sra.P2rmis.WebModels.HelperClasses
{
    /// <summary>
    /// Data to identify a single document type.
    /// </summary>
    public interface IDocumentType
    {
        /// <summary>
        /// Document type unique identifier
        /// </summary>
        int DocId { get; set; }
        /// <summary>
        /// Document type
        /// </summary>
        string DocType { get; set; }
    }
}
