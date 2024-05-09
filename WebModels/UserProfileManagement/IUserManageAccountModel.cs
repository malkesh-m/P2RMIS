using System;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Data to populate the Manage Account modal window
    /// </summary>
    public interface IUserManageAccountModel
    {
        /// <summary>
        /// No fieldset
        /// </summary>
        /// <summary>
        /// User name
        /// </summary>
        string Username { get; set; }
        /// <summary>
        /// Last login date
        /// </summary>
        DateTime LastLogin { get; set; }
        /// <summary>
        /// The user's readable name
        /// </summary>
        string RoleName { get; set; }

        /// <summary>
        /// Account fieldset
        /// </summary>
        /// <summary>
        /// UserId
        /// </summary>
        int UserId { get; set; }
        /// <summary>
        /// Account status
        /// </summary>
        string Status { get; set; }
        /// <summary>
        /// Account status entity identifier
        /// </summary>
        int StatusId { get; set; }
        /// <summary>
        /// Account status reason
        /// </summary>
        string StatusReason { get; set; }
        /// <summary>
        /// Account status reason entity identifier
        /// </summary>
        int StatusReasonId { get; set; }
        /// <summary>
        /// Is the account locked
        /// </summary>
        bool IsLocked { get; set; }
        /// <summary>
        /// Date account status was updated
        /// </summary>
        DateTime? StatusDate { get; set; }
        /// <summary>
        /// Date account was updated
        /// </summary>
        DateTime? AccountUpdated { get; set; }
        /// <summary>
        /// First name of the user who updated the account
        /// </summary>
        string AccountUpdatedByFirstName { get; set; }
        /// <summary>
        /// Last name of the user who updated the account
        /// </summary>
        string AccountUpdatedByLastName { get; set; }

        /// <summary>
        /// Send credentials fieldset
        /// </summary>
        /// <summary>
        /// Date credentials were sent
        /// </summary>
        DateTime? Sent { get; set; }
        /// <summary>
        /// First name of the user who sent the credentials
        /// </summary>
        string SentByFirstName { get; set; }
        /// <summary>
        /// Last name of the user who sent the credentials
        /// </summary>
        string SentByLastName { get; set; }

        /// <summary>
        /// Profile fieldset
        /// </summary>
        /// <summary>
        /// Date account status was created
        /// </summary>
        DateTime? Created { get; set; }
        /// <summary>
        /// First name of the user who created the profile
        /// </summary>
        string CreatedByFirstName { get; set; }
        /// <summary>
        /// Last name of the user who created the profile
        /// </summary>
        string CreatedByLastName { get; set; }
        /// <summary>
        /// Date profile status was updated
        /// </summary>
        DateTime? ProfileUpdated { get; set; }
        /// <summary>
        /// First name of the user who updated the profile
        /// </summary>
        string ProfileUpdatedByLastName { get; set; }
        /// <summary>
        /// Last name of the user who updated the profile
        /// </summary>
        string ProfileUpdatedByFirstName { get; set; }
        /// <summary>
        /// Preferred email address
        /// </summary>
        string PrefEmail { get; set; }

        /// <summary>
        /// Locked fieldset
        /// </summary>
        /// <summary>
        /// Indicates if the account is locked
        /// </summary>
        bool Locked { get; set; }
        /// <summary>
        /// Date account was locked
        /// </summary>
        DateTime? LockedDate { get; set; }
        /// <summary>
        /// Date account was unlocked
        /// </summary>
        DateTime? UnLockedDate { get; set; }
        /// <summary>
        /// First name of the user who unlocked the account
        /// </summary>
        string UnLockedByFirstName { get; set; }
        /// <summary>
        /// Last name of the user who unlocked the account
        /// </summary>
        string UnLockedByLastName { get; set; }
        /// <summary>
        /// Show the send credential button
        /// </summary>
        bool ShowSendCredentialButton { get; set; }
        /// <summary>
        /// Populate model properties that contain data for the fields not contained in field sets.
        /// </summary>
        /// <param name="userLogin">Username</param>
        /// <param name="lastLogin">Date/time user last logged in</param>
        void PopulateNonFieldsetData(string userLogin, DateTime lastLogin);
        /// <summary>
        /// Populate model properties for the Account field sets.
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="status">Account status</param>
        /// <param name="statusReason">Account status reason</param>
        /// <param name="statusDate">Date status was modified</param>
        /// <param name="accountUpdated">Date account was updated</param>
        /// <param name="accountUpdatedByFirstName">First name of user who modified account</param>
        /// <param name="accountUpdatedByLastName">Last name of user who modified account</param>
        /// <param name="isLocked">Indicates if the user account is locked</param>
        /// <param name="activeStatusidentifier">Entity identifier for ActiveStatus</param>
        /// <param name="inactiveStatusIdentifier">Entity identifier for InactiveStatus</param>
        /// <param name="inactiveReasonList">Comma separated list of inactive status reasons that the Activate button is enabled.</param>
        void PopulateAccount(int userId, int statusId, string status, int accountStatusReasonId, string statusReason, DateTime? statusDate, DateTime? accountUpdated, string accountUpdatedByFirstName, string accountUpdatedByLastName, bool isLocked, int activeStatusidentifier, int inactiveStatusIdentifier, string inactiveReasonList);
        /// <summary>
        /// Populate model properties for the Send Credentials field sets.
        /// </summary>
        /// <param name="credentialSentDate">Date credentials were sent</param>
        /// <param name="sentByFirstName">First name of user who sent credentials</param>
        /// <param name="sentByLastName">Last name of user who sent credentials</param>
        void PopulateSendCredentials(DateTime? credentialSentDate, string sentByFirstName, string sentByLastName);
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
        /// <param name="accoountStatusDate">Account status date</param>
        void PopulateSendCredentials(DateTime? credentialSentDate, string sentByFirstName, string sentByLastName, bool isLocked, string status, int statusId, string statusReason, int statusReasonId, DateTime? accoountStatusDate);
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
        void PopulateProfile(DateTime? created, string createdByFirstName, string createdByLastName, DateTime? modifiedDate, string modifiedByFirstName, string modifiedByLastName, string prefEmail);
        /// <summary>
        /// Populate model properties for the Lockout field sets.
        /// </summary>
        /// <param name="isLocked">Indicator if the account is locked</param>
        /// <param name="lockedDate">Indicates the account is locked out</param>
        /// <param name="unlocked">Indicates the account is locked out</param>
        /// <param name="firstName">First name of user who unlocked the user</param>
        /// <param name="lastName">Last name of user who unlocked out the user</param>
        void PopulateLockout(bool isLocked, DateTime? lockedDate, DateTime? unlocked, string firstName, string lastName);
        /// <summary>
        /// Populate the model properties for the Lockout field sets.
        /// </summary>
        /// <param name="lockedDate">The date the user was locked out</param>
        void PopulateLocked(DateTime? lockedDate);
        /// <summary>
        /// Populate the model properties for the user unlocked out field sets
        /// </summary>
        /// <param name="unlockedDate">The date the user was unlocked</param>
        /// <param name="firstName">The first name of the user who unlocked the locked out user</param>
        /// <param name="lastName">The last name of the user who unlocked the locked out user</param>
        void PopulateUnlock(DateTime? unlockedDate, string firstName, string lastName);

    }
}
