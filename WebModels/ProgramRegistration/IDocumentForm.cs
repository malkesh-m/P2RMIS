using System;
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ProgramRegistration
{
    /// <summary>
    /// Data model used for the registration document form
    /// </summary>
    public interface IDocumentForm : IRegistrationDocument
    {
        /// <summary>
        /// The form's key
        /// </summary>
        string FormKey { get; set; }
        /// <summary>
        /// The key/value pair collection of contents
        /// </summary>
        Dictionary<int, IDocumentItemRequired> Contents { get; set; }
        /// <summary>
        /// Operator overload for the key/value pairs
        /// </summary>
        /// <param name="key">Key value</param>
        IDocumentItemRequired this[int key] { get; }
        /// <summary>
        /// Access points for ReviewerItem keys
        /// </summary>
        int FinancialDisclosure { get; }
        int FinancialDisclosureDetails { get; }
        int AdditionalDisclosure { get; }
        int AdditionalDisclosureDetails { get;  }
        int ConsultantFeeAccepted { get;  }
        int BusinessCategory { get;  }
        //int EmergencyContactFirstName { get; }
        //int EmergencyContactLastName { get; }
        //int EmergencyContactPrimaryPhoneType { get; }
        //int EmergencyContactPrimaryPhoneNumber { get; }
        //int EmergencyContactPrimaryPhoneInternational { get; }
        //int EmergencyContactSecondaryPhoneType { get; }
        //int EmergencyContactSecondaryPhoneNumber { get; }
        //int EmergencyContactSecondaryPhoneInternational { get; }
        /// <summary>
        /// Gets or sets the contract data.
        /// </summary>
        /// <value>
        /// Data for generating the contract document.
        /// </value>
        IContractModel ContractData { get; set; }
        /// <summary>
        /// Signed off line indicator
        /// </summary>
        bool? SignedOffLine { get; }
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
        /// <param name="contractData">Data related to the contract generation</param>
        /// <param name="signedOffLine">Document signed off line indicator</param>
        void Populate(string formKey, int documentId, string documentVersion, DateTime? updatedDateTime, DateTime? signedDateTime, int? signedByUserId, string signedByName, IContractModel contractData, bool? signedOffLine);
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
        void Populate(string formKey, int documentId, string documentVersion, DateTime? updatedDateTime, DateTime? signedDateTime, int? signedByUserId, string signedByName, IContractModel contractData, string documentContent, bool? signedOffLine);
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
        /// <param name="contractData">Data related to the contract generation</param>
        /// <param name="signedOffLine">Document signed off line indicator</param>
        void Populate(string formKey, int documentId, string documentVersion, DateTime? updatedDateTime, DateTime? signedDateTime, int? signedByUserId, string signedByName, string documentContent, bool? signedOffLine);
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
        /// <param name="contractData">Data related to the contract generation</param>
        /// <param name="signedOffLine">Document signed off line indicator</param>
        /// <param name="fileName">File name</param>
        /// <param name="contractModel">Contract model containing contract specific data</param>
        void Populate(string formKey, int documentId, string documentVersion, DateTime? updatedDateTime, DateTime? signedDateTime, int? signedByUserId, string signedByName, string documentContent, bool? signedOffLine, string fileName, IContractModel contractModel);

        /// <summary>
        /// Initialize the wrapper properties for the ReviewerItem keys.
        /// </summary>
        /// <param name="financialDisclosure">Financial Disclosure ReviewerItem entity key</param>
        /// <param name="financialDisclosureDetails">Financial Disclosure Details ReviewerItem entity key</param>
        /// <param name="additionalDisclosure">Additional Disclosure ReviewerItem entity key</param>
        /// <param name="additionalDisclosureDetails">Additional Disclosure Details ReviewerItem entity key</param>
        /// <param name="consultantFeeAccepted">ConsultantFeeAccepted ReviewerItem entity key</param>
        /// <param name="businessCategory">Business Category ReviewerItem entity key</param>
        void SetReviewerItemKeys(int financialDisclosure, int financialDisclosureDetails, int additionalDisclosure, int additionalDisclosureDetails,
            int consultantFeeAccepted, int businessCategory);
   }

    public interface IDocumentItemRequired
    {
        #region Attributes
        /// <summary>
        /// The item value
        /// </summary>
        string ItemValue { get; }
        /// <summary>
        /// Indicates if the document item must be submitted by the user.
        /// </summary>
        bool IsRequired { get;  }
        /// <summary>
        /// Warning message if required
        /// </summary>
        string WarningMessage { get; }
        #endregion
    }
}
