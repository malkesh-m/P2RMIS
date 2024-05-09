using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Security;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.Dal
{
    public class MembershipRepository
    {
        public MembershipUser CreateUser(string userName, string password)
        {
            using (P2RMISNETEntities db = new P2RMISNETEntities())
            {
                User user = new User();

                user.UserLogin = userName;
                user.PasswordSalt = CreateSalt();
                user.Password = CreatePasswordHash(password, user.PasswordSalt);
                user.CreatedDate = GlobalProperties.P2rmisDateTimeNow;
                user.IsActivated = true;
                user.IsLockedOut = false;
                user.LastLockedOutDate = GlobalProperties.P2rmisDateTimeNow;
                user.LastLoginDate = GlobalProperties.P2rmisDateTimeNow;
                user.NewPasswordRequested = GlobalProperties.P2rmisDateTimeNow;
                user.NewEmailRequested = GlobalProperties.P2rmisDateTimeNow;
                //user.NewEmailKey = GenerateKey();

                db.Users.Add(user);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
                //TODO:  add function for email notification!


                return GetUser(userName);
            }
        }
        public bool ValidateUser(string userName, string password)
        {
            using (P2RMISNETEntities db = new P2RMISNETEntities())
            {
                var result = from u in db.Users where (u.UserLogin == userName) select u;

                if (result.Count() != 0)
                {
                    var dbuser = result.First();

                    if (dbuser.Password == CreatePasswordHash(password, dbuser.PasswordSalt))
                       
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public string GetUserNameByEmail(string email)
        {
            using (P2RMISNETEntities db = new P2RMISNETEntities())
            {
                var result = from u in db.UserEmails where (u.Email == email && u.PrimaryFlag == true) select u;

                if (result.Count() != 0)
                {
                    var dbuser = result.FirstOrDefault();

                    return dbuser.UserInfo.User.UserLogin;
                }
                else
                {
                    return "";
                }
            }
        }       
       
        public MembershipUser GetUser(string userName)
        {
            using (P2RMISNETEntities db = new P2RMISNETEntities())
            {
                var result = from u in db.Users where (u.UserLogin == userName) select u;

                if (result.Count() != 0)
                {
                    var dbuser = result.FirstOrDefault();
                    string _fullusername = dbuser.UserInfoes.Single().FullUserName;
                    string _userName = dbuser.UserLogin;
                    int _providerUserKey = dbuser.UserID;
                    string _email = dbuser.UserLogin;
                    string _passwordQuestion = "";
                    string _comment = "";
                    bool _isApproved = dbuser.IsActivated;
                    bool _isLockedOut = dbuser.IsLockedOut;
                    DateTime _creationDate = dbuser.CreatedDate;
                    DateTime _lastLoginDate = dbuser.LastLoginDate;
                    DateTime _lastActivityDate = GlobalProperties.P2rmisDateTimeNow;
                    DateTime _lastPasswordChangedDate = GlobalProperties.P2rmisDateTimeNow;
                    DateTime _lastLockedOutDate = dbuser.LastLockedOutDate;

                    MembershipUser user = new MembershipUser("CustomMembershipProvider",
                                                              _userName,
                                                              _providerUserKey,
                                                              _fullusername,
                                                              _passwordQuestion,
                                                              _comment,
                                                              _isApproved,
                                                              _isLockedOut,
                                                              _creationDate,
                                                              _lastLoginDate,
                                                              _lastActivityDate,
                                                              _lastPasswordChangedDate,
                                                              _lastLockedOutDate);

                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public string GenerateTemporaryPassword()
        {
            var randomPassword = string.Empty;
            var regex = new Regex(Membership.PasswordStrengthRegularExpression);
            while (!regex.IsMatch(randomPassword))
            {
                randomPassword = Membership.GeneratePassword(ConfigManager.PwdMinLength, ConfigManager.PwdNumberNonAlphanumericCharactersInGeneratedPassword);
            }
            return randomPassword;
        }
        public static string CreateSalt()

        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Hash and salt the password
        /// </summary>
        /// <param name="answer">User provided password</param>
        /// <returns>Returns a string that is the hashed and salted password</returns>
        public static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd =
                FormsAuthentication.HashPasswordForStoringInConfigFile(
                saltAndPwd, "sha1");
            return hashedPwd;
        }

        /// <summary>
        /// Hash the security answers
        /// </summary>
        /// <param name="answer">User provided answer to random security question</param>
        /// <returns>Returns a string that is the hashed security answer</returns>
        public static string CreateAnswerHash(string answer)
        {
            string hashedAnswer =
                FormsAuthentication.HashPasswordForStoringInConfigFile(
                answer.ToUpper(), "sha1");
            return hashedAnswer;
        }
        /// <summary>
        /// Check to see if the answer to the question matches the saved answer
        /// </summary>
        /// <param name="userID">ID of the user</param>
        /// <param name="QID">Question ID</param>
        /// <param name="answer">Answer submitted but the user to a specific question</param>
        /// <returns>true or false</returns>
        public bool CkAnswer(int userID, int QID, string answer)
        {
            bool retval = false;
            string answerLookup = null;
            using (P2RMISNETEntities db = new P2RMISNETEntities())
            {
                var result = from u in db.Users where (u.UserID == userID) select u;
                
                if (result.Count() != 0)
                {
                    var dbuser = result.First();

                    answerLookup = dbuser.UserAccountRecoveries.Where(x => x.QuestionOrder == QID).Select(x => x.Answer).FirstOrDefault();

                    retval = ((answerLookup != null) && (answerLookup == CreateAnswerHash(answer)));
                }
                else
                {
                    retval = false;
                }
            }
            return retval;
        }

        public bool CkAccountStatus(int userID, out string errorMessage)
        {
            bool retval = false;
            errorMessage = "";

            using (P2RMISNETEntities db = new P2RMISNETEntities())
            {
                var result = from u in db.Users where (u.UserID == userID) select u;

                if (result.Count() != 0)
                {
                    var dbuser = result.First();
                    switch (dbuser.ReadableAccountStatusReasonId())                     
                    {
                        case 1: //is invited
                            retval = true;
                            errorMessage = "";
                            return retval;

                        case 3: //is locked
                            retval = false;
                            errorMessage = string.Format(MessageService.AccountIsLocked, ConfigManager.LockedOutForInHours, ConfigManager.HelpDeskEmailAddress, ConfigManager.HelpDeskPhoneNumber, ConfigManager.HelpDeskHoursStandard);
                            return retval;

                        case 7: //is deactivated
                            retval = false;
                            errorMessage = "Your account has been deactivated. Please contact the help desk.";
                            return retval;
                        
                        case 2: //is Reset-Pending Confirmation
                            retval = true;
                            errorMessage = "";
                            return retval;

                        case 5: //is Invitation Expired
                            retval = true;
                            errorMessage = "";
                            return retval;

                        case 4: //is Password Expired
                            retval = true;
                            errorMessage = "";
                            return retval;
                        case AccountStatusReason.Indexes.Ineligible: // is Ineligible
                            retval = false;
                            errorMessage = "Your account has been deactivated. Please contact the help desk.";
                            return retval;
                        case AccountStatusReason.Indexes.PermCredentials:
                            retval = true;
                            errorMessage = "";
                            return retval;
                        default:
                            retval = true;
                            errorMessage = "";
                            return retval;
                    }//end switch
                }
            }

            return false;
        }
    }
}