using System;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.Common
{
    public class ManageAccountStatus
    {
        /// <summary>
        /// Resulting AccountStatus for an Activate or Deactivate command.
        /// </summary>
        public int AccountStatusId { get; set; }
        /// <summary>
        /// Resulting AccountStatusReason for an Activate or Deactivate command.
        /// </summary>
        public int AccountStatusReasonId { get; set; }
        /// <summary>
        /// Readable account status value
        /// </summary>
        public string ReadableAccountStatus { get; set; }
        /// <summary>
        /// Indicates if the Send Credential button should be displayed
        /// </summary>
        public bool EnableSendCredentialButton { get; set; }
        /// <summary>
        /// Indicates if the request was successful
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Date Credentials were successfully sent
        /// </summary>
        public DateTime SentDate { get; set; }
        /// <summary>
        /// First name of user that sent the credentials
        /// </summary>
        public string SentByFirstName { get; set; }
        /// <summary>
        /// Last name of user that sent the credentials
        /// </summary>
        public string SentByLastName { get; set; }
        /// <summary>
        /// The new account status name
        /// </summary>
        public string AccountStatusName { get; set; }
        /// <summary>
        /// Indicates if the account is locked
        /// </summary>
        public bool IsLocked { get; set; }
        /// <summary>
        /// The profile type identifier for this user
        /// </summary>
        public int ProfileTypeId { get; set; }
        /// <summary>
        /// The date the account status was last changed
        /// </summary>
        public DateTime? AccountStatusDate { get; set; }
        /// <summary>
        /// The success message associated with the requested manage acccount action
        /// </summary>
        public string ActionSuccessMessage { get; set; }
        /// <summary>
        /// Wrapper to format the credential Sent By user name
        /// </summary>
        public string SentByName
        {
            get { return ViewHelpers.ConstructShortName(SentByFirstName, SentByLastName); }
        }
        /// <summary>
        /// Wrapper to format the new status after Deactivation or Activation.
        /// </summary>
        public string ManageAccountAccountStatus
        {
            get { return ViewHelpers.FormatStatus(this.AccountStatusName, this.ReadableAccountStatus); }
        }
        public string FormattedAccountStatusDate
        {
            get { return ViewHelpers.FormatLastUpdateDateTime(AccountStatusDate); }
        }
        /// <summary>
        /// Return the SentDate as UTC
        /// </summary>
        public string SentDateAsUTC
        {
            get { return ViewHelpers.FormatDateTimeAsUtc(SentDate); }
        }
        /// <summary>
        /// Returns a formated value of the Sent Date
        /// </summary>
        public string FormattedSentDate
        {
            get { return ViewHelpers.FormatLastUpdateDateTime(this.SentDate); }
        }
        /// <summary>
        /// Wrapper to format the credential Sent By date
        /// </summary>
        public string SentByDate
        {
            get { return ViewHelpers.FormatLastUpdateDateTime(SentDate); }
        }
        /// <summary>
        /// Populates the container with credential information
        /// </summary>
        /// <param name="sentDate">Date credentials were sent</param>
        /// <param name="firstName">First name of user who sent credentials</param>
        /// <param name="lastName">Last name of user who sent credentials</param>
        public void Populate(DateTime sentDate, string firstName, string lastName)
        {
            this.SentDate = sentDate;
            this.SentByFirstName = firstName;
            this.SentByLastName = lastName;
        }
        /// <summary>
        /// Set the status values Deactivate command;
        /// </summary>
        /// <param name="accountStatusId">AccountStatus entity identifier</param>
        /// <param name="accountStatusReasonid">AccountStatusReason entity identifier</param>
        /// <param name="status">status value</param>
        /// <param name="readableAccountStatus">Readable status value</param>
        /// <param name="readableAccountStatusReason">Readable reason value</param>
        /// <param name="accountStatusDate">Account status date</param>
        public void SetStatus(int accountStatusId, int accountStatusReasonid, bool status, string readableAccountStatus, string readableAccountStatusReason, DateTime? accountStatusDate)
        {
            this.AccountStatusReasonId = accountStatusReasonid;
            this.AccountStatusId = accountStatusId;
            this.Status = status;
            this.AccountStatusName = readableAccountStatus;
            this.ReadableAccountStatus = readableAccountStatusReason;
            this.AccountStatusDate = accountStatusDate;
        }
        /// <summary>
        /// Set the profile type identifier
        /// </summary>
        /// <param name="profileTypeId">The progfile type identifier</param>
        public void SetProfileType(int profileTypeId)
        {
            this.ProfileTypeId = profileTypeId;
        }
    }
}