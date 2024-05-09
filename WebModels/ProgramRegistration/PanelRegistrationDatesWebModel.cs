using System;

namespace Sra.P2rmis.WebModels.ProgramRegistration
{
    /// <summary>
    /// Container to return a user's registration start & stop dates.
    /// </summary>
    public interface IPanelRegistrationDatesWebModel
    {
        /// <summary>
        /// Registration start date
        /// </summary>
        DateTime? RegistrationStartDate { get; }
        /// <summary>
        /// Registration complete date
        /// </summary>
        DateTime? RegistrationCompletedDate { get; }
    }
    /// <summary>
    /// Container to return a user's registration start & stop dates.
    /// </summary>
    public class PanelRegistrationDatesWebModel : IPanelRegistrationDatesWebModel
    {
        #region Attributes
        /// <summary>
        /// Registration start date
        /// </summary>
        public DateTime? RegistrationStartDate { get; private set; }
        /// <summary>
        /// Registration complete date
        /// </summary>
        public DateTime? RegistrationCompletedDate { get; private set; }
        #endregion
        #region Constructor & Set up
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startDate">Registration start date</param>
        /// <param name="completionDate">Registration completion date</param>
        public PanelRegistrationDatesWebModel(DateTime? startDate, DateTime? completionDate)
        {
            this.RegistrationStartDate = startDate;
            this.RegistrationCompletedDate = completionDate;
        }
        #endregion
    }
}
