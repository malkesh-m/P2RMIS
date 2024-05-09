using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Attributes;
using Microsoft.Ajax.Utilities;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Web.Controllers.UserProfileManagement;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Sra.P2rmis.Bll;

namespace Sra.P2rmis.Web.UI.Models
{
    [Validator(typeof(PasswordManagementValidator))]
    public class PasswordManagementViewModel : UserProfileManagementViewModel
    {
        private string _confirmPassword;
        #region Constants
        /// <summary>
        /// Labels for the fieldsets present on the form
        /// </summary>
        public static class FieldsetLabels
        {
            public const string ChangePassword = "Change Password";
            public const string Security = "Security";
        }
        /// <summary>
        /// Labels for invidividual form fields
        /// </summary>
        public static class FieldLabels
        {
            public const string LastUpdated = "Last Updated: ";
            public const string CurrentPassword = "Current Password";
            public const string NewPassword = "New Password";
            public const string ConfirmPassword = "Re-type Password";
            public const string Question = "Question ";
            public const string Answer = "Answer ";
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public PasswordManagementViewModel()
        {
            this.SecurityInfo = new UserManagePasswordModel();
        }
        #endregion
        #region Dropdowns
        /// <summary>
        /// The question dropdown list
        /// </summary>
        public List<IListEntry> QuestionDropdown { get; set; }
        #endregion
        #region Properties
        /// <summary>
        /// Object representing a user's account security information 
        /// </summary>
        public IUserManagePasswordModel SecurityInfo { get; set; }
        /// <summary>
        /// Placeholder in view model to store the confirmation password. Used for validation only.
        /// </summary>
        public string ConfirmPassword
        {
            get { return this._confirmPassword;}
            set { this._confirmPassword = value?.Trim(); }
        }
        /// <summary>
        /// Password Last update date formatted as string
        /// </summary>
        public string PasswordLastUpdateDateFormatted { get
        {
            return ViewHelpers.FormatLastUpdateDateTime(SecurityInfo.PasswordUpdateDate);
        } }
        /// <summary>
        /// Security question last update date formatted
        /// </summary>
        public string SecurityLastUpdateDateFormatted
        {
            get
            {
                return ViewHelpers.FormatLastUpdateDateTime(SecurityInfo.SecurityQuestionUpdateDate);
            }
        }
        /// <summary>
        /// The last page URL
        /// </summary>
        public string LastPageUrl { get; set; }
        #endregion
        #region Validation

        /// <summary>
        /// FluentValidation Validator for ProfileViewModel
        /// </summary>
        public class PasswordManagementValidator : AbstractValidator<PasswordManagementViewModel>
        {
            public PasswordManagementValidator()
            {
                RuleFor(x => x.SecurityInfo.CurrentPassword)
                    .NotEmpty()
                    .WithMessage(MessageService.Constants.ConditionalFieldRequired, "Current Password");
                RuleFor(x => x.SecurityInfo.NewPassword).NotEmpty()
                    .When(
                        x =>
                            !x.SecurityInfo.CurrentPassword.IsNullOrWhiteSpace() ||
                            !x.ConfirmPassword.IsNullOrWhiteSpace())
                    .WithMessage(MessageService.Constants.ConditionalFieldRequired, "New Password");
                RuleFor(x => x.ConfirmPassword)
                    .Must(MatchNewPassword)
                    .When(x => !x.SecurityInfo.NewPassword.IsNullOrWhiteSpace())
                    .WithMessage("Password does not match re-type password.");
                RuleFor(x => x.SecurityInfo.CurrentPassword)
                    .Must(MatchCurrentPassword)
                    .When(x => !x.SecurityInfo.NewPassword.IsNullOrWhiteSpace())
                    .WithMessage("Incorrect current password.");
                RuleFor(x => x.SecurityInfo.NewPassword)
                    .Must(NotMatchCurrentPassword)
                    .When(x => !x.SecurityInfo.NewPassword.IsNullOrWhiteSpace())
                    .WithMessage("Password must not match current password.");
                RuleFor(x => x.SecurityInfo.NewPassword)
                    .Must(NotMatchNamesOrEmails)
                    .When(x => !x.SecurityInfo.NewPassword.IsNullOrWhiteSpace())
                    .WithMessage("Your Password must not contain your username, first name, last name or email address.");
                RuleFor(x => x.SecurityInfo.NewPassword)
                    .Length(ConfigManager.PwdMinLength, ConfigManager.PwdMaxLength)
                    .When(x => !x.SecurityInfo.NewPassword.IsNullOrWhiteSpace())
                    .WithMessage($"Your Password must have a minimum of {ConfigManager.PwdMinLength} and a maximum of {ConfigManager.PwdMaxLength} characters.");
                RuleFor(x => x.SecurityInfo.NewPassword)
                    .Matches("^(?=.*[A-Z]).+$")
                    .When(x => !x.SecurityInfo.NewPassword.IsNullOrWhiteSpace())
                    .WithMessage("Your Password must include at least one upper case letter (A-Z).");
                RuleFor(x => x.SecurityInfo.NewPassword)
                    .Matches("^(?=.*[a-z]).+$")
                    .When(x => !x.SecurityInfo.NewPassword.IsNullOrWhiteSpace())
                    .WithMessage("Your Password must include at least one lower case letter (a-z).");
                RuleFor(x => x.SecurityInfo.NewPassword)
                    .Matches("^(?=.*\\d).+$")
                    .When(x => !x.SecurityInfo.NewPassword.IsNullOrWhiteSpace())
                    .WithMessage("Your Password must include at least one number (0-9).");
                RuleFor(x => x.SecurityInfo.NewPassword)
                    .Matches("^(?=.*\\W).+$")
                    .When(x => !x.SecurityInfo.NewPassword.IsNullOrWhiteSpace())
                    .WithMessage("Your Password must include at least one special character (!@#$%&*()_+|~{}[]:”;’<>?,./).");
                RuleFor(x => x.SecurityInfo.NewPassword)
                    .Must(NotContainRepeatingChars)
                    .When(x => !x.SecurityInfo.NewPassword.IsNullOrWhiteSpace())
                    .WithMessage("Your Password must not include more than four repeating characters (AAAAA, 11111, @@@@@).");
                RuleFor(x => x.SecurityInfo.NewPassword)
                    .Must(NotMatchPrevious)
                    .When(x => !x.SecurityInfo.NewPassword.IsNullOrWhiteSpace())
                    .WithMessage("Your Password must be different from your previous passwords.");

                //Security q&a validator
                RuleFor(x => x.SecurityInfo.SecurityQuestionsAndAnswers)
                    .Must(AllQuestionsAndAnswersSelected)
                    .WithMessage("Security Questions and Answers are required.");
                //Individual q&a validators
                RuleFor(x => x.SecurityInfo.SecurityQuestionsAndAnswers)
                    .SetCollectionValidator(new SecurityQuestionValidator());
                //Each question should be unique
                RuleFor(x => x.SecurityInfo.UserId)
                    .Must(AllQuestionsAreUnique)
                    .WithMessage("Please choose 3 unique questions.");
                //Each answer provided should be unique
                RuleFor(x => x.SecurityInfo.UserId)
                    .Must(AllAnswersAreUnique)
                    .WithMessage("Each answer should be unique.");
            }
            internal bool AllQuestionsAndAnswersSelected(PasswordManagementViewModel model, List<UserSecurityQuestionAnswerModel> securityQuestionsAndAnswers)
            {
                return securityQuestionsAndAnswers.All(x => !string.IsNullOrEmpty(x.AnswerText) && x.RecoveryQuestionId != 0);
            }

            internal bool AllQuestionsAreUnique(PasswordManagementViewModel model, int arg2)
            {
                return 
                    model.SecurityInfo.SecurityQuestionsAndAnswers.Select(x => x.RecoveryQuestionId).Distinct().Count() == 3;
            }

            internal bool AllAnswersAreUnique(PasswordManagementViewModel model, int arg2)
            {
                return !model.SecurityInfo.SecurityQuestionsAndAnswers
                        .Where(w => !w.AnswerText.IfNullOrWhiteSpace(string.Empty).Replace("*", string.Empty).IsNullOrWhiteSpace())
                        .DefaultIfEmpty(new UserSecurityQuestionAnswerModel())
                        .GroupBy(x => x.AnswerText.IfNullOrWhiteSpace(string.Empty)).Any(g => g.Count() > 1);

            }

            internal bool MatchNewPassword(PasswordManagementViewModel model, string password)
            {
                return password == model.SecurityInfo.NewPassword;
            }

            internal bool MatchCurrentPassword(PasswordManagementViewModel model, string password)
            {
                return UserProfileManagementController.DoesMatchCurrentPassword(password, model.SecurityInfo.UserId);
            }

            internal bool NotMatchCurrentPassword(PasswordManagementViewModel model, string password)
            {
                return !UserProfileManagementController.DoesMatchCurrentPassword(password, model.SecurityInfo.UserId);
            }

            internal bool NotMatchNamesOrEmails(PasswordManagementViewModel model, string password)
            {
                var manageUsers = new ManageUsers();
                var userEmails = manageUsers.GetUserEmails(model.SecurityInfo.UserId);
                return !(password.Contains(model.SecurityInfo.Username) || 
                    userEmails.Any(x => password.Contains(x)) ||
                    password.Contains(model.SecurityInfo.FirstName) ||
                    password.Contains(model.SecurityInfo.LastName));
            }

            internal bool NotContainRepeatingChars(PasswordManagementViewModel model, string password)
            {
                var iterationCount = password.Length - (ConfigManager.PwdNumberRepeatingCharacters + 1);
                for (int i = 0; i <= iterationCount; i++)
                {
                    var character = password[i];
                    if (character == password[i + 1] &&
                        character == password[i + 2] &&
                        character == password[i + 3] &&
                        character == password[i + 4])
                    {
                        return false;
                    }
                }
                return true;
            }

            internal bool NotMatchPrevious(PasswordManagementViewModel model, string password)
            {
                return !UserProfileManagementController.DoesMatchPreviousPasswords(password, model.SecurityInfo.UserId);
            }
        }
        /// <summary>
        /// Fluent validator for security questions
        /// </summary>
        public class SecurityQuestionValidator : AbstractValidator<IUserSecurityQuestionAnswerModel>
        {
            public SecurityQuestionValidator()
            {
                RuleFor(x => x.AnswerText).NotEmpty()
                    .WithMessage("Answer is required.");
                RuleFor(x => x.RecoveryQuestionId)
                    .NotEmpty()
                    .WithMessage("Question is required.");
            }
        }
        #endregion
    }
}