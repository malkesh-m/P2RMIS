using System;

namespace Sra.P2rmis.Bll.UserProfileManagement
{
    /// <summary>
    /// Results returned from a Reactivate or Deactivate request.
    /// </summary>
    public interface IReactivateDeactivateResult
    {
        /// <summary>
        /// Readable name of the user performing the action
        /// </summary>
        IUserNameResult NameResult { get; set; }
        /// <summary>
        /// Readable AccountStatus text.
        /// </summary>
        string Status { get; set; }
        /// <summary>
        /// Readable AccountStatusReason text.
        /// </summary>
        string Reason { get; set; }
        /// <summary>
        /// Entity identifier of result AccountStatus 
        /// </summary>
        int AccountStatusId { get; set; }
        /// <summary>
        /// Entity identifier of result AccountStatusReason
        /// </summary>
        int AccountStatusReasonId { get; set; }
        /// <summary>
        /// The profile type identifier for this user
        /// </summary>
        int ProfileTypeId { get; set; }
        /// <summary>
        /// The time the account status was last updated
        /// </summary>
        DateTime? AccountStatusDate { get; set; }
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
        void Populate(IUserNameResult nameResult, string status, string reason, int accountStatusId, int accountStatusReasonId, int profileTypeId, DateTime? accountStatusDate);
    }
}
