using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// User information pertaining to the found user
    /// </summary>
    public class FoundUserModel : IFoundUserModel
    {
        /// <summary>
        /// Unique identifier for user
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Unique identifier for UserInfoId
        /// </summary>
        public int UserInfoId { get; set; }
        /// <summary>
        /// Account Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Gets or sets the status reason.
        /// </summary>
        /// <value>
        /// The status reason.
        /// </value>
        public string StatusReason { get; set; }
        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// User's Middle Initial
        /// </summary>
        public string MI { get; set; }
        /// <summmary>
        /// User create date
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// User's Group
        /// </summary>
        public IEnumerable<string> Group { get; set; }
        /// <summary>
        /// Primary address of user
        /// </summary>
        public string Address1 { get; set; }
        /// <summary>
        /// Primary address (continued) of user
        /// </summary>
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
        /// Primary City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// State identifier
        /// </summary>
        public int? StateId { get; set; }
        /// <summary>
        /// Country identifier
        /// </summary>
        public int? CountryId { get; set; }
        /// <summary>
        /// Zip code
        /// </summary>
        public string Zip { get; set; }
        /// <summary>\
        /// Primary email address
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>\
        /// Secondary email address
        /// </summary>
        public string SecondaryEmailAddress { get; set; }
        /// <summary>
        /// Primary email identifier
        /// </summary>
        public int? EmailId { get; set; }
        /// <summary>
        /// List of session panels the user is assigned to
        /// </summary>
        public IEnumerable<int> SessionPanelId { get; set; }
        /// <summary>
        /// Relevancy ranking value
        /// </summary>
        public decimal? RelevancyRank { get; set; }

        #region Helpers
        /// <summary>
        /// Name in format
        /// </summary>
        public string FormattedName
        {
            get
            {
                ArrayList nameList = new ArrayList();
                if (!string.IsNullOrWhiteSpace(this.LastName)) 
                {
                    nameList.Add(this.LastName);
                }
                if (!string.IsNullOrWhiteSpace(this.FirstName)) 
                {
                    nameList.Add(this.FirstName);
                }
                if (!string.IsNullOrWhiteSpace(this.MI)) 
                {
                    nameList.Add(this.MI);
                }

                return string.Join(", ", nameList.ToArray());
            }
        }
        /// <summary>
        /// Gets the formatted status.
        /// </summary>
        /// <value>
        /// The formatted status.
        /// </value>
        /// <remarks>TODO: move formatted values to view model and use CrossCuttingServices to re-format</remarks>
        public string FormattedStatus
        {
            get
            {
                return (!string.IsNullOrEmpty(StatusReason)) ? Status + "-" + StatusReason : Status;
            }
        }
        /// <summary>
        /// Group in format
        /// </summary>
        public string FormattedGroup
        {
            get
            {
                return String.Join(", ", this.Group);
            }
        }
        /// <summary>
        /// Address and email address in format
        /// </summary>
        public string FormattedAddress
        {
            get
            {
                ArrayList addressList = new ArrayList();
                if (!string.IsNullOrWhiteSpace(this.Address1))
                {
                    addressList.Add(this.Address1);
                }
                if (!string.IsNullOrWhiteSpace(this.Address2))
                {
                    addressList.Add(this.Address2);
                }
                if (!string.IsNullOrWhiteSpace(this.Address3))
                {
                    addressList.Add(this.Address3);
                }
                if (!string.IsNullOrWhiteSpace(this.Address4))
                {
                    addressList.Add(this.Address4);
                }
                if (!string.IsNullOrWhiteSpace(this.City))
                {
                    addressList.Add(this.City);
                }
                if (!string.IsNullOrWhiteSpace(this.State))
                {
                    addressList.Add(this.State);
                }
                if (!string.IsNullOrWhiteSpace(this.Zip))
                {
                    addressList.Add(this.Zip);
                }
                if (!string.IsNullOrEmpty(this.Country))
                {
                    addressList.Add(this.Country);
                }
                return string.Join(", ", addressList.ToArray());
            }
        }
        /// <summary>
        /// Count of session panels
        /// </summary>
        public int SessionPanelCount
        {
            get
            {
                return this.SessionPanelId.ToList().Count();
            }
        }
        #endregion
    }
}
