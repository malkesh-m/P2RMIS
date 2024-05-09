using Sra.P2rmis.CrossCuttingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.MeetingManagement
{
    public interface IMeetingHotelModel
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        string LastName { get; set; }
        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the attendance start date.
        /// </summary>
        /// <value>
        /// The attendance start date.
        /// </value>
        DateTime? AttendanceStartDate { get; set; }
        /// <summary>
        /// Gets or sets the attendance end date.
        /// </summary>
        /// <value>
        /// The attendance end date.
        /// </value>
        DateTime? AttendanceEndDate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [hotel required flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hotel required flag]; otherwise, <c>false</c>.
        /// </value>
        bool HotelRequiredFlag { get; set; }
        /// <summary>
        /// Gets or sets the check in date.
        /// </summary>
        /// <value>
        /// The check in date.
        /// </value>
        DateTime? CheckInDate { get; set; }
        /// <summary>
        /// Gets or sets the check out date.
        /// </summary>
        /// <value>
        /// The check out date.
        /// </value>
        DateTime? CheckOutDate { get; set; }
        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        int? HotelId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [double occupancy].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [double occupancy]; otherwise, <c>false</c>.
        /// </value>
        bool DoubleOccupancy { get; set; }
        /// <summary>
        /// Gets or sets the hotel and food request comments.
        /// </summary>
        /// <value>
        /// The hotel and food request comments.
        /// </value>
        string HotelAndFoodRequestComments { get; set; }

        /// <summary>
        /// Gets or sets the isDataComplete flag.
        /// </summary>
        /// <value>
        /// The isDataComplete flag.
        /// </value>
        bool IsDataComplete { get; set; }

        /// <summary>
        /// Gets or sets the cancellation date.
        /// </summary>
        /// <value>
        /// The cancellation date.
        /// </value>
        DateTime? CancellationDate { get; set; }
        /// <summary>
        /// Gets or sets the participant modified date.
        /// </summary>
        /// <value>
        /// The participant modified date.
        /// </value>
        DateTime? ParticipantModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>
        /// The name of the modified by.
        /// </value>
        string ModifiedByName { get; set; }
        /// <summary>
        /// Gets or sets the panel start.
        /// </summary>
        /// <value>
        /// The panel start.
        /// </value>
        DateTime? PanelStart { get; set; }
        /// <summary>
        /// Gets or sets the panel end.
        /// </summary>
        /// <value>
        /// The panel end.
        /// </value>
        DateTime? PanelEnd { get; set; }
        /// <summary>
        /// Gets or sets the default hotel identifier.
        /// </summary>
        /// <value>
        /// The default hotel identifier.
        /// </value>
        int? DefaultHotelId { get; set; }
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        int? PanelUserAssignmentId {get;set;}
        /// <summary>
        /// Gets or sets the sub tab1 link.
        /// </summary>
        /// <value>
        /// The sub tab1 link.
        /// </value>
        string SubTab1Link {get;set;}
        /// <summary>
        /// Gets or sets the sub tab2 link.
        /// </summary>
        /// <value>
        /// The sub tab2 link.
        /// </value>
        string SubTab2Link {get;set;}
        /// <summary>
        /// Gets or sets the sub tab3 link.
        /// </summary>
        /// <value>
        /// The sub tab3 link.
        /// </value>
        string SubTab3Link {get;set;}
    }

    public class MeetingHotelModel
    {
        public MeetingHotelModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingHotelModel"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="panelAbbreviation">The panel abbreviation.</param>
        /// <param name="panelStart">The panel start.</param>
        /// <param name="panelEnd">The panel end.</param>
        /// <param name="defaultHotelId">The default hotel identifier.</param>
        public MeetingHotelModel(string firstName, string lastName, string panelAbbreviation, DateTime? panelStart, DateTime? panelEnd, int? defaultHotelId, int? panelUserAssignmentId)
        {
            FirstName = firstName;
            LastName = lastName;
            PanelAbbreviation = panelAbbreviation;
            PanelStart = panelStart;
            PanelEnd = panelEnd;
            DefaultHotelId = defaultHotelId;
            PanelUserAssignmentId = panelUserAssignmentId;
        }

        /// <summary>
        /// Overload for non-reviewers without hotel and travel data.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="defaultHotelId">The default hotel identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        public MeetingHotelModel(string firstName, string lastName, int? defaultHotelId, int? sessionUserAssignmentId)
        {
            FirstName = firstName;
            LastName = lastName;
            DefaultHotelId = defaultHotelId;
            SessionUserAssignmentId = sessionUserAssignmentId;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingHotelModel"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="panelAbbreviation">The panel abbreviation.</param>
        /// <param name="attendanceStartDate">The attendance start date.</param>
        /// <param name="attendanceEndDate">The attendance end date.</param>
        /// <param name="hotelNotRequiredFlag">if set to <c>true</c> hotel is not required.</param>
        /// <param name="checkInDate">The check in date.</param>
        /// <param name="checkOutDate">The check out date.</param>
        /// <param name="hotelId">The hotel identifier.</param>
        /// <param name="doubleOccupancy">if set to <c>true</c> [double occupancy].</param>
        /// <param name="hotelAndFoodRequestComments">The hotel and food request comments.</param>
        /// <param name="cancellationDate">The cancellation date.</param>
        /// <param name="participantModifiedDate">The participant modified date.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedByName">Name of the modified by.</param>
        public MeetingHotelModel(string firstName, string lastName, string panelAbbreviation, DateTime? attendanceStartDate, DateTime? attendanceEndDate,
            bool hotelNotRequiredFlag, DateTime? checkInDate, DateTime? checkOutDate, int? hotelId, bool doubleOccupancy, 
            string hotelAndFoodRequestComments, bool isDataComplete, DateTime? cancellationDate, DateTime? participantModifiedDate,
            DateTime? modifiedDate, string modifiedByName, DateTime? panelStart, DateTime? panelEnd, int? defaultHotelId, int? panelUserAssignmentId, string subTab1Link, string subTab2Link, string subTab3Link)
        {
            FirstName = firstName;
            LastName = lastName;
            PanelAbbreviation = panelAbbreviation;
            AttendanceStartDate = attendanceStartDate;
            AttendanceEndDate = attendanceEndDate;
            HotelNotRequiredFlag = hotelNotRequiredFlag;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            HotelId = hotelId;
            DoubleOccupancy = doubleOccupancy;
            HotelAndFoodRequestComments = hotelAndFoodRequestComments;
            IsDataComplete = isDataComplete;
            CancellationDate = cancellationDate;
            ParticipantModifiedDate = participantModifiedDate;
            ModifiedDate = modifiedDate;
            ModifiedByName = modifiedByName;
            PanelStart = panelStart;
            PanelEnd = panelEnd;
            DefaultHotelId = defaultHotelId;
            PanelUserAssignmentId = panelUserAssignmentId;
            SubTab1Link = ViewHelpers.BuildHotelTravelSublink(subTab1Link, panelUserAssignmentId, null);
            SubTab2Link = ViewHelpers.BuildHotelTravelSublink(subTab2Link, panelUserAssignmentId, null);
            SubTab3Link = ViewHelpers.BuildHotelTravelSublink(subTab3Link, panelUserAssignmentId, null);
        }

        /// <summary>
        /// Overload for non-reviewers with hotel and travel info.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="attendanceStartDate">The attendance start date.</param>
        /// <param name="attendanceEndDate">The attendance end date.</param>
        /// <param name="hotelNotRequiredFlag">if set to <c>true</c> hotel is not required.</param>
        /// <param name="checkInDate">The check in date.</param>
        /// <param name="checkOutDate">The check out date.</param>
        /// <param name="hotelId">The hotel identifier.</param>
        /// <param name="doubleOccupancy">if set to <c>true</c> [double occupancy].</param>
        /// <param name="hotelAndFoodRequestComments">The hotel and food request comments.</param>
        /// <param name="isDataComplete">if set to <c>true</c> [is data complete].</param>
        /// <param name="cancellationDate">The cancellation date.</param>
        /// <param name="participantModifiedDate">The participant modified date.</param>
        /// <param name="modifiedDate">The modified date.</param>
        /// <param name="modifiedByName">Name of the modified by.</param>
        /// <param name="defaultHotelId">The default hotel identifier.</param>
        /// <param name="sessionUserAssignmentId">The session user assignment identifier.</param>
        /// <param name="subTab1Link">The sub tab1 link.</param>
        /// <param name="subTab2Link">The sub tab2 link.</param>
        /// <param name="subTab3Link">The sub tab3 link.</param>
        public MeetingHotelModel(string firstName, string lastName, DateTime? attendanceStartDate, DateTime? attendanceEndDate,
            bool hotelNotRequiredFlag, DateTime? checkInDate, DateTime? checkOutDate, int? hotelId, bool doubleOccupancy,
            string hotelAndFoodRequestComments, bool isDataComplete, DateTime? cancellationDate, DateTime? participantModifiedDate,
            DateTime? modifiedDate, string modifiedByName, int? defaultHotelId, int? sessionUserAssignmentId, string subTab1Link, string subTab2Link, string subTab3Link)
        {
            FirstName = firstName;
            LastName = lastName;
            AttendanceStartDate = attendanceStartDate;
            AttendanceEndDate = attendanceEndDate;
            HotelNotRequiredFlag = hotelNotRequiredFlag;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            HotelId = hotelId;
            DoubleOccupancy = doubleOccupancy;
            HotelAndFoodRequestComments = hotelAndFoodRequestComments;
            IsDataComplete = isDataComplete;
            CancellationDate = cancellationDate;
            ParticipantModifiedDate = participantModifiedDate;
            ModifiedDate = modifiedDate;
            ModifiedByName = modifiedByName;
            DefaultHotelId = defaultHotelId;
            SessionUserAssignmentId = sessionUserAssignmentId;
            SubTab1Link = ViewHelpers.BuildHotelTravelSublink(subTab1Link, null, sessionUserAssignmentId);
            SubTab2Link = ViewHelpers.BuildHotelTravelSublink(subTab2Link, null, sessionUserAssignmentId);
            SubTab3Link = ViewHelpers.BuildHotelTravelSublink(subTab3Link, null, sessionUserAssignmentId);
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the attendance start date.
        /// </summary>
        /// <value>
        /// The attendance start date.
        /// </value>
        public DateTime? AttendanceStartDate { get; set; }
        /// <summary>
        /// Gets or sets the attendance end date.
        /// </summary>
        /// <value>
        /// The attendance end date.
        /// </value>
        public DateTime? AttendanceEndDate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether hotel is not required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if hotel is not required; otherwise, <c>false</c>.
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
        /// Gets or sets the hotel and food request comments.
        /// </summary>
        /// <value>
        /// The hotel and food request comments.
        /// </value>
        public string HotelAndFoodRequestComments { get; set; }

        /// <summary>
        /// Gets or sets the isDataComplete flag.
        /// </summary>
        /// <value>
        /// The isDataComplete flag.
        /// </value>
        public bool IsDataComplete { get; set; }

        /// <summary>
        /// Gets or sets the cancellation date.
        /// </summary>
        /// <value>
        /// The cancellation date.
        /// </value>
        public DateTime? CancellationDate { get; set; }
        /// <summary>
        /// Gets or sets the participant modified date.
        /// </summary>
        /// <value>
        /// The participant modified date.
        /// </value>
        public DateTime? ParticipantModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the modified by.
        /// </summary>
        /// <value>
        /// The name of the modified by.
        /// </value>
        public string ModifiedByName { get; set; }
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
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int? PanelUserAssignmentId {get;set;}
        /// <summary>
        /// Gets or sets the session user assignment identifier.
        /// </summary>
        /// <value>
        /// The session user assignment identifier.
        /// </value>
        public int? SessionUserAssignmentId { get;set;}
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
    }
}
