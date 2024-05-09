
namespace Sra.P2rmis.Web.ViewModels.UserProfileManagement
{
    public class UserVendorViewModel
    {
        public UserVendorViewModel(string vendorId, string vendorName, bool isActive)
        {
            VendorId = vendorId;
            VendorName = vendorName;
            IsActive = isActive;
        }
        /// <summary>
        /// Gets or sets the vendor identifier.
        /// </summary>
        /// <value>
        /// The vendor identifier.
        /// </value>
        public string VendorId { get; set; }
        /// <summary>
        /// Gets or sets the name of the vendor.
        /// </summary>
        /// <value>
        /// The name of the vendor.
        /// </value>
        public string VendorName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
    }
}