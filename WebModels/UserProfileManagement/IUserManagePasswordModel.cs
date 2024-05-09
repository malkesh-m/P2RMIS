using System;
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// The manage user password object
    /// </summary>
    public interface IUserManagePasswordModel 
    {
        /// <summary>
        /// The user identifier
        /// </summary>
        int UserId { get; set; }
        /// <summary>
        /// The user name
        /// </summary>
        string Username { get; set; }
        /// <summary>
        /// The user first name
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// The user last name
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// The users primary email address
        /// </summary>
        string PrimaryEmail { get; set; }
        /// <summary>
        /// The users current password
        /// </summary>
        string CurrentPassword { get; set; }
        /// <summary>
        /// The users new password
        /// </summary>
        string NewPassword { get; set; }
        /// <summary>
        /// The list of the users seccurity question and answers
        /// </summary>
        List<UserSecurityQuestionAnswerModel> SecurityQuestionsAndAnswers { get; set; }

        /// <summary>
        /// Date password was last updated
        /// </summary>
        DateTime? PasswordUpdateDate { get; set; }

        /// <summary>
        /// The days until password expiration, taking into account the most recent password policy release date and subsequent grace period
        /// </summary>
        int EffectiveDaysUntilPasswordExpiration { get; set; }

        /// <summary>
        /// Date security questions were last updated
        /// </summary>
        DateTime? SecurityQuestionUpdateDate { get; set; }
        /// <summary>
        /// True if the user's password is a temporary passwword, false otherwise
        /// </summary>
        bool TemporaryPassword { get; set; }
    }
}
