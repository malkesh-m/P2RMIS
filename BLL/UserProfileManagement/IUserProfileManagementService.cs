using System;
using System.Collections.Generic;
using System.IO;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Files = Sra.P2rmis.Bll.FileService;

namespace Sra.P2rmis.Bll.UserProfileManagement
{
    /// <summary>
    /// UserProfileManagementService provides services to perform business related functions for
    /// the User Profile Management Application. (Search Users; Create Profiles, Merge Profiles etc.)
    /// </summary>
    public interface IUserProfileManagementService
    {
        #region Search
        /// <summary>
        /// Performs a fuzzy search on supplied user information.  This version is used when searching
        /// for existing users.
        /// </summary>
        /// <param name="firstName">User's first name</param>
        /// <param name="lastName">User's last name</param>
        /// <param name="email">User's email</param>
        /// <param name="userName">User's name</param>
        /// <param name="userId">User's identifier</param>
        /// <param name="vendorId">User's vendor identifier</param>
        /// <exception cref="ArgumentException">Thrown if all parameters are null or empty string</exception>
        /// <returns>Container of IFoundUserModels that matched the criteria</returns>
        Container<IFoundUserModel> SearchUser(string firstName, string lastName, string email, string userName, int? userId, string vendorId);
        /// <summary>
        /// Performs a fuzzy search on supplied user information.  This version is used when searching to create
        /// a user.
        /// </summary>
        /// <param name="firstName">User's first name</param>
        /// <param name="lastName">User's last name</param>
        /// <param name="email">User's email</param>
        /// <exception cref="ArgumentException">Thrown if all parameters are null or empty string</exception>
        /// <returns>Container of IFoundUserModels that matched the criteria</returns>
        Container<IFoundUserModel> SearchUser(string firstName, string lastName, string email);
        #endregion
        #region Profile information        
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IUserModel GetUser(int userId);
        /// <summary>
        /// Get general user information
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>GeneralInfoModel</returns>
        IGeneralInfoModel GetUserGeneralInfo(int userInfoId);
        /// <summary>
        /// Get user websites
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>Container of IWebsiteModel</returns>
        Container<WebsiteModel> GetUserWebsite(int userInfoId);
        /// <summary>
        /// retrieves all user email addresses
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>Container of EmailAddressModel</returns>
        Container<EmailAddressModel> GetUserEmailAddress(int userInfoId);
        /// <summary>
        /// retrieves user institutional email address
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>EmailAddressModel</returns>
        IEmailAddressModel GetInstitutionalUserEmailAddress(int userInfoId);
        /// <summary>
        /// retrieves primary user institutional email address
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>EmailAddressModel</returns>
        IEmailAddressModel GetPrimaryInstitutionalUserEmailAddress(int userInfoId);
        /// <summary>
        /// retrieves user alternate email address
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>EmailAddressModel</returns>
        IEmailAddressModel GetAlternateUserEmailAddress(int userInfoId);
        /// <summary>
        /// retrieves user personal email address
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>EmailAddressModel</returns>
        IEmailAddressModel GetPersonalUserEmailAddress(int userInfoId);
        /// <summary>
        /// Retrieves institutional addresses
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>Container of institutional addresses</returns>
        Container<IInstitutionAddressModel> GetInstitutionalAddresses(int userInfoId);
        /// <summary>
        /// Retrieves organizational and personal addresses
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>Container of personal and organizational addresses</returns>
        Container<IAddressInfoModel> GetOrganizationalPersonalAddresses(int userInfoId);
        /// <summary>
        /// Retrieves personal addresses
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>Container of personal addresses</returns>
        Container<IAddressInfoModel> GetPersonalAddresses(int userInfoId);
        /// <summary>
        /// Retrieves W9 addresses
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>W9AddressModel</returns>
        IW9AddressModel GetW9Addresses(int userInfoId);
        /// <summary>
        /// Retrieves alternative contact persons
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>Container of alternative contact persons</returns>
        Container<IUserAlternateContactPersonModel> GetAlternativeContactPersons(int userInfoId);
        /// <summary>
        /// Gets the emergency contact person.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        IUserAlternateContactPersonModel GetEmergencyContactPerson(int userInfoId);
        /// <summary>
        /// Retrieves user phones
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>Container of phone number model</returns>
        Container<PhoneNumberModel> GetUserPhones(int userInfoId);

        /// <summary>
        /// Retrieves the user's military rank
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>UserMilitaryRankModel</returns>
        IUserMilitaryRankModel GetUserMilitaryRank(int userInfoId);
        /// <summary>
        /// Retrieves the User's military status
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>UserMilitaryStatusModel</returns>
        IUserMilitaryStatusModel GetUserMilitaryStatus(int userInfoId);
        /// <summary>
        /// Retrieves the User's Degrees
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>Container of User Degree Models</returns>
        Container<IUserDegreeModel> GetUserDegree(int userInfoId);
        /// <summary>
        /// Ensure that there is sufficient space in the WebsitModel list
        /// to bind 2 websites to.  If a user has 0 or 1 websites then new
        /// empty models are added to permit adding new websites.
        /// </summary>
        /// <param name="list">List of user websites</param>
        /// <returns>List of user websites with at least 2 entires</returns>
        List<WebsiteModel> EnsureSuffientWebsiteModels(List<WebsiteModel> list);
        /// <summary>
        /// Retrieves the name for the user entity identifier
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>IUserNameResult containing user name</returns>
        IUserNameResult GetuUserName(int userId);
        /// <summary>
        /// Cks the account status.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns></returns>
        bool CkAccountStatus(int userId, out string errorMessage);
        /// <summary>
        /// Cks the answer.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="qId">The q identifier.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns></returns>
        bool CKAnswer(int userId, int qId, string errorMessage);
        #endregion
        #region Saving user profile

        /// <summary>
        /// Master method to save the user profile.  The method verifies the source data was not "stale" (i.e. another
        /// user changed the profile while it was on this users screen) and calls individual methods to save each 
        /// table related to the User object.
        /// 
        /// After all is said and done the modifications are saved.
        /// </summary>
        /// <param name="profileId">User identifier of the profile being updated</param>
        /// <param name="dateTimeStamps">Dictionary of datetime stamps indexed by type to determine if data is stale</param>
        /// <param name="theGeneralInfoModel">General information model</param>
        /// <param name="theWebsiteModels">Website information model</param>
        /// <param name="theInstitutionEmailAddress"></param>
        /// <param name="thePersonalEmailAddress">Institution mail addresses information model</param>
        /// <param name="theAddresses">information model</param>
        /// <param name="w9Addresses">W9 information model</param>
        /// <param name="thePhoneTypeDropdowns">Phone type information model</param>
        /// <param name="theAlternateContactTypeDropdowns">Alternate contact information model</param>
        /// <param name="thePhoneNumberModels">Phone numbers information model</param>
        /// <param name="theAlternateContactPhoneModels">Alternate contact Phone model</param>
        /// <param name="theUserDegreeModels">User degrees information model</param>
        /// <param name="theMilitaryRankModel">MilitaryRankModel - provides the users service and rank</param>
        /// <param name="theMilitarySatatusModel">Military status model - provides the users military status</param>
        /// <param name="theMilitaryServiceId">Military Service identifier</param>
        /// <param name="theUserProfileClientModels">UserClient models</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <param name="myProfile"></param>
        /// <param name="isMyProfile"></param>
        /// <param name="theInsitutionEmailAddress">Institution mail addresses information model</param>
        ICollection<SaveProfileStatus> SaveProfile(int profileId, Dictionary<Type, DateTime> dateTimeStamps, bool W9Verify, IGeneralInfoModel theGeneralInfoModel, IList<WebsiteModel> theWebsiteModels, IEmailAddressModel theInstitutionEmailAddress, IEmailAddressModel thePersonalEmailAddress, IList<AddressInfoModel> theAddress, IProfessionalAffiliationModel theProfessionalAffiliation, IW9AddressModel w9Addresses, ICollection<IListEntry> thePhoneTypeDropdowns, ICollection<IListEntry> theAlternateContactTypeDropdowns, IList<PhoneNumberModel> thePhoneNumberModels, IList<UserAlternateContactPersonModel> theAlternateContactPhoneModels, IList<UserDegreeModel> theUserDegreeModels, IUserMilitaryRankModel theMilitaryRankModel, IUserMilitaryStatusModel theMilitaryStatusModel, int? theMilitaryServiceId, IUserVendorModel theVendorInfoIndividual, IUserVendorModel VendorInfoInstitutional, IList<UserProfileClientModel> theUserProfileClientModels, int userId, bool myProfile, bool isMyProfile);
        /// <summary>
        /// Saves the alternate contact.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="userContact">The user contact.</param>
        /// <param name="userId">The user identifier.</param>
        void SaveAlternateContact(int userInfoId, IUserAlternateContactPersonModel userContact, int userId);
        /// <summary>
        /// Uploads the w9 addresses.
        /// </summary>
        /// <param name="addresses">The addresses.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        ICollection<KeyValuePair<int, SaveAddressStatus>> UploadW9Addresses(IList<UserAddressUploadModel> addresses, int userId);
        /// <summary>
        /// Create a user profile.
        /// </summary>
        /// <param name="userToUpdate">User entity being updated</param>
        /// <param name="userInfo">UserInfo entity being updated</param>
        /// <param name="theGeneralInfoModel">General information model</param>
        /// <param name="theWebsiteModels">Website information model</param>
        /// <param name="theInstitutionEmailAddress"></param>
        /// <param name="thePersonalEmailAddress">Institution mail addresses information model</param>
        /// <param name="theAddresses"> information model</param>
        /// <param name="theProfessionalAffiliation">Professional affiliation model</param>
        /// <param name="theW9Addresses">W9 information model</param>
        /// <param name="thePhoneTypeDropdowns">Phone type information model</param>
        /// <param name="theAlternateContactTypeDropdowns">Alternate contact information model</param>
        /// <param name="thePhoneNumberModels">Phone numbers information model</param>
        /// <param name="theAlternateContactPhoneModels">Alternate contact Phone model</param>
        /// <param name="theUserDegreeModels">User degrees information model</param>
        /// <param name="theMilitaryRankModel">MilitaryRankModel - provides the users service and rank</param>
        /// <param name="theMilitarySatatusModel">Military status model - provides the users military status</param>
        /// <param name="theMilitaryServiceId">Military Service identifier</param>
        /// <param name="theUserProfileClientModels">List of assigned UserClient models</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <param name="isMyProfile">Whether the profile being save is from MyProfile</param>
        /// <param name="profileUserId">User identifier of the created profile</param>
        /// <returns>Collection of SaveProfileStatus enum values or empty collection (signifies no errors)</returns> 
        ICollection<SaveProfileStatus> CreateProfile(IGeneralInfoModel theGeneralInfoModel, IList<WebsiteModel> theWebsiteModels, IEmailAddressModel theInstitutionEmailAddress, IEmailAddressModel thePersonalEmailAddress,
            IList<AddressInfoModel> theAddresses, ProfessionalAffiliationModel theProfessinalAffiliation, IW9AddressModel w9Addresses, 
            ICollection<IListEntry> thePhoneTypeDropdowns, ICollection<IListEntry> theAlternateContactTypeDropdowns, IList<PhoneNumberModel> thePhoneNumberModels, IList<UserAlternateContactPersonModel> theAlternateContactPhoneModels, 
            IList<UserDegreeModel> theUserDegreeModels, IUserMilitaryRankModel theMilitaryRankModel, IUserMilitaryStatusModel theMilitarySatatusModel, int? theMilitaryServiceId, IUserVendorModel theVendorInfoIndividual, IUserVendorModel theVendorInfoInstitutional,
            IList<UserProfileClientModel> theUserProfileClientModels, 
            int userId, bool isMyProfile, ref int? profileUserId);
        #endregion
        #region Participation history
        /// <summary>
        /// Retrieves a list of participation assignments associated with a given user
        /// </summary>
        /// <param name="userInfoId">UserInfo entity identifier</param>
        /// <returns>List of assignment information</returns>
        Container<IUserParticipationHistoryModel> GetParticipationHistory(int userInfoId);
        #endregion
        /// <summary>
        /// Method to ensure a model is initialized
        /// </summary>
        /// <typeparam name="T">WebModel type to create</typeparam>
        /// <param name="model">WebModel from the database</param>
        /// <param name="initializeModel">Method to call to initialize.</param>
        /// <returns>WebModel containing the initialized information</returns>
        T EnsureInitializeModel<T>(T model, Action<T> initializeModel) where T : class, new();
        /// <summary>
        /// Method to ensure there is sufficient buffer space for controls that need to display a minimum
        /// number of entries and there may not be that many entries in the database.
        /// </summary>
        /// <typeparam name="T">WebModel type to create</typeparam>
        /// <param name="list">WebModel list returned from the database</param>
        /// <param name="minimuimEntries">Minimum number of entries to have</param>
        /// <param name="initializeModel">Method to call to initialize.</param>
        /// <returns>List of WebModels containing the minimum number of entries</returns>
        /// <remarks>
        /// The initialization method should look like this:
        /// 
        //      public void initializeModel(WebsiteModel model)
        //      {
        //          model.WebsiteTypeId = WebsiteType.SecondaryWebsiteTypeId;
        //       }
        /// </remarks>
        /// <remarks>need unit test</remarks>
        List<T> EnsureSuffientModels<T>(List<T> list, int minimuimEntries, Action<T> initializeModel) where T : class, new();
        /// <summary>
        /// Returns the UserInfo entity identifier for the specified User entity.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>UserInfo entity identifier if user located; null otherwise</returns>
        int GetUserInfoId(int userId);
        /// <summary>
        /// Returns the UserId for a specified UserInfo entity
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>User entity identifier</returns>
        int GetUserId(int userInfoId);
        /// <summary>
        /// Retrieve a user's resume information.
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>ResumeModel describing the user's resume</returns>
        IResumeModel GetUserResume(int userInfoId);
        /// <summary>
        /// Retrieve a user's professional affiliation
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>ProfessionalAffiiationModel for the user</returns>
        IProfessionalAffiliationModel GetUserProfessionalAffiliation(int userInfoId);
        /// <summary>
        /// Saves a resume file & updates the associated entity objects.
        /// </summary>
        /// <param name="service">File service object</param>
        /// <param name="stream">Stream from HttpPostedFileBase</param>
        /// <param name="uploadedFileName">Filename from HttpPostedFileBase</param>
        /// <param name="userInfoId">User's UserInfo entity identifier</param>
        /// <param name="resumeid">Resume entity identifier</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns>Collection of SaveResumeStatus indicators</returns>
        /// <remarks>needs unit testing</remarks>
        ISaveResumeResults Save(Files.IFileService service, Stream stream, string uploadedFileName, int userInfoId, int resumeid, int userId);
        /// <summary>
        /// Retrieve a user's CV (resume)
        /// </summary>
        /// <param name="fileService">The FileService</param>
        /// <param name="resumeId">Resume entity identifier</param>
        /// <returns>Byte array containing resume</returns>
        /// <remarks>needs unit tests</remarks>
        byte[] RetrieveCV(Files.IFileService fileService, int resumeId);
        /// <summary>
        /// Saves user security questions and answers
        /// </summary>
        /// <param name="model">The user manage password model containing the questions and answers</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="theMailService">The MailService</param>
        /// <returns>SaveSecurityQuestionStatus object indicating update status</returns>
        SaveSecurityQuestionStatus SaveSecurityQuestions(IUserManagePasswordModel model, int userId, IMailService theMailService);
        /// <summary>
        /// Update a password & send out the necessary emails when the user is authenticated by security questions.
        /// </summary>
        /// <param name="theMailService">The MailService</param>
        /// <param name="userEntityId">User entity identifier</param>
        void UpdatePasswordWhenSecurityQuestionsUsed(IMailService theMailService, int userEntityId);
        /// <summary>
        /// Set the user account status to temporary credentials
        /// </summary>
        /// <param name="targetUserId">The user whose status is being set to awaiting credentials</param>
        /// <param name="userId">The identifier of the user making the change</param>
        void SetUserAccountStatusTemporaryPassword(User targetUser, int userId);
        /// <summary>
        /// Set the user account status to awaiting credentials
        /// </summary>
        /// <param name="targetUserId">The user identifier whose status is being set to awaiting credentials</param>
        /// <param name="userId">The identifier of the user making the change</param>
        void SetUserAccountStatusAwaitingCredentials(int targetUserId, int userId);
        /// <summary>
        /// Set the user account status to expired
        /// </summary>
        /// <param name="targetUserId">The user whose status is being set to expired</param>
        /// <param name="userId">The identifier of the user making the change</param>
        void SetInvitationExpired(int targetUserId, int userId);
        /// <summary>
        /// Determines if the users password expired and updates user account status accordingly 
        /// </summary>
        /// <param name="userId">The user identifier of the user making the change</param>
        /// <returns>Returns true if password is expired, false otherwise</returns>
        bool IsPasswordExpired(int userId);
        /// <summary>
        /// Gets the days until password expiration, taking into account the most recent password policy release date and subsequent grace period
        /// </summary>
        /// <param name="passwordAge"></param>
        /// <returns></returns>
        int EffectiveDaysUntilPasswordExpiration(int passwordAge);
        /// <summary>
        /// Gets the users name and primary email.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Model containing users name and primary email.</returns>
        IUserInfoSmallModel GetUsersNameAndPrimaryEmail(int userId);
        #region Recruitment Blocking
        /// <summary>
        /// Gets the user client blocks.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="availableClientIds">The available client ids.</param>
        /// <returns></returns>
        List<KeyValuePair<int, string>> GetUserClientBlocks(int userInfoId, List<int> availableClientIds);
        /// <summary>
        /// Updates the user client block.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="blockedClientIds">The blocked client ids.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        bool UpdateUserClientBlock(int userInfoId, List<int> blockedClientIds, string comment, int userId);
        /// <summary>
        /// Gets the user client block logs.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        List<IUserClientBlockLog> GetUserClientBlockLogs(int userInfoId);
        #endregion
        /// <summary>
        /// Gets the individual vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        UserVendorModel GetVendorId(int userInfoId, bool intVendorId);
        /// <summary>
        /// Saves the user vendor identifier.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="vendorName">Name of the vendor.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="indVendorId">if set to <c>true</c> [ind vendor identifier].</param>
        /// <returns></returns>
        UserVendorModel SaveUserVendorId(int userInfoId, string vendorId, string vendorName, int userId, bool indVendorId);
        /// <summary>
        /// Saves the user vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="vendorName">Name of the vendor.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="indVendorId">if set to <c>true</c> [ind vendor identifier].</param>
        /// <param name="saveThis">if set to <c>true</c> [save this].</param>
        /// <returns></returns>
        UserVendorModel SaveUserVendor(int userInfoId, string vendorId, string vendorName, int userId, bool indVendorId, bool saveThis);
        /// <summary>
        /// check if vendor id is for this user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        bool UserIdNotVendorId(int? userId, string vendorId);
        #region User client
        /// <summary>
        /// Get the clients assigned to this user
        /// </summary>
        /// <param name="userId">The user identifier of to search for client assignment to</param>
        /// <returns>Collection of UserProfileClientModel for those clients assigned to the supplied user</returns>
        Container<UserProfileClientModel> GetAssignedUserProfileClient(int userId);
        /// <summary>
        /// Retrieve the active clients that are assigned to the user.
        /// </summary>
        /// <param name="userId">The user identifier of to search for client assignment to</param>
        /// <returns>Collection of UserProfileClientModels for those active clients assigned to the supplied user</returns>
        Container<UserProfileClientModel> GetAssignedActiveClients(int userId);
        /// <summary>
        /// Get a list of available clients.
        /// </summary>
        /// <returns>Collection of UserProfileClientModel for those clients not assigned to the supplied user</returns>
        Container<UserProfileClientModel> GetAvailableUserProfileClient();
        /// <summary>
        /// Checks for a duplicate email (institutional or personal) is provided.
        /// </summary>
        /// <param name="target">Email address to check</param>
        /// <param name="userInfoId">UserInfo entity identifier</param>
        /// <returns>True if the email address exists; false otherwise</returns>
        bool IsDuplicateEmailAddress(string target, int userInfoId);
        /// <summary>
        /// Determines whether [is duplicate individual vendor identifier] [the specified target].
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is duplicate individual vendor identifier] [the specified target]; otherwise, <c>false</c>.
        /// </returns>
        bool IsDuplicateVendorId(string target, int userInfoId, bool indVendorId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theLookupService">The Lookup Service</param>
        /// <param name="userInfoId">UserInfo entity identifier</param>
        /// <returns></returns>
        UserManageAccountModel GetUserManageAccount(ILookupService theLookupService, int userInfoId);

        #endregion
        #region Password/Security
        /// <summary>
        /// Retrieves user Password/Security information
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>user manage password/security information</returns>
        IUserManagePasswordModel GetUserPasswordAndSecurityQuestionInfo(int userId);
        /// <summary>
        /// Get Random Security Question
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>object containing recovery question information</returns>
        IUserRecoveryQuestionModel GetRandomSecurityQuestion(int userId);
        /// <summary>
        /// Does the user have security questions
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>true if the user has entered security questions, false otherwise</returns>
        bool HasSecurityQuestions(int userId);
        /// <summary>
        /// Checks if a provided password matches the user's current password in the db
        /// </summary>
        /// <param name="passwordToCheck"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool DoesMatchCurrentPassword(string passwordToCheck, int userId);
        /// <summary>
        /// Checks if a provided password matches the user's previous passwords
        /// </summary>
        /// <param name="passwordToCheck"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool DoesMatchPreviousPasswords(string passwordToCheck, int userId);
        /// <summary>
        /// Sets the account status to locked for the indicated user
        /// </summary>
        /// <param name="targetUserId">The user identifier of the user to be locked</param>
        /// <param name="userId">The user identifier of the user initiating the locked action</param>
        void Lock(int targetUserId, int userId);
        /// <summary>
        /// Sets the account status to locked for the indicated user
        /// </summary>
        /// <param name="targetUserId">The user identifier of the user to be unlocked</param>
        /// <param name="userId">The user identifier of the user initiating the locked action</param>
        IReactivateDeactivateResult Unlock(int targetUserId, int userId);
        #endregion
        #region Credentials
        /// <summary>
        /// Service method to send the user new credentials
        /// </summary>
        /// <param name="theMailService">The MailService</param>
        /// <param name="targetUser">Targeted user</param>
        /// <param name="userId">User sending the credentials</param>
        /// <returns>Mail status</returns>
        MailService.MailStatus SendNewCredentials(IMailService theMailService, int targetUser, int userId);
        /// <summary>
        /// Service method to send the existing new credentials
        /// </summary>
        /// <param name="theMailService">The MailService</param>
        /// <param name="targetUser">Targeted user</param>
        /// <param name="userId">User sending the credentials</param>
        /// <returns>Mail status</returns>
        MailService.MailStatus ResendCredentials(IMailService theMailService, int targetUser, int userId);
        /// <summary>
        /// Service method to determine if the user can be sent credentials
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>returns true if credentials can be sent, false otherwise</returns>
        bool IsSendCredentialsEnabled(int userId);
        /// <summary>
        /// Service method to determine if credentials can be sent, 
        /// when the user is initially created, for the indicated profile type 
        /// </summary>
        /// <param name="profileTypeId">The profile type identifier</param>
        /// <returns>returns true if credentials can be sent for this profile type, false otherwise</returns>
        bool IsSendCredentialsEnabledAtUserCreation(int profileTypeId);
        /// <summary>
        /// Get the current readable account status reason for this user
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <returns>The readable account status reason</returns>
        string GetUserAccountStatusName(int userId);
        /// <summary>
        /// Get the date of the last change to the user's account status
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <returns>The date of the last change to the account status</returns>
        DateTime? GetUserAccountStatusDate(int userId);
        /// <summary>
        /// Retrieves information about who sent the credentials & constructs a container to return
        /// the information
        /// </summary>
        /// <param name="userId">User entity identifier of user who sent the information</param>
        /// <returns></returns>
        /// 
        IUserManageAccountModel WhoSentCredentials(int userId);
        /// <summary>
        /// Does the user have permanent credentials
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>true if the user is active with permanent credentials, false otherwise</returns>
        bool IsPermanentCredentials(int userId);
        #endregion        
        #region Activate/Deactivate
        /// <summary>
        /// Deactivate a user's account.
        /// </summary>
        /// <param name="targetUserId">User entity identify of user being deactivate</param>
        /// <param name="accountStatusReasonId">AccountStatusReason entity identifier of reason why account is being deactivated</param>
        /// <param name="userId">User entity identifier of user who deactivated the account/param>
        /// <returns>Model containing information for screen refresh</returns>
        IReactivateDeactivateResult DeActivate(int targetUserId, int accountStatusReasonId, int userId);
        /// <summary>
        /// Activate a user's account.
        /// </summary>
        /// <param name="targetUserId">User entity identify of user being activated</param>
        /// <param name="accountStatusReasonId">AccountStatusReason entity identifier of reason why account is being activated</param>
        /// <param name="userId">User entity identifier of user who activated the account/param>
        /// <returns>Model containing information for screen refresh</returns>
        IReactivateDeactivateResult Reactivate(int targetUserId, int userId);
        #endregion
        #region Roles
        /// <summary>
        /// Retrieve the user's role order within the user's profile.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Users role priority order; null if no role priority order</returns>
        int? GetUsersSystemPriorityOrder(int userId);
        /// <summary>
        /// Retrieve the user's ProfileType entity identifier.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Users ProfileType entity identifier</returns>
        int GetUsersProfileType(int userId);
        /// <summary>
        /// Is the user's ProfileType Reviewer.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>True if user is a reviewer, false otherwise</returns>
        bool IsReviewer(int userId);
        /// <summary>
        /// Determines if the user's role is as an SRO or RTA.
        /// </summary>
        /// <param name="userId">User Entity identifier</param>
        /// <returns>True if the user is an SRO or RTA; false otherwise</returns>
        bool IsSroRta(int userId);
        /// <summary>
        /// Determines whether the specified user identifier is sro.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified user identifier is sro; otherwise, <c>false</c>.
        /// </returns>
        bool IsSro(int userId);
        #endregion
        #region Verify
        /// <summary>
        /// Is the user's w9 verified by the user
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>true if verified, false otherwise</returns>
        bool? IsW9Verified(int userId);
        /// <summary>
        /// Is the user's W9 missing
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>true if it is missing, false otherwise</returns>
        bool IsW9Missing(int userId);
        /// <summary>
        /// Is the user profile verified
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>true if profile is verified, false otherwise</returns>
        bool IsUserProfileVerified(int userId);
        // <summary>
        /// Checks if the user's W-9 has been updated.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>True if the user's W-9 has been updated; false otherwise</returns>
        bool IsW9Updated(int userId);
        /// <summary>
        /// Is W9 verification required for this user
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>True if the user is a reviewer and the W9 has not been verified</returns>
        bool IsW9VerificationRequired(int userId);
        /// <summary>
        /// Is user profile verification required
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns></returns>
        bool IsProfileVerificationRequired(int userId);
        #endregion


    }
}
