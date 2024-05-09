using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Dal.Repository.UserProfileManagement
{
    internal partial class RepositoryHelpers
    {
        /// <summary>
        /// Exact match find user by user identifier
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>Enumerable list of FoundUserModels</returns>
        internal static IEnumerable<IFoundUserModel> FindUserByUserId(P2RMISNETEntities context, int userID)
        {
            var result = from info in context.UserInfoes
                         join address in context.UserAddresses on new { info.UserInfoID, PrimaryFlag = true } equals
                             new { address.UserInfoID, address.PrimaryFlag }
                            into addr from address in addr.DefaultIfEmpty()
                         join email in
                             (from ue in context.UserEmails where ue.PrimaryFlag == true select ue) on info.UserInfoID equals email.UserInfoID into eml
                         from email in eml.DefaultIfEmpty()
                         join email2 in
                            (from ue in context.UserEmails where ue.PrimaryFlag == false select ue) on info.UserInfoID equals email2.UserInfoID into eml2
                         from email2 in eml2.DefaultIfEmpty()
                         where info.UserID == userID

                         select new FoundUserModel
                         {
                             UserId = info.UserID,
                             UserInfoId = info.UserInfoID,
                             CreateDate = info.User.CreatedDate,
                             Status = info.User.UserAccountStatus.FirstOrDefault().AccountStatu.AccountStatusName,
                             StatusReason = info.User.UserAccountStatus.FirstOrDefault().AccountStatusReason.AccountStatusReasonName,

                             LastName = info.LastName,
                             FirstName = info.FirstName,
                             MI = (info.MiddleName == null) ? string.Empty : (info.MiddleName.Trim().Length == 0) ? string.Empty : info.MiddleName.Substring(0, 1),
                             Address1 = address.Address1,
                             Address2 = address.Address2,
                             Address3 = address.Address3,
                             Address4 = address.Address4,
                             City = address.City,
                             State = (address.State == null) ? string.Empty : address.State.StateAbbreviation, 
                             Country = (address.Country == null) ? string.Empty : address.Country.CountryName,
                             CountryId = address.CountryId,
                             Zip = address.Zip,

                             EmailAddress = email.Email,
                             SecondaryEmailAddress = email2.Email,
                             EmailId = email.EmailID,

                             SessionPanelId =
                             from pua in context.PanelUserAssignments
                             where info.UserID == pua.UserId
                             select pua.SessionPanelId,

                             Group =
                             from roles in context.UserSystemRoles
                             join lupRoles in context.SystemRoles on roles.SystemRoleId equals lupRoles.SystemRoleId
                             where info.UserID == roles.UserID
                             select lupRoles.SystemRoleName,

                             RelevancyRank = 1
                         };

            return result;
        }

        /// <summary>
        /// Exact match find user by vendorId
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>Enumerable list of FoundUserModels</returns>
        internal static IEnumerable<IFoundUserModel> FindUserByVendorId(P2RMISNETEntities context, string vendorID)
        {
            var result = from vendor in context.UserVendors  
                         join info in context.UserInfoes on vendor.UserInfoId equals info.UserInfoID
                         join address in context.UserAddresses on new { info.UserInfoID, PrimaryFlag = true } equals new { address.UserInfoID, address.PrimaryFlag } into addr
                         from address in addr.DefaultIfEmpty()
                         join email in
                             (from ue in context.UserEmails where ue.PrimaryFlag == true select ue) on info.UserInfoID equals email.UserInfoID into eml
                         from email in eml.DefaultIfEmpty()
                         join email2 in
                            (from ue in context.UserEmails where ue.PrimaryFlag == false select ue) on info.UserInfoID equals email2.UserInfoID into eml2
                                                 from email2 in eml2.DefaultIfEmpty()
                         where vendor.VendorId == vendorID

                         select new FoundUserModel
                         {
                             UserId = info.UserID,
                             UserInfoId = info.UserInfoID,
                             CreateDate = info.User.CreatedDate,
                             Status = info.User.UserAccountStatus.FirstOrDefault().AccountStatu.AccountStatusName,
                             StatusReason = info.User.UserAccountStatus.FirstOrDefault().AccountStatusReason.AccountStatusReasonName,

                             LastName = info.LastName,
                             FirstName = info.FirstName,
                             MI = (info.MiddleName == null) ? string.Empty : (info.MiddleName.Trim().Length == 0) ? string.Empty : info.MiddleName.Substring(0, 1),
                             Address1 = address.Address1,
                             Address2 = address.Address2,
                             Address3 = address.Address3,
                             Address4 = address.Address4,
                             City = address.City,
                             State = (address.State == null) ? string.Empty : address.State.StateAbbreviation,
                             Country = (address.Country == null) ? string.Empty : address.Country.CountryName,
                             CountryId = address.CountryId,
                             Zip = address.Zip,

                             EmailAddress = email.Email,
                             SecondaryEmailAddress = email2.Email,
                             EmailId = email.EmailID,

                             SessionPanelId =
                             from pua in context.PanelUserAssignments
                             where info.UserID == pua.UserId
                             select pua.SessionPanelId,

                             Group =
                             from roles in context.UserSystemRoles
                             join lupRoles in context.SystemRoles on roles.SystemRoleId equals lupRoles.SystemRoleId
                             where info.UserID == roles.UserID
                             select lupRoles.SystemRoleName,

                             RelevancyRank = 1
                         };

            return result;
        }

        /// <summary>
        /// Fuzzy find user by name
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>Enumerable list of FoundUserModels</returns>
        internal static IEnumerable<IFoundUserModel> FindUserByName(P2RMISNETEntities context, string firstName, string lastName)
        {
            var uspResult = (from uspUsers in context.uspGetUsersByName(firstName, lastName)
			                select new KeyValuePair<int, decimal?>(uspUsers.UserID, uspUsers.Relevancy))
                            .ToList();

            var result = GetUsers(context, uspResult);

            return result;
        }
        /// <summary>
        /// Fuzzy find user by email address
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="email">Email address</param>
        /// <returns>Enumerable list of FoundUserModels</returns>
        internal static IEnumerable<IFoundUserModel> FindUserByEmail(P2RMISNETEntities context, string email)
        {
            var uspResult = (from uspUsers in context.uspGetUsersByEmail(email)			                
			                select new KeyValuePair<int, decimal?>(uspUsers.UserID, uspUsers.Relevancy))
                            .ToList();

            var result = GetUsers(context, uspResult);

            return result;
        }
        /// <summary>
        /// Fuzzy find user by userName
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="userName">User Name</param>
        /// <returns>Enumerable list of FoundUserModels</returns>
        internal static IEnumerable<IFoundUserModel> FindUserByUsername(P2RMISNETEntities context, string userName)
        {
            var uspResult = (from uspUsers in context.uspGetUsersByUserLogin(userName)			                
			                select new KeyValuePair<int, decimal?>(uspUsers.UserID, uspUsers.Relevancy))
                            .ToList();

            var result = GetUsers(context, uspResult);

            return result;
        }
        /// <summary>
        /// Retrieves a list of participation assignments associated with a given user
        /// </summary>
        /// <param name="user">Queryable of user information</param>
        /// <param name="userInfoId">Id for a user's profile information</param>
        /// <returns>Participation history list</returns>
        internal static IEnumerable<IUserParticipationHistoryModel> GetParticipationHistory(IQueryable<User> user, int userInfoId)
        {
            var result = user.Where(x => x.UserInfoes.FirstOrDefault().UserInfoID == userInfoId)
                .SelectMany(s => s.PanelUserAssignments)
                .Select(y => new UserParticipationHistoryModel
                {
                    PanelAbrv = y.SessionPanel.PanelAbbreviation,
                    FiscalYear = y.SessionPanel.ProgramPanels.FirstOrDefault().ProgramYear.Year,
                    ClientAbrv =
                        y.SessionPanel.ProgramPanels.FirstOrDefault().ProgramYear.ClientProgram.Client.ClientAbrv,
                    ParticipantType = y.ClientParticipantType.ParticipantTypeName,
                    ParticipantRole = y.ClientRole.RoleName,
                    ProgramAbrv =
                        y.SessionPanel.ProgramPanels.FirstOrDefault().ProgramYear.ClientProgram.ProgramAbbreviation,
                    PanelEndDate = y.SessionPanel.EndDate,
                    Scope = y.ClientParticipantType.ParticipantScope,
                    ParticipationId = y.PanelUserAssignmentId,
                    NotificationSent = y.NotificationDateSent,
                    ModifiedDate = y.ModifiedDate,
                    ProgramEndDate = y.SessionPanel.ProgramPanels.FirstOrDefault().ProgramYear.DateClosed,
                    RegistrationStartDate = y.PanelUserRegistrations.FirstOrDefault().RegistrationStartDate,
                    RegistrationCompletedDate = y.PanelUserRegistrations.FirstOrDefault().RegistrationCompletedDate,
                    ContractSignedDate = y.PanelUserRegistrations.FirstOrDefault().PanelUserRegistrationDocuments.FirstOrDefault(z => z.ClientRegistrationDocument.RegistrationDocumentTypeId == RegistrationDocumentType.Indexes.ContractualAgreement).DateSigned,
                    RegistrationId = (int?)y.PanelUserRegistrations.FirstOrDefault().PanelUserRegistrationId ?? 0,
                    SessionPanelId = y.SessionPanel.SessionPanelId
                }).Concat(user.Where(x => x.UserInfoes.FirstOrDefault().UserInfoID == userInfoId)
                .SelectMany(s => s.ProgramUserAssignments)
                .Select(y => new UserParticipationHistoryModel
                {
                    PanelAbrv = String.Empty,
                    FiscalYear = y.ProgramYear.Year,
                    ClientAbrv =
                        y.ProgramYear.ClientProgram.Client.ClientAbrv,
                    ParticipantType = y.ClientParticipantType.ParticipantTypeName,
                    ParticipantRole = String.Empty,
                    ProgramAbrv =
                        y.ProgramYear.ClientProgram.ProgramAbbreviation,
                    PanelEndDate = null,
                    Scope = y.ClientParticipantType.ParticipantScope,
                    ParticipationId = y.ProgramUserAssignmentId,
                    NotificationSent = null,
                    ModifiedDate = y.ModifiedDate,
                    ProgramEndDate = y.ProgramYear.DateClosed,
                    RegistrationStartDate = null,
                    RegistrationCompletedDate = null,
                    ContractSignedDate = null,
                    RegistrationId = 0,
                    SessionPanelId = null
                })).OrderByDescending(o => o.FiscalYear)
                .ThenByDescending(o => o.PanelEndDate)
                .ThenBy(o => o.ProgramAbrv)
                .ThenBy(o => o.PanelAbrv);

            return result;
        }
        #region Helpers
        /// <summary>
        /// Get user info data for users from stored procedure
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <param name="uspResult">User list from stored procedure</param>
        /// <returns>Enumerable list of FoundUserModels</returns>
        private static IEnumerable<IFoundUserModel> GetUsers(P2RMISNETEntities context, List<KeyValuePair<int, decimal?>> uspResult)
        {
            var matchedUserIds = from matchedResult in uspResult select matchedResult.Key;

            var userInfoResult = from uspUsers in context.UserInfoes
                                 join address in context.UserAddresses on new { uspUsers.UserInfoID, PrimaryFlag = true } equals
                                     new { address.UserInfoID, address.PrimaryFlag }
                                    into addr from address in addr.DefaultIfEmpty()
                                 join email in
                                    (from ue in context.UserEmails where ue.PrimaryFlag == true select ue) on uspUsers.UserInfoID equals email.UserInfoID into eml
                                 from email in eml.DefaultIfEmpty()
                                 join email2 in
                                   (from ue in context.UserEmails where ue.PrimaryFlag == false select ue) on uspUsers.UserInfoID equals email2.UserInfoID into eml2
                                                                 from email2 in eml2.DefaultIfEmpty()
                                 where matchedUserIds.Contains(uspUsers.UserID)

                                 select new
                                 {
                                     UserId = uspUsers.UserID,
                                     UserInfoId = uspUsers.UserInfoID,
                                     CreateDate = (DateTime?)uspUsers.User.CreatedDate,
                                     Status = uspUsers.User.UserAccountStatus.FirstOrDefault().AccountStatu.AccountStatusName,
                                     StatusReason = uspUsers.User.UserAccountStatus.FirstOrDefault().AccountStatusReason.AccountStatusReasonName,
                                     LastName = uspUsers.LastName,
                             FirstName = uspUsers.FirstName,
                             MiddleName = uspUsers.MiddleName,

                             Address1 = address.Address1,
                             Address2 = address.Address2,
                             Address3 = address.Address3,
                             Address4 = address.Address4,
                             City = address.City,
                             State = (address.State == null) ? string.Empty : address.State.StateAbbreviation, 
                             Country = (address.Country == null) ? string.Empty : address.Country.CountryName,
                             CountryId = address.CountryId,
                             Zip = address.Zip,

                             EmailAddress = email.Email,
                             SecondaryEmailAddress = email2.Email,
                             EmailId = (email.EmailID == null) ? -1 : email.EmailID,

                             SessionPanelId =
                             from pua in context.PanelUserAssignments
                             where uspUsers.UserID == pua.UserId  &&
                                pua.SessionPanel.ProgramPanels.FirstOrDefault(x => x.ProgramYear.DateClosed == null) != null
                             select pua.SessionPanelId,

                             Group = 
                             from roles in context.UserSystemRoles
                             join lupRoles in context.SystemRoles on roles.SystemRoleId equals lupRoles.SystemRoleId
                             where uspUsers.UserID == roles.UserID
                             select lupRoles.SystemRoleName
                         };

            var result = from userInfo in userInfoResult.AsEnumerable()
			            join usp in uspResult on new { userInfo.UserId } equals new { UserId = usp.Key }
                        select new FoundUserModel {
                             UserId = userInfo.UserId,
                             UserInfoId = userInfo.UserInfoId,
                             CreateDate = userInfo.CreateDate,
                             Status = userInfo.Status,
                             StatusReason = userInfo.StatusReason,

                             LastName = userInfo.LastName,
                             FirstName = userInfo.FirstName,
                             MI = (string.IsNullOrWhiteSpace(userInfo.MiddleName)) ? string.Empty : userInfo.MiddleName.Substring(0, 1),

                             Address1 = userInfo.Address1,
                             Address2 = userInfo.Address2,
                             Address3 = userInfo.Address3,
                             Address4 = userInfo.Address4,
                             City = userInfo.City,
                             State = userInfo.State,
                             Country = userInfo.Country,
                             CountryId = userInfo.CountryId,
                             Zip = userInfo.Zip,

                             EmailAddress = userInfo.EmailAddress,
                             SecondaryEmailAddress = userInfo.SecondaryEmailAddress,
                             EmailId = userInfo.EmailId,

                             SessionPanelId = userInfo.SessionPanelId,

                             Group = userInfo.Group,
                             RelevancyRank = usp.Value
                        };

            return result;
        }
        /// <summary>
        /// get this user vendor id
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal static string GetVendorId(P2RMISNETEntities context, int? userId)
        {
            return (from User in context.Users
                    join UserInfo in context.UserInfoes on User.UserID equals UserInfo.UserID
                    join UserVendor in context.UserVendors on UserInfo.UserInfoID equals UserVendor.UserInfoId
                    where User.UserID == userId
                    select UserVendor.VendorId).FirstOrDefault();
        }
        #endregion
    }
}
