using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Security;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Files = Sra.P2rmis.Bll.FileService;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.Dal.EntityChanges;
using Sra.P2rmis.Bll.ReviewerRecruitment;
using System.Text.RegularExpressions;
using Sra.P2rmis.Bll.Mail;
using Entity = Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.UserProfileManagement
{
    /// <summary>
    /// UserProfileManagementService provides services to perform business related functions for
    /// the User Profile Management Application. This file contains services related to saving the
    /// user profile.
    /// </summary>
    public partial class UserProfileManagementService
    {
        public const string YES = "YES";

        /// <summary>
        /// Master method to save the user profile.  The method verifies the source data was not "stale" (i.e. another
        /// user changed the profile while it was on this users screen) and calls individual methods to save each 
        /// table related to the User object.
        /// 
        /// After all is said and done the modifications are saved.
        /// </summary>
        /// <param name="profileId">User identifier of the profile being updated</param>
        /// <param name="dateTimeStamps">Dictionary of datetime stamps indexed by type to determine if data is stale</param>
        /// <param name="W9Verify">Indicates if verificatin of the W9 address is to be performed</param>
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
        /// <param name="isMyProfile">Whether the user is submitting from the isMyProfile section</param>
        /// <param name="isUpdateUser">Whether the user is an update (true) or a create (false)</param>
        public ICollection<SaveProfileStatus> SaveProfile(int profileId, Dictionary<Type, DateTime> dateTimeStamps, bool W9Verify, IGeneralInfoModel theGeneralInfoModel, IList<WebsiteModel> theWebsiteModels, IEmailAddressModel theInstitutionEmailAddress, IEmailAddressModel thePersonalEmailAddress, IList<AddressInfoModel> theAddress, IProfessionalAffiliationModel theProfessionalAffiliation, IW9AddressModel w9Addresses, ICollection<IListEntry> thePhoneTypeDropdowns, ICollection<IListEntry> theAlternateContactTypeDropdowns, IList<PhoneNumberModel> thePhoneNumberModels, IList<UserAlternateContactPersonModel> theAlternateContactPhoneModels, IList<UserDegreeModel> theUserDegreeModels, IUserMilitaryRankModel theMilitaryRankModel, IUserMilitaryStatusModel theMilitaryStatusModel, int? theMilitaryServiceId, IUserVendorModel theVendorInfoIndividual, IUserVendorModel theVendorInfoInstitutional, IList<UserProfileClientModel> theUserProfileClientModels, int userId, bool isMyProfile, bool isUpdateUser)
        {
            VerifySaveProfileParameters(profileId, dateTimeStamps, W9Verify, theGeneralInfoModel, theWebsiteModels, theAddress, theProfessionalAffiliation, w9Addresses, thePhoneTypeDropdowns, theAlternateContactTypeDropdowns, thePhoneNumberModels, theUserDegreeModels, theMilitaryRankModel, theMilitaryStatusModel, theUserProfileClientModels, userId);

            ICollection<SaveProfileStatus> results = IsDataCurrent(dateTimeStamps);

            if (results.Count() == 0)
            {
                User userToUpdate = GetUserById(profileId);
                UserInfo userInfo = userToUpdate.UserInfoes.FirstOrDefault();

                int? currentRoleId = userToUpdate.GetUserSystemRole();
                int? profileUserId = null;
                //List<AddressInfoModel> thePersonalAddresses = new List<AddressInfoModel>();
                results = UpdateProfile(userToUpdate, userInfo, W9Verify, theGeneralInfoModel, theWebsiteModels, theInstitutionEmailAddress, thePersonalEmailAddress, theAddress, theProfessionalAffiliation, w9Addresses, thePhoneTypeDropdowns, theAlternateContactTypeDropdowns, thePhoneNumberModels, theAlternateContactPhoneModels, theUserDegreeModels, theMilitaryRankModel, theMilitaryStatusModel, theMilitaryServiceId, theVendorInfoIndividual, theVendorInfoInstitutional, theUserProfileClientModels, isMyProfile, userId, ref profileUserId);

                // if profile was saved and the role changed, update the permissions
                if ((results.Count == 1) && (results.First() == SaveProfileStatus.Success) && theGeneralInfoModel.ProfileTypeId.HasValue)
                {
                    UpdateRoles(userToUpdate, theGeneralInfoModel.ProfileTypeId.Value, theGeneralInfoModel.SystemRoleId);
                }
            }

            return results;
        }
        /// <summary>
        /// Saves the alternate contact.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="userContact">The user contact.</param>
        /// <param name="userId">The user identifier.</param>
        public void SaveAlternateContact(int userInfoId, IUserAlternateContactPersonModel userContact, int userId)
        {
            Update(userInfoId, userContact, userId);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Uploads the w9 addresses.
        /// </summary>
        /// <param name="addresses">The addresses.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ICollection<KeyValuePair<int, SaveAddressStatus>> UploadW9Addresses(IList<UserAddressUploadModel> addresses, int userId)
        {
            var statusList = new List<KeyValuePair<int, SaveAddressStatus>>();
            var states = UnitOfWork.StateRepository.GetAll();
            var countries = UnitOfWork.CountryRepository.GetAll();
            for (var i = 0; i < addresses.Count; i++)
            {
                var address = addresses[i];
                if (String.IsNullOrEmpty(address.ReviewerName))
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.ReviewerNameNotSupplied));
                if (String.IsNullOrEmpty(address.VendorName))
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.VendorNameNotSupplied));
                else if (address.VendorName.Length > 200)
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.VendorNameTooLong));
                if (String.IsNullOrEmpty(address.InstVendorId))
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.InstVendorIdNotSupplied));
                if (String.IsNullOrEmpty(address.Address1))
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.Address1NotSupplied));
                else if (address.Address1.Length > 100)
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.Address1TooLong));
                if (address.Address2 != null && address.Address2.Length > 100)
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.Address2TooLong));
                if (address.Address3 != null && address.Address3.Length > 100)
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.Address3TooLong));
                if (address.Address4 != null && address.Address4.Length > 100)
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.Address4TooLong));
                if (String.IsNullOrEmpty(address.City))
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.CityNotSupplied));
                else if (address.City.Length > 100)
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.CityTooLong));
                if (String.IsNullOrEmpty(address.Zip))
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.ZipNotSupplied));
                else if (address.Zip.Length > 20)
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.ZipTooLong));
                if (String.IsNullOrEmpty(address.State))
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.StateNull));
                else
                {
                    var state = states.FirstOrDefault(x => x.StateAbbreviation == address.State);
                    if (state == null)
                        statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.StateInvalid));
                    else
                        address.StateId = state.StateId;
                }

                if (String.IsNullOrEmpty(address.Country))
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.CountryCodeNull));
                else
                {
                    var country = countries.FirstOrDefault(x => x.CountryAbbreviation == address.Country);
                    if (country == null)
                        statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.CountryCodeInvalid));
                    else
                        address.CountryId = country.CountryId;
                }
                Regex regex = new Regex(@"^[a-zA-Z0-9]+$");
                if (address.VendorId == null)
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.VendorIdNotSupplied));

                if (address.UserId == null)
                    statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.UserIdNotSupplied));
                else
                {
                    var user = UnitOfWork.UserRepository.GetByID((int)address.UserId);
                    if (user == null)
                    {
                        statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.UserIdInvalid));
                    }
                    else
                    {
                        var userInfo = user.UserInfoEntity();
                        if (userInfo == null)
                        {
                            if (address.VendorId != null && address.VendorId.Length > 10)
                            {
                                statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.VendorIdTooLong));
                            }
                            else if (address.VendorId != null && !regex.IsMatch(address.VendorId))
                            {
                                statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.VendorIdCharacterInvalid));
                            }
                        }
                        else
                        {
                            var vendorTypeId = address.InstVendorId == "YES" ? VendorType.Indexes.Institutional : VendorType.Indexes.Individual;
                            if (!userInfo.HasVendorId(address.VendorId, vendorTypeId))
                                statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.UserIdNotVendorId));

                            if (address.VendorId != null && address.VendorId.Length > 10)
                            {
                                statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.VendorIdTooLong));
                            }
                            else if (address.VendorId != null && !regex.IsMatch(address.VendorId))
                            {
                                statusList.Add(new KeyValuePair<int, SaveAddressStatus>(i + 1, SaveAddressStatus.VendorIdCharacterInvalid));
                            }
                            var userAddress =
                                UnitOfWork.UserRepository.GetUserAddress(userInfo.UserInfoID,
                                    AddressType.Indexes.W9);
                            var addressInfo = new AddressInfoModel()
                            {
                                AddressTypeId = AddressType.Indexes.W9,
                                UserAddressId = userAddress?.AddressID,
                                Address1 = address.Address1,
                                Address2 = address.Address2,
                                Address3 = address.Address3,
                                Address4 = address.Address4,
                                City = address.City,
                                State = address.State,
                                StateId = address.StateId,
                                Country = address.Country,
                                CountryId = address.CountryId,
                                Zip = address.Zip,
                            };
                            Update(user.UserInfoEntity(), addressInfo, userId);

                            // Reset the w9 status
                            ResetW9VerificationStatus(user, userId);

                            if (statusList.Count == 0)
                            {
                                var isIndividual = address.InstVendorId == "YES" ? false : true;   
                                // Activate/deactivate active flag as needed
                                ActivateUserVendor(userInfo.UserInfoID, !isIndividual, userId);
                                // Save user vendor
                                SaveUserVendor(userInfo.UserInfoID, address.VendorId, address.VendorName, userId, isIndividual, false);
                            }
                        }
                    }
                }
            }
            if (statusList.Count == 0)
                UnitOfWork.Save();
            return statusList;
        }
        /// <summary>
        /// Activates the user vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="isInstitutional">if set to <c>true</c> [is institutional].</param>
        /// <param name="userId">The user identifier.</param>
        private void ActivateUserVendor(int userInfoId, bool isInstitutional, int userId)
        {
            var uvIndividual = UnitOfWork.UserRepository.GetUserVendor(userInfoId, true);
            var uvInstitutional = UnitOfWork.UserRepository.GetUserVendor(userInfoId, false);
            if (isInstitutional)
            {
                UnitOfWork.UserRepository.ActivateUserVendor(uvInstitutional, userId);
                UnitOfWork.UserRepository.DeactivateUserVendor(uvIndividual, userId);
            }
            else
            {
                UnitOfWork.UserRepository.ActivateUserVendor(uvIndividual, userId);
                UnitOfWork.UserRepository.DeactivateUserVendor(uvInstitutional, userId);
            }
        }
        /// <summary>
        /// Updates the name of the vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="isInstitutional">if set to <c>true</c> [is institutional].</param>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        private void UpdateVendorName(int userInfoId, bool isInstitutional, string name, int userId)
        {
            if (isInstitutional)
            {
                var uvInstitutional = UnitOfWork.UserRepository.GetUserVendor(userInfoId, false);
                UnitOfWork.UserRepository.SetVendorName(uvInstitutional, name, userId);
            }
            else
            {
                var uvIndividual = UnitOfWork.UserRepository.GetUserVendor(userInfoId, true);
                UnitOfWork.UserRepository.SetVendorName(uvIndividual, name, userId);
            }
        }
        /// <summary>
        /// Create a user profile.
        /// </summary>
        /// <param name="userToUpdate">User entity being updated</param>
        /// <param name="userInfo">UserInfo entity being updated</param>
        /// <param name="theGeneralInfoModel">General information model</param>
        /// <param name="theWebsiteModels">Website information model</param>
        /// <param name="theInstitutionEmailAddress"></param>
        /// <param name="thePersonalEmailAddress">Institution mail addresses information model</param>
        /// <param name="theAddresses">Address information model</param>
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
        public ICollection<SaveProfileStatus> CreateProfile(IGeneralInfoModel theGeneralInfoModel, IList<WebsiteModel> theWebsiteModels, IEmailAddressModel theInstitutionEmailAddress, IEmailAddressModel thePersonalEmailAddress, IList<AddressInfoModel> theAddresses, ProfessionalAffiliationModel theProfessionalAffiliation, IW9AddressModel w9Addresses, ICollection<IListEntry> thePhoneTypeDropdowns, ICollection<IListEntry> theAlternateContactTypeDropdowns, IList<PhoneNumberModel> thePhoneNumberModels, IList<UserAlternateContactPersonModel> theAlternateContactPhoneModels, IList<UserDegreeModel> theUserDegreeModels, IUserMilitaryRankModel theMilitaryRankModel, IUserMilitaryStatusModel theMilitarySatatusModel, int? theMilitaryServiceId, IUserVendorModel theVendorInfoIndividual, IUserVendorModel theVendorInfoInstitutional, IList<UserProfileClientModel> theUserProfileClientModels, int userId, bool isMyProfile, ref int? profileUserId)
        {
            VerifyCreateProfileParmaeters(theGeneralInfoModel, theWebsiteModels, theAddresses, w9Addresses, thePhoneTypeDropdowns, theAlternateContactTypeDropdowns, thePhoneNumberModels, theUserDegreeModels, theMilitaryRankModel, theMilitarySatatusModel, theUserProfileClientModels, userId);

            var userEntity = CreateUser(userId, theGeneralInfoModel.FirstName, theGeneralInfoModel.LastName);
            CreateUserInitialAccountStatus(userEntity, theGeneralInfoModel.ProfileTypeId, userId);
            var userInfoEntity = CreateUserInfoEntityAndAssociateWithUser(userEntity, userId);
            bool w9Verify = false;

            ICollection<SaveProfileStatus> results = UpdateProfile(userEntity, userInfoEntity, w9Verify, theGeneralInfoModel, theWebsiteModels, theInstitutionEmailAddress, thePersonalEmailAddress, theAddresses, theProfessionalAffiliation, w9Addresses, thePhoneTypeDropdowns, theAlternateContactTypeDropdowns, thePhoneNumberModels, theAlternateContactPhoneModels, theUserDegreeModels, theMilitaryRankModel, theMilitarySatatusModel, theMilitaryServiceId, theVendorInfoIndividual, theVendorInfoInstitutional, theUserProfileClientModels, isMyProfile, userId, ref profileUserId);

            return results;
        }
        /// <summary>
        /// Master method to save the user profile.  The method verifies the source data was not "stale" (i.e. another
        /// user changed the profile while it was on this users screen) and calls individual methods to save each 
        /// table related to the User object.
        /// 
        /// After all is said and done the modifications are saved.
        /// </summary>
        /// <param name="userToUpdate">User entity being updated</param>
        /// <param name="userInfo">UserInfo entity being updated</param>
        /// <param name="theGeneralInfoModel">General information model</param>
        /// <param name="theWebsiteModels">Website information model</param>
        /// <param name="theInstitutionEmailAddress"></param>
        /// <param name="thePersonalEmailAddress">Institution mail addresses information model</param>
        /// <param name="theInstitutionAddresses"> information model</param>
        /// <param name="thePersonalAddresses">Personal information model</param>
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
        /// <param name="isMyProfile">Whether the profile being save is from MyProfile</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <param name="userProfileId">User identifier of the created profile</param>
        /// <returns>Collection of SaveProfileStatus enum values or empty collection (signifies no errors)</returns>
        internal virtual ICollection<SaveProfileStatus> UpdateProfile(User userToUpdate, UserInfo userInfo, bool W9Verify, IGeneralInfoModel theGeneralInfoModel, IList<WebsiteModel> theWebsiteModels, IEmailAddressModel theInstitutionEmailAddress, IEmailAddressModel thePersonalEmailAddress, IList<AddressInfoModel> theAddress, IProfessionalAffiliationModel theProfessionalAffiliation, IW9AddressModel theW9Addresses, ICollection<IListEntry> thePhoneTypeDropdowns, ICollection<IListEntry> theAlternateContactTypeDropdowns, IList<PhoneNumberModel> thePhoneNumberModels, IList<UserAlternateContactPersonModel> theAlternateContactPhoneModels, IList<UserDegreeModel> theUserDegreeModels, IUserMilitaryRankModel theMilitaryRankModel, IUserMilitaryStatusModel theMilitarySatatusModel, int? theMilitaryServiceId, IUserVendorModel theVendorInfoIndividual, IUserVendorModel theVendorInfoInstitutional, IList<UserProfileClientModel> theUserProfileClientModels, bool isMyProfile, int userId, ref int? profileUserId)
        {
            //
            // Set up the returns
            //
            List<SaveProfileStatus> result = new List<SaveProfileStatus>();
            //
            // Now we just go through all of the related tables, one by one and update the data.  If 
            //
            SaveGeneralInfoSection(userToUpdate, theGeneralInfoModel, theWebsiteModels, userId);
            //
            // There is a single stub for each table that is related to the user profile.
            // If any are missing it should be added mimicking the established pattern
            // as shown below.
            //
            SaveUserEmails(userToUpdate, userInfo, theInstitutionEmailAddress, thePersonalEmailAddress, userId);
            //SaveInstitutionAddress(userToUpdate, userInfo, theInstitutionAddresses, userId);
            //result.AddRange(SavePersonalAddress(userToUpdate, userInfo, thePersonalAddress, userId));
            result.AddRange(SaveAddress(userToUpdate, userInfo, theAddress, userId));
            result.AddRange(SaveUserPhone(userToUpdate, userInfo, thePhoneNumberModels, userId));
            SaveAlternateContacts(userToUpdate, userInfo, theAlternateContactPhoneModels, userId);
            result.AddRange(SaveUserMilitaryRankAndStatus(userToUpdate, theMilitaryRankModel, theMilitarySatatusModel, theMilitaryServiceId, userId));
            SaveUserDegrees(userToUpdate, userInfo, theUserDegreeModels, userId);
            SaveUserProfessionalAffiliation(theProfessionalAffiliation, userInfo, userId);

            if (theVendorInfoIndividual.VendorId != null || theVendorInfoIndividual.VendorName != null)
            {
                SaveUserVendorId(userInfo.UserInfoID, theVendorInfoIndividual.VendorId, theVendorInfoIndividual.VendorName, userId, true);
            }
            if (theVendorInfoInstitutional.VendorId != null || theVendorInfoInstitutional.VendorName != null)
            {
                SaveUserVendorId(userInfo.UserInfoID, theVendorInfoInstitutional.VendorId, theVendorInfoInstitutional.VendorName, userId, false);
            }

            UpdateW9VerificationStatus(userToUpdate, theW9Addresses, userId);
            UpdateUserVerification(userToUpdate, W9Verify, userId);

            if (!isMyProfile)
            {
                SaveUserClient(theUserProfileClientModels, userToUpdate, userId);
            }

            if (result.Count() == 0)
            {
                // UserInfoChangeLog table has an Identifier field
                // This field is the primary key of the record in the indicated table containing the NewValue
                // The table is different for different UserInfoChangeType

                //
                // Generate a list of the changes to be recorded.  
                // This list will contain the correct Identifier field value for all but the changes 
                // except those generated by a record being added to an underlying table, for example UserEmail.
                //

                //
                // Create the Work List
                //
                List<EntityPropertyChange> listOfChanges = CreateWorkList(userToUpdate);

                // So save everything and set the status to success.
                //
                UnitOfWork.Save();
                result.Add(SaveProfileStatus.Success);

                //
                // Manipulate the Work List
                //
                ManipulateWorkList(listOfChanges, userToUpdate, userId);

                // So save the user info change log.
                //
                UnitOfWork.Save();

                //
                // If this was a create operation, update the userInfo entity id in the 
                // view model because the value from the property will be used to
                // set the parameter for the reroute to View.  If not we reset it to the value it was.
                //
                theGeneralInfoModel.UserInfoId = userInfo.UserInfoID;
                profileUserId = userInfo.UserID;
            }
            return result;
        }
        internal List<EntityPropertyChange> CreateWorkList(User userToUpdate)
        {
            List<EntityPropertyChange> listOfChanges = new List<EntityPropertyChange>();
            if (userToUpdate.UserID != 0)
            {
                listOfChanges = UnitOfWork.GetChangedFields();
                listOfChanges = FilterUserInfoChangeLog(listOfChanges).ToList();
            }
            return listOfChanges;
        }
        /// <summary>
        /// Adds identifier for missing entity identifier, updates work list with the changes to log
        /// </summary>
        /// <param name="userToUpdate">User entity representing the user being updated.</param>
        /// <param name="userId">User entity identifier</param>
        internal void ManipulateWorkList(List<EntityPropertyChange> listOfChanges, User userToUpdate, int userId)
        {
            //
            // Not sure if there was a better way to indicate that the user was being created.  We do not
            // want to record any changes for a user creation.
            //
            if (userToUpdate.UserID != 0)
            {
                listOfChanges = AddIdentifierForAddedFields(listOfChanges, userToUpdate.UserID);

                UpdateWorkList(listOfChanges, userToUpdate.UserInfoEntity().UserInfoID, userId);
            }
            ClearWorkList(userToUpdate, userId);
        }
        /// <summary>
        /// Add identifier for case of the change prompting the change log is a new record and not just a changed record
        /// </summary>
        /// <param name="listOfChanges"></param>
        /// <param name="profileId"></param>
        /// <returns>List of EntityPrpertyChange updated with any missing Entity identifiers</returns>
        internal List<EntityPropertyChange> AddIdentifierForAddedFields(List<EntityPropertyChange> listOfChanges, int profileId)
        {
            // get the updated record
            User userToUpdate = GetUserById(profileId);
            UserInfo userInfo = userToUpdate.UserInfoes.FirstOrDefault();

            // for new user address, set EntityId to the new user address identifier
            AddUserAddressId(listOfChanges, userInfo);

            // for new user degress, set EntityId to the new user degree identifier
            AddUserDegreeId(listOfChanges, userInfo);

            foreach (EntityPropertyChange change in listOfChanges)
            {
                if (change.EntityId < 0)
                {
                    change.EntityId = GetIdentifier(userInfo, change);
                }
            }
            return listOfChanges;
        }
        /// <summary>
        /// Add user address id as EntityId for newly added user addresses
        /// </summary>
        /// <param name="listOfChanges">The current list of changes</param>
        /// <param name="userInfo">The user info object</param>
        internal void AddUserAddressId(List<EntityPropertyChange> listOfChanges, UserInfo userInfo)
        {
            List<EntityPropertyChange> userState = listOfChanges.Where(x => x.EntityTableName == UserAddressChangeLogRules.UserAddress && x.EntityId == 0).ToList();
            while (userState.Count > 0)
            {
                string stateId = userState[0].NewValue;
                int id = Int32.Parse(stateId);

                // may be multiple addresses may have changed to the same state
                List<UserAddress> userAddress = userInfo.UserAddresses.Where(x => x.StateId == id && x.AddressTypeId != AddressType.Indexes.W9).OrderByDescending(x => x.AddressID).ToList();
                List<EntityPropertyChange> userAddressChanged = userState.Where(x => x.NewValue == stateId).ToList();

                for (int i = 0; i < userAddressChanged.Count; i++)
                {
                    userAddressChanged[i].EntityId = userAddress[i].AddressID;
                }
                userState = listOfChanges.Where(x => x.EntityTableName == UserAddressChangeLogRules.UserAddress && x.EntityId == 0).ToList();
            }
        }

        /// <summary>
        /// Add user degree id as EntityId for newly added user degree
        /// </summary>
        /// <param name="listOfChanges">The current list of changes</param>
        /// <param name="userInfo">The user info object</param>
        internal void AddUserDegreeId(List<EntityPropertyChange> listOfChanges, UserInfo userInfo)
        {
            List<EntityPropertyChange> userDegree = listOfChanges.Where(x => x.EntityTableName == UserDegreeMajorChangeLogRules.UserDegree && x.EntityId < 0).ToList();
            while (userDegree.Count > 0)
            {
                // Parse old and new value to get old and new values for degree type identifier and major
                KeyValuePair<int, string> pair = UserDegreeMajorChangeLogRules.GetMajorAndDegress(userDegree[0]);
                int degreeId = pair.Key;
                string major = pair.Value;

                major = major.Trim();

                // covers the unlikely case if they add more than one degree with the same major
                List<UserDegree> degree = userInfo.UserDegrees.Where(x => x.DegreeId == degreeId).OrderByDescending(x => x.UserDegreeId).ToList();
                List<EntityPropertyChange> userDegreeChanged = userDegree.Where(x => x.NewValue == userDegree[0].NewValue).ToList();

                //List<EntityPropertyChange> userAddressChanged = userDegree.Where(x => x.NewValue == stateId).ToList();

                for (int i = 0; i < userDegreeChanged.Count; i++)
                {
                    userDegreeChanged[i].EntityId = degree[i].UserDegreeId;
                }
                userDegree = listOfChanges.Where(x => x.EntityTableName == UserDegreeMajorChangeLogRules.UserDegree && x.EntityId < 0).ToList();
            }
        }



        /// <summary>
        /// Get the identifier added record that resulted in the user info change log record being created
        /// </summary>
        /// <param name="userInfo">The userInfo entity object</param>
        /// <param name="change">The entity proper change object</param>
        /// <returns>The identifier of the record containing the new value</returns>
        internal int GetIdentifier(UserInfo userInfo, EntityPropertyChange change)
        {
            int id;

            if (change.EntityTableName == UserEmailChangeLogRules.UserEmail)
            {
                id = userInfo.GetPrimaryEmailId();
            }
            else if (change.EntityTableName == typeof(UserResume).Name)
            {
                int? i = userInfo.GetUserResumeId();
                id = (i.HasValue) ? i.Value : 0;
            }
            else
            {
                id = change.EntityId;
            }
            return id;
        }
        /// <summary>
        /// Updates the work list with the changes to log
        /// </summary>
        /// <param name="listOfChanges"></param>
        internal virtual void UpdateWorkList(List<EntityPropertyChange> listOfChanges, int userInfoId, int userId)
        {
            //
            // Create the ServiceAction to perform the Crud operations & initialize it
            //
            UserInfoChangeLogCreateServiceAction editAction = new UserInfoChangeLogCreateServiceAction();
            editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.UserInfoChangeLogRepository, ServiceAction<UserInfoChangeLog>.DoNotUpdate, 0, userId);

            foreach (EntityPropertyChange change in listOfChanges)
            {
                editAction.Populate(change, userInfoId);
                editAction.Execute();
            }
        }
        /// <summary>
        /// Clears the Work List entries for the UserProfile if the user performing
        /// the edits has a specified permission.
        /// </summary>
        /// <param name="userToUpdate">User entity representing the user being updated.</param>
        /// <param name="userId">User entity identifier</param>
        private void ClearWorkList(User userToUpdate, int userId)
        {
            User userEntity = UnitOfWork.UserRepository.GetByID(userId);

            if ((userToUpdate.AreProfileChangesRecorded()) && (userEntity.HasPermission(Permissions.ReviewerRecruitment.ManageWorkList)))
            {
                userToUpdate.ReviewWorkList(userId);
            }
        }
        /// <summary>
        /// Saves user security questions and answers
        /// </summary>
        /// <param name="model">The user manage password model containing the questions and answers</param>
        /// <param name="userId">The user identifier</param>
        /// <returns>SaveSecurityQuestionStatus object indicating update status</returns>
        public SaveSecurityQuestionStatus SaveSecurityQuestions(IUserManagePasswordModel model, int userId, IMailService theMailService)
        {
            bool updatedPassword;
            bool updatedSecurityQuestions;

            ValidateModelExists(model, "UserProfileManagementService.SaveSecurityQuestions", "model");
            ValidateInt(userId, "UserProfileManagementService.SaveSecurityQuestions", "userId");

            User target = UnitOfWork.UserRepository.GetByID(model.UserId);
            
            AddNewUserPassword(target);
            
            updatedPassword = Update(target, model, userId);
            updatedSecurityQuestions = Update(target, model.SecurityQuestionsAndAnswers, userId);

            // account status active with permanent credentials if password is updated
            if (updatedPassword)
            {
                UpdateUserAccountStatus(target, AccountStatu.Indexes.Active, AccountStatusReason.Indexes.PermCredentials, userId, userId);               
            }

            UnitOfWork.Save();

            //Send password notification after Save. Don't want to send email if save fails for some reason.
            if (updatedPassword)
            {                
                SendPassworkChangeNotification(theMailService, userId);
            }

            return SaveSecurityUpdateStatus(updatedPassword, updatedSecurityQuestions);
        }

        private void SendPassworkChangeNotification(IMailService theMailService, int userId)
        {

           Entity.User userEntity = UnitOfWork.UserRepository.GetByID(userId);
           theMailService.SendPasswordChangeNotification(Entity.SystemTemplate.Indexes.SYSTEM_TEMPLATE_PASSWORD_CHANGE_NOTIFICATION, userEntity);
        }

        /// <summary>
        /// Sets the new user account status/reason and sets the update field values if the new status represents a change in status
        /// </summary>
        /// <param name="target">The target User Entity object</param>
        /// <param name="accountStatusId">The new accout status</param>
        /// <param name="accountStatusReasonId">The new account status reason</param>
        /// <param name="userToChangeId">The user identifier of the changed user profile</param>
        /// <param name="userId">The dentifier of the user making the change</param>
        internal void UpdateUserAccountStatus(User target, int accountStatusId, int accountStatusReasonId, int userToChangeId, int userId)
        {
            UserAccountStatu statusEntity = target.UserAccountStatus.FirstOrDefault();

            // only update modified fields when it is set to a different status
            if (statusEntity.AccountStatusReasonId == accountStatusReasonId)
            {
                statusEntity.Populate(AccountStatu.Indexes.Active, AccountStatusReason.Indexes.PermCredentials, userId, userId);
            }
            else
            {
                statusEntity.UpdateUserAccountStatus(AccountStatusReason.Indexes.PermCredentials, userId, userId);
            }

            UnitOfWork.UserAccountStatusRepository.Update(statusEntity);
        }

        #region Individual User Profile section save methods
        /// <summary>
        /// Saves the data in the 'General Info' section.
        /// </summary>
        /// <param name="profileId">User identifier of the profile being updated</param>
        /// <param name="theGeneralInfoModel">General information model</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns>Collection of SaveProfileStatus enum values or empty collection (signifies no errors)</returns>
        internal virtual void SaveGeneralInfoSection(User theUserEntity, IGeneralInfoModel theGeneralInfoModel, IList<WebsiteModel> theWebsiteModels, int userId)
        {
            UserInfo userInfoEntity = theUserEntity.UserInfoes.FirstOrDefault();
            //
            // Now change the individual objects (UserWebsite  & UserInfo) objects associated with the General info.
            //
            Update(userInfoEntity, theWebsiteModels, userId);
            Update(userInfoEntity, theGeneralInfoModel, userId);
            //
            // And finally set the Modified date & times if there were changes
            // to the object.  (Note.  If the user has been created the create
            // datetime fields should be updated.  They were updated when the entity object
            // was created.
            //
            Helper.UpdateUserModifiedFields(this.UnitOfWork, theUserEntity, userId);
            Helper.UpdateModifiedFields(this.UnitOfWork, userInfoEntity, userId);
        }
        /// <summary>
        /// Update a user's UserInfo entity object
        /// </summary>
        /// <param name="userInfoEntity">UserInfo entity object</param>
        /// <param name="theGeneralInfoModel">General info model with edit information</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void Update(UserInfo userInfoEntity, IGeneralInfoModel theGeneralInfoModel, int userId)
        {
            ///
            /// Make the changes to the UserInfo entity
            /// 
            userInfoEntity.FirstName = theGeneralInfoModel.FirstName;
            userInfoEntity.MiddleName = theGeneralInfoModel.MI;
            userInfoEntity.LastName = theGeneralInfoModel.LastName;
            userInfoEntity.NickName = theGeneralInfoModel.NickName;
            userInfoEntity.PrefixId = theGeneralInfoModel.PrefixId;
            userInfoEntity.SuffixText = theGeneralInfoModel.Suffix;
            userInfoEntity.BadgeName = theGeneralInfoModel.Badge;
            userInfoEntity.GenderId = theGeneralInfoModel.GenderId;
            userInfoEntity.EthnicityId = theGeneralInfoModel.EthinicityId;
            userInfoEntity.AcademicRankId = theGeneralInfoModel.AcademicRankId;
            userInfoEntity.DegreeNotApplicable = theGeneralInfoModel.DegreeNotApplicable;
            userInfoEntity.Expertise = theGeneralInfoModel.Expertise;

            UserProfile userProfileEntity = userInfoEntity.UserProfiles.FirstOrDefault();
            ///
            /// This user does not have a profile type (must be a create) so we add it.
            /// 
            if (userProfileEntity == null)
            {
                userProfileEntity = new UserProfile();
                userProfileEntity.Populate((int)theGeneralInfoModel.ProfileTypeId);
                userInfoEntity.UserProfiles.Add(userProfileEntity);

                UnitOfWork.UserProfileRepository.Add(userProfileEntity);

                Helper.UpdateCreatedFields(userProfileEntity, userId);
                Helper.UpdateModifiedFields(userProfileEntity, userId);
            }
            //
            // Now add the role.
            //
            UserSystemRoleServiceAction editAction = new UserSystemRoleServiceAction();
            editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.UserSystemRoleRepository, ServiceAction<UserSystemRole>.DoNotUpdate, userId, theGeneralInfoModel);
            editAction.Execute();

            //
            // It is my understanding that there is no way that the profile type can be modified interactively
            // so we do not need to worry about the modify & delete cases.
            //
        }
        /// <summary>
        /// Updates the website information from the General info section
        /// </summary>
        /// <param name="userInfoEntity">UserInfo entity that contains the websites</param>
        /// <param name="theWebsiteModels">Website model with edit information</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void Update(UserInfo userInfoEntity, IList<WebsiteModel> theWebsiteModels, int userId)
        {
            foreach (var website in theWebsiteModels)
            {
                if ((website.UserWebsiteId == 0) & (website.HasData()))
                {
                    AddNewWebsite(website, userInfoEntity, userId);
                }
                else if ((website.UserWebsiteId == 0) & (!website.HasData()))
                {
                    //
                    // This was a model that was added to provide a buffer to bind to in the view.
                    // Since there was nothing there we don't need to do anything.
                    //
                }
                else
                {
                    //
                    // Locate this website
                    //
                    var target = UnitOfWork.UserWebsiteRepository.GetByID(website.UserWebsiteId);
                    //
                    // if we have something then it could be a modify or no change occurred
                    //
                    if (!string.IsNullOrWhiteSpace(website.WebsiteAddress))
                    {
                        ModifyWebsite(website, target, userId);
                    }
                    //
                    // The website address was empty which indicates it should be deleted
                    //
                    else
                    {
                        DeleteWebsite(target, userId);
                    }
                }
            }
        }
        /// <summary>
        /// Modify an existing website reference
        /// </summary>
        /// <param name="website">The WebModel containing the data</param>
        /// <param name="userWebsite">The user website to update</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void ModifyWebsite(IWebsiteModel website, UserWebsite userWebsite, int userId)
        {
            userWebsite.Populate(website.WebsiteAddress, website.WebsiteTypeId);

            Helper.UpdateModifiedFields(this.UnitOfWork, userWebsite, userId);
        }
        /// <summary>
        /// Add a new website entity.
        /// </summary>
        /// <param name="website">WebModel representing the user Website to add</param>
        /// <param name="userInfoEntity">UserInfo identifier</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void AddNewWebsite(IWebsiteModel website, UserInfo userInfoEntity, int userId)
        {
            UserWebsite userWebsiteEntity = new UserWebsite();
            userWebsiteEntity.Populate(website.WebsiteAddress, website.WebsiteTypeId);

            userInfoEntity.UserWebsites.Add(userWebsiteEntity);
            Helper.UpdateCreatedFields(userWebsiteEntity, userId);
            Helper.UpdateModifiedFields(userWebsiteEntity, userId);

            UnitOfWork.UserWebsiteRepository.Add(userWebsiteEntity);
        }
        /// <summary>
        /// Delete a user's website.
        /// </summary>
        /// <param name="theUserWebsite">The website to delete</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void DeleteWebsite(UserWebsite theUserWebsite, int userId)
        {
            Helper.UpdateDeletedFields(theUserWebsite, userId);
            UnitOfWork.UserWebsiteRepository.Delete(theUserWebsite.UserWebsiteId);
        }
        #region Verifying & Saving UserEmail data
        /// <summary>
        /// Updates the UserEmails object of a user's profile.
        /// </summary>
        /// <param name="theUser">The user whose profile is being modified</param>
        /// <param name="theInstitutionalEmail">The institutional email address</param>
        /// <param name="thePersonalEmail">the personal email address</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void SaveUserEmails(User theUser, UserInfo userInfo, IEmailAddressModel theInstitutionalEmail, IEmailAddressModel thePersonalEmail, int userId)
        {
            Update(theUser, userInfo, theInstitutionalEmail, EmailAddressType.Business, userId);
            Update(theUser, userInfo, thePersonalEmail, EmailAddressType.Personal, userId);
        }
        /// <summary>
        /// Updates the email information from the General info section
        /// </summary>
        /// <param name="theUser">The user whose profile is being modified</param>
        /// <param name="userInfoId">Unique identifier of info entity that contains the websites</param>
        /// <param name="email">email model with edit information</param>
        /// <param name="emailType">email type identifier</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void Update(User theUser, UserInfo userInfo, IEmailAddressModel email, int emailAddressType, int userId)
        {
            // 1. a valid address field with a invalid identifier indicates an add
            // 2. a null address field and an invalid identifier indicates that no action is to be taken
            // 3. a valid address field with a valid email identifier indicates a 'potential' modify
            // 4. a null address field with a valid email identifier indicates a delete

            email.EmailAddressTypeId = emailAddressType;

            if (email.IsAdd)
            {
                AddNewEmailAddress(userInfo, email, theUser.UserInfoes.FirstOrDefault().UserInfoID, userId);
            }
            else if (email.IsDeleted())
            {
                DeleteEmailAddress(email.EmailId, userId);
            }
            else if (email.EmailId == 0 && string.IsNullOrWhiteSpace(email.Address))
            {
                // do nothing
            }
            else
            {
                ModifyEmailAddress(email, email.EmailId, userId);
            }
        }
        /// <summary>
        /// Modify an existing email address
        /// </summary>
        /// <param name="emailAddress">The WebModel containing the data</param>
        /// <param name="emailId">The user email identifier to update</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void ModifyEmailAddress(IEmailAddressModel emailAddress, int emailId, int userId)
        {
            var theUserEmail = UnitOfWork.UserEmailRepository.GetByID(emailId);

            theUserEmail.Populate(emailAddress.Address, emailAddress.EmailAddressTypeId, theUserEmail.UserInfoID, emailAddress.Primary);
            Helper.UpdateModifiedFields(this.UnitOfWork, theUserEmail, userId);
        }
        /// <summary>
        /// Add a new website entity.
        /// </summary>
        /// <param name="userInfoEntity">UserInfo entity</param>
        /// <param name="emailAddress">WebModel representing the user Email to add</param>
        /// <param name="userInfoId">UserInfo identifier</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void AddNewEmailAddress(UserInfo userInfoEntity, IEmailAddressModel emailAddress, int userInfoId, int userId)
        {
            UserEmail newEmail = new UserEmail();
            newEmail.Populate(emailAddress.Address, emailAddress.EmailAddressTypeId, userInfoId, emailAddress.Primary);
            userInfoEntity.UserEmails.Add(newEmail);

            Helper.UpdateCreatedFields(newEmail, userId);
            Helper.UpdateModifiedFields(newEmail, userId);

            UnitOfWork.UserEmailRepository.Add(newEmail);
        }
        /// <summary>
        /// Delete a user's email address.
        /// </summary>
        /// <param name="emailId">The email address identifier to delete</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void DeleteEmailAddress(int emailId, int userId)
        {
            var theUserEmail = UnitOfWork.UserEmailRepository.GetByID(emailId);

            Helper.UpdateDeletedFields(theUserEmail, userId);
            UnitOfWork.UserEmailRepository.Delete(theUserEmail);
        }
        /// <summary>
        /// Updates the users professional affiliation
        /// </summary>
        /// <param name="affiliation">The profession affiliation moded</param>
        /// <param name="userInfo">The userinfo entitity object to update</param>
        /// <param name="userId">The user identifier</param>
        internal void SaveUserProfessionalAffiliation(IProfessionalAffiliationModel affiliation, UserInfo userInfo, int userId)
        {
            userInfo.UpdateProfessionalAffiliation(affiliation.ProfessionalAffiliationId, affiliation.Name, affiliation.Department, affiliation.Position, userId);
        }
        #endregion
        /// <summary>
        /// Updates the institution addresses object of a user's profile.
        /// </summary>
        /// <param name="User">User identifier of the profile being updated</param>
        /// <param name="addresses">List of institutional addresses</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns>Collection of SaveProfileStatus enum values or empty collection (signifies no errors)</returns>
        internal void SaveInstitutionAddress(User theUser, UserInfo userInfo, IList<InstitutionAddressModel> addresses, int userId)
        {
            Update(userInfo, addresses, userId);
        }
        /// <summary>
        /// Updates the addresses object of a user's profile.
        /// </summary>
        /// <param name="User">User identifier of the profile being updated</param>
        /// <param name="address">The user addresses </param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns>Collection of SaveProfileStatus enum values or empty collection (signifies no errors)</returns>
        internal ICollection<SaveProfileStatus> SaveAddress(User theUser, UserInfo userInfo, IList<AddressInfoModel> address, int userId)
        {
            Update(userInfo, address, userId);
            return VerifyAddress(userInfo, address, userId);
        }


        #region SaveUserPhone
        /// <summary>
        /// Updates the UserPhone object of a user's profile.
        /// </summary>
        /// <param name="theUser">User entity of the profile being updated</param>
        /// <param name="userInfo">UserInfo entity</param>
        /// <param name="thePhoneNumberModels">List of phone number models</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns>Collection of SaveProfileStatus enum values or empty collection (signifies no errors)</returns>
        internal ICollection<SaveProfileStatus> SaveUserPhone(User theUser, UserInfo userInfo, IList<PhoneNumberModel> thePhoneNumberModels, int userId)
        {
            Update(theUser, userInfo.UserInfoID, thePhoneNumberModels, userId);
            return VerifyUserPhones(theUser, userId);
        }

        /// <summary>
        /// Updates the user's phone numbers based on the editable models.
        /// </summary>
        /// <param name="theUser">The user whose profile is being modified</param>
        /// <param name="userInfoId">Unique identifier of info entity that contains the websites</param>
        /// <param name="theWebsiteModels">Phone number model with edit information</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void Update(User theUser, int userInfoId, IList<PhoneNumberModel> thePhoneNumberModels, int userId)
        {
            foreach (var phone in thePhoneNumberModels)
            {
                //
                // Locate this phone record
                //
                var target = phone.PhoneId > 0 ? UnitOfWork.UserPhoneRepository.GetByID(phone.PhoneId) : null;

                //
                // Determine action to take
                //
                if (Helper.IsAdd(phone.PhoneId) & phone.HasData())
                {
                    AddNewPhone(phone, userInfoId, userId);
                }
                else if (phone.IsDeleted())
                {
                    DeletePhone(target, userId);
                }
                //
                // if we have something then it could be a modify or no change occurred
                //
                else if ((target != null) && !(string.IsNullOrWhiteSpace(phone.Number)))
                {
                    ModifyPhone(phone, target, userId);
                }
                else
                {
                    // do nothing
                }
            }
        }
        /// <summary>
        /// Modify an existing phone reference
        /// </summary>
        /// <param name="phone">The WebModel containing the data</param>
        /// <param name="userPhone">The user phone to update</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void ModifyPhone(IPhoneNumberModel phone, UserPhone userPhone, int userId)
        {
            userPhone.Phone = phone.Number;
            userPhone.Extension = phone.Extension;
            userPhone.PrimaryFlag = phone.Primary;
            userPhone.PhoneTypeId = phone.PhoneTypeId;
            Helper.UpdateModifiedFields(this.UnitOfWork, userPhone, userId);

            UnitOfWork.UserPhoneRepository.Update(userPhone);
        }
        /// <summary>
        /// Add a new user phone entity.
        /// </summary>
        /// <param name="website">WebModel representing the user phone to add</param>
        /// <param name="userInfoId">UserInfo identifier</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void AddNewPhone(IPhoneNumberModel phone, int userInfoId, int userId)
        {
            UserPhone newPhone = new UserPhone();
            newPhone.Populate(userInfoId, phone.Number, phone.Extension, phone.PhoneTypeId, phone.Primary, phone.International, userId);
            Helper.UpdateCreatedFields(newPhone, userId);
            Helper.UpdateModifiedFields(newPhone, userId);

            UnitOfWork.UserPhoneRepository.Add(newPhone);
        }
        /// <summary>
        /// Delete a user's phone.
        /// </summary>
        /// <param name="theUserPhone">The phone to delete</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void DeletePhone(UserPhone theUserPhone, int userId)
        {
            Helper.UpdateDeletedFields(theUserPhone, userId);
            UnitOfWork.UserPhoneRepository.Delete(theUserPhone);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theUser"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal ICollection<SaveProfileStatus> VerifyUserPhones(User theUser, int userId)
        {
            //
            // Validate the updated entity models
            //
            // User has already been validated
            theUser.IsPhonePrimaryValid();

            List<SaveProfileStatus> results = new List<SaveProfileStatus>();
            results.AddRange(theUser.Errors);

            return results;
        }
        #endregion
        #region Alternate Contacts
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theUser">User identifier of the profile being updated</param>
        /// <param name="userInfo">User information of the user being updated</param>
        /// <param name="theAlternateContactPhoneModels">List of alternate contact information for this user</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void SaveAlternateContacts(User theUser, UserInfo userInfo, IList<UserAlternateContactPersonModel> theAlternateContactPersonModels, int userId)
        {
            Update(userInfo.UserInfoID, theAlternateContactPersonModels, userId);
        }
        #endregion
        /// <summary>
        /// Updates the UserDegree object of a user's profile.
        /// </summary>
        /// <param name="theUser">The user whose profile is being modified</param>
        /// <param name="userInfo">The userInfo entity object</param>
        /// <param name="theUserDegreeModels">UserDegree models</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns>Collection of SaveProfileStatus enum values or empty collection (signifies no errors)</returns>
        internal void SaveUserDegrees(User theUser, UserInfo userInfo, IList<UserDegreeModel> theUserDegreeModels, int userId)
        {
            Update(userInfo.UserInfoID, theUserDegreeModels, userId);
            return;
        }
        /// <summary>
        /// Updates the User's military rank & status
        /// </summary>
        /// <param name="profileId">User identifier of the profile being updated</param>
        /// <param name="theMilitaryRankModel">MilitaryRankModel - provides the users service and rank</param>
        /// <param name="theMilitarySatatusModel">Military status model - provides the users military status</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <param name="theMilitaryServiceId">Military Service identifier</param>
        /// <returns>Collection of SaveProfileStatus enum values or empty collection (signifies no errors)</returns>
        internal ICollection<SaveProfileStatus> SaveUserMilitaryRankAndStatus(User theUser, IUserMilitaryRankModel theMilitaryRankModel, IUserMilitaryStatusModel theMilitarySatatusModel, int? theMilitaryServiceId, int userId)
        {
            UserInfo userInfo = theUser.UserInfoes.FirstOrDefault();

            Update(userInfo, theMilitaryRankModel, theMilitarySatatusModel, userId);
            return VerifyUserMilitaryRankAndStatus(userInfo, theMilitaryServiceId, userId);
        }
        /// <summary>
        /// Updates the user's W9 verification status
        /// </summary>
        /// <param name="theW9Addresses">The W9 address model</param>
        /// <param name="userId">User entity identifier</param>
        internal virtual void UpdateW9VerificationStatus(User theUser, IW9AddressModel theW9Addresses, int userId)
        {
            theUser.Populate(theW9Addresses.W9Verified);
            Helper.UpdateUserModifiedFields(theUser, userId);
        }
        /// <summary>
        /// Resets the w9 verification status.
        /// </summary>
        /// <param name="theUser">The user.</param>
        /// <param name="userId">The user identifier.</param>
        internal virtual void ResetW9VerificationStatus(User theUser, int userId)
        {
            theUser.W9Verified = null;
            theUser.W9VerifiedDate = null;
            Helper.UpdateUserModifiedFields(theUser, userId);
        }
        /// <summary>
        /// Verifies the w9 status.
        /// </summary>
        /// <param name="theUser">The user.</param>
        /// <param name="userId">The user identifier.</param>
        internal virtual void VerifyW9Status(User theUser, int userId)
        {
            theUser.W9Verified = true;
            theUser.W9VerifiedDate = GlobalProperties.P2rmisDateTimeNow;
            Helper.UpdateUserModifiedFields(theUser, userId);
        }
        /// <summary>
        /// Updates the user's verification status
        /// </summary>
        /// <param name="theW9Addresses">The W9 address model</param>
        /// <param name="isUpdateForVerify">Indicates if this is for an verification</param>
        /// <param name="userId">User entity identifier</param>
        internal virtual void UpdateUserVerification(User theUser, bool isUpdateForVerify, int userId)
        {
            //
            // Currently the only option for the user is to "Verify" their informaiton.
            // There is no option to indication that it is not valid.  So by implication 
            // if the user 'Saves' the form they are saying the informaiton is valid.
            //
            if (isUpdateForVerify)
            {
                theUser.Populate();
                Helper.UpdateUserModifiedFields(theUser, userId);
            }
        }
        #endregion
        #region Update methods
        #region Update - Institutional Degrees
        /// <summary>
        /// Updates the degree information from the General info section
        /// </summary>
        /// <param name="DegreeNA">Are the degrees not applicable</param>
        /// <param name="userInfoId">Unique identifier of info entity that contains the websites</param>
        /// <param name="theUserDegreeModels">UserDegree model with edit information</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void Update(int userInfoId, IList<UserDegreeModel> theUserDegreeModels, int userId)
        {
            foreach (var userDegree in theUserDegreeModels)
            {
                if (userDegree.IsDeleted())
                {
                    var target = UnitOfWork.UserDegreeRepository.GetByID(userDegree.UserDegreeId);
                    DeleteDegree(target, userId);
                }
                else if (userDegree.UserDegreeId > 0)
                {
                    var target = UnitOfWork.UserDegreeRepository.GetByID(userDegree.UserDegreeId);
                    ModifyDegree(userDegree, target, userId);
                }
                else if (userDegree.HasData())
                {
                    AddNewDegree(userDegree, userInfoId, userId);
                }
            }
        }
        /// <summary>
        /// Add a new degree entity.
        /// </summary>
        /// <param name="userDegreeModel">WebModel representing the user degree to add</param>
        /// <param name="userInfoId">UserInfo identifier</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void AddNewDegree(UserDegreeModel userDegreeModel, int userInfoId, int userId)
        {
            UserDegree newDegree = new UserDegree();
            newDegree.Populate((int)userDegreeModel.DegreeId, userDegreeModel.Major, userInfoId, userId);
            Helper.UpdateCreatedFields(newDegree, userId);
            Helper.UpdateModifiedFields(newDegree, userId);

            UnitOfWork.UserDegreeRepository.Add(newDegree);
        }
        /// <summary>
        /// Modify an existing degree reference
        /// </summary>
        /// <param name="userDegreeModel">The WebModel containing the data</param>
        /// <param name="userDegree">The user degree to update</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void ModifyDegree(UserDegreeModel userDegreeModel, UserDegree userDegree, int userId)
        {
            userDegree.DegreeId = (int)userDegreeModel.DegreeId;
            userDegree.Major = userDegreeModel.Major;
            Helper.UpdateModifiedFields(this.UnitOfWork, userDegree, userId);
        }
        /// <summary>
        /// Delete a user's degree.
        /// </summary>
        /// <param name="userDegree">The degree to delete</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void DeleteDegree(UserDegree userDegree, int userId)
        {
            Helper.UpdateDeletedFields(userDegree, userId);
            UnitOfWork.UserDegreeRepository.Delete(userDegree.UserDegreeId);
        }
        /// <summary>
        /// Add a new user password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        internal void AddNewUserPassword(User user)
        {
            if (user.IsPermanentCredentials())
            {
                var newUserPassword = new UserPassword();
                newUserPassword.Populate(user.UserID, user.Password, user.PasswordDate, user.PasswordSalt);
                UnitOfWork.UserPasswordRepository.Add(newUserPassword);
            }
        }
        #endregion
        #region Updated Institutional addresses
        /// <summary>
        /// Updates the user profile data from the GeneralInfo section.
        /// </summary>
        /// <param name="theUser">The user whose profile is being modified</param>
        /// <param name="theGeneralInfoModel">General info model with edit information</param>
        /// <param name="theWebsiteModels">Website model with edit information</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns>Collection of SaveProfileStatus enum values or empty collection (signifies no errors)</returns>
        /// <remarks>need unit test</remarks>
        internal void Update(UserInfo userInfo, IList<InstitutionAddressModel> list, int userId)
        {
            foreach (var item in list)
            {
                //
                // Adds are identified by not having an address id.
                if (Helper.IsAdd(item.AddressId, item.IsDeleted()) && (item.HasData()))
                {
                    Add(userInfo, item, userId);
                }
                else if (item.IsDeleted())
                {
                    Delete(userInfo, item, userId);
                }
                else if (!Helper.IsAdd(item.AddressId))
                {
                    Modify(userInfo, item, userId);
                }
            }
        }
        /// <summary>
        /// Updates the specified user information.
        /// </summary>
        /// <param name="userInfo">The user information.</param>
        /// <param name="address">The address.</param>
        /// <param name="userId">The user identifier.</param>
        internal void Update(UserInfo userInfo, AddressInfoModel address, int userId)
        {
            // Adds are identified by not having an address id.
            if (Helper.IsAdd(address.UserAddressId, address.IsDeletable) && address.HasData())
            {
                Add(userInfo, address, userId);
            }
            else if (address.IsDeleted())
            {
                Delete(userInfo, address, userId);
            }
            else if (!Helper.IsAdd(address.UserAddressId))
            {
                Modify(userInfo, address, userId);
            }
            else
            {
                // do nothing
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="model"></param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns></returns>
        /// <remarks>need unit test</remarks>
        internal void Add(UserInfo userInfo, IInstitutionAddressModel model, int userId)
        {
            //
            // Create the new address; populate it & add it to the UserInfo's collection
            //
            UserAddress entity = new UserAddress();

            entity.Populate(model.Address.IsPreferredAddress, AddressType.Indexes.Organization, model.Address.Address1, model.Address.Address2, model.Address.Address3, model.Address.Address4, model.Address.City, model.Address.StateId, model.Address.Zip, model.Address.CountryId);
            userInfo.UserAddresses.Add(entity);

            Helper.UpdateCreatedFields(entity, userId);
            Helper.UpdateModifiedFields(entity, userId);
            //
            // no longer have position data for an institutional address
            // Now we need to handle the position for this address.  
            // (only if it has data)
            //
            //if (model.NewPosition.HasData())
            //{
            //    Add(entity, model.NewPosition, userId);
            //}
            UnitOfWork.UserAddressRepository.Add(entity);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="model"></param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns></returns>
        /// <remarks>need unit test</remarks>
        internal void Delete(UserInfo userInfo, IInstitutionAddressModel model, int userId)
        {
            var entity = userInfo.UserAddresses.FirstOrDefault(x => x.AddressID == model.AddressId);

            Helper.UpdateDeletedFields(entity, userId);
            UnitOfWork.UserAddressRepository.Delete(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="model"></param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns></returns>
        /// <remarks>need unit test</remarks>
        internal void Modify(UserInfo userInfo, IInstitutionAddressModel model, int userId)
        {
            var entity = userInfo.UserAddresses.FirstOrDefault(x => x.AddressID == model.AddressId);

            entity.Populate(model.Address.IsPreferredAddress, AddressType.Indexes.Organization, /*model.InstitutionName,*/ model.Address.Address1, model.Address.Address2, model.Address.Address3, model.Address.Address4, model.Address.City, model.Address.StateId, model.Address.Zip, model.Address.CountryId);
            Helper.UpdateModifiedFields<UserAddress>(UnitOfWork, entity, userId);
            //
            //  positions no longer associated with institution address
            // Now deal with the positions (which can be adds; modify or delete)
            //
        }
        #endregion
        #region Update Alternate Contacts
        /// <summary>
        /// Updates the alternate contact information
        /// </summary>
        /// <param name="userInfoId">Unique identifier of info entity that contains the websites</param>
        /// <param name="theAlternateContactPersonModels">UserDegree model with edit information</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void Update(int userInfoId, IList<UserAlternateContactPersonModel> theAlternateContactPersonModels, int userId)
        {
            foreach (UserAlternateContactPersonModel userContact in theAlternateContactPersonModels)
            {
                Update(userInfoId, userContact, userId);
            }
        }
        /// <summary>
        /// Updates the specified user information identifier.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="userContact">The user contact.</param>
        /// <param name="userId">The user identifier.</param>
        internal void Update(int userInfoId, IUserAlternateContactPersonModel userContact, int userId)
        {
            //
            // Locate this contact
            //
            if (Helper.IsAdd(userContact.UserAlternateContactId) & userContact.HasData())
            {
                AddNewAlternateContact(userContact, userInfoId, userId);
            }
            else if (userContact.IsDeleted())
            {
                var target = UnitOfWork.UserAlternateContactRepository.GetByID(userContact.UserAlternateContactId);
                DeleteAlternateContact(target, userId);
            }
            else if (!Helper.IsAdd(userContact.UserAlternateContactId) && userContact.UserAlternateContactTypeId.HasValue)
            {
                var target = UnitOfWork.UserAlternateContactRepository.GetByID(userContact.UserAlternateContactId);
                ModifyAlternateContact(userContact, target, userId);
            }
            else
            {
                // do nothing
            }
        }
        /// <summary>
        /// Delete an alternate contact.
        /// </summary>
        /// <param name="userDegree">The degree to delete</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal void DeleteAlternateContact(UserAlternateContact userAlternateContact, int userId)
        {
            // get a list of the user alternate contact phone ids to delete
            // as we cannot modify the UserAlternateContact object when we are using it as the object in a foreach loop.
            List<int> phoneList = new List<int>();
            foreach (UserAlternateContactPhone phone in userAlternateContact.UserAlternateContactPhones)
            {
                phoneList.Add(phone.UserAlternateContactPhoneId);
            }

            foreach (int phoneId in phoneList)
            {
                var target = UnitOfWork.UserAlternateContactPhoneRepository.GetByID(phoneId);

                Helper.UpdateDeletedFields(target, userId);
                UnitOfWork.UserAlternateContactPhoneRepository.Delete(target);
            }

            Helper.UpdateDeletedFields(userAlternateContact, userId);
            UnitOfWork.UserAlternateContactRepository.Delete(userAlternateContact);

        }
        /// <summary>
        /// Modify alternate contact
        /// </summary>
        /// <param name="userContactModel">The alternate contact person web model</param>
        /// <param name="userAlternateContact">The UserAlternateContact object to be modified</param>
        /// <param name="userId">The user identifier of the user making the change</param>
        internal void ModifyAlternateContact(IUserAlternateContactPersonModel userContactModel, UserAlternateContact userAlternateContact, int userId)
        {
            userAlternateContact.Modify(userContactModel.UserAlternateContactTypeId.HasValue ? userContactModel.UserAlternateContactTypeId.Value : 0,
                userContactModel.Email, userContactModel.FirstName, userContactModel.LastName, userContactModel.PrimaryFlag, userId);

            foreach (IUserAlternatePersonContactPhoneModel phone in userContactModel.AlternateContactPhone)
            {
                UserAlternateContactPhone target;

                if (phone.UserAlternateContactPhoneId.HasValue)
                {
                    // until the dust settles concerning delete and validations, don't ask the db to store null for phone number pr phone type id - they is not nullable in the database
                    if (!string.IsNullOrWhiteSpace(phone.Number) && phone.PhoneTypeId.HasValue)
                    {
                        // modify alternate contact's phone number information
                        target = UnitOfWork.UserAlternateContactPhoneRepository.GetByID(phone.UserAlternateContactPhoneId);

                        // modify existing phone
                        target.International = phone.International;
                        target.PhoneExtension = phone.Extension;
                        target.PhoneNumber = phone.Number;
                        target.PhoneTypeId = phone.PhoneTypeId.HasValue ? phone.PhoneTypeId.Value : 0;
                        target.PrimaryFlag = phone.PrimaryFlag;

                        Helper.UpdateModifiedFields(target, userId);
                        UnitOfWork.UserAlternateContactPhoneRepository.Update(target);
                    }
                    else if (string.IsNullOrWhiteSpace(phone.Number))
                    {
                        // delete alternate contact's phone number information
                        target = UnitOfWork.UserAlternateContactPhoneRepository.GetByID(phone.UserAlternateContactPhoneId);

                        Helper.UpdateDeletedFields(target, userId);
                        UnitOfWork.UserAlternateContactPhoneRepository.Delete(target);

                    }
                    else
                    {
                        // do nothing
                    }
                }

                else if (!string.IsNullOrWhiteSpace(phone.Number))
                {
                    bool primary = phone.PrimaryFlag;

                    // add new phone to contact - could be adding a null or empty phone to get to the minimum number of phones required
                    UserAlternateContactPhone entity = new UserAlternateContactPhone();
                    entity.Populate(phone.International, phone.Extension, phone.Number, phone.PhoneTypeId.HasValue ? phone.PhoneTypeId.Value : 0, primary, userContactModel.UserAlternateContactId, userId);
                    UnitOfWork.UserAlternateContactPhoneRepository.Add(entity);
                    userAlternateContact.UserAlternateContactPhones.Add(entity);
                }
                else
                {
                    // do nothing
                }
            }
            // includes adding of alternate phone from above
            UnitOfWork.UserAlternateContactRepository.Update(userAlternateContact);

        }
        /// <summary>
        /// Add a new alternate contact
        /// </summary>
        /// <param name="userContactModel">User alternate contact person model</param>
        /// <param name="userInfoId">The user information identifier of the user being updated</param>
        /// <param name="userId">The user identifier of the user making the change</param>
        internal void AddNewAlternateContact(IUserAlternateContactPersonModel userContactModel, int userInfoId, int userId)
        {
            UserAlternateContact entity = new UserAlternateContact();
            int altContactTypeId = userContactModel.UserAlternateContactTypeId.HasValue ? userContactModel.UserAlternateContactTypeId.Value : 0;

            entity.Populate(userInfoId, altContactTypeId, userContactModel.Email, userContactModel.FirstName, userContactModel.LastName, userContactModel.PrimaryFlag, userId);
            Helper.UpdateModifiedFields(entity, userId);

            foreach (IUserAlternatePersonContactPhoneModel phone in userContactModel.AlternateContactPhone)
            {
                bool primary = phone.PrimaryFlag;
                // add new phone to contact - could be adding a null or empty phone to get to the minimum number of phones required
                UserAlternateContactPhone phoneEntity = new UserAlternateContactPhone();
                phoneEntity.Populate(phone.International, phone.Extension, phone.Number, phone.PhoneTypeId.HasValue ? phone.PhoneTypeId.Value : 0, primary, userContactModel.UserAlternateContactId, userId);
                Helper.UpdateModifiedFields(phoneEntity, userId);

                if (phoneEntity.IsValid())
                {
                    UnitOfWork.UserAlternateContactPhoneRepository.Add(phoneEntity);

                    // add this phone to the user alternate contact entity.
                    entity.UserAlternateContactPhones.Add(phoneEntity);
                }
            }
            UnitOfWork.UserAlternateContactRepository.Add(entity);
        }

        #endregion
        #region Update Personal Address
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="address">The address to be modified, added or deleted</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <remarks>need unit test</remarks>
        internal void Update(UserInfo userInfo, IList<AddressInfoModel> list, int userId)
        {
            foreach (var item in list)
            {
                Update(userInfo, item, userId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="model"></param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns></returns>
        /// <remarks>need unit test</remarks>
        internal void Add(UserInfo userInfo, IAddressInfoModel model, int userId)
        {
            //
            // Create the new address; populate it & add it to the UserInfo's collection
            //
            UserAddress entity = new UserAddress();

            entity.Populate(model.IsPreferredAddress, model.AddressTypeId.Value, model.Address1, model.Address2, model.Address3, model.Address4, model.City, model.StateId, model.Zip, model.CountryId);
            userInfo.UserAddresses.Add(entity);
            Helper.UpdateCreatedFields(entity, userId);
            Helper.UpdateModifiedFields(entity, userId);

            UnitOfWork.UserAddressRepository.Add(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="model"></param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns></returns>
        /// <remarks>need unit test</remarks>
        internal void Delete(UserInfo userInfo, IAddressInfoModel model, int userId)
        {
            var entity = userInfo.UserAddresses.FirstOrDefault(x => x.AddressID == model.UserAddressId);

            Helper.UpdateDeletedFields(entity, userId);
            UnitOfWork.UserAddressRepository.Delete(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="model"></param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns></returns>
        /// <remarks>need unit test</remarks>
        internal void Modify(UserInfo userInfo, IAddressInfoModel model, int userId)
        {
            var entity = userInfo.UserAddresses.FirstOrDefault(x => x.AddressID == model.UserAddressId);

            entity.Populate(model.IsPreferredAddress, model.AddressTypeId.Value, model.Address1, model.Address2, model.Address3, model.Address4, model.City, model.StateId, model.Zip, model.CountryId);
            Helper.UpdateModifiedFields<UserAddress>(UnitOfWork, entity, userId);
        }
        #endregion
        #region Update Military information
        /// <summary>
        /// Updates the user profile data from the GeneralInfo section.
        /// </summary>
        /// <param name="theUser">The user whose profile is being modified</param>
        /// <param name="theGeneralInfoModel">General info model with edit information</param>
        /// <param name="theWebsiteModels">Website model with edit information</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns>Collection of SaveProfileStatus enum values or empty collection (signifies no errors)</returns>
        internal void Update(UserInfo userInfo, IUserMilitaryRankModel theMilitaryRankModel, IUserMilitaryStatusModel theMilitarySatatusModel, int userId)
        {
            //
            // Make the changes to the UserInfo entity
            // 
            userInfo.MilitaryRankId = theMilitaryRankModel.MilitaryRankId;
            userInfo.MilitaryStatusTypeId = theMilitarySatatusModel.MilitaryStatusTypeId;
            //
            //
            // Set the Modified date & times if there were changes
            // to the object
            //
            Helper.UpdateModifiedFields(this.UnitOfWork, userInfo, userId);
        }
        #endregion
        #region PasswordSecurity information
        /// <summary>
        /// Update the security questions and/or password information
        /// </summary>
        /// <param name="model">The user manage password model </param>
        /// <param name="userId">The identifier of the user making the change</param>
        /// <returns>True if the password was updated, false otherwise</returns>
        internal bool Update(User userToUpdate, IUserManagePasswordModel model, int userId)
        {
            bool result = false;

            // do we have a new password
            if (Helper.HasData(model.NewPassword))
            {
                userToUpdate.CreatePassword(model.NewPassword);
                UnitOfWork.UserRepository.Update(userToUpdate);
                result = true;
            }

            return result;
        }
        /// <summary>
        /// Update the security questions and/or password information
        /// </summary>
        /// <param name="model">The user manage password model </param>
        /// <param name="userId">The identifier of the user making the change</param>
        /// <returns>True if the password was updated, false otherwise</returns>
        internal bool Update(User userToUpdate, IEnumerable<UserSecurityQuestionAnswerModel> model, int userId)
        {
            bool result = false;
            // any updates to the security questions
            int order = 0;
            foreach (UserSecurityQuestionAnswerModel quesAns in model)
            {
                order++;
                quesAns.AnswerText = quesAns.AnswerText.Replace("*", String.Empty).ToUpper();
                // Did the user supply an answer
                if (Helper.HasData(quesAns.AnswerText))
                {
                    // is it a new or changed answer
                    if (Helper.IsAdd(quesAns.UserAccountRecoveryId))
                    {
                        // create record and add
                        UserAccountRecovery newRecovery = new UserAccountRecovery();

                        newRecovery.Populate(quesAns.RecoveryQuestionId, quesAns.AnswerText, order, userId);

                        UnitOfWork.UserAccountRecoveryRepository.Add(newRecovery);

                        userToUpdate.UserAccountRecoveries.Add(newRecovery);
                    }
                    else
                    {
                        // modify record and update
                        UserAccountRecovery existingQuesAns = userToUpdate.UserAccountRecoveries.Where(x => x.UserId == userId && x.UserAccountRecoveryId == quesAns.UserAccountRecoveryId).FirstOrDefault();

                        existingQuesAns.Update(quesAns.RecoveryQuestionId, quesAns.AnswerText, order, userId);

                        UnitOfWork.UserAccountRecoveryRepository.Update(existingQuesAns);
                    }
                    result = true;
                }
            }

            return result;
        }
        #endregion
        #endregion
        #region Verify methods
        /// <summary>
        /// Verify the user profile data that resides in the UserInfo table
        /// </summary>
        /// <param name="theUser"></param>
        /// <param name="theMilitaryServiceId">Military Service identifier</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns>Collection of SaveProfileStatus enum values or empty collection (signifies no errors)</returns>
        /// <remarks>need unit test</remarks>
        internal ICollection<SaveProfileStatus> VerifyUserMilitaryRankAndStatus(UserInfo userInfo, int? theMilitaryServiceId, int userId)
        {
            //
            // Validate the updated entity models
            //
            userInfo.IsMilitaryRankAndStatusValid();
            List<SaveProfileStatus> onlyServerSupplied = IsServiceOnlySupplied(userInfo, theMilitaryServiceId);
            //
            // And collect there errors
            //
            List<SaveProfileStatus> results = new List<SaveProfileStatus>();
            results.AddRange(userInfo.Errors);
            results.AddRange(onlyServerSupplied);

            return results;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theMilitaryServiceId"></param>
        /// <returns></returns>
        /// <remarks>need unit test</remarks>
        internal List<SaveProfileStatus> IsServiceOnlySupplied(UserInfo userInfo, int? theMilitaryServiceId)
        {
            List<SaveProfileStatus> result = new List<SaveProfileStatus>(1);
            if ((theMilitaryServiceId != null && theMilitaryServiceId.Value > 0) && (userInfo.MilitaryRankId == null && userInfo.MilitaryRankId > 0))
            {
                result.Add(SaveProfileStatus.IncompleteMilitaryIndex);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="addresses"></param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns></returns>
        internal ICollection<SaveProfileStatus> VerifyAddress(UserInfo userInfo, IList<AddressInfoModel> addresses, int userId)
        {
            List<SaveProfileStatus> results = new List<SaveProfileStatus>();
            //
            // There can be only 1 personal address
            //
            if (userInfo.UserAddresses.Count(x => x.AddressTypeId == AddressType.Indexes.Personal) > UserAddress.Limits.MaximumPersonalAddresses)
            {
                results.Add(SaveProfileStatus.TooManyPersonalAddresses);
            }
            //
            // There must be at least 1 address for a reviewer or prospect
            //
            if (userInfo.UserProfiles.FirstOrDefault() != null && userInfo.UserAddresses.Count() < UserAddress.Limits.MinimumAddressesForReviewer &&
                (userInfo.UserProfiles.FirstOrDefault().ProfileTypeId == ProfileType.Indexes.Reviewer))
            {
                results.Add(SaveProfileStatus.OneAddressIsRequired);
            }


            return results;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="addresses"></param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <returns></returns>
        //internal ICollection<SaveSecurityQuestionStatus> VerifySecurityQuestions(User user) //, IList<AddressInfoModel> addresses, int userId)
        //{
        //    List<SaveSecurityQuestionStatus> results = new List<SaveSecurityQuestionStatus>();
        //    //
        //    // There can be only be 3 security questions
        //    //
        //    if (user.UserAccountRecoveries.Count() != UserAccountRecovery.Limits.RequiredNumberSecurityQuestions ||
        //        user.UserAccountRecoveries.Select(x => x.RecoveryQuestionId).Distinct().Count() != UserAccountRecovery.Limits.RequiredNumberSecurityQuestions)
        //    {
        //        results.Add(SaveSecurityQuestionStatus.SecurityQuestionsFailed);
        //    }

        //    return results;
        //}
        #endregion
        #region Resume Save method
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
        public ISaveResumeResults Save(Files.IFileService service, Stream stream, string uploadedFileName, int userInfoId, int resumeid, int userId)
        {
            ValidateSaveParameters(stream, userInfoId, userId);

            ISaveResumeResults results = new SaveResumeResults();
            UserInfo userInfoEntity = UnitOfWork.UserInfoRepository.GetByID(userInfoId);
            UserResume userResumeEntity = UnitOfWork.UserResumeRepository.GetByID(resumeid);

            int prefiousVersionNumber = (Helper.IsAdd(resumeid)) ? NoResumeVersionNumber : userResumeEntity.Version;
            string fileName = string.Empty;

            if (!IsValid(stream, uploadedFileName, results.Status))
            {
                //
                // Since the stream does not contain a valid resume for one reason or another, do not need to do anything
                //
            }
            else
            {
                if (Helper.IsAdd(resumeid))
                {
                    fileName = Files.FileService.ResumeName(userInfoEntity.FirstName, userInfoEntity.LastName, UserResume.Constants.ResumeStartingVersion, UserResume.Constants.ResumeExtension);
                    Add(userInfoEntity, fileName, stream, userId);
                }
                else
                {
                    fileName = Files.FileService.ResumeName(userInfoEntity.FirstName, userInfoEntity.LastName, userResumeEntity.NextVersion, UserResume.Constants.ResumeExtension);
                    Modify(userResumeEntity, fileName, stream, userId);
                }
                //
                // Get the change caused by the CV update then save
                //
                List<EntityPropertyChange> changes = UnitOfWork.GetChangedFields();
                UnitOfWork.Save();
                //
                // Then manipulate the worklist and save the changes to the UserInfoChangeLog
                //
                ManipulateWorkList(changes, userInfoEntity.User, userId);
                UnitOfWork.Save();
                results.Status.Add(SaveResumeStatus.Success);
            }

            results.ResumeModel.Populate(fileName, userInfoEntity.UserResumes.DefaultIfEmpty(UserResume.Default).FirstOrDefault().UserResumeId);

            return results;
        }
        /// <summary>
        /// Validates that a resume is an acceptable resume
        /// </summary>
        /// <param name="stream">Stream from HttpPostedFileBase</param>
        ///  <param name="fileName">Filename from HttpPostedFileBase</param>
        /// <returns>True if the stream contains a valid resume; false otherwise</returns>
        /// <remarks>needs unit testing</remarks>
        internal virtual bool IsValid(Stream stream, string fileName, IList<SaveResumeStatus> list)
        {
            //
            // Check the file size
            //
            if (!Files.FileService.IsFileSizeCorrect(stream))
            {
                list.Add(SaveResumeStatus.TooLarge);
            }
            //
            // Ensure the file is a PDF file by checking the signature & extension
            //
            if ((!Files.FileService.IsPdfFile(stream)) || (!Files.FileService.IsPdfFile(fileName)))
            {
                list.Add(SaveResumeStatus.NotPdfFile);
            }

            return (list.Count == 0);
        }
        /// <summary>
        /// Add a new UserResume Entity
        /// </summary>
        /// <param name="userInfoEntity">UserInfo entity</param>
        /// <param name="fileName">Resume file name (includes extension)</param>
        /// <param name="stream">File stream</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <remarks>needs unit testing</remarks>
        internal virtual void Add(UserInfo userInfoEntity, string fileName, Stream stream, int userId)
        {
            UserResume userResumeEntity = new UserResume();
            byte[] fileByteArray = GetFileByteArray(stream);
            userResumeEntity.Populate(fileName, UserResume.Constants.ResumeStartingVersion, fileByteArray, GlobalProperties.P2rmisDateTimeNow);

            Helper.UpdateTimeFields(userResumeEntity, userResumeEntity.UserResumeId, userId);

            UnitOfWork.UserResumeRepository.Add(userResumeEntity);
            userInfoEntity.UserResumes.Add(userResumeEntity);
        }
        /// <summary>
        /// Modify an existing UserResume Entity
        /// </summary>
        /// <param name="userInfoEntity">UserInfo entity</param>
        /// <param name="fileName">Resume file name (includes extension)</param>
        /// <param name="stream">File stream</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        /// <remarks>needs unit testing</remarks>
        internal virtual void Modify(UserResume userResumeEntity, string fileName, Stream stream, int userId)
        {
            byte[] fileByteArray = GetFileByteArray(stream);
            userResumeEntity.Populate(fileName, userResumeEntity.NextVersion, fileByteArray);

            Helper.UpdateModifiedFields(userResumeEntity, userId);
        }
        #endregion
        #region User Client Save methods
        /// <summary>
        /// Saves the user's accessible clients.
        /// </summary>
        /// <param name="collection">Collection of IUserProfileClientModel objects (target list) of clients</param>
        /// <param name="userEntity">User entity</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal virtual void SaveUserClient(ICollection<UserProfileClientModel> collection, User userEntity, int userId)
        {
            //
            // We need to do some preparation before we can Add & Delete the UserClients.
            //
            PrepareModel(collection, userEntity);
            //
            // Now go through the model collection and Add/Delete/Modify.
            //
            foreach (var item in collection)
            {
                if (item.IsAdd())
                {
                    Add(item, userId);
                }
                else if (item.IsDelete())
                {
                    Delete(item, userId);
                }
                else
                {
                    //
                    // Our other options would be Modify.  But since the entity maintains the user &
                    // client relationship, there is only Add & Delete actions.
                    //
                }
            }
        }
        /// <summary>
        /// Prepare the model collection.  Existing UserClient entities that shoudl be deleted
        /// are identified and added to the collection.
        /// </summary>
        /// <param name="collection">Collection of IUserProfileClientModel objects (target list) of clients</param>
        /// <param name="userEntity">The user entity identifier</param>
        internal virtual void PrepareModel(ICollection<UserProfileClientModel> collection, User userEntity)
        {
            //
            // What we have coming in is a collection of the clients that a user is permitted to access.  However they
            // may have been assigned assess to others which are not in this list and should be deleted.  Retrieve their
            // current list and remove any that are in the new list.  The remaining entries in the current list
            // should be deleted.
            //
            userEntity.UserClients.ToList().ForEach(x =>
            {
                //
                // Does the target collection contain the existing UserClient entity?
                // If it does not this indicates a relationship that should be deleted.
                // Create a new model that will indicate it should be deleted and 
                // add it to the collections.
                //
                if (collection.FirstOrDefault(y => y.UserClientId == x.UserClientID) == null)
                {
                    var model = new UserProfileClientModel();
                    model.PopulateDelete(x.UserClientID);
                    collection.Add(model);
                }
            });
        }
        /// <summary>
        /// Add the UserClient entity.
        /// </summary>
        /// <param name="model">IUserProfileClientModel model representing the add</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal virtual void Add(IUserProfileClientModel model, int userId)
        {
            UserClient userClientEntity = new UserClient();
            userClientEntity.Populate(model.UserId.Value, model.ClientId);

            Helper.UpdateTimeFields(userClientEntity, userClientEntity.UserClientID, userId);
            UnitOfWork.UserClientRepository.Add(userClientEntity);
        }
        /// <summary>
        /// Deletes the UserClient entity.
        /// </summary>
        /// <param name="model">IUserProfileClientModel representing the UserClient entity to deleted</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        internal virtual void Delete(IUserProfileClientModel model, int userId)
        {
            UserClient userClientEntity = UnitOfWork.UserClientRepository.GetByID(model.UserClientId);

            Helper.UpdateDeletedFields(userClientEntity, userId);
            UnitOfWork.UserClientRepository.Delete(userClientEntity);
        }
        #endregion
        #region Create User Methods
        /// <summary>
        /// Create an empty User entity.
        /// </summary>
        /// <param name="userId">User identifier of user creating the profile</param>
        /// <param name="firstName">The user's first name</param>
        /// <param name="lastName">The user's last name</param>
        /// <returns>User entity</returns>
        public User CreateUser(int userId, string firstName, string lastName)
        {
            User userEntity = new User();
            //
            // This should go to user in a new method:
            //
            userEntity.LastLockedOutDate = GlobalProperties.P2rmisDateTimeNow;
            userEntity.LastLoginDate = GlobalProperties.P2rmisDateTimeNow;
            UnitOfWork.UserRepository.Add(userEntity);
            //
            // Assign the UserLogin after it has been added to the repository so the count will not need to be incremented.
            //
            userEntity.UserLogin = ConstructUserName(firstName, lastName);

            Helper.UpdateUserCreateedFields(userEntity, userId);
            Helper.UpdateUserModifiedFields(userEntity, userId);

            return userEntity;
        }
        /// <summary>
        /// Create an empty UserInfo entity and associate it with a User entity.
        /// </summary>
        /// <param name="userEntity">User entity object</param>
        /// <param name="userId">User identifier of user creating the profile</param>
        /// <returns>UserInfo entity</returns>
        public UserInfo CreateUserInfoEntityAndAssociateWithUser(User userEntity, int userId)
        {
            UserInfo userInfoEntity = new UserInfo();
            userEntity.UserInfoes.Add(userInfoEntity);

            UnitOfWork.UserInfoRepository.Add(userInfoEntity);

            Helper.UpdateCreatedFields(userInfoEntity, userId);
            Helper.UpdateModifiedFields(userInfoEntity, userId);

            return userInfoEntity;
        }
        /// <summary>
        /// Create the initial user account status for this user
        /// </summary>
        /// <param name="user">The user entity object to create the account status for</param>
        /// <param name="profileTypeId">The profile type identifier to determine the status</param>
        internal virtual void CreateUserInitialAccountStatus(User user, int? profileTypeId, int userId)
        {
            switch (profileTypeId)
            {
                case ProfileType.Indexes.Prospect:
                case ProfileType.Indexes.Reviewer:
                case ProfileType.Indexes.SraStaff:
                case ProfileType.Indexes.Client:
                    // awaiting credentials
                    SetUserAccountStatusAwaitingCredentials(user, userId);
                    break;
                case ProfileType.Indexes.Misconduct:     // Misconduct
                    SetUserAccountStatusMisconduct(user, userId);
                    break;
                default:
                    string val = profileTypeId.HasValue ? profileTypeId.ToString() : "null";
                    string message = string.Format("{0} detected an invalid parameter: {1} was {2}", "CreateUserInitialAccountStatus", "profileTypeId", val);
                    throw new ArgumentException(message);
            }
        }
        /// <summary>
        /// Construct the user's UserName value
        /// </summary>
        /// <param name="firstName">User's first name</param>
        /// <param name="lastName">User's last name</param>
        /// <returns>User name value to use.</returns>
        internal virtual string ConstructUserName(string firstName, string lastName)
        {
            //
            // First find the number of users with the same name.  Then we can construct the user name;
            //
            int count = SameUserNamesCount(firstName, lastName) + 1;
            return User.CounstructUserName(firstName, lastName, count);
        }
        /// <summary>
        /// Returns the number of UserInfo entities that have the same name.  
        /// </summary>
        /// <param name="firstName">User first name</param>
        /// <param name="lastName">User last name</param>
        /// <returns>Number of entires with the same name</returns>
        internal virtual int SameUserNamesCount(string firstName, string lastName)
        {
            //
            // Need to prune the names down to the user name portion length. 
            //
            string firstNameTarget = firstName.Substring(0, User.CreateFirstNameLengthValue(firstName));
            string lastNameTarget = lastName.Substring(0, User.CreateLastNameLengthValue(lastName));
            //
            // Now search on those values
            //
            return UnitOfWork.UserRepository.Get(x => x.UserLogin.StartsWith(string.Concat(firstNameTarget, lastNameTarget))).Count();
        }

        #endregion
        #region Helpers
        /// <summary>
        /// Retrieves the user identified by the unique identifier.
        /// </summary>
        /// <param name="profileId">User identifier</param>
        /// <returns>User object identified by the identifier</returns>
        /// <remarks>This could be a common method</remarks>
        protected User GetUserById(int profileId)
        {
            return this.UnitOfWork.UserRepository.GetByID(profileId);
        }
        /// <summary>
        /// Retrieves the user identified by the unique identifier.
        /// </summary>
        /// <param name="profileId">UserInfo identifier</param>
        /// <returns>UserInfo object identified by the identifier</returns>
        /// <remarks>This could be a common method</remarks>
        protected UserInfo GetUserInfoById(int profileId)
        {
            return this.UnitOfWork.UserInfoRepository.GetByID(profileId);
        }
        /// <summary>
        /// Validates the data the user changed was based on the current data.
        /// </summary>
        /// <param name="dateTimeStamps">Dictionary of datetime stamps indexed by type.</param>
        /// <returns>SaveProfileStatus enum value indicating result of the comparison</returns>
        internal ICollection<SaveProfileStatus> IsDataCurrent(Dictionary<Type, DateTime> dateTimeStamps)
        {
            ICollection<SaveProfileStatus> results = new List<SaveProfileStatus>();

            return results;
        }
        /// <summary>
        /// Compute SaveSecurityQuestionStatus based on password security question updates
        /// </summary>
        /// <param name="updatedPassword">Was the password updated</param>
        /// <param name="updatedSecurityQuestions">Were the security questions updated</param>
        /// <returns></returns>
        internal SaveSecurityQuestionStatus SaveSecurityUpdateStatus(bool updatedPassword, bool updatedSecurityQuestions)
        {
            return (updatedPassword && updatedSecurityQuestions) ? SaveSecurityQuestionStatus.Success :
                                            (updatedPassword) ? SaveSecurityQuestionStatus.PasswordSuccess :
                                            (updatedSecurityQuestions) ? SaveSecurityQuestionStatus.SecurityQuestionSuccess : SaveSecurityQuestionStatus.NoActionAttempted;
        }
        /// <summary>
        /// The user's system roles are updated
        /// </summary>
        /// <param name="userToUpdate">The user to grant to and/or remove permissions</param>
        /// <param name="profileType">The profile type</param>
        /// <param name="systemRole">The system role</param>
        /// <param name="username">The user's login name</param>
        internal void UpdateRoles(User userToUpdate, int profileType, int? systemRole)
        {
            int? currentRoleId = userToUpdate.GetUserSystemRole();
            string username = userToUpdate.UserLogin;

            // set up new role
            List<int> newRole = new List<int>();
            if (profileType != ProfileType.Indexes.Misconduct)
            {
                // all profile types, except misconduct, will have a valid system role
                AddSystemRole(newRole, systemRole);
            }
        }
        /// <summary>
        /// Adds the system role to the list of roles
        /// </summary>
        /// <param name="roleList">The list of roles</param>
        /// <param name="systemRole">The system role to add to the list</param>
        internal static void AddSystemRole(List<int> roleList, int? systemRole)
        {
            if (systemRole.HasValue)
            {
                roleList.Add(systemRole.Value);
            }
        }
        /// <summary>
        /// Computes a single UserInfoChangeLog record for changes that represent a change involving more than one record or removes unwanted records.
        /// </summary>
        /// <param name="changes">The list of entity property changes</param>
        /// <returns>The list of the single record changes plus any created composite change records minus the input records used to compute the addedcomposite change record</returns>
        internal IEnumerable<EntityPropertyChange> FilterUserInfoChangeLog(IEnumerable<EntityPropertyChange> changes)
        {
            List<EntityPropertyChange> result = changes.ToList();
            // email change records either do not apply or they will be reduced to a single record and that record added below
            result = result.Where(x => x.EntityTableName != UserEmailChangeLogRules.UserEmail).ToList();

            EntityPropertyChange email = FilterUserEmails(changes);

            if (email != null)
            {
                result.Add(email);
            }

            result = UserW9ChangeLogRules.Filter(result);

            result = result.Where(x => x.EntityTableName != UserDegreeMajorChangeLogRules.UserDegree).ToList();

            List<EntityPropertyChange> degrees = UserDegreeMajorChangeLogRules.ComputeMultipleUserInfoChangeRecord(changes);
            if (degrees.Count > 0)
            {
                result.AddRange(degrees);
            }
            return result;
        }
        /// <summary>
        /// A single degree/major change record is created from the degree and major change records
        /// </summary>
        /// <param name="changes">The change records containing the field values in the change user email entity object</param>
        /// <returns>The user degrees EnityPropertyChange object</returns>
        internal EntityPropertyChange FilterUserEmails(IEnumerable<EntityPropertyChange> changes)
        {
            List<EntityPropertyChange> emailRecords = changes.Where(x => x.EntityTableName == UserEmailChangeLogRules.UserEmail).OrderBy(x => x.EntityId).ToList();

            return UserEmailChangeLogRules.ComputeUserInfoChangeRecord(emailRecords);
        }
        /// <summary>
        /// Verifies the parameters for SaveProfile.  Validation is slightly different for the collections
        /// since each individual save validates the data elements.
        /// </summary>
        /// <param name="profileId">User identifier of the profile being updated</param>
        /// <param name="dateTimeStamps">Dictionary of datetime stamps indexed by type to determine if data is stale</param>
        /// <param name="theGeneralInfoModel">General information model</param>
        /// <param name="theWebsiteModels">Website information model</param>
        /// <param name="theEmailAddressModels">Email addresses information model</param>
        /// <param name="theInstitutionAddresses"> information model</param>
        /// <param name="thePersonalAddresses">Personal information model</param>
        /// <param name="w9Addresses">W9 information model</param>
        /// <param name="thePhoneTypeDropdowns">Phone type information model</param>
        /// <param name="theAlternateContactTypeDropdowns">Alternate contact information model</param>
        /// <param name="thePhoneNumberModels">Phone numbers information model</param>
        /// <param name="theUserDegreeModels">User degrees information model</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        private void VerifySaveProfileParameters(int profileId,
                                Dictionary<Type, DateTime> dateTimeStamps,
                                bool W9Verify,
                                IGeneralInfoModel theGeneralInfoModel,
                                IList<WebsiteModel> theWebsiteModels,
                                IList<AddressInfoModel> theAddress,
                                IProfessionalAffiliationModel theProfessionalAffiliation,
                                IW9AddressModel w9Addresses,
                                ICollection<IListEntry> thePhoneTypeDropdowns,
                                ICollection<IListEntry> theAlternateContactTypeDropdowns,
                                ICollection<PhoneNumberModel> thePhoneNumberModels,
                                IList<UserDegreeModel> theUserDegreeModels,
                                IUserMilitaryRankModel theMilitaryRankModel,
                                IUserMilitaryStatusModel theMilitarySatatusModel,
                                IList<UserProfileClientModel> theUserProfileClientModels,
                                int userId
                               )
        {
            // meed validation on dateTimeStamps dictionary
            ValidateInt(profileId, "UserProfileManagementService.SaveProfile", "profileId");

            // meed validation on dateTimeStamps dictionary

            this.ValidateModelExists<IGeneralInfoModel>(theGeneralInfoModel, "UserProfileManagementService.SaveProfile", "theGeneralInfoModel");
            this.ValidateCollectionExists<WebsiteModel>(theWebsiteModels, "UserProfileManagementService.SaveProfile", "theWebsiteModels");
            this.ValidateCollectionExists<AddressInfoModel>(theAddress, "UserProfileManagementService.SaveProfile", "theAddress");
            this.ValidateModelExists<IProfessionalAffiliationModel>(theProfessionalAffiliation, "UserProfileManagementService.SaveProfile", "theProfessionalAffiliation");
            this.ValidateModelExists<IW9AddressModel>(w9Addresses, "UserProfileManagementService.SaveProfile", "W9Addresses");
            this.ValidateCollectionExists<IListEntry>(thePhoneTypeDropdowns, "UserProfileManagementService.SaveProfile", "thePhoneTypeDropdowns");
            this.ValidateCollectionExists<IListEntry>(theAlternateContactTypeDropdowns, "UserProfileManagementService.SaveProfile", "theAlternateContactTypeDropdowns");
            this.ValidateCollectionExists<PhoneNumberModel>(thePhoneNumberModels, "UserProfileManagementService.SaveProfile", "thePhoneNumberModels");
            this.ValidateCollectionExists<UserDegreeModel>(theUserDegreeModels, "UserProfileManagementService.SaveProfile", "theUserDegreeModels");
            this.ValidateModelExists<IUserMilitaryRankModel>(theMilitaryRankModel, "UserProfileManagementService.SaveProfile", "theMilitaryRankModel");
            this.ValidateModelExists<IUserMilitaryStatusModel>(theMilitarySatatusModel, "UserProfileManagementService.SaveProfile", "theMilitarySatatusModel");
            this.ValidateCollectionExists<UserProfileClientModel>(theUserProfileClientModels, "UserProfileManagementService.SaveProfile", "theUserProfileClientModels");
            ValidateInt(userId, "UserProfileManagementService.SaveProfile", "userId");
        }
        /// <summary>
        /// Verifies the parameters for CreateProfile.  Validation is slightly different for the collections
        /// </summary>
        /// <param name="theGeneralInfoModel">General information model</param>
        /// <param name="theWebsiteModels">Website information model</param>
        /// <param name="theInstitutionAddresses"> information model</param>
        /// <param name="thePersonalAddresses">Personal information model</param>
        /// <param name="w9Addresses">W9 information model</param>
        /// <param name="thePhoneTypeDropdowns">Phone type information model</param>
        /// <param name="theAlternateContactTypeDropdowns">Alternate contact information model</param>
        /// <param name="thePhoneNumberModels">Phone numbers information model</param>
        /// <param name="theUserDegreeModels">User degrees information model</param>
        /// <param name="theMilitaryRankModel">User military rank models</param>
        /// <param name="theMilitarySatatusModel">User status model</param>
        /// <param name="theUserProfileClientModels">UserClient models</param>
        /// <param name="userId">User identifier of user making the modifications</param>
        private void VerifyCreateProfileParmaeters(
                                IGeneralInfoModel theGeneralInfoModel,
                                IList<WebsiteModel> theWebsiteModels,
                                IList<AddressInfoModel> theAddresses,
                                IW9AddressModel w9Addresses,
                                ICollection<IListEntry> thePhoneTypeDropdowns,
                                ICollection<IListEntry> theAlternateContactTypeDropdowns,
                                ICollection<PhoneNumberModel> thePhoneNumberModels,
                                IList<UserDegreeModel> theUserDegreeModels,
                                IUserMilitaryRankModel theMilitaryRankModel,
                                IUserMilitaryStatusModel theMilitarySatatusModel,
                                IList<UserProfileClientModel> theUserProfileClientModels,
                                int userId
                               )
        {
            this.ValidateModelExists<IGeneralInfoModel>(theGeneralInfoModel, "UserProfileManagementService.CreateProfile", "theGeneralInfoModel");
            this.ValidateCollectionExists<WebsiteModel>(theWebsiteModels, "UserProfileManagementService.CreateProfile", "theWebsiteModels");
            this.ValidateCollectionExists<AddressInfoModel>(theAddresses, "UserProfileManagementService.CreateProfile", "theAddresses");
            this.ValidateModelExists<IW9AddressModel>(w9Addresses, "UserProfileManagementService.CreateProfile", "w9Addresses");
            this.ValidateCollectionExists<IListEntry>(thePhoneTypeDropdowns, "UserProfileManagementService.CreateProfile", "thePhoneTypeDropdowns");
            this.ValidateCollectionExists<IListEntry>(theAlternateContactTypeDropdowns, "UserProfileManagementService.CreateProfile", "theAlternateContactTypeDropdowns");
            this.ValidateCollectionExists<PhoneNumberModel>(thePhoneNumberModels, "UserProfileManagementService.CreateProfile", "thePhoneNumberModels");
            this.ValidateCollectionExists<UserDegreeModel>(theUserDegreeModels, "UserProfileManagementService.CreateProfile", "theUserDegreeModels");
            this.ValidateModelExists<IUserMilitaryRankModel>(theMilitaryRankModel, "UserProfileManagementService.CreateProfile", "theMilitaryRankModel");
            this.ValidateModelExists<IUserMilitaryStatusModel>(theMilitarySatatusModel, "UserProfileManagementService.CreateProfile", "theMilitarySatatusModel");
            this.ValidateCollectionExists<UserProfileClientModel>(theUserProfileClientModels, "UserProfileManagementService.CreateProfile", "theUserProfileClientModels");
            ValidateInt(userId, "UserProfileManagementService.SaveProfile", "userId");
        }
        /// <summary>
        /// Validate the parameters to Save (resume)
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="userInfoId"></param>
        /// <param name="resumeid"></param>
        /// <param name="userId">User identifier of user making the modifications</param>
        private void ValidateSaveParameters(Stream stream, int userInfoId, int userId)
        {
            ValidateInt(userInfoId, "UserProfileManagementService.Save[Resume]", "userInfoId");
            ValidateInt(userId, "UserProfileManagementService.Save[Resume]", "userId");
        }
        /// <summary>
        /// Gets the file byte array.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>File in byte array</returns>
        private byte[] GetFileByteArray(Stream stream)
        {
            byte[] data;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(memoryStream);
                data = memoryStream.ToArray();
            }
            return data;
        }
        #endregion
    }
}
