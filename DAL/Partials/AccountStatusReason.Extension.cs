
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's AccountStatusReason object. 
    /// </summary>
    public partial class AccountStatusReason
    {
        /// <summary>
        /// Specific index values.
        /// </summary>
        public class Indexes
        {
            /// <summary>
            /// User is a prospect awaiting credentials
            /// </summary>
            public const int AwaitingCredentials = 1;
            /// <summary>
            /// User has been sent credentials, but has not yet accepted by logging in and creating a permanent password
            /// </summary>
            public const int TmpPwd = 2;
            /// <summary>
            /// Account is locked
            /// </summary>
            public const int Locked = 3;
            /// <summary>
            /// Password has expired
            /// </summary>
            public const int PwdExpired = 4;
            /// <summary>
            /// Deactivated due to account inactivity
            /// </summary>
            public const int Inactivity = 5;
            /// <summary>
            /// User is ineligible
            /// </summary>
            public const int Ineligible = 6;
            /// <summary>
            /// Account is closed
            /// </summary>
            public const int AccountClosed = 7;
            /// <summary>
            /// Account is active
            /// </summary>
            public const int PermCredentials = 8;
        }
        /// <summary>
        /// Returns a comma separated list of the reason entity identifiers that are valid to enable/show the Active button
        /// </summary>
        /// <returns></returns>
        public static string ActivateButtonReasonIndexes()
        {
            string result = string.Format("{0},{1},{2},{3}", Indexes.Locked, Indexes.AccountClosed, Indexes.PwdExpired, Indexes.Inactivity);
            return result;
        }
    }
}
