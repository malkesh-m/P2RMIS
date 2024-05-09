using System.Collections.Generic;
using Sra.P2rmis.WebModels.Lists;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Class containing the alternate contact information
    /// </summary>
    public interface IAlternateContactModel : IEditable
    {
        /// <summary>
        /// Phone type dropdown list
        /// </summary>
        ICollection<IListEntry> PhoneTypeDropdown { get; set; }
        /// <summary>
        /// Alternate contact type dropdown list
        /// </summary>
        ICollection<IListEntry> AlternateContactTypeDropdown { get; set; }
        /// <summary>
        /// Alternate contact phones
        /// </summary>
        ICollection<IPhoneNumberModel> AlternatePhones { get; set; }
    }
}
