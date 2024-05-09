using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// Client view model
    /// </summary>
    public class ClientViewModel
    {
        public ClientViewModel() { }

        public ClientViewModel(IUserProfileClientModel client) {
            ClientId = client.ClientId;
            ClientName = client.ClientAbrv;
            ClientFullName = client.ClientName;
            IsActive = client.IsActive;
        }

        /// <summary>
        /// Gets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public int ClientId { get; private set; }

        /// <summary>
        /// Gets the name of the client.
        /// </summary>
        /// <value>
        /// The name of the client.
        /// </value>
        public string ClientName { get; private set; }
        /// <summary>
        /// Client full name (description).
        /// </summary>
        public string ClientFullName { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; private set; }
    }
}