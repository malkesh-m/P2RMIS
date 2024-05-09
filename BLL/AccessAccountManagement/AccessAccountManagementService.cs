using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;

namespace Sra.P2rmis.Bll.AccessAccountManagement
{
    /// <summary>
    /// AccessManagement provides services to perform business related functions related
    /// to the users access to the application.
    /// </summary> 
    public partial class AccessAccountManagementService : ServerBase, IAccessAccountManagementService
    {

        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public AccessAccountManagementService()
        {
            UnitOfWork = new UnitOfWork();
        }
        public AccessAccountManagementService(UnitOfWork unitOfWork)
        {
                UnitOfWork = unitOfWork;
        }
    #endregion
    /// <summary>
    /// Gets the login capability of the user
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <returns>Returns LoginCapability indicating the login capability of the user and the reason</returns>
    public LoginCapability GetUserLoginCapability(int userId)
        {
            this.ValidateInteger(userId, "AccessManagementService.GetUserLoginCapability", "userId");

            User userEntity = UnitOfWork.UserRepository.GetByID(userId);

            LoginCapability capability = new LoginCapability();

            capability.CapabilityType = userEntity.IsPermanentCredentials() ? LoginType.PermanentCredentials :
                                        userEntity.CanUserLoginWithTemporaryCredentials() ? LoginType.TemporaryCredentials : 
                                        LoginType.NoCredentials;

            capability.CapabilityReason = GetReason(userEntity);

            return capability;
        }
        /// <summary>
        /// Determines if the users invitation expired and updates user account status accordingly 
        /// </summary>
        /// <param name="daysExpired">The number of days the invitation remains valid</param>
        /// <param name="userToChangeId">The user identifier of the the user being changed</param>
        /// <param name="userId">The user identifier of the user making the change</param>
        /// <returns>Returns true if invitation expired, false otherwise</returns>
        public bool IsInvitationExpired(int daysExpired, int userToChangeId, int userId)
        {
            bool expired;

            this.ValidateInteger(userId, "AccessManagementService.GetUserLoginCapability", "userId");

            User userEntity = UnitOfWork.UserRepository.GetByID(userId);

            DateTime expDate = userEntity.PasswordDate.Value;

            expDate = expDate.AddDays(daysExpired);

            expired = GlobalProperties.P2rmisDateTimeNow > expDate;

            if (expired)
            {
                UserAccountStatu status = userEntity.UsersCurrentAccountStatus();
                status.InvitationExpired(userId);
                UnitOfWork.Save();
            }

            return expired;
        }
        /// <summary>
        /// Saves Password and updates user status if required
        /// </summary>
        /// <param name="userEntity">The user entity object</param>
        /// <param name="newPassword">The user's new password</param>
        internal virtual void SavePassword(User userEntity, string newPassword, int statusReason)
        {
            UserAccountStatu status = userEntity.UsersCurrentAccountStatus();
            userEntity.CreatePassword(newPassword);

            if (status.AccountStatusReasonId == statusReason)
            {
                status.UpdateUserAccountStatus(AccountStatusReason.Indexes.PermCredentials);
                UnitOfWork.UserAccountStatusRepository.Update(status);
            }
            UnitOfWork.UofwUserRepository.Update(userEntity);
        }
        /// <summary>
        /// Saves Password and updates user status if required
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="newPassword">The new password</param>
        public void SavePassword(int userId, string newPassword)
        {
            User userEntity = UnitOfWork.UserRepository.GetByID(userId);

            SavePassword(userEntity, newPassword, AccountStatusReason.Indexes.TmpPwd);

            UnitOfWork.Save();
        }
        /// <summary>
        /// Performs a case insensitive comparison of the username against existing names in the database
        /// </summary>
        /// <param name="username">The name of the user to validate</param>
        /// <returns>True if the username exists in the database, false otherwise</returns>
        public bool IsValidUserName(string username)
        {
            User user = UnitOfWork.UserRepository.Get(x => x.UserLogin == username).FirstOrDefault();
            
            return user != null;
        }
        /// <summary>
        /// Compare stored password against supplied password
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password to evaluate for the supplied user name</param>
        /// <returns>True if the password is correct for this user, false otherwise</returns>
        public bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            else
            {
                var user = UnitOfWork.UserRepository.Get(x => x.UserLogin == username)?.FirstOrDefault();
                return user == null ? false : user.CheckPasswordMatchesCurrent(password);
            }
        }
        /// <summary>
        /// Determines the user's login account status.
        /// </summary>
        /// <param name="userEntity">User entity</param>
        /// <returns>LoginReasonType</returns>
        internal LoginReasonType GetReason(User userEntity)
        {
            LoginReasonType type;

            switch (userEntity.UserAccountStatus.FirstOrDefault().AccountStatusReasonId)
            {
                case AccountStatusReason.Indexes.AwaitingCredentials:
                    type = LoginReasonType.AwaitingCredentials;
                    break;
                case AccountStatusReason.Indexes.TmpPwd:
                    type = LoginReasonType.TemporaryCredentials;
                    break;
                case AccountStatusReason.Indexes.Locked:
                    type = LoginReasonType.Locked;
                    break;
                case AccountStatusReason.Indexes.PwdExpired:
                    type = LoginReasonType.PasswordExpired;
                    break;
                case AccountStatusReason.Indexes.Inactivity:
                    type = LoginReasonType.Inactivity;
                    break;
                case AccountStatusReason.Indexes.Ineligible:
                    type = LoginReasonType.Ineligible;
                    break;
                case AccountStatusReason.Indexes.AccountClosed:
                    type = LoginReasonType.AccountClosed;
                    break;
                case AccountStatusReason.Indexes.PermCredentials:
                    type = LoginReasonType.PermanentCredentials;
                    break;
                default:
                    string message = "";
                    throw new ArgumentException(message);
            }
            return type;
        }
        /// <summary>
        /// Lockout the indicated user
        /// </summary>
        /// <param name="targetUserId">The user identifier of the user being locked out</param>
        /// <param name="userId">The user identifier of the user initiating the lockout action</param>
        public void LockoutUser(int targetUserId, int userId) 
        {
            User user = UnitOfWork.UserRepository.GetByID(userId);
            UserAccountStatu status = user.UsersCurrentAccountStatus();
            if (!user.LockedStatus())
            {
                status.Lock(userId, userId);
                user.SetLastLockoutDate();
                UnitOfWork.Save();
            }                      
        }
        /// <summary>
        /// Sets the date of the last user login to the current date
        /// </summary>
        /// <param name="userId">The user identifier</param>
        public void SetLastLoginDate(int userId)
        {
            this.ValidateInteger(userId, "AccessManagementService.SetLastLoginDate", "userId");
            User user = UnitOfWork.UserRepository.GetByID(userId);
            user.SetLastLoginDate();

            UnitOfWork.Save();
        }

        /// <summary>
        /// Gets the user's authorized operations.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of operation names to which the user is authorized</returns>
        public List<string> GetUserOperations(int userId)
        {
            User user = UnitOfWork.UserRepository.GetWithPermission(userId);
            List<string> operationsList = user.GetAllAuthorizedOperations();
            return operationsList;
        }

        public bool IsUserLockedOut(int userId)
        {            
                var user = UnitOfWork.UserRepository.Get(x => x.UserID == userId)?.FirstOrDefault();
                return user.LockedStatus();            
        }
        /// <summary>
        /// Auto Unlock Account after lock out period
        /// </summary>
        /// <param name="userId">The User Identifier</param>
        /// <param name="lockOutPeriodInHours">Lock out period in hours</param>
        public void AutoUnlockAccount(int userId, int lockOutPeriodInHours )
        {
            this.ValidateInteger(userId, "AccessManagementService.AutoUnlockAccount", "userId");
            this.ValidateInteger(lockOutPeriodInHours, "AccessManagementService.AutoUnlockAccount", "lockOutPeriodInHours");
            var user = UnitOfWork.UserRepository.Get(x => x.UserID == userId)?.FirstOrDefault();            
            if (user.LockedStatus() && user.HoursSinceLockOut >= lockOutPeriodInHours)
            {
                UserAccountStatusChangeLog userAccountStatusChangeLogEntity = UnitOfWork.UserAccountStatusChangeLogRepository.GetLastLock(userId);
                UserAccountStatu userAccountStatus = user.UsersCurrentAccountStatus();
                // note: UserAccountStatusChangeLog is updated by the database triggers
                userAccountStatus.Unlock(userAccountStatusChangeLogEntity.OldAccountStatusReasonId.Value, userAccountStatusChangeLogEntity.OldAccountStatusId.Value, userId);
                UnitOfWork.UserAccountStatusRepository.Update(userAccountStatus);
                UnitOfWork.Save();
            }

        }
    }
}
