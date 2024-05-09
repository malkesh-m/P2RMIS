namespace Sra.P2rmis.Bll.AccessAccountManagement
{
    /// <summary>
    /// An object containing information relating to the login capability of the user
    /// </summary>
    public class LoginCapability
    {
        /// <summary>
        /// Indicates the type of login, if any, the user is permitted to perform
        /// </summary>
        public LoginType CapabilityType { get; set; }
        /// <summary>
        /// The reason for the user's login capability
        /// </summary>
        public LoginReasonType CapabilityReason { get; set; }
    }
}
