using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Web.UI.Models
{
    public class SessionSetupViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionSetupViewModel"/> class.
        /// </summary>
        public SessionSetupViewModel()
        {
        }
                
        /// <summary>
        /// Gets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public int? ClientId { get; set; }       

        /// <summary>
        /// Gets or sets the meeting identifier.
        /// </summary>
        /// <value>
        /// The meeting identifier.
        /// </value>
        public int? MeetingId { get; set; }
    }
}