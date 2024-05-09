using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.EntityChanges;
using System;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's User object.
    /// </summary>
    public partial class UserInfo: IStandardDateFields, ILogEntityChanges
    {
        #region Static Attributes
        public static readonly Dictionary<string, PropertyChange> ChangeLogRequired = new Dictionary<string, PropertyChange>
        {
            { "LastName", new PropertyChange(typeof(string), UserInfoChangeType.Indexes.LastName) },                            // User's first name
            { "FirstName", new PropertyChange(typeof(string), UserInfoChangeType.Indexes.FirstName) },                          // User's last name
            { "ProfessionalAffiliationId", new PropertyChange(typeof(int?), UserInfoChangeType.Indexes.OrgAffiliationType)},    // organization type
            { "Institution", new PropertyChange(typeof(string), UserInfoChangeType.Indexes.OrgAffiliationName) },               // organization name
            { "Department", new PropertyChange(typeof(string), UserInfoChangeType.Indexes.OrgAffiliationDept)},                 // Affiliation - Department
            { "Position", new PropertyChange(typeof(string), UserInfoChangeType.Indexes.OrgAffiliationPosition) },              // Affiliation - Position
            { "SuffixText", new PropertyChange(typeof(string), UserInfoChangeType.Indexes.Suffix) },                            // Suffix
            { "Expertise", new PropertyChange(typeof(string), UserInfoChangeType.Indexes.Expertise) },                          // Expertise
            { "DegreeNotApplicable", new PropertyChange(typeof(bool), UserInfoChangeType.Indexes.DegreesNA) }                   // Degree not applicable
        };
        #endregion
        /// <summary>
        /// List of errors after validation is performed.
        /// </summary>
        public IList<SaveProfileStatus> Errors { get; set; }
        /// <summary>
        /// Determines if the UserInfo military rank & status values are valid.
        /// </summary>
        /// <returns>True if all properties are correct length & required are defined; false otherwise</returns>
        public bool IsMilitaryRankAndStatusValid()
        {
            Errors = new List<SaveProfileStatus>();

            return AreIndexesValid() & AreIndexesSupplied();
        }
        /// <summary>
        /// Determines if the UserInfo military rank & status values are valid.
        /// </summary>
        /// <returns>True if all properties are correct length & required are defined; false otherwise</returns>
        public bool AreIndexesValid()
        {
            return (
                    Helper.IsValidIndex(this.MilitaryRankId, MilitaryRank.Indexes.Minimum, MilitaryRank.Indexes.Maximum, SaveProfileStatus.MilitaryRankInvalid, Errors) &
                    Helper.IsValidIndex(this.MilitaryStatusTypeId, MilitaryStatusType.Indexes.Minimum, MilitaryStatusType.Indexes.Maximum, SaveProfileStatus.MilitaryStatusInvalid, Errors)
                    );
        }
        /// <summary>
        /// Determines if the UserInfo military rank & status values are supplied
        /// </summary>
        /// <returns>True if indexes supplied; false otherwise</returns>
        public bool AreIndexesSupplied()
        {
            bool result = (
                            ((this.MilitaryRankId == null) && (this.MilitaryStatusTypeId == null)) ||
                            (this.MilitaryRankId != null) && (this.MilitaryStatusTypeId != null)
                            );
            if (!result)
            {
                Errors.Add(SaveProfileStatus.IncompleteMilitaryIndex);
            }
            return result;
        }
        /// <summary>
        /// Retrieves the users resume;
        /// </summary>
        /// <returns>UserResume entity</returns>
        public UserResume GetResume()
        {
            return this.UserResumes.FirstOrDefault();
        }
        /// <summary>
        /// Updates the UserInfo with the professional affiliation data
        /// </summary>
        /// <param name="professionalAffiliationId"></param>
        /// <param name="institution"></param>
        /// <param name="department"></param>
        /// <param name="position"></param>
        /// <param name="userId"></param>
        public void UpdateProfessionalAffiliation(int? professionalAffiliationId, string institution, string department, string position, int userId)
        {
            this.ProfessionalAffiliationId = professionalAffiliationId;
            this.Institution = institution;
            this.Department = department;
            this.Position = position;

            Helper.UpdateModifiedFields(this, userId);
        }
        /// <summary>
        /// Get preferred user address
        /// </summary>
        /// <returns></returns>
        public UserAddress PreferredUserAddress()
        {
            return UserAddresses.Where(x => x.PrimaryFlag).DefaultIfEmpty(new UserAddress()).First();
        }
        /// <summary>
        /// Get W9 address if exists, otherwise primary user address
        /// </summary>
        /// <returns></returns>
        public UserAddress W9AddressOrPrimaryUserAddress()
        {
            if (IsW9Verified() == false)
            {
                return PreferredUserAddress();
            }
            else
            {
                return W9Address() ?? PreferredUserAddress();
            }

        }
        /// <summary>
        /// Get W9 address
        /// </summary>
        /// <returns></returns>
        public UserAddress W9Address()
        {
            return UserAddresses.Where(x => x.AddressTypeId == (AddressType.Indexes.W9)).FirstOrDefault();
        }
        /// <summary>
        /// Full name with degree.
        /// </summary>
        /// <returns></returns>
        public string FullNameWithDegree()
        {
            return this.SuffixText != null ?
                string.Format("{0} {1}, {2}", FirstName, LastName, SuffixText) :
                string.Format("{0} {1}", FirstName, LastName);
        }
        /// <summary>
        /// Vendor's name or full name with degree.
        /// </summary>
        /// <returns></returns>
        public string VendorNameOrFullNameWithDegree()
        {
            if (!String.IsNullOrWhiteSpace(this.UserVendors.DefaultIfEmpty(new UserVendor())
                .FirstOrDefault(x => x.ActiveFlag == true)?.VendorName) && VendorName() != "N/A")
            {
                if (IsW9Verified() == false)
                {
                    return FullNameWithDegree();
                }
                else
                {
                    return this.UserVendors.FirstOrDefault(x => x.ActiveFlag == true).VendorName;
                }

            }
            else
            {
                return FullNameWithDegree();
            }

            //return !String.IsNullOrWhiteSpace(this.UserVendors.DefaultIfEmpty(new UserVendor()).FirstOrDefault(x => x.ActiveFlag == true)?.VendorName) ?
            //    this.UserVendors.FirstOrDefault(x => x.ActiveFlag == true).VendorName : FullNameWithDegree();
        }
        /// <summary>
        /// Vendors the name.
        /// </summary>
        /// <returns>User's currently active vendor name</returns>
        public string VendorName()
        {
            return this.UserVendors.DefaultIfEmpty(new UserVendor()).FirstOrDefault(x => x.ActiveFlag == true)?.VendorName;
        }
        /// <summary>
        /// Vendors the identifier.
        /// </summary>
        /// <returns>User's current active vendor Id</returns>
        public string VendorId()
        {
            return this.UserVendors.DefaultIfEmpty(new UserVendor()).FirstOrDefault(x => x.ActiveFlag == true)?.VendorId;
        }
        /// <summary>
        /// Determines whether [has vendor identifier] [the specified vendor identifier].
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns>
        ///   <c>true</c> if [has vendor identifier] [the specified vendor identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasVendorId(string vendorId, int vendorTypeId)
        {
            return this.UserVendors.Any(x => x.VendorId == vendorId && x.VendorTypeId == vendorTypeId);
        }
        #region WorkList
        /// <summary>
        /// Update all entries in the UserInfoChangeLog enumeration that is not marked as
        /// 'Reviewed' to 'Reviewed'
        /// </summary>
        /// <param name="userId">User entity identifier of the user making the changes</param>
        public void ReviewWorkList(int userId)
        {
            IEnumerable<UserInfoChangeLog> userInfoChangeLogEntityCollection = this.UserInfoChangeLogs.Where(x => !x.ReviewedFlag);
            foreach (var userInfoChangeLogEntity in userInfoChangeLogEntityCollection)
            {
                userInfoChangeLogEntity.Populate(userId);
                Helper.UpdateModifiedFields(userInfoChangeLogEntity, userId);
            }
        }
        public Dictionary<string, PropertyChange> PropertiesToLog()
        {
            return UserInfo.ChangeLogRequired;
        }
        /// <summary>
        /// Returns the name of the entity's key property.
        /// </summary>
        public string KeyPropertyName
        {
            get { return nameof(UserInfoID); }
        }
        #endregion
        /// <summary>
        /// Gets the user's military branch name if one exists
        /// </summary>
        /// <returns>Military branch name if one exists; empty string otherwise</returns>
        public string GetMilitaryBranch()
        {
            return (!this.MilitaryRankId.HasValue) ? string.Empty : MilitaryRank.Service;
        }
        /// <summary>
        /// Gets the user's military rank name if one exists
        /// </summary>
        /// <returns>Military rank name if one exists; empty string otherwise</returns>
        public string GetMilitaryRank()
        {
            return (!this.MilitaryRankId.HasValue) ? string.Empty : MilitaryRank.MilitaryRankName;
        }
        /// <summary>
        /// Gets the user's military status if one exists
        /// </summary>
        /// <returns>Military rank name if one exists; empty string otherwise</returns>
        public string GetMilitaryStatus()
        {
            return (!this.MilitaryStatusTypeId.HasValue) ? string.Empty : MilitaryStatusType.StatusType;
        }
        /// <summary>
        /// Returns the UerResume entity identifier.
        /// </summary>
        /// <returns>UserResume entity identifier if one exists; null otherwise</returns>
        public int? GetUserResumeId()
        {
            return (this.UserResumes.Count() > 0) ? this.UserResumes.First().UserResumeId : (int?) null;
        }
        //
        // Returns an enumeration of the user's degrees names.
        //
        public IEnumerable<string> GetDegreesNames()
        {
            return this.UserDegrees.Select(x => x.Degree.DegreeName);
        }
        /// <summary>
        /// Returns the primary WebSite entity
        /// </summary>
        /// <returns></returns>
        public UserWebsite GetPreferredWebsite()
        {
            return this.UserWebsites.FirstOrDefault(x => x.WebsiteTypeId == WebsiteType.PrimaryWebsiteTypeId);
        }
        /// <summary>
        /// Determines whether the specified client identifier is blocked.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified client identifier is blocked; otherwise, <c>false</c>.
        /// </returns>
        public bool IsBlocked(int clientId)
        {
            return this.UserClientBlocks.Any(x => x.ClientId == clientId);
        }
        /// <summary>
        /// Returns the user's primary email address
        /// </summary>
        /// <returns>The user's primary email identifier</returns>
        public int GetPrimaryEmailId()
        {
            return this.UserEmails.Where(x => x.PrimaryFlag == true).Select(x => x.EmailID).FirstOrDefault();
        }
        /// <summary>
        /// Gets whether or not the user has a preferred email address
        /// </summary>
        /// <returns>true if the user has a preferred email address, otherwise false</returns>
        public bool HasPreferredEmail()
        {
            return this.UserEmails.Where(x => x.PrimaryFlag == true).Count() == 1;
        }
        /// <summary>
        /// Gets the primary phone number for a user.
        /// </summary>
        /// <returns>Primary phone number as a string</returns>
        public string GetPrimaryPhoneNumber()
        {
            return this.UserPhones.Where(x => x.PrimaryFlag ?? false).DefaultIfEmpty(new UserPhone()).First().Phone;
        }

        public bool? IsW9Verified()
        {
            return User.W9Verified;
        }

    }
}
