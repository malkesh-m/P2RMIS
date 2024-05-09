using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.CrossCuttingServices.MessageServices
{
    /// <summary>
    /// Status enum when saving address
    /// </summary>
    public enum SaveAddressStatus
    {
        /// <summary>
        /// All enums should have a default
        /// </summary>
        Default = 0,
        /// <summary>
        /// Reviewer was successfully assigned
        /// </summary>
        Success = 1,
        /// <summary>
        /// The user identifier invalid
        /// </summary>
        UserIdInvalid = 2,
        /// <summary>
        /// The reviewer name not supplied
        /// </summary>
        ReviewerNameNotSupplied = 3,
        /// <summary>
        /// The vendor name not supplied
        /// </summary>
        VendorNameNotSupplied = 4,
        /// <summary>
        /// The vendor name too long
        /// </summary>
        VendorNameTooLong = 5,
        /// <summary>
        /// The zip too long
        /// </summary>
        ZipTooLong = 6,
        /// <summary>
        /// The address 1 too long
        /// </summary>
        Address1TooLong = 7,
        /// <summary>
        /// The address 2 too long
        /// </summary>
        Address2TooLong = 8,
        /// <summary>
        /// The address 3 too long
        /// </summary>
        Address3TooLong = 9,
        /// <summary>
        /// The address4 too long
        /// </summary>
        Address4TooLong = 10,
        /// <summary>
        /// The city too long
        /// </summary>
        CityTooLong = 11,
        /// <summary>
        /// The country code invalid
        /// </summary>
        CountryCodeInvalid = 12,
        /// <summary>
        /// The state invalid
        /// </summary>
        StateInvalid = 13,
        /// <summary>
        /// The address1 not supplied
        /// </summary>
        Address1NotSupplied = 14,
        /// <summary>
        /// The city not supplied
        /// </summary>
        CityNotSupplied = 15,
        /// <summary>
        /// The zip not supplied
        /// </summary>
        ZipNotSupplied = 16,
        /// <summary>
        /// The country code not supplied
        /// </summary>
        CountryCodeNull = 17,
        /// <summary>
        /// The country code not supplied
        /// </summary>
        StateNull = 18,
        /// <summary>
        /// The inst vendor identifier not supplied
        /// </summary>
        InstVendorIdNotSupplied = 19,
        /// <summary>
        /// The user identifier not supplied
        /// </summary>
        UserIdNotSupplied = 20,
        /// <summary>
        /// The vendor identifier not supplied
        /// </summary>
        VendorIdNotSupplied = 21,
        /// <summary>
        /// The vendor identifier invalid/
        /// </summary>
        VendorIdInvalid = 22,
        /// <summary>
        /// The vendor identifier character invalid
        /// </summary>
        VendorIdCharacterInvalid = 23,
        /// <summary>
        /// The vendor identifier too long
        /// </summary>
        VendorIdTooLong = 24,
        /// <summary>
        /// The vendor identifier is a duplicate
        /// </summary>
        VendorIdDuplicate = 25,
        /// <summary>
        /// The vendor id is not for this user id
        /// </summary>
        UserIdNotVendorId = 26,
    }
}
