using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.HotelAndTravel;

namespace Sra.P2rmis.Bll.HotelAndTravel
{
    /// <summary>
    /// Service Action method for CRUD operations on MeetingRegistration
    /// </summary>
    public class MeetingRegistrationServiceAction: ServiceAction<MeetingRegistration>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public MeetingRegistrationServiceAction() { }
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        /// <param name="model">Model containing information from the modal</param>
        public void Populate(ISessionAttendanceDetailsModel model, bool registrationSubmittedFlag)
        {
            this.EntityIdentifier = model.MeetingRegistrationId ?? 0;
            this.RegistrationSubmittedFlag = registrationSubmittedFlag;
            this.PanelUserAssignmentId = model.PanelUserAssignmentId.Value;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Is the registration being submitted.
        /// </summary>
        private bool RegistrationSubmittedFlag { get; set; }
        /// <summary>
        /// PanelUserAssignment entity identifier
        /// </summary>
        private int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Retrieve the entity updated or created
        /// </summary>
        private MeetingRegistration TheEntity { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the MeetingRegistration entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">MeetingRegistration entity</param>
        protected override void Populate(MeetingRegistration entity)
        {
            entity.Populate(this.PanelUserAssignmentId, this.RegistrationSubmittedFlag);
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
        /// Save the MeetingRegistration created.
        /// </summary>
        /// <param name="entity"></param>
        protected override void PostAdd(MeetingRegistration entity)
        {
            this.TheEntity = entity;
        }
        /// <summary>
        /// Save the MeetingRegistration modified.
        /// </summary>
        /// <param name="entity"></param>
        protected override void PostModify(MeetingRegistration entity)
        {
            this.TheEntity = entity;
        }
        #endregion
        #region Services
        /// <summary>
        /// Returns the MeetingRegistration created or updated.
        /// </summary>
        /// <returns>MeetingRegistration created or updated</returns>
        public MeetingRegistration GetEntity()
        {
            return this.TheEntity;
        }
        #endregion
    }
}
