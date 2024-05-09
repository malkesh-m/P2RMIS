using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.MeetingManagement
{
    public interface IMeetingRegistrationTravelRepository : IGenericRepository<MeetingRegistrationTravel>
    {
        /// <summary>
        /// Adds the default travel.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        MeetingRegistrationTravel AddDefaultTravel(int userId);
        /// <summary>
        /// Updates the reservation code.
        /// </summary>
        /// <param name="travel">The travel.</param>
        /// <param name="reservationCode">The reservation code.</param>
        /// <param name="userId">The user identifier.</param>
        void UpdateReservationCode(MeetingRegistrationTravel travel, string reservationCode, int userId);
    }

    public class MeetingRegistrationTravelRepository : GenericRepository<MeetingRegistrationTravel>, IMeetingRegistrationTravelRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public MeetingRegistrationTravelRepository(P2RMISNETEntities context) : base(context) { }
        #endregion

        /// <summary>
        /// Adds the default travel.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public MeetingRegistrationTravel AddDefaultTravel(int userId)
        {
            var o = new MeetingRegistrationTravel();
            o.NoGsa = false;
            Helper.UpdateCreatedFields(o, userId);
            Helper.UpdateModifiedFields(o, userId);
            return o;
        }
        /// <summary>
        /// Updates the reservation code.
        /// </summary>
        /// <param name="travel"></param>
        /// <param name="reservationCode">The reservation code.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateReservationCode(MeetingRegistrationTravel travel, string reservationCode, int userId)
        {
            travel.ReservationCode = reservationCode;
            Helper.UpdateModifiedFields(travel, userId);
        }
    }
}
