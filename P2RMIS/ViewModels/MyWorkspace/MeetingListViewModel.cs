using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.HotelAndTravel;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;

namespace Sra.P2rmis.Web.UI.Models
{
    public class MeetingListViewModel
    {
        public const string TBD = "TBD";

        public MeetingListViewModel(IMeetingListModel meetingList)
        {
            Year = meetingList.Year;
            ProgramAbbreviation = meetingList.ProgramAbbreviation;
            PanelAbbreviation = meetingList.PanelAbbreviation;
            StartDate = meetingList.StartDate;
            EndDate = meetingList.EndDate;
            HotelName = !string.IsNullOrEmpty(meetingList.HotelName) ? meetingList.HotelName : TBD;
            HotelCheckInDate = meetingList.HotelCheckInDate;
            HotelCheckOutDate = meetingList.HotelCheckOutDate;
            AttendanceStartDate = meetingList.AttendanceStartDate;
            AttendanceEndDate = meetingList.AttendanceEndDate;
            CanStart = meetingList.Start;
            CanEdit = meetingList.Edit;
            CanView = meetingList.View;
            PanelUserAssignmentId = meetingList.PanelUserAssignmentId;
            MeetingRegistrationId = meetingList.MeetingRegistrationId;
            MeetingRegistrationTravelId = meetingList.MeetingRegistrationTravelId;
            MeetingRegistrationHotelId = meetingList.MeetingRegistrationHotelId;
            MeetingRegistrationAttendanceId = meetingList.MeetingRegistrationAttendanceId;
            Panel = String.Format("{0} {1} {2} - {3}-{4}", meetingList.Year, meetingList.ProgramAbbreviation, meetingList.PanelAbbreviation,
                ViewHelpers.FormatDate(meetingList.StartDate), ViewHelpers.FormatDate(meetingList.EndDate));
            HotelStay = String.Format("{0}-{1}", ViewHelpers.FormatDate(HotelCheckInDate), ViewHelpers.FormatDate(HotelCheckOutDate));
            MeetingAttendance = String.Format("{0}-{1}", ViewHelpers.FormatDate(AttendanceStartDate), ViewHelpers.FormatDate(AttendanceEndDate));
            Action = meetingList.Start ? Invariables.Labels.Start : (meetingList.Edit ? Invariables.Labels.Edit : (meetingList.View ? Invariables.Labels.View : String.Empty));
        }

        #region Attributes        
        /// <summary>
        /// Gets the panel.
        /// </summary>
        /// <value>
        /// The panel.
        /// </value>
        [JsonProperty("panel")]
        public string Panel { get; private set; }
        /// <summary>
        /// Gets the name of the hotel.
        /// </summary>
        /// <value>
        /// The name of the hotel.
        /// </value>
        [JsonProperty("hotelName")]
        public string HotelName { get; private set; }
        /// <summary>
        /// Gets the hotel stay.
        /// </summary>
        /// <value>
        /// The hotel stay.
        /// </value>
        [JsonProperty("hotelStay")]
        public string HotelStay { get; private set; }
        /// <summary>
        /// Gets the meeting attendance.
        /// </summary>
        /// <value>
        /// The meeting attendance.
        /// </value>
        [JsonProperty("meetingAttendance")]
        public string MeetingAttendance { get; private set; }
        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [JsonProperty("action")]
        public string Action { get; private set; }
        /// <summary>
        /// Program fiscal year
        /// </summary>
        public string Year { get; private set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; private set; }
        /// <summary>
        /// Panel Abbreviation
        /// </summary>
        public string PanelAbbreviation { get; private set; }
        /// <summary>
        /// SessionPanel start date
        /// </summary>
        public Nullable<DateTime> StartDate { get; private set; }
        /// <summary>
        /// SessionPanel end date
        /// </summary>
        public Nullable<DateTime> EndDate { get; private set; }
        /// <summary>
        /// Hotel check in date
        /// </summary>
        public Nullable<DateTime> HotelCheckInDate { get; private set; }
        /// <summary>
        /// Hotel check out date
        /// </summary>
        public Nullable<DateTime> HotelCheckOutDate { get; private set; }
        /// <summary>
        /// Attendance start date
        /// </summary>
        public Nullable<DateTime> AttendanceStartDate { get; private set; }
        /// <summary>
        /// Attendance end date
        /// </summary>
        public Nullable<DateTime> AttendanceEndDate { get; private set; }
        /// <summary>
        /// Indicates the reviewer can start the registration.
        /// </summary>
        public bool CanStart { get; set; }
        /// <summary>
        /// Indicates the reviewer can edit the registration.
        /// </summary>
        public bool CanEdit { get; set; }
        /// <summary>
        /// Indicates the reviewer can view the registration.
        /// </summary>
        public bool CanView { get; set; }
        /// <summary>
        /// MeetingRegistration entity identifier
        /// </summary>
        public Nullable<int> MeetingRegistrationId { get; private set; }
        /// <summary>
        /// MeetingRegistrationTravel entity identifier
        /// </summary>
        public Nullable<int> MeetingRegistrationTravelId { get; private set; }
        /// <summary>
        /// MeetingRegistrationHotel entity identifier
        /// </summary>
        public Nullable<int> MeetingRegistrationHotelId { get; private set; }
        /// <summary>
        /// MeetingRegistrationAttendance entity identifier
        /// </summary>
        public Nullable<int> MeetingRegistrationAttendanceId { get; private set; }
        /// <summary>
        /// PanelUserAssignment entity identifier
        /// </summary>
        [JsonProperty("panelUserAssignmentId")]
        public Nullable<int> PanelUserAssignmentId { get; private set; }
        #endregion
    }
}