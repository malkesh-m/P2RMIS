using System;
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// The manage user password object
    /// </summary>
    public class UserManagePasswordModel : IUserManagePasswordModel
    {
        private string _currentPassword;
        private string _newPassword;
        
        /// <summary>
        /// The user identifier
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// The user name
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// The user first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The user last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The users primary email address
        /// </summary>
        public string PrimaryEmail { get; set; }
        /// <summary>
        /// The users current password
        /// </summary>
        public string CurrentPassword {
            get { return this._currentPassword; }
            set { this._currentPassword = value?.Trim(); }
        }

        /// <summary>
        /// The users new password
        /// </summary>
        public string NewPassword
        {
            get { return this._newPassword;}
            set { this._newPassword = value?.Trim(); }
        }
        /// <summary>
        /// The list of the users seccurity question and answers
        /// </summary>
        public List<UserSecurityQuestionAnswerModel> SecurityQuestionsAndAnswers { get; set; }
        /// <summary>
        /// Date password was last updated
        /// </summary>
        public DateTime? PasswordUpdateDate { get; set; }
        /// <summary>
        /// The days until password expiration, taking into account the most recent password policy release date and subsequent grace period
        /// </summary>
        public int EffectiveDaysUntilPasswordExpiration { get; set; }
        /// <summary>
        /// Date security questions were last updated
        /// </summary>
        public DateTime? SecurityQuestionUpdateDate { get; set; }
        /// <summary>
        /// True if the user's password is a temporary passwword, false otherwise
        /// </summary>
        public bool TemporaryPassword { get; set; }
    }
}
