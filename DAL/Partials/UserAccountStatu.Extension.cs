using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's UserAcountStatu object.
    /// </summary>
    public partial class UserAccountStatu : IStandardDateFields, ISpecifyEntitySetName
    {
        /// <summary>
        /// Populates a new or existing UserAccountStatu object in preparation for addition or update to the repository.
        /// </summary>
        /// <param name="accountStatusId">Account status identifier</param>
        /// <param name="accountStatusReasonId">Account reason identifier</param>
        /// <param name="userToChangeId">The user identifier of the user whose status is changing</param>
        /// <param name="userId">The user identifier of the user making the change</param>
        public void Populate(int accountStatusId, int accountStatusReasonId, int userToChangeId, int userId)
        {
            Populate(accountStatusId, accountStatusReasonId); 
            this.UserId = userToChangeId;
        }
        /// <summary>
        /// Populates the UserAccountStatu object
        /// </summary>
        /// <param name="AccountStatusId">AccountStatus entity identifier</param>
        /// <param name="accountStatusReasonId">AccountStatusReason entity identifier</param>
        public void Populate(int AccountStatusId, int accountStatusReasonId)
        {
            this.AccountStatusId = AccountStatusId;
            this.AccountStatusReasonId = accountStatusReasonId;
        }
        /// <summary>
        /// Populates the entity with the status, reason, update and create dates
        /// </summary>
        /// <param name="accountStatusReasonId">The user account status reason id</param>
        /// <param name="userToChangeId"></param>
        /// <param name="userId"></param>
        public void CreateUserAccountStatus(int accountStatusReasonId, int userToChangeId, int userId)
        {
            // note: UserAccountStatusChangeLog is updated by the database triggers
            switch (accountStatusReasonId)
            {
                case AccountStatusReason.Indexes.TmpPwd:
                case AccountStatusReason.Indexes.PermCredentials:
                    Populate(AccountStatu.Indexes.Active, accountStatusReasonId, userToChangeId, userId);
                    break;
                case AccountStatusReason.Indexes.AccountClosed:
                case AccountStatusReason.Indexes.AwaitingCredentials:
                case AccountStatusReason.Indexes.Inactivity:
                case AccountStatusReason.Indexes.Ineligible:
                case AccountStatusReason.Indexes.Locked:
                case AccountStatusReason.Indexes.PwdExpired:
                    Populate(AccountStatu.Indexes.Inactive, accountStatusReasonId, userToChangeId, userId);
                    break;
            }
            Helper.UpdateModifiedFields(this, userId);
            Helper.UpdateCreatedFields(this, userId);
        }
        /// <summary>
        /// Populates the entity with the status, reason, and update dates
        /// </summary>
        /// <param name="accountStatusReasonId">The user account status reason id</param>
        /// <param name="userToChangeId"></param>
        /// <param name="userId"></param>
        public void UpdateUserAccountStatus(int accountStatusReasonId, int userToChangeId, int userId)
        {
            // note: UserAccountStatusChangeLog is updated by the database triggers
            switch (accountStatusReasonId)
            {
                case AccountStatusReason.Indexes.TmpPwd:
                case AccountStatusReason.Indexes.PermCredentials:
                    Populate(AccountStatu.Indexes.Active, accountStatusReasonId, userToChangeId, userId);
                    break;
                case AccountStatusReason.Indexes.AccountClosed:
                case AccountStatusReason.Indexes.AwaitingCredentials:
                case AccountStatusReason.Indexes.Inactivity:
                case AccountStatusReason.Indexes.Ineligible:
                case AccountStatusReason.Indexes.Locked:
                case AccountStatusReason.Indexes.PwdExpired:
                    Populate(AccountStatu.Indexes.Inactive, accountStatusReasonId, userToChangeId, userId);
                    break;
            }
            Helper.UpdateModifiedFields(this, userId);
        }
        /// <summary>
        /// Populates this entity with the status, reason, and update dates
        /// for this user and by this user
        /// </summary>
        /// <param name="accountStatusReasonId">The user account status reason id</param>
        public void UpdateUserAccountStatus(int accountStatusReasonId)
        {
            UpdateUserAccountStatus(accountStatusReasonId, this.UserId, this.UserId);
        }

        /// <summary>
        /// Calls Populate to set Locked state in the UserAccountStatu entity object
        /// </summary>
        /// <param name="userToChangeId">The user identifier of the user whose status is changing</param>
        /// <param name="userId">The user identifier of the user making the change</param>
        public void Lock(int userToChangeId, int userId)
        {
            Populate(AccountStatu.Indexes.Inactive, AccountStatusReason.Indexes.Locked, userToChangeId, userId);
            // this.UserAccountStatusId will be 0 for new objects which will cause create as well as modify fields to be updated 
            Helper.UpdateTimeFields(this, this.UserAccountStatusId, userId);
        }
        /// <summary>
        /// Set status to Active Temporary Password issued
        /// </summary>
        /// <param name="newReason">The new status reason</param>
        /// <param name="newStatus">The new status</param>
        /// <param name="userId">The user identifier of the user making the change</param>
        public void Unlock(int newReason, int newStatus, int userId)
        {
            Populate(newStatus, newReason);
            Helper.UpdateModifiedFields(this, userId);
        }
        /// <summary>
        /// Set status to Active Temporary Password issued
        /// </summary>
        /// <param name="userId">The user identifier of the user making the change</param>
        public void TempPassword(int userId)
        {
            Populate(AccountStatu.Indexes.Active, AccountStatusReason.Indexes.TmpPwd);
            Helper.UpdateModifiedFields(this, userId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">The user identifier of the user making the change</param>
        public void InvitationExpired(int userId)
        {
            Populate(AccountStatu.Indexes.Inactive, AccountStatusReason.Indexes.Inactivity);
            Helper.UpdateModifiedFields(this, userId);
        }
        /// <summary>
        /// Set status to Password Expired
        /// </summary>
        /// <param name="userId">The user identifier of the user making the change</param>
        public void PasswordExpired(int userId)
        {
            Populate(AccountStatu.Indexes.Active, AccountStatusReason.Indexes.PwdExpired);
            Helper.UpdateModifiedFields(this, userId);
        }
        /// <summary>
        /// Returns the EntitySet Name for this entity.
        /// </summary>
        public string EntitySetName
        {
            get { return "UserAccountStatus"; }
        }
    }
}
