using System;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.HotelAndTravel;

namespace Sra.P2rmis.Bll.HotelAndTravel
{
    /// <summary>
    /// Service Action method for CRUD operations on MeetingRegistrationAttendance
    /// </summary>
    public class MeetingRegistrationAttendanceServiceAction : ServiceAction<MeetingRegistrationAttendance>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public MeetingRegistrationAttendanceServiceAction()
        {
        }
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        /// <param name="model">Model containing information from the modal</param>
        public void Populate(ISessionAttendanceDetailsModel model, int? meetingRegistrationAttandanceId)
        {
            this.EntityIdentifier = meetingRegistrationAttandanceId ?? 0;
            this.AttendanceStartDate = model.AttendanceStartDate;
            this.AttendanceEndDate = model.AttendanceEndDate;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Attendance start date
        /// </summary>
        private Nullable<DateTime> AttendanceStartDate { get; set; }
        /// <summary>
        /// Attendance end date
        /// </summary>
        private Nullable<DateTime> AttendanceEndDate { get; set; }
        /// <summary>
        /// Meals special needs.
        /// </summary>
        private string HotelRequestComments { get; set; }
        /// <summary>
        /// Retrieve the entity updated or created
        /// </summary>
        private MeetingRegistrationAttendance TheEntity { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the MeetingRegistrationAttendance entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">MeetingRegistration entity</param>
        protected override void Populate(MeetingRegistrationAttendance entity)
        {
            entity.Populate(this.AttendanceStartDate, this.AttendanceEndDate);
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
        /// Save the MeetingRegistrationAttendance created.
        /// </summary>
        /// <param name="entity">MeetingRegistrationAttendance entity</param>
        protected override void PostAdd(MeetingRegistrationAttendance entity)
        {
            this.TheEntity = entity;
        }
        /// <summary>
        /// Save the MeetingRegistrationAttendance modified.
        /// </summary>
        /// <param name="entity">MeetingRegistrationAttendance entity</param>
        protected override void PostModify(MeetingRegistrationAttendance entity)
        {
            this.TheEntity = entity;
        }
        #endregion
        #region Services
        /// <summary>
        /// Returns the MeetingRegistrationAttendance created or updated.
        /// </summary>
        /// <returns>MeetingRegistrationAttendance created or updated</returns>
        public MeetingRegistrationAttendance GetEntity()
        {
            return this.TheEntity;
        }
        #endregion
    }

}
