using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    public class MeetingViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingViewModel"/> class.
        /// </summary>
        public MeetingViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingViewModel"/> class.
        /// </summary>
        /// <param name="meeting">The meeting.</param>
        public MeetingViewModel(IMeetingSetupModel meeting) {
            ClientId = meeting.ClientId;
            ClientMeetingId = meeting.ClientMeetingId;
            MeetingDesc = meeting.MeetingDescription;
            MeetingAbbr = meeting.MeetingAbbreviation;
            MeetingName = String.Format("{0} - {1}", meeting.MeetingAbbreviation, meeting.MeetingDescription);
            StartDate = ViewHelpers.FormatDate(meeting.StartDate);
            EndDate = ViewHelpers.FormatDate(meeting.EndDate);
            MeetingType = meeting.MeetingTypeName;
            ProgramCount = meeting.ProgramCount;
            SessionCount = meeting.SessionCount;
            PanelCount = meeting.PanelCount;
            Hotel = meeting.HotelName;
            Active = meeting.EndDate > GlobalProperties.P2rmisDateTimeNow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingViewModel"/> class.
        /// </summary>
        /// <param name="meeting">The meeting.</param>
        public MeetingViewModel(ISessionSetupMeetingListEntryModel meeting)
        {
            ClientId = meeting.ClientId;
            ClientMeetingId = meeting.ClientMeetingId;
            MeetingDesc = meeting.MeetingDescription;
            MeetingAbbr = meeting.MeetingAbbreviation;
            MeetingName = String.Format("{0} - {1}", meeting.MeetingAbbreviation, meeting.MeetingDescription);
            StartDate = ViewHelpers.FormatDate(meeting.StartDate);
            EndDate = ViewHelpers.FormatDate(meeting.EndDate);
            MeetingType = meeting.MeetingTypeName;
            Active = meeting.EndDate > GlobalProperties.P2rmisDateTimeNow;
            ActiveProgram = meeting.ActiveProgram;
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
        /// Gets the meeting abbr.
        /// </summary>
        /// <value>
        /// The meeting abbr.
        /// </value>
        [JsonProperty("meetingAbbr")]
        public string MeetingAbbr { get; private set; }

        /// <summary>
        /// Gets the meeting desc.
        /// </summary>
        /// <value>
        /// The meeting desc.
        /// </value>
        [JsonProperty("meetingDesc")]
        public string MeetingDesc { get; private set; }

        /// <summary>
        /// Gets the name of the meeting.
        /// </summary>
        /// <value>
        /// The name of the meeting.
        /// </value>
        [JsonProperty("meetingName")]
        public string MeetingName { get; private set; }

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
        /// Gets the type of the meeting.
        /// </summary>
        /// <value>
        /// The type of the meeting.
        /// </value>
        [JsonProperty("meetingType")]
        public string MeetingType { get; private set; }

        /// <summary>
        /// Gets the program count.
        /// </summary>
        /// <value>
        /// The program count.
        /// </value>
        [JsonProperty("programCount")]
        public int ProgramCount { get; private set; }

        /// <summary>
        /// Gets the session count.
        /// </summary>
        /// <value>
        /// The session count.
        /// </value>
        [JsonProperty("sessionCount")]
        public int SessionCount { get; private set; }

        /// <summary>
        /// Gets the panel count.
        /// </summary>
        /// <value>
        /// The panel count.
        /// </value>
        [JsonProperty("panelCount")]
        public int PanelCount { get; private set; }

        /// <summary>
        /// Gets the hotel.
        /// </summary>
        /// <value>
        /// The hotel.
        /// </value>
        [JsonProperty("hotel")]
        public string Hotel { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance has panel passed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has panel passed; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("active")]
        public bool Active { get; private set; }

        /// <summary>
        /// Gets a value indicatating whether the meeting is associated with an active program
        /// </summary>
        [JsonProperty("activeProgram")]
        public bool ActiveProgram { get; private set; }
    }
}