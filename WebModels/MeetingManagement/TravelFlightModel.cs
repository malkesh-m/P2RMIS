using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.MeetingManagement
{
    public interface ITravelFlightModel
    {
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        int? PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the session user assignment identifier.
        /// </summary>
        /// <value>
        /// The session user assignment identifier.
        /// </value>
        int? SessionUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the flight identifier.
        /// </summary>
        int? FlightId { get; set; }
        /// <summary>
        /// Gets or sets the name of the carrier.
        /// </summary>
        /// <value>
        /// The name of the carrier.
        /// </value>
        string CarrierName { get; set; }
        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        string FlightNumber { get; set; }
        /// <summary>
        /// Gets or sets the departure city.
        /// </summary>
        /// <value>
        /// The departure city.
        /// </value>
        string DepartureCity { get; set; }
        /// <summary>
        /// Gets or sets the departure date.
        /// </summary>
        /// <value>
        /// The departure date.
        /// </value>
        DateTime DepartureDate { get; set; }
        /// <summary>
        /// Gets or sets the arrival city.
        /// </summary>
        /// <value>
        /// The arrival city.
        /// </value>
        string ArrivalCity { get; set; }
        /// <summary>
        /// Gets or sets the arrival date.
        /// </summary>
        /// <value>
        /// The arrival date.
        /// </value>
        DateTime ArrivalDate { get; set; }
    }

    public class TravelFlightModel : ITravelFlightModel
    {
        public TravelFlightModel(int? panelUserAssignmentId, int? sessionUserAssignmentId, int? flightId,
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
            DepartureDate = departureDate;
            ArrivalCity = arrivalCity;
            ArrivalDate = arrivalDate;
        }

        public TravelFlightModel(int? panelUserAssignmentId, int? sessionUserAssignmentId, int? flightId,
            string carrierName, string flightNumber,
            string departureCity, DateTime departureDate,
            string arrivalCity, DateTime arrivalDate, 
            string lastModifiedBy, DateTime lastModifiedDate) :
            this(panelUserAssignmentId, sessionUserAssignmentId, flightId, carrierName, flightNumber,
                departureCity, departureDate, arrivalCity, arrivalDate)
        {
            LastModifiedBy = lastModifiedBy;
            LastModifiedDate = lastModifiedDate;
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
        /// Gets or sets the flight identifier.
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
        public DateTime DepartureDate { get; set; }
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
        public DateTime ArrivalDate { get; set; }
        /// <summary>
        /// Gets or sets the username of the acount who made the last modification.
        /// </summary>
        public string LastModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the date/time when the last modification was made.
        /// </summary>
        public DateTime LastModifiedDate { get; set; }
    }
}
