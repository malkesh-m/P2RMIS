using System;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Mail
{
    /// <summary>
    /// This class acts an interface between the Web module and the MergeField Replacer service.
    /// </summary>
    /// <remarks>
    /// temporary class renaming.
    /// </remarks>
    public class MailMergeFieldBuilder
    {
        private const string BLANK_STRING = " ";

        private ManageUsers _usrMgr = new ManageUsers();
        private MergeField _mergeFieldInput = new MergeField();

        /// <summary>
        /// If any of the passed in fields are null it will be assumed that the replacement fields are empty.  All the fields will have a value of
        /// at least blank if null fields are passed in
        /// </summary>
        /// <param name="tousr"></param>
        /// <param name="fromUsr"></param>
        public MailMergeFieldBuilder(User tousr, User fromUsr)
        {
            this.InitMergeFields();
            this.SetDefaultMergeFields();

            if (tousr != null)
            {
                this.SetToUserRelatedMergeFields(tousr);
            }

            if (fromUsr != null)
            {
                this.SetFromUserRelatedMergeFields(fromUsr);
            }

        }

        private void InitMergeFields()
        {
            _mergeFieldInput.ApplicationTitle = BLANK_STRING;
            _mergeFieldInput.CritiqueDeadline = BLANK_STRING;
            _mergeFieldInput.MeetingNameAndDate = BLANK_STRING;
            _mergeFieldInput.PanelName = BLANK_STRING;
            _mergeFieldInput.PanelAbbrev = BLANK_STRING;
            _mergeFieldInput.RoleOnPanel = BLANK_STRING;

            _mergeFieldInput.FromEmailAddress = BLANK_STRING;
            _mergeFieldInput.FromFirstName = BLANK_STRING;
            _mergeFieldInput.FromFullMailingAddress = BLANK_STRING;
            _mergeFieldInput.FromFullName = BLANK_STRING;
            _mergeFieldInput.FromLastName = BLANK_STRING;
            _mergeFieldInput.FromPrefix = BLANK_STRING;
            _mergeFieldInput.FromRole = BLANK_STRING;
            _mergeFieldInput.FromSuffix = BLANK_STRING;

            _mergeFieldInput.ToEmailAddress = BLANK_STRING;
            _mergeFieldInput.ToFirstName = BLANK_STRING;
            _mergeFieldInput.ToFullMailingAddress = BLANK_STRING;
            _mergeFieldInput.ToFullName = BLANK_STRING;
            _mergeFieldInput.ToLastName = BLANK_STRING;
            _mergeFieldInput.ToPrefix = BLANK_STRING;
            _mergeFieldInput.ToRole = BLANK_STRING;
            _mergeFieldInput.ToSuffix = BLANK_STRING;

            _mergeFieldInput.UserLogin = BLANK_STRING;

            _mergeFieldInput.hostname = BLANK_STRING;
            _mergeFieldInput.TemporaryPassword = BLANK_STRING;  //special field only used for To user in the Create.
            _mergeFieldInput.ParticipantType = BLANK_STRING;
            _mergeFieldInput.FY = BLANK_STRING;
            _mergeFieldInput.ProgramName = BLANK_STRING;

            _mergeFieldInput.CurrentDateTime = BLANK_STRING;
            _mergeFieldInput.HelpDeskEmail = BLANK_STRING;
            _mergeFieldInput.HelpDeskPhone = BLANK_STRING;
            _mergeFieldInput.HelpDeskHours = BLANK_STRING;
            _mergeFieldInput.PasswoirdExpirationDate = BLANK_STRING;


        }

        private void SetFromUserRelatedMergeFields(User fromUsr)
        {
            _mergeFieldInput.FromEmailAddress = _usrMgr.GetEmailAddress(fromUsr);
            _mergeFieldInput.FromFirstName = fromUsr.UserInfoes.FirstOrDefault().FirstName;
            _mergeFieldInput.FromFullMailingAddress = _usrMgr.GetMailingAddress(fromUsr, "<br>");
            _mergeFieldInput.FromLastName = fromUsr.UserInfoes.Single().LastName;
            if (fromUsr.UserInfoes.Single().PrefixId.HasValue)
            {
                _mergeFieldInput.FromPrefix = _usrMgr.GetPrefixString((int)fromUsr.UserInfoes.Single().PrefixId);
            }
            else
            {
                _mergeFieldInput.FromPrefix = String.Empty;
            }
            _mergeFieldInput.FromLastName = fromUsr.UserInfoes.Single().LastName;
            _mergeFieldInput.FromFullName = fromUsr.UserInfoes.Single().FirstName + " " + fromUsr.UserInfoes.Single().LastName;
            _mergeFieldInput.FromRole = _usrMgr.GetSystemRolesString(fromUsr, ",");
            _mergeFieldInput.FromSuffix = String.Empty;
        }



        private void SetToUserRelatedMergeFields(User toUsr)
        {
            //User Login specific to To User =  created by us - not the client required field
            _mergeFieldInput.UserLogin = toUsr.UserLogin;

            _mergeFieldInput.ToEmailAddress = _usrMgr.GetEmailAddress(toUsr);
            _mergeFieldInput.ToFirstName = toUsr.UserInfoes.FirstOrDefault().FirstName;
            _mergeFieldInput.ToFullMailingAddress = _usrMgr.GetMailingAddress(toUsr, "<br>");
            _mergeFieldInput.ToLastName = toUsr.UserInfoes.Single().LastName;
            if (toUsr.UserInfoes.Single().PrefixId.HasValue)
            {
                _mergeFieldInput.ToPrefix = _usrMgr.GetPrefixString((int)toUsr.UserInfoes.Single().PrefixId);
            }
            else
            {
                _mergeFieldInput.ToPrefix = String.Empty;
            }
            _mergeFieldInput.ToLastName = toUsr.UserInfoes.Single().LastName;
            _mergeFieldInput.ToFullName = toUsr.UserInfoes.Single().FirstName + " " + toUsr.UserInfoes.Single().LastName;
            _mergeFieldInput.ToRole = _usrMgr.GetSystemRolesString(toUsr, ",");
            _mergeFieldInput.ToSuffix = String.Empty;
        }

        private void SetDefaultMergeFields()
        {
            _mergeFieldInput.hostname = GetHostURL();
            _mergeFieldInput.CurrentDateTime = GlobalProperties.P2rmisDateTimeNow.ToString();
            _mergeFieldInput.HelpDeskEmail = ConfigManager.HelpDeskEmailAddress;
            _mergeFieldInput.HelpDeskPhone = ConfigManager.HelpDeskPhoneNumber;
            _mergeFieldInput.HelpDeskHours = ConfigManager.HelpDeskHoursStandard;
        }
        /// <summary>
        /// Gets the current server information from the web.config and builds a URL
        /// <scheme>://<host>/<path>
        /// </summary>
        /// <returns>appPath - URL of current server</returns>
        private static string GetHostURL()
        {
            //get the host name of the server
            string host = ConfigManager.UrlHost;
            //get the first part of url (http or https)
            string scheme = ConfigManager.UrlScheme;

            //build the URL format
            var appPath = string.Format("{0}{1}", scheme, host);

            return appPath;
        }
        /// <summary>
        /// Sets the temporary password into the merge field input variable
        /// </summary>
        /// <param name="passWrd"></param>
        public void SetTemporaryPassword(string passWrd)
        {
            _mergeFieldInput.TemporaryPassword = passWrd;
        }
        /// <summary>
        /// Sets the  password expiration date into the merge field input variable
        /// </summary>
        /// <param name="expirationDate"></param>
        public void SetPasswordExpirationDate(string expirationDate)
        {
            _mergeFieldInput.PasswoirdExpirationDate = expirationDate;
        }
        /// <summary>
        /// Sets the Replace merge field input variable
        /// </summary>
        /// <param name="inStr"></param>
        public string MergeFieldProcesser(string inStr)
        {
            String retStr = MergeFieldReplacer.Replace(inStr, _mergeFieldInput);

            return retStr;
        }
        /// <summary>
        /// Sets the Panel Name merge field input variable
        /// </summary>
        /// <param name="inStr"></param>
        public void SetPanelName(string instr)
        {
            _mergeFieldInput.PanelName = instr;
        }
        /// <summary>
        /// Sets the Panel Abbrev merge field input variable
        /// </summary>
        /// <param name="inStr"></param>
        public void SetPanelAbbrev(string instr)
        {
            _mergeFieldInput.PanelAbbrev = instr;
        }
        /// <summary>
        /// Sets the Participant Type merge field input variable
        /// </summary>
        /// <param name="inStr"></param>
        public void SetParticipantType(string inStr)
        {
            _mergeFieldInput.ParticipantType = inStr;
        }
        /// <summary>
        /// Sets the FY merge field input variable
        /// </summary>
        /// <param name="inStr"></param>
        public void SetFY(string inStr)
        {
            _mergeFieldInput.FY = inStr;
        }
        /// <summary>
        /// Sets the Program Name merge field input variable
        /// </summary>
        /// <param name="inStr"></param>
        public void SetProgramName(string inStr)
        {
            _mergeFieldInput.ProgramName = inStr;
        }
        /// <summary>
        /// Sets the Company Name merge field input variable
        /// </summary>
        /// <param name="inStr"></param>
        public void SetCompanyName(string inStr)
        {
            _mergeFieldInput.CompanyName = inStr;
        }
        /// <summary>
        /// Sets the Company Division merge field input variable
        /// </summary>
        /// <param name="inStr"></param>
        public void SetCompanyDivision(string inStr)
        {
            _mergeFieldInput.CompanyDivision = inStr;
        } 
        /// <summary>
        /// Sets the Company Address1 merge field input variable
        /// </summary>
        /// <param name="inStr"></param>
        public void SetCompanyAddress1(string inStr)
        {
            _mergeFieldInput.CompanyAddress1 = inStr;
        }
        /// <summary>
        /// Sets the Company Address2 merge field input variable
        /// </summary>
        /// <param name="inStr"></param>
        public void SetCompanyAddress2(string inStr)
        {
            _mergeFieldInput.CompanyAddress2 = inStr;
        }
        /// <summary>
        /// Sets the Compan yCity merge field input variable
        /// </summary>
        /// <param name="inStr"></param>
        public void SetCompanyCity(string inStr)
        {
            _mergeFieldInput.CompanyCity = inStr;
        }
        /// <summary>
        /// Sets the Company Phone merge field input variable
        /// </summary>
        /// <param name="inStr"></param>
        public void SetCompanyPhone(string inStr)
        {
            _mergeFieldInput.CompanyPhone = inStr;
        }
        /// <summary>
        /// Sets the Company Fax merge field input variable
        /// </summary>
        /// <param name="inStr"></param>
        public void SetCompanyFax(string inStr)
        {
            _mergeFieldInput.CompanyFax = inStr;
        }
    }
}
