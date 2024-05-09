using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.MeetingManagement
{
    public interface ITravelFlightImportModel
    {
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        int? PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the reservation code.
        /// </summary>
        /// <value>
        /// The reservation code.
        /// </value>
        string ReservationCode { get; set; }
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
        DateTime? DepartureDate { get; set; }
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
        DateTime? ArrivalDate { get; set; }
        /// <summary>
        /// Gets or sets the fare.
        /// </summary>
        /// <value>
        /// The fare.
        /// </value>
        decimal? Fare { get; set; }
    }

    public class TravelFlightImportModel
    {
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int? PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the reservation code.
        /// </summary>
        /// <value>
        /// The reservation code.
        /// </value>
        public string ReservationCode { get; set; }
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
        public DateTime? DepartureDate { get; set; }
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
        public DateTime? ArrivalDate { get; set; }
        /// <summary>
        /// Gets or sets the fare.
        /// </summary>
        /// <value>
        /// The fare.
        /// </value>
        public decimal? Fare { get; set; }
    }
}
