using System;
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ProgramRegistration
{
    /// <summary>
    /// Data model used for the registration document form
    /// </summary>
    public class DocumentForm : RegistrationDocument, IDocumentForm, IWizardModel
    {
        #region Attributes
        /// <summary>
        /// The form's key
        /// </summary>
        public string FormKey { get; set; }
        /// <summary>
        /// The key/value pair collection of contents
        /// </summary>
        //public Dictionary<int, string> Contents { get; set; }
        public Dictionary<int, IDocumentItemRequired> Contents { get; set; }
        /// <summary>
        /// Access points for ReviewerItem keys
        /// </summary>
        public int FinancialDisclosure { get; set; }
        public int FinancialDisclosureDetails { get; set; }
        public int AdditionalDisclosure { get; set; }
        public int AdditionalDisclosureDetails { get; set; }
        public int ConsultantFeeAccepted { get; set; }
        public int BusinessCategory { get; set; }
/// <summary>
/// Gets or sets the contract data.
/// </summary>
/// <value>
/// Data for generating the contract document.
/// </value>
public IContractModel ContractData { get; set; }
        /// <summary>
        /// Signed off line indicator
        /// </summary>
        public bool? SignedOffLine { get; private set; }
        #endregion
        #region Construction & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentForm()
        {
            Contents = new Dictionary<int, IDocumentItemRequired>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrationDocument"></param>
        public DocumentForm(IRegistrationDocument registrationDocument)
            : this()
        {
            this.FiscalYear = registrationDocument.FiscalYear;
            this.ProgramAbbreviation = registrationDocument.ProgramAbbreviation;
            this.ProgramDescription = registrationDocument.ProgramDescription;
            this.PanelAbbreviation = registrationDocument.PanelAbbreviation;
            this.PanelName = registrationDocument.PanelName;
            this.ClientAbbreviation = registrationDocument.ClientAbbreviation;
            this.ArePayRatesUploaded = registrationDocument.ArePayRatesUploaded;
        }
        /// <summary>
        /// Populate the document form
        /// </summary>
        /// <param name="formKey">Form key</param>
        /// <param name="documentId">Document identifier</param>
        /// <param name="documentVersion">Document version</param>
        /// <param name="updatedDateTime">Date/time updated</param>
        /// <param name="signedDateTime">Date/time signed</param>
        /// <param name="signedByUserId">User entity identifier of user who signed document</param>
        /// <param name="signedByName">Full name of user who signed document</param>
        /// <param name="contractData">Data related to the contract document</param>
        /// <param name="signedOffLine">Document signed off line indicator</param>
        public void Populate(string formKey, int documentId, string documentVersion, DateTime? updatedDateTime, DateTime? signedDateTime, int? signedByUserId, string signedByName, IContractModel contractData, bool? signedOffLine)
        {
            this.FormKey = formKey;
            this.DocumentId = documentId;
            this.DocumentVersion = documentVersion;
            this.DocumentUpdatedDateTime = updatedDateTime;
            this.DocumentSigned = (signedDateTime != null);
            this.DocumentSignedDateTime = signedDateTime;
            this.SignedByUserId = signedByUserId;
            this.SignedByName = signedByName;
            this.ContractData = contractData;
            this.SignedOffLine = signedOffLine;
        }
        /// <summary>
        /// Populates the specified form key.
        /// </summary>
        /// <param name="formKey">The form key.</param>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="documentVersion">The document version.</param>
        /// <param name="updatedDateTime">The updated date time.</param>
        /// <param name="signedDateTime">The signed date time.</param>
        /// <param name="signedByUserId">The signed by user identifier.</param>
        /// <param name="signedByName">Name of the signed by.</param>
        /// <param name="contractData">The contract data.</param>
        /// <param name="documentContent">Content of the document.</param>
        /// <param name="signedOffLine">The signed off line.</param>
        public void Populate(string formKey, int documentId, string documentVersion, DateTime? updatedDateTime, DateTime? signedDateTime, int? signedByUserId, string signedByName, IContractModel contractData, string documentContent, bool? signedOffLine)
        {
            this.Populate(formKey, documentId, documentVersion, updatedDateTime, signedDateTime, signedByUserId, signedByName, contractData, signedOffLine);
            this.DocumentContent = documentContent;
        }
        /// <summary>
        /// Populate the document form
        /// </summary>
        /// <param name="formKey">Form key</param>
        /// <param name="documentId">Document identifier</param>
        /// <param name="documentVersion">Document version</param>
        /// <param name="updatedDateTime">Date/time updated</param>
        /// <param name="signedDateTime">Date/time signed</param>
        /// <param name="signedByUserId">User entity identifier of user who signed document</param>
        /// <param name="signedByName">Full name of user who signed document</param>
        /// <param name="documentContent">Document content</param>
        /// <param name="signedOffLine">Signed off-line indicator</param>
        public void Populate(string formKey, int documentId, string documentVersion, DateTime? updatedDateTime, DateTime? signedDateTime, int? signedByUserId, string signedByName, string documentContent, bool? signedOffLine)                
        {
            this.Populate(formKey, documentId, documentVersion, updatedDateTime, signedDateTime, signedByUserId, signedByName, new ContractModel(), signedOffLine);
            this.DocumentContent = documentContent;
        }

        /// <summary>
        /// Populate the document form
        /// </summary>
        /// <param name="formKey">Form key</param>
        /// <param name="documentId">Document identifier</param>
        /// <param name="documentVersion">Document version</param>
        /// <param name="updatedDateTime">Date/time updated</param>
        /// <param name="signedDateTime">Date/time signed</param>
        /// <param name="signedByUserId">User entity identifier of user who signed document</param>
        /// <param name="signedByName">Full name of user who signed document</param>
        /// <param name="documentContent">Document content</param>
        /// <param name="signedOffLine">Signed off-line indicator</param>
        /// <param name="fileName">File name</param>
        /// <param name="contractModel">Contract model containing contract specific data</param>
        public void Populate(string formKey, int documentId, string documentVersion, DateTime? updatedDateTime, DateTime? signedDateTime, int? signedByUserId, string signedByName, string documentContent, bool? signedOffLine, string fileName, IContractModel contractModel)
        {
            this.Populate(formKey, documentId, documentVersion, updatedDateTime, signedDateTime, signedByUserId, signedByName, documentContent, signedOffLine);
            this.DocumentContent = documentContent;
            this.FileName = fileName;
            this.ContractData = contractModel;
        }
        /// <summary>
        /// Initialize the wrapper properties for the ReviewerItem keys.
        /// </summary>
        /// <param name="financialDisclosure">Financial Disclosure ReviewerItem entity key</param>
        /// <param name="financialDisclosureDetails">Financial Disclosure Details ReviewerItem entity key</param>
        /// <param name="additionalDisclosure">Additional Disclosure ReviewerItem entity key</param>
        /// <param name="additionalDisclosureDetails">Additional Disclosure Details ReviewerItem entity key</param>
        /// <param name="consultantFeeAccepted">ConsultantFeeAccepted ReviewerItem entity key</param>
        /// <param name="businessCategory">Business Category ReviewerItem entity key</param>
        public void SetReviewerItemKeys(int financialDisclosure, int financialDisclosureDetails, int additionalDisclosure, int additionalDisclosureDetails, 
            int consultantFeeAccepted, int businessCategory)
        {
            this.FinancialDisclosure = financialDisclosure;
            this.FinancialDisclosureDetails = financialDisclosureDetails;
            this.AdditionalDisclosure = additionalDisclosure;
            this.AdditionalDisclosureDetails = additionalDisclosureDetails;
            this.ConsultantFeeAccepted = consultantFeeAccepted;
            this.BusinessCategory = businessCategory;
        }
        #endregion
        #region Services
        /// <summary>
        /// Operator overload for the key/value pairs
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IDocumentItemRequired this[int key]
        {
            get
            {
                return Contents[key];
            }
            set
            {
                Contents[key] = value;
            }
        }  
        #endregion
    }

    public class DocumentItemRequired : IDocumentItemRequired
    {
        #region Construction & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentItemRequired() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value of the item</param>
        /// <param name="isRequired">Whether the item is required or not</param>
        /// <param name="warningMessage">Warning message</param>
        public DocumentItemRequired(string value, bool isRequired, string warningMessage)
        {
            this.ItemValue = value;
            this.IsRequired = isRequired;
            this.WarningMessage = warningMessage;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// The item value
        /// </summary>
        public string ItemValue { get; private set; }
        /// <summary>
        /// Indicates if the document item must be submitted by the user.
        /// </summary>
        public bool IsRequired { get; private set; }
        /// <summary>
        /// Warning message if required
        /// </summary>
        public string WarningMessage { get; private set; }
        #endregion
    }
}
