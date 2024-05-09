using System;
using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.ViewModels.MeetingManagement
{
    public class TravelFlightViewModel
    {
        public TravelFlightViewModel() { }

        public TravelFlightViewModel(int? panelUserAssignmentId, int? sessionUserAssignmentId, int? flightId,
            string carrierName, string flightNumber,
            string departureCity, DateTime departureDate,
            string arrivalCity, DateTime arrivalDate)
        {
            PanelUserAssignmentId = panelUserAssignmentId;
            SessionUserAssignmentId = sessionUserAssignmentId;
            FlightId = flightId;
            CarrierName = carrierName;
            FlightNumber = flightNumber;
            DepartureCity = departureCity;
            DepartureDate = ViewHelpers.FormatDateTime3(departureDate);
            ArrivalCity = arrivalCity;
            ArrivalDate = ViewHelpers.FormatDateTime3(arrivalDate);
        }

        public TravelFlightViewModel(int? panelUserAssignmentId, int? sessionUserAssignmentId, int? flightId,
            string carrierName, string flightNumber,
            string departureCity, DateTime departureDate,
            string arrivalCity, DateTime arrivalDate,
            string lastModifiedBy, DateTime lastModifiedDate) :
            this(panelUserAssignmentId, sessionUserAssignmentId, flightId, carrierName, flightNumber,
                departureCity, departureDate, arrivalCity, arrivalDate)
        {
            LastModifiedBy = lastModifiedBy;
            LastModifiedDate = ViewHelpers.FormatDateTime3(lastModifiedDate);
        }

        public TravelFlightViewModel(int? panelUserAssignmentId, int? sessionUserAssignmentId, int? flightId,
            string carrierName, string flightNumber, 
            string departureCity, DateTime departureDate,
            string arrivalCity, DateTime arrivalDate, List<string> carrierList,
            List<KeyValuePair<string, string>> airportList) :
            this(panelUserAssignmentId, sessionUserAssignmentId, flightId, carrierName, flightNumber,
                departureCity, departureDate, arrivalCity, arrivalDate)
        {
            CarrierList = carrierList;
            AirportList = airportList;
        }

        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int? PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the session user assignment identifier.
        /// </summary>
        /// <value>
        /// The session user assignment identifier.
        /// </value>
        public int? SessionUserAssignmentId { get; set; }
        /// <summary>
        /// The flight identifier.
        /// </summary>
        public int? FlightId { get; set; }
        /// <summary>
        /// Gets or sets the name of the carrier.
        /// </summary>
        /// <value>
        /// The name of the carrier.
        /// </value>
        public string CarrierName { get; set; }
        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        public string FlightNumber { get; set; }
        /// <summary>
        /// Gets or sets the departure city.
        /// </summary>
        /// <value>
        /// The departure city.
        /// </value>
        public string DepartureCity { get; set; }
        /// <summary>
        /// Gets or sets the departure date.
        /// </summary>
        /// <value>
        /// The departure date.
        /// </value>
        public string DepartureDate { get; set; }
        /// <summary>
        /// Gets or sets the arrival city.
        /// </summary>
        /// <value>
        /// The arrival city.
        /// </value>
        public string ArrivalCity { get; set; }
        /// <summary>
        /// Gets or sets the arrival date.
        /// </summary>
        /// <value>
        /// The arrival date.
        /// </value>
        public string ArrivalDate { get; set; }
        /// <summary>
        /// Gets or sets the username of the acount who made the last modification.
        /// </summary>
        public string LastModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the date/time when the last modification was made.
        /// </summary>
        public string LastModifiedDate { get; set; }
        /// <summary>
        /// Airport list.
        /// </summary>
        public List<string> CarrierList { get; set; }
        /// <summary>
        /// Carrier list.
        /// </summary>
        public List<KeyValuePair<string, string>> AirportList { get; set; }
    }
}