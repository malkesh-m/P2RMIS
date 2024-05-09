using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// User service provides service methods relating to the User class.  Services provided are:
    ///     - TBD
    /// </summary>
    public partial class ManageUsers : IManageUsers, IDisposable
    {
        #region Attributes
        /// <summary>
        /// 
        /// </summary>
        protected IUnitOfWork unitOfWork;
        #endregion
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        public ManageUsers()
        {
            unitOfWork = new UnitOfWork();
        }
        /// <summary>
        /// Dispose of the service.
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)unitOfWork).Dispose();
        }
        #endregion

        #region Services

        public IEnumerable<Client> GetClients()
        {
            return unitOfWork.UofwUserRepository.GetClients();
        }
        public bool AddUser(User user)
        {
            unitOfWork.UofwUserRepository.Add(user);
            unitOfWork.Save();
            return true;
        }

        //changed by Peg, identical id's where being passes, changed one to looked up user (id) and one to currently logged in user (userident)
        public void UpdateUser(int id, int userident)
        {
            User userProfile = unitOfWork.UofwUserRepository.GetByID(id);
            userProfile.ModifiedBy = userident;
            userProfile.ModifiedDate = GlobalProperties.P2rmisDateTimeNow;
            unitOfWork.UofwUserRepository.UpdateUser(userProfile);

            unitOfWork.Save();

        }

		public void UpdateUserSecurity(User mngUser)
        {
            User userProfile = unitOfWork.UofwUserRepository.GetByID(mngUser.UserID);
            userProfile.ModifiedBy = mngUser.UserID;
            userProfile.ModifiedDate = GlobalProperties.P2rmisDateTimeNow;
            unitOfWork.Save();

        }

        //changed by Peg, identical id's where being passes, changed one to looked up user (id) and one to currently logged in user (userident)
        public bool VerifyUser(int id, int userident)
        {
            User userProfile = unitOfWork.UofwUserRepository.GetByID(id);

            userProfile.ModifiedBy = userident;
            userProfile.ModifiedDate = GlobalProperties.P2rmisDateTimeNow;
            unitOfWork.UofwUserRepository.UpdateUser(userProfile);
            userProfile.Verified = true;
            userProfile.VerifiedDate = GlobalProperties.P2rmisDateTimeNow;
            unitOfWork.Save();
            return true;
        }

        public bool DeleteUser(int id)
        {
            User user = null;

            user = unitOfWork.UofwUserRepository.GetByID(id);
            unitOfWork.UofwUserRepository.Delete(user);
            unitOfWork.Save();
            
            return true;
        }

        public void ClearSystemRoles(int id)
        {
            unitOfWork.UofwUserRepository.ClearSystemRoles(id);
        }

        public User GetById(int id)
        {
            return unitOfWork.UofwUserRepository.GetByID(id);
        }

        public List<string> GetUserEmails(int id)
        {
            return unitOfWork.UofwUserRepository.GetUserEmails(id);
        }

        public List<User> GetUserAutocomplete()
        {
            return unitOfWork.UofwUserRepository.GetUserAutocomplete();
        }

        public String GetSystemRolesString(User usr, string delimiter)
        {
            IEnumerable<UserSystemRole> sysRoles = usr.UserSystemRoles.Where(a => a.SystemRole.SystemRoleContext ==  SystemRole.RoleContext.System);

            return (sysRoles.Count() > 0 )? sysRoles.Select(a => a.SystemRole.SystemRoleName).Aggregate((i, j) => i + delimiter + j): string.Empty;
        }

        public List<int> GetUserClients(User usr)
        {
            IEnumerable<UserClient> usrClients = usr.UserClients;
            return usrClients.Select(a => a.ClientID).DefaultIfEmpty().ToList();
        }
        public List<int> GetAllClients()
        {
            return GetClients().Select(a => a.ClientID).DefaultIfEmpty().ToList();
        }
        public String GetEmailAddress(User usr)
        {
            string emailAddress = usr.UserInfoes.Single().UserEmails.Single(a => a.PrimaryFlag == true).Email;
            return emailAddress;
        }

        public string GetMailingAddress(User usr, string delimiter)
        {
            string retStr =null;
            UserAddress mailAddr = usr.UserInfoes.Single().UserAddresses.FirstOrDefault(a => a.AddressTypeId != AddressType.Indexes.W9);

            if (mailAddr != null)
            {
                retStr = mailAddr.Address1 + "," + delimiter;

                if (!String.IsNullOrEmpty(mailAddr.Address2))
                {
                    retStr += (mailAddr.Address2 + "," + delimiter);
                }

                if (!String.IsNullOrEmpty(mailAddr.Address3))
                {
                    retStr += (mailAddr.Address3 + "," + delimiter);
                }

                retStr += (mailAddr.City + ", " + GetUSStateString(mailAddr.AddressTypeId) + " " + mailAddr.Zip);
            }
            return retStr;
        }

        public int GetRoleId(string roleName)
        {
            List<SystemRole> lstRoles = GetRoles().ToList();

            return lstRoles.Single(f => f.SystemRoleName == roleName).SystemRoleId;
        }


        #region Lookup Helpers

        public IEnumerable<State> GetUSStates()
        {

            return unitOfWork.UofwUserRepository.GetUSStates();

        }
        /// <summary>
        /// Wrapper to test for existence of Id
        /// </summary>
        /// <param name="id">nullable index value</param>
        /// <returns>State string if id exists; empty string otherwise</returns>
        public string GetUSStateString(int? id)
        {
            return (id.HasValue) ? GetUSStateString(id.Value) : string.Empty;
        }
        public string GetUSStateString(int id)
        {
            try
            {
                List<State> lstUSStateNames = GetUSStates().ToList();

                if (id == 99) return "Other";

                return lstUSStateNames.Single(f => f.StateId == id).StateName;

            }
            catch (InvalidOperationException)
            {
                //Will come here for non-US states
                return "Other";
            }
        }

        public IEnumerable<Prefix> GetPrefixes()
        {
            return unitOfWork.UofwUserRepository.GetPrefixes();
        }

        public IEnumerable<SystemRole> GetRoles()
        {
            return unitOfWork.UofwUserRepository.GetRoles();
        }

        public string GetPrefixString(int id)
        {
            List<Prefix> lstPrefixStrings = GetPrefixes().ToList();

            return lstPrefixStrings.Single(f => f.PrefixId == id).PrefixName;
        }

        #endregion
        /// <summary>
        /// Service layer method adds and removes a users clients.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <param name="selectedClients">List of new client identifiers</param>
        /// <returns>True if list was successfully updated; false otherwise</returns>
        /// <exception cref="">Passes through exception that are thrown from the entity framework</exception>
        public bool UpdateClients(int id, int[] selectedClients)
        {
            bool result = false;

            if (ValidParameters(id, selectedClients))
            {
                User theUser = unitOfWork.UofwUserRepository.GetByID(id);
                //
                // First thing we need to do is empty the list.  Entity frame work is a bit
                // thin here.  Need to delete them because it does not know what to do with them
                // otherwise
                //
                unitOfWork.UserClientRepository.DeleteAll(theUser.UserClients);
                theUser.UserClients.Clear();

                unitOfWork.UofwUserRepository.UpdateUser(theUser);
                unitOfWork.Save(); 
                //    
                // then create the new list
                //    
                foreach (int clientId in selectedClients)
                {
                    UserClient usersClient = new UserClient();
                    usersClient.ClientID = clientId;
                    usersClient.UserID = theUser.UserID;
                    theUser.UserClients.Add(usersClient);
                }
                unitOfWork.UofwUserRepository.UpdateUser(theUser);
                unitOfWork.Save();
                result = true;
            }
            return result;
        }
        /// <summary>
        /// Validates the parameters for GetUserPrimaryEmailAddress.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <exception cref="ArgumentException">Thrown if User identifier fails validation</exception>
        private void ValidateGetUserPrimaryEmailAddressParameters(int userId)
        {
            ServerBase.ValidateInt(userId, "ManageUsers.GetUserPrimaryEmailAddress", "userId");
        }
        #endregion

        #region Helpers

        /// <summary>
        /// Validates the parameters for UpdateClients.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <param name="selectedClients">List of clients identifiers</param>
        /// <returns>True if the client list is valid; false otherwise</returns>
        private bool ValidParameters(int id, int[] selectedClients)
        {
            ///
            /// Validations:
            ///    - identifier is greater than 0
            ///    - list of clients is not null  
            ///    - there is at least one client
            /// 
            return ((id > 0) && (selectedClients != null) && (selectedClients.Length != 0));
        }

        /// <summary>
        /// Checks to make sure the password is not equal to the email or username 
        /// </summary>
        /// <param name="newPassword">the new password the user wants to change to</param>
        /// <param name="currentUserEmail">the users current email address</param>
        /// <param name="currentUserName">the users current user name</param>
        /// <returns>true if password does not match the email or username/false if the password equals either the username or email</returns>
        public static bool IsPasswordMatching(string newPassword, string currentUserEmail, string userName)
        {
            string password = newPassword.Trim().ToUpper();
            string email = currentUserEmail.Trim().ToUpper();
            string tmpUserName = userName.Trim().ToUpper();
            if (password == email || password == tmpUserName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
