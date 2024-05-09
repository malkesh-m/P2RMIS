using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ProgramWizardViewModel
    {
        #region Constants

        #endregion        
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramViewModel"/> class.
        /// </summary>
        public ProgramWizardViewModel()
        {
            SetFiscalYears();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramViewModel"/> class.
        /// </summary>
        /// <param name="programSetupModel">The program setup model.</param>
        public ProgramWizardViewModel(IProgramSetupModel programSetupModel)
        {
            ClientProgramId = programSetupModel.ClientProgramId;
            ClientAbbr = programSetupModel.ClientAbrv;
            ClientName = programSetupModel.ClientDesc;
            Fy = programSetupModel.Year;
            ClientId = programSetupModel.ClientId;
            ProgramYearId = programSetupModel.ProgramYearId;
            SetFiscalYears();
        }

        /// <summary>
        /// Sets the clients.
        /// </summary>
        /// <param name="clients">The clients.</param>
        public void SetClients(List<UserProfileClientModel> clients)
        {
            Clients = clients.ConvertAll(x => new KeyValuePair<int, string>((int)x.ClientId, x.ClientName));
        }

        /// <summary>
        /// Sets the fiscal years.
        /// </summary>
        public void SetFiscalYears()
        {
            var fys = new List<KeyValuePair<string, string>>();
            var currentYear = GlobalProperties.P2rmisDateTimeNow.Year;
            if (!String.IsNullOrEmpty(Fy) && Convert.ToInt32(Fy) < currentYear - 1)
            {
                fys.Add(new KeyValuePair<string, string>(Fy, Fy));
            }
            for (var i = currentYear + 3; i >= currentYear -1; i--)
            {
                fys.Add(new KeyValuePair<string, string>(i.ToString(), i.ToString()));
            }
            FiscalYears = fys;
        }

        /// <summary>
        /// Gets or sets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        public List<KeyValuePair<int, string>> Clients { get; set; }

        /// <summary>
        /// Gets or sets the fiscal years.
        /// </summary>
        /// <value>
        /// The fiscal years.
        /// </value>
        public List<KeyValuePair<string, string>> FiscalYears { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        [JsonProperty("clientId")]
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        [JsonProperty("programYearId")]
        public int ProgramYearId { get; set; }

        /// <summary>
        /// Gets or sets the client program identifier.
        /// </summary>
        /// <value>
        /// The client program identifier.
        /// </value>
        [JsonProperty("clientProgramId")]
        public int ClientProgramId { get; set; }

        /// <summary>
        /// Gets or sets the client abbr.
        /// </summary>
        /// <value>
        /// The client abbr.
        /// </value>
        [JsonProperty("clientAbbr")]
        public string ClientAbbr { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>
        /// The name of the client.
        /// </value>
        [JsonProperty("clientName")]
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        [JsonProperty("index")]
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        [JsonProperty("fy")]
        public string Fy { get; set; }        
    }
}