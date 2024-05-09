
namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Phone Number Information
    /// </summary>
    public interface IPhoneNumberModel : IEditable
    {
        /// <summary>
        /// Phone Number identifier
        /// </summary>
        int PhoneId { get; set; }
        /// <summary>
        /// Phone Type identifier
        /// </summary>
        int? PhoneTypeId { get; set; }
        /// <summary>
        /// Phone Number
        /// </summary>
        string Number { get; set; }
        /// <Summary>
        /// Primary Phone
        /// </Summary>
        bool Primary { get; set; }
        /// <summary>
        /// Optional extension
        /// </summary>
        string Extension { get; set; }
        /// <summary>
        /// International Phone
        /// </summary>
        bool International { get; set; }
    }
}
