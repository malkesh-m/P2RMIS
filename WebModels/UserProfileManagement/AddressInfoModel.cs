using System;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// User Position Information
    /// </summary>
    public class AddressInfoModel : Editable, IAddressInfoModel
    {
        /// <summary>
        /// Minimum entries
        /// </summary>
        public const int MinimumEntries = 1;
        /// <summary>
        /// Initialize model
        /// </summary>
        /// <param name="model">the AddressInfoModel</param>
        public static void InitializeModel(AddressInfoModel model) { }
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
        /// Whether the address is the primary address
        /// </summary>
        public bool? PrimaryFlag { get; set; }
        /// <summary>
        /// Modified date
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Indicates if the position is deleted
        /// </summary>
        /// <returns>Reurns true if the item is to be deleted, false otherwise</returns>
        public override bool IsDeleted()
        {
            return UserAddressId > 0 && (IsDeletable || !HasData());
        }
        /// <summary>
        /// Indicates if the address is primary (since it can be null)
        /// </summary>
        public bool IsPreferredAddress 
        {
            get { return ((PrimaryFlag.HasValue) && (PrimaryFlag.Value)) ? true : false; }
        }
        /// <summary>
        /// Is there actual data to save.  Check Address1 since it is required.
        /// </summary>
        /// <returns>True if there is data to save; false otherwise/returns>
        public override bool HasData()
        {
            return (!string.IsNullOrWhiteSpace(Address1) || !string.IsNullOrWhiteSpace(Address2) || !string.IsNullOrWhiteSpace(City)
                  || !string.IsNullOrWhiteSpace(Zip) || !string.IsNullOrWhiteSpace(Address2));
        }
    }
}
