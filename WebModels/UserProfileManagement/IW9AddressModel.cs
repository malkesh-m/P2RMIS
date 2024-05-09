
namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Interface defining the W9 address information
    /// </summary>
    public interface IW9AddressModel
    {
        /// <summary>
        /// The physical address
        /// </summary>
        IAddressInfoModel Address { get; set; }
        /// <summary>
        /// Indicates if a W9 address has been received for the user
        /// </summary>
        bool W9AddressExists { get; set; }
        /// <summary>
        /// W9 verification status (accurate, inaccurate or not specified)
        /// </summary>
        bool? W9Verified { get; set; }
        /// <summary>
        /// Vendor name for W9
        /// </summary>
        string VendorName { get; set; }
        /// <summary>
        /// Vendor Id 
        /// </summary>
        string VendorId { get; set; }
    }
}
