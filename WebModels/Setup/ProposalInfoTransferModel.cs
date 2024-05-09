using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Proposal information transfer root model
    /// </summary>
    [Serializable]
    [XmlRoot("p2rmis")]
    public class ProposalInfoTransferModel
    {
        /// <summary>
        /// Gets or sets the proposal information list.
        /// </summary>
        /// <value>
        /// The proposal information list.
        /// </value>
        [XmlElement("proposalInfo")]
        public List<ProposalInfo> ProposalInfoList { get; set; }
    }
    /// <summary>
    /// Proposal information
    /// </summary>
    [Serializable]
    public class ProposalInfo
    {
        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        [XmlElement("logNo")]
        public SimpleString LogNo { get; set; }
        /// <summary>
        /// Gets or sets the compliance status.
        /// </summary>
        /// <value>
        /// The compliance status.
        /// </value>
        [XmlElement("complianceStatus")]
        public SimpleString ComplianceStatus { get; set; }
        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        [XmlElement("program")]
        public SimpleString Program { get; set; }
        /// <summary>
        /// Gets or sets the gg tracking NBR.
        /// </summary>
        /// <value>
        /// The gg tracking NBR.
        /// </value>
        [XmlElement("ggTrackingNbr")]
        public SimpleString GgTrackingNbr { get; set; }
        /// <summary>
        /// Gets or sets the opportunity identifier.
        /// </summary>
        /// <value>
        /// The opportunity identifier.
        /// </value>
        [XmlElement("opportunityId")]
        public SimpleString OpportunityId { get; set; }
        /// <summary>
        /// Gets or sets the fy.
        /// </summary>
        /// <value>
        /// The fy.
        /// </value>
        [XmlElement("fy")]
        public SimpleString Fy { get; set; }
        /// <summary>
        /// Gets or sets the type of the award.
        /// </summary>
        /// <value>
        /// The type of the award.
        /// </value>
        [XmlElement("awardType")]
        public SimpleString AwardType { get; set; }
        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>
        /// The keywords.
        /// </value>
        [XmlElement("keywords")]
        public SimpleString Keywords { get; set; }
        /// <summary>
        /// Gets or sets the rceipt cycle.
        /// </summary>
        /// <value>
        /// The rceipt cycle.
        /// </value>
        [XmlElement("receiptCycle")]
        public SimpleString ReceiptCycle { get; set; }
        /// <summary>
        /// Gets or sets the proposal title.
        /// </summary>
        /// <value>
        /// The proposal title.
        /// </value>
        [XmlElement("proposalTitle")]
        public SimpleString ProposalTitle { get; set; }
        /// <summary>
        /// Gets or sets the total funding.
        /// </summary>
        /// <value>
        /// The total funding.
        /// </value>
        [XmlElement("totalFunding")]
        public SimpleString TotalFunding { get; set; }
        /// <summary>
        /// Gets or sets the requested direct.
        /// </summary>
        /// <value>
        /// The requested direct.
        /// </value>
        [XmlElement("requestedDirect")]
        public SimpleString RequestedDirect { get; set; }
        /// <summary>
        /// Gets or sets the requested indirect.
        /// </summary>
        /// <value>
        /// The requested indirect.
        /// </value>
        [XmlElement("requestedIndirect")]
        public SimpleString RequestedIndirect { get; set; }
        /// <summary>
        /// Gets or sets the duration of the requested.
        /// </summary>
        /// <value>
        /// The duration of the requested.
        /// </value>
        [XmlElement("requestedDuration")]
        public SimpleString RequestedDuration { get; set; }
        /// <summary>
        /// Gets or sets the research area.
        /// </summary>
        /// <value>
        /// The research area.
        /// </value>
        [XmlElement("researchArea")]
        public SimpleString ResearchArea { get; set; }
        /// <summary>
        /// Gets or sets the type org.
        /// </summary>
        /// <value>
        /// The type org.
        /// </value>
        [XmlElement("typeOrg")]
        public SimpleString TypeOrg { get; set; }
        /// <summary>
        /// Gets or sets the proposal receipt date.
        /// </summary>
        /// <value>
        /// The proposal receipt date.
        /// </value>
        [XmlElement("proposalReceiptDate")]
        public SimpleString ProposalReceiptDate { get; set; }
        /// <summary>
        /// Gets or sets the withdrawn date.
        /// </summary>
        /// <value>
        /// The withdrawn date.
        /// </value>
        [XmlElement("withdrawnDate")]
        public SimpleString WithdrawnDate { get; set; }
        /// <summary>
        /// Gets or sets the record letters.
        /// </summary>
        /// <value>
        /// The record letters.
        /// </value>
        [XmlElement("recLetters")]
        public SimpleString RecLetters { get; set; }
        /// <summary>
        /// Gets or sets the center sequence.
        /// </summary>
        /// <value>
        /// The center sequence.
        /// </value>
        [XmlElement("centerSequence")]
        public SimpleString CenterSequence { get; set; }
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        [XmlElement("startDate")]
        public SimpleString StartDate { get; set; }
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        [XmlElement("endDate")]
        public SimpleString EndDate { get; set; }
        /// <summary>
        /// Gets or sets the first name of the pi.
        /// </summary>
        /// <value>
        /// The first name of the pi.
        /// </value>
        [XmlElement("piFirstName")]
        public SimpleString PiFirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name of the pi.
        /// </summary>
        /// <value>
        /// The last name of the pi.
        /// </value>
        [XmlElement("piLastName")]
        public SimpleString PiLastName { get; set; }
        /// <summary>
        /// Gets or sets the pi middle initial.
        /// </summary>
        /// <value>
        /// The pi middle initial.
        /// </value>
        [XmlElement("piMiddleInitial")]
        public SimpleString PiMiddleInitial { get; set; }
        /// <summary>
        /// Gets or sets the pi prefix.
        /// </summary>
        /// <value>
        /// The pi prefix.
        /// </value>
        [XmlElement("piPrefix")]
        public SimpleString PiPrefix { get; set; }
        /// <summary>
        /// Gets or sets the pi suffix.
        /// </summary>
        /// <value>
        /// The pi suffix.
        /// </value>
        [XmlElement("piSuffix")]
        public SimpleString PiSuffix { get; set; }
        /// <summary>
        /// Gets or sets the pi title.
        /// </summary>
        /// <value>
        /// The pi title.
        /// </value>
        [XmlElement("piTitle")]
        public SimpleString PiTitle { get; set; }
        /// <summary>
        /// Gets or sets the pi gender.
        /// </summary>
        /// <value>
        /// The pi gender.
        /// </value>
        [XmlElement("piGender")]
        public SimpleString PiGender { get; set; }
        /// <summary>
        /// Gets or sets the pi department.
        /// </summary>
        /// <value>
        /// The pi department.
        /// </value>
        [XmlElement("piDepartment")]
        public SimpleString PiDepartment { get; set; }
        /// <summary>
        /// Gets or sets the pi organization.
        /// </summary>
        /// <value>
        /// The pi organization.
        /// </value>
        [XmlElement("piOrganization")]
        public SimpleString PiOrganization { get; set; }
        /// <summary>
        /// Gets or sets the cr organization.
        /// </summary>
        /// <value>
        /// The cr organization.
        /// </value>
        [XmlElement("crOrganization")]
        public SimpleString CrOrganization { get; set; }
        /// <summary>
        /// Gets or sets the pi address1.
        /// </summary>
        /// <value>
        /// The pi address1.
        /// </value>
        [XmlElement("piAddress1")]
        public SimpleString PiAddress1 { get; set; }
        /// <summary>
        /// Gets or sets the pi address2.
        /// </summary>
        /// <value>
        /// The pi address2.
        /// </value>
        [XmlElement("piAddress2")]
        public SimpleString PiAddress2 { get; set; }
        /// <summary>
        /// Gets or sets the pi address4.
        /// </summary>
        /// <value>
        /// The pi address4.
        /// </value>
        [XmlElement("piAddress3")]
        public SimpleString PiAddress4 { get; set; }
        /// <summary>
        /// Gets or sets the pi city.
        /// </summary>
        /// <value>
        /// The pi city.
        /// </value>
        [XmlElement("piCity")]
        public SimpleString PiCity { get; set; }
        /// <summary>
        /// Gets or sets the state of the pi.
        /// </summary>
        /// <value>
        /// The state of the pi.
        /// </value>
        [XmlElement("piState")]
        public SimpleString PiState { get; set; }
        /// <summary>
        /// Gets or sets the pi zip code.
        /// </summary>
        /// <value>
        /// The pi zip code.
        /// </value>
        [XmlElement("piZipCode")]
        public SimpleString PiZipCode { get; set; }
        /// <summary>
        /// Gets or sets the pi country code.
        /// </summary>
        /// <value>
        /// The pi country code.
        /// </value>
        [XmlElement("piCountryCode")]
        public SimpleString PiCountryCode { get; set; }
        /// <summary>
        /// Gets or sets the pi email.
        /// </summary>
        /// <value>
        /// The pi email.
        /// </value>
        [XmlElement("piEmail")]
        public SimpleString PiEmail { get; set; }
        /// <summary>
        /// Gets or sets the pi telephone.
        /// </summary>
        /// <value>
        /// The pi telephone.
        /// </value>
        [XmlElement("piTelephone")]
        public SimpleString PiTelephone { get; set; }
        /// <summary>
        /// Gets or sets the pi fax.
        /// </summary>
        /// <value>
        /// The pi fax.
        /// </value>
        [XmlElement("piFax")]
        public SimpleString PiFax { get; set; }
        /// <summary>
        /// Gets or sets the first name of the cr.
        /// </summary>
        /// <value>
        /// The first name of the cr.
        /// </value>
        [XmlElement("crFirstName")]
        public SimpleString CrFirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name of the cr.
        /// </summary>
        /// <value>
        /// The last name of the cr.
        /// </value>
        [XmlElement("crLastName")]
        public SimpleString CrLastName { get; set; }
        /// <summary>
        /// Gets or sets the cr middle initial.
        /// </summary>
        /// <value>
        /// The cr middle initial.
        /// </value>
        [XmlElement("crMiddleInitial")]
        public SimpleString CrMiddleInitial { get; set; }
        /// <summary>
        /// Gets or sets the cr prefix.
        /// </summary>
        /// <value>
        /// The cr prefix.
        /// </value>
        [XmlElement("crPrefix")]
        public SimpleString CrPrefix { get; set; }
        /// <summary>
        /// Gets or sets the cr suffix.
        /// </summary>
        /// <value>
        /// The cr suffix.
        /// </value>
        [XmlElement("crSuffix")]
        public SimpleString CrSuffix { get; set; }
        /// <summary>
        /// Gets or sets the cr title.
        /// </summary>
        /// <value>
        /// The cr title.
        /// </value>
        [XmlElement("crTitle")]
        public SimpleString CrTitle { get; set; }
        /// <summary>
        /// Gets or sets the cr department.
        /// </summary>
        /// <value>
        /// The cr department.
        /// </value>
        [XmlElement("crDepartment")]
        public SimpleString CrDepartment { get; set; }
        /// <summary>
        /// Gets or sets the cr address1.
        /// </summary>
        /// <value>
        /// The cr address1.
        /// </value>
        [XmlElement("crAddress1")]
        public SimpleString CrAddress1 { get; set; }
        /// <summary>
        /// Gets or sets the cr address2.
        /// </summary>
        /// <value>
        /// The cr address2.
        /// </value>
        [XmlElement("crAddress2")]
        public SimpleString CrAddress2 { get; set; }
        /// <summary>
        /// Gets or sets the cr address3.
        /// </summary>
        /// <value>
        /// The cr address3.
        /// </value>
        [XmlElement("crAddress3")]
        public SimpleString CrAddress3 { get; set; }
        /// <summary>
        /// Gets or sets the cr city.
        /// </summary>
        /// <value>
        /// The cr city.
        /// </value>
        [XmlElement("crCity")]
        public SimpleString CrCity { get; set; }
        /// <summary>
        /// Gets or sets the state of the cr.
        /// </summary>
        /// <value>
        /// The state of the cr.
        /// </value>
        [XmlElement("crState")]
        public SimpleString CrState { get; set; }
        /// <summary>
        /// Gets or sets the cr zip code.
        /// </summary>
        /// <value>
        /// The cr zip code.
        /// </value>
        [XmlElement("crZipCode")]
        public SimpleString CrZipCode { get; set; }
        /// <summary>
        /// Gets or sets the cr country code.
        /// </summary>
        /// <value>
        /// The cr country code.
        /// </value>
        [XmlElement("crCountryCode")]
        public SimpleString CrCountryCode { get; set; }
        /// <summary>
        /// Gets or sets the cr telephone.
        /// </summary>
        /// <value>
        /// The cr telephone.
        /// </value>
        [XmlElement("crTelephone")]
        public SimpleString CrTelephone { get; set; }
        /// <summary>
        /// Gets or sets the cr fax.
        /// </summary>
        /// <value>
        /// The cr fax.
        /// </value>
        [XmlElement("crFax")]
        public SimpleString CrFax { get; set; }
        /// <summary>
        /// Gets or sets the cr email.
        /// </summary>
        /// <value>
        /// The cr email.
        /// </value>
        [XmlElement("crEmail")]
        public SimpleString CrEmail { get; set; }
        /// <summary>
        /// Gets or sets the first name of the mentor.
        /// </summary>
        /// <value>
        /// The first name of the mentor.
        /// </value>
        [XmlElement("mentorFirstName")]
        public SimpleString MentorFirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name of the mentor.
        /// </summary>
        /// <value>
        /// The last name of the mentor.
        /// </value>
        [XmlElement("mentorLastName")]
        public SimpleString MentorLastName { get; set; }
        /// <summary>
        /// Gets or sets the mentor middle initial.
        /// </summary>
        /// <value>
        /// The mentor middle initial.
        /// </value>
        [XmlElement("mentorMiddleInitial")]
        public SimpleString MentorMiddleInitial { get; set; }
        /// <summary>
        /// Gets or sets the key gender used.
        /// </summary>
        /// <value>
        /// The key gender used.
        /// </value>
        [XmlElement("keyGenderUsed")]
        public SimpleString KeyGenderUsed { get; set; }
        /// <summary>
        /// Gets or sets the coi data list.
        /// </summary>
        /// <value>
        /// The coi data list.
        /// </value>
        [XmlElement("coiData")]
        public List<CoiData> CoiDataList { get; set; }
        /// <summary>
        /// Gets or sets the emphasis areas.
        /// </summary>
        /// <value>
        /// The emphasis areas.
        /// </value>
        [XmlElement("emphasisAreas")]
        public SimpleString EmphasisAreas { get; set; }
    }
    /// <summary>
    /// Conflict of interest data
    /// </summary>
    [Serializable]
    public class CoiData
    {
        /// <summary>
        /// Gets or sets the first name of the coi.
        /// </summary>
        /// <value>
        /// The first name of the coi.
        /// </value>
        [XmlElement("coiFirstName")]
        public SimpleString CoiFirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name of the coi.
        /// </summary>
        /// <value>
        /// The last name of the coi.
        /// </value>
        [XmlElement("coiLastName")]
        public SimpleString CoiLastName { get; set; }
        /// <summary>
        /// Gets or sets the name of the coi org.
        /// </summary>
        /// <value>
        /// The name of the coi org.
        /// </value>
        [XmlElement("coiOrgName")]
        public SimpleString CoiOrgName { get; set; }
        /// <summary>
        /// Gets or sets the coi phone.
        /// </summary>
        /// <value>
        /// The coi phone.
        /// </value>
        [XmlElement("coiPhone")]
        public SimpleString CoiPhone { get; set; }
        /// <summary>
        /// Gets or sets the coi email.
        /// </summary>
        /// <value>
        /// The coi email.
        /// </value>
        [XmlElement("coiEmail")]
        public SimpleString CoiEmail { get; set; }
        /// <summary>
        /// Gets or sets the type of the coi.
        /// </summary>
        /// <value>
        /// The type of the coi.
        /// </value>
        [XmlElement("coiType")]
        public SimpleString CoiType { get; set; }
        /// <summary>
        /// Gets or sets the coi source.
        /// </summary>
        /// <value>
        /// The coi source.
        /// </value>
        [XmlElement("coiSource")]
        public SimpleString CoiSource { get; set; }
    }
    /// <summary>
    /// Simple string node
    /// </summary>
    [Serializable]
    public class SimpleString
    {
        [XmlText]
        public string Value { get; set; }
    }
}
