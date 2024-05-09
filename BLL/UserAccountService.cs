using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Container object returning the DAL user object from the BL.  Provides functions for accessing and updating specific
    /// objects in the DAL User object.
    /// </summary>
    public class UserAccountContainer
    {
        #region Constants
        private const bool PRIMARY_PHONE = true;
        private const bool ALTERNATE_PHONE = false;
        #endregion
        /// <summary>
        /// The user object
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Return the user's primary phone number
        /// </summary>
        public UserPhone PrimaryPhone
        {
            get { return GetPhoneNumber(PRIMARY_PHONE); }
        }
        /// <summary>
        /// Return the user's alternate phone number
        /// </summary>
        public UserPhone AlternatePhone
        {
            get { return GetPhoneNumber(ALTERNATE_PHONE); }
        }
        /// <summary>
        /// Return the user's primary email address
        /// </summary>
        public UserEmail PrimaryEmail
        {
            get { return GetEMail(true); }
        }
        /// <summary>
        /// Updates the UserPhone object with the provided phone number & extension number.
        /// </summary>
        /// <param name="phone">User Phone object</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <param name="extension">Phone extension</param>
        /// <exception cref="">Any exceptions are passed up and should be caught by the calling method.</exception>
        public void UpdatePhone(UserPhone phone, string phoneNumber, string extension)
        {
            phone.Phone = phoneNumber;
            phone.Extension = extension;

        }
        /// <summary>
        /// Updates the primary email address
        /// </summary>
        /// <param name="address">EMail address</param>
        /// <exception cref="">Any exceptions are passed up and should be caught by the calling method.</exception>
        public void UpdateEMail(string address)
        {
            PrimaryEmail.Email = address;
        }
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
        public void UpdateUserInfo(string firstName, string lastName, string middleName, string fullUserName, int? degreeLkpID, int? prefixLkpID, int? suffixLkpID)
        {
            List<UserInfo> usrInfoList = User.UserInfoes.ToList();
            int userInfoID = usrInfoList.FindIndex(a => a.UserInfoID != 0);
            UserInfo userInfo = usrInfoList[userInfoID];

            userInfo.FirstName = firstName;
            userInfo.LastName = lastName;
            userInfo.MiddleName = middleName;
            userInfo.BadgeName = fullUserName;
            userInfo.FullName = fullUserName;
            userInfo.DegreeLkpID = degreeLkpID;
            userInfo.PrefixLkpID = prefixLkpID;
            userInfo.SuffixLkpID = suffixLkpID;
        }
        #region Helpers
        /// <summary>
        /// Returns a primary or alternate phone number.
        /// </summary>
        /// <param name="state">Indicator for primary (true) or alternate (false)</param>
        /// <returns>Phone number if one is located; Empty phone number if not</returns>
        private UserPhone GetPhoneNumber(bool state)
        {
            UserPhone result = null;
            if (User != null)
            {
                result = User.UserInfoes.Single().UserPhones.Single(f => f.IsPrimary == state);
            }
            return (result != null) ? result : new UserPhone();
        }
        /// <summary>
        /// Returns a primary or alternate EMail address.
        /// </summary>
        /// <param name="state">Indicator for primary (true) or alternate (false)</param>
        /// <returns>EMail if one is located; Empty phone number if not</returns>
        private UserEmail GetEMail(bool state)
        {
            UserEmail result = null;
            if (User != null)
            {
                result = User.UserInfoes.Single().UserEmails.Single(f => f.IsPrimary == state);
            }
            return (result != null) ? result : new UserEmail();
        }
        #endregion
    }
}
