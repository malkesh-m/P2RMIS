using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.ConsumerManagement
{
    /// <summary>
    /// Nominee model
    /// </summary>
    public class NomineeUpdateModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NomineeUpdateModel() { }
        /// <summary>
        /// Nominee's user identifier
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// Nominee's user info identifier
        /// </summary>
        public int? UserInfoId { get; set; }
        /// <summary>
        /// Nominee identifier
        /// </summary>
        public int? NomineeId { get; set; }
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Prefix
        /// </summary>
        public int? PrefixId { get; set; }
        /// <summary>
        /// Date on Birth
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Gender
        /// </summary>
        public int? GenderId { get; set; }
        /// <summary>
        /// Ethnicity
        /// </summary>
        public int? EthnicityId { get; set; }
        /// <summary>
        /// Address line 1
        /// </summary>
        public string Address1 { get; set; }
        /// <summary>
        /// Address line 2
        /// </summary>

        public string Address2 { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// State identifier
        /// </summary>
        public int? StateId { get; set; }
        /// <summary>
        /// Zip code
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// Country identifier
        /// </summary>
        public int? CountryId { get; set; }
        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// First phone Id
        /// </summary>
        public int? PhoneId1 { get; set; }
        /// <summary>
        /// First phone type
        /// </summary>
        public int? PhoneType1 { get; set; }
        /// <summary>
        /// First phone number
        /// </summary>
        public string PhoneNumber1 { get; set; }
        /// <summary>
        /// Second phone Id
        /// </summary>
        public int? PhoneId2 { get; set; }
        /// <summary>
        /// Second phone type
        /// </summary>
        public int? PhoneType2 { get; set; }
        /// <summary>
        /// Second phone number
        /// </summary>
        public string PhoneNumber2 { get; set; }
        /// <summary>
        /// Created date
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Modified date
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Last modified by
        /// </summary>
        public string LastModifiedBy { get; set; }
    }
}
