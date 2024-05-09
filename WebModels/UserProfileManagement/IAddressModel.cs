using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Class containing the institution and personal address and the position at each institutional address
    /// </summary>
    public interface IAddressModel
    {
        /// <summary>
        /// Institutional address and position
        /// </summary>
        ICollection<IInstitutionAddressModel> Institution { get; set; }
        /// <summary>
        /// Personal address
        /// </summary>
        ICollection<IAddressInfoModel> Personal { get; set; }
        IW9AddressModel W9 { get; set; }
    }
}
