using System;
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// User information pertaining to the found user
    /// </summary>
    public interface IFoundUserModel
    {
        /// <summary>
        /// Unique identifier for user
        /// </summary>
        int UserId { get; set; }
        /// <summary>
        /// Unique identifier for UserInfoId
        /// </summary>
        int UserInfoId { get; set; }
        /// <summary>
        /// Account Status
        /// </summary>
        string Status { get; set; }
        /// <summary>
        /// Gets or sets the status reason.
        /// </summary>
        /// <value>
        /// The status reason.
        /// </value>
        string StatusReason { get; set; }
        /// <summary>
        /// User's last name
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// User's first name
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// User's Middle Initial
        /// </summary>
        string MI { get; set; }
        /// <summmary>
        /// User create date
        /// </summary>
        DateTime? CreateDate { get; set; }
        /// <summary>
        /// User's Group
        /// </summary>
        IEnumerable<string> Group { get; set; }
        /// <summary>
        /// Primary address of user
        /// </summary>
        string Address1 { get; set; }
        /// <summary>
        /// Primary address (continued) of user
        /// </summary>
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
        /// Primary City
        /// </summary>
        string City { get; set; }
        /// <summary>
        /// State
        /// </summary>
        string State {get; set; }
        /// <summary>
        /// Country
        /// </summary>
        string Country { get; set; }
        /// <summary>
        /// State identifier
        /// </summary>
        int? StateId { get; set; }
        /// <summary>
        /// Country identifier
        /// </summary>
        int? CountryId { get; set; }
        /// <summary>
        /// Zip code
        /// </summary>
        string Zip { get; set; }
        /// <summary>\
        /// Primary email address
        /// </summary>
        string EmailAddress { get; set; }
        /// <summary>\
        /// Secondary email address
        /// </summary>
        string SecondaryEmailAddress { get; set; }
        /// <summary>
        /// Primary email identifier
        /// </summary>
        int? EmailId { get; set; }
        /// <summary>
        /// List of session panels the user is assigned to
        /// </summary>
        /// <summary>
        /// List of session panels the user is assigned to
        /// </summary>
        IEnumerable<int> SessionPanelId { get; set; }
        /// <summary>
        /// Relevancy ranking value
        /// </summary>
        decimal? RelevancyRank { get; set; }
    }
}
