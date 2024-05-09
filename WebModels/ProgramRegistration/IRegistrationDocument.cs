using System;

namespace Sra.P2rmis.WebModels.ProgramRegistration
{
    /// <summary>
    /// Interface for the registration document
    /// </summary>
    public interface IRegistrationDocument
    {
        /// <summary>
        /// Client abbreviation
        /// </summary>
        string ClientAbbreviation { get; set; }
        /// <summary>
        /// Fiscal year
        /// </summary>
        string FiscalYear { get; set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Panel abbreviation
        /// </summary>
        string PanelAbbreviation { get; set; }
        /// <summary>
        /// Panel name
        /// </summary>
        string PanelName { get; set; }
        /// <summary>
        /// Document identifier
        /// </summary>
        int DocumentId { get; set; }
        /// <summary>
        /// Document's version
        /// </summary>
        string DocumentVersion { get; set; }
        /// <summary>
        /// Document's updated date/time
        /// </summary>
        DateTime? DocumentUpdatedDateTime { get; set; }
        /// <summary>
        /// Whether the document is signed
        /// </summary>
        bool DocumentSigned { get; set; }
        /// <summary>
        /// Document's signed date/time
        /// </summary>
        DateTime? DocumentSignedDateTime { get; set; }
        /// <summary>
        /// Full name of contract signer
        /// </summary>
        string SignedByName { get; set; }
        /// <summary>
        /// User entity identifier of user signing the document.
        /// </summary>
        int? SignedByUserId { get; set; }
        /// <summary>
        /// Document content
        /// </summary>
        string DocumentContent { get; set; }
        /// <summary>
        /// Gets or sets the content of the document web.
        /// </summary>
        /// <value>
        /// The content of the document web.
        /// </value>
        string DocumentWebContent { get; set; }
        /// <summary>
        /// Program description
        /// </summary>
        string ProgramDescription { get; set; }
        /// <summary>
        /// Html for the contract body
        /// </summary>
        string ContractHtml { get; set; }

        /// <summary>
        /// Html for the attachment body
        /// </summary>
        string AttachmentHtml { get; set; }
        /// <summary>
        /// Indicates if the Program FiscalYear and Session pay rates 
        /// have been uploaded.
        /// </summary>
        bool ArePayRatesUploaded { get; }
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        string FileName { get; }
    }
}
