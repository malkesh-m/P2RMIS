using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.MeetingManagement
{
    public interface IMeetingRegistrationTravelFlightRepository : IGenericRepository<MeetingRegistrationTravelFlight>
    {
        /// <summary>
        /// Adds the flight.
        /// </summary>
        /// <param name="carrierName">Name of the carrier.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="departureCity">The departure city.</param>
        /// <param name="departureDate">The departure date.</param>
        /// <param name="arrivalCity">The arrival city.</param>
        /// <param name="arrivalDate">The arrival date.</param>
        /// <returns></returns>
        MeetingRegistrationTravelFlight AddDefaultFlight(string carrierName, string flightNumber, string departureCity, DateTime? departureDate, string arrivalCity, DateTime? arrivalDate, int userId);
        /// <summary>
        /// Deletes the flight.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <param name="userId">The user identifier.</param>
        void DeleteFlight(MeetingRegistrationTravelFlight flight, int userId);
        /// <summary>
        /// Updates the flight.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <param name="carrierName">Name of the carrier.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="departureCity">The departure city.</param>
        /// <param name="departureDate">The departure date.</param>
        /// <param name="arrivalCity">The arrival city.</param>
        /// <param name="arrivalDate">The arrival date.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        MeetingRegistrationTravelFlight UpdateFlight(MeetingRegistrationTravelFlight flight, string carrierName, string flightNumber, string departureCity, DateTime? departureDate, string arrivalCity, DateTime? arrivalDate, int userId);
    }

    public class MeetingRegistrationTravelFlightRepository : GenericRepository<MeetingRegistrationTravelFlight>, IMeetingRegistrationTravelFlightRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public MeetingRegistrationTravelFlightRepository(P2RMISNETEntities context) : base(context) { }
        #endregion

        /// <summary>
        /// Adds the flight.
        /// </summary>
        /// <param name="carrierName">Name of the carrier.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="departureCity">The departure city.</param>
        /// <param name="departureDate">The departure date.</param>
        /// <param name="arrivalCity">The arrival city.</param>
        /// <param name="arrivalDate">The arrival date.</param>
        /// <returns></returns>
        public MeetingRegistrationTravelFlight AddDefaultFlight(string carrierName, string flightNumber, string departureCity, DateTime? departureDate, string arrivalCity, DateTime? arrivalDate, int userId)
        {
            var o = new MeetingRegistrationTravelFlight();
            o.CarrierName = carrierName;
            o.FlightNumber = flightNumber;
            o.DepartureCity = departureCity;
            o.DepartureDate = departureDate;
            o.ArrivalCity = arrivalCity;
            o.ArrivalDate = arrivalDate;
            Helper.UpdateCreatedFields(o, userId);
            Helper.UpdateModifiedFields(o, userId);
            return o;
        }

        /// <summary>
        /// Updates the flight.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <param name="carrierName">Name of the carrier.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="departureCity">The departure city.</param>
        /// <param name="departureDate">The departure date.</param>
        /// <param name="arrivalCity">The arrival city.</param>
        /// <param name="arrivalDate">The arrival date.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public MeetingRegistrationTravelFlight UpdateFlight(MeetingRegistrationTravelFlight flight, string carrierName, string flightNumber, string departureCity, DateTime? departureDate, string arrivalCity, DateTime? arrivalDate, int userId)
        {
            flight.CarrierName = carrierName;
            flight.FlightNumber = flightNumber;
            flight.DepartureCity = departureCity;
            flight.DepartureDate = departureDate;
            flight.ArrivalCity = arrivalCity;
            flight.ArrivalDate = arrivalDate;
            Helper.UpdateModifiedFields(flight, userId);
            return flight;
        }
        /// <summary>
        /// Deletes the flight.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteFlight(MeetingRegistrationTravelFlight flight, int userId)
        {
            Helper.UpdateDeletedFields(flight, userId);
            Delete(flight);
        }
    }
}
