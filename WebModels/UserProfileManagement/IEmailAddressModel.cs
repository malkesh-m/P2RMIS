

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Email Address Information
    /// </summary>
    public interface IEmailAddressModel : IEditable
    {
        /// <summary>
        /// Email identifier
        /// </summary>
        int EmailId { get; set; }
        /// <summary>
        /// Email Address Type identifier
        /// </summary>
        int EmailAddressTypeId { get; set; }
        ///<summary>
        /// EmailAddress
        /// </summary>
        string Address { get; set; }
        /// <summary>
        /// Primary Email
        /// </summary>
        bool Primary { get; set; }
        /// <summary>
        /// Indicates if the email is a new email address to be added
        /// </summary>
        bool IsAdd { get; }
    }
}
