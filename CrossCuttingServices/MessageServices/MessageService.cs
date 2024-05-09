using System;
using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;

namespace Sra.P2rmis.CrossCuttingServices.MessageServices
{
    /// <summary>
    /// Message provides access to user facing messages.
    /// </summary>
    public class MessageService : IMessageService
    {
        #region Constants
        /// <summary>
        /// Constants
        /// </summary>
        public class Constants
        {
            #region Parameterized
            /// <summary>
            /// Message for required field (param: Field Label)
            /// </summary>
            public const string FieldRequired = "{0} is required.";

            /// <summary>
            /// Message for field length exceeded (param: Field Label, Max Length)
            /// </summary>
            public const string MaxLengthExceeded = "{0} must not exceed {1} characters.";

            /// <summary>
            /// Message for conditionally required field (param: Field Label)
            /// </summary>
            public const string ConditionalFieldRequired = "Please provide {0}.";

            /// <summary>
            /// Message for conditionally required field with fieldset (param: Field Label)
            /// </summary>
            public const string ConditionalFieldRequiredWithFieldset = "Please provide {0} for {1}.";
            /// <summary>
            /// Message for duplicate email address
            /// </summary>
            public const string UniqueEmailAddress = "{0} email address must be unique.";
            /// <summary>
            /// The duplicate vendor identifier
            /// </summary>
            public const string DuplicateIndVendorId = "This vendor ID is already in use. Please enter a different individual vendor ID.";
            /// <summary>
            /// The duplicate ins vendor identifier
            /// </summary>
            public const string DuplicateInsVendorId = "This vendor ID is already in use. Please enter a different institutional vendor ID.";

            /// <summary>
            /// The unsubmitted critique notification (param: ReviewPhase)
            /// </summary>
            public const string UnsubmittedCritiqueNotification =
                "Your {0} Critique has not been submitted. ";

            /// <summary>
            /// The last update date message
            /// </summary>
            public const string LastUpdateDateMessage = "Last updated on {0}.";
            #endregion
            #region UserProfileManagement

            /// <summary>
            /// The email address error message
            /// </summary>
            public const string EmailAddressErrorMessage = "Please enter a valid email address.";
            /// <summary>
            /// Message for selection of an email address as primary required
            /// </summary>
            public const string PreferredEmailRequired = "Please provide a preferred email address";
            /// <summary>
            /// Message for a preferred phone being required
            /// </summary>
            public const string PreferredPhoneRequired = "Please provide a preferred phone number.";
            /// <summary>
            /// Message for US zip length being incorrect
            /// </summary>
            public const string USZipLengthIncorrect = "U.S. Zip Code must be 5 or 9 numerals.";
            /// <summary>
            /// Message for US zip length being incorrect
            /// </summary>
            public const string InternationalZipLengthIncorrect = "Zip Code must not exceed 9 characters.";
            /// <summary>
            /// Validation message for more than one spouse specified
            /// </summary>
            public const string AlternateContactNoMoreThanOneSpouse =
                "No more than one alternate contact of type spouse can be specified.";
            /// <summary>
            /// Validation message for more than one spouse specified
            /// </summary>
            public const string AlternateContactNoMoreThanOneEmergencyContact =
                "No more than one emergency contact can be specified.";

            /// <summary>
            /// Message for at least one client is required
            /// </summary>
            public const string ClientRequired = "At least one client is required.";

            /// <summary>
            /// Validation message for an invalid phone length
            /// </summary>
            public const string PhoneLengthInvalid =
                "The phone number specified is of invalid length. U.S. phone numbers should contain at least 10 numeric characters.";
            /// <summary>
            /// Validation message for validation of W-9 information
            /// </summary>
            public const string W9AccuracyRequired =
                "Please indicate whether your W-8/W-9 information is accurate.";
            /// <summary>
            /// Validation message for more than one personal address specified
            /// </summary>
            public const string NoMoreThanOnePersonalAddressAllowed =
                "Only one personal address is permitted.";
            /// <summary>
            /// Validation message for Prospects when an email or address is not supplied.
            /// </summary>
            public const string ProspectRequiresEmailOrAddress = "Please provide a preferred email or address.";
            /// <summary>
            /// Validation message for Prospects when an email or address is not supplied.
            /// </summary>
            public const string ProspectRequiresEmailOrPhone = "Please provide a preferred email or phone number.";
            #endregion
            #region Manage Account
            /// <summary>
            /// Message for Credentials have been successully sent
            /// </summary>
            public const string SendCredentialsSuccess = "Congratulations! You have successfully sent the credentials.";
            /// <summary>
            /// Message for sending credentials failure
            /// </summary>
            public const string SendCredentialsFailure = "Error! Credentials were not sent successfully.";
            #endregion
        }
        #endregion
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageService()
        {
        }
        #endregion
        //
        // By convention the MessageService methods & properties will follow a naming
        // convention as shown below.
        //
        //  public static string Get<Description>Message { get;}
        //
        // The above example shows a property but method services may exist but will follow
        // the same naming convention.  As a general rule methods & properties should be static but 
        // instances could exist where the static keyword is not warranted.  These will be addressed
        // on a case by case basis. 
        //
        // Until a service method exists without the static keyword the MessageService will not be 
        // injected by Unity.
        //
        #region Service Messages for other Services
        /// <summary>
        /// Generic message string indicating the parameters to a server method were invalid.  Usually this
        /// error message is used in an exception which is thrown from the validation method.
        /// </summary>
        /// <param name="serverAndMethod">Server name and method (ex: Server.MethodName)</param>
        /// <param name="parameter">Parameter name (ex. firstName)</param>
        /// <param name="value">Parameter value</param>
        /// <returns>Error message string.</returns>
        public static string GetInvalidServiceParameterMessage(string serverAndMethod, string parameter, string value)
        {
            return string.Format("{0} detected an invalid parameter: {1} was [{2}]", serverAndMethod, parameter, value);
        }
        #endregion
        #region User Profile Management
        /// <summary>
        /// First Name field is to long
        /// </summary>
        public static string FirstNameToLong
        {
            get { return ""; }
        }
        /// <summary>
        /// Middle initial field is to long
        /// </summary>
        public static string MiddleInitialToLong
        {
            get { return ""; }
        }
        /// <summary>
        /// Last Name field is to long
        /// </summary>
	    public static string LastNameToLong
        {
            get { return ""; }
        }
        /// <summary>
        /// Nick Name field is to long
        /// </summary>
	    public static string NickNameToLong
        {
            get { return ""; }
        }
        /// <summary>
        /// Suffix field is to long
        /// </summary>
 	    public static string SuffixToLong
        {
            get { return ""; }
        }
        /// <summary>
        /// Badge Name field is to long
        /// </summary>
 	    public static string BadgeToLong
        {
            get { return ""; }
        }
        /// <summary>
        /// First name was not supplied
        /// </summary>
        public static string FirstNameNotSupplied
        {
            get { return ""; }
        }
        /// <summary>
        /// Last name was not supplied
        /// </summary>
        public static string LastNameNotSupplied
        {
            get { return ""; }
        }
        /// <summary>
        /// UserLogin field is to long
        /// </summary>
        public static string UserLoginToLong
        {
            get { return ""; }
        }
        /// <summary>
        /// UserLogin was not supplied
        /// </summary>

        public static string UserLoginNotSupplied
        {
            get { return ""; }
        }
        /// <summary>
        /// WebsiteAddress field is to long
        /// </summary>
        public static string WebsiteAddressToLong
        {
            get { return ""; }
        }
        /// <summary>
        /// WebsiteAddress was not supplied
        /// </summary>        
        public static string WebsiteAddressNotSupplied
        {
            get { return ""; }
        }
        /// <summary>
        /// The military service was not fully completed
        /// </summary>        
        public static string MilitaryServiceIncomplete
        {
            get { return "Selection of military branch, rank and status are required. Please correct the errors and try again."; }
        }
        /// <summary>
        /// More than one personal address was selected
        /// </summary>
        public static string TooManyPersonalAddresses
        {
            get { return "Only one personal address is permitted."; }
        }
        /// <summary>
        /// One address is required
        /// </summary>
        public static string OneAddressIsRequired
        {
            get { return MessageService.Constants.ConditionalFieldRequired; }
        }
        /// <summary>
        /// A unexpected error occurred.
        /// </summary>
        public static string FailedSave
        {
            get { return string.Format("An unexpected error has occurred.  Your data may not have been saved.  If the error re-occurs, please contact the helpdesk at {0}.  Please allow 24 hours for an email response.", ConfigManager.HelpDeskEmailAddress); }
        }

        /// <summary>
        /// For use when field validation messages are summarized at top of the screen, preceeding the individual items
        /// </summary>
        public static string FailedValidationSummary
        {
            get { return "Please correct the following error(s):"; }
        }

        /// <summary>
        /// Pdf file was larger than the configured maximum
        /// </summary>
        /// <remarks></remarks>
        public static string PdfFileTooLarge
        {
            get { return string.Format("Upload failed. CV must not exceed {0} MB.", ConfigManager.UserResumeMaximuSize); }
        }
        /// <summary>
        /// Uploaded file is not a PDF file.
        /// </summary>
        public static string NotPdfFile
        {
            get { return "Your CV was not saved successfully - file is not a pdf file"; }
        }
        /// <summary>
        /// CV was not saved
        /// </summary>
        public static string ResumeSaveFailure
        {
            get { return "Your CV was not saved successfully - file write failure"; }
        }
        /// <summary>
        /// Operation was a success.
        /// </summary>
        public static string ResumeUploadSuccess
        {
            get { return "CV upload was successful."; }
        }
        /// <summary>
        /// Three different security questions must be answered
        /// </summary>
        public static string SecurityQuestionsFailed
        {
            get { return "Three different security questions must be answered."; }
        }
        /// <summary>
        /// Operation was a success.
        /// </summary>
        public static string SecurityUpdateSuccess
        {
            get { return "Congratulations! You have successfully saved your password and security questions and answers"; }
        }
        /// <summary>
        /// Returns string 'Password'.
        /// </summary>
        public static string PassWord
        {
            get { return "Password"; }
        }
        /// <summary>
        /// Returns the string 'Security Questions and Answers'.
        /// </summary>
        public static string SecurityQuestionAnswers
        {
            get { return "Security Questions and Answers"; }
        }
        /// <summary>
        /// Return null.
        /// </summary>
        public static string NoActionAttempted
        {
            get { return null; }
        }
        /// <summary>
        /// Returns string indicating that username and/or password are invalid
        /// </summary>
        public static string InvalidUserNamePassword
        {
            get { return "Username and/or password are invalid.  Please enter a valid username and password."; }
        }
        /// <summary>
        /// Return string indicating numbers of unsuccesssful login attempts remaining before account is locked
        /// </summary>
        public static string AccountLockCountdownMessage
        {
            get {  return "After {0} consecutive unsuccessful login attempts, your account will be locked.  You have {1} attempts remaining.";}
        }
        /// <summary>
        /// Returns string containing the contact help desk message
        /// </summary>
        public static string ContactHelpDesk
        {
            get { return String.Format("Please contact the helpdesk at {0} or {1}   {2}.", ConfigManager.HelpDeskEmailAddress, ConfigManager.HelpDeskPhoneNumber, ConfigManager.HelpDeskHours); }
        }
        /// <summary>
        /// Returns string containing the help desk contact information
        /// </summary>
        public static string HelpDeskContactInfo
        {
            get { return String.Format("Please contact the helpdesk at {0} or {1}", ConfigManager.HelpDeskEmailAddress, ConfigManager.HelpDeskPhoneNumber); }
        }
        /// <summary>
        /// Returns string containing the help desk hours
        /// </summary>
        public static string HelpDeskHours
        {
            get { return String.Format("{0}.", ConfigManager.HelpDeskHours); }
        }
        /// <summary>
        /// Returns string containing instructions for the first login post password reset
        /// </summary>
        public static string ChangePasswordEnterSecurityQuestions
        {
            get { return String.Format("After successful login, please go to the My Workspace <i>Change Password</i> tab to select Security Questions for future password retrieval."); }
        }
        /// <summary>
        /// Returns string containing instructions for reseting an expired password
        /// </summary>
        public static string ChangeExpiredPassword
        {
            get { return String.Format("You must reset your expired password."); }
        }
        /// <summary>
        /// Returns string containing the account is locked message
        /// </summary>
        public static string AccountIsLocked
        {
            get { return "Your account has been locked. You can try again in {0} hours or contact the Help Desk at {1} or {2}   {3}."; }
        }
        /// <summary>
        /// Returns string containing password is expired message
        /// </summary>
        public static string PasswordIsExpired
        {
            get { return String.Format("Your password has expired."); }
        }
        /// <summary>
        /// Returns string containing account is inactive message
        /// </summary>
        public static string AccountIsInactive
        {
            get { return String.Format("Your username and/or password have expired."); }
        }
        /// <summary>
        /// Returns string containing password expiration in X days message
        /// </summary>
        public static string PasswordExpirationInXDays
        {
            get { return "Your password will expire in {0} days. Please reset your password on the <a href='{1}'>Manage Password</a> page."; }
        }
        /// <summary>
        /// Returns string containing password expires today message
        /// </summary>
        public static string PasswordExpiresToday
        {
            get { return "Your password will expire today. Please reset your password on the <a href='{0}'>Manage Password</a> page."; }
        }
        /// <summary>
        /// Returns string containing the account has been deactivated message
        /// </summary>
        public static string AccountIsDeactivated
        {
            get { return String.Format("Your account has been deactivated."); }
        }
        /// <summary>
        /// Returns string indicating the user's session has expired
        /// </summary>
        public static string SessionExpired
        {
            get { return "Your session has expired.  Please refresh the page and try again."; }
        }
        /// <summary>
        /// Returns string indicating that the user needs to verify their account
        /// </summary>
        public static string VerifyAccount
        {
            get { return "You must verify your account information before proceeding on the system."; }
        }
        /// <summary>
        /// Gets the missing document template.
        /// </summary>
        /// <value>
        /// The missing document template.
        /// </value>
        public static string MissingDocumentTemplate
        {
            get { return "Document template is missing."; }
        }
        /// <summary>
        /// Gets the insufficient application data.
        /// </summary>
        /// <value>
        /// The insufficient application data.
        /// </value>
        public static string InsufficientApplicationData
        {
            get { return "Application data are insufficient."; }
        }
        /// <summary>
        /// Gets the missing application template.
        /// </summary>
        /// <value>
        /// The missing application template.
        /// </value>
        public static string MissingApplicationTemplate
        {
            get { return "Application template is missing."; }
        }
        /// <summary>
        /// Gets the unexpected exception.
        /// </summary>
        /// <value>
        /// The unexpected exception.
        /// </value>
        public static string UnexpectedException
        {
            get { return "There was an unexpected exception. More details were recorded in error logs."; }
        }
        /// <summary>
        /// Gets the start process failure message.
        /// </summary>
        /// <value>
        /// The start process failure message.
        /// </value>
        public static string StartProcessFailureMessage
        {
            get { return "{0}, Not posted for processing due to a missing template or critique data."; }
        }
        /// <summary>
        /// Gets the file no contents.
        /// </summary>
        /// <value>
        /// The file no contents.
        /// </value>
        public static string FileNoContents
        {
            get { return "File has no contents."; }
        }
        /// <summary>
        /// Gets the could not get document.
        /// </summary>
        /// <value>
        /// The could not get document.
        /// </value>
        public static string CouldNotGetDocument
        {
            get { return "Sorry we couldn't get the document."; }
        }
        /// <summary>
        /// Retrieve one or more messages.
        /// </summary>
        /// <param name="theStatuses">Collection of SaveProfileStatus messages</param>
        /// <returns>List of string messages</returns>
        public static List<string> GetMessages(ICollection<SaveProfileStatus> theStatuses)
        {
            List<string> messages = new List<string>();

            foreach (var status in theStatuses)
            {

                switch (status)
                {
                    case SaveProfileStatus.FirstNameToLong:
                        {
                            messages.Add(FirstNameToLong);
                            break;
                        }
                    case SaveProfileStatus.BadgeToLong:
                        {
                            messages.Add(BadgeToLong);
                            break;
                        }
                    case SaveProfileStatus.FirstNameNotSupplied:
                        {
                            messages.Add(FirstNameNotSupplied);
                            break;
                        }

                    case SaveProfileStatus.LastNameNotSupplied:
                        {
                            messages.Add(LastNameNotSupplied);
                            break;
                        }

                    case SaveProfileStatus.LastNameToLong:
                        {
                            messages.Add(LastNameToLong);
                            break;
                        }

                    case SaveProfileStatus.MiddleInitialToLong:
                        {
                            messages.Add(MiddleInitialToLong);
                            break;
                        }

                    case SaveProfileStatus.NickNameToLong:
                        {
                            messages.Add(NickNameToLong);
                            break;
                        }

                    case SaveProfileStatus.SuffixToLong:
                        {
                            messages.Add(SuffixToLong);
                            break;
                        }

                    case SaveProfileStatus.UserLoginNotSupplied:
                        {
                            messages.Add(UserLoginNotSupplied);
                            break;
                        }

                    case SaveProfileStatus.UserLoginToLong:
                        {
                            messages.Add(UserLoginToLong);
                            break;
                        }

                    case SaveProfileStatus.WebsiteAddressNotSupplied:
                        {
                            messages.Add(WebsiteAddressNotSupplied);
                            break;
                        }

                    case SaveProfileStatus.WebsiteAddressToLong:
                        {
                            messages.Add(WebsiteAddressToLong);
                            break;
                        }
                    case SaveProfileStatus.MilitaryRankInvalid:
                        {
                            messages.Add(MilitaryServiceIncomplete);
                            break;
                        }
                    case SaveProfileStatus.MilitaryStatusInvalid:
                        {
                            messages.Add(MilitaryServiceIncomplete);
                            break;
                        }
                    case SaveProfileStatus.IncompleteMilitaryIndex:
                        {
                            messages.Add(MilitaryServiceIncomplete);
                            break;
                        }
                    case SaveProfileStatus.TooManyPersonalAddresses:
                        {
                            messages.Add(TooManyPersonalAddresses);
                            break;
                        }
                    case SaveProfileStatus.OneAddressIsRequired:
                        {
                            messages.Add(OneAddressIsRequired);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            return messages;
        }
        /// <summary>
        /// Retrieve one or more messages.
        /// </summary>
        /// <param name="theStatuses">Collection of SaveResumeStatus messages</param>
        /// <returns>List of string messages</returns>
        public static List<string> GetMessages(ICollection<SaveResumeStatus> theStatuses)
        {
            List<string> messages = new List<string>();

            foreach (var status in theStatuses)
            {
                switch (status)
                {
                    case SaveResumeStatus.NotPdfFile:
                        {
                            messages.Add(NotPdfFile);
                            break;
                        }
                    case SaveResumeStatus.TooLarge:
                        {
                            messages.Add(PdfFileTooLarge);
                            break;
                        }
                    case SaveResumeStatus.Success:
                        {
                            messages.Add(ResumeUploadSuccess);
                            break;
                        }
                    case SaveResumeStatus.SaveFailure:
                        {
                            messages.Add(ResumeSaveFailure);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }

            return messages;
        }
        /// <summary>
        /// Retrieve one or more messages.
        /// </summary>
        /// <param name="theStatuses">Collection of SaveSecurityQuestionStatus messages</param>
        /// <returns>List of string messages</returns>
        public static List<string> GetMessages(ICollection<SaveSecurityQuestionStatus> theStatuses)
        {
            List<string> messages = new List<string>();

            foreach (var status in theStatuses)
            {
                switch (status)
                {
                    case SaveSecurityQuestionStatus.NoActionAttempted:
                        {
                            messages.Add(NoActionAttempted);
                        }
                        break;
                    case SaveSecurityQuestionStatus.PasswordSuccess:
                        {
                            messages.Add(MessageService.GetSuccessMessage(MessageService.PassWord));
                        }
                        break;
                    case SaveSecurityQuestionStatus.SecurityQuestionSuccess:
                        {
                            messages.Add(MessageService.GetSuccessMessage(MessageService.SecurityQuestionAnswers));
                        }
                        break;
                    case SaveSecurityQuestionStatus.Success:
                        {
                            messages.Add(SecurityUpdateSuccess);
                        }
                        break;
                    default:
                        {
                            break;
                        }
                }
            }
            return messages;
        }
        /// <summary>
        /// What to say when an error occurred and the update could not be saved.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetFailMessage()
        {
            List<string> messages = new List<string>();

            messages.Add(FailedSave);
            return messages;
        }
        /// <summary>
        /// Validation error message for user's role assignment
        /// </summary>
        public static string RoleRequiredMessage
        {
            get { return "Please select a role."; }
        }
        #endregion
        #region Generic Messages
        /// <summary>
        /// Generates a success message for a successful save
        /// </summary>
        /// <param name="objectThatWasUpdated">The object that was successfully saved</param>
        /// <returns>Success message</returns>
        public static string GetSuccessMessage(string objectThatWasUpdated)
        {
            return String.Format("Congratulations! You have successfully saved your {0}.", objectThatWasUpdated);
        }
        /// <summary>
        /// Returns a generic retrieval failure message.
        /// </summary>
        /// <returns>Failure message</returns>
        public static string GetFailureMessage()
        {
            return "There was a problem retrieving the requested data.  Please try again.";
        }
        #endregion
        #region Application Scoring Messages
        /// <summary>
        /// Standard text for no active panels available for scoring
        /// </summary>
        public const string NoActivePanels = "There are no active panels at this time.";
        /// <summary>
        /// Standard text for not registration not completed for this panel
        /// </summary>
        public const string NotRegistratedForPanel = "You are not registered for this panel.  Please go to Program Participation to register.";
        /// <summary>
        /// Invalid score message.
        /// </summary>
        public static string InvalidScore()
        {
            return "Please enter a value between {0} and {1}.";
        }
        /// <summary>
        /// Invalid score message.
        /// </summary>
        /// <returns></returns>
        public static string InvalidScoreMessage()
        {
            return "Invalid score was entered.  Please see instructions.";
        }
        /// <summary>
        /// Invalid score message - score was provided when none required
        /// </summary>
        /// <returns>Full message text</returns>
        public static string InvalidScoreValueProvided()
        {
            return "Score was provided when none was required.";
        }
        /// <summary>
        /// Application closed for scoring message.
        /// </summary>
        /// <returns></returns>
        public static string ScoringClosed()
        {
            return "Your scores cannot be saved because scoring has ended for this application. Please contact your RTA for assistance.";
        }
        /// <summary>
        /// Message to warn user that scoring has ended.
        /// </summary>
        /// <returns></returns>
        public static string ScoringClosedWarning()
        {
            return "The scoring window for this application has ended.";
        }
        /// <summary>
        /// Exception message when the current status is not valid for a status change request verification.
        /// </summary>
        public static string InvalidStatus
        {
            get { return "The current status value {0} is not valid"; }
        }
        /// <summary>
        /// Exception message when an active application was detected during activation.
        /// </summary>
        public static string ActiveApplicationFound
        {
            get { return "{0} detected an active application [{1}, {2}] was found"; }
        }
        #endregion
        #region Login Messages
        /// <summary>
        /// W9 verification required
        /// {0} is the page to navigate to  
        /// </summary>
        public static string W9Verify
        {
            get { return "Your W-9 has been updated. Please go to the <a href='{0}'>Profile</a> page to verify its accuracy."; }
        }
        /// <summary>
        /// W9 form is missing
        /// {0} is the page to navigate to for W9
        /// {1} is the page to navigate to for W8
        /// {2} Fax number
        /// </summary>
        public static string W9FormMissing
        {
            get { return "Your W-9 or W-8 information is missing. Please download the <a href='{0}' target='_blank'>W-9 Form</a> or <a href='{1}' target='_blank'>W-8 Form</a> and FAX the form to {2}"; }
        }
        /// <summary>
        /// W9 form is inaccurate
        /// {0} is the page to navigate to for W9
        /// {1} is the page to navigate to for W8
        /// {2} Fax number
        /// </summary>
        public static string W9Inaccurate
        {
            get { return "Your W-9 or W-8 information is inaccurate. Please download the <a href='{0}' target='_blank'>W-9 Form</a> or <a href='{1}' target='_blank'>W-8 Form</a> and FAX the form to {2}"; }
        }
        /// <summary>
        /// Message presented when Program/Panel registration is incomplete.
        /// </summary>
        public static string RegistrationIncomplete
        {
            get { return "Registration is incomplete.  Please go to the {0} screen to complete registration."; }
        }
        /// <summary>
        /// Message presented to user to notify their contract has been updated and needs re-signed.
        /// </summary>
        public static string ContractHasBeenUpdated
        {
            get { return "There has been a contract update.  Please go to {0} to review your contract."; }
        }

        /// <summary>
        /// Message shown when a customized contract has been saved
        /// </summary>
        public static string ContractSaveSuccess
        {
            get { return "Contract changes have been saved successfully."; }
        }
        /// <summary>
        /// Message presented when the users W-9 has been updated
        /// </summary>
        public static string W9Updated
        {
            get { return "Your W-9 has been updated. Please go to the {0} page to verify its accuracy."; }
        }
        #endregion
        #region Panel Registration Messages
        /// <summary>
        /// Thrown when an invalid RegistrationDocumentItem entity identifier was detected during registration
        /// </summary>
        public static string InvalidRegistrationDocumentItemId
        {
            get { return "An invalid RegistrationDocumentItemId [{0}] was detected"; }
        }

        /// <summary>
        /// Gets the confirm off-line contract success message.
        /// </summary>
        /// <value>
        /// The confirm off-line contract success message.
        /// </value>
        public static string ConfirmOfflineContractSuccess
        {
            get { return "Congratulations! You have successfully confirmed off-line signature of this contract."; }
        }
        /// <summary>
        /// Exception message for when a critique is submitted for update, but the critique was submitted.
        /// </summary>
        /// <param name="applicationWorkflowStepContentId">ApplicationWorkflowStepContnet entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Message</returns>
        public static string FailedToSaveCritiqueBecauseCritiqueWasSubmitted(int applicationWorkflowStepContentId, int userId)
        {
            return string.Format("This critique has already been submitted. Please refresh the page to see the updated critique.");
        }

        /// <summary>
        /// Message displayed to users if they require assistance completing the registration form
        /// </summary>
        public static string RegistrationFormHelpText => "Please contact your RTA if you have any questions regarding this form.";

        #endregion
        #region Training & Tutorials Messages
        /// <summary>
        /// Message displayed when the user has at least one incomplete registration.
        /// </summary>
        public static string TrainingIncompleteRegistrations
        {
            get
            {
                return "You have not completed registration for all your assigned panels.  To view all documents for your panel assignments, you must complete your registration.";
            }
        }
        /// <summary>
        /// Message displayed when the user has restricted access.
        /// </summary>
        public static string TrainingAccessRestriction
        {
            get
            {
                return "No information is available at this time due to an access issue.  If you have completed registration, and still see this message, please contact your SRO for assistance.";
            }
        }
        /// <summary>
        /// Message displayed when the user has no assigned programs.
        /// </summary>
        public static string TrainingNoProgramAssignment
        {
            get
            {
                return "You are not currently assigned to any programs.";
            }
        }
        #endregion
        #region Summary Statements
        /// <summary>
        /// Message displayed when a workflow failed check out.
        /// </summary>
        public static string CheckOutFailure
        {
            get
            {
                return "The document you selected is currently unavailable";
            }
        }
        /// <summary>
        /// Gets the check in invalid.
        /// </summary>
        /// <value>
        /// The check in invalid.
        /// </value>
        public static string CheckInInvalid => "The file uploaded did not contain all expected fields.  Please verify the file is correct and try again.";

        /// <summary>
        /// Message for a successful save
        /// </summary>
        public static string PrioritySaveSuccess => "You have successfully updated priorities for the selected applications.";

        /// <summary>
        /// Message for a failed priority save attempt
        /// </summary>
        public static string PrioritySaveFailure => "There was an error updating priorities. Please try again.";

        #endregion
        #region Reviewer Recruitment

        /// <summary>
        /// Gets the no work list message.
        /// </summary>
        /// <value>
        /// The no work list message.
        /// </value>
        public static string NoWorkListMessage
        {
            get
            {
                return "There are no worklist items at this time.";
            }
        }
        #endregion
        #region Panel Management Messages
        /// <summary>
        /// Constructs and returns the message for the Expertise tab on the PanelManagement overview
        /// Expertise/Assignments tab.
        /// </summary>
        /// <param name="dateTime">DateTime assignments were released</param>
        /// <returns>Release message</returns>
        public static string ReleaseAssignmentMessage(DateTime dateTime)
        {
            return $"The application assignments were released on {ViewHelpers.FormatDateTime2(dateTime)}.";
        }
        /// <summary>
        /// Returns the message for the Expertise tab on the PanelManagement overview
        /// Expertise/Assignments tab when a release date does not exist.
        /// </summary>
        /// <returns>Release message</returns>
        public static string ReleaseAssignmentMessage()
        {
            return $"The application assignments were released.";
        }
        /// <summary>
        /// Returns a formatted scoring scale instructions for the criterion edit modal.
        /// </summary>
        /// <param name="lowValue">Low scoring scale value</param>
        /// <param name="highValue">High scoring scale value</param>
        public static string CriterionScoreingScaleInstructions(string lowValue, string highValue)
        {
            return $"Please enter a value between {lowValue} and {highValue}";
        }
        /// <summary>
        /// Returns a formatted scoring scale label for the criterion edit modal.
        /// </summary>
        /// <param name="lowValue">Low scoring scale value</param>
        /// <param name="lowLabel">Low scoring scale label value</param>
        /// <param name="highValue">High scoring scale value</param>
        /// <param name="highLabel">High scoring scale label value</param>
        public static string CriterionScoreingScaleLabel(string lowValue, string lowLabel, string highValue, string highLabel)
        {
            return $"{lowValue} - {lowLabel}, {highValue} - {highLabel}";
        }
        /// <summary>
        /// The intro message for Emails sent out from PanelManagement communications tab.
        /// </summary>
        public static string RecipientListMessage { get { return "The original email was sent to the following recipients:"; } }
        /// <summary>
        /// Short version of Triaged symbol
        /// </summary>
        public static string TriagedAbbreviation { get { return "ND"; } }
        /// <summary>
        /// Short version of Triaged label
        /// </summary>
        public static string TriagedShortLabel { get { return "Not Discussed"; } }
        /// <summary>
        /// Triaged label
        /// </summary>
        public static string TriagedLabel { get { return "Not Discussed"; } }
        /// <summary>
        /// Triaged label for help text
        /// </summary>
        public static string TriageHelp { get { return "not discuss"; } }
        /// <summary>
        /// Triaged label for help text
        /// </summary>
        public static string TriageHelped { get { return "not discussed"; } }
        /// <summary>
        /// Triaged label for help text
        /// </summary>
        public static string TriageHelpOpposite { get { return "discuss"; } }
        /// <summary>
        /// Triaged label for help text
        /// </summary>
        public static string TriageHelpedOpposite { get { return "discussed"; } }
        /// <summary>
        /// Scoring not setup messge for expertise tab of PanelManagement
        /// </summary>
        public static string ScoringNotSetup { get { return "Scoring setup has not been completed. Please contact your RTA for assistance"; } }
        /// <summary>
        /// Gets the save order invalid format.
        /// </summary>
        /// <value>
        /// The save order invalid format.
        /// </value>
        public static string SaveOrderInvalidFormat { get { return "Invalid format. Please enter digits only."; } }
        /// <summary>
        /// Gets the save order blank number.
        /// </summary>
        /// <value>
        /// The save order blank number.
        /// </value>
        public static string SaveOrderBlankNumber { get { return "Order number cannot be blank. Please enter the order number for all applicable applications and try again."; } }
        /// <summary>
        /// Gets the save order duplicate number.
        /// </summary>
        /// <value>
        /// The save order duplicate number.
        /// </value>
        public static string SaveOrderDuplicateNumber { get { return "Same order number cannot be used for more than one application. Please enter unique order number for each application and try again."; } }
        /// <summary>
        /// Gets the generic save order error.
        /// </summary>
        /// <value>
        /// The generic save order error.
        /// </value>
        public static string SaveOrderGenericError { get { return "There was an error updating the order numbers. Please try again."; } }

        /// <summary>
        /// Message to display to user's who cannot send panel communication emails
        /// </summary>
        public static string CannotSendPanelCommunicationEmail => "Your institutional email is not set to Preferred.  You can only send emails from this module if your institutional email is set to Preferred.";

        /// <summary>
        /// Message to display if registered panel user has not completed their registration and attempt to access PanelManagement
        /// </summary>
        public static string RegistrationNotComplete => "You have not completed registration for this panel.";

        /// <summary>
        /// Message to display if a panel user who is expected to complete COI declarations has not done so
        /// </summary>
        public static string ExpertiseNotComplete => "You have not completed declaring COIs for all applications in this panel.";


        #endregion
        #region System Setup
        /// <summary>
        /// Message generated when a user tries to remove a program with assigned awards.
        /// </summary>
        public static string ProgramCannotBeRemovedOnceAwardAssigned
        {
            get { return $"You may not remove a program if an award has been assigned."; }
        }
        /// <summary>
        /// Message generated for trying to delete a program if awards have been assigned.
        /// </summary>
        public static string CannotRemoveProgramIfAwardsAssigned
        {
            get { return $"You may not remove a program if an award is assigned."; }
        }
        /// <summary>
        /// Message generated when trying to delete the last program.
        /// </summary>
        public static string CannotRemoveLastProgram
        {
            get { return $"You may not remove the last program of a fiscal year."; }
        }
        /// <summary>
        /// Error message when award with applications cannot be modified.
        /// </summary>
        public static string CannotModifyAwardWithApplications
        {
            get { return $"You may not edit an award or pre-application award once applications are assigned to the award."; }
        }
        /// <summary>
        /// Error message when award with criteria cannot be modified
        /// </summary>
        public static string CannotModifyAwardWithCriteria
        {
            get { return $"You may not edit an award or pre-application award once evaluation criteria is assigned to the award."; }
        }
        /// <summary>
        /// Error message when award with applications cannot be deleted.
        /// </summary>
        public static string CannotDeleteAwardWithApplications
        {
            get { return $"You may not delete an award or pre-application award once applications are assigned to the award."; }
        }
        /// <summary>
        /// Error message when award with criteria cannot be deleted.
        /// </summary>
        public static string CannotDeleteAwardWithCriteria
        {
            get { return $"You may not delete an award or pre-application award once evaluation criteria is assigned to the award."; }
        }
        /// <summary>
        /// Gets the cannot delete award with child award apps.
        /// </summary>
        /// <value>
        /// The cannot delete award with child award apps.
        /// </value>
        public static string CannotDeleteAwardWithChildAwardApps
        {
            get { return $"You may not delete an award if the pre-application award has assigned applications."; }
        }
        /// <summary>
        /// Error message when a duplicate award would be added
        /// </summary>
        public static string AddDuplicateAward
        {
            get { return $"You may not add an award or pre-application award that would result in duplicate receipt cycles, client award types and fiscal years."; }
        }
        /// <summary>
        /// Gets the add duplicate meeting.
        /// </summary>
        /// <value>
        /// The add duplicate meeting.
        /// </value>
        public static string AddDuplicateMeeting
        {
            get { return $"This meeting already exists."; }
        }
        /// <summary>
        /// Gets the modify duplicate meeting.
        /// </summary>
        /// <value>
        /// The modify duplicate meeting.
        /// </value>
        public static string ModifyDuplicateMeeting
        {
            get { return $"This meeting already exists."; }
        }
        /// <summary>
        /// Gets the add duplicate panel.
        /// </summary>
        /// <value>
        /// The add duplicate panel.
        /// </value>
        public static string AddDuplicatePanel
        {
            get { return $"You may not add a panel that would result in duplicate panel name or panel abbreviation."; }
        }
        /// <summary>
        /// Gets the modify duplicate panel.
        /// </summary>
        /// <value>
        /// The modify duplicate panel.
        /// </value>
        public static string ModifyDuplicatePanel
        {
            get { return $"You may not modify a panel that would result in duplicate panel name or panel abbreviation."; }
        }
        /// <summary>
        /// Gets the add duplicate session.
        /// </summary>
        /// <value>
        /// The add duplicate session.
        /// </value>
        public static string AddDuplicateSession
        {
            get { return $"You may not add a session that would result in duplicate session description or session abbreviation."; }
        }
        /// <summary>
        /// Gets the modify duplicate session.
        /// </summary>
        /// <value>
        /// The modify duplicate session.
        /// </value>
        public static string ModifyDuplicateSession
        {
            get { return $"You may not modify a session that would result in duplicate session description or session abbreviation."; }
        }
        /// <summary>
        /// Error message when a duplicate award would be created when modifying an existing ProgramMechanism
        /// </summary>
        public static string ModifyDuplicateAward
        {
            get { return $"You may not modify an award or pre-application award that would result in duplicate receipt cycles, client award types and fiscal years."; }
        }
        /// <summary>
        /// Error message when a program would be created with program abbreviations.
        /// </summary>
        public static string AddProgramWithDuplicateAbbreviation
        {
            get { return $"This program abbreviation already exists. Please enter a different abbreviation."; }
        }
        /// <summary>
        /// Error message when a program would be created with duplicate fiscal years.
        /// </summary>
        /// <param name="programAbbreviation">Program abbreviation</param>
        /// <returns>Error message for the rule violation</returns>
        public static string AddDuplicateFiscalYear(string programAbbreviation)
        {
            return $"The fiscal year already exists for {programAbbreviation}. Please select another fiscal year.";
        }
        /// <summary>
        /// Error message when more than one Evaluation Criteria is marked as "Overall"
        /// </summary>
        public static string SingleOverallCriterionRuleViolation
        {
            get { return $"Only one evaluation criteria can be marked as overall."; }
        }
        /// <summary>
        /// Error message displayed when deleting a ProgramMechanism is prevented because there is
        /// a a Pre-App award
        /// </summary>
        public static string PreAppRuleViolation
        {
            get { return $"You may not modify an award that has a pre app award without deleting the pre app award first."; }
        }
        /// <summary>
        /// Error message displayed when adding or modifying an existing evaluation criteria that has
        /// been setup with scoring.
        /// </summary>
        public static string EvaluationCriteriaScoringViolation
        {
            get { return $"You may not add or modify an evaluation criteria that has scoring setup."; }
        }
        /// <summary>
        /// Error message displayed when adding or modifying an existing evaluation criteria that has
        /// assignments released.
        /// </summary>
        public static string ReleasedAssignmentsForAwardsRuleViolation
        {
            get { return $"You may not add or modify an evaluation criteria that has assignments released."; }
        }
        /// <summary>
        /// Gets the released assignments for moving panel violation.
        /// </summary>
        /// <value>
        /// The released assignments for moving panel violation.
        /// </value>
        public static string ReleasedAssignmentsForPanelViolation
        {
            get { return $"You may not move panel to another session if application assignments have been released"; }
        }
        /// <summary>
        /// Error message displayed when moving a panel
        /// </summary>
        public static string ReviewsOrApplicationsAssignedToPanelViolation
        {
            get { return $"You may not remove a panel if reviewers or applications are assigned to the panel."; }
        }

        /// <summary>
        /// Success message upon editing reviewer descriptions
        /// </summary>
        public static string ReviewerDescriptionSaveSuccess
        {
            get { return $"Reviewer Description(s) saved."; }
        }
        /// <summary>
        /// Sucess message upon editing assigned summary statement templates
        /// </summary>
        public static string SelectedTemplateSaveSuccess
        {
            get { return $"Selected Template(s) have been saved."; }
        }

        /// <summary>
        /// Success message upon editing reviewer description and summary statements
        /// </summary>
        public static string ReviewerDescriptionAndTemplateSaveSuccess
        {
            get { return $"Selected Template(s) and/or Reviewer Descriptions saved."; }
        }
        #endregion
        #region Data Transfer        
        /// <summary>
        /// Gets the import log mesage.
        /// </summary>
        /// <value>
        /// The import log mesage.
        /// </value>
        public static string ImportLogMessage
        {
            get { return @"Processed: {0}, Imported: {1}, Unchanged: {2}, Failed: {3}."; }
        }
        /// <summary>
        /// Import failure title message.
        /// </summary>
        public static string ImportFailureTitle
        {
            get { return @"{0} applications were not imported due to errors. 
                Please correct the following error(s) and try again:"; }
        }
        /// <summary>
        /// Gets the message when there are no applications.
        /// </summary>
        /// <value>
        /// The NoApplications message
        /// </value>
        public static string NoApplications
        {
            get { return @"No applications available for import."; }
        }
        /// <summary>
        /// Gets the invalid XML.
        /// </summary>
        /// <value>
        /// The invalid XML.
        /// </value>
        public static string InvalidXml
        {
            get { return @"Invalid XML was detected."; }
        }
        /// <summary>
        /// Gets the failed to connect.
        /// </summary>
        /// <value>
        /// The failed to connect.
        /// </value>
        public static string FailedToConnect
        {
            get { return @"Failed to connect with the server."; }
        }
        /// <summary>
        /// Gets the program mechanism invalid change.
        /// </summary>
        /// <value>
        /// The program mechanism invalid change.
        /// </value>
        public static string ProgramMechanismInvalidChange
        {
            get { return @"Program Mechanism ID was changed after its assignments were released."; }
        }
        /// <summary>
        /// Gets the invalid start date.
        /// </summary>
        /// <value>
        /// The invalid start date.
        /// </value>
        public static string InvalidStartDate
        {
            get { return @"Invalid Start Date."; }
        }
        /// <summary>
        /// Gets the invalid end date.
        /// </summary>
        /// <value>
        /// The invalid end date.
        /// </value>
        public static string InvalidEndDate
        {
            get { return @"Invalid End Date."; }
        }
        /// <summary>
        /// Gets the invalid withdrawn date.
        /// </summary>
        /// <value>
        /// The invalid withdrawn date.
        /// </value>
        public static string InvalidWithdrawnDate
        {
            get { return @"Invalid withdrawn Date."; }
        }
        /// <summary>
        /// Gets the invalid program value.
        /// </summary>
        /// <value>
        /// The invalid program value.
        /// </value>
        public static string InvalidProgramValue
        {
            get { return @"Invalid Program."; }
        }
        /// <summary>
        /// Gets the invalid fy value.
        /// </summary>
        /// <value>
        /// The invalid fy value.
        /// </value>
        public static string InvalidFyValue
        {
            get { return @"Invalid FY."; }
        }
        /// <summary>
        /// Gets the invalid receipt cycle.
        /// </summary>
        /// <value>
        /// The invalid receipt cycle.
        /// </value>
        public static string InvalidReceiptCycle
        {
            get { return @"Invalid Receipt Cycle."; }
        }
        /// <summary>
        /// Gets the invalid compliance status.
        /// </summary>
        /// <value>
        /// The invalid compliance status.
        /// </value>
        public static string InvalidComplianceStatus
        {
            get { return @"Invalid Compliance Status."; }
        }
        /// <summary>
        /// Gets the type of the missing grant identifier.
        /// </summary>
        /// <value>
        /// The type of the missing grant identifier.
        /// </value>
        public static string MissingGrantIdType
        {
            get { return @"Missing ClientApplicationInfoType for GrantId"; }
        }
        /// <summary>
        /// Gets the invalid total funding value.
        /// </summary>
        /// <value>
        /// The invalid total funding value.
        /// </value>
        public static string InvalidTotalFundingValue
        {
            get { return @"Invalid TotalFunding value."; }
        }
        /// <summary>
        /// Gets the invalid requested direct value.
        /// </summary>
        /// <value>
        /// The invalid requested direct value.
        /// </value>
        public static string InvalidRequestedDirectValue
        {
            get { return @"Invalid RequestedDirect value."; }
        }
        /// <summary>
        /// Gets the invalid requested indirect value.
        /// </summary>
        /// <value>
        /// The invalid requested indirect value.
        /// </value>
        public static string InvalidRequestedIndirectValue
        {
            get { return @"Invalid RequestedIndirect value."; }
        }
        /// <summary>
        /// Gets the type of the could not find pi.
        /// </summary>
        /// <value>
        /// The type of the could not find pi.
        /// </value>
        public static string CouldNotMatchPiType
        {
            get { return @"Matching Principal Investigator personnel type record not found."; }
        }
        /// <summary>
        /// Gets the type of the could not find cr.
        /// </summary>
        /// <value>
        /// The type of the could not find cr.
        /// </value>
        public static string CouldNotMatchCrType
        {
            get { return @"Matching Contract Representative personnel type record not found."; }
        }
        /// <summary>
        /// Gets the type of the could not find mentor.
        /// </summary>
        /// <value>
        /// The type of the could not find mentor.
        /// </value>
        public static string CouldNotMatchMentorType
        {
            get { return @"Matching Mentor personnel type record not found."; }
        }
        /// <summary>
        /// Gets the type of the invalid coi.
        /// </summary>
        /// <value>
        /// The type of the invalid coi.
        /// </value>
        public static string InvalidCoiType
        {
            get { return @"Invalid COI type: {0}."; }
        }
        /// <summary>
        /// Gets the type of the could not find coi.
        /// </summary>
        /// <value>
        /// The type of the could not find coi.
        /// </value>
        public static string CouldNotMatchCoiType
        {
            get { return @"Matching COI type not found: {0}."; }
        }
        /// <summary>
        /// Gets the type of the missing coi.
        /// </summary>
        /// <value>
        /// The type of the missing coi.
        /// </value>
        public static string MissingCoiType
        {
            get { return @"Missing COI type."; }
        }
        /// <summary>
        /// Gets the unexpected exception message.
        /// </summary>
        /// <value>
        /// The unexpected exception message.
        /// </value>
        public static string UnexpectedExceptionMessage
        {
            get { return @"There was an unexpected exception: {0}"; }
        }
        #endregion
        #region Meeting Management Messages
        /// <summary>

        /// Generates a success message for a successful save
        /// </summary>
        /// <returns>Failure message</returns>
        public static string MeetingManagemntAttendanceDate()
        {
            return "Attendance End cannot be before Attendance Start";
        }
        /// <summary>
        /// Returns a generic retrieval failure message.
        /// </summary>
        /// <returns>Failure message</returns>
        public static string MeetingManagemntCheckinDate()
        {
            return "Check-out Date cannot be before Check-in Date";
        }
        /// <summary>
        /// Returns a generic retrieval failure message.
        /// </summary>
        /// <returns>Failure message</returns>
        public static string MeetingManagemntCommentCharacters()
        {
            return "Special Request must be less than 500 characters";
        }
        #endregion
        #region Security Policy Messages
        
        /// <summary>
        /// Policy Type Message.
        /// </summary>
        public static string PolicyTypeRequired
        {
            get
            {
                return @"Please select a Security Policy Type.";
            }
        }
        /// <summary>
        /// Policy Name Required Message.
        /// </summary>
        public static string PolicyNameRequired
        {
            get
            {
                return @"Please enter a Security Policy Name.";
            }
        }
        /// <summary>
        /// Policy Name Duplicate Message.
        /// </summary>
        public static string PolicyNameDuplicate
        {
            get
            {
                return @"Security Policy Name {0} already exists. Security Policy names must be unique. Please enter another Security Policy name.";
            }
        }
        /// <summary>
        /// Field contains special characters message.
        /// </summary>
        public static string SpecialCharactersMessage
        {
            get
            {
                return @"Special characters are not allowed.";
            }
        }
        /// <summary>
        /// Policy Start Date Required Message
        /// </summary>
        public static string PolicyStartDateRequired
        {
            get
            {
                return @"Please specify a Policy Start Date.";
            }
        }
        /// <summary>
        /// Invalid Policy Start Date 
        /// </summary>
        public static string PolicyStartDateInvalid
        {
            get
            {
                return @"Invalid Start Date.";
            }
        }
        /// <summary>
        /// Policy Start Time Required Message
        /// </summary>
        public static string PolicyStartTimeRequired
        {
            get
            {
                return @"Please select a Policy Start Time.";
            }
        }
        /// <summary>
        /// Incorrect policy time.
        /// </summary>
        public static string PolicyIncorrectTime
        {
            get
            {
                return @"Invalid time.";
            }
        }
        /// <summary>
        /// Policy End Time Required Message
        /// </summary>
        public static string PolicyEndTimeRequired
        {
            get
            {
                return @"Please select a Policy End Time.";
            }
        }
        /// <summary>
        /// Policy Start Time Required Message
        /// </summary>
        public static string PolicyStartDateGreaterThenEndDate
        {
            get
            {
                return @"Please have start date before end date.";
            }
        }
        /// <summary>
        /// Policy Network Range Required Message
        /// </summary>
        public static string PolicyNetworkRangeRequired
        {
            get
            {
                return @"At least one approved source network range must be provided.";
            }
        }
        /// <summary>
        /// Invalid Policy End Date 
        /// </summary>
        public static string PolicyEndDateInvalid
        {
            get
            {
                return @"Invalid End Date.";
            }
        }
        /// <summary>
        /// Restriction Hours Required Message
        /// </summary>
        public static string RestrictionHoursRequired
        {
            get
            {
                return @"Please select Hours of the Day to Apply Restriction.";
            }
        }

        /// <summary>
        /// Policy Days Required Message
        /// </summary>
        public static string PolicyDaysRequired
        {
            get
            {
                return @"Please select Days of the Week to Apply Restriction.";
            }
        }
        
        #endregion
        /// <summary>
        /// <summary>
        /// Retrieve one or more messages.
        /// </summary>
        /// <param name="errorMessages">Collection of SaveProfileStatus messages</param>
        /// <returns>List of string messages</returns>
        public static List<string> GetErrorMessages(ICollection<KeyValuePair<int, SaveAddressStatus>> errorMessages)
        {
            List<string> messages = new List<string>();

            foreach (var status in errorMessages)
            {
                string rowNumber = status.Key.ToString();

                switch (status.Value)
                {
                    case SaveAddressStatus.Address1NotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "W-9 Address1 is required"));
                            break;
                        }
                    case SaveAddressStatus.Address1TooLong:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "W-9 Address 1 exceeds max length"));
                            break;
                        }
                    case SaveAddressStatus.Address2TooLong:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "W-9 Address 2 exceeds max length"));
                            break;
                        }

                    case SaveAddressStatus.Address3TooLong:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "W-9 Address 3 exceeds max length"));
                            break;
                        }

                    case SaveAddressStatus.Address4TooLong:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "W-9 Address 4 exceeds max length"));
                            break;
                        }

                    case SaveAddressStatus.CityNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "City is required"));
                            break;
                        }

                    case SaveAddressStatus.CityTooLong:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "City exceeds max length"));
                            break;
                        }

                    case SaveAddressStatus.CountryCodeInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Invalid Country Id"));
                            break;
                        }

                    case SaveAddressStatus.CountryCodeNull:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Country Id is required"));
                            break;
                        }

                    case SaveAddressStatus.Default:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "All enums should have a default"));
                            break;
                        }

                    case SaveAddressStatus.ReviewerNameNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Reviewer Name is required"));
                            break;
                        }

                    case SaveAddressStatus.StateInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Invalid State"));
                            break;
                        }

                    case SaveAddressStatus.StateNull:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "State is required"));
                            break;
                        }
                    case SaveAddressStatus.Success:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Reviewer was successfully assigned"));
                            break;
                        }
                    case SaveAddressStatus.UserIdNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "User Id is required"));
                            break;
                        }
                    case SaveAddressStatus.UserIdInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Invalid User Id"));
                            break;
                        }
                    case SaveAddressStatus.VendorIdNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Vendor Id is required"));
                            break;
                        }
                    case SaveAddressStatus.VendorIdInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Invalid Vendor Id"));
                            break;
                        }
                    case SaveAddressStatus.InstVendorIdNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Inst Vendor Id is required"));
                            break;
                        }
                    case SaveAddressStatus.VendorIdCharacterInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Vendor Id contains a special character"));
                            break;
                        }
                    case SaveAddressStatus.VendorIdTooLong:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Vendor Id can be no longer than 10 characters"));
                            break;
                        }
                    case SaveAddressStatus.VendorNameNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Vendor name is required"));
                            break;
                        }
                    case SaveAddressStatus.VendorNameTooLong:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Vendor name exceeds max length"));
                            break;
                        }
                    case SaveAddressStatus.ZipNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Zip is required"));
                            break;
                        }
                    case SaveAddressStatus.ZipTooLong:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Zip exceeds max length"));
                            break;
                        }
                    case SaveAddressStatus.VendorIdDuplicate:
                        {
                            messages.Add(string.Format("Row #{0}: {1}", rowNumber, "Vendor Id is used already"));
                            break;
                        }
                    case SaveAddressStatus.UserIdNotVendorId:
                        {
                            messages.Add(string.Format("Row #{0}: {1}", rowNumber, "User ID and Vendor ID combination is invalid"));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            return messages;
        }
        /// <summary>
        /// Gets the error messages.
        /// </summary>
        /// <param name="errorMessages">The error messages.</param>
        /// <returns></returns>
        public static List<string> GetErrorMessages(ICollection<KeyValuePair<int, SaveFeeScheduleStatus>> errorMessages)
        {
            var messages = new List<string>();
            foreach (var status in errorMessages)
            {
                // Fee schedule Excel contains a header row
                string rowNumber = (status.Key + 1).ToString();
                switch (status.Value)
                {
                    case SaveFeeScheduleStatus.ParticipationTypeNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Participation type is required."));
                            break;
                        }
                    case SaveFeeScheduleStatus.ParticipationTypeInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Invalid participation type."));
                            break;
                        }
                    case SaveFeeScheduleStatus.ParticipantMethodNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Participant method is required."));
                            break;
                        }
                    case SaveFeeScheduleStatus.ParticipantMethodInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Invalid participant method."));
                            break;
                        }
                    case SaveFeeScheduleStatus.ParticipantLevelNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Participant level is required."));
                            break;
                        }
                    case SaveFeeScheduleStatus.ParticipantLevelInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Invalid participant level."));
                            break;
                        }
                    case SaveFeeScheduleStatus.EmploymentCategoryNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Employment Category is required."));
                            break;
                        }
                    case SaveFeeScheduleStatus.EmploymentCategoryInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Invalid Employment Category."));
                            break;
                        }
                    case SaveFeeScheduleStatus.ConsultantTextNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Consultant Text is required."));
                            break;
                        }
                    case SaveFeeScheduleStatus.ConsultantTextInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Consultant Text value exceeds the max limit of 200 characters."));
                            break;
                        }
                    case SaveFeeScheduleStatus.ConsultantFeeNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Consultant Fee is required."));
                            break;
                        }
                    case SaveFeeScheduleStatus.ConsultantFeeInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Invalid Consultant Fee. Must be a numeric value."));
                            break;
                        }
                    case SaveFeeScheduleStatus.StartDateNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Start Date is required."));
                            break;
                        }
                    case SaveFeeScheduleStatus.StartDateInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Invalid Start Date. Must be MM/DD/YYYY."));
                            break;
                        }
                    case SaveFeeScheduleStatus.EndDateNotSupplied:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "End Date is required."));
                            break;
                        }
                    case SaveFeeScheduleStatus.EndDateInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Invalid End Date. Must be MM/DD/YYYY."));
                            break;
                        }
                    case SaveFeeScheduleStatus.DateRangeInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "End Date cannot be before Start Date."));
                            break;
                        }
                    case SaveFeeScheduleStatus.ManagerListInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "SRA Managers value exceeds max limit of 500 characters."));
                            break;
                        }
                    case SaveFeeScheduleStatus.WorkDescriptionInvalid:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Work Description exceeds max limit of 8000 characters."));
                            break;
                        }
                    case SaveFeeScheduleStatus.AlreadyExists:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Participation type, method, level and employment category combination must be unique."));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            return messages;
        }
        /// <summary>
        /// Gets the error messages.
        /// </summary>
        /// <param name="errorMessages">The error messages.</param>
        /// <returns></returns>
        public static List<string> GetErrorMessages(ICollection<KeyValuePair<int, SaveTravelFlightStatus>> errorMessages)
        {
            var messages = new List<string>();
            foreach (var status in errorMessages)
            {
                string rowNumber = status.Key.ToString();
                switch (status.Value)
                {
                    case SaveTravelFlightStatus.InvalidPanelUserAssignmentId:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Panel User Assignment ID is invalid."));
                            break;





                        }

                    case SaveTravelFlightStatus.InvalidDepartureDate:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Departure date or time is invalid."));
                            break;
                        }
                    case SaveTravelFlightStatus.InvalidArrivalDate:
                        {
                            messages.Add(String.Format("Row #{0}: {1}", rowNumber, "Arrival date or time is invalid."));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            return messages;
        }
    }
}
