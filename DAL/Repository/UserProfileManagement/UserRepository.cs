using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.UserProfileManagement;
using System.Data.Entity;

namespace Sra.P2rmis.Dal.Repository.UserProfileManagement
{
    /// <summary>
    /// Provides access to search methods for User Profile Management.
    /// </summary>
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public UserRepository(P2RMISNETEntities context) : base(context)
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
        /// <param name="vendorId">User's vendor identifier</param>
        /// <returns>Zero or more users that match the search criteria</returns>
        public ResultModel<IFoundUserModel> FindUser(string firstName, string lastName, string email, string username, int? userId, string vendorId)
        {
            ResultModel<IFoundUserModel> result = new ResultModel<IFoundUserModel>();

            if (userId.HasValue && userId.Value > 0)
            {
                result.ModelList = RepositoryHelpers.FindUserByUserId(context, userId.Value);
            }
            else if(!string.IsNullOrEmpty(vendorId))
            {
                result.ModelList = RepositoryHelpers.FindUserByVendorId(context, vendorId);
            }
            else
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
        /// Gets the user address.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="addressTypeId">The address type identifier.</param>
        /// <returns></returns>
        public UserAddress GetUserAddress(int userInfoId, int addressTypeId)
        {
            var userAddress = context.UserAddresses.FirstOrDefault(x => x.UserInfoID == userInfoId && x.AddressTypeId == addressTypeId);
            return userAddress;
        }
        /// <summary>
        /// Gets the user by login.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns></returns>
        public User GetByLogin(string userLogin)
        {
            var user = Get(x => x.UserLogin == userLogin).FirstOrDefault();
            return user;
        }
        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public UserInfo GetUserInfo(int userId)
        {
            var userInfo = context.UserInfoes.FirstOrDefault(x => x.UserID == userId);
            return userInfo;
        }
        /// <summary>
        /// Add individual user vendor
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <param name="vendorId"></param>
        /// <param name="vendorName"></param>
        /// <param name="userId"></param>
        public void AddIndividualUserVendor(int userInfoId, string vendorId, string vendorName, int userId)
        {
            UserVendor model = new UserVendor();
            model.UserInfoId = userInfoId;
            model.VendorId = vendorId;
            model.VendorName = vendorName;
            model.ActiveFlag = false;
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
        public void AddInstitutionalUserVendor(int userInfoId, string vendorId, string vendorName, int userId)
        {
            UserVendor model = new UserVendor();
            model.UserInfoId = userInfoId;
            model.VendorId = vendorId;
            model.VendorName = vendorName;
            model.ActiveFlag = false;
            model.VendorTypeId = VendorType.Indexes.Institutional;
            Helper.UpdateCreatedFields(model, userId);
            context.UserVendors.Add(model);
        }
        /// <summary>
        /// Adds the vendor identifier assigned.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void AddVendorIdAssigned(string vendorId, int userId)
        {
            var model = new VendorIdAssigned();
            model.VendorId = vendorId;
            model.AssignedFlag = true;
            Helper.UpdateCreatedFields(model, userId);
            context.VendorIdAssigneds.Add(model);
        }
        /// <summary>
        /// Get individual user vendor
        /// </summary>
        /// <param name="userInfoId"></param>
        /// <returns></returns>
        public UserVendor GetUserVendor(int userInfoId, bool isIndividual)
        {
            if (isIndividual)
            {
                return context.UserVendors.FirstOrDefault(x => x.UserInfoId == userInfoId && x.VendorTypeId == VendorType.Indexes.Individual);
            }
            else
            {
                return context.UserVendors.FirstOrDefault(x => x.UserInfoId == userInfoId && x.VendorTypeId == VendorType.Indexes.Institutional);
            }
        }
        /// <summary>
        /// Update user vendor
        /// </summary>
        /// <param name="vendor"></param>
        /// <param name="vendorName"></param>
        /// <param name="userId"></param>
        public void UpdateUserVendor(UserVendor vendor, string vendorId, string vendorName, int userId)
        {
            if (vendor != null)
            {
                vendor.VendorId = vendorId;
                vendor.VendorName = vendorName;
                Helper.UpdateModifiedFields(vendor, userId);
            }
        }
        /// <summary>
        /// Activates the user vendor.
        /// </summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="userId">The user identifier.</param>
        public void ActivateUserVendor(UserVendor vendor, int userId)
        {
            if (vendor != null)
            {
                vendor.ActiveFlag = true;
                Helper.UpdateModifiedFields(vendor, userId);
            }
        }
        /// <summary>
        /// Deactivates the user vendor.
        /// </summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeactivateUserVendor(UserVendor vendor, int userId)
        {
            if (vendor != null)
            {
                vendor.ActiveFlag = false;
                Helper.UpdateModifiedFields(vendor, userId);
            }
        }
        /// <summary>
        /// Set vendor ID as assigned
        /// </summary>
        /// <param name="vendorId"></param>
        /// <param name="userId"></param>
        /// <returns>True if already assigned; false if adding a new assigned record or updating an existing unassigned record to be assigned.</returns>
        public bool SetVendorIdAssigned(string vendorId, int userId)
        {
            bool flag = false;
            var model = context.VendorIdAssigneds.FirstOrDefault(x => x.VendorId == vendorId);
            if (model != null)
            {
                if (!model.AssignedFlag)
                {
                    model.AssignedFlag = true;
                    Helper.UpdateModifiedFields(model, userId);
                }
                else
                {
                    flag = true;
                }
            }
            else
            {
                // Add 
                if(vendorId != null)
                {
                    AddVendorIdAssigned(vendorId, userId);
                }
            }
            return flag;
        }
        /// <summary>
        /// Set vendor ID as assigned
        /// </summary>
        /// <param name="vendorId"></param>
        /// <param name="userId"></param>
        /// <returns>True if successfully unassigned; otherwise false.</returns>
        public bool UnassignVendorIdAssigned(string vendorId, int userId)
        {
            bool flag = false;
            var model = context.VendorIdAssigneds.FirstOrDefault(x => x.VendorId == vendorId);
            if (model != null)
            {
                model.AssignedFlag = false;
                Helper.UpdateModifiedFields(model, userId);
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// Determines whether [is vendor identifier assigned] [the specified vendor identifier].
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is vendor identifier assigned] [the specified vendor identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsVendorIdAssigned(string vendorId)
        {
            bool flag = false; 
            var userVendor = context.UserVendors.Where(x => x.VendorId == vendorId).FirstOrDefault();
            if (userVendor != null) flag = true; 
            return flag;
        }
        /// <summary>
        /// Determines whether [is vendor identifier used] [the specified vendor identifier].
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is vendor identifier used] [the specified vendor identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsVendorIdUsed(string vendorId, bool isIndividual, int userInfoId)
        {
            var getUserVendor = GetUserVendor(userInfoId, isIndividual);
            if(getUserVendor == null)
            {
                return false;
            }
            return getUserVendor.VendorId == vendorId;
        }
        /// <summary>
        /// Sets the name of the vendor.
        /// </summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        public void SetVendorName(UserVendor vendor, string name, int userId)
        {
            if (vendor != null)
            {
                vendor.VendorName = name;
                Helper.UpdateModifiedFields(vendor, userId);
            }
        }
        /// <summary>
        /// Get with permission data
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public User GetWithPermission(int userId)
        {
            var user = GetEager(x => x.UserID == userId, null, z1 => z1.UserSystemRoles.Select(z2 => z2.SystemRole
                .RoleTasks.Select(z3 => z3.SystemTask
                .TaskOperations.Select(z4 => z4.SystemOperation)))).FirstOrDefault();
            return user;
        }
        /// <summary>
        /// Determines whether the specified user identifier is reviewer.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified user identifier is reviewer; otherwise, <c>false</c>.
        /// </returns>
        public bool IsReviewer(int userId)
        {
            var role = context.Users.Include(z => z.UserSystemRoles)
                .FirstOrDefault(x => x.UserID == userId)?.UserSystemRoles
                .FirstOrDefault(y => y.SystemRoleId == SystemRole.Indexes.Reviewer);
            return role != null;
        }

        public string GetVendorId(int? userId)
        {
            return RepositoryHelpers.GetVendorId(context, userId);
        }
    }
}
