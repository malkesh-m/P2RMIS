using System;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.HotelAndTravel;

namespace Sra.P2rmis.Bll.HotelAndTravel
{
    /// <summary>
    /// Service Action method for CRUD operations on MeetingRegistrationHotel
    /// </summary>
    public class MeetingRegistrationHotelServiceAction : ServiceAction<MeetingRegistrationHotel>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public MeetingRegistrationHotelServiceAction()
        {
        }
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        /// <param name="model">Model containing information from the modal</param>
        public void Populate(ISessionAttendanceDetailsModel model, int? meetingRegistrationTravelId)
        {
            this.EntityIdentifier = meetingRegistrationTravelId ?? 0;
            this.HotelCheckInDate = model.HotelCheckInDate;
            this.HotelCheckOutDate = model.HotelCheckOutDate;
            this.HotelNotRequiredFlag = model.HotelNotRequired;
            this.HotelDoubleOccupancy = model.HotelDoubleOccupancy;
            this.HotelAndFoodRequestComments = model.HotelAndFoodRequestComments;
            this.HotelId = model.HotelId;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Hotel check-in date
        /// </summary>
        private Nullable<DateTime> HotelCheckInDate { get; set; }
        /// <summary>
        /// Hotel check-out date
        /// </summary>
        private Nullable<DateTime> HotelCheckOutDate { get; set; }
        /// <summary>
        /// Indicates if hotel is not required by participant.
        /// </summary>
        private bool HotelNotRequiredFlag { get; set; }
        /// <summary>
        /// Indicates a double occupancy
        /// </summary>
        private bool HotelDoubleOccupancy { get; set; }
        /// <summary>
        /// Gets or sets the hotel and food request comments.
        /// </summary>
        /// <value>
        /// The hotel and food request comments.
        /// </value>
        private string HotelAndFoodRequestComments { get; set; }
        /// <summary>
        /// Retrieve the entity updated or created
        /// </summary>
        private MeetingRegistrationHotel TheEntity { get; set; }
        /// <summary>
        /// Hotel entity identifier
        /// </summary>
        private int? HotelId { get; set; }
        /// <summary>
        /// Gets or sets the cancellation date.
        /// </summary>
        /// <value>
        /// The cancellation date.
        /// </value>
        private Nullable<DateTime> CancellationDate { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the MeetingRegistrationHotel entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">MeetingRegistration entity</param>
        protected override void Populate(MeetingRegistrationHotel entity)
        {
            entity.Populate(this.HotelCheckInDate, this.HotelCheckOutDate, this.HotelNotRequiredFlag, this.HotelDoubleOccupancy, this.HotelId, this.HotelAndFoodRequestComments, this.CancellationDate);
        }
        /// <summary>
        /// Indicates if the UserApplicationComment has data.
        /// </summary>
        protected override bool HasData
        {
            get
            {
                //
                // By definition we are expecting that it will have data.  Even
                // null or the empty string should be stored.
                //
                return true;
            }
        }
        #endregion
        #region Optional Overrides
        /// <summary>
        /// Save the MeetingRegistrationHotel created.
        /// </summary>
        /// <param name="entity"></param>
        protected override void PostAdd(MeetingRegistrationHotel entity)
        {
            this.TheEntity = entity;
        }
        /// <summary>
        /// Save the MeetingRegistrationHotel modified.
        /// </summary>
        /// <param name="entity"></param>
        protected override void PostModify(MeetingRegistrationHotel entity)
        {
            this.TheEntity = entity;
        }
        #endregion
        #region Services
        /// <summary>
        /// Returns the MeetingRegistrationHotel created or updated.
        /// </summary>
        /// <returns>MeetingRegistrationTravel created or updated</returns>
        public MeetingRegistrationHotel GetEntity()
        {
            return this.TheEntity;
        }
        #endregion
    }
}
