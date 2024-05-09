
namespace Sra.P2rmis.CrossCuttingServices.MessageServices
{
    /// <summary>
    /// Status values returned when performing a manage account action
    /// </summary>
    public enum ManageAccount
    {
        /// <summary>
        /// All enums should have a default
        /// </summary>
        Default = 0,
        /// <summary>
        /// Credentials were successfully sent
        /// </summary>
        SendCredentialsSuccess = 1,
        /// <summary>
        /// Resume exceeded maximum size.
        /// </summary>
        SendCredentialsfailure = 2
    }
}
