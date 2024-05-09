using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.ProgramRegistration;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for participation history
    /// </summary>
    public class ParticipationHistoryViewModel : UserProfileManagementViewModel
    {
        #region Constants
        /// <summary>
        /// Index postion of the registration wizard contract tab
        /// </summary>
        public readonly int ContractualTabIndex = 3;
        /// <summary>
        /// Default starting tab of the registration wizard
        /// </summary>
        public readonly int DefaultTabIndex = 1;
        #endregion

        #region Constructor
        public ParticipationHistoryViewModel()
        {
            this.ParticipationHistory = new List<IUserParticipationHistoryModel>();
            this.IsMyProfile = false;
            this.LastPageUrl = string.Empty;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Collection of the user's participation history
        /// </summary>
        public List<IUserParticipationHistoryModel> ParticipationHistory { get; set; }

        /// <summary>
        /// Does this user have permanent credentials
        /// </summary>
        public bool PermanentCredentials { get; set; }

        /// <summary>
        /// The date a user's participation was last updated
        /// </summary>
        public string ParticipationLastUpdateDate
        {
            get { return LastUpdateDateFormatter(ParticipationHistory.Max(x => x.ModifiedDate)); }
        }
        /// <summary>
        /// Whether the profile is coming from the my profile section
        /// </summary>
        public bool IsMyProfile { get; set; }
        /// <summary>
        /// User profile identifier
        /// </summary>
        public int UserInfoId { get; set; }
        /// <summary>
        /// Whether the user can manage their password
        /// </summary>
        public bool CanManagePassword { get; set; }
        /// <summary>
        /// Indicates if a status is valid for this panel.
        /// </summary>
        /// <param name="panelAbbreviation">Panel Abbreviation</param>
        /// <returns>True if the Panel Abbreviation is supplied; false otherwise</returns>
        public bool IsRegistrationStateValid(string panelAbbreviation)
        {
            return ProgramRegistrationService.IsRegistrationStateValid(panelAbbreviation);
        }
        /// <summary>
        /// Indicates if a registrations is not started
        /// </summary>
        /// <param name="registrationStartDate">Registration start date</param>
        /// <returns>True if the registration has not started.</returns>
        public bool IsRegistrationNotStarted(DateTime? registrationStartDate)
        {
            return ProgramRegistrationService.IsRegistrationNotStarted(registrationStartDate);
        }
        /// <summary>
        /// Indicates if a registrations is not complete
        /// </summary>
        /// <param name="registrationStartDate">Registration start date</param>
        /// <param name="registrationCompletionDate">Registration completion date</param>
        /// <returns>True if the registration has not completed.</returns>
        public bool IsRegistrationContinued(DateTime? registrationStartDate, DateTime? registrationCompletionDate)
        {
            return ProgramRegistrationService.IsRegistrationContinued(registrationStartDate, registrationCompletionDate);
        }
        /// <summary>
        /// Indicates if a registrations is complete
        /// </summary>
        /// <param name="registrationStartDate">Registration start date</param>
        /// <param name="registrationCompletionDate">Registration completion date</param>
        /// <returns>True if the registration has completed.</returns>
        public bool IsRegistrationCompleted(DateTime? registrationCompletionDate)
        {
            return !ProgramRegistrationService.IsRegistrationNotCompleted(registrationCompletionDate);
        }
        /// <summary>
        /// Last page's URL
        /// </summary>
        public string LastPageUrl { get; set; }
        #endregion
        #region Helpers
        /// <summary>
        /// Formats string for last update date
        /// </summary>
        /// <param name="dateToFormat">Date that will be formatted</param>
        /// <returns>String representation of the last update date</returns>
        private string LastUpdateDateFormatter(DateTime? dateToFormat)
        {
            return dateToFormat == null
                ? String.Empty
                : dateToFormat.Value.ToString("MM/dd/yyyy hh:mm:ss tt");
        }
        /// <summary>
        /// Formats a Nullable DateTime as a Date string
        /// </summary>
        /// <param name="dateToFormat"></param>
        /// <returns>Date string representation</returns>
        public string PanelEndDateFormatter(DateTime? dateToFormat)
        {
            return dateToFormat == null
                ? String.Empty
                : dateToFormat.Value.Date.ToString("MM/dd/yyyy");
        }
        
        #endregion
    }
}