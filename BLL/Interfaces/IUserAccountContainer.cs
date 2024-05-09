using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Interfaces
{
    public interface IUserAccountContainer
    {
         /// <summary>
        /// The user object
        /// </summary>
        User User { get; set; }
        /// <summary>
        /// Return the user's primary phone number
        /// </summary>
        UserPhone PrimaryPhone  {get;}
        /// <summary>
        /// Return the user's alternate phone number
        /// </summary>
        UserPhone AlternatePhone  {get;}
        /// <summary>
        /// Return the user's primary email address
        /// </summary>
        UserEmail PrimaryEmail {get;}
       /// <summary>
        /// Updates the UserPhone object with the provided phone number & extension number.
        /// </summary>
        /// <param name="phone">User Phone object</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="extension">Phone extension</param>
        /// <exception cref="">Any exceptions are passed up and should be caught by the calling method.</exception>
        void UpdatePhone(UserPhone phone, string phoneNumber, string extension);
        /// <summary>
        /// Updates the primary email address
        /// </summary>
        /// <param name="address">EMail address</param>
        /// <exception cref="">Any exceptions are passed up and should be caught by the calling method.</exception>
        void UpdateEMail(string address);
        /// <summary>
        /// Updates the user's name and related information.
        /// </summary>
        /// <param name="firstName">User's first name</param>
        /// <param name="lastName">User's last name</param>
        /// <param name="middleName">User's middle name</param>
        /// <param name="fullUserName">User's  full name</param>
        /// <param name="degreeLkpID">Degree lookup id</param>
        /// <param name="prefixLkpID">Prefix lookup id</param>
        /// <param name="suffixLkpID">Suffix lookup id</param>
        /// <exception cref="">Any exceptions are passed up and should be caught by the calling method.</exception>
        void UpdateUserInfo(string firstName, string lastName, string middleName, string fullUserName, int? prefixLkpID, int? suffixLkpID);
        }
}
