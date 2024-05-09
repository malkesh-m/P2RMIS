using Sra.P2rmis.Bll.Views;

namespace Sra.P2rmis.Bll
{
    public interface IManageUsers
    {
        /// <summary>
        /// Retrieve the information for a specific user.
        /// </summary>
        /// <param name="userID">User's unique identifier</param>
        UserAccountContainer GetUserAccountInformation(int userID);
        /// <summary>
        /// Updates the primary phone number & extension.
        /// </summary>
        /// <param name="container">UserAccountContainer object</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="extension">Phone extension</param>
        /// <exception cref="">Any exceptions are passed up and should be caught by the calling method.</exception>
        void UpdatePrimaryPhone(UserAccountContainer container, string phoneNumber, string extension);
        /// <summary>
        /// Updates the primary phone number & extension.
        /// </summary>
        /// <param name="container">UserAccountContainer object</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="extension">Phone extension</param>
        /// <exception cref="">Any exceptions are passed up and should be caught by the calling method.</exception>
        void UpdateAlternatePhone(UserAccountContainer container, string phoneNumber, string extension);
        /// <summary>
        /// Updates the user's email information
        /// </summary>
        /// <param name="container">UserAccountContainer object</param>
        /// <param name="address">EMail address information</param>
        /// <exception cref="">Any exceptions are passed up and should be caught by the calling method.</exception>
        void UpdateEmail(UserAccountContainer container, string address);
        /// <summary>
        /// Updates the user's information.
        /// </summary>
        /// <param name="container">UserAccountContainer object</param>
        /// <param name="info">Users name information</param>
        /// <exception cref="">Any exceptions are passed up and should be caught by the calling method.</exception>
        void UpdateUserInfo(UserAccountContainer container, string firstName, string lastName, string middleName, string fullUserName, int? degreeLkpID, int? prefixLkpID, int? suffixLkpID);
    }
}
