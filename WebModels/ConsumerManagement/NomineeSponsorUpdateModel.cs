using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.ConsumerManagement
{
    public class NomineeSponsorUpdateModel
    {
        public NomineeSponsorUpdateModel() { }
        /// <summary>
        /// Nominator identifier
        /// </summary>
        public int? NominatorId { get; set; }
        /// <summary>
        /// Nominating organization
        /// </summary>
        public string NominatingOrganization { get; set; }
        /// <summary>
        /// Nominating Organization Id
        /// </summary>
        public int? NominatingOrganizationId { get; set; }
        /// <summary>
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
        public int? NominatorStateId { get; set; }
        /// <summary>
        /// Nominator zip code
        /// </summary>
        public string NominatorZip { get; set; }
        /// <summary>
        /// Nominator country identifier
        /// </summary>
        public int? NominatorCountryId { get; set; }
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
        /// Nominator first phone id
        /// </summary>
        public int? NominatorPhoneId1 { get; set; }
        /// <summary>
        /// Nominator first phone type
        /// </summary>
        public int? NominatorPhoneTypeId1 { get; set; }
        /// <summary>
        /// Nominator first phone number
        /// </summary>
        public string NominatorPhoneNumber1 { get; set; }
        /// <summary>
        /// Nominator second phone id
        /// </summary>
        public int? NominatorPhoneId2 { get; set; }
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
