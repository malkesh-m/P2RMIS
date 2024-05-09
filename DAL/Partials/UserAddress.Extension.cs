using Sra.P2rmis.Dal.EntityChanges;
using Sra.P2rmis.Dal.Interfaces;
using System.Collections.Generic;


namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's UserAddress object.
    /// </summary>
    public partial class UserAddress : IStandardDateFields, ILogEntityChanges
    {
        #region constants
        /// <summary>
        /// List of the properties to log when changes to the User Profile are detected.
        /// </summary>
        public static readonly Dictionary<string, PropertyChange> ChangeLogRequired = new Dictionary<string, PropertyChange>
        {
            { "StateId", new PropertyChange(typeof(int?), UserInfoChangeType.Indexes.PersonalAddressState, false) }
        };

        public class Limits
        {
            /// <summary>
            /// Maximum number of personal addresses a user can have.
            /// </summary>
            public static int MaximumPersonalAddresses = 1;
            /// <summary>
            /// Minimum number of addresses a reviewer must have
            /// </summary>
            public static int MinimumAddressesForReviewer = 1;
        }
        #endregion
        /// <summary>
        /// Populate a new UserAddress with data.
        /// </summary>
        /// <param name="isPreferred"><Preferred indicator/param>
        /// <param name="address1">Address 1</param>
        /// <param name="address2">Address 2</param>
        /// <param name="address3">Address 3</param>
        /// <param name="address4">Address 4</param>
        /// <param name="city">City</param>
        /// <param name="stateId"><State identifier/param>
        /// <param name="zip">Zip code</param>
        /// <param name="countryId">Country identifier</param>
        public void Populate(bool isPreferred, int addressTypeId, string address1, string address2, string address3, string address4, string city, int? stateId, string zip, int? countryId)
        {
            this.Address1 = address1;
            this.Address2 = address2;
            this.Address3 = address3;
            this.Address4 = address4;
            this.City = city;
            this.Zip = zip;
            this.PrimaryFlag = isPreferred;
            this.StateId = stateId;
            this.CountryId = countryId;
            this.AddressTypeId = addressTypeId;
        }
        /// <summary>
        /// Wrapper to return the state abbreviation if one exists, null if not
        /// </summary>
        /// <returns>State abbreviation</returns>
        public string StateAbbreviation()
        {
            return (this.State == null) ? null : this.State.StateAbbreviation;
        }
        /// <summary>
        /// Determines if the W9 address is valid.
        /// </summary>
        /// <returns>True if the address is invalid; false otherwise</returns>
        /// <remarks>Assumes the address is a  W9 Address</remarks>
        public bool IsInvalidW9()
        {
            return 
                   string.IsNullOrWhiteSpace(this.Address1) &
                   string.IsNullOrWhiteSpace(this.Address2) &
                   string.IsNullOrWhiteSpace(this.City) &
                   (StateId == null) & 
                   string.IsNullOrWhiteSpace(this.Zip) &
                   (CountryId == null);

        }
        /// <summary>
        /// Wrapper to return the country abbreviation if one exists, null if not
        /// </summary>
        /// <returns></returns>
        public string CountryAbbreviation()
        {
            return (this.CountryId == null)? null: this.Country.CountryAbbreviation;
        }
        /// <summary>
        /// Wrapper to return the country name if one exists, null if not
        /// </summary>
        /// <returns></returns>
        public string CountryName()
        {
            return (this.CountryId == null)? null: this.Country.CountryName;
        }
        /// <summary>
        /// Returns the properties to log.
        /// </summary>
        /// <returns>Dictionary of properties to log for changes</returns>
        public Dictionary<string, PropertyChange> PropertiesToLog()
        {
            return ChangeLogRequired;
        }
        /// <summary>
        /// Returns the name of the entity's key property.
        /// </summary>
        public string KeyPropertyName
        {
            get { return nameof(AddressID); }
        }

    }
}
