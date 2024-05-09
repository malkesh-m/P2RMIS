using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.AccessAccountManagement;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.ProgramRegistration;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.UI.Models;
using Sra.P2rmis.WebModels.UserProfileManagement;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace Sra.P2rmis.Web.Controllers.AccountManagement
{
    /// <summary>
    /// Account Controller provides access to account services
    /// </summary>
    public class AccountController : AccountBaseController
    {
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        public AccountController()
        {
        }

        /// <summary>
        /// Constructor for the UserProfileManagement controller.
        /// </summary>
        /// <param name="theAccessAccountService">Access account service</param>
        /// <param name="theUserProfileManagementService">User profile management service</param>
        /// <param name="theFileService">File Service</param>
        public AccountController(IAccessAccountManagementService theAccessAccountService, IUserProfileManagementService theUserProfileManagementService, IProgramRegistrationService theProgramRegistrationService, IMailService theMailService)
        {
            this.theAccessAccountService = theAccessAccountService;
            this.theUserProfileManagementService = theUserProfileManagementService;
            this.theProgramRegistrationService = theProgramRegistrationService;
            this.theMailService = theMailService;
        }

        /// <summary>
        /// Logon for authentication
        /// </summary>
        public ActionResult LogOn(string returnUrl)
        {
            if (Session["FailedLoginCount"] == null)
            {
                Session["FailedLoginCount"] = 0;
            }
            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnURL = returnUrl;
            }
            return View();
        }
        /// <summary>
        /// Logon for post authentication, validates user and sets principal object with userid and authorizations
        /// </summary>
        /// <param name="model">Entries from the logon form</param>
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            RemoveErrorMessages();
            if (model.UserName == null)
            {
                SetLoginErrorMessage(MessageService.InvalidUserNamePassword, string.Empty);
            }
            else
            {
                var userName = TrimNonAscii(model.UserName.Trim());
                var user = Membership.GetUser(userName, true);
                if (user == null)
                {
                    SetLoginErrorMessage(MessageService.InvalidUserNamePassword, string.Empty);
                }
                else
                {
                    var userId = Convert.ToInt32(user.ProviderUserKey);
                    //AutoUnlock User Account
                    theAccessAccountService.AutoUnlockAccount(userId,ConfigManager.LockedOutForInHours);                    
                    if (model.Password == null) 
                    {
                        SetInvalidPasswordErrorMessage(userId, userName);
                    }
                    else
                    {
                        var password = TrimNonAscii(model.Password.Trim());                        
                        var userIsValid = theAccessAccountService.ValidateUser(userName, password);
                        if (userIsValid)
                        {
                            var capability = theAccessAccountService.GetUserLoginCapability(userId);
                            var capabilityType = capability.CapabilityType;
                            var userLoginCapable = IsUserLoginEligible(capabilityType);
                            if (userLoginCapable)
                            {
                                return ProcessLogin(capabilityType, user, returnUrl);
                            }
                            else
                            {
                                ProcessInvalidLoginMessage(capability);
                            }
                        }
                        else
                        {

                            SetInvalidPasswordErrorMessage(userId, userName);
                        }
                    }
                }
            }

            return View(model);

        }
        /// <summary>
        /// Handle log in attempt after session timeout. If multiple tabs are open when timeout, user is redirected to return url after logging in any tab.
        /// </summary>
        /// <param name="returnUrl">Redirect Url after valid login</param>
        public ActionResult LogOnAfterTimeout(string returnUrl)
        {           
            //redirect user to return url if user is already logged in
            if (Session[SessionVariables.CredentialPermanent] != null && Session[SessionVariables.CredentialPermanent].Equals(1))
            {
                string decodedUrl = "";
                if (!string.IsNullOrEmpty(returnUrl))
                    decodedUrl = Server.UrlDecode(returnUrl);
                if (Url.IsLocalUrl(decodedUrl))
                {
                    return Redirect(decodedUrl);
                }                
            }            
            return RedirectToAction(Routing.Account.LogOn, Routing.P2rmisControllers.Account, new { returnUrl });                              
        }
        /// <summary>
        /// Set lockout/unsuccessful attempt message for Invalid Login.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="userName">The username</param>
        private void SetInvalidPasswordErrorMessage(int userId, string userName)
        {

            var userFailedAttemptsSession = BuildSessionVariableName("FailedLoginCount", userName);
            CountFailedLoginAttempt(userFailedAttemptsSession);
            LockoutUser(userId, userFailedAttemptsSession);
            if (theAccessAccountService.IsUserLockedOut(userId))
            {
                SetLoginErrorMessage(string.Format(MessageService.AccountIsLocked, ConfigManager.LockedOutForInHours, ConfigManager.HelpDeskEmailAddress, ConfigManager.HelpDeskPhoneNumber, ConfigManager.HelpDeskHoursStandard), string.Empty);
                Session[userFailedAttemptsSession] = null;

            }
            else
            {
                SetLoginErrorMessage(MessageService.InvalidUserNamePassword, string.Format(MessageService.AccountLockCountdownMessage,
                    ConfigManager.MaxNumberFailedAttempts, ConfigManager.MaxNumberFailedAttempts - Convert.ToInt32(Session[userFailedAttemptsSession] ?? 0)));
            }
        }       
        /// <summary>
        /// Sets up session and permissions for the login user
        /// </summary>
        /// <param name="userName">The username</param>
        /// <param name="userId">The user identifier</param>
        /// <param name="email">The users email</param>
        protected void UserSetup(string userName, int userId, string email)
        {
            FormsAuthentication.SetAuthCookie(userName, false);

            //get the user's Fullname
            //Retrieve the user list of operations from the database            
            List<string> authorizedList = theAccessAccountService.GetUserOperations(userId);
            Session[SessionVariables.AuthorizedActionList] = authorizedList;

            bool ViewAllClients = authorizedList.Contains("View All Clients");
            var manageUsers = new ManageUsers();
            //If user has permission for all clients get list of all clients otherwise only assigned
            List<int> userClientList = ViewAllClients ? manageUsers.GetAllClients() :
                theUserProfileManagementService.GetAssignedActiveClients(userId).ModelList.ToList().ConvertAll(x => x.ClientId);
            Session[SessionVariables.AuthorizedClientList] = userClientList;

            //Store the Client logo to display in Session
            if ((userClientList == null) || (userClientList.Count() != 1))
            {
                Session["ClientLogo"] = "P2RMIS";
            }
            else
            {
                Session["ClientLogo"] = Convert.ToString(userClientList[0]);
            }
            IUserModel userModel = theUserProfileManagementService.GetUser(userId);
            DateTime LastLoginDate = userModel.LastLoginDate;

            //load userid, fullname and authorized actions into the userdataString as lost element in ticket
            string userDataString = string.Concat(userId, "|", email,  "|", LastLoginDate, "|", userModel.RoleName);

            // Get the FormsAuthenticationTicket out of the encrypted cookie
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(userName, false);

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            //load the new user data into the ticket and re-encrypt
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                ticket.Version,
                ticket.Name,
                ticket.IssueDate,
                ticket.Expiration,
                ticket.IsPersistent,
                userDataString);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                            FormsAuthentication.Encrypt(authTicket));
            Response.Cookies.Add(cookie);

        }
        /// <summary>
        /// Logoff the user and redirect to Logon page
        /// </summary>
        public ActionResult LogOff()
        {
            Cache.ClearCacheForSession(Session.SessionID);
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("LogOn", "Account");
        }        

        /// <summary>
        /// View for User reset when clicks "Forgot Password" Link on Login page.
        /// </summary>
        /// <returns>ResetModel</returns>
        public ActionResult Reset()
        {
            //Check to see if Password reset is allowed
            if (!Membership.EnablePasswordReset)
            {
                throw new Exception(ControllerHelpers.SecuritySettingConstants.PasswordResetNotAllow);
            }
            return View();
        }

        /// <summary>
        /// Post for User reset. Checks email and then gets security question.
        /// </summary>
        /// <param name="model">ResetModel</param>
        /// <returns>email address and security question</returns>
        [HttpPost]
        public ActionResult Reset(ResetModel model)
        {
            if (ModelState.IsValid)
            {
                // Check to see if email is in the system
                string UserLogin = Membership.GetUserNameByEmail(model.Email);

                if (!String.IsNullOrEmpty(UserLogin))
                {
                    MembershipUser oMu = Membership.GetUser(UserLogin);

                    ////check status of useraccount
                    string errorMessage = "";
                    int userID = (int)oMu.ProviderUserKey;
                    bool hasSecurityQuestions = theUserProfileManagementService.HasSecurityQuestions(userID);

                    if (!theUserProfileManagementService.CkAccountStatus(userID, out errorMessage))
                    {
                        //there is something wrong (status account status reason) with the users account and they can not reset password
                        ModelState.AddModelError("", errorMessage);
                        return View(model);
                    }
                    else if (!hasSecurityQuestions)
                    {
                        // the user does not have the required number of security questions
                        ModelState.AddModelError("", MessageService.AccountIsInactive);
                        ModelState.AddModelError("", MessageService.HelpDeskContactInfo);
                        ModelState.AddModelError("", MessageService.HelpDeskHours);
                        ModelState.AddModelError("", MessageService.ChangePasswordEnterSecurityQuestions);
                        return View(model);
                    }
                    else
                    {
                        //if email exists, then go to security question
                        if (oMu != null)
                        {
                            Session[ControllerHelpers.AccountSessionVariables.SessionUsernameVar] = UserLogin;
                            Session[ControllerHelpers.AccountSessionVariables.SessionEmailVar] = model.Email;
                            Session[ControllerHelpers.AccountSessionVariables.SessionAnswerCountVar] = 0;
                            Session[ControllerHelpers.AccountSessionVariables.SessionUserIdVar] = userID;

                            return RedirectToAction("Reset2", "Account", new { userID = Session[ControllerHelpers.AccountSessionVariables.SessionUserIdVar] });
                        }
                        else
                        {
                            ModelState.AddModelError("", ControllerHelpers.SecuritySettingConstants.NoAssociatedEmail);
                            return View(model);
                        }
                    }
                }//ck if email exists
                else
                {
                    ModelState.AddModelError("", ControllerHelpers.SecuritySettingConstants.NoAssociatedEmail);
                    return View(model);
                }
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", ControllerHelpers.SecuritySettingConstants.SomethingWentWrong);
            return View(model);
        }

        public ActionResult Reset2(int userID)
        {
            ResetModel2 vResetModel2 = new ResetModel2();
            //Check to see if Password reset is allowed
            if (!Membership.EnablePasswordReset)
            {
                throw new Exception(ControllerHelpers.SecuritySettingConstants.PasswordResetNotAllow);
            }

            if (Session[ControllerHelpers.AccountSessionVariables.SessionEmailVar] == null)
            {
                //They did not complete the first part of the from, send them back
                ModelState.AddModelError("", ControllerHelpers.SecuritySettingConstants.CompleteFirstStage);
                return RedirectToAction("Reset", "Account");
            }
            else
            {
                IUserModel mngUser = theUserProfileManagementService.GetUser(userID);
                vResetModel2.FullUserName = mngUser.FullUsername;
                vResetModel2.UserID = userID;
                vResetModel2.UserLogin = mngUser.UserLogin;

                //get security question
                IUserRecoveryQuestionModel question = theUserProfileManagementService.GetRandomSecurityQuestion(userID);
                if (question != null)
                {
                    ViewBag.securityQuestion = question.QuestionText;
                    vResetModel2.QuestionNumber = question.QuestionOrder;
                    Session[ControllerHelpers.AccountSessionVariables.SessionQuestionNumberVar] = question.QuestionOrder;

                }
                ViewBag.email = Session[ControllerHelpers.AccountSessionVariables.SessionEmailVar];
                               
                return View(vResetModel2);
            }
        }

        /// <summary>
        /// Post for User reset2. Checks answer and counts how many times user answered the question wrong.
        /// </summary>
        /// <param name="model">ResetModel</param>
        /// <returns>email address and security question</returns>
        [HttpPost]
        public ActionResult Reset2(ResetModel2 model)
        {
            //chk to see if the answer matches the answer in database for that question
            if (theUserProfileManagementService.CKAnswer(model.UserID, model.QuestionNumber, model.Answer))
            {    
                if (ModelState.IsValid)
                {
                    theUserProfileManagementService.UpdatePasswordWhenSecurityQuestionsUsed(theMailService, model.UserID);
                    return RedirectToAction("Reset3", "Account", new { id = model.UserID });
                }
                else 
                {
                    ModelState.AddModelError("", ControllerHelpers.SecuritySettingConstants.UnableToSave);
                    return View(model);
                }
            }
            else
            {
                //get security question

                ViewBag.securityQuestion = (model.SecurityQuestion);
                ViewBag.email = Session["email"];
                ModelState.AddModelError("", ControllerHelpers.SecuritySettingConstants.WrongAnswer);
                return View(model);
            }
        }
        /// <summary>
        /// View for final step of reset password
        /// </summary>
        /// <returns>ResetModel</returns>
        public ActionResult Reset3()
        {
          return View();
        }
        /// <summary>
        /// Processes a potential valid login attempt
        /// </summary>
        /// <param name="type">The login type</param>
        /// <param name="user">The user object</param>
        /// <param name="returnUrl">The return Url</param>
        /// <returns>The action result to be sent to the browser</returns>
        private ActionResult ProcessLogin(LoginType type, MembershipUser user, string returnUrl)
        {
            var userId = Convert.ToInt32(user.ProviderUserKey);
            switch (type)
            {
                case LoginType.PermanentCredentials:
                    // set last login date
                    theAccessAccountService.SetLastLoginDate(userId);

                    break;
                case LoginType.TemporaryCredentials:
                    if (theAccessAccountService.IsInvitationExpired(ConfigManager.PwdNumberDaysBeforeResetExpire, userId, userId))
                    {
                        SetLoginErrorMessage(MessageService.AccountIsInactive, MessageService.ContactHelpDesk);
                        // note:  IsInvitationExpired sets invitation expired (Inactive)
                        return RedirectToAction("Logon", "Account");
                    }
                    // set last login date
                    theAccessAccountService.SetLastLoginDate(userId);
                    break;
                default:
                    string message = string.Format("AccountController.LogOn()  Invalid Invalid LoginType detected.");
                    throw new ArgumentException(message);
            }

            UserSetup(user.UserName, userId, user.Email);

            // Permanent or temporary credential
            if (type != LoginType.PermanentCredentials)
            {
                TempData[ControllerHelpers.AccountSessionVariables.SessionSuccessMessageVar1] = MessageService.VerifyAccount;
                //set session so that non-validated user can not see the menu items
                Session[SessionVariables.CredentialPermanent] = 0;
            }
            else
            {
                Session[SessionVariables.CredentialPermanent] = 1;
            }
            // Force a new user to validate account if it has not been validated yet
            if (!theUserProfileManagementService.IsUserProfileVerified(userId))
            {
                Session[SessionVariables.Verified] = 0;
            }
            else
            {
                Session[SessionVariables.Verified] = 1;
            }
            //Set Default Password Age Expired Value
            Session[SessionVariables.PasswordAgeExpired] = 0;
            // Redirection
            ActionResult redirect;
            //returnURL needs to be decoded
            string decodedUrl = "";
            if (!string.IsNullOrEmpty(returnUrl))
                decodedUrl = Server.UrlDecode(returnUrl);
            if (Convert.ToInt32(Session[SessionVariables.CredentialPermanent]) == 0 || 
                theUserProfileManagementService.IsPasswordExpired(userId))
            {
                redirect = RedirectToAction(Routing.UserProfileManagement.PasswordManagement, Routing.P2rmisControllers.UserProfile);
            }
            else if (Convert.ToInt32(Session[SessionVariables.Verified]) == 0)
            {
                redirect = RedirectToAction(Routing.UserProfileManagement.ViewUserToVerify, Routing.P2rmisControllers.UserProfile);
            }
            else if (Url.IsLocalUrl(decodedUrl))
            {
                redirect = Redirect(decodedUrl);
            }
            else
            {
                redirect = RedirectToAction(Routing.Home.Dashboard, Routing.P2rmisControllers.Home);
            }
            return redirect;
        }
        /// <summary>
        /// Processes and invalid login attempt
        /// </summary>
        /// <param name="capability">The user's login capability</param>
        /// <returns>The action result to be sent to the browser</returns>
        private void ProcessInvalidLoginMessage(LoginCapability capability)
        {
            GetLoginReasonMessage(capability.CapabilityReason);
        }

        #region Helpers
        /// <summary>
        /// Gets the message to be returned to the user for accounts that are inactive
        /// </summary>
        /// <param name="capabilityReason">The login capability of the user</param>
        internal void GetLoginReasonMessage(LoginReasonType capabilityReason)
        {
            switch (capabilityReason)
            {
                case LoginReasonType.AwaitingCredentials:
                    SetLoginErrorMessage(MessageService.AccountIsInactive, MessageService.ContactHelpDesk);
                    break;
                case LoginReasonType.Locked:
                    SetLoginErrorMessage(string.Format(MessageService.AccountIsLocked, ConfigManager.LockedOutForInHours, ConfigManager.HelpDeskEmailAddress, ConfigManager.HelpDeskPhoneNumber, ConfigManager.HelpDeskHoursStandard), String.Empty);
                    break;
                case LoginReasonType.PasswordExpired:
                    SetLoginErrorMessage(MessageService.PasswordIsExpired, MessageService.ContactHelpDesk);
                    break;
                case LoginReasonType.Inactivity:
                    SetLoginErrorMessage(MessageService.AccountIsInactive, MessageService.ContactHelpDesk);
                    break;
                case LoginReasonType.Ineligible:
                    SetLoginErrorMessage(MessageService.AccountIsDeactivated, MessageService.ContactHelpDesk);
                    break;
                case LoginReasonType.AccountClosed:
                    SetLoginErrorMessage(MessageService.AccountIsDeactivated, MessageService.ContactHelpDesk);
                    break;
                default:
                    SetLoginErrorMessage(string.Empty, string.Empty);
                    break;
            }
        }
        /// <summary>
        /// Builds a session variable name given a prefix and a unique value
        /// </summary>
        /// <param name="prefix">The prefix to use for the variable name</param>
        /// <param name="name">The unique main body portion of the variable name</param>
        /// <returns></returns>
        internal string BuildSessionVariableName(string prefix, string value)
        {
            StringBuilder result = new StringBuilder(prefix);

            return result.AppendFormat("_{0}", value).ToString();
        }
        /// <summary>
        /// Increment this users failed login attempt
        /// </summary>
        /// <param name="userFailedAttemptsSession">The name of this users failed login attemp session variable</param>
        /// <param name="maxFailures">Max number of failed login attempts allowed</param>
        internal void CountFailedLoginAttempt(string userFailedAttemptsSession)
        {
            Session[userFailedAttemptsSession] = (Session[userFailedAttemptsSession] == null) ? 1 :
                            (Convert.ToInt32(Session[userFailedAttemptsSession]) < ConfigManager.MaxNumberFailedAttempts) ? Convert.ToInt32(Session[userFailedAttemptsSession]) + 1 : Session[userFailedAttemptsSession];
        }
        /// <summary>
        /// Sets the user account status to Locked for this user if the allowable number of logon attempts has been reached
        /// </summary>
        /// <param name="userId">The user identifier of the user attempting to logon</param>
        /// <param name="userFailedAttemptsSession">The name of the session variable containing the count of failed logon attempts for this user</param>
        /// <param name="maxAttemptsAllowed">The maximum number of failed logon attempt allowed</param>
        internal void LockoutUser(int userId, string userFailedAttemptsSession)
        {
            if (Convert.ToInt32(Session[userFailedAttemptsSession]) == ConfigManager.MaxNumberFailedAttempts)
            {
                theAccessAccountService.LockoutUser(userId, userId);                             
            }            
        }
        internal bool IsUserLoginEligible(LoginType type)
        {
            bool result = false;
            switch (type)
            {
                case LoginType.Default:
                case LoginType.NoCredentials:
                    result = false;
                    break;
                case LoginType.TemporaryCredentials:
                case LoginType.PermanentCredentials:
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }
        internal void SetLoginErrorMessage(string line1, string line2)
        {
            StringBuilder sb = new StringBuilder();
            TempData[ControllerHelpers.AccountSessionVariables.SessionSuccessMessageVar1] = line1;
            TempData[ControllerHelpers.AccountSessionVariables.SessionSuccessMessageVar2] = line2;
        }
        internal void RemoveErrorMessages()
        {
            TempData.Remove(ControllerHelpers.AccountSessionVariables.SessionSuccessMessageVar1);
            TempData.Remove(ControllerHelpers.AccountSessionVariables.SessionSuccessMessageVar2);
        }
        /// <summary>
        /// Remove non-printable characters from string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal string TrimNonAscii(string value)
        {
            string pattern = "[\x00\x08\x0B\x0C\x0E-\x1F]";
            Regex reg_exp = new Regex(pattern);
            return reg_exp.Replace(value, "");
        }
        #endregion
    }
}
