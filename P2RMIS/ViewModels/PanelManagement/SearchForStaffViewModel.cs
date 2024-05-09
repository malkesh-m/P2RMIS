using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;
using Newtonsoft.Json;

namespace Sra.P2rmis.Web.ViewModels.PanelManagement
{
    /// <summary>
    /// Review result model
    /// </summary>
    public class SearchForStaffViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchForStaffViewModel"/> class.
        /// </summary>
        public SearchForStaffViewModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchForStaffViewModel"/> class.
        /// </summary>
        /// <param name="staffSearchResultModel">The staff search result model.</param>
        public SearchForStaffViewModel(IStaffSearchResultModel staffSearchResultModel)
        {
            StaffName = ViewHelpers.ConstructName(staffSearchResultModel.LastName, staffSearchResultModel.FirstName);
            UserId = staffSearchResultModel.UserId;
            UserInfoId = staffSearchResultModel.UserInfoId;
            IsOnPanel = staffSearchResultModel.IsOnPanel;
            Organization = staffSearchResultModel.Organization;
            Email = staffSearchResultModel.Email;
            Role = staffSearchResultModel.Role;
        }
        /// <summary>
        /// Gets the name of the reviewer.
        /// </summary>
        /// <value>
        /// The name of the reviewer.
        /// </value>
        [JsonProperty(PropertyName = "name")]
        public string StaffName { get; private set; }
        /// <summary>
        /// Gets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        [JsonProperty(PropertyName = "organization")]
        public string Organization { get; private set; }
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [JsonProperty(PropertyName = "userId")]
        public int? UserId { get; private set; }
        /// <summary>
        /// Gets the user information identifier.
        /// </summary>
        /// <value>
        /// The user information identifier.
        /// </value>
        [JsonProperty(PropertyName = "userInfoId")]
        public int? UserInfoId { get; private set; }
        /// <summary>
        /// Gets the is on panel status.
        /// </summary>
        /// <value>
        /// The is on panel status.
        /// </value>
        [JsonProperty(PropertyName = "isOnPanel")]
        public bool IsOnPanel { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can manage account.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can manage account; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "canManageAccount")]
        public bool CanManageAccount { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }
    }
}