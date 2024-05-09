using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Dal.Repository.UserProfileManagement
{
    public interface IUserRepository : IGenericRepository<User>
    {
        /// <summary>
        /// Performs a fuzzy find on supplied user information
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="userId"></param>
        /// <param name="vendorId">User's vendor identifier</param>
        /// <returns>Zero or more users that match the search criteria</returns>
        ResultModel<IFoundUserModel> FindUser(string FirstName, string LastName, string email, string username, int? userId, string vendorId);
        /// <summary>
        /// Performs a fuzzy find on supplied user information
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="email"></param>
        /// <returns>Zero or more users that match the search criteria</returns>
        ResultModel<IFoundUserModel> FindUser(string FirstName, string LastName, string email);
        /// <summary>
        /// Retrieves a list of participation assignments associated with a given user
        /// </summary>
        /// <param name="userInfoId">UserInfo entity identifier</param>
        /// <returns>Result model of assignment information</returns>
        ResultModel<IUserParticipationHistoryModel> GetParticipationHistory(int userInfoId);
        /// <summary>
        /// Gets the user address.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="addressTypeId">The address type identifier.</param>
        /// <returns></returns>
        UserAddress GetUserAddress(int userInfoId, int addressTypeId);
        /// <summary>
        /// Gets the user by login.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns></returns>
        User GetByLogin(string userLogin);
        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        UserInfo GetUserInfo(int userId);
        /// <summary>
        /// Adds the individual user vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="vendorName">Name of the vendor.</param>
        /// <param name="userId">The user identifier.</param>
        void AddIndividualUserVendor(int userInfoId, string vendorId, string vendorName, int userId);
        /// <summary>
        /// Adds the institutional user vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="vendorName">Name of the vendor.</param>
        /// <param name="userId">The user identifier.</param>
        void AddInstitutionalUserVendor(int userInfoId, string vendorId, string vendorName, int userId);
        /// <summary>
        /// Gets the individual user vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        UserVendor GetUserVendor(int userInfoId, bool isIndividual);
        /// <summary>
        /// Updates the user vendor.
        /// </summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="vendorName">Name of the vendor.</param>
        /// <param name="userId">The user identifier.</param>
        void UpdateUserVendor(UserVendor vendor, string vendorId, string vendorName, int userId);
        /// <summary>
        /// Activates the user vendor.
        /// </summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="userId">The user identifier.</param>
        void ActivateUserVendor(UserVendor vendor, int userId);
        /// <summary>
        /// Deactivates the user vendor.
        /// </summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="userId">The user identifier.</param>
        void DeactivateUserVendor(UserVendor vendor, int userId);
        /// <summary>
        /// Sets the vendor identifier assigned.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="userId">The user identifier.</param>
        bool SetVendorIdAssigned(string vendorId, int userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool UnassignVendorIdAssigned(string vendorId, int userId);
        /// <summary>
        /// Determines whether [is vendor identifier assigned] [the specified vendor identifier].
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is vendor identifier assigned] [the specified vendor identifier]; otherwise, <c>false</c>.
        /// </returns>
        bool IsVendorIdAssigned(string vendorId);
        /// <summary>
        /// Determines whether /[is vendor identifier used] [the specified vendor identifier].
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is vendor identifier used] [the specified vendor identifier]; otherwise, <c>false</c>.
        /// </returns>
        bool IsVendorIdUsed(string vendorId, bool isIndividual, int userInfoId);
        /// <summary>
        /// Sets the name of the vendor.
        /// </summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        void SetVendorName(UserVendor vendor, string name, int userId);
        /// <summary>
        /// Get with permission data
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        User GetWithPermission(int userId);
        /// <summary>
        /// Determines whether the specified user identifier is reviewer.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified user identifier is reviewer; otherwise, <c>false</c>.
        /// </returns>
        bool IsReviewer(int userId);
        /// <summary>
        /// get user info vendor id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetVendorId(int? userId);
    }
}
