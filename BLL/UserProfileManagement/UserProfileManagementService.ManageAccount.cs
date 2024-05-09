using System.Linq;
using System.Web.Security;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Entity = Sra.P2rmis.Dal;
using System;
using System.Threading;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.UserProfileManagement
{
    /// <summary>
    /// UserProfileManagementService provides services to perform business related functions for
    /// the User Profile Management Application. This file contains services for managing the
    /// user's account .
    /// </summary>
    public partial class UserProfileManagementService
    {
        /// <summary>
        /// The interval of multiple emails in milliseonds
        /// </summary>
        public const int IntervalOfMultipleEmails = 5000;
        /// <summary>
        /// Update a password & send out the necessary emails when the user is authenticated by security questions.
        /// </summary>
        /// <param name="theMailService">The MailService</param>
        /// <param name="userEntityId">User entity identifier</param>
        public void UpdatePasswordWhenSecurityQuestionsUsed(IMailService theMailService, int userEntityId)
        {
            ValidateInt(userEntityId, FullName(nameof(UserProfileManagementService), nameof(UpdatePasswordWhenSecurityQuestionsUsed)), nameof(userEntityId));
            //
            // Because the actions are spread across two services, there is an implied order
            // of execution of the methods.  The framework is updated in the MailService (ResendCredentials)
            // so any changes need to be done prior.
            //
            Entity.User userEntity = UnitOfWork.UserRepository.GetByID(userEntityId);
            AddNewUserPassword(userEntity);
            userEntity.ModifyWhenAuthenticatedBySecurityQuestions();
            SetUserAccountStatusTemporaryPassword(userEntity, userEntity.UserID);
            ResendCredentials(theMailService, userEntity);
        }
        /// <summary>
        /// Set the user account status to temporary credentials
        /// </summary>
        /// <param name="targetUserId">The user whose status is being set to awaiting credentials</param>
        /// <param name="userId">The identifier of the user making the change</param>
        public void SetUserAccountStatusTemporaryPassword(Entity.User targetUser, int userId)
        {
            this.ValidateInteger(userId, FullName(nameof(UserProfileManagementService), nameof(SetUserAccountStatusTemporaryPassword)), nameof(userId));

            CreateOrUpdateUserAccountStatus(Entity.AccountStatusReason.Indexes.TmpPwd, targetUser, userId);
        }
        /// <summary>
        /// SetPasswordExpired sets the user account status to password expired
        /// </summary>
        /// <param name="targetUserId">The user identification to be set to expired</param>
        /// <param name="userId">The user setting expired</param>
        public void SetPasswordExpired(int targetUserId, int userId)
        {

            this.ValidateInteger(targetUserId, FullName(nameof(UserProfileManagementService), nameof(SetPasswordExpired)), nameof(targetUserId));
            this.ValidateInteger(userId, FullName(nameof(UserProfileManagementService), nameof(SetPasswordExpired)), nameof(userId));

            Entity.User userEntity = UnitOfWork.UserRepository.GetByID(targetUserId);
            Entity.UserAccountStatu userAccountStatus = userEntity.UsersCurrentAccountStatus();

            userAccountStatus.UpdateUserAccountStatus(Entity.AccountStatusReason.Indexes.PwdExpired, targetUserId, userId);
            UnitOfWork.UserAccountStatusRepository.Update(userAccountStatus);

            UnitOfWork.Save();
        }
        /// <summary>
        /// Create or update user account status 
        /// </summary>
        /// <param name="reason">The status reason identifier</param>
        /// <param name="targetUser">The user to apply the status to</param>
        /// <param name="userId">The user making status change</param>
        internal void CreateOrUpdateUserAccountStatus(int reason, Entity.User targetUser, int userId)
        {
            Entity.UserAccountStatu userStatus = (targetUser.UserAccountStatus == null || targetUser.UserAccountStatus.Count == 0) ? new Entity.UserAccountStatu() : targetUser.UserAccountStatus.FirstOrDefault();

            if (targetUser.UserAccountStatus == null || targetUser.UserAccountStatus.Count() == 0)
            {
                userStatus.CreateUserAccountStatus(reason, targetUser.UserID, userId);
                UnitOfWork.UserAccountStatusRepository.Add(userStatus);
                targetUser.UserAccountStatus.Add(userStatus);
            }
            else
            {
                userStatus.UpdateUserAccountStatus(reason, targetUser.UserID, userId);
                UnitOfWork.UserAccountStatusRepository.Update(userStatus);
            }
        }
        /// <summary>
        /// Service method to send the user new credentials
        /// </summary>
        /// <param name="theMailService">The MailService</param>
        /// <param name="targetUser">Targeted user</param>
        /// <param name="userId">User sending the credentials</param>
        /// <returns>Mail status</returns>
        public MailService.MailStatus SendNewCredentials(IMailService theMailService, int targetUser, int userId)
        {
            Entity.User userEntity = UnitOfWork.UserRepository.GetByID(targetUser);
            return SendCredentials(Entity.SystemTemplate.Indexes.SYSTEM_TEMPLATE_NEW_USER, Entity.SystemTemplate.Indexes.SYSTEM_TEMPLATE_NEW_USER_PW, theMailService, userEntity, userId);
        }
        /// <summary>
        /// Service method to send the existing new credentials
        /// </summary>
        /// <param name="theMailService">The MailService</param>
        /// <param name="targetUser">Targeted user</param>
        /// <param name="userId">User sending the credentials</param>
        /// <returns>Mail status</returns>
        public MailService.MailStatus ResendCredentials(IMailService theMailService, int targetUser, int userId)
        {
            Entity.User userEntity = UnitOfWork.UserRepository.GetByID(targetUser);
            return SendCredentials(Entity.SystemTemplate.Indexes.SYSTEM_TEMPLATE_RESET_USER_ACCOUNT, Entity.SystemTemplate.Indexes.SYSTEM_TEMPLATE_RESET_USER_ACCOUNT_PW, theMailService, userEntity, userId);
        }
        /// <summary>
        /// Service method to resend an existing user new credentials.
        /// </summary>
        /// <param name="theMailService">The MailService</param>
        /// <param name="userEntity">User (sending the email) entity</param>
        /// <returns>Mail status</returns>
        public MailService.MailStatus ResendCredentials(IMailService theMailService, Entity.User userEntity)
        {
            return SendCredentials(Entity.SystemTemplate.Indexes.SYSTEM_TEMPLATE_RESET_USER_ACCOUNT, Entity.SystemTemplate.Indexes.SYSTEM_TEMPLATE_RESET_USER_ACCOUNT_PW, theMailService, userEntity, userEntity.UserID);
        }
        /// <summary>
        /// Service method sends out credential emails (password change email & password email).
        /// </summary>
        /// <param name="firstEmailTemplateId">Template id for the first email</param>
        /// <param name="secondEmailTemplateId">Template id for the second email</param>
        /// <param name="theMailService">The mail service</param>
        /// <param name="userEntity">Targeted user entity</param>
        /// <param name="userId">User sending the credentials</param>
        /// <returns>Mail status</returns>
        internal MailService.MailStatus SendCredentials(string firstEmailTemplateId, string secondEmailTemplateId, IMailService theMailService, Entity.User userEntity, int userId)
        {
            //capture password history
            AddNewUserPassword(userEntity);
            //
            // Create the user's password & hash
            //

            var membershipRepository = new MembershipRepository();
            var initialPassword = membershipRepository.GenerateTemporaryPassword();
            userEntity.CreatePassword(initialPassword);
            //
            // And then we send out the email.  If the email is sent, we update the database with the appropriate status.
            //
            MailService.MailStatus result = theMailService.SendCredential(firstEmailTemplateId, userEntity);

            Thread.Sleep(IntervalOfMultipleEmails);

            MailService.MailStatus result2 = theMailService.SendCredential(secondEmailTemplateId, initialPassword, userEntity);

            if (result == MailService.MailStatus.Success && result2 == MailService.MailStatus.Success)
            {
                // password has been created and stored in user entity object -- update user account status accordingly
                Entity.UserAccountStatu userAccountStatus = userEntity.UsersCurrentAccountStatus();
                userAccountStatus.TempPassword(userId);
                UnitOfWork.UserAccountStatusRepository.Update(userAccountStatus);

                userEntity.CredentialsWereSent(userId);
                Entity.Helper.UpdateUserModifiedFields(userEntity, userId);
                UnitOfWork.UserRepository.Update(userEntity);

                UnitOfWork.Save();
            }

            return result & result2;
        }
        /// <summary>
        /// Service method to determine if the user can be sent credentials
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>returns true if credentials can be sent, false otherwise</returns>
        public bool IsSendCredentialsEnabled(int userId)
        {
            Entity.User entity = UnitOfWork.UserRepository.GetByID(userId);

            Entity.UserAccountStatu status = entity.UserAccountStatus.FirstOrDefault();

            bool result = status != null && (status.AccountStatusId == Entity.AccountStatu.Indexes.Active || status.AccountStatusReasonId == Entity.AccountStatusReason.Indexes.AccountClosed || status.AccountStatusReasonId == Entity.AccountStatusReason.Indexes.AwaitingCredentials ||
                                             status.AccountStatusReasonId == Entity.AccountStatusReason.Indexes.Inactivity || status.AccountStatusReasonId == Entity.AccountStatusReason.Indexes.PwdExpired || status.AccountStatusReasonId == Entity.AccountStatusReason.Indexes.Locked);

            return result;
        }
        /// <summary>
        /// Service method to determine if credentials can be sent, 
        /// when the user is initially created, for the indicated profile type 
        /// </summary>
        /// <param name="profileTypeId">The profile type identifier</param>
        /// <returns>returns true if credentials can be sent for this profile type, false otherwise</returns>
        public bool IsSendCredentialsEnabledAtUserCreation(int profileTypeId)
        {
            return Entity.ProfileType.IsSendCredentialsEnabled(profileTypeId);
        }
        /// <summary>
        /// Retrieves information about who sent the credentials & constructs a container to return
        /// the information
        /// </summary>
        /// <param name="userId">User entity identifier of user who sent the information</param>
        /// <returns>IUserManageAccountModel populated with only credential information</returns>
        public IUserManageAccountModel WhoSentCredentials(int userId)
        {
            IUserManageAccountModel model = new UserManageAccountModel();

            Entity.User userEntity = UnitOfWork.UserRepository.GetByID(userId);
            Entity.User userWhoSentCredentials = UnitOfWork.UserRepository.GetByID(userEntity.CredentialSentBy);
            Entity.AccountStatusReason accountStatusReasonEntity = userEntity.AccountStatus();

            model.PopulateSendCredentials(userEntity.CredentialSentDate, userWhoSentCredentials.FirstName(), userWhoSentCredentials.LastName(), userEntity.LockedStatus(), accountStatusReasonEntity.AccountStatu.AccountStatusName, accountStatusReasonEntity.AccountStatu.AccountStatusId, accountStatusReasonEntity.AccountStatusReasonName, accountStatusReasonEntity.AccountStatusReasonId, userEntity.AccountStatusDate());

            return model;
        }
        /// <summary>
        /// Deactivate a user's account.
        /// </summary>
        /// <param name="targetUserId">User entity identify of user being deactivate</param>
        /// <param name="accountStatusReasonId">AccountStatusReason entity identifier of reason why account is being deactivated</param>
        /// <param name="userId">User entity identifier of user who deactivated the account</param>
        /// <returns>Model containing information for screen refresh</returns>
        public IReactivateDeactivateResult DeActivate(int targetUserId, int accountStatusReasonId, int userId)
        {
            ValidateDeActivateParameters(targetUserId, accountStatusReasonId, userId);

            Entity.User userEntity = UnitOfWork.UserRepository.GetByID(targetUserId);

            Entity.UserAccountStatu UserAccountStatuEntity = userEntity.Deactivate(accountStatusReasonId, userId);

            UnitOfWork.UserAccountStatusRepository.Update(UserAccountStatuEntity);

            if (accountStatusReasonId == Entity.AccountStatusReason.Indexes.Ineligible)
            {
                Entity.UserProfile UserProfileEntity = userEntity.Misconduct(userId);
                UnitOfWork.UserProfileRepository.Update(UserProfileEntity);
            }
            UnitOfWork.Save();

            IReactivateDeactivateResult result = new ReactivateDeactivateResult();
            result.Populate(GetuUserName(userId), userEntity.ReadableAccountStatus(), userEntity.ReadableAccountStatusReason(), userEntity.AccountStatus().AccountStatusId, userEntity.AccountStatus().AccountStatusReasonId, userEntity.UserProfileTypeId(), userEntity.AccountStatusDate());

            return result;
        }
        /// <summary>
        /// Activate a user's account.
        /// </summary>
        /// <param name="targetUserId">User entity identify of user being activated</param>
        /// <param name="accountStatusReasonId">AccountStatusReason entity identifier of reason why account is being activated</param>
        /// <param name="userId">User entity identifier of user who activated the account</param>
        /// <returns>Model containing information for screen refresh</returns>
        public IReactivateDeactivateResult Reactivate(int targetUserId, int userId)
        {
            ValidateActivateParameters(targetUserId, userId);

            Entity.User userEntity = UnitOfWork.UserRepository.GetByID(targetUserId);

            Entity.UserAccountStatu UserAccountStatuEntity = userEntity.Activate(userId);
            UnitOfWork.UserAccountStatusRepository.Update(UserAccountStatuEntity);
            UnitOfWork.Save();

            IReactivateDeactivateResult result = new ReactivateDeactivateResult();
            result.Populate(GetuUserName(userId), userEntity.ReadableAccountStatus(), userEntity.ReadableAccountStatusReason(), userEntity.AccountStatus().AccountStatusId, userEntity.AccountStatus().AccountStatusReasonId, userEntity.UserProfileTypeId(), userEntity.AccountStatusDate());

            return result;
        }
        /// <summary>
        /// Gets the individual vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        public UserVendorModel GetVendorId(int userInfoId, bool indVendorId)
        {
            var model = default(UserVendorModel);
            if (indVendorId)
            {
                Validate("GetIndividualVendor", userInfoId, nameof(userInfoId));

                var vendor = UnitOfWork.UserRepository.GetUserVendor(userInfoId, true);
                if (vendor != null)
                    model = new UserVendorModel(vendor.VendorId, vendor.VendorName, vendor.ActiveFlag);
            }
            else
            {
                Validate("GetInstitutionalVendor", userInfoId, nameof(userInfoId));

                var vendor = UnitOfWork.UserRepository.GetUserVendor(userInfoId, false);
                if (vendor != null)
                    model = new UserVendorModel(vendor.VendorId, vendor.VendorName, vendor.ActiveFlag);
            }

            return model;
        }
        /// <summary>
        /// Saves the individual user vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="vendorName">Name of the vendor.</param>
        /// <param name="userId">The user identifier.</param>
        public UserVendorModel SaveUserVendorId(int userInfoId, string vendorId, string vendorName, int userId, bool isIndividual)
        {
            return SaveUserVendor(userInfoId, vendorId, vendorName, userId, isIndividual, true);
        }
        /// <summary>
        /// Saves the user vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="vendorName">Name of the vendor.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isIndividual">if set to <c>true</c> [is individual].</param>
        /// <param name="saveThis">if set to <c>true</c> [save this].</param>
        /// <returns></returns>
        public UserVendorModel SaveUserVendor(int userInfoId, string vendorId, string vendorName, int userId, bool isIndividual, bool saveThis)
        {
            var model = default(UserVendorModel);
            Validate("SaveIndividualUserVendor", userInfoId, nameof(userInfoId), vendorId, nameof(vendorId));

            var vendor = UnitOfWork.UserRepository.GetUserVendor(userInfoId, isIndividual);
            vendorName = vendorName ?? "N/A";
            vendorId = vendorId ?? String.Empty;

            var alternativeVendor = UnitOfWork.UserRepository.GetUserVendor(userInfoId, !isIndividual);
            // Handles scenario that both values are same
            if (alternativeVendor == null || String.IsNullOrEmpty(vendorId) || vendorId != alternativeVendor.VendorId)
            {
                if (vendor != null)
                {
                    if (vendorId != vendor.VendorId)
                    {                     
                        // Handles scenario when swapping vendorId values
                        if (alternativeVendor == null || vendor.VendorId != alternativeVendor.VendorId)
                        {
                            // Unassigned old one
                            UnitOfWork.UserRepository.UnassignVendorIdAssigned(vendor.VendorId, userId);
                        }
                        if (!String.IsNullOrEmpty(vendorId))
                        {
                            // Assign new one
                            UnitOfWork.UserRepository.SetVendorIdAssigned(vendorId, userId);
                        }
                    }
                    // Update to userVendor
                    UnitOfWork.UserRepository.UpdateUserVendor(vendor, vendorId, vendorName, userId);
                    model = new UserVendorModel(vendor.VendorId, vendor.VendorName, vendor.ActiveFlag);
                }
                else
                {
                    if (!String.IsNullOrEmpty(vendorId))
                    {
                        // Assign new one
                        UnitOfWork.UserRepository.SetVendorIdAssigned(vendorId, userId);
                    }
                    // Add to userVendor
                    if (isIndividual)
                        UnitOfWork.UserRepository.AddIndividualUserVendor(userInfoId, vendorId, vendorName, userId);
                    else
                        UnitOfWork.UserRepository.AddInstitutionalUserVendor(userInfoId, vendorId, vendorName, userId);
                    model = new UserVendorModel(vendorId, vendorName, true);
                }
            }

            if (saveThis)
                UnitOfWork.Save();

            return model;
        }
        #region Helpers
        /// <summary>
        /// Validate the parameters for Deactivate
        /// </summary>
        /// <param name="targetUserId">User entity identify of user being deactivate</param>
        /// <param name="accountStatusReasonId">AccountStatusReason entity identifier of reason why account is being deactivated</param>
        /// <param name="userId">User entity identifier of user who deactivated the account/param>
        private void ValidateDeActivateParameters(int targetUserId, int accountStatusReasonId, int userId)
        {
            ValidateInt(targetUserId, "UserProfileManagementService.Deactivate", "targetUserId");
            ValidateInt(accountStatusReasonId, "UserProfileManagementService.Deactivate", "accountStatusReasonId");
            ValidateInt(userId, "UserProfileManagementService.Deactivate", "userId");
        }
        /// <summary>
        /// Validate the parameters for Activate.
        /// </summary>
        /// <param name="targetUserId">User entity identify of user being deactivate</param>
        /// <param name="userId">User entity identifier of user who deactivated the account/param>
        private void ValidateActivateParameters(int targetUserId, int userId)
        {
            ValidateInt(targetUserId, "UserProfileManagementService.Activate", "targetUserId");
            ValidateInt(userId, "UserProfileManagementService.Activate", "userId");
        }
        #endregion
    }
}
