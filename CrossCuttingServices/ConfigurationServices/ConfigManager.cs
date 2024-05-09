using System;
using System.Configuration;
using System.Globalization;

namespace Sra.P2rmis.CrossCuttingServices.ConfigurationServices
{
    /// <summary>
    /// Provides uniform access to configured items
    /// </summary>
    public class ConfigManager
    {
        /// <summary>
        /// Configuration value of the SQL Report server
        /// </summary>
        public static string ReportServerUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["url-reportServer"].ToString();
            }
        }
        /// <summary>
        /// Configuration value of the SQL Report report location
        /// </summary>
        public static string ReportPath
        {
            get
            {
                return ConfigurationManager.AppSettings["url-reportPath"].ToString();
            }
        }
        /// <summary>
        /// Configuration value of the SQL Report storage root UNC path
        /// </summary>
        public static string ReportStorageRoot
        {
            get
            {
                return ConfigurationManager.AppSettings["ReportStorageRoot"].ToString();
            }
        }
        /// <summary>
        /// Gets the egs storage root.
        /// </summary>
        /// <value>
        /// The egs storage root.
        /// </value>
        public static string EgsStorageRoot
        {
            get
            {
                return ConfigurationManager.AppSettings["EgsStorageRoot"].ToString();
            }
        }
        /// <summary>
        /// Gets the egs data transfer URI.
        /// </summary>
        /// <value>
        /// The egs data transfer URI.
        /// </value>
        public static string EgsDataTransferUri
        {
            get { return (ConfigurationManager.AppSettings["EgsDataTransferUri"]); }
        }
        /// <summary>
        /// Gets the egs data transfer key.
        /// </summary>
        /// <value>
        /// The egs data transfer key.
        /// </value>
        public static string EgsDataTransferKey
        {
            get { return (ConfigurationManager.AppSettings["EgsDataTransferKey"]); }
        }
        /// <summary>
        /// Gets the AssignmentTypeThreshold max value.
        /// </summary>
        /// <value>
        /// The AssignmentTypeThreshold max
        /// </value>
        public static string AssignmentTypeThresholdMax
        {
            get
            {
                return ConfigurationManager.AppSettings["AssignmentTypeThresholdMax"].ToString();
            }
        }
        /// <summary>
        /// Configuration value of the system email address.
        /// </summary>
        public static string SystemEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["system-email-address"].ToString();
            }
        }/// <summary>
        /// Configuration value of the help desk email address.
        /// </summary>
        public static string HelpDeskEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["helpdesk-email-address"].ToString();
            }
        }
        /// <summary>
        /// Configuration value of the help desk phone number
        /// </summary>
        public static string HelpDeskPhoneNumber
        {
            get
            {
                return ConfigurationManager.AppSettings["helpdesk-phone-number"].ToString();
            }
        }
        /// <summary>
        /// Configuration value of the help desk hours
        /// </summary>
        public static string HelpDeskHours
        {
            get
            {
                return ConfigurationManager.AppSettings["helpdesk-hours"].ToString();
            }
        }
        /// <summary>
        /// Configuration value of the help desk hours with formatted time
        /// </summary>
        public static string HelpDeskHoursStandard
        {
            get
            {
                return ConfigurationManager.AppSettings["helpdesk-hours-standard"].ToString();
            }
        }
        /// <summary>
        /// Configuration value for the maximum number of presenters
        /// </summary>
        public static int PresentationOrderMaximum
        {
            get
            {
                int result = 20;
                try
                {
                    result = Convert.ToInt32(ConfigurationManager.AppSettings["PresentationOrderMaximum"].ToString());
                }
                catch { }

                return result;
            }
        }
        /// <summary>
        /// Configuration value for the resume version number
        /// </summary>
        public static string UserResumeVersionFormatSpecification
        {
            get
            {
                return ConfigurationManager.AppSettings["user-resume-version-format-Specification"].ToString();
            }

        }
        /// <summary>
        /// Configured value for resume (MB)
        /// </summary>
        public static long UserResumeMaximuSize
        {
            get
            {
                return Convert.ToInt64(ConfigurationManager.AppSettings["user-resume-maximum-length"].ToString());
            }
        }

        /// <summary>
        /// Configuration value for the Url Host
        /// </summary>
        public static string UrlHost
        {
            get
            {
                return ConfigurationManager.AppSettings["url-host"].ToString();
            }

        }
        /// <summary>
        /// Configuration value for the Url scheme
        /// </summary>
        public static string UrlScheme
        {
            get
            {
                return ConfigurationManager.AppSettings["url-scheme"].ToString();
            }

        }
        /// <summary>
        /// Configuration value for the Url port number
        /// </summary>
        public static string UrlPort
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["url-Port"]);
            }

        }
        /// <summary>
        /// Configuration value specifying the minimum password length
        /// </summary>
        public static int PwdMinLength
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["password-min-length"].ToString());
            }
        }
        /// <summary>
        /// Configuration value specifying the minimum password length text
        /// </summary>
        public static string PwdMinLengthText
        {
            get
            {
                return ConfigurationManager.AppSettings["password-min-length-text"].ToString();
            }
        }
        /// <summary>
        /// Configuration value specifying the maximum password length
        /// </summary>
        public static int PwdMaxLength
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["password-max-length"].ToString());
            }
        }
        /// <summary>
        /// Configuration value specifying the maximum password length text
        /// </summary>
        public static string PwdMaxLengthText
        {
            get
            {
                return ConfigurationManager.AppSettings["password-max-length-text"].ToString();
            }
        }
        /// <summary>
        /// Configuration value specifying the number of non alpha-numeric characters in a generated password
        /// </summary>
        public static int PwdNumberNonAlphanumericCharactersInGeneratedPassword
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["password-non-alpha-numeric"].ToString());
            }
        }
        /// <summary>
        /// Configuration value specifying the number of repeating consecutive characters in a password
        /// </summary>
        public static int PwdNumberRepeatingCharacters
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["password-repeating-characters"].ToString());
            }
        }
        /// <summary>
        /// Configuration value specifying the number of previous passwords to validate against
        /// </summary>
        public static int PwdNumberPrevious
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["password-previous"].ToString());
            }
        }
        /// <summary>
        /// Configuration value specifying the datetime of the next password policy release
        /// </summary>
        public static DateTime PwdPolicyReleaseDate
        {
            get
            {
                //return Convert.ToDateTime(ConfigurationManager.AppSettings["PasswordPolicyReleaseDate"].ToString(),"MM/dd/yyyy");
                return DateTime.ParseExact(ConfigurationManager.AppSettings["PasswordPolicyReleaseDate"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
        }
        /// <summary>
        /// Configuration value specifying the datetime of the initial password expiration date, which represents the start date of password expiration policy enforcement for all users
        /// </summary>
        public static DateTime PwdInitialExpirationDate
        {
            get
            {
                return Convert.ToDateTime(ConfigurationManager.AppSettings["InitialPasswordExpirationDate"].ToString());
            }
        }
        /// <summary>
        /// Configuration value specifying the number of days before a permanent password expires
        /// </summary>
        public static int PwdNumberDaysBeforeExpire
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["NoDaysBeforeExpire"].ToString());
            }
        }
        /// <summary>
        /// Configuration value specifying number of hours user is locked out for
        /// </summary>
        public static int LockedOutForInHours
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["NoHoursLocked"].ToString());
            }
        }
        /// <summary>
        /// Configuration value specifying the number of days before a the user receives notice of password expiration (inclusive)
        /// </summary>
        public static int PwdNumberDaysBeforeNotice
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["NoDaysBeforeNotice"].ToString());
            }
        }
        /// <summary>
        /// Configuration value specifying the number of days before an invitation expires
        /// </summary>
        public static int PwdNumberDaysBeforeResetExpire
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["NoDaysBeforeResetExpire"].ToString());
            }
        }
        /// <summary>
        /// Configuration value specifying max number of failed login attempts
        /// </summary>
        public static int MaxNumberFailedAttempts
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["NoFailedAttempts"].ToString());
            }
        }
        /// <summary>
        /// Configuration value for the maximum length of in line comments.
        /// </summary>
        public static int CommentInlineMaximum
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["CommentInlineMaximum"].ToString());

            }
        }
        /// <summary>
        /// Configuration value for the maximum length of in general comments.
        /// </summary>
        public static int CommentGeneralMaximum
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["CommentGeneralMaximum"].ToString());

            }
        }
        /// <summary>
        /// Configuration value for the user to authenticate reporting services
        /// </summary>
        public static string ReportUser
        {
            get { return (ConfigurationManager.AppSettings["MyReportViewerUser"]); }
        }
        /// <summary>
        /// Configuration value for the password to authenticate reporting services
        /// </summary>
        public static string ReportPassword
        {
            get { return (ConfigurationManager.AppSettings["MyReportViewerPassword"]); }
        }
        /// <summary>
        /// Configuration value for the domain to authenticate reporting services
        /// </summary>
        public static string ReportDomain
        {
            get { return (ConfigurationManager.AppSettings["MyReportViewerDomain"]); }
        }
        /// <summary>
        /// Configuration value for the url to download the W9 form from
        /// </summary>
        public static string W9FormDownload
        {
            get { return (ConfigurationManager.AppSettings["W9FormDownload"]); }
        }
        /// <summary>
        /// Configuration value for the url to download the W8 form from
        /// </summary>
        public static string W8FormDownload
        {
            get { return (ConfigurationManager.AppSettings["W8FormDownload"]); }
        }
        /// <summary>
        /// Configuration value for the W9 FAX number
        /// </summary>
        public static string W9FormFax
        {
            get { return (ConfigurationManager.AppSettings["W9FormFax"]); }
        }
        /// <summary>
        /// Configuration value for the Blinded replacement string
        /// </summary>
        public static string BlindedReplacementString
        {
            get { return (ConfigurationManager.AppSettings["Blinded"]); }
        }
        /// <summary>
        /// Configuration value for the Company Name replacement string
        /// </summary>
        public static string CompanyName
        {
            get { return (ConfigurationManager.AppSettings["CompanyName"]); }
        }
        /// <summary>
        /// Configuration value for the Company Division replacement string
        /// </summary>
        public static string CompanyDivision
        {
            get { return (ConfigurationManager.AppSettings["CompanyDivision"]); }
        }
        /// <summary>
        /// Configuration value for the CompanyAddress1 replacement string
        /// </summary>
        public static string CompanyAddress1
        {
            get { return (ConfigurationManager.AppSettings["CompanyAddress1"]); }
        }
        /// <summary>
        /// Configuration value for the CompanyAddress2 replacement string
        /// </summary>
        public static string CompanyAddress2
        {
            get { return (ConfigurationManager.AppSettings["CompanyAddress2"]); }
        }
        /// <summary>
        /// Configuration value for the Company City replacement string
        /// </summary>
        public static string CompanyCity
        {
            get { return (ConfigurationManager.AppSettings["CompanyCity"]); }
        }
        /// <summary>
        /// Configuration value for the Company Phone replacement string
        /// </summary>
        public static string CompanyPhone
        {
            get { return (ConfigurationManager.AppSettings["CompanyPhone"]); }
        }
        #region Reviewer Assignment Configuration 
        /// <summary>
        /// Configuration value limiting the span of PanelUserAssignment and
        /// PanelUserPotentialAssignment entities.
        /// </summary>
        public static int PanelManagementAssignmentRetrievalLimit

        {
            get
            {
                int result = 3;
                try
                {
                    result = Convert.ToInt32(ConfigurationManager.AppSettings["PanelManagementAssignmentRetrievalLimit"].ToString());
                }
                catch { }

                return result;
            }
        }
        /// <summary>
        /// Configuration value limiting the span of PanelUserAssignment and
        /// PanelUserPotentialAssignment entities.
        /// </summary>
        public static int PanelManagementPotentialAssignmentRetrievalLimit
        {
            get
            {
                int result = 1;
                try
                {
                    result = Convert.ToInt32(ConfigurationManager.AppSettings["PanelManagementPotentialAssignmentRetrievalLimit"].ToString());
                }
                catch { }

                return result;
            }
        }
        #endregion
        #region Reviewer Search Configuration

        /// <summary>
        /// Gets the panel management reviewer search limit.
        /// </summary>
        /// <value>
        /// The maximum number of results to return in the Panel Management Reviewer Search.
        /// </value>
        public static int PanelManagementReviewerSearchLimit
        {
            get
            {
                int result = 100;
                try
                {
                    result = Convert.ToInt32(ConfigurationManager.AppSettings["PanelManagementReviewerSearchLimit"]);
                }
                catch { }
                return result;
            }
        }
        /// <summary>
        /// Gets the panel management staff search limit.
        /// </summary>
        /// <value>
        /// The panel management staff search limit.
        /// </value>
        public static int PanelManagementStaffSearchLimit
        {
            get
            {
                int result = 100;
                try
                {
                    result = Convert.ToInt32(ConfigurationManager.AppSettings["PanelManagementStaffSearchLimit"]);
                }
                catch { }
                return result;
            }
        }
        #endregion
        #region Legacy Authentication
        /// <summary>
        /// Configured value for the Legacy authentication (PREMIS Ver. 1) server.
        /// </summary>
        public static string LegacyAuthenticationServer
        {
            get { return ConfigurationManager.AppSettings["LegacyAuthenticationServer"]; }
        }
        #endregion
        /// <summary>
        /// Configuration value for MyWorkspace Scorable Application polling interval.
        /// </summary>
        public static int MyWorkspaceScorableApplicationPollingInterval
        {
            get
            {
                int result = 30000;
                try
                {
                    result = Convert.ToInt32(ConfigurationManager.AppSettings["MyWorkspaceScorableApplicationPollingInterval"].ToString());
                }
                catch { }

                return result;
            }
        }
        /// <summary>
        /// Configuration value for MyWorkspace Scorable Application cache duration in seconds.
        /// </summary>
        public static int MyWorkspaceScorableApplicationCacheDuration
        {
            get
            {
                int result = 10;
                try
                {
                    result = Convert.ToInt32(ConfigurationManager.AppSettings["MyWorkspaceScorableApplicationCacheDuration"].ToString());
                }
                catch { }

                return result;
            }
        }
        /// <summary>
        /// Configuration value for the ManageApplicationScoring polling interval.
        /// </summary>
        public static int ManageApplicationScoringPollingInterval
        {
            get
            {
                int result = 30000;
                try
                {
                    result = Convert.ToInt32(ConfigurationManager.AppSettings["ManageApplicationScoringPollingInterval"].ToString());
                }
                catch { }

                return result;
            }
        }
        /// <summary>
        /// Configuration value for P2RMIS v.1 server.
        /// </summary>
        public static string P2RMISv1Server
        {
            get { return (ConfigurationManager.AppSettings["url-P2RMISv1Server"]); }
        }
        /// <summary>
        /// Gets the spell check settings dictionary path.
        /// </summary>
        /// <value>
        /// The spell check settings dictionary path.
        /// </value>
        public static string SpellCheckSettingsDictionaryPath
        {
            get { return (ConfigurationManager.AppSettings["SpellCheckSettingsDictionaryPath"]); }
        }
        /// <summary>
        /// Gets the document directory path.
        /// </summary>
        /// <value>
        /// The document directory path.
        /// </value>
        public static string DocumentDirectoryPath
        {
            get { return (ConfigurationManager.AppSettings["DocumentDirectoryPath"]); }
        }
        #region Jira Configuration

        /// <summary>
        /// Gets the name of the jira user.
        /// </summary>
        /// <value>
        /// The name of the jira user.
        /// </value>
        public static string JiraUserName => ConfigurationManager.AppSettings["JiraUserName"];

        /// <summary>
        /// Gets the jira password.
        /// </summary>
        /// <value>
        /// The jira password.
        /// </value>
        public static string JiraPassword => ConfigurationManager.AppSettings["JiraPassword"];

        /// <summary>
        /// Gets the jira URL.
        /// </summary>
        /// <value>
        /// The jira URL.
        /// </value>
        public static string JiraUrl => ConfigurationManager.AppSettings["JiraUrl"];

        /// <summary>
        /// Gets the jira port.
        /// </summary>
        /// <value>
        /// The jira port.
        /// </value>
        public static string JiraPort => ConfigurationManager.AppSettings["JiraPort"];

        /// <summary>
        /// Gets the jira default assignee.
        /// </summary>
        /// <value>
        /// The jira default assignee.
        /// </value>
        public static string JiraDefaultAssignee => ConfigurationManager.AppSettings["JiraDefaultAssignee"];

        /// <summary>
        /// Gets the jira project identifier.
        /// </summary>
        /// <value>
        /// The jira project identifier.
        /// </value>
        public static string JiraProjectId => ConfigurationManager.AppSettings["JiraProjectId"];

        /// <summary>
        /// Gets the jira metadata URL.
        /// </summary>
        /// <value>
        /// The jira metadata URL.
        /// </value>
        public static string JiraMetadataUrl => ConfigurationManager.AppSettings["JiraMetadataUrl"];

        #endregion
        /// <summary>
        /// Configuration value for the +/- range for the program setup physical year drop down
        /// </summary>
        public static int SetupPhysicalYearRange
        {
            get {
                int defaultSetupPhysicalYearRange = 3;

                return RetrieveKeyAsIntegerWithDefault(defaultSetupPhysicalYearRange, "SetupPhysicalYearRange");
                } 

        }
        /// <summary>
        /// Retrieves the specified AppSetting from the config file and converts it to 
        /// an integer.  If the conversion fails the default value is returned.
        /// </summary>
        /// <param name="defaultValue">Default value</param>
        /// <param name="key">AppSettings key</param>
        /// <returns></returns>
        private static int RetrieveKeyAsIntegerWithDefault(int defaultValue, string key)
        {
            int result = defaultValue;
            try
            {
                result = Convert.ToInt32(ConfigurationManager.AppSettings[key].ToString());
            }
            catch { }

            return result;
        }
        /// <summary>
        /// Gets the PreAward marker.
        /// </summary>
        public static string PreAwardMarker => ConfigurationManager.AppSettings["PreAwardMarker"];
        #region Summary Statements
        /// <summary>
        /// Gets the full template path.
        /// </summary>
        /// <param name="templateName">The template relative location.</param>
        /// <returns>Full path of the file for retrieval</returns>
        public static string GetTemplateFullPath(string templateName)
        {
            return $"{ReportStorageRoot}{templateName}";
        }
        #endregion
        #region Web Services        
        /// <summary>
        /// Gets the basic authentication username.
        /// </summary>
        /// <value>
        /// The basic authentication username.
        /// </value>
        public static string BasicAuthenticationUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["BasicAuthenticationUserName"];
            }
        }
        /// <summary>
        /// Gets the basic authentication password.
        /// </summary>
        /// <value>
        /// The basic authentication password.
        /// </value>
        public static string BasicAuthenticationPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["BasicAuthenticationPassword"];
            }
        }
        /// <summary>
        /// Adopts pre-defined security protocol
        /// </summary>
        public static bool AdoptPredefinedSecurityProtocol
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["AdoptPredefinedSecurityProtocol"]);
            }
        }
        /// <summary>
        /// Proxy URL/Port for outgoing traffic
        /// </summary>
        public static string OutgoingProxy
        {
            get
            {
                return ConfigurationManager.AppSettings["OutgoingProxy"];
            }
        }
        #endregion
        #region AWS
        /// <summary>
        /// Gets the name of the s3 bucket.
        /// </summary>
        /// <value>
        /// The name of the s3 bucket.
        /// </value>
        public static string S3BucketName => ConfigurationManager.AppSettings["S3BucketName"];
        /// <summary>
        /// Gets the name of the s3 application folder.
        /// </summary>
        /// <value>
        /// The name of the s3 application folder.
        /// </value>
        public static string S3AppFolderName => ConfigurationManager.AppSettings["S3AppFolderName"];
        /// <summary>
        /// Gets the name of the base s3 folder where contract are stored.
        /// </summary>
        /// <value>
        /// The name of the s3 contract folder.
        /// </value>
        public static string S3ContractFolderName => ConfigurationManager.AppSettings["S3ContractFolderName"];
        #endregion

        /// <summary>
        /// Configuration value to logout after minutes of inactivity
        /// </summary>
        public static int AutoLogoutAfter
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["AutoLogoutAfter"].ToString());
            }
        }

        /// <summary>
        /// Configuration value for the maximum number of nav menu items displayed
        /// </summary>
        public static int NavMenuItemsMaxDisplay
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["NavMenuItemsMaxDisplay"].ToString());
            }
        }

        /// <summary>
        /// Configuration value for the maximum number of mask bits
        /// </summary>
        public static int MaxMaskBits
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["MaxMaskBits"].ToString());
            }
        }
    }
}
