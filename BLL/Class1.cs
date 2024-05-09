using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using DAL;


namespace CustomMembership.Models
{
    /// <summary>
    /// Repository for user accounts
    /// </summary>

    public class UserRepository
    {
        public MembershipUser CreateUser(string username, string password, string email)
        {
            using (CustomMembershipDB db = new CustomMembershipDB())
            {
                tbl_users user = new tbl_users();

                user.email_login = username;
                user.email = email;
                user.password_salt = CreateSalt();
                user.password = EncodePassword(password, 1, user.password_salt);
                user.created_date = DateTime.Now;
                user.is_activated = false;
                user.is_locked_out = false;
                user.last_locked_out_date = DateTime.Now;
                user.last_login_date = DateTime.Now;
                user.new_email_key = GenerateKey();

                db.AddTotbl_users(user);
                db.SaveChanges();

                string ActivationLink = "http://localhost:PORT/Account/Activate/" +
                                     user.email_login + "/" + user.new_email_key;

                var message = new MailMessage("EMAIL_FROM", user.email_login)
                {
                    Subject = "Activate your account",
                    Body = ActivationLink
                };


                var client = new SmtpClient("SERVER");
                client.Credentials = new System.Net.NetworkCredential("USERNAME", "PASSWORD");
                client.UseDefaultCredentials = false;

                client.Send(message);


                return GetUser(username);
            }
        }

        public bool ValidateUser(string username, string password)
        {
            using (CustomMembershipDB db = new CustomMembershipDB())
            {
                var result = from u in db.tbl_users where (u.email_login == username) select u;

                if (result.Count() != 0)
                {
                    var dbuser = result.First();

                    if (dbuser.password == EncodePassword(password, 1, dbuser.password_salt) && dbuser.is_activated == true)
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
            using (CustomMembershipDB db = new CustomMembershipDB())
            {
                var result = from u in db.tbl_users where (u.email == email) select u;

                if (result.Count() != 0)
                {
                    var dbuser = result.FirstOrDefault();

                    return dbuser.email;
                }
                else
                {
                    return "";
                }
            }
        }

        public MembershipUser GetUser(string username)
        {
            using (CustomMembershipDB db = new CustomMembershipDB())
            {
                var result = from u in db.tbl_users where (u.lname == username) select u;

                if (result.Count() != 0)
                {
                    var dbuser = result.FirstOrDefault();

                    string _username = dbuser.email;
                    int _providerUserKey = dbuser.user_id;
                    string _email = dbuser.email;
                    string _passwordQuestion = "";
                    string _comment = "";
                    bool _isApproved = dbuser.is_activated;
                    bool _isLockedOut = dbuser.is_locked_out;
                    DateTime _creationDate = dbuser.created_date;
                    DateTime _lastLoginDate = dbuser.last_login_date;
                    DateTime _lastActivityDate = DateTime.Now;
                    DateTime _lastPasswordChangedDate = DateTime.Now;
                    DateTime _lastLockedOutDate = dbuser.last_locked_out_date;

                    MembershipUser user = new MembershipUser("CustomMembershipProvider",
                                                              _username,
                                                              _providerUserKey,
                                                              _email,
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


        public bool ActivateUser(string username, string key)
        {
            using (CustomMembershipDB db = new CustomMembershipDB())
            {
                var result = from u in db.tbl_users where (u.email_login == username) select u;

                if (result.Count() != 0)
                {
                    var dbuser = result.First();

                    if (dbuser.new_email_key == key)
                    {
                        dbuser.is_activated = true;
                        // TODO:  LastModifiedDate 
                        dbuser.last_login_date = DateTime.Now;
                        dbuser.new_email_key = null;

                        db.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
        }

        private static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }







        private static string GenerateKey()
        {
            Guid emailKey = Guid.NewGuid();

            return emailKey.ToString();
        }


    }
}