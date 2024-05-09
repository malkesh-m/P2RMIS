namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Interface for the user alternate contact phone numbers
    /// </summary>
    public class UserAlternateContactPersonPhoneModel : IUserAlternatePersonContactPhoneModel
    {
        /// <summary>
        /// Minimum entries
        /// </summary>
        public const int MinimumEntries = 2;
        /// <summary>
        /// Initialize model
        /// </summary>
        /// <param name="model">the UserAlternateContactPersonModel</param>
        public static void InitializeModel(UserAlternateContactPersonPhoneModel model) { }
        /// <summary>
        /// Phone Number identifier
        /// </summary>
        public int? UserAlternateContactPhoneId { get; set; }
        /// <summary>
        /// Phone Type identifier
        /// </summary>
        public int? PhoneTypeId { get; set; }
        /// <summary>
        /// Phone Number
        /// </summary>
        public string Number { get; set; }
        /// <Summary>
        /// International Phone
        /// </Summary>
        public bool International { get; set; }
        /// <summary>
        /// Optional extension
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// Primary Flag
        /// </summary>
        public bool PrimaryFlag { get; set; }
    }
}
