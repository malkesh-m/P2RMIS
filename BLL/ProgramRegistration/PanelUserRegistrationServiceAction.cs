using System;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.ProgramRegistration
{
    /// <summary>
    /// Service action to update the Registration Start & End dates.
    /// </summary>
    public class PanelUserRegistrationServiceAction : ServiceAction<PanelUserRegistration>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public PanelUserRegistrationServiceAction() {}
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        /// <param name="registrationStartDate">Registration start date</param>
        /// <param name="registrationCompletedDate">Registration end date</param>
        public void Populate(DateTime? registrationStartDate, DateTime? registrationCompletedDate)
        {
            this.RegistrationStartDate = registrationStartDate;
            this.RegistrationCompletedDate = registrationCompletedDate;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Registration start date
        /// </summary>
        private DateTime? RegistrationStartDate { get; set; }
        /// <summary>
        /// Registration end date
        /// </summary>
        private DateTime? RegistrationCompletedDate { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Indicates if the PanelUserRegistration has data.
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
        /// <summary>
        /// Indicates if the data represents a delete.
        /// </summary>
        protected override bool IsDelete { get { return false; } }
        /// <summary>
        /// Indicates if the data represents an add.
        /// </summary>
        public override bool IsAdd { get { return false; } }
        /// <summary>
        /// Populate the PanelUserRegistrationDocument entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">The entity to populate</param>
        protected override void Populate(PanelUserRegistration entity)
        {
            entity.RegistrationStartDate = this.RegistrationStartDate;
            entity.RegistrationCompletedDate = this.RegistrationCompletedDate;
        }
        #endregion
    }
}
