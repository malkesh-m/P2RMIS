using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.Views.ApplicationDetails;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.Models;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;

namespace Sra.P2rmis.Web.Controllers
{
    /// <summary>
    /// Utility; common & shared methods used by controllers.
    /// </summary>
    internal partial class ControllerHelpers
    {
        #region Constants

        public class SecuritySettingConstants
        {
            internal const string PasswordMatchingEmailUsername = "Your password cannot match your email or username. Please select a different password.";
            internal const string PasswordInvalid = "The current password is incorrect or the new password is invalid.";
            internal const string SecurityUpdateSuccess = "Security Information update was successful.  Please Login with new user name and password.";

            internal static string SomethingWentWrong = "Something has gone wrong. If this persists please contact " + ConfigManager.HelpDeskEmailAddress + ".";
            internal const string PasswordHasChanged = "Your password has been changed.  Please login to access the system.";
            internal const string PasswordNotMeetReqs = "The current password does not meet the password requirements.  Please try again.";
            internal static string PasswordResetNotAllow = "Password reset is not allowed, please contact " + ConfigManager.HelpDeskEmailAddress + ".";
            internal const string NoAssociatedEmail = "The specified e-mail address is not associated with any account in P2RMIS.";
            internal const string CompleteFirstStage = "You must first complete the first stage of the reset form.";
            internal static string UnableToSave = "Unable to save changes. Try again, and if the problem persists, contact " + ConfigManager.HelpDeskEmailAddress + ".";
            internal const string WrongAnswer = "The answer specified is incorrect.  Please contact the helpdesk to have your password reset.";
            internal const string AccountExists = "An account with this User Name already exists.";
            internal const string CurrentPasswordIncorrect = "The current password is not correct.";
            internal const string NewPasswordMatchesOld = "The new password matches the current password.  Please try again.";
            internal const string SecurityInfoUpdate = "Your security info has been updated.  Please login to access the system.";
        }

        public class AccountSessionVariables
        {
            internal const string SessionSuccessMessageVar1 = "SuccessMessage1";
            internal const string SessionSuccessMessageVar2 = "SuccessMessage2";
            internal const string SessionUsernameVar = "username";
            internal const string SessionEmailVar = "email";
            internal const string SessionAnswerCountVar = "answerCount";
            internal const string SessionUserIdVar = "userID";
            internal const string SessionQuestionNumberVar = "QuestionNumber";
            internal const string SessionSystemEmailVar = "system-email-address";
            internal const string SessionErrorMessageVar = "errorMessage";
        }

        /// <summary>
        /// Value returned if there is not a valid user id.
        /// </summary>
        internal static int NoUserId = -1;

        #endregion

        #region Helper Methods
        /// <summary>
        /// Returns the current users id.  The id is obtained from the 
        /// custom identify in the custom principal.
        /// </summary>
        /// <param name="userIdenty">User object</param>
        /// <returns>User id if </returns>
        public static int GetCurrentUserId(IIdentity userIdenty)
        {
            int result = NoUserId;

            if (userIdenty != null)
            {
                ///
                /// Cast the identify to the custom identify and obtain the user's id.
                /// 
                CustomIdentity id = userIdenty as CustomIdentity;
                if (id != null)
                {
                    result = id.UserID;
                }
            }

            return result;
        }
        /// <summary>
        /// Sets the credential permanent.
        /// </summary>
        public static void SetCredentialPermanent()
        {
            HttpContext.Current.Session[SessionVariables.CredentialPermanent] = 1;
        }
        #endregion
        #region Extensions
        /// <summary>
        /// Allows overloading of ActionResults
        /// </summary>
        public class RequireRequestValueAttribute : ActionMethodSelectorAttribute
        {
            public RequireRequestValueAttribute(string valueName)
            {
                ValueName = valueName;
            }
            public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
            {
                return (controllerContext.HttpContext.Request[ValueName] != null);
            }
            public string ValueName { get; private set; }
        }
        #endregion
    }
}