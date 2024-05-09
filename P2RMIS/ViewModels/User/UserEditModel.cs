using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

//Only leave fields here that are required by the View - remove the other fields which are manipulated behind the 
//scenes.   The name of field should be same as the model.  Also Data Annotations can be added here
namespace Sra.P2rmis.Web.UI.Models
{
    public class UserEditModel
    {

        [HiddenInput(DisplayValue = true)]
        public string SubmitButtonName { get; set; }
        public int UserID { get; set; }

        [StringLength(100, ErrorMessage = "User Login cannot be longer than 100 characters.")]
        public string UserLogin { get; set; }

        public string Password { get; set; }
        public Nullable<bool> Verified { get; set; }
        public Nullable<System.DateTime> VerifiedDate { get; set; }
        public Nullable<System.DateTime> PasswordDate { get; set; }
        public string PasswordSalt { get; set; }
        public Nullable<int> Q1ID { get; set; }
        public Nullable<int> Q2ID { get; set; }
        public Nullable<int> Q3ID { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }

        
        public Nullable<int> ResumeFileID { get; set; }
        public Nullable<System.DateTime> ResumeDate { get; set; }
       

        public PrimaryPhoneModel PrimaryPhone { get; set; }
        public AlternatePhoneModel AlternatePhone { get; set; }
        public PrimaryEmailModel PrimaryEmail { get; set; }
        public AddressModel MainAddress { get; set; }
        public AddressModel W9Address { get; set; }
        public int[] SelectedRoles { get; set; }
        public SystemRoleModel[] SystemRole { get; set; }
        public UserInfoModel UserInfo { get; set; }

        public class SystemRoleModel
        {
            public int SystemRoleId { get; set; }
        }


        public class PhoneModel
        {
            public int PhoneID { get; set; }
            public int UserID { get; set; }
            public Nullable<bool> IsPrimary { get; set; }
            [StringLength(10, ErrorMessage = "Extension cannot be longer than 10 characters.")]
            public string Extension { get; set; }
            public bool International { get; set; }
        }

        public class PrimaryPhoneModel : PhoneModel
        {

            [Required(ErrorMessage = "Primary Phone Required"), StringLength(50, ErrorMessage = "Primary Phone cannot be longer than 50 characters.")]
            public string Phone { get; set; }

        }

        public class AlternatePhoneModel : PhoneModel
        {
            [StringLength(50, ErrorMessage = "Alternate Phone cannot be longer than 50 characters.")]
            public string Phone { get; set; }

        }

        public class EmailModel
        {
            public int EmailID { get; set; }
            public Nullable<bool> IsPrimary { get; set; }
        }

        public class PrimaryEmailModel : EmailModel
        {
            public int UserID { get; set; }
            [Required(ErrorMessage = "Email Required")]
            [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
            [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address.")]
            [Remote("IsEmailAvailable", "User", AdditionalFields = "UserID", ErrorMessage = "An account with this email address already exists.")]
            public string Email { get; set; }
        }

        public class AlternateEmailModel : EmailModel
        {
            [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
            [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address.")]
            public string Email { get; set; }
        }

        public class UserInfoModel
        {
            [Required(ErrorMessage = "First Name Required"), StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
            public string FirstName { get; set; }

            [StringLength(10, ErrorMessage = "Middle Name cannot be longer than 10 characters.")]
            public string MiddleName { get; set; }

            [Required(ErrorMessage = "Last Name Required"), StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
            public string LastName { get; set; }

            public Nullable<int> DegreeLkpID { get; set; }

            public Nullable<int> PrefixId { get; set; }

            public Nullable<int> SuffixLkpID { get; set; }

            public string FullUserName
            {
                get
                {
                    return FirstName + " " + LastName;
                }
            }
        }
        /// <summary>
        /// Gets/Sets a list of selected clients.
        /// </summary>
        public int[] SelectedClients { get; set; }
        /// <summary>
        /// Gets/Sets the current client list.
        /// </summary>
        public List<int> UsersClients { get; set; }

    }
}