using System;

namespace Sra.P2rmis.WebModels.ProgramRegistration
{
    /// <summary>
    /// Data model for the registration document
    /// </summary>
    public class RegistrationDocument : IRegistrationDocument
    {
        #region Attributes
        /// <summary>
        /// Client abbreviation
        /// </summary>
        public string ClientAbbreviation { get; set; }
        /// <summary>
        /// Fiscal year
        /// </summary>
        public string FiscalYear { get; set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Program description
        /// </summary>
        public string ProgramDescription { get; set; }
        /// <summary>
        /// Panel abbreviation
        /// </summary>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// Panel name
        /// </summary>
        public string PanelName { get; set; }
        /// <summary>
        /// Document identifier
        /// </summary>
        public int DocumentId { get; set; }
        /// <summary>
        /// Document's version
        /// </summary>
        public string DocumentVersion { get; set; }
        /// <summary>
        /// Document's updated date/time
        /// </summary>
        public DateTime? DocumentUpdatedDateTime { get; set; }
        /// <summary>
        /// Whether the document is signed
        /// </summary>
        public bool DocumentSigned
        {
            get { return DocumentSignedDateTime.HasValue; }
            set { }
        }
        /// <summary>
        /// Document's signed date/time
        /// </summary>
        public DateTime? DocumentSignedDateTime { get; set; }
        /// <summary>
        /// Full name of contract signer
        /// </summary>
        public string SignedByName { get; set; }
        /// <summary>
        /// User entity identifier of user signing the document.
        /// </summary>
        public int? SignedByUserId { get; set; }
        /// <summary>
        /// Document content
        /// </summary>
        public string DocumentContent { get; set; }
        /// <summary>
        /// Gets or sets the content of the document web.
        /// </summary>
        /// <value>
        /// The content of the document web.
        /// </value>
        public string DocumentWebContent { get; set; }
        /// <summary>
        /// Html for the contract body
        /// </summary>
        public string ContractHtml { get; set; }
        /// <summary>
        /// Html for the attachment body
        /// </summary>
        public string AttachmentHtml { get; set; }
        /// <summary>
        /// Indicates if the Program FiscalYear and Session pay rates 
        /// have been uploaded.
        /// </summary>
        public bool ArePayRatesUploaded { get; set; }
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }
        #endregion
        #region Constructor & Setup
        /// <summary>
        /// Parameter less constructor
        /// </summary>
        public RegistrationDocument() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fiscalYear">Fiscal year</param>
        /// <param name="programAbbreviation">Program abbreviation</param>
        /// <param name="programDescription">Program full name</param>
        /// <param name="panelAbbreviation">Panel abbreviation</param>
        /// <param name="panelName">Panel name</param>
        /// <param name="clientAbbreviation">Client abbreviation</param>
        public RegistrationDocument(string fiscalYear, string programAbbreviation, string programDescription, string panelAbbreviation, string panelName, string clientAbbreviation, bool isPayRatesUploaded)
        {
            this.FiscalYear = fiscalYear;
            this.ProgramAbbreviation = programAbbreviation;
            this.ProgramDescription = programDescription;
            this.PanelAbbreviation = panelAbbreviation;
            this.PanelName = panelName;
            this.ClientAbbreviation = clientAbbreviation;
            this.ArePayRatesUploaded = isPayRatesUploaded;
        }
        #endregion
    }
}
