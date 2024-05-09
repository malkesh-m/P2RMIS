using System;

namespace Sra.P2rmis.Bll.UserProfileManagement
{
    /// <summary>
    /// Results returned from a Reactivate or Deactivate request.
    /// </summary>
    public class ReactivateDeactivateResult : IReactivateDeactivateResult
    {
        #region Attributes
        /// <summary>
        /// Readable name of the user performing the action
        /// </summary>
        public IUserNameResult NameResult { get; set; }
        /// <summary>
        /// Readable AccountStatus text.
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Readable AccountStatusReason text.
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// Entity identifier of result AccountStatus 
        /// </summary>
        public int AccountStatusId { get; set; }
        /// <summary>
        /// Entity identifier of result AccountStatusReason
        /// </summary>
        public int AccountStatusReasonId { get; set; }
        /// <summary>
        /// The profile type identifier for this user
        /// </summary>
        public int ProfileTypeId { get; set; }
        /// <summary>
        /// The time the account status was last updated
        /// </summary>
        public DateTime? AccountStatusDate { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// Populates the returned model
        /// </summary>
        /// <param name="nameResult">Readable name of the user performing the action</param>
        /// <param name="status">Readable AccountStatus text.</param>
        /// <param name="reason">Readable AccountStatusReason text.</param>
        /// <param name="accountStatusId">Entity identifier of result AccountStatus</param>
        /// <param name="accountStatusReasonId">Entity identifier of result AccountStatusReason</param>
        /// <param name="profileTypeId">Entity identifier of the profile type</param>
        /// <param name="statusDate">Date the account status last changed</param>
        public void Populate(IUserNameResult nameResult, string status, string reason, int accountStatusId, int accountStatusReasonId, int profileTypeId, DateTime? accountStatusDate)
        {
            this.NameResult = nameResult;
            this.Status = status;
            this.Reason = reason;
            this.AccountStatusId = accountStatusId;
            this.AccountStatusReasonId = accountStatusReasonId;
            this.ProfileTypeId = profileTypeId;
            this.AccountStatusDate = accountStatusDate;
        }
        #endregion


    }
}
