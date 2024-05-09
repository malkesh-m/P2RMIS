using Sra.P2rmis.Dal.EntityChanges;
using Sra.P2rmis.Dal.Interfaces;
using System.Collections.Generic;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's UserEmail object. 
    /// </summary>
    public partial class UserEmail : IStandardDateFields, ILogEntityChanges
    {
        /// <summary>
        /// List of the properties to log when changes to the User Profile are detected.
        /// </summary>
        public static readonly Dictionary<string, PropertyChange> ChangeLogRequired = new Dictionary<string, PropertyChange>
        {
            { "Email", new PropertyChange(typeof(string), UserInfoChangeType.Indexes.Email, true) },  
            { "PrimaryFlag", new PropertyChange(typeof(bool), UserInfoChangeType.Indexes.PrimaryFlag, true) }
        };
        /// <summary>
        /// Populates a new UserEmail in preparation for addition to the repository.
        /// </summary>
        /// <param name="address">The new email address</param>
        /// <param name="emailAddressTypeId">The email address type</param>
        /// <param name="userInfoId">The user info identifier</param>
        /// <param name="primary">Primary or secondary email address</param>
        public void Populate(string address, int emailAddressTypeId, int userInfoId, bool primary)
        {
            this.Email = address;
            this.EmailAddressTypeId = emailAddressTypeId;
            this.UserInfoID = userInfoId;
            this.PrimaryFlag = primary;
        }
        /// <summary>
        /// Default value for Linq search.
        /// </summary>
        private static UserEmail _default;
        public static UserEmail Default
        {
            get
            {
                //
                // Lazy load the default
                //
                if (_default == null)
                {
                    _default = new UserEmail();
                }
                return _default;
            }
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
            get { return nameof(EmailID); }
        }
    }
}
