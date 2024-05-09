using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

[assembly: InternalsVisibleTo("Sra.P2rmis.CrossCuttingServicesTest")]

namespace Sra.P2rmis.CrossCuttingServices
{
    /// <summary>
    /// Custom attribute to decorate properties in MergeField class
    /// </summary>
    
    [System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class MergeFieldTagAttribute : System.Attribute
    {
        private string _tag;
        /// <summary>
        /// Constant character indicating the beginning of a named merge field
        /// </summary>
        public const string MergeFieldOpeningDelimeter = "{^";
        /// <summary>
        /// Constant character indicating the end of a named merge field
        /// </summary>
        public const string MergeFieldClosingDelimiter = "^}";

        /// <summary>
        /// Constructor for the attribute
        /// </summary>
        /// <param name="tag">The text found in an email template that will be replaced in a merge</param>
        public MergeFieldTagAttribute(string tag)
        {
            _tag = tag;
        }

        /// <summary>
        /// The text found in an email template that will be replaced in a merge
        /// </summary>
        public string Tag
        {
            get { return MergeFieldOpeningDelimeter + _tag + MergeFieldClosingDelimiter; }
        }
    }

    /// <summary>
    /// An exception indicating the presence of an unrecognized merge field.  May occur with older templates that contain deprecated merge fields.
    /// </summary>
    [Serializable]
    public class UnknownMergeFieldException : System.Exception
    {
        private string[] _unknownFields;

        /// <summary>
        /// Contains a message about the unknown merge fields
        /// </summary>
        public override string Message
        {
            get
            {
                StringBuilder sb = new StringBuilder("The following merge fields are unknwon and did not have values substituted for them: ");
                foreach (string fld in _unknownFields)
                {
                    sb.Append(fld + ", ");
                }
                return sb.ToString(0, sb.Length - 2);  // removes final comma
            }
        }

        #region Constructors
        /// <summary>
        /// Constructor for the exception
        /// </summary>
        /// <param name="unknownFields">An array of unknown merge field names</param>
        public UnknownMergeFieldException(string[] unknownFields)
        {
            _unknownFields = new string[unknownFields.Count()];
            unknownFields.CopyTo(_unknownFields, 0);
        }

        /// <summary>
        /// Standard constructor for Exception classes (deprecated).  Use constructor UnknownMergeFieldException(string[]) instead.
        /// </summary>
        public UnknownMergeFieldException() : base() { }

        /// <summary>
        /// Standard constructor for Exception classes (deprecated).  Use constructor UnknownMergeFieldException(string[]) instead.
        /// </summary>
        /// <param name="message"></param>
        public UnknownMergeFieldException(string message) : base(message) { }

        /// <summary>
        /// Standard constructor for Exception classes (deprecated).  Use constructor UnknownMergeFieldException(string[]) instead.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public UnknownMergeFieldException(string message, Exception e) : base(message, e) { }

        /// <summary>
        /// Standard constructor for Exception classes (deprecated).  Use constructor UnknownMergeFieldException(string[]) instead.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="sc"></param>
        protected UnknownMergeFieldException(SerializationInfo info, StreamingContext sc) : base(info, sc) { }
        #endregion

        /// <summary>
        /// Required for class that is Serializable
        /// </summary>
        /// <param name="info">Serialization information for this class</param>
        /// <param name="context">Contains contextual information about the source or destination</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }

    /// <summary>
    /// This entity class holds the data that will replace the merge field tags in an email template
    /// </summary>
    public class MergeField
    {
        #region Private Members
        private string _toPrefix = string.Empty;
        private string _toFirstName = string.Empty;
        private string _toLastName = string.Empty;
        private string _toSuffix = string.Empty;
        private string _toFullName = string.Empty;
        private string _toEmailAddress = string.Empty;
        private string _toFullMailingAddress = string.Empty;
        private string _toRole = string.Empty;
        private string _fromPrefix = string.Empty;
        private string _fromFirstName = string.Empty;
        private string _fromLastName = string.Empty;
        private string _fromSuffix = string.Empty;
        private string _fromFullName = string.Empty;
        private string _fromEmailAddress = string.Empty;
        private string _fromFullMailingAddress = string.Empty;
        private string _fromRole = string.Empty;
        private string _panelName = string.Empty;
        private string _panelAbbrev = string.Empty;
        private string _roleOnPanel = string.Empty;
        private string _applicationTitle = string.Empty;
        private string _critiqueDeadline = string.Empty;
        private string _meetingNameAndDate = string.Empty;
        private string _temporarypassword = string.Empty;
        private string _userlogin = string.Empty;
        private string _hostname = string.Empty;
        private string _participantType = string.Empty;
        private string _fy = string.Empty;
        private string _programName = string.Empty;
        private string _companyName = string.Empty;
        private string _companyDivision = string.Empty;
        private string _companyAddress1 = string.Empty;
        private string _companyAddress2 = string.Empty;
        private string _companyCity = string.Empty;
        private string _companyPhone = string.Empty;
        private string _companyFax = string.Empty;
        private string _currentDateTime = string.Empty;
        private string _helpDeskEmail = string.Empty;
        private string _helpDeskPhone = string.Empty;
        private string _helpDeskHours = string.Empty;
        private string _passwordExpirationDate = string.Empty;
        #endregion

        #region Public Properties
        /// <summary>
        /// Contains the value for the "to-prefix" merge field
        /// </summary>
        [MergeFieldTag("to-prefix")]
        public string ToPrefix { get { return _toPrefix;} set {_toPrefix = value;}}

        /// <summary>
        /// Contains the value for the "to-first-name" merge field
        /// </summary>
        [MergeFieldTag("to-first-name")]
        public string ToFirstName { get { return _toFirstName; } set { _toFirstName= value; } }

        /// <summary>
        /// Contains the value for the "to-last-name" merge field
        /// </summary>
        [MergeFieldTag("to-last-name")]
        public string ToLastName { get { return _toLastName; } set { _toLastName = value; } }

        /// <summary>
        /// Contains the value for the "to-suffix" merge field
        /// </summary>
        [MergeFieldTag("to-suffix")]
        public string ToSuffix { get { return _toSuffix; } set { _toSuffix= value; } }

        /// <summary>
        /// Contains the value for the "to-full-name" merge field
        /// </summary>
        [MergeFieldTag("to-full-name")]
        public string ToFullName { get { return _toFullName; } set { _toFullName = value; } }

        /// <summary>
        /// Contains the value for the "to-email-address" merge field
        /// </summary>
        [MergeFieldTag("to-email-address")]
        public string ToEmailAddress { get { return _toEmailAddress; } set { _toEmailAddress = value; } }

        /// <summary>
        /// Contains the value for the "to-full-mailing-address" merge field
        /// </summary>
        [MergeFieldTag("to-full-mailing-address")]
        public string ToFullMailingAddress { get { return _toFullMailingAddress; } set { _toFullMailingAddress = value; } }

        /// <summary>
        /// Contains the value for the "to-role" merge field
        /// </summary>
        [MergeFieldTag("to-role")]
        public string ToRole { get { return _toRole; } set { _toRole = value; } }

        /// <summary>
        /// Contains the value for the "from-prefix" merge field
        /// </summary>
        [MergeFieldTag("from-prefix")]
        public string FromPrefix { get { return _fromPrefix; } set { _fromPrefix = value; } }

        /// <summary>
        /// Contains the value for the "from-first-name" merge field
        /// </summary>
        [MergeFieldTag("from-first-name")]
        public string FromFirstName { get { return _fromFirstName; } set { _fromFirstName= value; } }

        /// <summary>
        /// Contains the value for the "from-last-name" merge field
        /// </summary>
        [MergeFieldTag("from-last-name")]
        public string FromLastName { get { return _fromLastName; } set { _fromLastName = value; } }

        /// <summary>
        /// Contains the value for the "from-suffix" merge field
        /// </summary>
        [MergeFieldTag("from-suffix")]
        public string FromSuffix { get { return _fromSuffix; } set { _fromSuffix= value; } }

        /// <summary>
        /// Contains the value for the "from-full-name" merge field
        /// </summary>
        [MergeFieldTag("from-full-name")]
        public string FromFullName { get { return _fromFullName; } set { _fromFullName = value; } }

        /// <summary>
        /// Contains the value for the "from-email-address" merge field
        /// </summary>
        [MergeFieldTag("from-email-address")]
        public string FromEmailAddress { get { return _fromEmailAddress; } set { _fromEmailAddress= value; } }

        /// <summary>
        /// Contains the value for the "from-full-mailing-address" merge field
        /// </summary>
        [MergeFieldTag("from-full-mailing-address")]
        public string FromFullMailingAddress { get { return _fromFullMailingAddress; } set { _fromFullMailingAddress= value; } }

        /// <summary>
        /// Contains the value for the "from-role" merge field
        /// </summary>
        [MergeFieldTag("from-role")]
        public string FromRole { get { return _fromRole; } set { _fromRole= value; } }

        /// <summary>
        /// Contains the value for the "panel-name" merge field
        /// </summary>
        [MergeFieldTag("panel-name")]
        public string PanelName { get { return _panelName; } set { _panelName= value; } }
        /// <summary>
        /// Contains the value for the "panel-abbrev" merge field
        /// </summary>
        [MergeFieldTag("panel-abbrev")]
        public string PanelAbbrev { get { return _panelAbbrev; } set { _panelAbbrev = value; } }
        /// <summary>
        /// Contains the value for the "role-on-panel" merge field
        /// </summary>
        [MergeFieldTag("role-on-panel")]
        public string RoleOnPanel { get { return _roleOnPanel; } set { _roleOnPanel = value; } }

        /// <summary>
        /// Contains the value for the "application-title" merge field
        /// </summary>
        [MergeFieldTag("application-title")]
        public string ApplicationTitle { get { return _applicationTitle; } set { _applicationTitle = value; } }

        /// <summary>
        /// Contains the value for the "critique-deadline" merge field
        /// </summary>
        [MergeFieldTag("critique-deadline")]
        public string CritiqueDeadline { get { return _critiqueDeadline; } set { _critiqueDeadline = value; } }

        /// <summary>
        /// Contains the value for the "meeting-name-and-date" merge field
        /// </summary>
        [MergeFieldTag("meeting-name-and-date")]
        public string MeetingNameAndDate { get { return _meetingNameAndDate; } set { _meetingNameAndDate = value; } }

        /// <summary>
        /// Contains the value for the "temporary-password" merge field
        /// </summary>
        [MergeFieldTag("temporary-password")]
        public string TemporaryPassword { get { return _temporarypassword; } set { _temporarypassword = value; } }

        /// <summary>
        /// Contains the value for the "UserLogin" merge field
        /// </summary>
        [MergeFieldTag("UserLogin")]
        public string UserLogin { get { return _userlogin; } set { _userlogin = value; } }

        /// <summary>
        /// Contains the value for the "hostname" merge field
        /// </summary>
        [MergeFieldTag("hostname")]
        public string hostname { get { return _hostname; } set { _hostname = value; } }
        /// <summary>
        /// Contains the value for the "ParticipantType" merge field
        /// </summary>
        [MergeFieldTag("ParticipantType")]
        public string ParticipantType { get { return _participantType; } set { _participantType = value; } }
        /// <summary>
        /// Contains the value for the "ParticipantType" merge field
        /// </summary>
        [MergeFieldTag("FY")]
        public string FY { get { return _fy; } set { _fy = value; } }
        /// <summary>
        /// Contains the value for the "ProgramName" merge field
        /// </summary>      
        [MergeFieldTag("ProgramName")]
        public string ProgramName { get { return _programName; } set { _programName = value; } }


        /// <summary>
        /// Contains the value for the "CompanyName" merge field
        /// </summary>      
        [MergeFieldTag("CompanyName")]
        public string CompanyName { get { return _companyName; } set { _companyName = value; } }
        /// <summary>
        /// Contains the value for the "ProgramName" merge field
        /// </summary>      
        [MergeFieldTag("CompanyDivision")]
        public string CompanyDivision { get { return _companyDivision; } set { _companyDivision = value; } }
        /// <summary>
        /// Contains the value for the "CompanyAddress1" merge field
        /// </summary>      
        [MergeFieldTag("CompanyAddress1")]
        public string CompanyAddress1 { get { return _companyAddress1; } set { _companyAddress1 = value; } }
        /// <summary>
        /// Contains the value for the "CompanyAddress2" merge field
        /// </summary>      
        [MergeFieldTag("CompanyAddress2")]
        public string CompanyAddress2 { get { return _companyAddress2; } set { _companyAddress2 = value; } }
        /// <summary>
        /// Contains the value for the "CompanyCity" merge field
        /// </summary>      
        [MergeFieldTag("CompanyCity")]
        public string CompanyCity { get { return _companyCity; } set { _companyCity = value; } }
        /// <summary>
        /// Contains the value for the "_CompanyPhone" merge field
        /// </summary>      
        [MergeFieldTag("CompanyPhone")]
        public string CompanyPhone { get { return _companyPhone; } set { _companyPhone = value; } }
        /// <summary>
        /// Contains the value for the "CompanyFax" merge field
        /// </summary>      
        [MergeFieldTag("CompanyFax")]
        public string CompanyFax { get { return _companyFax; } set { _companyFax = value; } }

        /// <summary>
        /// Contains the value for the "Current Date Time" merge field
        /// </summary>      
        [MergeFieldTag("current-date-time")]
        public string CurrentDateTime { get { return _currentDateTime; } set { _currentDateTime = value; } }

        /// <summary>
        /// Contains the value for the "HelpDeskEmail" merge field
        /// </summary>      
        [MergeFieldTag("help-desk-email")]
        public string HelpDeskEmail { get { return _helpDeskEmail; } set { _helpDeskEmail = value; } }

        /// <summary>
        /// Contains the value for the "HelpDeskPhoneNumber" merge field
        /// </summary>      
        [MergeFieldTag("helpdesk-phone-number")]
        public string HelpDeskPhone { get { return _helpDeskPhone; } set { _helpDeskPhone = value; } }

        /// <summary>
        /// Contains the value for the "HelpDeskHours" merge field
        /// </summary>      
        [MergeFieldTag("helpdesk-hours")]
        public string HelpDeskHours { get { return _helpDeskHours; } set { _helpDeskHours = value; } }

        /// <summary>
        /// Contains the value for the "PasseordExpirationdate" merge field
        /// </summary>      
        [MergeFieldTag("password-expiration-date")]
        public string PasswoirdExpirationDate { get { return _passwordExpirationDate; } set { _passwordExpirationDate = value; } }
    }


    #endregion

    /// <summary>
    /// This class will replace merge field tags in an email template with data provided by a user
    /// </summary>
    public static class MergeFieldReplacer
    {
        /// <summary>
        /// This method dynamically scans the fields in the MergeField class and builds a dictionary of key/value pairs.  The keys are the merge field tags, and the values are the user-supplied substitute values
        /// </summary>
        /// <param name="mergeFields">An entity class containing the user-supplied substitute values</param>
        /// <returns>A dictionary containing the mergeField/userValue pairs</returns>
        internal static Dictionary<string, string> GetMergeData(MergeField mergeFields)
        {
            Dictionary<string, string> replData = new Dictionary<string, string>();
            Type t = mergeFields.GetType();
            foreach (PropertyInfo property in t.GetProperties())
            {
                object[] attrs = property.GetCustomAttributes(typeof(MergeFieldTagAttribute), false);
                if (attrs.Count() > 0)
                {

                    replData.Add(((MergeFieldTagAttribute)attrs[0]).Tag, (string)property.GetValue(mergeFields, null));
                }
            }
            return replData;
        }

        /// <summary>
        /// Replaces merge field tags in a string with user-supplied values
        /// </summary>
        /// <param name="msgBodyText">The text containing the merge field tags that will be replaced</param>
        /// <param name="mergeFields">The entity containing the user-supplied values that will substitute for the merge tags</param>
        /// <returns>Text with the merge field tags replaced by the user-supplied values</returns>
        /// <exception cref="UnknownMergeFieldException">Thrown when the message body contains unrecognized merge field tags</exception>
        public static string Replace(string msgBodyText, MergeField mergeFields)
        {
            Dictionary<string, string> mergeData = GetMergeData(mergeFields);
            foreach (KeyValuePair<string, string> kvp in mergeData)
            {
                msgBodyText = msgBodyText.Replace(kvp.Key, kvp.Value);
            }

            // check if there are any unmerged fields, and if so, throw an exception
            int StartPos = 0;
            int EndPos = 0;
            List<string> UnknownMergeFields = new List<string>();
            while ((StartPos = msgBodyText.IndexOf(MergeFieldTagAttribute.MergeFieldOpeningDelimeter, StartPos)) != -1)
            {
                EndPos = msgBodyText.IndexOf(MergeFieldTagAttribute.MergeFieldClosingDelimiter);
                UnknownMergeFields.Add(msgBodyText.Substring(StartPos+1, EndPos - StartPos + 2));
                StartPos = EndPos+1;
            }
            if (UnknownMergeFields.Count > 0)
            {
                throw new UnknownMergeFieldException(UnknownMergeFields.ToArray<string>());
            }
            else
            {
               return msgBodyText;
            }
        }
    }
}
