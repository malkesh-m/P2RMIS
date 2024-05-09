
namespace Sra.P2rmis.Bll
{
	/// <summary>
	/// Enumeration for type of login allowed
	/// </summary>
    public enum LoginType
    {
        /// <summary>
        /// All enums should have a default
        /// </summary>
        Default = 0,
        /// <summary>
        /// User is not permitted to Login
        /// </summary>
        NoCredentials = 1,
        /// <summary>
        /// User is allowed to login under a temporary password
        /// </summary>
        TemporaryCredentials = 2,
        /// <summary>
        /// User is allowed to login under permanent login credentials
        /// </summary>
        PermanentCredentials = 3
    }
}
