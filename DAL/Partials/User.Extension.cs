using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Dal.EntityChanges;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's User object.
    /// </summary>
    public partial class User : ILogEntityChanges
    {
        #region Constants
        public class Constants
        {
            /// <summary>
            /// Maximum length of user's last name to use for constructing UserName
            /// </summary>
            internal const int ConstructUserNameLastNameLengthMaxLength = 5;
            /// <summary>
            /// Maximum length of user's first name to use for constructing UserName
            /// </summary>
            internal const int ConstructUserNameFirstNameLengthMaxLength = 1;
        }
        #endregion
        #region Static Attributes
        public static readonly Dictionary<string, PropertyChange> ChangeLogRequired = new Dictionary<string, PropertyChange>
        {
            { "W9Verified", new PropertyChange(typeof(bool?), UserInfoChangeType.Indexes.W9Verified) }                   // W9 Verified
        };
         #endregion

        #region WorkList
        public Dictionary<string, PropertyChange> PropertiesToLog()
        {
            return User.ChangeLogRequired;
        }
        /// <summary>
        /// Returns the name of the entity's key property.
        /// </summary>
        public string KeyPropertyName
        {
            get { return nameof(UserID); }
        }
        /// <summary>
        /// Specific Field Names
        /// </summary>
        public class Fields
        {
            /// <summary>
            /// Status of W9 Verified
            /// </summary>
            public const string W9Verified = "W9Verified";
            /// <summary>
            /// Status of W9 Verified Accurate
            /// </summary>
            public static string W9VerifiedAccurate { get { return "Verified-Accurate"; } }
            /// <summary>
            /// Status of W9 Verified Inaccurate
            /// </summary>
            public static string W9VerifiedInaccurate { get { return "Verified-Inaccurate"; } }
            /// <summary>
            /// Status of W9 Uploaded but not verified
            /// </summary>
            public static string W9Uploaded { get { return "Uploaded"; } }
            /// <summary>
            /// Status of W9 missing
            /// </summary>
            public static string W9Missing { get { return "Missing"; } }
        }

        #endregion


        #region Other Attributes
        /// <summary>
        /// List of errors after validation is performed.
        /// </summary>
        public IList<SaveProfileStatus> Errors { get; set; }

        /// <summary>
        /// Returns time in hours since last lock out
        /// </summary>
        /// <returns></returns>
        public int HoursSinceLockOut
        {
            get
            {
                return Convert.ToInt32((GlobalProperties.P2rmisDateTimeNow - LastLockedOutDate).TotalHours);
            }
        }
        #endregion
        #region Services provided
        /// <summary>
        /// Gets User's First Name
        /// </summary>
        /// <returns>User First Name</returns>
        public string FirstName()
        {
            return this.UserInfoes.DefaultIfEmpty(UserInfo.Default).First().FirstName;
        }
        /// <summary>
        /// Gets User's Last Name
        /// </summary>
        /// <returns>User Last Name</returns>
        public string LastName()
        {
            return this.UserInfoes.DefaultIfEmpty(UserInfo.Default).First().LastName;
        }
        /// <summary>
        /// Gets User's Full Name
        /// </summary>
        /// <returns></returns>
        public string FullName()
        {
            return string.Format("{0} {1}", this.UserInfoes.DefaultIfEmpty(UserInfo.Default).First().FirstName, this.UserInfoes.DefaultIfEmpty(UserInfo.Default).First().LastName);
        }
        /// <summary>
        /// Gets User's Primary Email address
        /// </summary>
        /// <returns>User Primary Email Address</returns>
        public string PrimaryUserEmailAddress()
        {
            return this.UserInfoes.FirstOrDefault().UserEmails.Where(x => x.PrimaryFlag == true).DefaultIfEmpty(UserEmail.Default).First().Email;
        }
        /// <summary>
        /// Verifies that one and only one phone or when profile type is misconduct, no phones and one primary email.
        /// </summary>
        /// <returns><True if all Required properties have data; false otherwise/returns>
        public bool IsPhonePrimaryValid()
        {
            Errors = new List<SaveProfileStatus>();
            //
            // Validate the updated entity models
            //
            // User has already been validated
            UserInfo userInfo = this.UserInfoes.FirstOrDefault();

            List<UserPhone> phoneList = userInfo.UserPhones.ToList();
            List<UserEmail> emailList = userInfo.UserEmails.ToList();
            int countPrimaryEmails = userInfo.UserEmails.Where(x => x.PrimaryFlag == true).Count();

            // should be one and only one one primary phone in the list 
            int numPrimaryPhones = phoneList.Count(x => x.PrimaryFlag == true);

            // unless ProfileType is misconduct and the emailList contains one or more emails
            bool valid = numPrimaryPhones <= 1;

            Helper.IsPrimaryPhoneCountValid(valid, SaveProfileStatus.OneAndOnlyOnePhoneIsPrimary, Errors);

            return valid;
        }
        /// <summary>
        /// Creates a new hashed password saving the salt and 
        /// hashed password in the entity object
        /// </summary>
        /// <param name="newPassword">The plain text password to be created and saved</param>
        public void CreatePassword(string newPassword)
        {
            string salt = User.CreateSalt();
            string newPasswordHash = User.CreatePasswordHash(newPassword, salt);

            this.PasswordSalt = salt;
            this.Password = newPasswordHash;

            this.PasswordDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Checks if a password matches a user's current password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPasswordMatchesCurrent(string password)
        {
            string salt = this.PasswordSalt;
            string passwordHashed = CreatePasswordHash(password, salt);
            return (passwordHashed == this.Password);
        }

        /// <summary>
        /// Checks if a password matches a user's previous passwords
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPasswordMatchesPrevious(string password)
        {
            var previousPasswords = this.UserPasswords?
                .OrderByDescending(x => x.CreatedDate)
                .Take(ConfigManager.PwdNumberPrevious);
            return previousPasswords.Any(x => CreatePasswordHash(password, x.PasswordSalt) == x.Password);
        }

        /// <summary>
        /// Gets the age of a users password in days
        /// </summary>
        /// <returns></returns>
        public int PasswordAge()
        {
            return GlobalProperties.P2rmisDateToday.Subtract(this.PasswordDate.Value.Date).Days;
        }
        /// <summary>
        /// Gets the profile identifier for a user.
        /// </summary>
        /// <returns>The user profile identifier</returns>
        public int UserProfileId()
        {
            return this.UserInfoes.FirstOrDefault()?.UserProfiles.FirstOrDefault()?.UserProfileId ?? 0;
        }
        /// <summary>
        /// Returns the account status as a readable string.
        /// </summary>
        /// <returns>Account status as a readable string</returns>
        public string ReadableAccountStatus()
        {
            return AccountStatus().AccountStatu.AccountStatusName;
        }
        /// <summary>
        /// Returns the account status entity identifier
        /// </summary>
        /// <returns>Account status entity identifier</returns>
        public int ReadableAccountStatusId()
        {
            return AccountStatus().AccountStatu.AccountStatusId;
        }
        /// <summary>
        /// Returns the account status reason as a readable string.
        /// </summary>
        /// <returns>Account status reason as a readable string</returns>
        public string ReadableAccountStatusReason()
        {
            return AccountStatus().AccountStatusReasonName;
        }
        /// <summary>
        /// Returns the account status reason entity identifier.
        /// </summary>
        /// <returns>Account status reason entity identifier</returns>
        public int ReadableAccountStatusReasonId()
        {
            return AccountStatus().AccountStatusReasonId;
        }
        /// <summary>
        /// Returns true if the account is locked
        /// </summary>
        /// <returns>True if the account is locked; false otherwise</returns>
        public bool LockedStatus()
        {
            return AccountStatus().AccountStatusReasonId == AccountStatusReason.Indexes.Locked;
        }
        /// <summary>
        /// Gets the AccountStatusReason for this user
        /// </summary>
        /// <returns>AccountStatusReason of the user's account</returns>
        public AccountStatusReason AccountStatus()
        {
            return UserAccountStatus.FirstOrDefault().AccountStatusReason;
        }
        /// <summary>
        /// Gets the date the account status last changed for this user
        /// </summary>
        /// <returns>Last change date of theuser's account status</returns>
        public DateTime? AccountStatusDate()
        {
            return UserAccountStatus.FirstOrDefault().ModifiedDate;
        }
        /// <summary>
        /// Returns indicator if the user is locked out.
        /// </summary>
        /// <returns>True if the user is locked out; false otherwise</returns>
        public bool IsLocked()
        {
            return (this.UserAccountStatus.FirstOrDefault().AccountStatusReasonId == AccountStatusReason.Indexes.Locked);
        }
        /// <summary>
        /// Returns the current user's status.
        /// </summary>
        /// <returns>User's current account status</returns>
        public UserAccountStatu UsersCurrentAccountStatus()
        {
            return this.UserAccountStatus.FirstOrDefault();
        }
        /// <summary>
        /// Update the credential information fields.
        /// </summary>
        /// <param name="userId">User entity identifier of user who sent the credentials/param>
        public void CredentialsWereSent(int userId)
        {
            this.CredentialSentBy = userId;
            this.CredentialSentDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// determines if the user has permanent login credentials
        /// </summary>
        /// <returns>true if the user has permanent login credentials</returns>
        public bool IsPermanentCredentials()
        {
            UserAccountStatu status = UsersCurrentAccountStatus();

            return status.AccountStatusId == AccountStatu.Indexes.Active && 
                (status.AccountStatusReasonId == AccountStatusReason.Indexes.PermCredentials || status.AccountStatusReasonId == AccountStatusReason.Indexes.PwdExpired);
        }
        /// <summary>
        /// determines if the user has permanent login credentials
        /// </summary>
        /// <returns>true if the user has permanent login credentials</returns>
        public bool CanUserLoginWithTemporaryCredentials()
        {
            UserAccountStatu status = UsersCurrentAccountStatus();

            return status.AccountStatusId == AccountStatu.Indexes.Active && status.AccountStatusReasonId == AccountStatusReason.Indexes.TmpPwd;
        }
        /// <summary>
        /// Determines if the user is an active user
        /// </summary>
        /// <returns>True if the user is an active user, false otherwise</returns>
        public bool IsActiveUser()
        {
            UserAccountStatu status = UsersCurrentAccountStatus();
            return status.AccountStatusId == AccountStatu.Indexes.Active;
        }
        /// <summary>
        /// Sets the users last login date
        /// </summary>
        public void SetLastLoginDate()
        {
            this.LastLoginDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Sets the users last login date
        /// </summary>
        public void SetLastLockoutDate()
        {
            this.LastLockedOutDate = GlobalProperties.P2rmisDateTimeNow;
            Helper.UpdateUserModifiedFields(this, this.UserID);
        }        
        /// <summary>
        /// Returns the W9 address if any exist.
        /// </summary>
        /// <returns>W-9 UserAddress entity</returns>
        public UserAddress W9Address()
        {
            return this.UserInfoEntity().UserAddresses.FirstOrDefault(x => x.AddressTypeId == AddressType.Indexes.W9);
        }
        /// <summary>
        /// Indicates if all the users registration are complete.
        /// </summary>
        /// <returns>False - the users registration are complete; True otherwise</returns>
        public bool AreUsersRegistrationInComplete()
        {
            return this.PanelUserAssignments.Any(x => x.IsRegistrationIncomplete());
        }
        /// <summary>
        /// Populates the User with W-9 validation status
        /// </summary>
        /// <param name="isVerified">True if the user is verified; false if not</param>
        public void Populate(bool? isVerified)
        {
            //
            // if there is a change in value then we change the values.  Otherwise
            // nothing needs to be set.  The radio buttons have three states
            //   - true
            //   - false
            //   - null: null implies that no selection has been made.  In which case
            //           there should not be any change to the record.
            //
            if ((isVerified.HasValue) && (this.W9Verified != isVerified) || (this.W9VerifiedDate < GlobalProperties.P2rmisDateTimeNow && isVerified.HasValue && isVerified.Value))
            {
                this.W9Verified = isVerified;
                this.W9VerifiedDate = GlobalProperties.P2rmisDateTimeNow;
            }
        }
        /// <summary>
        /// Populates the User validation status
        /// </summary>
        /// <param name="isVerified"></param>
        public void Populate()
        {
            this.Verified = true;
            this.VerifiedDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Properties updated when the user is authenticated by security questions.
        /// </summary>
        public void ModifyWhenAuthenticatedBySecurityQuestions()
        {
            this.LastLoginDate = GlobalProperties.P2rmisDateTimeNow;
            this.LastLockedOutDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Indicates W9 Status
        /// </summary>
        /// <returns></returns>
        public string GetW9Status()
        {
            return (this.W9Verified.HasValue && this.W9VerifiedDate.HasValue ? this.W9Verified.Value ? User.Fields.W9VerifiedAccurate : User.Fields.W9VerifiedInaccurate :
                                    this.UserInfoes.FirstOrDefault().UserAddresses.Any(x => x.AddressTypeId == AddressType.Indexes.W9) ? User.Fields.W9Uploaded : User.Fields.W9Missing);
        }
        /// <summary>
        /// Indicates W9 Status Date
        /// </summary>
        /// <returns></returns>
        public DateTime? GetW9StatusDate()
        {
            return (this.W9Verified.HasValue ? this.W9VerifiedDate : GetLastW9ModifiedDate());
        }
        /// <summary>
        /// Indicates last W9 modified date
        /// </summary>
        /// <returns></returns>
        protected DateTime? GetLastW9ModifiedDate()
        {
            var userAddress = this.UserInfoes.FirstOrDefault().UserAddresses.Where(x => x.AddressTypeId == AddressType.Indexes.W9).OrderByDescending(x => x.ModifiedDate).FirstOrDefault();
            return  (userAddress != null ? userAddress.ModifiedDate : null);
        }
        #region static methods
        /// <summary>
        /// Construct the user's UserName
        /// </summary>
        /// <param name="firstName">User's first name</param>
        /// <param name="lastName">User's last name</param>
        /// <param name="count">Unique identifier to use</param>
        /// <returns>User name</returns>
        public static string CounstructUserName(string firstName, string lastName, int count)
        {
            string userNameFormat = (count > 999) ? "{0}{1}{2}" : "{0}{1}{2:000}";
            //
            // Calculated the needed lengths
            //
            int lengthFirstNamePart = CreateFirstNameLengthValue(firstName);
            int lengthLastNamePart = CreateLastNameLengthValue(lastName);
            //
            // convert the first character to upper case
            //
            firstName = ToUpperFirstCharacter(firstName);
            lastName = ToUpperFirstCharacter(lastName);

            return string.Format(userNameFormat, firstName.Substring(0, lengthFirstNamePart).ToUpper(), lastName.Substring(0, lengthLastNamePart), count);
        }
        /// <summary>
        /// Create password hash based on supplied plain text password and the entity's object salt value
        /// (produces same hashed value as obsolete 'HashPasswordForStoringInConfigFile')
        /// </summary>
        /// <param name="password">The plain text password to hash</param>
        /// <param name="salt">The salt value to use in hashing the password</param>
        /// <returns>The hashed password</returns>
        public static string CreatePasswordHash(string password, string salt)
        {
            string saltAndPwd = String.Concat(password, salt);
            return Helper.CreateEncodedHash(saltAndPwd);
        }
        /// <summary>
        /// Create salt value for encryption/hashing
        /// </summary>
        /// <returns>A byte array of cryptographically strong sequence of randon numbers</returns>
        public static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }
        /// <summary>
        /// Utility method to change the first character of a string to upper case.
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Value with first character's case changed to upper case</returns>
        private static string ToUpperFirstCharacter(string value)
        {
            char[] x = value.ToCharArray();
            x[0] = char.ToUpper(x[0]);
            return new string(x);
        }
        /// <summary>
        /// Determines the maximum length of the first name portion of the user login.
        /// </summary>
        /// <param name="firstName">User's first name</param>
        /// <returns>Length of first name portion of the user name</returns>
        public static int CreateFirstNameLengthValue(string firstName)
        {
            return (firstName.Length < User.Constants.ConstructUserNameFirstNameLengthMaxLength) ? firstName.Length : User.Constants.ConstructUserNameFirstNameLengthMaxLength;
        }
        /// <summary>
        /// Determines the maximum length of the first name portion of the user login.
        /// </summary>
        /// <param name="lastName">User's last name</param>
        /// <returns>Length of last name portion of the user name</returns>
        public static int CreateLastNameLengthValue(string lastName)
        {
            return (lastName.Length < User.Constants.ConstructUserNameLastNameLengthMaxLength) ? lastName.Length : User.Constants.ConstructUserNameLastNameLengthMaxLength;
        }
        /// <summary>
        /// Get the user's UserInfo entity containing user descriptive information
        /// </summary>
        /// <returns>UserInfor entity</returns>
        /// <remarks>
        /// Assuming that there will be one and only one UserInfo entity per User entity.
        /// </remarks>
        public UserInfo UserInfoEntity()
        {
            return this.UserInfoes.FirstOrDefault();
        }
        /// <summary>
        /// Retrieve the SessionPanel entity for the user for the identified SessionPanel entity identifier.
        /// </summary>
        /// <param name="sessionPanelId"></param>
        /// <returns>SessionPanel entity identifier</returns>
        public PanelUserAssignment RetrieveSessionPanelByPanelid(int sessionPanelId)
        {
            return this.PanelUserAssignments.FirstOrDefault(x => x.SessionPanelId == sessionPanelId);
        }
        #endregion
        #region Activate/Deactivate
        /// <summary>
        /// Deactivate the user
        /// </summary>
        /// <param name="accountStatusReasonId">Reason entity identifier</param>
        /// <param name="userId">User entity identifier of user who deactivated the account/param>
        public UserAccountStatu Deactivate(int accountStatusReasonId, int userId)
        {
           UserAccountStatu UserAccountStatuEntity = this.UserAccountStatus.FirstOrDefault();
           UserAccountStatuEntity.Populate(AccountStatu.Indexes.Inactive, accountStatusReasonId);
           Helper.UpdateModifiedFields(UserAccountStatuEntity, userId);
           return UserAccountStatuEntity;
        }
        /// <summary>
        /// Activate the user
        /// </summary>
        /// <param name="userId">User entity identifier of user who activated the account/param>
        public UserAccountStatu Activate(int userId)
        {
            UserAccountStatu UserAccountStatuEntity = this.UserAccountStatus.FirstOrDefault();
            UserAccountStatuEntity.Populate(AccountStatu.Indexes.Active, AccountStatusReason.Indexes.AwaitingCredentials);
            Helper.UpdateModifiedFields(UserAccountStatuEntity, userId);
            return UserAccountStatuEntity;
        }
        /// <summary>
        /// Set user profile type to misconduct
        /// </summary>
        /// <param name="userId">User entity identifier of user who is deactivating the account due to misconduct</param>
        /// <returns></returns>
        public UserProfile Misconduct(int userId)
        {
            UserProfile UserProfileEntity = this.UserInfoes.DefaultIfEmpty(UserInfo.Default).First().UserProfiles.FirstOrDefault();
            UserProfileEntity.Populate(ProfileType.Indexes.Misconduct);
            Helper.UpdateModifiedFields(UserProfileEntity, userId);
            return UserProfileEntity;
        }
        #endregion
        #region Roles
        /// <summary>
        /// Determines the user's Role priority order within the profile type.
        /// </summary>
        /// <returns>Users priority order</returns>
        public int? RolePriorityOrder()
        {
            UserSystemRole userSystemRoleEntity = this.UserSystemRoles.FirstOrDefault();
            int? result = null;
            if (userSystemRoleEntity != null)
            {
                result = userSystemRoleEntity.SystemRole.SystemPriorityOrder;
            }
            return result;
        }
        /// <summary>
        /// Return the user's ProfileType entity identifier.
        /// </summary>
        /// <returns><Users ProfileType entity identifier/returns>
        /// <remarks>
        /// The following assumptions are made here:
        /// - A user has one UserInfo entity
        /// - A user always has a UserInfo 
        /// - A user has one UserProfile
        /// - A user always has a UserProfile
        /// </remarks>
        public int UserProfileTypeId()
        {
            return this.UserInfoes.First().UserProfiles.First().ProfileTypeId;
        }
        /// <summary>
        /// Return the user's system role entity identifier.
        /// </summary>
        /// <returns><Users SystemRole entity identifier/returns>
        /// <remarks>
        /// The following assumptions are made here:
        /// - A user has one UserInfo entity
        /// - A user always has a UserInfo 
        /// - A user has one UserProfile
        /// - A user always has a UserProfile
        /// </remarks>
        public int? GetUserSystemRole()
        {
            UserSystemRole userSystemRoleEntity = this.UserSystemRoles.FirstOrDefault();
            int? result = null;
            if (userSystemRoleEntity != null)
            {
                result = userSystemRoleEntity.SystemRoleId;
            }
            return result;
        }
        /// <summary>
        /// Returns the user's SystemRole name
        /// </summary>
        /// <returns>SystemRoleName if UserSystemRole exists; empty string otherwise</returns>
        public string GetUserSystemRoleName()
        {
            UserSystemRole userSystemRoleEntity = this.UserSystemRoles.FirstOrDefault();
            return (userSystemRoleEntity != null) ? userSystemRoleEntity?.SystemRole?.SystemRoleName : string.Empty;
        }
        /// <summary>
        /// Checks if the W-9 verification requires an update.
        /// </summary>
        /// <returns>True if W-9 information has been updated; false otherwise</returns>
        public bool IsW9Updated()
        {
            bool result = false;

            UserAddress userAddressEntity = W9Address();

            //
            // If there is no W-9 address then by default we are not updated
            //
            if (userAddressEntity == null)
            {

            }
            else
            {
                DateTime w9Date = (this.W9VerifiedDate.HasValue) ? this.W9VerifiedDate.Value : DateTime.MinValue;
                DateTime addressDate = ((userAddressEntity != null) && (userAddressEntity.ModifiedDate.HasValue)) ? userAddressEntity.ModifiedDate.Value : DateTime.MinValue;
                //
                // Else if we are verified then we can check the dates
                //
                if ((this.W9Verified.HasValue) && (this.W9Verified.Value))
                {
                    result = addressDate > w9Date;
                }
            }

            return result;
        }
        #endregion
        /// <summary>
        /// Calculate the average rating for this user.
        /// </summary>
        /// <returns></returns>
        public decimal? Rating()
        {
            IEnumerable<ReviewerEvaluation> resultSet = this.PanelUserAssignments.SelectMany(x => x.ReviewerEvaluations);

            return (resultSet.Count() > 0) ? Convert.ToDecimal(resultSet.Average(x => x.Rating)) : (decimal?)null;
        }
        /// <summary>
        /// Generate a list of the user's ClientProgram ids.
        /// </summary>
        /// <returns>List of ClientProgramIds</returns>
        public List<int> ListClientProgramId()
        {
            //
            // To get a list of this user's client program ids we get a list of their
            // clients.
            //
            return this.UserClients.Select(y => y.Client).
                //
                // Then we select all the programs for the clients
                //
                SelectMany(z => z.ClientPrograms1).
                //
                // and retrieve the ClientProgram id from the entity
                //
                Select(a => a.ClientProgramId).
                //
                // And convert it to a list for return
                //
                ToList();
        }
        /// <summary>
        /// Determines if the user is a client
        /// </summary>
        /// <returns>True if the user is a client; False otherwise</returns>
        public bool IsClient()
        {
            return UserProfileTypeId() == ProfileType.Indexes.Client;
        }
        #endregion

        #region Permissions
        /// <summary>
        /// Return a list of the user's permissions (operations)
        /// </summary>
        /// <returns>List containing the user's permission (operation)  names</returns>
        public List<string> GetAllAuthorizedOperations()
        {
            return this.UserSystemRoles.DefaultIfEmpty(new UserSystemRole()).Select(x => x.SystemRole)
                .SelectMany(x => x.RoleTasks).DefaultIfEmpty(new RoleTask()).Select(x => x.SystemTask)
                .SelectMany(x => x.TaskOperations).DefaultIfEmpty(new TaskOperation())
                .Select(x => x.SystemOperation.OperationName).ToList();
        }
        /// <summary>
        /// Checks if the user has the specified permission.
        /// </summary>
        /// <param name="permission">Permission (operation) to check</param>
        /// <returns>True if the user has the specified permission; false otherwise</returns>
        public bool HasPermission(string permission)
        {
            return GetAllAuthorizedOperations().Contains(permission);
        }
        #endregion
        #region WorkList
        /// <summary>
        /// Update all entries in the UserInfoChangeLog enumeration that is not marked as
        /// 'Reviewed' to 'Reviewed'
        /// </summary>
        /// <param name="userId">User entity identifier of the user making the changes</param>
        public void ReviewWorkList(int userId)
        {
            this.UserInfoEntity().ReviewWorkList(userId);
        }
        /// <summary>
        /// Determines if changes to the User's Profile are recorded.  Changes are recorded
        /// only for users with the Reviewer role.
        /// </summary>
        /// <returns>True if the changes are recorded; false otherwise</returns>
        public bool AreProfileChangesRecorded()
        {
            int? systemRoleId = this.GetUserSystemRole();
            return (systemRoleId.HasValue) ? (systemRoleId == SystemRole.Indexes.Reviewer) : false;
        }
        /// <summary>
        /// Determines if the user has been previously assigned to a program.
        /// </summary>
        /// <param name="programYears">List of ClientProgramId values for fiscal years</param>
        /// <returns>True if the user was assigned to the program; false otherwise</returns>
        public bool IsUserPreviouslyAssigned(List<int> programYears)
        {
            return this.PanelUserAssignments.Any(x => programYears.Contains(x.SessionPanel.ProgramPanels.DefaultIfEmpty(new ProgramPanel()).First().ProgramYearId));
        }
        #endregion
        /// <summary>
        /// Retrieves the Client entity identifiers on which the 
        /// user is an SRO on any panel.
        /// </summary>
        /// <returns>List of Client entity identifiers</returns>
        public IEnumerable<ClientProgram> SROAssignmentsBuyClient()
        {
            var result = PanelUserAssignments.
                //
                // From their Assignments that are SRO
                //
                Where(x => x.ClientParticipantType.IsSro() && (x.SessionPanel.ProgramPanels.Count() > 0)).
                //
                // Determine the client by this torturous path
                //
                Select(x => x.SessionPanel.ProgramPanels.FirstOrDefault().ProgramYear.ClientProgram).
                //
                // And we only want single entries so
                //
                Distinct();

            return result;
        }
        /// <summary>
        /// Retrieves the Client entity identifiers on which the 
        /// user is an SRO on any panel.
        /// </summary>
        /// <returns>List of Client entity identifiers</returns>
        public IEnumerable<ProgramYear> SROAssignmentsBuyYear()
        {
            var result = PanelUserAssignments.
                //
                // From their Assignments that are SRO
                //
                Where(x => x.ClientParticipantType.IsSro() && (x.SessionPanel.ProgramPanels.Count() > 0)).
                //
                // Determine the client by this torturous path
                //
                Select(x => x.SessionPanel.ProgramPanels.FirstOrDefault().ProgramYear).
                //
                // And we only want single entries so
                //
                Distinct();

            return result;
        }
    }
}
