using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Web.UI.Models
{
    public class UpdatePanelViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePanelViewModel"/> class.
        /// </summary>
        public UpdatePanelViewModel()
        {
            Sessions = new List<KeyValuePair<int, string>>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePanelViewModel"/> class.
        /// </summary>
        /// <param name="panel">The panel.</param>
        public UpdatePanelViewModel(IUpdatePanelModel panel) : this()
        {
            PanelAbbr = panel.PanelAbbreviation;
            PanelName = panel.PanelName;
            Sessions = panel.Sessions.OrderBy(y => y.Value).ToList().ConvertAll(x => new KeyValuePair<int, string>(x.Index, x.Value));
            AreApplicationsReleased = panel.AreApplicationsReleased;
            AreApplicationsAssigned = panel.AreApplicationsAssigned;
            AreUsersAssigned = panel.AreUsersAssigned;
            MeetingSessionId = panel.MeetingSessionId;
            ProgramYear = string.Format("{0} - {1}", panel.Year, panel.ProgramAbbr);
            SessionPanelId = panel.SessionPanelId;
        }
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
        /// Gets a value indicating whether [are applications released].
        /// </summary>
        /// <value>
        /// <c>true</c> if [are applications released]; otherwise, <c>false</c>.
        /// </value>
        public bool AreApplicationsReleased { get; private set; }
        /// <summary>
        /// Whether applications are assigned.
        /// </summary>
        public bool AreApplicationsAssigned { get; private set; }
        /// <summary>
        /// Whether users are assigned.
        /// </summary>
        public bool AreUsersAssigned { get; private set; }
        /// <summary>
        /// Program year combination.
        /// </summary>
        public string ProgramYear { get; private set; }
        /// <summary>
        /// Gets the sessions.
        /// </summary>
        /// <value>
        /// The sessions.
        /// </value>
        public List<KeyValuePair<int, string>> Sessions { get; private set; }
        /// <summary>
        /// </summary>
        public int MeetingSessionId { get; private set; }
        /// <summary>
        /// </summary>
        public int SessionPanelId { get; private set; }
    }
}