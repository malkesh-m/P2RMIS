using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Setup;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ProgramViewModel
    {
        #region Constants

        #endregion        
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramViewModel"/> class.
        /// </summary>
        public ProgramViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramViewModel"/> class.
        /// </summary>
        /// <param name="programSetupModel">The program setup model.</param>
        public ProgramViewModel(IProgramSetupModel programSetupModel)
        {
            ClientProgramId = programSetupModel.ClientProgramId;
            ClientAbbr = programSetupModel.ClientAbrv;
            ClientName = programSetupModel.ClientDesc;
            Fy = programSetupModel.Year;
            ProgramAbbr = programSetupModel.ProgramAbbreviation;
            ProgramName = programSetupModel.ProgramDescription;
            ActiveText = ViewHelpers.FormatBoolean(programSetupModel.Active);
            Cycles = programSetupModel.CycleCount;
            Awards = programSetupModel.ProgramMechanismCount;
            Mtgs = programSetupModel.ClientMeetingCount;
            ClientId = programSetupModel.ClientId;
            ProgramYearId = programSetupModel.ProgramYearId;
        }

        /// <summary>
        /// Gets the clientid.
        /// </summary>
        /// <value>
        /// The clientid.
        /// </value>
        [JsonProperty("clientId")]
        public int ClientId { get; private set; }

        /// <summary>
        /// Gets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        [JsonProperty("programYearId")]
        public int ProgramYearId { get; private set; }

        /// <summary>
        /// Gets the client program identifier.
        /// </summary>
        /// <value>
        /// The client program identifier.
        /// </value>
        [JsonProperty("clientProgramId")]
        public int ClientProgramId { get; private set; }

        /// <summary>
        /// Gets the client abbr.
        /// </summary>
        /// <value>
        /// The client abbr.
        /// </value>
        [JsonProperty("clientAbbr")]
        public string ClientAbbr { get; private set; }

        /// <summary>
        /// Gets the name of the client.
        /// </summary>
        /// <value>
        /// The name of the client.
        /// </value>
        [JsonProperty("clientName")]
        public string ClientName { get; private set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        [JsonProperty("index")]
        public int Index { get; set; }

        /// <summary>
        /// Gets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        [JsonProperty("fy")]
        public string Fy { get; private set; }

        [JsonProperty("programAbbr")]
        public string ProgramAbbr { get; private set; }

        /// <summary>
        /// Gets the program name.
        /// </summary>
        /// <value>
        /// The program name.
        /// </value>
        [JsonProperty("programName")]
        public string ProgramName { get; private set; }

        /// <summary>
        /// Gets the active text.
        /// </summary>
        /// <value>
        /// The active text.
        /// </value>
        [JsonProperty("active")]
        public string ActiveText { get; set; }
        /// <summary>
        /// The count of cycles.
        /// </summary>
        [JsonProperty("cycles")]
        public int Cycles { get; private set; }
        /// <summary>
        /// Gets the count of awards.
        /// </summary>
        /// <value>
        /// The count of awards.
        /// </value>
        [JsonProperty("awards")]
        public int Awards { get; private set; }

        /// <summary>
        /// Gets the count of meetings.
        /// </summary>
        /// <value>
        /// The count of meetings.
        /// </value>
        [JsonProperty("mtgs")]
        public int Mtgs { get; private set; }
    }
}