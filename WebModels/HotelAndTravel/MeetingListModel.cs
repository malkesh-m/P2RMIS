using System;

namespace Sra.P2rmis.WebModels.HotelAndTravel
{
    /// <summary>
    /// Represents an entry in the Hotel & Travel Meeting List grid.
    /// </summary>
    public interface IMeetingListModel : IBuiltModel
    {
        #region Constructor & Setup
        /// <summary>
        /// Populate the model with information about the session.
        /// </summary>
        /// <param name="year">Fiscal year</param>
        /// <param name="programAbbreviaton">Program abbreviation</param>
        /// <param name="panelAbbreviation">Panel abbreviation</param>
        /// <param name="startDate">SessionPanel start date</param>
        /// <param name="endDate">SessionPanel end date</param>
        void PopulateSessionInformation(string year, string programAbbreviaton, string panelAbbreviation, Nullable<DateTime> startDate, Nullable<DateTime> endDate);
        /// <summary>
        /// Populate the model with information about the hotel registration
        /// </summary>
        /// <param name="hotelName">Hotel name</param>
        /// <param name="checkinDate">Date reviewer will check into the hotel</param>
        /// <param name="checkoutDate">Date reviewer will check out of the hotel</param>
        void PopulateHotelRegistration(string hotelName, Nullable<DateTime> checkinDate, Nullable<DateTime> checkoutDate);
        /// <summary>
        /// Populate the model with information about the registration
        /// </summary>
        /// <param name="attendanceStartDate">Start date reviewer will attend the meeting</param>
        /// <param name="attendanceEndDate">End date reviewer will attend the meeting</param>
        void PopulateRegistration(Nullable<DateTime> attendanceStartDate, Nullable<DateTime> attendanceEndDate);
        /// <summary>
        /// Sets the action indicators.
        /// </summary>
        /// <param name="edit">Edit action indicator</param>
        /// <param name="view">View action indicator</param>
        void PopulateActions(bool edit, bool view);
        /// <summary>
        /// Sets the start action indicator.
        /// </summary>
        void PopulateStartAction();
        /// <summary>
        /// Set the hotel name.  
        /// </summary>
        /// <param name="hotelName"></param>
        void PopulateHotel(string hotelName);
        /// <summary>
        /// Sets the attendance dates
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        void PopulateAttendanceDates(DateTime? startDate, DateTime? endDate);
        /// <summary>
        /// Populate the PanelUserAssignment entity identifier that the model represents
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        void PopulatePanelUserAssignment(int panelUserAssignmentId);
        /// <summary>
        /// Populate the meeting registration entities identifiers associated with the 
        /// PanelUserAssignment entity represented by the model.
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="meetingRegistrationId">MeetingRegistration entity identifier</param>
        /// <param name="travelId">MeetingRegistrationTravel entity identifier</param>
        /// <param name="hotelId">MeetingRegistrationHotel entity identifier</param>
        /// <param name="attendanceId">MeetingRegistrationAttendance entity identifier</param>
        void PopulateMeetingRegistrationEntityIdentifiers(int panelUserAssignmentId, int meetingRegistrationId, int? travelId, int? hotelId, int? attendanceId);
        #endregion
        #region Attributes
        /// <summary>
        /// Program fiscal year
        /// </summary>
        string Year { get; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        string ProgramAbbreviation { get; }
        /// <summary>
        /// Panel Abbreviation
        /// </summary>
        string PanelAbbreviation { get; }
        /// <summary>
        /// SessionPanel start date
        /// </summary>
        Nullable<DateTime> StartDate { get; }
        /// <summary>
        /// SessionPanel end date
        /// </summary>
        Nullable<DateTime> EndDate { get; }
        /// <summary>
        /// Hotel name
        /// </summary>
        string HotelName { get; }
        /// <summary>
        /// Hotel check in date
        /// </summary>
        Nullable<DateTime> HotelCheckInDate { get; }
        /// <summary>
        /// Hotel check out date
        /// </summary>
        Nullable<DateTime> HotelCheckOutDate { get; }
        /// <summary>
        /// Attendance start date
        /// </summary>
        Nullable<DateTime> AttendanceStartDate { get; }
        /// <summary>
        /// Attendance end date
        /// </summary>
        Nullable<DateTime> AttendanceEndDate { get; }
        /// <summary>
        /// Indicates the reviewer can start the registration.
        /// </summary>
        bool Start { get; set; }
        /// <summary>
        /// Indicates the reviewer can edit the registration.
        /// </summary>
        bool Edit { get; set; }
        /// <summary>
        /// Indicates the reviewer can view the registration.
        /// </summary>
        bool View { get; set; }
        /// <summary>
        /// MeetingRegistration entity identifier
        /// </summary>
        Nullable<int> MeetingRegistrationId { get; }
        /// <summary>
        /// MeetingRegistrationTravel entity identifier
        /// </summary>
        Nullable<int> MeetingRegistrationTravelId { get; }
        /// <summary>
        /// MeetingRegistrationHotel entity identifier
        /// </summary>
        Nullable<int> MeetingRegistrationHotelId { get; }
        /// <summary>
        /// MeetingRegistrationAttendance entity identifier
        /// </summary>
        Nullable<int> MeetingRegistrationAttendanceId { get; }
        /// <summary>
        /// PanelUserAssignment entity identifier
        /// </summary>
        Nullable<int> PanelUserAssignmentId { get; }
        #endregion
    }
    /// <summary>
    /// Represents an entry in the Hotel & Travel Meeting List grid.
    /// </summary>
    public class MeetingListModel : IMeetingListModel
    {
        #region Constructor & Setup
        /// <summary>
        /// Parameter-less constructor
        /// </summary>
        public MeetingListModel() { }
        /// <summary>
        /// Populate the model with information about the session.
        /// </summary>
        /// <param name="year">Fiscal year</param>
        /// <param name="programAbbreviaton">Program abbreviation</param>
        /// <param name="panelAbbreviation">Panel abbreviation</param>
        /// <param name="startDate">SessionPanel start date</param>
        /// <param name="endDate">SessionPanel end date</param>
        public void PopulateSessionInformation(string year, string programAbbreviaton, string panelAbbreviation, Nullable<DateTime> startDate, Nullable<DateTime> endDate)
        {
            this.Year = year;
            this.ProgramAbbreviation = programAbbreviaton;
            this.PanelAbbreviation = panelAbbreviation;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
        /// <summary>
        /// Populate the model with information about the hotel registration
        /// </summary>
        /// <param name="hotelName">Hotel name</param>
        /// <param name="checkinDate">Date reviewer will check into the hotel</param>
        /// <param name="checkoutDate">Date reviewer will check out of the hotel</param>
        public void PopulateHotelRegistration(string hotelName, Nullable<DateTime> checkinDate, Nullable<DateTime> checkoutDate)
        {
            this.HotelName = hotelName;
            this.HotelCheckInDate = checkinDate;
            this.HotelCheckOutDate = checkoutDate;
        }
        /// <summary>
        /// Populate the model with information about the registration
        /// </summary>
        /// <param name="attendanceStartDate">Start date reviewer will attend the meeting</param>
        /// <param name="attendanceEndDate">End date reviewer will attend the meeting</param>
        public void PopulateRegistration(Nullable<DateTime> attendanceStartDate, Nullable<DateTime> attendanceEndDate)
        {
            this.AttendanceStartDate = attendanceStartDate;
            this.AttendanceEndDate = attendanceEndDate;
        }
        /// <summary>
        /// Sets the action indicators.
        /// </summary>
        /// <param name="edit">Edit action indicator</param>
        /// <param name="view">View action indicator</param>
        public void PopulateActions(bool edit, bool view)
        {
            this.Edit = edit;
            this.View = view;
        }
        /// <summary>
        /// Sets the start action indicator.
        /// </summary>
        public void PopulateStartAction()
        {
            this.Start = true;
        }
        /// <summary>
        /// Set the hotel name.  
        /// </summary>
        /// <remarks>
        /// This method is intended to be invoked when the reviewer has not started their registration.
        /// </remarks>
        /// <param name="hotelName"></param>
        public void PopulateHotel(string hotelName)
        {
            this.HotelName = hotelName;
        }
        /// <summary>
        /// Sets the attendance dates
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        public void PopulateAttendanceDates(DateTime? startDate, DateTime? endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
        /// <summary>
        /// Populate the PanelUserAssignment entity identifier that the model represents
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        public void PopulatePanelUserAssignment(int panelUserAssignmentId)
        {
            this.PanelUserAssignmentId = panelUserAssignmentId;
        }
        /// <summary>
        /// Populate the meeting registration entities identifiers associated with the 
        /// PanelUserAssignment entity represented by the model.
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="meetingRegistrationId">MeetingRegistration entity identifier</param>
        /// <param name="travelId">MeetingRegistrationTravel entity identifier</param>
        /// <param name="hotelId">MeetingRegistrationHotel entity identifier</param>
        /// <param name="attendanceId">MeetingRegistrationAttendance entity identifier</param>
        public void PopulateMeetingRegistrationEntityIdentifiers(int panelUserAssignmentId, int meetingRegistrationId, int? travelId, int? hotelId, int? attendanceId)
        {
            PopulatePanelUserAssignment(panelUserAssignmentId);
            this.MeetingRegistrationId = meetingRegistrationId;
            this.MeetingRegistrationTravelId = travelId;
            this.MeetingRegistrationHotelId = hotelId;
            this.MeetingRegistrationAttendanceId = attendanceId;
        }
        #endregion
        #region Attributes
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
        /// Hotel name
        /// </summary>
        public string HotelName { get; private set; }
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
        public bool Start { get; set; }
        /// <summary>
        /// Indicates the reviewer can edit the registration.
        /// </summary>
        public bool Edit { get; set; }
        /// <summary>
        /// Indicates the reviewer can view the registration.
        /// </summary>
        public bool View { get; set; }
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
        public Nullable<int> PanelUserAssignmentId { get; private set; }
        #endregion
    }
}
