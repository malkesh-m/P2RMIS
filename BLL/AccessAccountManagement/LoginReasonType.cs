
namespace Sra.P2rmis.Bll.AccessAccountManagement
{
	/// <summary>
	/// Enumeration of the Account Status Reasons for account management login
	/// </summary>
    public enum LoginReasonType
    {
        /// <summary>
        /// All enums should have a default
        /// </summary>
        Default = 0,
        /// <summary>
        /// User is not permitted to Login
        /// </summary>
        AwaitingCredentials = 1,
        /// <summary>
        /// User is allowed to login under a temporary password
        /// </summary>
        TemporaryCredentials = 2,
        /// <summary>
        /// User is Locked
        /// </summary>
        Locked = 3,
        /// <summary>
        /// Password is expired
        /// </summary>
        PasswordExpired = 4,
        /// <summary>
        /// User Inactivity
        /// </summary>
        Inactivity = 5,
        /// <summary>
        /// User is ineligible
        /// </summary>
        Ineligible = 6,
        /// <summary>
        /// Account is closed
        /// </summary>
        AccountClosed = 7,
        /// <summary>
        /// User is allowed to login under permament login credentials
        /// </summary>
        PermanentCredentials = 8,
    }
}
