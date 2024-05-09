using System;

namespace Sra.P2rmis.WebModels.ProgramRegistration
{
    /// <summary>
    /// Web model containing data for population of Registration Status grid.
    /// </summary>
    public interface IProgramRegistrationWebModel
    {
        /// <summary>
        /// Panelist last name
        /// </summary>
        string LastName { get; }
        /// <summary>
        /// Panelist first name
        /// </summary>
        string FirstName { get; }
        /// <summary>
        /// Panelists participation type
        /// </summary>
        string ClientParticipationType { get; }
        /// <summary>
        /// Panel abbreviation
        /// </summary>
        string PanelAbbreviation { get; }
        /// <summary>
        /// Date personal information was verified
        /// </summary>
        DateTime? VerifiedDate { get; }
        /// <summary>
        /// Date Ack/NDA document was signed
        /// </summary>
        DateTime? AckNdaDateSigned { get; }
        /// <summary>
        /// Date BIAS/COI document was signed
        /// </summary>
        DateTime? BiasCoiDateSigned { get; }
        /// <summary>
        /// Payment Category
        /// </summary>
        string PaymentCategory { get; }
        /// <summary>
        /// Date contract document was signed
        /// </summary>
        DateTime? ContractDateSigned { get; }
        /// <summary>
        /// Date CV was received
        /// </summary>
        DateTime? CvReceivedDate { get; }
        /// <summary>
        /// Contract status ID
        /// </summary>
        int ContractStatusId { get; }
        /// <summary>
        /// Contract status
        /// </summary>
        string ContractStatus { get; }
        /// <summary>
        /// Contract status
        /// </summary>
        decimal? FeeAmount { get; }
        /// <summary>
        /// Contract status: isBypassed
        /// </summary>
        bool? isContractBypassed { get;  }
        /// <summary>
        /// Contract status: isRegenerated
        /// </summary>
        bool? isContractRegenerated { get;  }
        /// <summary>
        /// Contract status: isOriginal
        /// </summary>
        bool? isContractOriginal { get; }
        /// <summary>
        /// Whether the contract can have an addendum added
        /// </summary>
        bool? CanAddAddendum { get; }
        /// <summary>
        /// Date W9 was received
        /// </summary>
        DateTime? W9ReceivedDate { get; }
        /// <summary>
        /// Date W9 was verified
        /// </summary>
        DateTime? W9VerifiedDate { get; }
        /// <summary>
        /// W9 Institution (vendor) name
        /// </summary>
        string W9Institution { get; }
        /// <summary>
        /// W9 Street address
        /// </summary>
        string W9Address { get; }
        /// <summary>
        /// W9 Address state
        /// </summary>
        string W9State { get; }
        /// <summary>
        /// W9 Address zip code
        /// </summary>
        string W9Zip { get; }
        /// <summary>
        /// Panelist's User entity identifier
        /// </summary>
        int UserID { get; }
        /// <summary>
        /// The PanelUserRegistrationDocumentId for the PanelUserRegistrationDocument entity identifying the Acknowledge/NDA document
        /// </summary>
        int? AcknowledgeNdaPanelUserRegistrationDocumentId { get; }
        /// <summary>
        /// The PanelUserRegistrationDocumentId for the PanelUserRegistrationDocument entity identifying the Bias/COI
        /// </summary>
        int? BiasCoidPanelUserRegistrationDocumentId { get; }
        /// <summary>
        /// The PanelUserRegistrationDocumentId for the PanelUserRegistrationDocument entity identifying the contract
        /// </summary>
        int? ContractPanelUserRegistrationDocumentId { get; }
        /// <summary>
        /// Was the Acknowledge/NDA document signed off line
        /// </summary>
        bool? AcknowledgeNdaPanelUserRegistrationDocumentSignedOffLine { get; }
        /// <summary>
        /// Was the Bias/COI signed off line
        /// </summary>
        bool? BiasCoidPanelUserRegistrationDocumentSignedOffLine { get; }
        /// <summary>
        /// Was the contract signed off line
        /// </summary>
        bool? ContractPanelUserRegistrationDocumentSignedOffLine { get; }
        /// <summary>
        /// Gets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        int PanelUserAssignmentId { get; }
        /// <summary>
        /// W-9 Address 2
        /// </summary>
        string Address2 { get; }
        /// <summary>
        /// W-9 Address 3
        /// </summary>
        string Address3 { get; }
        /// <summary>
        /// W-9 Address 4
        /// </summary>
        string Address4 { get; }
        /// <summary>
        /// W-9 City
        /// </summary>
        string City { get; }
        /// <summary>
        /// W-9 Country full name
        /// </summary>
        string CountryFullName { get; }
        /// <summary>
        /// W-9 Country abbreviation
        /// </summary>
        string CountryAbbreviation { get; }
        /// <summary>
        /// W-9 country Id
        /// </summary>
        int? CountryId { get; }
        /// <summary>
        /// SessionPanel entiy identifier
        /// </summary>
        int SessionPanelId { get; }
        string W9Status { get; }
        DateTime? W9StatusDate { get; }
        bool RestrictedAssignedFlag { get; set; }

        string VendorName { get; set; }
        int ProgramYearId { get; set; }
    }
    /// <summary>
    /// Web model containing data for population of Registration Status grid.
    /// </summary>
    public class ProgramRegistrationWebModel : IProgramRegistrationWebModel
    {
        #region Construction & Set up
        /// <summary>
        /// Initialize the user descriptive information
        /// </summary>
        /// <param name="lastName">Panelist last name</param>
        /// <param name="firstName">Panelist first name</param>
        /// <param name="userId">Panelist User entity identifier</param>
        /// <param name="clientParticipationType">Participation type</param>
        /// <param name="panelAbbreviation">The panel abbreviation.</param>
        /// <param name="verifiedDate">The verified date.</param>
        /// <param name="paymentCategory">The payment category.</param>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        public void SetUserInformation(string lastName, string firstName, int userId, string clientParticipationType, string panelAbbreviation, DateTime? verifiedDate, string paymentCategory, int panelUserAssignmentId, int sessionPanelId, bool restrictedAssignedFlag)
        {
            this.LastName = lastName;
            this.FirstName = firstName;
            this.UserID = userId;
            this.ClientParticipationType = clientParticipationType;
            this.PanelAbbreviation = panelAbbreviation;
            this.VerifiedDate = verifiedDate;
            this.PaymentCategory = paymentCategory;
            this.PanelUserAssignmentId = panelUserAssignmentId;
            this.SessionPanelId = sessionPanelId;
            this.RestrictedAssignedFlag = restrictedAssignedFlag;
        }
        /// <summary>
        /// Initialize the document times.
        /// </summary>
        /// <param name="ackNdaDateTime">Acknowledgment/NDA document signed date/time</param>
        /// <param name="biasCoiDateTime">BIias/COI document signed date/time</param>
        /// <param name="contractDateTime">Contract document signed date/time</param>
        public void SetDocumentDates(DateTime? ackNdaDateTime, DateTime? biasCoiDateTime, DateTime? contractDateTime, DateTime? cvReceivedDate)
        {
            this.AckNdaDateSigned = ackNdaDateTime;
            this.BiasCoiDateSigned = biasCoiDateTime;
            this.ContractDateSigned = contractDateTime;
            this.CvReceivedDate = cvReceivedDate;
        }
        /// <summary>
        /// Initialize the W9 information.
        /// </summary>
        /// <param name="w9Institution">W-9 institution</param>
        /// <param name="w9Address">W-9 street address</param>
        /// <param name="w9State">W-9 state</param>
        /// <param name="w9Zip">W-9 zip code</param>
        /// <param name="w9VerifiedDate">W-9 verified date</param>
        /// <param name="w9ReceivedDate">W-9 received date/time</param>
        /// <param name="address2">W-9 address2</param>
        /// <param name="address3">W-9 address3</param>
        /// <param name="address4">W-9 address4</param>
        /// <param name="city">W-9 city</param>
        /// <param name="countryAbbreviation">W-9 country abbreviation</param>
        /// <param name="countryFullName">W-9 country full name</param>
        /// <param name="countryId">W-9 country id</param>
        /// <param name="w9Status">W9 status</param>
        /// <param name="w9StatusDate">W9 status date</param>
        public void SetW9Information(string w9Institution, string w9Address, string w9State, string w9Zip, DateTime? w9VerifiedDate, string address2, string address3, string address4, string city, string countryAbbreviation, string countryFullName, int? countryId, string w9Status, DateTime? w9StatusDate, string vendorName)
        {
            this.W9Institution = w9Institution;
            this.W9Address = w9Address;
            this.W9State = w9State;
            this.W9Zip = w9Zip;
            this.W9VerifiedDate = w9VerifiedDate;
            this.Address2 = address2;
            this.Address3 = address3;
            this.Address4 = address4;
            this.City = city;
            this.CountryAbbreviation = countryAbbreviation;
            this.CountryFullName = countryFullName;
            this.CountryId = countryId;
            this.W9Status = w9Status;
            this.W9StatusDate = w9StatusDate;
            this.VendorName = vendorName;
        }
        /// <summary>
        /// Initialize the document entity identifiers properties.
        /// </summary>
        /// <param name="acknowledgeNdaPanelUserRegistrationDocumentId">PanelUserRegistration entity identifier for the Acknowledgment/NDA document</param>
        /// <param name="biasCoidPanelUserRegistrationDocumentId">PanelUserRegistration entity identifier for the Bias/COI document</param>
        /// <param name="contractPanelUserRegistrationDocumentId">PanelUserRegistration entity identifier for the contract document</param>
        public void SetDocumentIds(int? acknowledgeNdaPanelUserRegistrationDocumentId, int? biasCoidPanelUserRegistrationDocumentId, int? contractPanelUserRegistrationDocumentId)
        {
            this.AcknowledgeNdaPanelUserRegistrationDocumentId = acknowledgeNdaPanelUserRegistrationDocumentId;
            this.BiasCoidPanelUserRegistrationDocumentId = biasCoidPanelUserRegistrationDocumentId;
            this.ContractPanelUserRegistrationDocumentId = contractPanelUserRegistrationDocumentId;
        }
        /// <summary>
        /// Initialize the properties that indicate if a document was signed off line.
        /// </summary>
        /// <param name="acknowledgeNdaPanelUserRegistrationDocumentSignedOffLine">Indicates if the Acknowledgment/NDA document was signed off line</param>
        /// <param name="biasCoidPanelUserRegistrationDocumentSignedOffLine">Indicates if the Bias/COI document was signed off line</param>
        /// <param name="contractPanelUserRegistrationDocumentSignedOffLine">Indicates if the contract document was signed off line</param>
        public void SetSingedOffLineIndicators(bool? acknowledgeNdaPanelUserRegistrationDocumentSignedOffLine, bool? biasCoidPanelUserRegistrationDocumentSignedOffLine, bool? contractPanelUserRegistrationDocumentSignedOffLine)
        {
            this.AcknowledgeNdaPanelUserRegistrationDocumentSignedOffLine = acknowledgeNdaPanelUserRegistrationDocumentSignedOffLine;
            this.BiasCoidPanelUserRegistrationDocumentSignedOffLine = biasCoidPanelUserRegistrationDocumentSignedOffLine;
            this.ContractPanelUserRegistrationDocumentSignedOffLine = contractPanelUserRegistrationDocumentSignedOffLine;
        }
        /// <summary>
        /// Initialize the document entity contract properties.
        /// </summary>
        /// <param name="contractDocumentContractStatusId">PanelUserRegistration contract document contract item status id</param>
        /// <param name="contractStatusLabel">PanelUserRegistration contract document contract status label</param>
        /// <param name="feeAmount">PanelUserRegistration entity contract fee</param>
        /// <param name="canAddAddendum">Whether the contract can have an addendum added</param>
        public void SetContractInfo(int contractDocumentContractStatusId, string contractStatusLabel, decimal? feeAmount, bool isOriginal, bool isBypass, bool isRegenerate, bool canAddAddendum )
        {
            this.ContractStatus = contractStatusLabel;
            this.ContractStatusId = contractDocumentContractStatusId;
            this.FeeAmount = feeAmount;
            this.isContractOriginal = isOriginal;
            this.isContractBypassed = isBypass;
            this.isContractRegenerated = isRegenerate;
            this.CanAddAddendum = canAddAddendum;
        }

        #endregion
        #region Attributes
        /// <summary>
        /// Panelist last name
        /// </summary>
        public string LastName { get; private set; }
        /// <summary>
        /// Panelist first name
        /// </summary>
        public string FirstName { get; private set; }
        /// <summary>
        /// Panelists participation type
        /// </summary>
        public string ClientParticipationType { get; private set; }
        /// <summary>
        /// Panel abbreviation
        /// </summary>
        public string PanelAbbreviation { get; private set; }
        /// <summary>
        /// Date personal information was verified
        /// </summary>
        public DateTime? VerifiedDate { get; private set; }
        /// <summary>
        /// Date Ack/NDA document was signed
        /// </summary>
        public DateTime? AckNdaDateSigned { get; private set; }
        /// <summary>
        /// Date BIAS/COI document was signed
        /// </summary>
        public DateTime? BiasCoiDateSigned { get; private set; }
        /// <summary>
        /// Payment Category
        /// </summary>
        public string PaymentCategory { get; private set; }
        /// <summary>
        /// Date contract document was signed
        /// </summary>
        public DateTime? ContractDateSigned { get; private set; }
        /// <summary>
        /// Date CV was received
        /// </summary>
        public DateTime? CvReceivedDate { get; private set; }
        /// <summary>
        /// Contract status ID
        /// </summary>
        public int ContractStatusId { get; private set; }
        /// <summary>
        /// Contract status
        /// </summary>
        public string ContractStatus { get; private set; }
        /// <summary>
        /// Contract status
        /// </summary>
        public decimal? FeeAmount { get; private set; }
        /// <summary>
        /// Contract status: isBypassed
        /// </summary>
        public bool? isContractBypassed { get; private set; }
        /// <summary>
        /// Contract status: isRegenerated
        /// </summary>
        public bool? isContractRegenerated { get; private set; }
        /// <summary>
        /// Contract status: isOriginal
        /// </summary>
        public bool? isContractOriginal { get; private set; }

        /// <summary>
        /// Whether the contract can have an addendum added
        /// </summary>
        public bool? CanAddAddendum { get; private set; }
        /// <summary>
        /// Date W9 was received
        /// </summary>
        public DateTime? W9ReceivedDate { get; private set; }
        /// <summary>
        /// Date W9 was verified
        /// </summary>
        public DateTime? W9VerifiedDate { get; private set; }
        /// <summary>
        /// W9 Institution (vendor) name
        /// </summary>
        public string W9Institution { get; private set; }
        /// <summary>
        /// W9 Street address
        /// </summary>
        public string W9Address { get; private set; }
        /// <summary>
        /// W9 Address state
        /// </summary>
        public string W9State { get; private set; }
        /// <summary>
        /// W9 Address zip code
        /// </summary>
        public string W9Zip { get; private set; }
        /// <summary>
        /// Panelist's User entity identifier
        /// </summary>
        public int UserID { get; private set; }
        /// <summary>
        /// Gets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int PanelUserAssignmentId { get; private set; }
        /// <summary>
        /// The PanelUserRegistrationDocumentId for the PanelUserRegistrationDocument entity identifying the Acknowledge/NDA document
        /// </summary>
        public int? AcknowledgeNdaPanelUserRegistrationDocumentId { get; private set; }
        /// <summary>
        /// The PanelUserRegistrationDocumentId for the PanelUserRegistrationDocument entity identifying the Bias/COI
        /// </summary>
        public int? BiasCoidPanelUserRegistrationDocumentId { get; private set; }
        /// <summary>
        /// The PanelUserRegistrationDocumentId for the PanelUserRegistrationDocument entity identifying the contract
        /// </summary>
        public int? ContractPanelUserRegistrationDocumentId { get; private set; }
        /// <summary>
        /// Was the Acknowledge/NDA document signed off line
        /// </summary>
        public bool? AcknowledgeNdaPanelUserRegistrationDocumentSignedOffLine { get; private set; }
        /// <summary>
        /// Was the Bias/COI signed off line
        /// </summary>
        public bool? BiasCoidPanelUserRegistrationDocumentSignedOffLine { get; private set; }
        /// <summary>
        /// Was the contract signed off line
        /// </summary>
        public bool? ContractPanelUserRegistrationDocumentSignedOffLine { get; private set; }
        /// <summary>
        /// W-9 Address 2
        /// </summary>
        public string Address2 { get; private set; }
        /// <summary>
        /// W-9 Address 3
        /// </summary>
        public string Address3 { get; private set; }
        /// <summary>
        /// W-9 Address 4
        /// </summary>
        public string Address4 { get; private set; }
        /// <summary>
        /// W-9 City
        /// </summary>
        public string City { get; private set; }
        /// <summary>
        /// W-9 Country full name
        /// </summary>
        public string CountryFullName { get; private set; }
        /// <summary>
        /// W-9 Country abbreviation
        /// </summary>
        public string CountryAbbreviation { get; private set; }
        /// <summary>
        /// W-9 country Id
        /// </summary>
        public int? CountryId { get; private set; }
        /// <summary>
        /// SessionPanel entiy identifier
        /// </summary>
        public int SessionPanelId { get; private set; }
        public string W9Status { get; private set; }
        public DateTime? W9StatusDate { get; private set; }
        public bool RestrictedAssignedFlag { get; set; }
        public string VendorName { get; set; }
        public int ProgramYearId { get; set; }

        #endregion
    }
}
