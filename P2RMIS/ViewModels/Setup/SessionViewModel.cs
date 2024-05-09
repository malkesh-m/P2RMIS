using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Setup;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    public class SessionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionViewModel"/> class.
        /// </summary>
        public SessionViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionViewModel"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public SessionViewModel(ISessionSetupModel session)
        {
            SessionAbbr = session.SessionAbbreviation;
            SessionName = session.SessionDescription;
            StartDate = ViewHelpers.FormatDateTime2(session.StartDate);
            EndDate = ViewHelpers.FormatDateTime2(session.EndDate);
            Panels = session.SessionPanelList.ToList().ConvertAll(x => new KeyValuePair<int, string>(
                x.SessionPanelId, String.Format("{0} {1}", x.ProgramAbbreviation, x.PanelAbbreviation)));
            Active = session.EndDate > GlobalProperties.P2rmisDateTimeNow;
            ClientId = session.ClientId;
            ClientMeetingId = session.ClientMeetingId;
            MeetingSessionId = session.MeetingSessionId;
            HasProgramMeetings = session.HasProgramMeetings;
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        [JsonProperty("index")]
        public int Index { get; set; }

        /// <summary>
        /// Gets the session abbr.
        /// </summary>
        /// <value>
        /// The session abbr.
        /// </value>
        [JsonProperty("sessionAbbr")]
        public string SessionAbbr { get; private set; }

        /// <summary>
        /// Gets the name of the session.
        /// </summary>
        /// <value>
        /// The name of the session.
        /// </value>
        [JsonProperty("sessionName")]
        public string SessionName { get; private set; }

        /// <summary>
        /// Gets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        [JsonProperty("startDate")]
        public string StartDate { get; private set; }

        /// <summary>
        /// Gets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        [JsonProperty("endDate")]
        public string EndDate { get; private set; }

        /// <summary>
        /// Gets the panels.
        /// </summary>
        /// <value>
        /// The panels.
        /// </value>
        [JsonProperty("panels")]
        public List<KeyValuePair<int, string>> Panels { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SessionViewModel"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("active")]
        public bool Active { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SessionViewModel"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("hasProgramMeetings")]
        public bool HasProgramMeetings { get; private set; }

        /// <summary>
        /// Gets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        [JsonProperty("clientId")]
        public int ClientId { get; private set; }

        /// <summary>
        /// Gets the client meeting identifier.
        /// </summary>
        /// <value>
        /// The client meeting identifier.
        /// </value>
        [JsonProperty("clientMeetingId")]
        public int ClientMeetingId { get; private set; }

        /// <summary>
        /// Gets the meeting session identifier.
        /// </summary>
        /// <value>
        /// The meeting session identifier.
        /// </value>
        [JsonProperty("meetingSessionId")]
        public int MeetingSessionId { get; private set; }
    }
}