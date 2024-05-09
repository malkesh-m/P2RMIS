using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// UserController methods associated with the user's account.
    /// </summary>
    public partial class ManageUsers
    {
        #region Constants
        /// <summary>
        /// Value for no phone number
        /// </summary>
        public const string NO_PHONE_NUMBER = null;
        /// <summary>
        /// Value for no phone number extension
        /// </summary>
        public const string NO_PHONE_EXTENSION = null;
        #endregion
        #region Services
        /// <summary>
        /// Retrieve the information for a specific user.
        /// </summary>
        /// <param name="userID">User's unique identifier</param>
        public UserAccountContainer GetUserAccountInformation(int userID)
        {
            UserAccountContainer result = new UserAccountContainer();

            if (userID > 0)
            {
                result.User = GetById(userID);
            }

            return result;
        }
        /// <summary>
        /// Updates the primary phone number & extension.
        /// </summary>
        /// <param name="container">UserAccountContainer object</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="extension">Phone extension</param>
        /// <exception cref="">Any exceptions are passed up and should be caught by the calling method.</exception>
        public void UpdatePrimaryPhone(UserAccountContainer container, string phoneNumber, string extension)
        {
            UserPhone phone = container.PrimaryPhone;
            container.UpdatePhone(phone, phoneNumber, extension);
        }
        /// <summary>
        /// Updates the primary phone number & extension.
        /// </summary>
        /// <param name="container">UserAccountContainer object</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="extension">Phone extension</param>
        /// <exception cref="">Any exceptions are passed up and should be caught by the calling method.</exception>
        public void UpdateAlternatePhone(UserAccountContainer container, string phoneNumber, string extension)
        {
            UserPhone phone = container.AlternatePhone;
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                container.UpdatePhone(phone, phoneNumber, extension);
            }
            else
            {
                container.UpdatePhone(phone, NO_PHONE_NUMBER, NO_PHONE_EXTENSION);
            }
        }
        /// <summary>
        /// Updates the user's email information
        /// </summary>
        /// <param name="container">UserAccountContainer object</param>
        /// <param name="address">EMail address information</param>
        /// <exception cref="">Any exceptions are passed up and should be caught by the calling method.</exception>
        public void UpdateEmail(UserAccountContainer container, string address)
        {
            container.UpdateEMail(address);
        }
        /// <summary>
        /// Updates the user's name and related information.
        /// </summary>
        /// <param name="container">UserAccountContainer object</param>
        /// <param name="firstName">User's first name</param>
        /// <param name="lastName">User's last name</param>
        /// <param name="middleName">User's middle name</param>
        /// <param name="fullUserName">User's  full name</param>
        /// <param name="degreeLkpID">Degree lookup id</param>
        /// <param name="prefixLkpID">Prefix lookup id</param>
        /// <param name="suffixLkpID">Suffix lookup id</param>
        /// <exception cref="">Any exceptions are passed up and should be caught by the calling method.</exception>
        public void UpdateUserInfo(UserAccountContainer container, string firstName, string lastName, string middleName, string fullUserName, int? degreeLkpID, int? prefixId, int? suffixLkpID)
        {
            container.UpdateUserInfo(firstName, lastName, middleName, fullUserName, prefixId, suffixLkpID);
        }
        #endregion
    }    
}
