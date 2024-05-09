using System;

namespace Sra.P2rmis.WebModels.HotelAndTravel
{
    /// <summary>
    /// Interface for SessionAttenanceDetails Model plus the dates in text format (as received from view)
    /// </summary>
    public interface ISessionAttendanceDetailsStringDateModel : ISessionAttendanceDetailsModel
    {
        /// <summary>
        /// Text formatted attendance start date
        /// </summary>
        string FormattedAttendanceStartDate { get; }
        /// <summary>
        /// Text formatted attendancee end date
        /// </summary>
        string FormattedAttendanceEndDate { get; }
        /// <summary>
        /// Text formatted hotel check in date
        /// </summary>
        string FormattedHotelCheckInDate { get; }
        /// <summary>
        /// Text formatted hotel check out date
        /// </summary>
        string FormattedHotelCheckOutDate { get; }

        /// <summary>
        /// Populates the hotel registration.
        /// </summary>
        /// <param name="hotelId">Hotel identifier</param>
        /// <param name="hotelName">Name of the hotel.</param>
        /// <param name="checkinDate">The check in date.</param>
        /// <param name="checkoutDate">The checkout date.</param>
        /// <param name="travelModeId">The travel mode identifier.</param>
        /// <param name="hotelRequired">if set to <c>true</c> [hotel required].</param>
        /// <param name="hotelDoubleOccupancy">if set to <c>true</c> [hotel double occupancy].</param>
        void PopulateHotelRegistration(int? hotelId, string hotelName, string checkinDate, string checkoutDate,
            int? travelModeId, bool hotelRequired, bool hotelDoubleOccupancy);
        /// <summary>
        /// Populates the registration.
        /// </summary>
        /// <param name="meetingRegistrationId">The meeting registration identifier.</param>
        /// <param name="attendanceStartDate">The attendance start date.</param>
        /// <param name="attendanceEndDate">The attendance end date.</param>
        /// <param name="submitted">if set to <c>true</c> [submitted].</param>
        void PopulateRegistration(int? meetingRegistrationId, string attendanceStartDate, string attendanceEndDate,
            bool submitted);
        /// <summary>
        /// Converts the string formatted checkin date to date time format
        /// </summary>
        /// <returns>true if converted, false otherwise</returns>
        bool ConvertHotelCheckinDate();
        /// <summary>
        /// Converts the string formatted checkout date to date time format
        /// </summary>
        /// <returns>true if converted, false otherwise</returns>
        bool ConvertHotelCheckoutDate();
        /// <summary>
        /// Converts the string formatted attendance start date to date time format
        /// </summary>
        /// <returns>true if converted, false otherwise</returns>
        bool ConvertAttendanceStartDate();
        /// <summary>
        /// Converts the string formatted attendance end date to date time format
        /// </summary>
        /// <returns>true if converted, false otherwise</returns>
        bool ConvertAttendanceEndDate();
    }
    public interface ISessionAttendanceDetailsModel : IBuiltModel
    {
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
        /// Populates the hotel registration.
        /// </summary>
        /// <param name="hotelId">Hotel identifier</param>
        /// <param name="hotelName">Name of the hotel.</param>
        /// <param name="checkinDate">The check in date.</param>
        /// <param name="checkoutDate">The checkout date.</param>
        /// <param name="travelModeId">The travel mode identifier.</param>
        /// <param name="hotelNotRequired">if set to <c>true</c> hotel is not required.</param>
        /// <param name="hotelDoubleOccupancy">if set to <c>true</c> [hotel double occupancy].</param>
        void PopulateHotelRegistration(int? hotelId, string hotelName, Nullable<DateTime> checkinDate, Nullable<DateTime> checkoutDate,
            int? travelModeId, bool hotelNotRequired, bool hotelDoubleOccupancy);
        /// <summary>
        /// Populates the registration.
        /// </summary>
        /// <param name="meetingRegistrationId">The meeting registration identifier.</param>
        /// <param name="attendanceStartDate">The attendance start date.</param>
        /// <param name="attendanceEndDate">The attendance end date.</param>
        /// <param name="submitted">if set to <c>true</c> [submitted].</param>
        void PopulateRegistration(int? meetingRegistrationId, Nullable<DateTime> attendanceStartDate, Nullable<DateTime> attendanceEndDate,
            bool submitted);
        /// <summary>
        /// Populate the PanelUserAssignment entity identifier that the model represents
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        void PopulatePanelUserAssignment(int? panelUserAssignmentId);
        /// <summary>
        /// Populates the special needs.
        /// </summary>
        /// <param name="hotelAndFoodRequestComments">The hotel and food request comments.</param>
        /// <param name="travelRequestComments">The travel request comments.</param>
        void PopulateSpecialNeeds(string hotelAndFoodRequestComments, string travelRequestComments);
        /// <summary>
        /// Set the hotel name.  
        /// </summary>
        /// <param name="hotelName">hotel name</param>
        /// <param name="hotelId">hotel identifier</param>
        /// <param name="checkinDate">checkin date</param>
        /// <param name="checkoutDate">checkout date</param>
        void PopulateHotel(string hotelName, int? hotelId, DateTime? checkinDate, DateTime? checkoutDate);
        /// <summary>
        /// Validates the travel mode
        /// </summary>
        /// <returns>true if valid, false otherwise</returns>
        bool IsTravelModeValid();
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
        /// Gets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        int? HotelId { get; }
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
        /// Gets the travel mode identifier.
        /// </summary>
        /// <value>
        /// The travel mode identifier.
        /// </value>
        int? TravelModeId { get; }
        /// <summary>
        /// Attendance start date
        /// </summary>
        Nullable<DateTime> AttendanceStartDate { get; }
        /// <summary>
        /// Attendance end date
        /// </summary>
        Nullable<DateTime> AttendanceEndDate { get; }
        /// <summary>
        /// Gets a value indicating whether [hotel required].
        /// </summary>
        /// <value>
        ///   <c>true</c> if hotel is not required; otherwise, <c>false</c>.
        /// </value>
        bool HotelNotRequired { get; }
        /// <summary>
        /// Gets a value indicating whether [hotel double occupancy].
        /// </summary>
        /// <value>
        /// <c>true</c> if [hotel double occupancy]; otherwise, <c>false</c>.
        /// </value>
        bool HotelDoubleOccupancy { get; }
        /// <summary>
        /// MeetingRegistration entity identifier
        /// </summary>
        Nullable<int> MeetingRegistrationId { get; }
        /// <summary>
        /// PanelUserAssignment entity identifier
        /// </summary>
        Nullable<int> PanelUserAssignmentId { get; }
        /// <summary>
        /// Gets the hotel and food request comments.
        /// </summary>
        /// <value>
        /// The hotel and food request comments.
        /// </value>
        string HotelAndFoodRequestComments { get; }
        /// <summary>
        /// Gets the travel request comments.
        /// </summary>
        /// <value>
        /// The travel request comments.
        /// </value>
        string TravelRequestComments { get; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="SessionAttendanceDetailsModel"/> is submitted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if submitted; otherwise, <c>false</c>.
        /// </value>
        bool Submitted { get; }
        #endregion
    }
    /// <summary>
    /// Class for SessionAttenanceDetails Model plus the dates in text format (as received from view)
    /// </summary>
    public class SessionAttendanceDetailsStringDateModel : SessionAttendanceDetailsModel, ISessionAttendanceDetailsStringDateModel
    {
        /// <summary>
        /// Text formatted attendance start date
        /// </summary>
        public string FormattedAttendanceStartDate { get; private set; }
        /// <summary>
        /// Text formatted attendancee end date
        /// </summary>
        public string FormattedAttendanceEndDate { get; private set; }
        /// <summary>
        /// Text formatted hotel check in date
        /// </summary>
        public string FormattedHotelCheckInDate { get; private set; }
        /// <summary>
        /// Text formatted hotel check out date
        /// </summary>
        public string FormattedHotelCheckOutDate { get; private set; }

        /// <summary>
        /// Populates the hotel registration.
        /// </summary>
        /// <param name="hotelId">Hotel identifier</param>
        /// <param name="hotelName">Name of the hotel.</param>
        /// <param name="checkinDate">The check in date.</param>
        /// <param name="checkoutDate">The checkout date.</param>
        /// <param name="travelModeId">The travel mode identifier.</param>
        /// <param name="hotelNotRequired">if set to <c>true</c> hotel is not required.</param>
        /// <param name="hotelDoubleOccupancy">if set to <c>true</c> [hotel double occupancy].</param>
        public void PopulateHotelRegistration(int? hotelId, string hotelName, string checkinDate, string checkoutDate,
            int? travelModeId, bool hotelNotRequired, bool hotelDoubleOccupancy)
        {
            this.HotelId = hotelId;
            this.HotelName = hotelName;
            this.FormattedHotelCheckInDate = checkinDate;
            this.FormattedHotelCheckOutDate = checkoutDate;
            this.TravelModeId = travelModeId;
            this.HotelNotRequired = hotelNotRequired;
            this.HotelDoubleOccupancy = hotelDoubleOccupancy;
        }
        /// <summary>
        /// Populates the registration.
        /// </summary>
        /// <param name="meetingRegistrationId">The meeting registration identifier.</param>
        /// <param name="attendanceStartDate">The attendance start date.</param>
        /// <param name="attendanceEndDate">The attendance end date.</param>
        /// <param name="submitted">if set to <c>true</c> [submitted].</param>
        public void PopulateRegistration(int? meetingRegistrationId, string attendanceStartDate, string attendanceEndDate,
            bool submitted)
        {
            this.MeetingRegistrationId = meetingRegistrationId;
            this.FormattedAttendanceStartDate = attendanceStartDate;
            this.FormattedAttendanceEndDate = attendanceEndDate;
            this.Submitted = submitted;
        }
        /// <summary>
        /// Converts the string formatted checkin date to date time format
        /// </summary>
        /// <returns>true if converted, false otherwise</returns>
        public bool ConvertHotelCheckinDate()
        {
            bool result;
            DateTime date;
            result = DateTime.TryParse(FormattedHotelCheckInDate, out date);
            HotelCheckInDate = (result) ? date as DateTime? : null;

            return result;
        }
        /// <summary>
        /// Converts the string formatted checkout date to date time format
        /// </summary>
        /// <returns>true if converted, false otherwise</returns>
        public bool ConvertHotelCheckoutDate()
        {
            bool result;
            DateTime date;
            result = DateTime.TryParse(FormattedHotelCheckOutDate, out date);
            HotelCheckOutDate = (result) ? date as DateTime? : null;

            return result;
        }
        /// <summary>
        /// Converts the string formatted attendance start date to date time format
        /// </summary>
        /// <returns>true if converted, false otherwise</returns>
        public bool ConvertAttendanceStartDate()
        {
            bool result;
            DateTime date;
            result = DateTime.TryParse(FormattedAttendanceStartDate, out date);
            AttendanceStartDate = (result) ? date as DateTime? : null;

            return result;
        }
        /// <summary>
        /// Converts the string formatted attendance end date to date time format
        /// </summary>
        /// <returns>true if converted, false otherwise</returns>
        public bool ConvertAttendanceEndDate()
        {
            bool result;
            DateTime date;
            result = DateTime.TryParse(FormattedAttendanceEndDate, out date);
            AttendanceEndDate = (result) ? date as DateTime? : null;

            return result;
        }
    }

    public class SessionAttendanceDetailsModel : ISessionAttendanceDetailsModel
    {
        public SessionAttendanceDetailsModel()
        {
            HotelNotRequired = false;
        }
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
        /// Populates the hotel registration.
        /// </summary>
        /// <param name="hotelId">Hotel identifier</param>
        /// <param name="hotelName">Name of the hotel.</param>
        /// <param name="checkinDate">The check in date.</param>
        /// <param name="checkoutDate">The checkout date.</param>
        /// <param name="travelModeId">The travel mode identifier.</param>
        /// <param name="hotelNotRequired">if set to <c>true</c> [hotel required].</param>
        /// <param name="hotelDoubleOccupancy">if set to <c>true</c> [hotel double occupancy].</param>
        public void PopulateHotelRegistration(int? hotelId, string hotelName, Nullable<DateTime> checkinDate, Nullable<DateTime> checkoutDate,
            int? travelModeId, bool hotelNotRequired, bool hotelDoubleOccupancy)
        {
            this.HotelId = hotelId;
            this.HotelName = hotelName;
            this.HotelCheckInDate = checkinDate;
            this.HotelCheckOutDate = checkoutDate;
            this.TravelModeId = travelModeId;
            this.HotelNotRequired = hotelNotRequired;
            this.HotelDoubleOccupancy = hotelDoubleOccupancy;
        }
        /// <summary>
        /// Populates the registration.
        /// </summary>
        /// <param name="meetingRegistrationId">The meeting registration identifier.</param>
        /// <param name="attendanceStartDate">The attendance start date.</param>
        /// <param name="attendanceEndDate">The attendance end date.</param>
        /// <param name="submitted">if set to <c>true</c> [submitted].</param>
        public void PopulateRegistration(int? meetingRegistrationId, Nullable<DateTime> attendanceStartDate, Nullable<DateTime> attendanceEndDate,
            bool submitted)
        {
            this.MeetingRegistrationId = meetingRegistrationId;
            this.AttendanceStartDate = attendanceStartDate;
            this.AttendanceEndDate = attendanceEndDate;
            this.Submitted = submitted;
        }
        /// <summary>
        /// Populate the PanelUserAssignment entity identifier that the model represents
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        public void PopulatePanelUserAssignment(int? panelUserAssignmentId)
        {
            this.PanelUserAssignmentId = panelUserAssignmentId;
        }
        /// <summary>
        /// Populates the special needs.
        /// </summary>
        /// <param name="hotelAndFoodRequestComments">The hotel and food request comments.</param>
        /// <param name="travelRequestComments">The travel request comments.</param>
        public void PopulateSpecialNeeds(string hotelAndFoodRequestComments, string travelRequestComments)
        {
            this.HotelAndFoodRequestComments = hotelAndFoodRequestComments;
            this.TravelRequestComments = travelRequestComments;
        }
        /// <summary>
        /// Set the hotel name.  
        /// </summary>
        /// <remarks>
        /// This method is intended to be invoked when the reviewer has not started their registration.
        /// </remarks>
        /// <param name="hotelName">Hotel name</param>
        /// <param name="hotelId">Hotel identifier</param>
        /// <param name="checkinDate">checkin Date</param>
        /// <param name="checkoutDate">checkoutDate</param>
        public void PopulateHotel(string hotelName, int? hotelId, DateTime? checkinDate, DateTime? checkoutDate)
        {
            this.HotelName = hotelName;
            this.HotelId = hotelId;
            this.HotelCheckInDate = checkinDate;
            this.HotelCheckOutDate = checkoutDate;
        }
        /// <summary>
        /// Validates travel mode
        /// </summary>
        /// <returns>true if travel mode is valid, false otherwise</returns>
        public bool IsTravelModeValid()
        {
            return (!HotelNotRequired || TravelModeId.HasValue);
        }
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
        /// Gets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int? HotelId { get; protected set; }
        /// <summary>
        /// Hotel name
        /// </summary>
        public string HotelName { get; protected set; }
        /// <summary>
        /// Hotel check in date
        /// </summary>
        public Nullable<DateTime> HotelCheckInDate { get; protected set; }
        /// <summary>
        /// Hotel check out date
        /// </summary>
        public Nullable<DateTime> HotelCheckOutDate { get; protected set; }
        /// <summary>
        /// Gets the travel mode identifier.
        /// </summary>
        /// <value>
        /// The travel mode identifier.
        /// </value>
        public int? TravelModeId { get; protected set; }
        /// <summary>
        /// Attendance start date
        /// </summary>
        public Nullable<DateTime> AttendanceStartDate { get; protected set; }
        /// <summary>
        /// Attendance end date
        /// </summary>
        public Nullable<DateTime> AttendanceEndDate { get; protected set; }
        /// <summary>
        /// Gets a value indicating whether hotel is not required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if hotel is not required; otherwise, <c>false</c>.
        /// </value>
        public bool HotelNotRequired { get; protected set; }
        /// <summary>
        /// Gets a value indicating whether [hotel double occupancy].
        /// </summary>
        /// <value>
        /// <c>true</c> if [hotel double occupancy]; otherwise, <c>false</c>.
        /// </value>
        public bool HotelDoubleOccupancy { get; protected set; }
        /// <summary>
        /// <summary>
        /// MeetingRegistration entity identifier
        /// </summary>
        public Nullable<int> MeetingRegistrationId { get; protected set; }
        /// <summary>
        /// PanelUserAssignment entity identifier
        /// </summary>
        public Nullable<int> PanelUserAssignmentId { get; private set; }
        /// <summary>
        /// Gets the hotel and food request comments.
        /// </summary>
        /// <value>
        /// The hotel and food request comments.
        /// </value>
        public string HotelAndFoodRequestComments { get; private set; }
        /// <summary>
        /// Gets the travel request comments.
        /// </summary>
        /// <value>
        /// The travel request comments.
        /// </value>
        public string TravelRequestComments { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="SessionAttendanceDetailsModel"/> is submitted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if submitted; otherwise, <c>false</c>.
        /// </value>
        public bool Submitted { get; protected set; }
        #endregion
    }
}
