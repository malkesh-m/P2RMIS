namespace Sra.P2rmis.WebModels.HelperClasses
{
    public class DocumentType : IDocumentType
    {
        /// <summary>
        /// Document type unique identifier
        /// </summary>
        public int DocId { get; set; }
        /// <summary>
        /// Document type
        /// </summary>
        public string DocType { get; set; }
    }
}
