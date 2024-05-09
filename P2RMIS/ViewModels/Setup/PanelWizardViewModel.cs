using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Setup;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    public class PanelWizardViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PanelWizardViewModel"/> class.
        /// </summary>
        public PanelWizardViewModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PanelWizardViewModel"/> class.
        /// </summary>
        /// <param name="panel">The panel.</param>
        public PanelWizardViewModel(IAddPanelModel panel)
        {
            Panels = panel.Panels.OrderBy(y => y.Value).ToList().ConvertAll(x => new KeyValuePair<string, string>(x.Index, x.Value));
            Programs = panel.Programs.ToList().ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            MeetingSessionId = panel.MeetingSessionId;
            ClientMeetingId = panel.ClientMeetingId;
        }
        /// <summary>
        /// Gets the panels.
        /// </summary>
        /// <value>
        /// The panels.
        /// </value>
        public List<KeyValuePair<string, string>> Panels { get; private set; }
        /// <summary>
        /// Gets the programs.
        /// </summary>
        /// <value>
        /// The programs.
        /// </value>
        public List<KeyValuePair<int, string>> Programs { get; private set; }
        /// <summary>
        /// Gets the program identifier.
        /// </summary>
        /// <value>
        /// The program identifier.
        /// </value>
        public int ProgramId { get; private set; }
        /// <summary>
        /// Gets the panel abbr.
        /// </summary>
        /// <value>
        /// The panel abbr.
        /// </value>
        public string PanelAbbr { get; private set; }
        /// <summary>
        /// Gets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        public string PanelName { get; private set; }
        /// <summary>
        /// Gets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        public int MeetingSessionId { get; private set; }
        /// <summary>
        /// Gets the client meeting identifier.
        /// </summary>
        /// <value>
        /// The client meeting identifier.
        /// </value>
        public int ClientMeetingId { get; private set; }
    }
}