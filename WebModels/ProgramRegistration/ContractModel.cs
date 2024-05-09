using System;
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ProgramRegistration
{
    /// <summary>
    /// Interface for contract document data
    /// </summary>
    public interface IContractModel
    {
        /// <summary>
        /// Client abbreviation
        /// </summary>
        /// <value>
        /// The client abbreviation.
        /// </value>
        string ClientAbbreviation { get; set; }

        /// <summary>
        /// Fiscal year
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        string FiscalYear { get; set; }

        /// <summary>
        /// Program description
        /// </summary>
        /// <value>
        /// The program description.
        /// </value>
        string ProgramDescription { get; set; }

        /// <summary>
        /// Panel name
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        string PanelName { get; set; }

        /// <summary>
        /// Gets or sets the full name with suffix.
        /// </summary>
        /// <value>
        /// The full name with suffix.
        /// </value>
        string FullNameWithSuffix { get; set; }

        /// <summary>
        /// Gets or sets the name of the participant type.
        /// </summary>
        /// <value>
        /// The name of the participant type.
        /// </value>
        string ParticipantTypeName { get; set; }

        /// <summary>
        /// Gets or sets the period of performance start date.
        /// </summary>
        /// <value>
        /// The period of performance start date.
        /// </value>
        DateTime? PeriodOfPerformanceStartDate { get; set; }

        /// <summary>
        /// Gets or sets the period of performance end date.
        /// </summary>
        /// <value>
        /// The period of performance end date.
        /// </value>
        DateTime? PeriodOfPerformanceEndDate { get; set; }

        /// <summary>
        /// Gets or sets the manager list.
        /// </summary>
        /// <value>
        /// The semi-colon delimited manager list.
        /// </value>
        List<string> ManagerList { get; set; }

        /// <summary>
        /// Gets or sets the consultant fee text.
        /// </summary>
        /// <value>
        /// The consultant fee text.
        /// </value>
        string ConsultantFeeText { get; set; }

        /// <summary>
        /// Gets or sets the description of work.
        /// </summary>
        /// <value>
        /// The description of work.
        /// </value>
        string DescriptionOfWork { get; set; }

        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>
        /// The address2.
        /// </value>
        string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the address3.
        /// </summary>
        /// <value>
        /// The address3.
        /// </value>
        string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the address4.
        /// </summary>
        /// <value>
        /// The address4.
        /// </value>
        string Address4 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        string City { get; set; }

        /// <summary>
        /// Gets or sets the state abrv.
        /// </summary>
        /// <value>
        /// The state abrv.
        /// </value>
        string StateAbrv { get; set; }

        /// <summary>
        /// Gets or sets the zipcode.
        /// </summary>
        /// <value>
        /// The zipcode.
        /// </value>
        string Zipcode { get; set; }

        /// <summary>
        /// Gets or sets the country abrv.
        /// </summary>
        /// <value>
        /// The country abrv.
        /// </value>
        string CountryAbrv { get; set; }
        /// <summary>
        /// if w9 is accurate or inaccurate
        /// </summary>
        bool? W9Verified { get; set; }

        /// <summary>
        /// The consultant fee amount
        /// </summary>
        decimal? ConsultantFee { get; set; }
        /// <summary>
        /// Whether the contract was bypassed
        /// </summary>
        bool IsContractBypassed { get; set; }

        /// <summary>
        /// Whether the contract is custom uploaded by a user (not system generated)
        /// </summary>
        bool IsContractCustomized { get; set; }
        /// <summary>
        /// The location the document exists
        /// </summary>
        string ContractFileLocation { get; set; }
    }

    /// <summary>
    /// Model for contract document data
    /// </summary>
    public class ContractModel : IContractModel
    {
        #region Properties
        /// <summary>
        /// Client abbreviation
        /// </summary>
        /// <value>
        /// The client abbreviation.
        /// </value>
        public string ClientAbbreviation { get; set; }
        /// <summary>
        /// Fiscal year
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        public string FiscalYear { get; set; }
        /// <summary>
        /// Program description
        /// </summary>
        /// <value>
        /// The program description.
        /// </value>
        public string ProgramDescription { get; set; }
        /// <summary>
        /// Panel name
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        public string PanelName { get; set; }

        /// <summary>
        /// Gets or sets the full name with suffix.
        /// </summary>
        /// <value>
        /// The full name with suffix.
        /// </value>
        public string FullNameWithSuffix { get; set; }
        /// <summary>
        /// Gets or sets the name of the participant type.
        /// </summary>
        /// <value>
        /// The name of the participant type.
        /// </value>
        public string ParticipantTypeName { get; set; }
        /// <summary>
        /// Gets or sets the period of performance start date.
        /// </summary>
        /// <value>
        /// The period of performance start date.
        /// </value>
        public DateTime? PeriodOfPerformanceStartDate { get; set; }
        /// <summary>
        /// Gets or sets the period of performance end date.
        /// </summary>
        /// <value>
        /// The period of performance end date.
        /// </value>
        public DateTime? PeriodOfPerformanceEndDate { get; set; }
        /// <summary>
        /// Gets or sets the manager list.
        /// </summary>
        /// <value>
        /// The semi-colon delimited manager list.
        /// </value>
        public List<string> ManagerList { get; set; }
        /// <summary>
        /// Gets or sets the consultant fee text.
        /// </summary>
        /// <value>
        /// The consultant fee text.
        /// </value>
        public string ConsultantFeeText { get; set; }

        /// <summary>
        /// The numeric consultant fee value
        /// </summary>
        public decimal? ConsultantFee { get; set; }
        /// <summary>
        /// Gets or sets the description of work.
        /// </summary>
        /// <value>
        /// The description of work.
        /// </value>
        public string DescriptionOfWork { get; set; }
        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        public string Address1 { get; set; }
        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>
        /// The address2.
        /// </value>
        public string Address2 { get; set; }
        /// <summary>
        /// Gets or sets the address3.
        /// </summary>
        /// <value>
        /// The address3.
        /// </value>
        public string Address3 { get; set; }
        /// <summary>
        /// Gets or sets the address4.
        /// </summary>
        /// <value>
        /// The address4.
        /// </value>
        public string Address4 { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the state abrv.
        /// </summary>
        /// <value>
        /// The state abrv.
        /// </value>
        public string StateAbrv { get; set; }
        /// <summary>
        /// Gets or sets the zipcode.
        /// </summary>
        /// <value>
        /// The zipcode.
        /// </value>
        public string Zipcode { get; set; }
        /// <summary>
        /// Gets or sets the country abrv.
        /// </summary>
        /// <value>
        /// The country abrv.
        /// </value>
        public string CountryAbrv { get; set; }
        /// <summary>
        /// check if w9 is accurate or inacurate
        /// </summary>
        public bool? W9Verified { get; set; }

        /// <summary>
        /// Whether the contract was bypassed
        /// </summary>
        public bool IsContractBypassed { get; set; }

        /// <summary>
        /// Whether the contract is custom uploaded by a user (not system generated)
        /// </summary>
        public bool IsContractCustomized { get; set; }

        /// <summary>
        /// The location the document exists
        /// </summary>
        public string ContractFileLocation { get; set; }
        #endregion
        #region Constructor


        /// <summary>
        /// Initializes a new instance of the <see cref="ContractModel" /> class.
        /// </summary>
        /// <param name="clientAbbreviation">The client abbreviation.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programDescription">The program description.</param>
        /// <param name="panelName">Name of the panel.</param>
        /// <param name="fullNameWithSuffix">The full name with suffix.</param>
        /// <param name="participantTypeName">Name of the participant type.</param>
        /// <param name="periodOfPerformanceStartDate">The period of performance start date.</param>
        /// <param name="periodOfPerformanceEndDate">The period of performance end date.</param>
        /// <param name="managerList">The manager list.</param>
        /// <param name="consultantFeeText">The consultant fee text.</param>
        /// <param name="descriptionOfWork">The description of work.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="address3">The address3.</param>
        /// <param name="address4">The address4.</param>
        /// <param name="city">The city.</param>
        /// <param name="stateAbrv">The state abrv.</param>
        /// <param name="zipcode">The zipcode.</param>
        /// <param name="countryAbrv">The country abrv.</param>
        /// <param name="consultantFee">The consultant fee amount</param>
        public ContractModel(string clientAbbreviation, string fiscalYear, string programDescription, string panelName, string fullNameWithSuffix, string participantTypeName, DateTime? periodOfPerformanceStartDate, DateTime? periodOfPerformanceEndDate, List<string> managerList, string consultantFeeText, string descriptionOfWork, string address1, string address2, string address3, string address4, string city, string stateAbrv, string zipcode, string countryAbrv, bool? w9Verified, decimal? consultantFee, bool isContractBypassed, bool isContractCustomized)
        {
            ClientAbbreviation = clientAbbreviation;
            FiscalYear = fiscalYear;
            ProgramDescription = programDescription;
            PanelName = panelName;
            FullNameWithSuffix = fullNameWithSuffix;
            ParticipantTypeName = participantTypeName;
            PeriodOfPerformanceStartDate = periodOfPerformanceStartDate;
            PeriodOfPerformanceEndDate = periodOfPerformanceEndDate;
            ManagerList = managerList;
            ConsultantFeeText = consultantFeeText;
            DescriptionOfWork = descriptionOfWork;
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            Address4 = address4;
            City = city;
            StateAbrv = stateAbrv;
            Zipcode = zipcode;
            CountryAbrv = countryAbrv;
            W9Verified = w9Verified;
            ConsultantFee = consultantFee;
            IsContractBypassed = isContractBypassed;
            IsContractCustomized = isContractCustomized;
        }

        

        /// <summary>
        /// Default constructor of the <see cref="ContractModel"/> class.
        /// </summary>
        public ContractModel()
        {
        }

        #endregion

    }
}
