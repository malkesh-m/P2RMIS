using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    public class MeetingSetupViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingSetupViewModel"/> class.
        /// </summary>
        public MeetingSetupViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingSetupViewModel"/> class.
        /// </summary>
        /// <param name="clients">The clients.</param>
        public MeetingSetupViewModel(List<UserProfileClientModel> clients)
        {
            Clients = clients.ConvertAll(x => new KeyValuePair<int, string>(x.ClientId, x.ClientAbrv))
                .OrderBy(y => y.Value).ToList();
        }

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        public List<KeyValuePair<int, string>> Clients { get; private set; }

        /// <summary>
        /// Gets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public int ClientId { get; private set; }
    }
}