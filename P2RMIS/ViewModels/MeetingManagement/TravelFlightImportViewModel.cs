using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.MeetingManagement;
using System.Text.RegularExpressions;

namespace Sra.P2rmis.Web.ViewModels.MeetingManagement
{
    public class TravelFlightImportViewModel
    {
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public string PanelUserAssignmentId { get; set; }
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
        public string DepartureDate { get; set; }
        /// <summary>
        /// Gets or sets the departure time.
        /// </summary>
        /// <value>
        /// The departure time.
        /// </value>
        public string DepartureTime { get; set; }
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
        /// Gets or sets the arrival time.
        /// </summary>
        /// <value>
        /// The arrival time.
        /// </value>
        public string ArrivalTime { get; set; }
        /// <summary>
        /// Gets or sets the fare.
        /// </summary>
        /// <value>
        /// The fare.
        /// </value>
        public decimal? Fare { get; set; }

        /// <summary>
        /// Gets the flight.
        /// </summary>
        /// <returns></returns>
        public TravelFlightImportModel GetFlight()
        {
            var flight = new TravelFlightImportModel();
            Regex regex = new Regex(@"^\d+$");
            flight.PanelUserAssignmentId = PanelUserAssignmentId != null ? (regex.IsMatch(PanelUserAssignmentId.Trim()) ? Int32.Parse(PanelUserAssignmentId.Trim()) :
                String.IsNullOrEmpty(PanelUserAssignmentId.Trim()) ? null : (int?)-1) : (int?)-1;

            flight.CarrierName = CarrierName;
            flight.FlightNumber = FlightNumber;
            flight.ReservationCode = ReservationCode;
            flight.DepartureCity = DepartureCity;
            if (DepartureDate == null || DepartureTime == null)
                flight.DepartureDate = null;
            else
                flight.DepartureDate = Convert.ToDateTime(String.Format("{0} {1}", DepartureDate, DepartureTime));
            flight.ArrivalCity = ArrivalCity;
            if (ArrivalDate == null || ArrivalTime == null)
                flight.ArrivalDate = null;
            else
                flight.ArrivalDate = Convert.ToDateTime(String.Format("{0} {1}", ArrivalDate, ArrivalTime));
            flight.Fare = Fare;
            return flight;
        }
    }
}