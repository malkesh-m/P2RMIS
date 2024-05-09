using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Sra.P2rmis.WebModels.Criteria;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// Client program view model
    /// </summary>
    public class ClientProgramViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientProgramViewModel"/> class.
        /// </summary>
        public ClientProgramViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientProgramViewModel"/> class.
        /// </summary>
        /// <param name="clientProgramModel">The client program model.</param>
        public ClientProgramViewModel(IClientProgramModel clientProgramModel) {
            ClientProgramId = clientProgramModel.ClientProgramId;
            ProgramName = clientProgramModel.ProgramName;
            ProgramAbbr = clientProgramModel.ProgramAbbreviation;
            FiscalYears = clientProgramModel.FiscalYears;
        }

        /// <summary>
        /// Gets or sets the client program identifier.
        /// </summary>
        /// <value>
        /// The client program identifier.
        /// </value>
        [JsonProperty("clientProgramId")]
        public int ClientProgramId { get; set; }

        /// <summary>
        /// Gets or sets the name of the program.
        /// </summary>
        /// <value>
        /// The name of the program.
        /// </value>
        [JsonProperty("programName")]
        public string ProgramName { get; set; }

        /// <summary>
        /// Gets or sets the program abbr.
        /// </summary>
        /// <value>
        /// The program abbr.
        /// </value>
        [JsonProperty("programAbbr")]
        public string ProgramAbbr { get; set; }

        /// <summary>
        /// Gets or sets the fiscal years.
        /// </summary>
        /// <value>
        /// The fiscal years.
        /// </value>
        [JsonProperty("fiscalYears")]
        public List<string> FiscalYears { get; set; }
    }
}