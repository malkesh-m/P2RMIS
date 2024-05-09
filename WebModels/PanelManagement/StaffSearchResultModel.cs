

using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Interface representing results returned from a reviewer search
    /// </summary>
    public interface IStaffSearchResultModel : IUserSearchResultModel
    {
        /// <summary>
        /// Gets a value indicating whether this user is on panel.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is assigned; otherwise, <c>false</c>.
        /// </value>
        bool IsOnPanel { get; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        string Email { get; set; }
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        string Role { get; set; }
        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        string Organization { get; set; }
    }
    
    public class StaffSearchResultModel : IStaffSearchResultModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the user information identifier.
        /// </summary>
        /// <value>
        /// The user information identifier.
        /// </value>
        public int UserInfoId { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        public string Organization { get; set; }
        /// <summary>
        /// Gets a value indicating whether this user is on panel.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is on the panel Potential or Assigned; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnPanel { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role { get; set; }
    }
}

