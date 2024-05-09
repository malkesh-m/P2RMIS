using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sra.P2rmis.Web.UI.Models
{
    public class NomineeUpdateViewModel
    {
        public NomineeUpdateViewModel() { }
        /// <summary>
        /// Nominee type identifer
        /// </summary>
        public int NomineeTypeId { get; set; }
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Prefix identifer
        /// </summary>
        public int? PrefixId { get; set; }
        /// <summary>
        /// Date of birth
        /// </summary>
        public string DateOfBirth { get; set; }
        /// <summary>
        /// Gender identifier
        /// </summary>
        public int? GenderId { get; set; }
        /// <summary>
        /// Ethnicity identifier
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
        public int StateId { get; set; }
        /// <summary>
        /// Zip code
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// Country identifier
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// First phone type
        /// </summary>
        public int PhoneType1 { get; set; }
        /// <summary>
        /// First phone number
        /// </summary>
        public string PhoneNumber1 { get; set; }
        /// <summary>
        /// Second phone type
        /// </summary>
        public int? PhoneType2 { get; set; }
        /// <summary>
        /// Second phone number
        /// </summary>
        public string PhoneNumber2 { get; set; }
    }
}