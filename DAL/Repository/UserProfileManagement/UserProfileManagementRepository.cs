using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Dal.Repository.UserProfileManagement
{
    /// <summary>
    /// Provides access to search methods for User Profile Management.
    /// </summary>
    public class UserProfileManagementRepository : GenericRepository<User>, IUserProfileManagementRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public UserProfileManagementRepository(P2RMISNETEntities context)
            : base(context)
        {
        }
        #endregion
        /// <summary>
        /// Performs a fuzzy find on supplied user information
        /// </summary>
        /// <param name="firstName">User first name</param>
        /// <param name="lastName">User last name</param>
        /// <param name="email">User email address</param>
        /// <param name="username">user username</param>
        /// <param name="userId">User identifier</param>
        /// <returns>Zero or more users that match the search criteria</returns>
        public ResultModel<IFoundUserModel> FindUser(string firstName, string lastName, string email, string username, int? userId)
        {
            ResultModel<IFoundUserModel> result = new ResultModel<IFoundUserModel>();

            // if userid is supplied, only search by userid is conducted
            if (!userId.HasValue ||userId.Value <= 0)
            {
                List<ResultModel<IFoundUserModel>> interiumResult = new List<ResultModel<IFoundUserModel>>();

                ResultModel<IFoundUserModel> resultByName = new ResultModel<IFoundUserModel>();
                ResultModel<IFoundUserModel> resultByEmail = new ResultModel<IFoundUserModel>();
                ResultModel<IFoundUserModel> resultByUserName = new ResultModel<IFoundUserModel>();

                // only conduct query for non null inputs
                if (!string.IsNullOrWhiteSpace(firstName) || !string.IsNullOrWhiteSpace(lastName))
                {
                    resultByName.ModelList = RepositoryHelpers.FindUserByName(context, firstName, lastName);
                    interiumResult.Add(resultByName);
                }

                if (!string.IsNullOrWhiteSpace(email))
                {
                    resultByEmail.ModelList = RepositoryHelpers.FindUserByEmail(context, email);
                    interiumResult.Add(resultByEmail);
                }

                if (!string.IsNullOrWhiteSpace(username))
                {
                    resultByUserName.ModelList = RepositoryHelpers.FindUserByUsername(context, username);
                    interiumResult.Add(resultByUserName);
                }

                result = ConcatenateUserLists(interiumResult);
            }
            else
            {
                result.ModelList = RepositoryHelpers.FindUserByUserId(context, userId.Value);
            }

            return result;
        }
        /// <summary>
        /// Performs a fuzzy find on supplied user information
        /// </summary>
        /// <param name="firstName">User first name</param>
        /// <param name="lastName">User last name</param>
        /// <param name="email">user email address</param>
        /// <returns>Zero or more users that match the search criteria</returns>
        public ResultModel<IFoundUserModel> FindUser(string firstName, string lastName, string email)
        {
            List<ResultModel<IFoundUserModel>> interiumResult = new List<ResultModel<IFoundUserModel>>();

            ResultModel<IFoundUserModel> resultByName = new ResultModel<IFoundUserModel>();
            ResultModel<IFoundUserModel> resultByEmail = new ResultModel<IFoundUserModel>();

            // only conduct query for non null inputs
            if (!string.IsNullOrWhiteSpace(firstName) || !string.IsNullOrWhiteSpace(lastName))
            {
                resultByName.ModelList = RepositoryHelpers.FindUserByName(context, firstName, lastName);
                interiumResult.Add(resultByName);
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                resultByEmail.ModelList = RepositoryHelpers.FindUserByEmail(context, email);
                interiumResult.Add(resultByEmail);
            }

            return ConcatenateUserLists(interiumResult);
        }
        /// <summary>
        /// Merges and concatenates user lists
        /// </summary>
        /// <param name="results">Results of user lists</param>
        /// <returns>A user list ResultModel</returns>
        private ResultModel<IFoundUserModel> ConcatenateUserLists(ICollection<ResultModel<IFoundUserModel>> results)
        {
            ResultModel<IFoundUserModel> newResult = new ResultModel<IFoundUserModel>();
            List<IFoundUserModel> newUsers = new List<IFoundUserModel>();

            foreach (ResultModel<IFoundUserModel> result in results)
            {
                List<IFoundUserModel> users = result.ModelList.ToList();
                foreach (IFoundUserModel user in users)
                {
                    var duplicatedUsers = newUsers.Where(o => o.UserId == user.UserId);
                    if (duplicatedUsers != null && duplicatedUsers.Count() > 0)
                    {
                        // Take the highest value if user shows up more than once
                        if (duplicatedUsers.ToList()[0].RelevancyRank < user.RelevancyRank)
                            duplicatedUsers.ToList()[0].RelevancyRank = user.RelevancyRank;
                    }
                    else
                        newUsers.Add(user);
                }
            }
            newResult.ModelList = newUsers.AsEnumerable<IFoundUserModel>();

            return newResult;
        }

        /// <summary>
        /// Retrieves a list of participation assignments associated with a given user
        /// </summary>
        /// <param name="userInfoId">Id for a user's profile information</param>
        /// <returns>Result model of assignment information</returns>
        public ResultModel<IUserParticipationHistoryModel> GetParticipationHistory(int userInfoId)
        {
            ResultModel<IUserParticipationHistoryModel> result = new ResultModel<IUserParticipationHistoryModel>
            {
                ModelList = RepositoryHelpers.GetParticipationHistory(context.Users, userInfoId)
            };
            return result;
        }
        /// <summary>
        /// Gets the vendors.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        public IEnumerable<UserVendor> GetVendors(int userInfoId)
        {
            var userInfo = context.UserInfoes.FirstOrDefault(x => x.UserInfoID == userInfoId);
            var userVendors = userInfo != null ? userInfo.UserVendors.OrderBy(x => x.VendorTypeId) : null;
            return userVendors;
        }
        /// <summary>
        /// Gets the individual vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        public UserVendor GetIndividualVendor(int userInfoId)
        {
            return GetVendors(userInfoId)?.FirstOrDefault(x => x.VendorTypeId == VendorType.Indexes.Individual);
        }
        /// <summary>
        /// Gets the institutional vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        public UserVendor GetInstitutionalVendor(int userInfoId)
        {
            return GetVendors(userInfoId)?.FirstOrDefault(x => x.VendorTypeId == VendorType.Indexes.Institutional);
        }
        /// <summary>
        /// Get next available vendor identifier
        /// </summary>
        /// <returns></returns>
        public int? GetAvailableVendorId()
        {
            return context.VendorIdAssigneds.OrderBy(x => x.VendorId).FirstOrDefault(x => !x.AssignedFlag)?.VendorId;
        }
        /// <summary>
        /// Add individual user vendor
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <param name="vendorId"></param>
        /// <param name="vendorName"></param>
        /// <param name="userId"></param>
        public void AddIndividualUserVendor(int userInfoId, int vendorId, string vendorName, int userId)
        {
            UserVendor model = new UserVendor();
            model.UserInfoId = userInfoId;
            model.VendorId = vendorId;
            model.VendorName = vendorName;
            model.ActiveFlag = true;
            model.VendorTypeId = VendorType.Indexes.Individual;
            Helper.UpdateCreatedFields(model, userId);
            context.UserVendors.Add(model);
        }
        /// <summary>
        /// Add institutional user vendor
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <param name="vendorId"></param>
        /// <param name="vendorName"></param>
        /// <param name="userId"></param>
        public void AddInstitutionalUserVendor(int userInfoId, int vendorId, string vendorName, int userId)
        {
            UserVendor model = new UserVendor();
            model.UserInfoId = userInfoId;
            model.VendorId = vendorId;
            model.VendorName = vendorName;
            model.ActiveFlag = true;
            model.VendorTypeId = VendorType.Indexes.Institutional;
            Helper.UpdateCreatedFields(model, userId);
            context.UserVendors.Add(model);
        }
        /// <summary>
        /// Get individual user vendor
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns></returns>
        public UserVendor GetIndividualUserVendor(int userInfoId)
        {
            return context.UserVendors.FirstOrDefault(x => x.UserInfoId == userInfoId && x.ActiveFlag && x.VendorTypeId == VendorType.Indexes.Individual);
        }
        /// <summary>
        /// Get institutional user vendor
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns></returns>
        public UserVendor GetInstitutionalUserVendor(int userInfoId)
        {
            return context.UserVendors.FirstOrDefault(x => x.UserInfoId == userInfoId && x.ActiveFlag && x.VendorTypeId == VendorType.Indexes.Institutional);
        }
        /// <summary>
        /// Update user vendor
        /// </summary>
        /// <param name="vendor"></param>
        /// <param name="vendorName"></param>
        /// <param name="userId"></param>
        public void UpdateUserVendor(UserVendor vendor, int vendorId, string vendorName, int userId)
        {
            vendor.VendorId = vendorId;
            vendor.VendorName = vendorName;
            Helper.UpdateModifiedFields(vendor, userId);
        }
        /// <summary>
        /// Set vendor ID as assigned
        /// </summary>
        /// <param name="vendorId"></param>
        /// <param name="userId"></param>
        public void SetVendorIdAssigned(int vendorId, int userId)
        {
            var model = context.VendorIdAssigneds.FirstOrDefault(x => x.VendorId == vendorId && !x.AssignedFlag);
            model.AssignedFlag = true;
            Helper.UpdateModifiedFields(model, userId);
        }
    }
}
