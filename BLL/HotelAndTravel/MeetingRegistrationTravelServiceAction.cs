using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.HotelAndTravel;
using System;

namespace Sra.P2rmis.Bll.HotelAndTravel
{
    /// <summary>
    /// Service Action method for CRUD operations on MeetingRegistrationTravel 
    /// </summary>
    public class MeetingRegistrationTravelServiceAction : ServiceAction<MeetingRegistrationTravel>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public MeetingRegistrationTravelServiceAction()
        {
        }
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        /// <param name="model">Model containing information from the modal</param>
        public void Populate(ISessionAttendanceDetailsModel model, int? meetingRegistrationTravelId)
        {
            this.EntityIdentifier = meetingRegistrationTravelId ?? 0;
            this.TravelRequestComments = model.TravelRequestComments;
            this.TravelModelId = model.TravelModeId;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Travel special needs
        /// </summary>
        private string TravelRequestComments { get; set; }
        /// <summary>
        /// TravelMode entity identifier
        /// </summary>
        private int? TravelModelId { get; set; }
        /// <summary>
        /// Gets or sets the fare.
        /// </summary>
        /// <value>
        /// The fare.
        /// </value>
        private decimal? Fare { get; set; }
        /// <summary>
        /// Gets or sets the agent fee.
        /// </summary>
        /// <value>
        /// The agent fee.
        /// </value>
        private decimal? AgentFee { get; set; }
        /// <summary>
        /// Gets or sets the 2nd agent fee.
        /// </summary>
        /// <value>
        /// The 2nd agent fee.
        /// </value>
        private decimal? AgentFee2 { get; set; }
        /// <summary>
        /// Gets or sets the change fee.
        /// </summary>
        /// <value>
        /// The change fee.
        /// </value>
        private decimal? ChangeFee { get; set; }
        /// <summary>
        /// Gets or sets the nte amount.
        /// </summary>
        /// <value>
        /// The nte amount.
        /// </value>
        private decimal? NteAmount { get; set; }
        /// <summary>
        /// Gets or sets the gsa rate.
        /// </summary>
        /// <value>
        /// The gsa rate.
        /// </value>
        private decimal? GsaRate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether is ground.
        /// </summary>
        /// <value>
        ///   <c>true</c> if ground; otherwise, <c>false</c>.
        /// </value>
        private bool Ground { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [no gsa].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [no gsa]; otherwise, <c>false</c>.
        /// </value>
        private bool NoGsa { get; set; }
        /// <summary>
        /// Gets or sets the client approved amount.
        /// </summary>
        /// <value>
        /// The client approved amount.
        /// </value>
        private decimal? ClientApprovedAmount { get; set; }
        /// <summary>
        /// Gets or sets the cancellation date.
        /// </summary>
        /// <value>
        /// The cancellation date.
        /// </value>
        private DateTime? CancellationDate { get; set; }
        /// <summary>
        /// Retrieve the entity updated or created
        /// </summary>
        private MeetingRegistrationTravel TheEntity { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the MeetingRegistrationTravel entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">MeetingRegistrationTravel entity</param>
        protected override void Populate(MeetingRegistrationTravel entity)
        {
            entity.Populate(this.TravelModelId, this.Fare, this.AgentFee, this.AgentFee2, this.ChangeFee, this.Ground, this.NteAmount, this.GsaRate, this.NoGsa,
                this.ClientApprovedAmount, this.CancellationDate, this.TravelRequestComments);
        }
        /// <summary>
        /// Indicates if the ServiceAction has data.
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
        /// Save the MeetingRegistrationTravel created.
        /// </summary>
        /// <param name="entity"></param>
        protected override void PostAdd(MeetingRegistrationTravel entity)
        {
            this.TheEntity = entity;
        }
        /// <summary>
        /// Save the MeetingRegistrationTravel modified.
        /// </summary>
        /// <param name="entity"></param>
        protected override void PostModify(MeetingRegistrationTravel entity)
        {
            this.TheEntity = entity;
        }
        #endregion
        #region Services
        /// <summary>
        /// Returns the MeetingRegistrationTravel created or updated.
        /// </summary>
        /// <returns>MeetingRegistrationTravel created or updated</returns>
        public MeetingRegistrationTravel GetEntity()
        {
            return this.TheEntity;
        }
        #endregion
    }

}
