using System;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// User Position Information
    /// </summary>
    public interface IAddressInfoModel : IEditable
    {
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
        /// <summary>
        /// Whether the address is the primary address
        /// </summary>
        bool? PrimaryFlag { get; set; }
        /// <summary>
        /// Modified date
        /// </summary>
        DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Indicates if the address is primary (since it can be null)
        /// </summary>
        bool IsPreferredAddress { get; }
    }
}
