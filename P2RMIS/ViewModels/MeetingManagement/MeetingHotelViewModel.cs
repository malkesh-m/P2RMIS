using System;
using Sra.P2rmis.CrossCuttingServices;
using Newtonsoft.Json;
using Sra.P2rmis.WebModels.MeetingManagement;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.Web.UI.Models;

namespace Sra.P2rmis.Web.ViewModels.MeetingManagement
{
    public class MeetingHotelViewModel : MMTabsViewModel
    {
        public MeetingHotelViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingHotelViewModel"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="panelName">Name of the panel.</param>
        /// <param name="attendanceStart">The attendance start.</param>
        /// <param name="attendanceEnd">The attendance end.</param>
        /// <param name="hotelNotRequiredFlag">if set to <c>true</c> hotel is not required.</param>
        /// <param name="checkInDate">The check in date.</param>
        /// <param name="checkOutDate">The check out date.</param>
        /// <param name="hotelId">The hotel identifier.</param>
        /// <param name="doubleOccupancy">if set to <c>true</c> [double occupancy].</param>
        /// <param name="specialRequest">The special request.</param>
        /// <param name="cancelDate">The cancel date.</param>
        /// <param name="participantModifiedDate">The participant modified date.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedByName">Name of the modified by.</param>
        public MeetingHotelViewModel(string firstName, string lastName, string panelName, DateTime? attendanceStart, DateTime? attendanceEnd,
            bool hotelNotRequiredFlag, DateTime? checkInDate, DateTime? checkOutDate, int? hotelId, bool doubleOccupancy,
            string specialRequest, bool isDataComplete,  DateTime? cancelDate, DateTime? participantModifiedDate, DateTime? modifiedDate, string modifiedByName,
            DateTime? panelStart, DateTime? panelEnd, int? defaultHotelId, int? panelUserAssignmentId, int? sessionUserAssignmentId, string subTab1Link, string subTab2Link, string subTab3Link)
        {
            ReviewerName = ViewHelpers.ConstructNameWithSpace(firstName, lastName);
            PanelName = panelName;
            AttendanceStart = attendanceStart;
            AttendanceEnd = attendanceEnd;
            HotelNotRequiredFlag = hotelNotRequiredFlag;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            HotelId = hotelId;
            DoubleOccupancy = doubleOccupancy;
            SpecialRequest = specialRequest;
            IsDataComplete = isDataComplete;
            CancelDate = cancelDate;
            ParticipantSubmitted = ViewHelpers.FormatDate(participantModifiedDate);
            LastUpdated = modifiedDate;
            LastUpdatedBy = modifiedByName;
            PanelStarted = ViewHelpers.FormatDate(panelStart);
            PanelEnded = ViewHelpers.FormatDate(panelEnd);
            DefaultHotelId = defaultHotelId;
            PanelUserAssignmentId = panelUserAssignmentId;
            SessionUserAssignmentId = sessionUserAssignmentId;
            SubTab1Link = ViewHelpers.BuildHotelTravelSublink(subTab1Link, panelUserAssignmentId, sessionUserAssignmentId);
            SubTab2Link = ViewHelpers.BuildHotelTravelSublink(subTab2Link, panelUserAssignmentId, sessionUserAssignmentId);
            SubTab3Link = ViewHelpers.BuildHotelTravelSublink(subTab3Link, panelUserAssignmentId, sessionUserAssignmentId);

        }
        #region Properties
        /// <summary>
        /// Gets or sets the name of the reviewer.
        /// </summary>
        /// <value>
        /// The name of the reviewer.
        /// </value>
        public string ReviewerName { get; set; }
        /// <summary>
        /// Gets or sets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        public string PanelName { get; set; }
        /// <summary>
        /// Gets or sets the attendance start.
        /// </summary>
        /// <value>
        /// The attendance start.
        /// </value>
        public DateTime? AttendanceStart { get; set; }
        /// <summary>
        /// Gets or sets the attendance end.
        /// </summary>
        /// <value>
        /// The attendance end.
        /// </value>
        public DateTime? AttendanceEnd { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [hotel not required].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hotel not required]; otherwise, <c>false</c>.
        /// </value>
        public bool HotelNotRequiredFlag { get; set; }
        /// <summary>
        /// Gets or sets the check in date.
        /// </summary>
        /// <value>
        /// The check in date.
        /// </value>
        public DateTime? CheckInDate { get; set; }
        /// <summary>
        /// Gets or sets the check out date.
        /// </summary>
        /// <value>
        /// The check out date.
        /// </value>
        public DateTime? CheckOutDate { get; set; }
        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int? HotelId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [double occupancy].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [double occupancy]; otherwise, <c>false</c>.
        /// </value>
        public bool DoubleOccupancy { get; set; }
        /// <summary>
        /// Gets or sets the special request.
        /// </summary>
        /// <value>
        /// The special request.
        /// </value>
        public string SpecialRequest { get; set; }
        /// <summary>
        /// Gets or sets the cancel date.
        /// </summary>
        /// <value>
        /// The cancel date.
        /// </value>
        public DateTime? CancelDate { get; set; }
        /// <summary>
        /// Gets or sets the participant submitted.
        /// </summary>
        /// <value>
        /// The participant submitted.
        /// </value>
        public string ParticipantSubmitted { get; set; }
        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        /// <value>
        /// The last updated.
        /// </value>
        public DateTime? LastUpdated { get; set; }
        /// <summary>
        /// Gets or sets the last updated by.
        /// </summary>
        /// <value>
        /// The last updated by.
        /// </value>
        public string LastUpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the travel mode dropdown.
        /// </summary>
        /// <value>
        /// The travel mode dropdown.
        /// </value>
        public List<IListEntry> TravelModeDropdown { get; set; } = new List<IListEntry>();
        /// <summary>
        /// Gets or sets the hotel dropdown.
        /// </summary>
        /// <value>
        /// The hotel dropdown.
        /// </value>
        public List<IListEntry> HotelDropdown { get; set; } = new List<IListEntry>();
        /// <summary>
        /// Gets or sets the panel start.
        /// </summary>
        /// <value>
        /// The panel start.
        /// </value>
        public DateTime? PanelStart { get; set; }
        /// <summary>
        /// Gets or sets the panel end.
        /// </summary>
        /// <value>
        /// The panel end.
        /// </value>
        public DateTime? PanelEnd { get; set; }
        /// <summary>
        /// Gets or sets the default hotel identifier.
        /// </summary>
        /// <value>
        /// The default hotel identifier.
        /// </value>
        public int? DefaultHotelId { get; set; }
        /// <summary>
        /// Gets or sets the panel started.
        /// </summary>
        /// <value>
        /// The panel started.
        /// </value>
        public string PanelStarted { get; set; }
        /// <summary>
        /// Gets or sets the panel ended.
        /// </summary>
        /// <value>
        /// The panel ended.
        /// </value>
        public string PanelEnded { get; set; }

        /// <summary>
        /// Gets or sets the sub tab1 link.
        /// </summary>
        /// <value>
        /// The sub tab1 link.
        /// </value>
        public string SubTab1Link {get;set;}
        /// <summary>
        /// Gets or sets the sub tab2 link.
        /// </summary>
        /// <value>
        /// The sub tab2 link.
        /// </value>
        public string SubTab2Link {get;set;}
        /// <summary>
        /// Gets or sets the sub tab3 link.
        /// </summary>
        /// <value>
        /// The sub tab3 link.
        /// </value>
        public string SubTab3Link {get;set;}
        /// <summary>
        /// Gets or sets the meeting details header model.
        /// </summary>
        /// <value>
        /// The meeting details header model.
        /// </value>
        public MeetingDetailsHeaderModel DetailsHeader { get; set; }

        /// <summary>
        /// Gets or sets the isDataComplete flag.
        /// </summary>
        /// <value>
        /// The isDataComplete flag.
        /// </value>
        public bool IsDataComplete { get; set; }
        #endregion
    }
}