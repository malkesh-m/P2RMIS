
namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Interface for the user alternate contact phone numbers
    /// </summary>
    public interface IUserAlternatePersonContactPhoneModel 
    {
        /// <summary>
        /// Phone Number identifier
        /// </summary>
        int? UserAlternateContactPhoneId { get; set; }
        /// <summary>
        /// Phone Type identifier
        /// </summary>
        int? PhoneTypeId { get; set; }
        /// <summary>
        /// Phone Number
        /// </summary>
        string Number { get; set; } 
        /// <Summary>
        /// International Phone
        /// </Summary>
        bool International { get; set; }
        /// <summary>
        /// Optional extension
        /// </summary>
        string Extension { get; set; }
        /// <summary>
        /// Primary Flag
        /// </summary>
        bool PrimaryFlag { get; set; }

    }
}
