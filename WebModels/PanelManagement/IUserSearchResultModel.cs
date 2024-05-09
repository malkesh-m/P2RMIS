

using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    public interface IUserSearchResultModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        int UserId { get; }

        /// <summary>
        /// Gets or sets the user information identifier.
        /// </summary>
        /// <value>
        /// The user information identifier.
        /// </value>
        int UserInfoId { get; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        string FirstName { get; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        string LastName { get; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }
    }
}

