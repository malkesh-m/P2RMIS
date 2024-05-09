using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    public interface IUserModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        int UserId { get; set; }
        /// <summary>
        /// Gets or sets the user information identifier.
        /// </summary>
        /// <value>
        /// The user information identifier.
        /// </value>
        int UserInfoId { get; set; }
        /// <summary>
        /// Gets or sets the full username.
        /// </summary>
        /// <value>
        /// The full username.
        /// </value>
        string FullUsername { get; set; }
        /// <summary>
        /// Gets or sets the user login.
        /// </summary>
        /// <value>
        /// The user login.
        /// </value>
        string UserLogin { get; set; }
        /// <summary>
        /// Gets or sets the last login date.
        /// </summary>
        /// <value>
        /// The last login date.
        /// </value>
        DateTime LastLoginDate { get; set; }

        /// <summary>
        /// The name of the logged in user's role
        /// </summary>
        string RoleName { get; set; }
    }
    public class UserModel : IUserModel
    {
        public UserModel(int userId, int userInfoId, string fullUsername, string userLogin, DateTime lastLoginDate, string roleName)
        {
            UserId = userId;
            UserInfoId = userInfoId;
            FullUsername = fullUsername;
            UserLogin = userLogin;
            LastLoginDate = lastLoginDate;
            RoleName = roleName;
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the user information identifier.
        /// </summary>
        /// <value>
        /// The user information identifier.
        /// </value>
        public int UserInfoId { get; set; }
        /// <summary>
        /// Gets or sets the full username.
        /// </summary>
        /// <value>
        /// The full username.
        /// </value>
        public string FullUsername { get; set; }
        /// <summary>
        /// Gets or sets the user login.
        /// </summary>
        /// <value>
        /// The user login.
        /// </value>
        public string UserLogin { get; set; }
        /// <summary>
        /// Gets or sets the last login date.
        /// </summary>
        /// <value>
        /// The last login date.
        /// </value>
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// The name of the logged in user's role
        /// </summary>
        public string RoleName { get; set; }
    }
}
