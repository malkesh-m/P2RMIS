using System;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.Lists;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Data to populate the Manage Account modal window
    /// </summary>
    public class UserManageAccountModel: IUserManageAccountModel
    {
        /// <summary>
        /// No fieldset
        /// </summary>
        public string Username { get; set; }
        public DateTime LastLogin { get; set; }
        public string RoleName { get; set; }

        /// <summary>
        /// Account fieldset
        /// </summary>
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public int StatusReasonId { get; set; }
        public string StatusReason { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? AccountUpdated { get; set; }
        public string AccountUpdatedByFirstName { get; set; }
        public string AccountUpdatedByLastName { get; set; }
        //
        // Data values need for java script functions
        //
        public int ActiveStatusIdentifer { get; set; }
        public int InactiveStatusIdentifer { get; set; }
        public string InactiveReasonIdentifers { get; set; }

        /// <summary>
        /// Send credentials fieldset
        /// </summary>
        public DateTime? Sent { get; set; }
        public string SentByFirstName { get; set; }
        public string SentByLastName { get; set; }

        /// <summary>
        /// Profile fieldset
        /// </summary>
        public DateTime? Created { get; set; }
        public string CreatedByFirstName { get; set; }
        public string CreatedByLastName { get; set; }
        public DateTime? ProfileUpdated { get; set; }
        public string ProfileUpdatedByLastName { get; set; }
        public string ProfileUpdatedByFirstName { get; set; }
        public string PrefEmail { get; set; }

        /// <summary>
        /// Locked fieldset
        /// </summary>
        public bool Locked { get; set; }


        public DateTime? LockedDate { get; set; }
        public DateTime? UnLockedDate { get; set; }
        public string UnLockedByFirstName { get; set; }
        public string UnLockedByLastName { get; set; }
        /// <summary>
        /// De-Activate drop down
        /// </summary>
        public IEnumerable<IListEntry> DeactivateAccountDropdown { get; set; }
        /// <summary>
        /// Selected reason why the account was deactivated
        /// </summary>
        public int? DeactivatedReason { get; set; }
        /// <summary>
        /// Show the send credential button
        /// </summary>
        public bool ShowSendCredentialButton { get; set; }
        /// <summary>
        /// Show the send credential modal 
        /// </summary>
        public bool ShowSendCredentialsModal { get; set; }
        #region Helpers
        /// <summary>
        /// Populate model properties that contain data for the fields not contained in field sets.
        /// </summary>
        /// <param name="userLogin">User name</param>
        /// <param name="lastLogin">Date/time user last logged in</param>
        public void PopulateNonFieldsetData(string userLogin, DateTime lastLogin)
        {
            this.Username = userLogin;
            this.LastLogin = lastLogin;
        }
        /// <summary>
        /// Populate model properties for the Account field sets.
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="statusId">Status entity identifier</param>
        /// <param name="status">Account status</param>
        /// <param name="accountStatusReasonId">AccountStatusReason entity identifier</param>
        /// <param name="statusReason">Account status reason</param>
        /// <param name="statusDate">Date status was modified</param>
        /// <param name="accountUpdated">Date account was updated</param>
        /// <param name="accountUpdatedByFirstName">First name of user who modified account</param>
        /// <param name="accountUpdatedByLastName">Last name of user who modified account</param>
        /// <param name="isLocked">Indicates if the user account is locked</param>
        /// <param name="activeStatusidentifier">Entity identifier for ActiveStatus</param>
        /// <param name="inactiveStatusIdentifier">Entity identifier for InactiveStatus</param>
        /// <param name="inactiveReasonList">Comma separated list of inactive status reasons that the Activate button is enabled.</param>
        public void PopulateAccount(int userId, int statusId, string status, int accountStatusReasonId, string statusReason, DateTime? statusDate, DateTime? accountUpdated, string accountUpdatedByFirstName, string accountUpdatedByLastName, bool isLocked, int activeStatusidentifier, int inactiveStatusIdentifier, string inactiveReasonList)
        {
            this.UserId = userId;
            this.StatusId = statusId;
            this.Status = status;
            this.StatusReasonId = accountStatusReasonId;
            this.StatusReason = statusReason;
            this.StatusDate = statusDate;
            this.AccountUpdated = accountUpdated;
            this.AccountUpdatedByFirstName = accountUpdatedByFirstName;
            this.AccountUpdatedByLastName = accountUpdatedByLastName;
            this.IsLocked = isLocked;
            this.ActiveStatusIdentifer = activeStatusidentifier;
            this.InactiveStatusIdentifer = inactiveStatusIdentifier;
            this.InactiveReasonIdentifers = inactiveReasonList;
        }
        /// <summary>
        /// Populate model properties for the Send Credentials field sets.
        /// </summary>
        /// <param name="credentialSentDate">Date credentials were sent</param>
        /// <param name="sentByFirstName">First name of user who sent credentials</param>
        /// <param name="sentByLastName">Last name of user who sent credentials</param>
        public void PopulateSendCredentials(DateTime? credentialSentDate, string sentByFirstName, string sentByLastName)
        {
            this.Sent = credentialSentDate;
            this.SentByFirstName = sentByFirstName;
            this.SentByLastName = sentByLastName;
        }
        /// <summary>
        /// Populate model properties for the Send Credentials field sets.
        /// </summary>
        /// <param name="credentialSentDate">Date credentials were sent</param>
        /// <param name="sentByFirstName">First name of user who sent credentials</param>
        /// <param name="sentByLastName">Last name of user who sent credentials</param>
        /// <param name="isLocked">Indicates if the user was locked out</param>
        /// <param name="status">Account status</param>
        /// <param name="statusId">Status entity identifier</param>
        /// <param name="statusReason">Account status reason</param>
        /// <param name="statusReasonId">AccountStatusReason entity identifier</param>
        /// <param name="accountStatusDate">Account Status Date</param>
        public void PopulateSendCredentials(DateTime? credentialSentDate, string sentByFirstName, string sentByLastName, bool isLocked, string status, int statusId, string statusReason, int statusReasonId, DateTime? accountStatusDate)
        {
            PopulateSendCredentials(credentialSentDate, sentByFirstName, sentByLastName);
            IsLocked = isLocked;
            Status = status;
            StatusId = statusId;
            StatusReason = statusReason;
            StatusReasonId = statusReasonId;
            StatusDate = accountStatusDate;
        }
        /// <summary>
        /// Populate model properties for the Profile field sets.
        /// </summary>
        /// <param name="created">Date profile was created</param>
        /// <param name="createdByFirstName">First name of user who created profile</param>
        /// <param name="createdByLastName">Last name of user who created profile</param>
        /// <param name="modifiedDate">Date profile was modified</param>
        /// <param name="modifiedByFirstName">>First name of user who created profile</param>
        /// <param name="modifiedByLastName">>Last name of user who created profile</param>
        /// <param name="prefEmail">Preferred email address</param>
        public void PopulateProfile(DateTime? created, string createdByFirstName, string createdByLastName, DateTime? modifiedDate, string modifiedByFirstName, string modifiedByLastName, string prefEmail)
        {
            this.Created = created;
            this.CreatedByFirstName = createdByFirstName;
            this.CreatedByLastName = createdByLastName;
            this.ProfileUpdated = modifiedDate;
            this.ProfileUpdatedByFirstName = modifiedByFirstName;
            this.ProfileUpdatedByLastName = modifiedByLastName;
            this.PrefEmail = prefEmail;
        }


        /// <summary>
        /// Populate model properties for the Lockout field sets.
        /// </summary>
        /// <param name="isLocked">Indicator if the account is locked</param>
        /// <param name="lockedDate">Indicates the date account is locked out</param>
        /// <param name="unlockedDate">Indicates the date account is unlocked</param>
        /// <param name="firstName">First name of user who unlocked the user</param>
        /// <param name="lastName">Last name of user who unlocked out the user</param>
        public void PopulateLockout(bool isLocked, DateTime? lockedDate, DateTime? unlockedDate, string firstName, string lastName)
        {
            this.Locked = isLocked;
            this.LockedDate = isLocked? lockedDate : null;
            this.UnLockedDate = isLocked ? null : unlockedDate;
            this.UnLockedByFirstName = firstName;
            this.UnLockedByLastName = lastName;
        }
        /// <summary>
        /// Populate the model properties for the Lockout field sets.
        /// </summary>
        /// <param name="lockedDate">The date the user was locked out</param>
        public void PopulateLocked(DateTime? lockedDate)
        {
            PopulateLockout(true, lockedDate, null, string.Empty, string.Empty);
        }
        /// <summary>
        /// Populate the model properties for the user unlocked out field sets
        /// </summary>
        /// <param name="unlockedDate">The date the user was unlocked</param>
        /// <param name="firstName">The first name of the user who unlocked the locked out user</param>
        /// <param name="lastName">The last name of the user who unlocked the locked out user</param>
        public void PopulateUnlock(DateTime? unlockedDate, string firstName, string lastName)
        {
            PopulateLockout(false, null, unlockedDate, firstName, lastName);
        }
        #endregion
    }
}
