using System;
using System.Text.RegularExpressions;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the tabs in user profile management
    /// </summary
    public class UserAddressUploadViewModel
    {
        public UserAddressUploadViewModel()
        {
        }
        public UserAddressUploadViewModel(IUserAddressUploadModel address)
        {
            UserId = Convert.ToString(address.UserId);
            VendorName = address.VendorName;
            VendorId = Convert.ToString(address.VendorId);
            ReviewerName = address.ReviewerName;
            AddressTypeId = address.AddressTypeId;
            UserAddressId = address.UserAddressId;
            Address1 = address.Address1;
            Address2 = address.Address2;
            Address3 = address.Address3;
            Address4 = address.Address4;
            City = address.City;
            State = address.State;
            StateId = address.StateId;
            Zip = address.Zip;
            Country = address.Country;
            CountryId = address.CountryId;
        }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }
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
        /// Gets or sets the inst vendor identifier.
        /// </summary>
        /// <value>
        /// The inst vendor identifier.
        /// </value>
        public string InstVendorId { get; set; }
        /// <summary>
        /// Gets or sets the name of the reviewer.
        /// </summary>
        /// <value>
        /// The name of the reviewer.
        /// </value>
        public string ReviewerName { get; set; }
        /// <summary>
        /// Address type identifier
        /// </summary>
        public int? AddressTypeId { get; set; }
        /// <summary>
        /// Address identifier
        /// </summary>
        public int? UserAddressId { get; set; }
        /// <summary>
        /// Address1 
        /// </summary>
        public string Address1 { get; set; }
        /// <summary>
        /// Address2 
        /// </summary>
        public string Address2 { get; set; }
        /// <summary>
        /// Address3 
        /// </summary>
        public string Address3 { get; set; }
        /// <summary>
        /// Address4 
        /// </summary>
        public string Address4 { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// State identifier
        /// </summary>
        public int? StateId { get; set; }
        /// <summary>
        /// Zip code
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Country identifier
        /// </summary>
        public int? CountryId { get; set; }
        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <returns></returns>
        public UserAddressUploadModel GetAddress()
        {
            var address = new UserAddressUploadModel();
            Regex regex = new Regex(@"^\d+$");
            if (UserId != null)
                address.UserId = regex.IsMatch(UserId.Trim()) ? Int32.Parse(UserId.Trim()) : 
                    String.IsNullOrEmpty(UserId.Trim()) ? null : (int?)-1;
            address.VendorName = VendorName;
            address.VendorId = VendorId;
            address.InstVendorId = InstVendorId;
            address.ReviewerName = ReviewerName;
            address.AddressTypeId = AddressTypeId;
            address.UserAddressId = UserAddressId;
            address.Address1 = Address1;
            address.Address2 = Address2;
            address.Address3 = Address3;
            address.Address4 = Address4;
            address.City = City;
            address.State = State;
            address.StateId = StateId;
            address.Zip = Zip;
            address.Country = Country;
            address.CountryId = CountryId;
            return address;
        }
    }
}