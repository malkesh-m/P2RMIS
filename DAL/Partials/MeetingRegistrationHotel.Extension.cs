using Sra.P2rmis.Dal.Interfaces;
using System;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's MeetingRegistrationHotel object. 
    /// </summary>
    public partial class MeetingRegistrationHotel : IStandardDateFields
    {
        /// <summary>
        /// Populates the specified hotel check in date.
        /// </summary>
        /// <param name="hotelCheckInDate">The hotel check in date.</param>
        /// <param name="hotelCheckOutDate">The hotel check out date.</param>
        /// <param name="hotelNotRequiredFlag">if set to <c>true</c> hotel is not required for the participant.</param>
        /// <param name="hotelDoubleOccupancy">if set to <c>true</c> [hotel double occupancy].</param>
        /// <param name="hotelId">The hotel identifier.</param>
        /// <param name="hotelAndFoodRequestComments">The hotel and food request comments.</param>
        /// <param name="cancellationDate">The hotel and food request comments.</param>
        public void Populate(DateTime? hotelCheckInDate, DateTime? hotelCheckOutDate, bool hotelNotRequiredFlag, bool hotelDoubleOccupancy, int? hotelId, string hotelAndFoodRequestComments, DateTime? cancellationDate)
        {
            this.HotelCheckInDate = hotelCheckInDate;
            this.HotelCheckOutDate = hotelCheckOutDate;
            this.HotelNotRequiredFlag = hotelNotRequiredFlag;
            this.HotelDoubleOccupancy = hotelDoubleOccupancy;
            this.HotelId = hotelId;
            this.HotelAndFoodRequestComments = hotelAndFoodRequestComments;
            this.CancellationDate = cancellationDate;
        }
    }
}
