using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sra.P2rmis.Web.UI.Models
{
    public class NomineeSponsorUpdateViewModel
    {
        public NomineeSponsorUpdateViewModel() { }

        public NomineeSponsorUpdateViewModel(string nominatingOrganization, string nominatorAddress1, string nominatorAddress2,
            string nominatorCity, int nominatorStateId, string nominatorZip, int nominatorCountryId, 
            string nominatorFirstName, string nominatorLastName, string nominatorTitle, string nominatorEmail,
            int nominatorPhoneTypeId1, string nominatorPhoneNumber1, int? nominatorPhoneTypeId2, string nominatorPhoneNumber2)
        {
            NominatingOrganization = nominatingOrganization;
            NominatorAddress1 = nominatorAddress1;
            NominatorAddress2 = nominatorAddress2;
            NominatorCity = nominatorCity;
            NominatorStateId = nominatorStateId;
            NominatorZip = nominatorZip;
            NominatorCountryId = nominatorCountryId;
            NominatorFirstName = nominatorFirstName;
            NominatorLastName = nominatorLastName;
            NominatorTitle = nominatorTitle;
            NominatorEmail = nominatorEmail;
            NominatorPhoneTypeId1 = nominatorPhoneTypeId1;
            NominatorPhoneNumber1 = nominatorPhoneNumber1;
            NominatorPhoneTypeId2 = nominatorPhoneTypeId2;
            NominatorPhoneNumber2 = nominatorPhoneNumber2;
        }
        /// <summary>
        /// Nominating organization
        /// </summary>
        public string NominatingOrganization { get; set; }
        /// <summary>
        /// Nominator address line 1
        /// </summary>
        public string NominatorAddress1 { get; set; }
        /// <summary>
        /// Nominator address line 2
        /// </summary>
        public string NominatorAddress2 { get; set; }
        /// <summary>
        /// Nominator city
        /// </summary>
        public string NominatorCity { get; set; }
        /// <summary>
        /// Nominator state identifier
        /// </summary>
        public int NominatorStateId { get; set; }
        /// <summary>
        /// Nominator zip code
        /// </summary>
        public string NominatorZip { get; set; }
        /// <summary>
        /// Nominator country identifier
        /// </summary>
        public int NominatorCountryId { get; set; }
        /// <summary>
        /// Nominator first name
        /// </summary>
        public string NominatorFirstName { get; set; }
        /// <summary>
        /// Nominator last name
        /// </summary>
        public string NominatorLastName { get; set; }
        /// <summary>
        /// Nominator title
        /// </summary>
        public string NominatorTitle { get; set; }
        /// <summary>
        /// Nominator email address
        /// </summary>
        public string NominatorEmail { get; set; }
        /// <summary>
        /// Nominator first phone type
        /// </summary>
        public int NominatorPhoneTypeId1 { get; set; }
        /// <summary>
        /// Nominator first phone number
        /// </summary>
        public string NominatorPhoneNumber1 { get; set; }
        /// <summary>
        /// Nominator second phone type
        /// </summary>
        public int? NominatorPhoneTypeId2 { get; set; }
        /// <summary>
        /// Nominator second phone number
        /// </summary>
        public string NominatorPhoneNumber2 { get; set; }
    }
}