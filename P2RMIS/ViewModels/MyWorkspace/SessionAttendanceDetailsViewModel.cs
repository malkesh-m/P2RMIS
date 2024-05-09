using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll;
using Sra.P2rmis.WebModels.HotelAndTravel;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.WebModels.Lists;

namespace Sra.P2rmis.Web.UI.Models
{
    public class SessionAttendanceDetailsViewModel
    {
        /// <summary>
        /// The "to be determined" string.
        /// </summary>
        public const string TBD = "TBD";

        public SessionAttendanceDetailsViewModel()
        {
            //HotelNotRequired = true;
        }

        public SessionAttendanceDetailsViewModel(ISessionAttendanceDetailsModel model)
        {
            var hotelName = !string.IsNullOrEmpty(model.HotelName) ? model.HotelName : TBD;
            Panel = String.Format("{0} {1} {2} &nbsp;{3}-{4} | {5}", model.Year, model.ProgramAbbreviation, model.PanelAbbreviation,
                ViewHelpers.FormatDate(model.StartDate), ViewHelpers.FormatDate(model.EndDate), hotelName);
            AttendanceStartDate = ViewHelpers.FormatDate(model.AttendanceStartDate);
            AttendanceEndDate = ViewHelpers.FormatDate(model.AttendanceEndDate);
            HotelCheckInDate = ViewHelpers.FormatDate(model.HotelCheckInDate);
            HotelCheckOutDate = ViewHelpers.FormatDate(model.HotelCheckOutDate);
            TravelModeId = model.TravelModeId;
            HotelNotRequired = model.HotelNotRequired;
            HotelDoubleOccupancy = model.HotelDoubleOccupancy;
            HotelAndFoodRequestComments = model.HotelAndFoodRequestComments;
            TravelRequestComments = model.TravelRequestComments;
            PanelUserAssignmentId = model.PanelUserAssignmentId;
            MeetingRegistrationId = model.MeetingRegistrationId;
            HotelId = model.HotelId;
            Submitted = model.Submitted;
        }

        public SessionAttendanceDetailsViewModel(int? panelUserAssignmentId, int? meetingRegistrationId, string attendanceStartDate, string attendanceEndDate,
            int? hotelId, string hotelCheckInDate, string hotelCheckOutDate, int? travelModeId, bool hotelNotRequired, bool hotelDoubleOccupancy,
            string hotelAndFoodRequestComments, string travelRequestComments)
        {
            PanelUserAssignmentId = panelUserAssignmentId;
            MeetingRegistrationId = meetingRegistrationId;
            AttendanceStartDate = attendanceStartDate;
            AttendanceEndDate = attendanceEndDate;
            HotelId = hotelId;
            HotelCheckInDate = hotelCheckInDate;
            HotelCheckOutDate = hotelCheckOutDate;
            TravelModeId = travelModeId;
            HotelNotRequired = hotelNotRequired;
            HotelDoubleOccupancy = hotelDoubleOccupancy;
            HotelAndFoodRequestComments = hotelAndFoodRequestComments;
            TravelRequestComments = travelRequestComments;
        }
        #region Properties        
        /// <summary>
        /// Gets the panel.
        /// </summary>
        /// <value>
        /// The panel.
        /// </value>
        public string Panel { get; private set; }
        /// <summary>
        /// Gets the attendance start date.
        /// </summary>
        /// <value>
        /// The attendance start date.
        /// </value>
        public string AttendanceStartDate { get; private set; }
        /// <summary>
        /// Gets the attendance end date.
        /// </summary>
        /// <value>
        /// The attendance end date.
        /// </value>
        public string AttendanceEndDate { get; private set; }
        /// <summary>
        /// Gets the hotel check in date.
        /// </summary>
        /// <value>
        /// The hotel check in date.
        /// </value>
        public string HotelCheckInDate { get; private set; }
        /// <summary>
        /// Gets the hotel check out date.
        /// </summary>
        /// <value>
        /// The hotel check out date.
        /// </value>
        public string HotelCheckOutDate { get; private set; }
        /// <summary>
        /// Gets the travel mode identifier.
        /// </summary>
        /// <value>
        /// The travel mode identifier.
        /// </value>
        public int? TravelModeId { get; private set; }
        /// <summary>
        /// Gets or sets the travel mode dropdown.
        /// </summary>
        /// <value>
        /// The travel mode dropdown.
        /// </value>
        public List<IListEntry> TravelModeDropdown { get; set; }
        /// <summary>
        /// Gets a value indicating whether [hotel not required].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hotel not required]; otherwise, <c>false</c>.
        /// </value>
        public bool HotelNotRequired { get; private set; }
        /// <summary>
        /// Gets a value indicating whether [hotel double occupancy].
        /// </summary>
        /// <value>
        /// <c>true</c> if [hotel double occupancy]; otherwise, <c>false</c>.
        /// </value>
        public bool HotelDoubleOccupancy { get; private set; }
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
        /// Gets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int? HotelId { get; private set; }
        /// <summary>
        /// Gets the meeting registration identifier.
        /// </summary>
        /// <value>
        /// The meeting registration identifier.
        /// </value>
        public int? MeetingRegistrationId { get; private set; }
        /// <summary>
        /// Gets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int? PanelUserAssignmentId { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="SessionAttendanceDetailsViewModel"/> is submitted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if submitted; otherwise, <c>false</c>.
        /// </value>
        public bool Submitted { get; private set; }
        /// <summary>
        /// Identifier Air Travel Mode
        /// </summary>
        public int AirTravelModeId = LookupService.LookupTravelModeIdAir;
        /// <summary>
        /// Identifier for Train Travel Mode
        /// </summary>
        public int TrainTravelModeId = LookupService.LookupTravelModeIdTrain;
        #endregion

        #region Helpers        
        /// <summary>
        /// Gets the session attendance details model.
        /// </summary>
        /// <returns></returns>
        public ISessionAttendanceDetailsStringDateModel GetSessionAttendanceDetailsModel()
        {
            var model = new SessionAttendanceDetailsStringDateModel();
            model.PopulateRegistration(MeetingRegistrationId, AttendanceStartDate, AttendanceEndDate, Submitted);
            model.PopulatePanelUserAssignment(PanelUserAssignmentId);
            model.PopulateHotelRegistration(HotelId, string.Empty, HotelCheckInDate, HotelCheckOutDate,
                TravelModeId, HotelNotRequired, HotelDoubleOccupancy);
            model.PopulateSpecialNeeds(HotelAndFoodRequestComments, TravelRequestComments);
            return model;
        }
        #endregion
    }
}