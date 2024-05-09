using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;

namespace Sra.P2rmis.Bll.UserProfileManagement
{
    /// <summary>
    /// UserProfileManagementService provides services to perform business related functions for
    /// the User Profile Management Application. (Search Users; Create Profiles, Merge Profiles etc.)
    /// </summary>
    public partial class UserProfileManagementService : ServerBase, IUserProfileManagementService
    {
        #region Constants
        /// <summary>
        /// What version to use for the previous version when there was not a previous version
        /// </summary>
        private const int NoResumeVersionNumber = 0;
        /// <summary>
        /// Minimum number of websites to return in the view model.
        /// </summary>
        private const int MinimumNumberOfUserWebsites = 2;
        #endregion
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public UserProfileManagementService()
        {
            UnitOfWork = new UnitOfWork();
        }
        public UserProfileManagementService(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    #endregion
    #region Services Provided
    #region User Search
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
    public Container<IFoundUserModel> SearchUser(string firstName, string lastName, string email, string userName, int? userId, string vendorId)
        {
            ValidateSearchUserParameters(firstName, lastName, email, userName, userId, vendorId);

            Container<IFoundUserModel> container = new Container<IFoundUserModel>();
            var results = UnitOfWork.UserRepository.FindUser(firstName, lastName, email, userName, userId, vendorId);
            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Performs a fuzzy search on supplied user information.  This version is used when searching to create
        /// a user.
        /// </summary>
        /// <param name="firstName">User's first name</param>
        /// <param name="lastName">User's last name</param>
        /// <param name="email">User's email</param>
        /// <exception cref="ArgumentException">Thrown if all parameters are null or empty string</exception>
        /// <returns>Container of IFoundUserModels that matched the criteria</returns>
        public Container<IFoundUserModel> SearchUser(string firstName, string lastName, string email)
        {
            ValidateSearchUserParameters(firstName, lastName, email);

            Container<IFoundUserModel> container = new Container<IFoundUserModel>();
            var results = UnitOfWork.UserRepository.FindUser(firstName, lastName, email);
            container.SetModelList(results);

            return container;
        }
        #endregion
        #region Profile Based User Searches
        /// <summary>
        /// Get user websites
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>Container of IWebsiteModel</returns>
        public Container<WebsiteModel> GetUserWebsite(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetUserWebsite", "userInfoId");

            Container<WebsiteModel> container = new Container<WebsiteModel>();

            var results = UnitOfWork.UserInfoRepository.GetByID(userInfoId);
            var listOfObjects = results.UserWebsites.Where(x => x.UserInfoId == userInfoId).Select(x => new WebsiteModel(x.UserWebsiteId, x.WebsiteTypeId, x.WebsiteAddress, x.WebsiteTypeId == WebsiteType.PrimaryWebsiteTypeId));

            container.ModelList = listOfObjects;

            return container;
        }
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IUserModel GetUser(int userId)
        {
            var user = UnitOfWork.UserRepository.GetByID(userId);
            var userInfo = user.UserInfoes.FirstOrDefault();
            var model = new UserModel(user.UserID, userInfo.UserInfoID, userInfo.FullUserName, user.UserLogin, user.LastLoginDate, user.GetUserSystemRoleName());
            return model as IUserModel;
        }
        /// <summary>
        /// Get general user information
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>IGeneralInfoModel object</returns>
        public IGeneralInfoModel GetUserGeneralInfo(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetUserGeneralInfo", "userInfoId");

            var results = UnitOfWork.UserInfoRepository.GetByID(userInfoId);
            GeneralInfoModel generalInfo = null;
            if (results != null)
            {
                GetUsersRoleResult roleResult = GetUsersRole(results.UserID);
                generalInfo = new GeneralInfoModel(results.FirstName, (string.IsNullOrWhiteSpace(results.MiddleName)) ? string.Empty : results.MiddleName.Substring(0, 1),
                                                        results.LastName, results.NickName, results.User.UserLogin, results.BadgeName, results.PrefixId,
                                                        (results.SuffixText == null) ? string.Empty : results.SuffixText,
                                                        results.ModifiedDate, results.EthnicityId, results.GenderId, results.UserProfiles.FirstOrDefault().ProfileTypeId, 
                                                        results.UserID, userInfoId, results.UserProfiles.FirstOrDefault().ProfileType.ProfileTypeName, 
                                                        roleResult.SystemRoleId, roleResult.UserSystemRoleId, roleResult.RoleOrder,results.AcademicRankId,
                                                        results.DegreeNotApplicable, results.Expertise, roleResult.IsClient);
            }

            return generalInfo;
        }
        /// <summary>
        /// Cks the account status.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns></returns>
        public bool CkAccountStatus(int userId, out string errorMessage)
        {
            var membershipRepo = new MembershipRepository();
            return membershipRepo.CkAccountStatus(userId, out errorMessage);
        }
        /// <summary>
        /// Cks the answer.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="qId">The q identifier.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns></returns>
        public bool CKAnswer(int userId, int qId, string errorMessage)
        {
            var membershipRepo = new MembershipRepository();
            return membershipRepo.CkAnswer(userId, qId, errorMessage);
        }
        /// <summary>
        /// Container to return results concerning the user's role.
        /// </summary>
        internal class GetUsersRoleResult 
        {
            #region Constructor & set up
            /// <summary>
            /// Default constructor
            /// </summary>
            public GetUsersRoleResult() { }
            /// <summary>
            /// Instantiate a GetUsersRoleResult with data.
            /// </summary>
            /// <param name="role">Role object</param>
            /// <param name="systemRoleId">SystemRole entity identifier</param> 
            /// <param name="userSystemRoleId">UserSystemRole entity identifier</param>
            /// <param name="roleOrder">SystemRole entity identifier</param>
            /// <param name="roleName">Readable role name</param>
            /// <param name="isClient">Indicates the role is a client role</param>
            public void Populate(SystemRole role, int? systemRoleId, int? userSystemRoleId, int? roleOrder, string roleName, bool isClient)
            {
                this.Role = role;
                this.SystemRoleId = systemRoleId;
                this.UserSystemRoleId = userSystemRoleId;
                this.RoleOrder = roleOrder;
                this.RoleName = roleName;
                this.IsClient = isClient;
            }
            #endregion
            #region Attributes
            /// <summary>
            /// User's SystemRole
            /// </summary>
            public SystemRole Role { get; private set; }
            /// <summary>
            /// The UserSystemRole entity identifier
            /// </summary>
            public int? SystemRoleId { get; private set; }
            /// <summary>
            /// The UserSystemRole identifier
            /// </summary>
            public int? UserSystemRoleId { get; private set; }
            /// <summary>
            /// The role's order
            /// </summary>
            public int? RoleOrder { get; private set; }
            /// <summary>
            /// Readable role name
            /// </summary>
            public string RoleName { get; private set; }
            /// <summary>
            /// Indicates the user is a Client
            /// </summary>
            public bool IsClient { get; private set; }
            #endregion
        }
        //
        // This will need updated with the correct names when john is complete.
        //
        /// <summary>
        /// Retrieves the user's role.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>SystemRole </returns>
        /// <remarks>
        /// We are making the following assumptions:
        ///  1. There will always be a role.
        ///  2. A user can only have a single role.
        /// </remarks>
        private GetUsersRoleResult GetUsersRole(int userId)
        {
            //
            // The user's role is determined by an entry in the UserSystemRole table.  So get the link entry.  There will only be one.
            //
            UserSystemRole userSystemRoleEntity = UnitOfWork.UserSystemRoleRepository.Get(x => x.UserID == userId).FirstOrDefault();
            User userEntity = UnitOfWork.UofwUserRepository.GetByID(userId);
            //
            // Since we have the link table entry, now we can get the SystemRole.
            //
            GetUsersRoleResult result = new GetUsersRoleResult(); 
            if (userSystemRoleEntity != null)
            {
                SystemRole systemRoleEntity = UnitOfWork.SystemRoleRepository.GetByID(userSystemRoleEntity.SystemRoleId);
                result.Populate(systemRoleEntity, systemRoleEntity.SystemRoleId, userSystemRoleEntity.UserSystemRoleID, systemRoleEntity.SystemPriorityOrder, systemRoleEntity.SystemRoleName, userEntity.IsClient());
            }

            return result;
        }
        /// <summary>
        /// retrieves user email addresses
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns></returns>
        public Container<EmailAddressModel> GetUserEmailAddress(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetUserEmailAddress", "userInfoId");

            Container<EmailAddressModel> container = new Container<EmailAddressModel>();

            var results = UnitOfWork.UserInfoRepository.GetByID(userInfoId);
            var listOfObjects = results.UserEmails.Where(x => x.UserInfoID == userInfoId).Select(x => new EmailAddressModel(x.EmailID, x.EmailAddressTypeId, x.Email, x.PrimaryFlag));

            container.ModelList = listOfObjects;

            return container;
        }
        /// <summary>
        /// retrieves user institutional email address
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>EmailAddressModel</returns>
        public IEmailAddressModel GetInstitutionalUserEmailAddress(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetInstitutionalUserEmailAddress", "userInfoId");

            return GetUserEmailAddress(userInfoId, EmailAddressType.Business);
        }
        /// <summary>
        /// retrieves primary user institutional email address
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>EmailAddressModel</returns>
        public IEmailAddressModel GetPrimaryInstitutionalUserEmailAddress(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetInstitutionalUserEmailAddress", "userInfoId");

            return GetUserEmailAddress(userInfoId, EmailAddressType.Business, true);
        }

        /// <summary>
        /// retrieves user personal email address
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>EmailAddressModel</returns>
        public IEmailAddressModel GetPersonalUserEmailAddress(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetPersonalUserEmailAddress", "userInfoId");

            return GetUserEmailAddress(userInfoId, EmailAddressType.Personal);
        }
        /// <summary>
        /// retrieves user alternate email address
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>EmailAddressModel</returns>
        public IEmailAddressModel GetAlternateUserEmailAddress(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetPersonalUserEmailAddress", "userInfoId");

            return GetUserEmailAddress(userInfoId, EmailAddressType.Alternate);
        }
        /// <summary>
        /// Retrieves institutional addresses
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>Container of institutional addresses</returns>
        public Container<IInstitutionAddressModel> GetInstitutionalAddresses(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetInstitutionalAddresses", "userInfoId");
  
            Container<IInstitutionAddressModel> container = new Container<IInstitutionAddressModel>();
            var results = UnitOfWork.UserInfoRepository.GetByID(userInfoId);

            var listOfObjects = results.UserAddresses.Where(x => x.UserInfoID == userInfoId && x.AddressTypeId == AddressType.Indexes.Organization)
                .Select(x => new InstitutionAddressModel
                {
                    AddressId = x.AddressID,
                    Address = new AddressInfoModel {
                        UserAddressId = x.AddressID,
                        Address1 = x.Address1,
                        Address2 = x.Address2,
                        Address3 = x.Address3,
                        Address4 = x.Address4,
                        City = x.City,
                        State = (x.State != null) ? x.State.StateName : string.Empty,
                        StateId = x.StateId,
                        Zip = x.Zip,
                        Country = (x.Country != null) ? x.Country.CountryName : string.Empty,
                        CountryId = x.CountryId,
                        PrimaryFlag = x.PrimaryFlag,
                        ModifiedDate = x.ModifiedDate
                    }
                });

            container.ModelList = listOfObjects;

            return container;
        }
        /// <summary>
        /// Retrieves personal addresses
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>Container of personal addresses</returns>
        public Container<IAddressInfoModel> GetPersonalAddresses(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetPersonalAddresses", "userInfoId");
  
            Container<IAddressInfoModel> container = new Container<IAddressInfoModel>();
            var results = UnitOfWork.UserInfoRepository.GetByID(userInfoId);

            var listOfObjects = results.UserAddresses.Where(x => x.UserInfoID == userInfoId && x.AddressTypeId == AddressType.Indexes.Personal)
                .Select(x => new AddressInfoModel {
                    UserAddressId = x.AddressID,
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    Address3 = x.Address3,
                    Address4 = x.Address4,
                    City = x.City,
                    State = (x.State != null) ? x.State.StateName : string.Empty,
                    StateId = x.StateId,
                    Zip = x.Zip,
                    Country = (x.Country != null) ? x.Country.CountryName : string.Empty,
                    CountryId = x.CountryId,
                    PrimaryFlag = x.PrimaryFlag,
                    ModifiedDate = x.ModifiedDate
                });

            container.ModelList = listOfObjects;

            return container;
        }
        /// <summary>
        /// Retrieves organizational and personal addresses
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>Container of personal and organizational addresses</returns>
        public Container<IAddressInfoModel> GetOrganizationalPersonalAddresses(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetOrganizationalPersonalAddresses", "userInfoId");

            Container<IAddressInfoModel> container = new Container<IAddressInfoModel>();
            var results = UnitOfWork.UserInfoRepository.GetByID(userInfoId);

            var listOfObjects = results.UserAddresses.Where(x => x.UserInfoID == userInfoId && (x.AddressTypeId == AddressType.Indexes.Personal || x.AddressTypeId == AddressType.Indexes.Organization))
                .Select(x => new AddressInfoModel
                {
                    AddressTypeId = x.AddressTypeId,
                    UserAddressId = x.AddressID,
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    Address3 = x.Address3,
                    Address4 = x.Address4,
                    City = x.City,
                    State = (x.State != null) ? x.State.StateName : string.Empty,
                    StateId = x.StateId,
                    Zip = x.Zip,
                    Country = (x.Country != null) ? x.Country.CountryName : string.Empty,
                    CountryId = x.CountryId,
                    PrimaryFlag = x.PrimaryFlag,
                    ModifiedDate = x.ModifiedDate
                });

            container.ModelList = listOfObjects;

            return container;
        }

        /// <summary>
        /// Retrieves W9 addresses
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>W9 address information</returns>
        public IW9AddressModel GetW9Addresses(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetW9Addresses", "userInfoId");
            IW9AddressModel model = new W9AddressModel();
            
            var results = UnitOfWork.UserInfoRepository.GetByID(userInfoId);

            var listOfObjects = results.UserAddresses.Where(x => x.UserInfoID == userInfoId && x.AddressTypeId == AddressType.Indexes.W9)
                .Select(x => new AddressInfoModel
                {
                    UserAddressId = x.AddressID,
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    City = x.City,
                    State = (x.State != null) ? x.State.StateName : string.Empty,
                    StateId = x.StateId,
                    Zip = x.Zip,
                    Country = (x.Country != null) ? x.Country.CountryName : string.Empty,
                    CountryId = x.CountryId,
                    PrimaryFlag = x.PrimaryFlag,
                    ModifiedDate = x.ModifiedDate
                });


            model.Address = listOfObjects.FirstOrDefault();
            model.W9AddressExists = model.Address != null;
            model.VendorName = results.VendorName();
            model.VendorId = results.VendorId();
            model.W9Verified = results.User.W9Verified;

            return model;
        }
        /// <summary>
        /// Retrieves user phones
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>Container of phone number model</returns>
        public Container<PhoneNumberModel> GetUserPhones(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetAlternativeContacts", "userInfoId");

            Container<PhoneNumberModel> container = new Container<PhoneNumberModel>();
            var results = UnitOfWork.UserInfoRepository.GetByID(userInfoId);

            var listOfObjects = results.UserPhones.Where(x => x.UserInfoID == userInfoId)
                .Select(x => new PhoneNumberModel
                {
                    PhoneId = x.PhoneID,
                    PhoneTypeId = x.PhoneTypeId,
                    Number = x.Phone,
                    Primary = (x.PrimaryFlag != null) ? (bool)x.PrimaryFlag : false,
                    International = x.International ?? false
                });

            container.ModelList = listOfObjects;

            return container;
        }

        /// <summary>
        /// Retrieves alternative contacts
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>Container of alternative contacts</returns>
        public Container<IUserAlternateContactPersonModel> GetAlternativeContactPersons(int userInfoId)
        {
            ValidateInteger(userInfoId, "UserProfileManagementService.GetAlternativeContacts", "userInfoId");

            Container<IUserAlternateContactPersonModel> container = new Container<IUserAlternateContactPersonModel>();

            var results = UnitOfWork.UserInfoRepository.GetByID(userInfoId);

            if (results.UserAlternateContacts != null)
            {

                var listOfObjects = results.UserAlternateContacts.Select(x => new UserAlternateContactPersonModel
                {
                    UserAlternateContactId = x.UserAlternateContactId,
                    UserAlternateContactTypeId = x.AlternateContactTypeId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.EmailAddress,
                    PrimaryFlag = x.PrimaryFlag,
 
                    AlternateContactPhone = x.UserAlternateContactPhones.Select(z => new UserAlternateContactPersonPhoneModel
                    {
                        UserAlternateContactPhoneId = z.UserAlternateContactPhoneId,
                        PhoneTypeId = z.PhoneTypeId,
                        Number = z.PhoneNumber,
                        Extension = z.PhoneExtension,
                        International = z.International,
                        PrimaryFlag = z.PrimaryFlag
                    }).ToList<UserAlternateContactPersonPhoneModel>()
                });

                container.ModelList = listOfObjects;
            }

            return container;
        }
        /// <summary>
        /// Gets the emergency contact person.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        public IUserAlternateContactPersonModel GetEmergencyContactPerson(int userInfoId)
        {
            ValidateInteger(userInfoId, "UserProfileManagementService.GetEmergencyContactPerson", "userInfoId");
            var container = GetAlternativeContactPersons(userInfoId);

            var em = container.ModelList?.FirstOrDefault(x => x.UserAlternateContactTypeId == LookupService.LookupEmContactTypeId);
            if(em == null)
            {
                return container.Model;
            }
            else
            {
                return em;
            }
        }
        /// <summary>
        /// Retrieves the user's military rank
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>UserMilitaryRankModel</returns>
        public IUserMilitaryRankModel GetUserMilitaryRank(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetUserMilitaryRank", "userInfoId");

            var results = UnitOfWork.UserInfoRepository.GetByID(userInfoId);

            UserMilitaryRankModel rank;

            if (results != null && results.MilitaryRank != null)
            {
                rank = new UserMilitaryRankModel(results.MilitaryRankId, results.MilitaryRank.MilitaryRankAbbreviation, results.MilitaryRank.MilitaryRankName, results.MilitaryRank.Service);
            }
            else
            {
                rank = new UserMilitaryRankModel();
            }

            return rank;
        }
        /// <summary>
        /// Retrieves the User's military status
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>UserMilitaryStatusModel or null if no military status</returns>
        public IUserMilitaryStatusModel GetUserMilitaryStatus(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetUserMilitaryStatus", "userInfoId");

            var results = UnitOfWork.UserInfoRepository.GetByID(userInfoId);

            UserMilitaryStatusModel status = new UserMilitaryStatusModel();
            
            if (results.MilitaryStatusType !=  null)
            {
                status.MilitaryStatusTypeId = results.MilitaryStatusType.MilitaryStatusTypeId;
                status.MilitaryStatus = results.MilitaryStatusType.StatusType;
            }
            //else
            //{
            //    status = null;
            //}

            return status;
        }
        /// <summary>
        /// Retrieves the User's Degrees
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns>Container of User Degree Models</returns>
        public Container<IUserDegreeModel> GetUserDegree(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetUserDegree", "userInfoId");

            Container<IUserDegreeModel> container = new Container<IUserDegreeModel>();
            var results = UnitOfWork.UserInfoRepository.GetByID(userInfoId);

            var listOfObjects = results.UserDegrees.Where(x => x.UserInfoId == userInfoId)
                .Select(x => new UserDegreeModel
                {
                    UserDegreeId = x.UserDegreeId,
                    DegreeId = x.DegreeId,
                    DegreeName = x.Degree.DegreeName,
                    Major = x.Major
                })
                .OrderBy(o => o.DegreeName);

            container.ModelList = listOfObjects;
            return container;

        }
        /// <summary>
        /// Retrieve a user's resume information.
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>ResumeModel describing the user's resume</returns>
        /// <remarks>needs unit tests</remarks>
        public IResumeModel GetUserResume(int userInfoId)
        {
            ValidateParameter(userInfoId, "UserProfileManagementService.GetUserResume", "userInfoId");

            UserInfo userInfoEntity = UnitOfWork.UserInfoRepository.GetByID(userInfoId);

            UserResume userResumeEntity = userInfoEntity.UserResumes.DefaultIfEmpty(UserResume.Default).FirstOrDefault();

            IResumeModel model = new ResumeModel(userResumeEntity.FileName, userResumeEntity.UserResumeId);

            return model;
        }
        /// <summary>
        /// Retrieve a user's professional affiliation
        /// </summary>
        /// <param name="userInfoId">The user info identifier</param>
        /// <returns>ProfessionalAffiiationModel for the user</returns>
        public IProfessionalAffiliationModel GetUserProfessionalAffiliation(int userInfoId)
        {
            ValidateParameter(userInfoId, "UserProfileManagementService.GetUserProfessionalAffiliation", "userInfoId");

            UserInfo userInfoEntity = UnitOfWork.UserInfoRepository.GetByID(userInfoId);

            ProfessionalAffiliationModel model = new ProfessionalAffiliationModel(userInfoEntity.ProfessionalAffiliationId, userInfoEntity.Institution, userInfoEntity.Department, userInfoEntity.Position);

            return model;
        }
        /// <summary>
        /// Ensure that there is sufficient space in the WebsitModel list
        /// to bind 2 websites to.  If a user has 0 or 1 websites then new
        /// empty models are added to permit adding new websites.
        /// </summary>
        /// <param name="list">List of user websites</param>
        /// <returns>List of user websites with at least 2 entires</returns>
        public List<WebsiteModel> EnsureSuffientWebsiteModels(List<WebsiteModel> list)
        {
            while (list.Count() < MinimumNumberOfUserWebsites)
            {
                if (list.Count() == 0 || list[0].WebsiteTypeId == WebsiteType.SecondaryWebsiteTypeId)
                {
                    list.Add(new WebsiteModel { WebsiteTypeId = WebsiteType.PrimaryWebsiteTypeId, Primary = true });
                }
                else
                {
                    list.Add(new WebsiteModel { WebsiteTypeId = WebsiteType.SecondaryWebsiteTypeId, Primary = false });
                }
            }
            return list.OrderBy(x => x.WebsiteTypeId).ToList();
        }
        /// <summary>
        /// Method to ensure a model is initialized
        /// </summary>
        /// <typeparam name="T">WebModel type to create</typeparam>
        /// <param name="model">WebModel from the database</param>
        /// <param name="initializeModel">Method to call to initialize.</param>
        /// <returns>WebModel containing the initialized information</returns>
        public T EnsureInitializeModel<T>(T model, Action<T> initializeModel) where T : class, new()
        {
            T temp = model;
            if (model == null)
            {
                temp = new T();
                initializeModel(temp);
            }
            return temp;
        }
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
        public List<T> EnsureSuffientModels<T>(List<T> list, int minimuimEntries, Action<T> initializeModel) where T : class, new()
        {
            //
            // If there are insufficient entries
            //
            while (list.Count() < minimuimEntries)
            {
                //
                // Create a WebModel; initialize it and add it to the list
                //
                T temp = new T();
                initializeModel(temp);
                list.Add(temp);
            }
            return list;
        }
        /// <summary>
        /// Returns the UserInfo entity identifier for the specified User entity.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>UserInfo entity identifier if user located; null otherwise</returns>
        /// <exception cref="NullReferenceException">Thrown if userId is not an identifier of an User entity </exception>
        /// <remarks>needs unit tests</remarks>
        /// <remarks>Unit tests were attempted.  However Rhinomocks was not retaining the UserInfo object that was added to the User object</remarks>
        public int GetUserInfoId(int userId)
        {
            ValidateParameter(userId, "UserProfileManagementService.GetUserInfoId", "userId");

            return GetUserById(userId).UserInfoes.FirstOrDefault().UserInfoID;
        }
        /// <summary>
        /// Returns the User info entity identifier for the specified UserInfo entity.
        /// </summary>
        /// <param name="userInfoId">User info entity identifier</param>
        /// <returns>UserInfo entity identifier if user located; null otherwise</returns>
        /// <exception cref="NullReferenceException">Thrown if userId is not an identifier of an User entity </exception>
        /// <remarks>needs unit tests</remarks>
        public int GetUserId(int userInfoId)
        {
            ValidateParameter(userInfoId, "UserProfileManagementService.GetUserId", "userInfoId");

            return GetUserInfoById(userInfoId).UserID;
        }
        /// <summary>
        /// Retrieves the name for the user entity identifier
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>IUserNameResult containing user name</returns>
        public IUserNameResult GetuUserName(int userId)
        {
            IUserNameResult result = new UserNameResult();

            User userEntity = UnitOfWork.UserRepository.GetByID(userId);
            result.Populate(userEntity.FirstName(), userEntity.LastName(), userEntity.FullName());

            return result;
        }
        #endregion
        #region CV Services
        /// <summary>
        /// Retrieve a user's CV (resume)
        /// </summary>
        /// <param name="fileService">The FileService</param>
        /// <param name="resumeId">Resume entity identifier</param>
        /// <returns>Byte array containing resume</returns>
        /// <remarks>needs unit tests</remarks>
        public byte[] RetrieveCV(IFileService fileService, int resumeId)
        {
            ValidateParameter(resumeId, "UserProfileManagementService.RetrieveCV", "resumeId");

            UserResume userResumeEntity = UnitOfWork.UserResumeRepository.GetByID(resumeId);

            return userResumeEntity.ResumeFile;
        }
        #endregion
        #region User Client
        /// <summary>
        /// Get the clients assigned to this user
        /// </summary>
        /// <param name="userId">The user identifier of to search for client assignment to</param>
        /// <returns>Collection of UserProfileClientModel for those clients assigned to the supplied user</returns>
        public Container<UserProfileClientModel> GetAssignedUserProfileClient(int userId)
        {
            this.ValidateInteger(userId, "UserProfileManagementService.GetAssignedUserProfileClient", "userId");

            Container<UserProfileClientModel> container = new Container<UserProfileClientModel>();

            var results = UnitOfWork.UserRepository.GetByID(userId);
            var listOfObjects = results.UserClients.Where(x => x.UserID == userId).Select(x => new UserProfileClientModel(x.UserID, x.UserClientID, 
                                                                                x.ClientID, x.Client.ClientAbrv, x.Client.ClientDesc, true, x.Client.IsActive())
            );

            container.ModelList = listOfObjects;

            return container;

        }
        /// <summary>
        /// Retrieve the active clients that are assigned to the user.
        /// </summary>
        /// <param name="userId">The user identifier of to search for client assignment to</param>
        /// <returns>Collection of UserProfileClientModels for those active clients assigned to the supplied user</returns>
        public Container<UserProfileClientModel> GetAssignedActiveClients(int userId)
        {
            //
            // Once we have the list of the user's clients, we just filter them
            // by their active attribute
            //
            Container<UserProfileClientModel> container = this.GetAssignedUserProfileClient(userId);
            container.ModelList = container.ModelList.Where(x => x.IsActive);
            return container;
        }

        /// <summary>
        /// Get the clients not assigned to this user
        /// </summary>
        /// <param name="userId">he user identifier of to search for not assigned to</param>
        /// <returns>Collection of UserProfileClientModel for those clients not assigned to the supplied user</returns>
        public Container<UserProfileClientModel> GetAvailableUserProfileClient()
        {
            Container<UserProfileClientModel> container = new Container<UserProfileClientModel>();

            var results = UnitOfWork.ClientRepository.GetAll();
            var listOfObjects = results.Select(x => new UserProfileClientModel(null, null, x.ClientID, x.ClientAbrv, x.ClientDesc, true, true));
            container.ModelList = listOfObjects;

            return container;
        }
        /// <summary>
        /// Retrieves the data displayed on the 'Manage Account' modal window
        /// </summary>
        /// <param name="theLookupService">LookupService</param>
        /// <param name="userInfoId">UserInfo entity identifier</param>
        /// <returns>UserManageAccountModel model populated with required information</returns>
        public UserManageAccountModel GetUserManageAccount(ILookupService theLookupService, int userInfoId)
        {
            this.ValidateParameter(userInfoId, "UserProfileManagementService.UserManageAccountModel", "userInfoId");

            UserManageAccountModel result = new UserManageAccountModel();

            UserInfo userInfoEntity = UnitOfWork.UserInfoRepository.GetByID(userInfoId);
            User userEntity = userInfoEntity.User;
            UserAccountStatusChangeLog userChangeLogEntityUnlock = UnitOfWork.UserAccountStatusChangeLogRepository.GetLastUnlock(userEntity.UserID);
            UserAccountStatusChangeLog userChangeLogEntityLock = UnitOfWork.UserAccountStatusChangeLogRepository.GetLastLock(userEntity.UserID);
            //
            // User who created the user we are talking about
            //
            User userInfoCreateEntity = UnitOfWork.UserRepository.GetByID(userInfoEntity.CreatedBy) ?? new User();
            //
            // User who last modified the user we are talking about
            //
            User userUserInfoModifiedEntity = UnitOfWork.UserRepository.GetByID(userInfoEntity.ModifiedBy) ?? new User();
            //
            // User who last sent credentials to the user we are talking about
            //
            User userCredentiialEntity = UnitOfWork.UserRepository.GetByID(userEntity.CredentialSentBy) ?? new User();
            //
            // User who last modified the user account we are talking about
            //
            User userUserModifiedEentity = UnitOfWork.UserRepository.GetByID(userEntity.ModifiedBy) ?? new User();

            result.PopulateNonFieldsetData(userEntity.UserLogin, userEntity.LastLoginDate);
            result.PopulateAccount(userEntity.UserID, userEntity.ReadableAccountStatusId(), userEntity.ReadableAccountStatus(), userEntity.ReadableAccountStatusReasonId(), userEntity.ReadableAccountStatusReason(), userEntity.AccountStatusDate(), userEntity.ModifiedDate, userUserModifiedEentity.FirstName(), userUserModifiedEentity.LastName(), userEntity.IsLocked(), AccountStatu.Indexes.Active, AccountStatu.Indexes.Inactive, AccountStatusReason.ActivateButtonReasonIndexes() );
            result.PopulateSendCredentials(userEntity.CredentialSentDate, userCredentiialEntity.FirstName(), userCredentiialEntity.LastName());
            result.PopulateProfile(userInfoEntity.CreatedDate, userInfoCreateEntity.FirstName(), userInfoCreateEntity.LastName(), userInfoEntity.ModifiedDate, userUserInfoModifiedEntity.FirstName(), userUserInfoModifiedEntity.LastName(), userEntity.PrimaryUserEmailAddress());
            //
            // Now populate the Lockout section.  This needs to handle the case where there may not be an entry for either lock or unlock, 
            // an entry for both or an entry for neither in the UserAccountStatusChangeLog.
            //
            if (userChangeLogEntityUnlock != null && !userEntity.IsLocked())
            {
                //
                // User who unlocked the user we are talking about
                //
                User userUserUnlockEntity = UnitOfWork.UserRepository.GetByID(userChangeLogEntityUnlock.CreatedBy);
                result.PopulateUnlock(userChangeLogEntityUnlock.CreatedDate, userUserUnlockEntity.FirstName(), userUserUnlockEntity.LastName());
            }
            if (userChangeLogEntityLock != null && userEntity.IsLocked())
            {
                // the user is locked
                result.PopulateLocked(userChangeLogEntityLock.CreatedDate);
            }
            //
            // Now lets do the drop downs
            //
            result.DeactivateAccountDropdown = theLookupService.ListDeActivateAccount().ModelList;

            result.ShowSendCredentialButton = IsSendCredentialsEnabled(userEntity.UserID);
            //
            // Get the name of the role for display
            //
            GetUsersRoleResult roleResult = GetUsersRole(userInfoEntity.UserID);
            result.RoleName = (userEntity.UserProfileTypeId() == ProfileType.Indexes.Misconduct)? SystemRole.NoRole : roleResult.RoleName;
            return result;
        }
        /// <summary>
        /// Get the current readable account status reason for this user
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <returns>The readable account status reason</returns>
        public string GetUserAccountStatusName(int userId)
        {
            this.ValidateParameter(userId, "UserProfileManagementService.GetUserAccountStatusName", "userId");

            User userEntity = UnitOfWork.UserRepository.GetByID(userId);

            return userEntity.ReadableAccountStatusReason();
        }
        /// <summary>
        /// Get the date of the last change to the user's account status
        /// </summary>
        /// <param name="userId">The identifier of the user</param>
        /// <returns>The date of the last change to the account status</returns>
        public DateTime? GetUserAccountStatusDate(int userId)
        {
            ValidateInt(userId, FullName(nameof(IUserProfileManagementService), nameof(GetUserAccountStatusDate)), nameof(userId));

            User userEntity = UnitOfWork.UserRepository.GetByID(userId);

            return userEntity.AccountStatusDate();
        }
        /// <summary>
        /// Does the user have permanent credentials
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>true if the user is active with permanent credentials, false otherwise</returns>
        public bool IsPermanentCredentials(int userId)
        {
            this.ValidateParameter(userId, "UserProfileManagementService.IsPermanentcredentials", "userId");

            User userEntity = UnitOfWork.UserRepository.GetByID(userId);

            return userEntity.IsPermanentCredentials();
        }
        #endregion
        #region Password/Security
        /// <summary>
        /// Retrieves user Password/Security information
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>user manage password/security information</returns>
        public IUserManagePasswordModel GetUserPasswordAndSecurityQuestionInfo(int userId)
        {
            this.ValidateInteger(userId, "UserProfileManagementService.GetUserPasswordAndSecurityQuestionInfo", "userId");

            var results = UnitOfWork.UserRepository.GetByID(userId);

            IUserManagePasswordModel passwordSecurityInfo = new UserManagePasswordModel();

            if (results != null && results.UserAccountRecoveries != null)
            {

                passwordSecurityInfo = results.UserAccountRecoveries.Select(x => new UserManagePasswordModel
                {
                    UserId = x.UserId,
                    Username = x.User.UserLogin,
                    FirstName = x.User.FirstName(),
                    LastName = x.User.LastName(),
                    PrimaryEmail = x.User.UserInfoes.FirstOrDefault().UserEmails.FirstOrDefault().Email,
                    PasswordUpdateDate = x.User.PasswordDate,
                    EffectiveDaysUntilPasswordExpiration = EffectiveDaysUntilPasswordExpiration(x.User.PasswordAge()),
                    SecurityQuestionUpdateDate = x.User.UserAccountRecoveries.Max(m => m.ModifiedDate),
                    TemporaryPassword = x.User.UsersCurrentAccountStatus().AccountStatusReasonId == AccountStatusReason.Indexes.TmpPwd,
                    SecurityQuestionsAndAnswers = x.User.UserAccountRecoveries.Select(z => new UserSecurityQuestionAnswerModel
                    {
                        UserAccountRecoveryId = z.UserAccountRecoveryId,
                        RecoveryQuestionId = z.RecoveryQuestionId,
                        AnswerText = z.UserAccountRecoveryId > 0 ? "********" : string.Empty
                    }).ToList()
                }).FirstOrDefault();
            }

            if (passwordSecurityInfo == null)
            {
                passwordSecurityInfo = new UserManagePasswordModel();
                passwordSecurityInfo.UserId = results.UserID;
                passwordSecurityInfo.Username = results.UserLogin;
                passwordSecurityInfo.FirstName = results.FirstName();
                passwordSecurityInfo.LastName = results.LastName();
                passwordSecurityInfo.PrimaryEmail = results.UserInfoes.FirstOrDefault().UserEmails.FirstOrDefault()?.Email;
                passwordSecurityInfo.PasswordUpdateDate = results.PasswordDate;
                passwordSecurityInfo.EffectiveDaysUntilPasswordExpiration = EffectiveDaysUntilPasswordExpiration(results.PasswordAge());
                passwordSecurityInfo.SecurityQuestionsAndAnswers = new List<UserSecurityQuestionAnswerModel>();
            }

            return passwordSecurityInfo;
        }
        /// <summary>
        /// Determines if the users password expired and updates user account status accordingly 
        /// </summary>
        /// <param name="userId">The user identifier of the user making the change</param>
        /// <returns>Returns true if password is expired, false otherwise</returns>
        public bool IsPasswordExpired(int userId)
        {
            this.ValidateInteger(userId, "AccessManagementService.GetUserLoginCapability", "userId");

            var userEntity = UnitOfWork.UserRepository.GetByID(userId);
            var passwordAge = userEntity.PasswordAge();
            var effectiveDaysLeft = EffectiveDaysUntilPasswordExpiration(passwordAge);
            var expired = effectiveDaysLeft < 0;
            if (expired)
            {
                UserAccountStatu status = userEntity.UsersCurrentAccountStatus();
                status.PasswordExpired(userId);
                UnitOfWork.Save();
            }

            return expired;
        }
        /// <summary>
        /// Gets the days until password expiration, taking into account the most recent password policy release date and subsequent grace period
        /// </summary>
        /// <param name="passwordAge"></param>
        /// <returns></returns>
        public int EffectiveDaysUntilPasswordExpiration(int passwordAge)
        {
            var effectiveDaysLeft = ConfigManager.PwdNumberDaysBeforeExpire - passwordAge;
            var today = GlobalProperties.P2rmisDateTimeNow.Date;
            var withinGracePeriod = (today >= ConfigManager.PwdPolicyReleaseDate.Date) && (today <= ConfigManager.PwdInitialExpirationDate.Date);
            if (withinGracePeriod)
            {
                var initialPwdExpDate = ConfigManager.PwdInitialExpirationDate.Date;
                var daysLeftInGracePeriod = Math.Abs(today.Subtract(initialPwdExpDate).Days);
                if (effectiveDaysLeft <= daysLeftInGracePeriod)
                {
                    effectiveDaysLeft = daysLeftInGracePeriod;
                }
            }
            return effectiveDaysLeft;
        }
        /// <summary>
        /// Get Random Security Question
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>object containing user question text and user recovery question Id</returns>
        public IUserRecoveryQuestionModel GetRandomSecurityQuestion(int userId)
        {
            this.ValidateInteger(userId, "UserProfileManagementService.GetRandomSecurityQuestion", "userId");

            var securityQuestions = UnitOfWork.UserAccountRecoveryRepository.Get(x => x.UserId == userId).Select(x =>

               new UserRecoveryQuestionModel
               {
                   QuestionText = x.RecoveryQuestion.QuestionText,
                   QuestionOrder = x.QuestionOrder,
                   UserAccountRecoveryId = x.UserAccountRecoveryId,
                   RecoveryQuestionId = x.RecoveryQuestionId
               });
            //get randow number for security question
            Random random = new Random();
            var userRecoveryQuestionModels = securityQuestions as IList<UserRecoveryQuestionModel> ?? securityQuestions.ToList();
            int num = random.Next(1, userRecoveryQuestionModels.Count() + 1);  // will choose a number based on the number of security questions provided

            var result = userRecoveryQuestionModels.Where(x => x.QuestionOrder == num);

            return result.FirstOrDefault();
        }
        /// <summary>
        /// Does the user have security questions
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>true if the user has entered security questions, false otherwise</returns>
        public bool HasSecurityQuestions(int userId)
        {
            this.ValidateInteger(userId, "UserProfileManagementService.HasSecurityQuestion", "userId");
            // At least one security question should exist
            return UnitOfWork.UserAccountRecoveryRepository.Get(x => x.UserId == userId).ToList().Count > 0;
        }

        #endregion
        #region User Status Updates
        /// <summary>
        /// Sets the account status to locked for the indicated user
        /// </summary>
        /// <param name="targetUserId">The user identifier of the user to be locked</param>
        /// <param name="userId">The user identifier of the user initiating the locked action</param>
        public void Lock(int targetUserId, int userId)
        {
            this.ValidateInteger(targetUserId, "UserProfileManagementService.Lock", "targetUserId");
            this.ValidateInteger(userId, "UserProfileManagementService.Lock", "userId");

            User userEntity = UnitOfWork.UserRepository.GetByID(targetUserId);

            UserAccountStatu userAccountStatus = userEntity.UsersCurrentAccountStatus();

            // note: UserAccountStatusChangeLog is updated by the database triggers

            userAccountStatus.Lock(targetUserId, userId);
            UnitOfWork.UserAccountStatusRepository.Update(userAccountStatus);

            UnitOfWork.Save();
        }
        /// <summary>
        /// Set the user account status to expired
        /// </summary>
        /// <param name="targetUserId">The user whose status is being set to expired</param>
        /// <param name="userId">The identifier of the user making the change</param>
        public void SetInvitationExpired(int targetUserId, int userId)
        {
            this.ValidateInteger(targetUserId, FullName(nameof(UserProfileManagementService), nameof(SetInvitationExpired)), nameof(targetUserId));
            this.ValidateInteger(userId, FullName(nameof(UserProfileManagementService), nameof(SetInvitationExpired)), nameof(userId));

            User userEntity = UnitOfWork.UserRepository.GetByID(targetUserId);

            UserAccountStatu userAccountStatus = userEntity.UsersCurrentAccountStatus();

            // note: UserAccountStatusChangeLog is updated by the database triggers

            userAccountStatus.InvitationExpired(userId);
            UnitOfWork.UserAccountStatusRepository.Update(userAccountStatus);

            UnitOfWork.Save();
        }
        /// <summary>
        /// Set the user account status to awaiting credentials
        /// </summary>
        /// <param name="targetUserId">The user identifier whose status is being set to awaiting credentials</param>
        /// <param name="userId">The identifier of the user making the change</param>
        public void SetUserAccountStatusAwaitingCredentials(int targetUserId, int userId)
        {
            ValidateInt(targetUserId, FullName(nameof(UserProfileManagementService), nameof(SetUserAccountStatusAwaitingCredentials)), nameof(targetUserId));
            ValidateInt(userId, FullName(nameof(UserProfileManagementService), nameof(SetUserAccountStatusAwaitingCredentials)), nameof(userId));

            User targetUser = UnitOfWork.UserRepository.GetByID(targetUserId);

            SetUserAccountStatusAwaitingCredentials(targetUser, userId);

            UnitOfWork.Save();
        }

        /// <summary>
        /// Set the user account status to awaiting credentials
        /// </summary>
        /// <param name="targetUserId">The user whose status is being set to awaiting credentials</param>
        /// <param name="userId">The identifier of the user making the change</param>
        internal void SetUserAccountStatusAwaitingCredentials(User targetUser, int userId)
        {
            UserAccountStatu userAccountStatus = targetUser.UsersCurrentAccountStatus();

            CreateOrUpdateUserAccountStatus(AccountStatusReason.Indexes.AwaitingCredentials, targetUser, userId);
        }

        /// <summary>
        /// Set the user account status to misconduct
        /// </summary>
        /// <param name="targetUserId">The user whose status is being set to misconduct</param>
        /// <param name="userId">The identifier of the user making the change</param>
        internal void SetUserAccountStatusMisconduct(User targetUser, int userId)
        {
            UserAccountStatu userAccountStatus = targetUser.UsersCurrentAccountStatus();

            CreateOrUpdateUserAccountStatus(AccountStatusReason.Indexes.Ineligible, targetUser, userId);
        }
        /// <summary>
        /// Sets the account status to unlocked for the indicated user
        /// </summary>
        /// <param name="userToLockoutId">The user identifier of the user to be locked</param>
        /// <param name="userId">The user identifier of the user initiating the locked action</param>
        public IReactivateDeactivateResult Unlock(int targetUserId, int userId)
        {
            this.ValidateInteger(targetUserId, "UserProfileManagementService.Lock", "targetUserId");
            this.ValidateInteger(userId, "UserProfileManagementService.Lock", "userId");

            User userEntity = UnitOfWork.UserRepository.GetByID(targetUserId);
            UserAccountStatusChangeLog userAccountStatusChangeLogEntity = UnitOfWork.UserAccountStatusChangeLogRepository.GetLastLock(targetUserId);

            UserAccountStatu userAccountStatus = userEntity.UsersCurrentAccountStatus();

            // note: UserAccountStatusChangeLog is updated by the database triggers

            userAccountStatus.Unlock(userAccountStatusChangeLogEntity.OldAccountStatusReasonId.Value, userAccountStatusChangeLogEntity.OldAccountStatusId.Value, userId);
            UnitOfWork.UserAccountStatusRepository.Update(userAccountStatus);

            UnitOfWork.Save();


            IReactivateDeactivateResult result = new ReactivateDeactivateResult();
            result.Populate(GetuUserName(userId), userEntity.ReadableAccountStatus(), userEntity.ReadableAccountStatusReason(), userEntity.AccountStatus().AccountStatusId, userEntity.AccountStatus().AccountStatusReasonId, userEntity.UserProfileTypeId(), userEntity.AccountStatusDate());

            return result;
        }

        #endregion
        #region Participation history

        /// <summary>
        /// Retrieves a list of participation assignments associated with a given user
        /// </summary>
        /// <param name="userInfoId">UserInfo entity identifier</param>
        /// <returns>List of assignment information</returns>
        public Container<IUserParticipationHistoryModel> GetParticipationHistory(int userInfoId)
        {
            this.ValidateInteger(userInfoId, "UserProfileManagementService.GetParticipationHistory", "userInfoId");
            Container<IUserParticipationHistoryModel> container = new Container<IUserParticipationHistoryModel>();
            var results = UnitOfWork.UserRepository.GetParticipationHistory(userInfoId);
            container.SetModelList(results);
            return container;
        }
        #endregion
        #region Validation Services
        /// <summary>
        /// Checks for a duplicate email (institutional or personal) is provided.
        /// </summary>
        /// <param name="target">Email address to check</param>
        /// <param name="userInfoId">UserInfo entity identifier</param>
        /// <returns>True if the email address exists; false otherwise</returns>
        public bool IsDuplicateEmailAddress(string target, int userInfoId)
        {
            var result = this.UnitOfWork.UserEmailRepository.Get(x => (x.Email == target) & (x.UserInfoID != userInfoId));
            return (result.Count() != 0);
        }
        /// <summary>
        /// Checks for duplicates
        /// </summary>
        /// <param name="target"></param>
        /// <param name="userInfoId"></param>
        /// <returns></returns>
        public bool IsDuplicateVendorId(string target, int userInfoId, bool isIndividual)
        {
            var isDuplicate = false;
            // Check if the target value is in VendorIdAssigned table with "assigned" flag but assigned to another user
            if (!String.IsNullOrEmpty(target))
            {
                target = target ?? string.Empty;
                bool isAssigned = UnitOfWork.UserRepository.IsVendorIdAssigned(target);
                if (isAssigned)
                {
                    bool isUsedByCurrentUser = UnitOfWork.UserRepository.IsVendorIdUsed(target, isIndividual, userInfoId);
                    if (!isUsedByCurrentUser)
                        isDuplicate = true;
                }
            }
            return isDuplicate;
        }
        /// <summary>
        /// check if the vendor id is for this user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        public bool UserIdNotVendorId(int? userId, string vendorId)
        {
            var isUserIdNotVendorId = false;
            var userVendorId = UnitOfWork.UserRepository.GetVendorId(userId);
            if (userVendorId != vendorId)
            {
                isUserIdNotVendorId = true;
            }

            return isUserIdNotVendorId;
        }
        /// <summary>
        /// Checks if a provided password matches the user's current password in the db
        /// </summary>
        /// <param name="passwordToCheck">New password that is being compared</param>
        /// <param name="userId">Identifier of the user account</param>
        /// <returns>True is password matches current; false otherwise</returns>
        public bool DoesMatchCurrentPassword(string passwordToCheck, int userId)
        {
            var currentUser = this.UnitOfWork.UserRepository.GetByID(userId);
            return currentUser.CheckPasswordMatchesCurrent(passwordToCheck);
        }

        /// <summary>
        /// Checks if a provided password matches the user's previous passwords in the db
        /// </summary>
        /// <param name="passwordToCheck"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DoesMatchPreviousPasswords(string passwordToCheck, int userId)
        {
            var currentUser = this.UnitOfWork.UserRepository.GetByID(userId);
            return currentUser.CheckPasswordMatchesPrevious(passwordToCheck);
        }

        #endregion
        #region Role Related Services
        /// <summary>
        /// Retrieve the user's role order within the user's profile.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Users role priority order; null if no role priority order</returns>
        public int? GetUsersSystemPriorityOrder(int userId)
        {
            ValidateInt(userId, "UserProfileManagementService.GetUsersSystemPriorityOrder", "userId");

            User userEntiy = UnitOfWork.UserRepository.GetByID(userId);
            return userEntiy.RolePriorityOrder();
        }
        /// <summary>
        /// Retrieve the user's ProfileType entity identifier.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Users ProfileType entity identifier</returns>
        public int GetUsersProfileType(int userId)
        {
            ValidateInt(userId, "UserProfileManagementService.GetUsersProfileType", "userId");

            User userEntiy = UnitOfWork.UserRepository.GetByID(userId);
            return userEntiy.UserProfileTypeId();
        }
        /// <summary>
        /// Is the user's ProfileType Reviewer.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>True if user is a reviewer, false otherwise</returns>
        public bool IsReviewer(int userId)
        {
            ValidateInt(userId, "UserProfileManagementService.IsReviewer", "userId");

            User userEntiy = UnitOfWork.UserRepository.GetByID(userId);
            return userEntiy.UserProfileTypeId() == ProfileType.Indexes.Reviewer;
        }
        /// <summary>
        /// Determines if the user's role is as an SRO or RTA.
        /// </summary>
        /// <param name="userId">User Entity identifier</param>
        /// <returns>True if the user is an SRO or RTA; false otherwise</returns>
        public bool IsSroRta(int userId)
        {
            ValidateInt(userId, "UserProfileManagementService.IsSroRta", "userId");
            //
            // Find the user's role
            //
            var roleResult = GetUsersRole(userId);
            //
            // Now just check if it an SRO or RTA
            //
            return (roleResult.Role.IsSro() || roleResult.Role.IsRta());
        }
        /// <summary>
        /// Determines whether the specified user identifier is sro.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified user identifier is sro; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSro(int userId)
        {
            ValidateInt(userId, "UserProfileManagementService.IsSroRta", "userId");
            //
            // Find the user's role
            //
            var roleResult = GetUsersRole(userId);
            //
            // Now just check if it an SRO or RTA
            //
            return roleResult.Role.IsSro();
        }
        #endregion
        #region W9
        /// <summary>
        /// Is the user's w9 verified by the user
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>true if verified, false otherwise</returns>
        public bool? IsW9Verified(int userId)
        {
            bool? result;
            ValidateParameter(userId, "UserProfileManagementService.IsW9Verified", "userId");

            User thisUser = this.UnitOfWork.UserRepository.GetByID(userId);

            result = thisUser.W9Verified;

            return result;
        }
        /// <summary>
        /// Is W9 verification required for this user
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>True if the user is a reviewer and the W9 has not been verified</returns>
        public bool IsW9VerificationRequired(int userId)
        {
            ValidateParameter(userId, "UserProfileManagementService.IsW9VerificationRequired", "userId");

            User thisUser = this.UnitOfWork.UserRepository.GetByID(userId);

            // handle null modified date
            PanelUserAssignment pua = thisUser.PanelUserAssignments.OrderByDescending(x => x.ModifiedDate).FirstOrDefault();
            DateTime? assignDate = pua == null ? null : pua.ModifiedDate;

            return (thisUser.UserProfileTypeId() == ProfileType.Indexes.Reviewer && (!thisUser.W9Verified.HasValue || assignDate > thisUser.W9VerifiedDate));
        }
        /// <summary>
        /// Checks if the user's W-9 has been updated.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>True if the user's W-9 has been updated; false otherwise</returns>
        public bool IsW9Updated(int userId)
        {
            ValidateParameter(userId, "UserProfileManagementService.IsW9Updated", "userId");

            User userEntity = UnitOfWork.UserRepository.GetByID(userId);
            return userEntity.IsW9Updated();
        }
        /// <summary>
        /// Is the user's W9 missing
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>true if it is missing, false otherwise</returns>
        public bool IsW9Missing(int userId)
        {
            ValidateParameter(userId, "UserProfileManagementService.IsW9Missing", "userId");

            User userEntity = UnitOfWork.UserRepository.GetByID(userId);
            UserAddress userW9Address = userEntity.W9Address();

            return (userW9Address == null) ? true : userW9Address.IsInvalidW9();
        }
        /// <summary>
        /// Is the user profile verified
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>true if profile is verified, false otherwise</returns>
        public bool IsUserProfileVerified(int userId)
        {
            ValidateParameter(userId, "UserProfileManagementService.IsUserProfileVerified", "userId");
            User userEntity = this.UnitOfWork.UserRepository.GetByID(userId);

            return (userEntity.Verified.HasValue && userEntity.Verified.Value && userEntity.VerifiedDate >= ProfileVerificationRequiredDate());
        }
        /// <summary>
        /// Is user profile verifcation required
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns></returns>
        public bool IsProfileVerificationRequired(int userId)
        {
            return IsW9VerificationRequired(userId) || !IsUserProfileVerified(userId);
        }

        /// <summary>
        /// Date by which profile verification must have occured after to be considered current 
        /// </summary>
        /// <returns>DateTime in which anything prior is considered to require a mandatory update</returns>
        public static DateTime ProfileVerificationRequiredDate()
        {
            var timeNow = GlobalProperties.P2rmisDateToday;
            return timeNow.AddYears(-1);
        }
        #endregion
        #region General User Information

        /// <summary>
        /// Gets the users name and primary email.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Model containing users name and primary email.</returns>
        public IUserInfoSmallModel GetUsersNameAndPrimaryEmail(int userId)
        {
            IUserInfoSmallModel result = new UserInfoSmallModel();
            var user = UnitOfWork.UserRepository.GetByID(userId);
            result.Populate(user.FirstName(), user.LastName(), user.PrimaryUserEmailAddress());
            return result;
        }
        #endregion
        #region Recruitment Blocking                  
        /// <summary>
        /// Updates the user client block.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="blockedClientIds">The blocked client ids.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public bool UpdateUserClientBlock(int userInfoId, List<int> blockedClientIds, string comment, int userId)
        {
            Validate("UserProfileManagementService.UpdateUserClientBlock", userInfoId, nameof(userInfoId), userId, nameof(userId));

            bool flag = false;
            var availableBlockedClientIds = UnitOfWork.UserClientBlockRepository.GetList(userInfoId).ToList().ConvertAll(x => x.ClientId).ToList();
            var assignedClientIds = GetAssignedUserProfileClient(userId).ModelList.ToList().ConvertAll(x => x.ClientId).ToList();
            var currentUserBlockedClientIds = new List<int>();
            for (var i = 0; i < assignedClientIds.Count; i++)
            {
                if (availableBlockedClientIds.Contains(assignedClientIds[i]))
                {
                    currentUserBlockedClientIds.Add(assignedClientIds[i]);
                }
            }
            var toAddBlockedClientIds = blockedClientIds != null ? blockedClientIds.Except(currentUserBlockedClientIds).ToList() : new List<int>();
            var toDeletedBlockedClientIds = blockedClientIds != null ? currentUserBlockedClientIds.Except(blockedClientIds).ToList() : currentUserBlockedClientIds;
            for (var i = 0; i < toAddBlockedClientIds.Count; i++)
            {
                UnitOfWork.UserClientBlockRepository.Add(userInfoId, toAddBlockedClientIds[i], userId);
                flag = true;
            }
            for (var i = 0; i < toDeletedBlockedClientIds.Count; i++)
            {
                UnitOfWork.UserClientBlockRepository.Delete(userInfoId, toDeletedBlockedClientIds[i], userId);
                flag = true;
            }
            UnitOfWork.UserClientBlockRepository.AddLog(userInfoId, toAddBlockedClientIds, toDeletedBlockedClientIds, comment, userId);
            if (flag) 
                UnitOfWork.Save();
            return flag;
        }
        /// <summary>
        /// Gets the user client blocks.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="availableClientIds">The available client ids.</param>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetUserClientBlocks(int userInfoId, List<int> availableClientIds)
        {
            Validate("UserProfileManagementService.GetUserClientBlocks", userInfoId, nameof(userInfoId));

            var userClientBlocks = UnitOfWork.UserClientBlockRepository.GetList(userInfoId);
            var list = userClientBlocks.AsEnumerable().Select(x => new KeyValuePair<int, string>(x.ClientId, x.Client.ClientAbrv))
                .Where(y => availableClientIds.Contains(y.Key)).ToList();
            return list;
        }
        /// <summary>
        /// Gets the user client block logs.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        public List<IUserClientBlockLog> GetUserClientBlockLogs(int userInfoId)
        {
            Validate("UserProfileManagementService.GetUserClientBlockLogs", userInfoId, nameof(userInfoId));

            var logList = new List<IUserClientBlockLog>();
            var userClientBlockLogs = UnitOfWork.UserClientBlockRepository.GetLogs(userInfoId).ToList();

            for (var iLog = 0; iLog < userClientBlockLogs.Count; iLog++)
            {
                IUserClientBlockLog log = new UserClientBlockLog();
                log.Comments = userClientBlockLogs[iLog].BlockComment;
                log.EnteredBy = UnitOfWork.UserRepository.GetByID(userClientBlockLogs[iLog].EnteredByUserId).FullName();
                log.CreatedDate = userClientBlockLogs[iLog].CreatedDate;
                for (var iClient = 0; iClient < userClientBlockLogs[iLog].UserBlockLogClients.ToList().Count; iClient++)
                {
                    var logClient = userClientBlockLogs[iLog].UserBlockLogClients.ToList()[iClient];
                    var clientAbrv = UnitOfWork.ClientRepository.GetByID(logClient.ClientId).ClientAbrv;
                    var clientFlag = new KeyValuePair<string, bool>(clientAbrv, logClient.BlockFlag);
                    log.ClientBlockFlags.Add(clientFlag);
                }
                logList.Add(log);
            }
            return logList;
        }
        #endregion
        #endregion
        #region Helpers
        /// <summary>
        /// Validates the parameters for SearchUser - Create User version.  Each parameter
        /// can be null by itself, it is just that all parameters can be supplied.
        /// </summary>
        /// <param name="firstName">User's first name</param>
        /// <param name="lastName">User's last name</param>
        /// <param name="email">User's email</param>
        /// <exception cref="ArgumentException">Thrown if all parameters are null or empty string</exception>
        private void ValidateSearchUserParameters(string firstName, string lastName, string email)
        {
            if (string.IsNullOrEmpty(firstName) &&
                string.IsNullOrEmpty(lastName) &&
                string.IsNullOrEmpty(email))
            {
                string message = MessageService.GetInvalidServiceParameterMessage("UserProfileManagementService.SearchUser", "firstName, lastName & email", "null");
                throw new ArgumentException(message);
            }
        }
        /// <summary>
        /// Validates the parameters for SearchUser - Create User version.  Each parameter
        /// can be null by itself, it is just that all parameters can be supplied.
        /// </summary>
        /// <param name="firstName">User's first name</param>
        /// <param name="lastName">User's last name</param>
        /// <param name="email">User's email</param>
        /// <param name="userName">User's name</param>
        /// <param name="userId">User's identifier</param>
        /// <param name="vendorId">User's vendor identifier</param>
        /// <exception cref="ArgumentException">Thrown if all parameters are null, empty string and <= 0</exception>
        private void ValidateSearchUserParameters(string firstName, string lastName, string email, string userName, int? userId, string vendorId)
        {
            if (string.IsNullOrEmpty(firstName) &&
                string.IsNullOrEmpty(lastName)  &&
                string.IsNullOrEmpty(email)     &&
                string.IsNullOrEmpty(userName)  &&
                (!userId.HasValue || userId.Value <= 0) &&
                string.IsNullOrEmpty(vendorId)
               )
            {
                string message = MessageService.GetInvalidServiceParameterMessage("UserProfileManagementService.SearchUser", "firstName, lastName, email, userName, userId and vendorId", "null or <=0");
                throw new ArgumentException(message);
            }
        }
        /// <summary>
        /// Gets the user email of the indicated type
        /// </summary>
        /// <param name="userInfoId">UserInfo identifier</param>
        /// <param name="emailType">The email type</param>
        /// <returns>EmailAddressModel</returns>
        private IEmailAddressModel GetUserEmailAddress(int userInfoId, int emailType)
        {
            var results = UnitOfWork.UserInfoRepository.GetByID(userInfoId);
            var emailAddress = results.UserEmails.Where(x => x.UserInfoID == userInfoId && x.EmailAddressTypeId == emailType).Select(x => new EmailAddressModel(x.EmailID, x.EmailAddressTypeId, x.Email, x.PrimaryFlag)).FirstOrDefault();

            return emailAddress;
        }
        /// <summary>
        /// Gets the user email of the indicated type
        /// </summary>
        /// <param name="userInfoId">UserInfo identifier</param>
        /// <param name="emailType">The email type</param>
        /// <param name="primary">Primary email</param>
        /// <returns>EmailAddressModel</returns>
        /// <param name="primary"></param>
        private IEmailAddressModel GetUserEmailAddress(int userInfoId, int emailType, bool primary)
        {
            var results = UnitOfWork.UserInfoRepository.GetByID(userInfoId);
            var emailAddress = results.UserEmails
                .Where(x => x.UserInfoID == userInfoId && x.EmailAddressTypeId == emailType && x.PrimaryFlag == primary).
                Select(x => new EmailAddressModel(x.EmailID, x.EmailAddressTypeId, x.Email, x.PrimaryFlag)).FirstOrDefault();

            return (emailAddress == null)? new EmailAddressModel () : emailAddress;
        }
        #endregion		
    }
}
