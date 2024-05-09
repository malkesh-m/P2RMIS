using System.Collections.Generic;

namespace Sra.P2rmis.Bll.AccessAccountManagement
{
    /// <summary>
    /// AccessManagement provides services to perform business related functions related
    /// to the users access to the application.
    /// </summary>     
    public interface IAccessAccountManagementService
    {
        /// <summary>
        /// Gets the login capability of the user
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>Returns LoginCapability indicating the login capability of the user and the reason</returns>
        LoginCapability GetUserLoginCapability(int userId);
        /// <summary>
        /// Determines if the users invitation expired and updates user accoutn status accordingly 
        /// </summary>
        /// <param name="daysExpired">The number of days the invitation remains valid</param>
        /// <param name="userToChangeId">The user identifer of the the user being changed</param>
        /// <param name="userId">The user identifier of the user making the change</param>
        /// <returns>Returns true if invitation expired, false otherwise</returns>
        bool IsInvitationExpired(int daysExpired, int userToChangeId, int userId);
        /// <summary>
        /// Saves Password and updates user status if required
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="newPassword">The new password</param>
        void SavePassword(int userId, string newPassword);
        /// <summary>
        /// Validates the username against existing names in the database
        /// </summary>
        /// <param name="username">The name of the user to validate</param>
        /// <returns>True if the username exists in the database, false otherwise</returns>
        bool IsValidUserName(string username);
        /// <summary>
        /// Compare stored password against supplied password
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password to evaluate for the supplied user name</param>
        /// <returns>True if the password is correct for this user, false otherwise</returns>
        bool ValidateUser(string username, string password);
        /// <summary>
        /// Lockout the indicated user
        /// </summary>
        /// <param name="targetUserId">The user identifier of the user being locked out</param>
        /// <param name="userId">The user identifier of the user initiating the lockout action</param>
        void LockoutUser(int targetUserId, int userId);
        /// <summary>
        /// Sets the date of the last user login to the current date
        /// </summary>
        /// <param name="userId">The user identifier</param>
        void SetLastLoginDate(int userId);

        /// <summary>
        /// Gets the user's authorized operations.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        List<string> GetUserOperations(int userId);
        /// <summary>
        /// Gets if user is locked out
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>user is locked out</returns>
        bool IsUserLockedOut(int userId);
        /// <summary>
        /// Auto Unlock Account after lock out period
        /// </summary>
        /// <param name="userId">The User Identifier</param>
        void AutoUnlockAccount(int userId, int lockOutPeriodInHours);
    }
}
