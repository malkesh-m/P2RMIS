using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.UserProfileManagement;

namespace Sra.P2rmis.Dal.Repository.UserProfileManagement
{
    public interface IUserProfileManagementRepository : IGenericRepository<User>
    {
        /// <summary>
        /// Performs a fuzzy find on supplied user information
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="userId"></param>
        /// <returns>Zero or more users that match the search criteria</returns>
        ResultModel<IFoundUserModel> FindUser(string FirstName, string LastName, string email, string username, int? userId);
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
        /// Gets the individual vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        UserVendor GetIndividualVendor(int userInfoId);
        /// <summary>
        /// Gets the institutional vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        UserVendor GetInstitutionalVendor(int userInfoId);
        /// <summary>
        /// Gets the available vendor identifier.
        /// </summary>
        /// <returns></returns>
        int? GetAvailableVendorId();
        /// <summary>
        /// Adds the individual user vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="vendorName">Name of the vendor.</param>
        /// <param name="userId">The user identifier.</param>
        void AddIndividualUserVendor(int userInfoId, int vendorId, string vendorName, int userId);
        /// <summary>
        /// Adds the institutional user vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="vendorName">Name of the vendor.</param>
        /// <param name="userId">The user identifier.</param>
        void AddInstitutionalUserVendor(int userInfoId, int vendorId, string vendorName, int userId);
        /// <summary>
        /// Gets the individual user vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        UserVendor GetIndividualUserVendor(int userInfoId);
        /// <summary>
        /// Gets the institutional user vendor.
        /// </summary>
        /// <param name="userInfoId">The user information identifier.</param>
        /// <returns></returns>
        UserVendor GetInstitutionalUserVendor(int userInfoId);
        /// <summary>
        /// Updates the user vendor.
        /// </summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="vendorName">Name of the vendor.</param>
        /// <param name="userId">The user identifier.</param>
        void UpdateUserVendor(UserVendor vendor, int vendorId, string vendorName, int userId);
        /// <summary>
        /// Sets the vendor identifier assigned.
        /// </summary>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void SetVendorIdAssigned(int vendorId, int userId);
    }
}
