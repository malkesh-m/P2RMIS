using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    public class UserAddressUploadModel : IUserAddressUploadModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int? UserId { get; set; }
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
    }

    public interface IUserAddressUploadModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        int? UserId { get; set; }
        /// <summary>
        /// Gets or sets the vendor identifier.
        /// </summary>
        /// <value>
        /// The vendor identifier.
        /// </value>
        string VendorId { get; set; }
        /// <summary>
        /// Gets or sets the name of the vendor.
        /// </summary>
        /// <value>
        /// The name of the vendor.
        /// </value>
        string VendorName { get; set; }
        /// <summary>
        /// Gets or sets the inst vendor identifier.
        /// </summary>
        /// <value>
        /// The inst vendor identifier.
        /// </value>
        string InstVendorId { get; set; }
        /// <summary>
        /// Gets or sets the name of the reviewer.
        /// </summary>
        /// <value>
        /// The name of the reviewer.
        /// </value>
        string ReviewerName { get; set; }
        /// <summary>
        /// Address type identifier
        /// </summary>
        int? AddressTypeId { get; set; }
        /// <summary>
        /// Address identifier
        /// </summary>
        int? UserAddressId { get; set; }
        /// <summary>
        /// Address1 
        /// </summary>
        string Address1 { get; set; }
        /// <summary>
        /// Address2 
        /// </summary>
        string Address2 { get; set; }
        /// <summary>
        /// Address3 
        /// </summary>
        string Address3 { get; set; }
        /// <summary>
        /// Address4 
        /// </summary>
        string Address4 { get; set; }
        /// <summary>
        /// City
        /// </summary>
        string City { get; set; }
        /// <summary>
        /// State
        /// </summary>
        string State { get; set; }
        /// <summary>
        /// State identifier
        /// </summary>
        int? StateId { get; set; }
        /// <summary>
        /// Zip code
        /// </summary>
        string Zip { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        string Country { get; set; }
        /// <summary>
        /// Country identifier
        /// </summary>
        int? CountryId { get; set; }
    }
}
