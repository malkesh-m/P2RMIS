using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    public class FeeScheduleSetupViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeeScheduleSetupViewModel"/> class.
        /// </summary>
        public FeeScheduleSetupViewModel()
        {
            Clients = new List<KeyValuePair<int, string>>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FeeScheduleSetupViewModel"/> class.
        /// </summary>
        /// <param name="clients">The clients.</param>
        public FeeScheduleSetupViewModel(List<UserProfileClientModel> clients) : this()
        {
            Clients = clients.ConvertAll(x => new KeyValuePair<int, string>(x.ClientId, x.ClientName));
        }

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        public List<KeyValuePair<int, string>> Clients { get; private set; }
    }
}