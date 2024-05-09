using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Alternate contact Information
    /// </summary>
    /// 
    public interface IUserAlternateContactPersonModel : IEditable
    {
        /// <summary>
        /// User alternate contact identifier
        /// </summary>
        int UserAlternateContactId { get; set; }
        /// <summary>
        /// User alternate contact type identifier
        /// </summary>
        int? UserAlternateContactTypeId { get; set; }
        /// <summary>
        /// Alternate contact first name
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// 
        /// Alternate contact last name
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// collection of the alternate contact's phone numbers
        /// </summary>
        List<UserAlternateContactPersonPhoneModel> AlternateContactPhone { get; set; }
        /// <summary>
        /// Contact Email Address
        /// </summary>
        string Email { get; set; }
        /// <summary>
        /// Indicates if this is the primary contact
        /// </summary>
        bool PrimaryFlag { get; set; }
    }
}
