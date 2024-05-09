using System;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's MeetingRegistration object. 
    /// </summary>

    public partial class MeetingRegistration : IStandardDateFields
    {
        /// <summary>
        /// Retrieves the attendance start date for the registration.
        /// </summary>
        /// <remarks>
        /// The relationship between MeetingRegistration & MeetingRegistration is a 1:1 relationship
        /// even though it is shown as 1:Many.  The 1:Many relationship is required because of 
        /// logical deletion.
        /// </remarks>
        /// <returns>AttenendanceStartDate if one exists; null otherwise</returns>
        public Nullable<DateTime> AttenendanceStartDate()
        {
            return this.MeetingRegistrationAttendances.FirstOrDefault()?.AttendanceStartDate;
        }
        /// <summary>
        /// Retrieves the attendance end date for the registration.
        /// </summary>
        /// <remarks>
        /// The relationship between MeetingRegistration & MeetingRegistration is a 1:1 relationship
        /// even though it is shown as 1:Many.  The 1:Many relationship is required because of 
        /// logical deletion.
        /// </remarks>
        /// <returns>AttenendanceEndDate if one exists; null otherwise</returns>
        public Nullable<DateTime> AttenendanceEndDate()
        {
            return this.MeetingRegistrationAttendances.FirstOrDefault()?.AttendanceEndDate;
        }
        /// <summary>
        /// Retrieves the reviewer's hotel check in date for the registration.
        /// </summary>
        /// <remarks>
        /// The relationship between MeetingRegistration & MeetingRegistration is a 1:1 relationship
        /// even though it is shown as 1:Many.  The 1:Many relationship is required because of 
        /// logical deletion.
        /// </remarks>
        /// <returns>HotelCheckInDate if one exists; null otherwise</returns>
        public Nullable<DateTime> HotelCheckInDate()
        {
            return this.MeetingRegistrationHotels.FirstOrDefault(x => !x.CancellationFlag)?.HotelCheckInDate;
        }
        /// <summary>
        /// Retrieves the reviewer's hotel check out date for the registration.
        /// </summary>
        /// <remarks>
        /// The relationship between MeetingRegistration & MeetingRegistrationHotel is a 1:1 relationship
        /// even though it is shown as 1:Many.  The 1:Many relationship is required because of 
        /// logical deletion.
        /// </remarks>
        /// <returns>HotelCheckOutDate if one exists; null otherwise</returns>
        public Nullable<DateTime> HotelCheckOutDate()
        {
            return this.MeetingRegistrationHotels.FirstOrDefault(x => !x.CancellationFlag)?.HotelCheckOutDate;
        }
        /// <summary>
        /// Determines if the reviewer can edit their hotel & travel information.
        /// </summary>
        /// <returns>True if the reviewer can edit their registration (RegistrationSubmittedFlag == false); false otherwise</returns>
        public bool CanEdit()
        {
            return this.RegistrationSubmittedFlag == false;
        }
        /// <summary>
        /// Determines if the reviewer can view their hotel & travel information.
        /// </summary>
        /// <returns>True if the reviewer can view their registration (RegistrationSubmittedFlag == true); false otherwise</returns>
        public bool CanView()
        {
            return this.RegistrationSubmittedFlag == true;
        }
        /// <summary>
        /// Hotels the identifier.
        /// </summary>
        /// <returns></returns>
        public int? HotelId()
        {
            int? HotelId = this.MeetingRegistrationHotels.FirstOrDefault(x => !x.CancellationFlag)?.HotelId;
            return HotelId;
        }
        /// <summary>
        /// Retrieves the hotel name for the registration.
        /// </summary>
        /// <returns>Hotel name if one exists; empty string otherwise</returns>
        public string HotelName()
        {
            string hotelName = this.MeetingRegistrationHotels.FirstOrDefault(x => !x.CancellationFlag)?.Hotel?.HotelName;
            return (hotelName == null) ? string.Empty : hotelName;
        }
        /// <summary>
        /// Whether hotel is NOT required.
        /// </summary>
        /// <returns></returns>
        public bool HotelNotRequired()
        {
            bool? hotelNotRequired = this.MeetingRegistrationHotels.FirstOrDefault(x => !x.CancellationFlag)?.HotelNotRequiredFlag;
            return (hotelNotRequired == null) ? false : (bool)hotelNotRequired;
        }
        /// <summary>
        /// Hotels the double occupancy.
        /// </summary>
        /// <returns></returns>
        public bool HotelDoubleOccupancy()
        {
            bool? hotelDoubleOccupancy = this.MeetingRegistrationHotels.FirstOrDefault(x => !x.CancellationFlag)?.HotelDoubleOccupancy;
            return (hotelDoubleOccupancy == null) ? false : (bool)hotelDoubleOccupancy;
        }
        /// <summary>
        /// Travels the mode identifier.
        /// </summary>
        /// <returns></returns>
        public int? TravelModeId()
        {
            return this.MeetingRegistrationTravels.FirstOrDefault(x => !x.CancellationFlag)?.TravelModeId;
        }
        /// <summary>
        /// Hotels the and food request comments.
        /// </summary>
        /// <returns></returns>
        public string HotelAndFoodRequestComments()
        {
            return this.MeetingRegistrationHotels.FirstOrDefault(x => !x.CancellationFlag)?.HotelAndFoodRequestComments;
        }
        /// <summary>
        /// Travels the request comments.
        /// </summary>
        /// <returns></returns>
        public string TravelRequestComments()
        {
            return this.MeetingRegistrationTravels.FirstOrDefault(x => !x.CancellationFlag)?.TravelRequestComments;
        }
        /// <summary>
        /// Retrieves the MeetingRegistrationHotel entity identifier.
        /// </summary>
        /// <returns>MeetingRegistrationHotel entity identifier; null if none exists</returns>
        public int? MeetingRegistrationHotelId()
        {
            return this.MeetingRegistrationHotels.FirstOrDefault(x => !x.CancellationFlag)?.MeetingRegistrationHotelId;
        }
        /// <summary>
        /// Retrieves the MeetingRegistrationTravel entity identifier.
        /// </summary>
        /// <returns>MeetingRegistrationTravel entity identifier; null if none exists</returns>
        public int? MeetingRegistrationTravelId()
        {
            return this.MeetingRegistrationTravels.FirstOrDefault(x => !x.CancellationFlag)?.MeetingRegistrationTravelId;
        }
        /// <summary>
        /// Retrieves the MeetingRegistrationAttendance entity identifier.
        /// </summary>
        /// <returns>MeetingRegistrationAttendance entity identifier; null if none exists</returns>
        public int? MeetingRegistrationAttendanceId()
        {
            return this.MeetingRegistrationAttendances.FirstOrDefault()?.MeetingRegistrationAttendanceId;
        }
        /// <summary>
        /// Adds a MeetingRegistrationTravel entity if one does not exist.
        /// </summary>
        /// <param name="entity">MeetingRegistrationTravel entity</param>
        public void AddMeetingRegistrationTravel(MeetingRegistrationTravel entity)
        {
            if ((!this.MeetingRegistrationTravelId().HasValue) & (entity != null))
            {
                this.MeetingRegistrationTravels.Add(entity);
            }
        }
        /// <summary>
        /// Adds a AddMeetingRegistrationHotel entity if one does not exist.
        /// </summary>
        /// <param name="entity">MeetingRegistrationTravel entity</param>
        public void AddMeetingRegistrationHotel(MeetingRegistrationHotel entity)
        {
            if ((!this.MeetingRegistrationHotelId().HasValue) & (entity != null))
            {
                this.MeetingRegistrationHotels.Add(entity);
            }
        }
        /// <summary>
        /// Adds a AddMeetingRegistrationHotel entity if one does not exist.
        /// </summary>
        /// <param name="entity">MeetingRegistrationTravel entity</param>
        public void AddMeetingRegistrationAttendance(MeetingRegistrationAttendance entity)
        {
            if ((!this.MeetingRegistrationAttendanceId().HasValue) & (entity != null))
            {
                this.MeetingRegistrationAttendances.Add(entity);
            }
        }
        /// <summary>
        /// Populate the MeetingRegistration entity.
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="registrationSubmittedFlag">Indicates if the registration was submitted.</param>
        public void Populate(int panelUserAssignmentId, bool registrationSubmittedFlag)
        {
            this.PanelUserAssignmentId = panelUserAssignmentId;
            this.RegistrationSubmittedFlag = registrationSubmittedFlag;
            if (registrationSubmittedFlag)
            {
                this.RegistrationSubmittedDate = GlobalProperties.P2rmisDateTimeNow;
            }
        }
    }
}
