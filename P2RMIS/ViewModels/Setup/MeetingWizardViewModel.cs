using System;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for Add/Edit Meeting Wizard
    /// </summary>
    public class MeetingWizardViewModel
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        public MeetingWizardViewModel()
        {
            this.MeetingTypeList = new List<KeyValuePair<int, string>>();
            this.HotelList = new List<KeyValuePair<int, string>>();
        }
        /// <summary>
        /// Configure the lists
        /// </summary>
        /// <param name="theMeetingTypeList">List of MeetingTypes & their indexes</param>
        /// <param name="theHotelList">List of Hotels & their indexes</param>
        internal void ConfigureLists(List<KeyValuePair<int, string>> theMeetingTypeList, List<KeyValuePair<int, string>> theHotelList)
        {
            this.MeetingTypeList = theMeetingTypeList;
            this.HotelList = theHotelList;
        }
        /// <summary>
        /// Initializes the client information 
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="client">Client abbreviation</param>
        internal void ConfigureClient(int clientId, string client)
        {
            this.ClientId = clientId;
            this.Client = client;
        }
        /// <summary>
        /// Initializes the ClientMeeting information 
        /// </summary>
        /// <param name="model">Model returned from the service layer</param>
        internal void ConfigureMeeting(IMeetingSetupModalModel model)
        {
            this.ClientMeetingId = model.ClientMeetingId;
            this.MeetingName = model.MeetingDescription;
            this.MtgAbbr = model.MeetingAbbreviation;
            this.Start = model.StartDate;
            this.End = model.EndDate;
            this.MeetingLocation = model.MeetingLocation;
            this.HotelId = model.HotelId;
            this.MeetingTypeId = model.MeetingTypeId;
            this.HasSessions = model.HasSessions;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Identifies the ClientMeeting being edited.
        /// </summary>
        public Nullable<int> ClientMeetingId { get; set; }
        /// <summary>
        /// Identifies the client that has the meeting
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// Client abbreviation
        /// </summary>
        public string Client { get; set; } = string.Empty;
        /// <summary>
        /// Meeting abbreviation
        /// </summary>
        public string MtgAbbr { get; set; } = string.Empty;
        /// <summary>
        /// Meeting name
        /// </summary>
        public string MeetingName { get; set; } = string.Empty;
        /// <summary>
        /// Meeting start date
        /// </summary>
        public Nullable<DateTime> Start { get; set; }
        /// <summary>
        /// Meeting end date
        /// </summary>
        public Nullable<DateTime> End { get; set; }
        /// <summary>
        /// Meeting location
        /// </summary>
        public string MeetingLocation { get; set; }
        /// <summary>
        /// Selected meeting type
        /// </summary>
        public Nullable<int> MeetingTypeId { get; set; }
        /// <summary>
        /// Selected hotel
        /// </summary>
        public Nullable<int> HotelId { get; set; }
        /// <summary>
        /// Whether the meeting contains sessions
        /// </summary>
        public bool HasSessions { get; set; }
        /// <summary>
        /// Meeting type list
        /// </summary>
        //[JsonProperty("awardTypes")]
        public List<KeyValuePair<int, string>> MeetingTypeList { get; set; }
        /// <summary>
        /// Hotel list
        /// </summary>
        public List<KeyValuePair<int, string>> HotelList { get; set; }
        #endregion
    }
}