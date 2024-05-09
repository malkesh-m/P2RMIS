using System.Text;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the communication's content
    /// </summary>
    public class CommunicationContentViewModel
    {
        #region Constructor
        /// <summary>
        /// Constructor - initialize so model is usable.
        /// </summary>
        public CommunicationContentViewModel()
        {
            Content = new EmailContent();
        }
        #endregion
        #region Properties
        /// <summary>
        /// The content of the communications
        /// </summary>
        public EmailContent Content { get; set; }
        #endregion
        #region Formating helpers
        /// <summary>
        /// Format who received the communications
        /// </summary>
        /// <returns></returns>
        public string FormatTo()
        {
            StringBuilder sb = new StringBuilder();

            foreach (EmailAddress value in Content.To)
            {
                AddEmailAddress(sb, value);
            }

            return sb.ToString();
        }
        /// <summary>
        /// Format who received CC of the communications 
        /// </summary>
        /// <returns></returns>
        public string FomatCc()
        {
            StringBuilder sb = new StringBuilder();

            foreach (EmailAddress value in Content.Cc)
            {
                AddEmailAddress(sb, value);
            }

            return sb.ToString();
        }
        /// <summary>
        /// Format the communications date time field
        /// </summary>
        /// <returns></returns>
        public string FormatDateTime()
        {
            return ViewHelpers.FormatLastUpdateDateTime(Content.Date);
        }
        /// <summary>
        /// Format the communication's attachment list
        /// </summary>
        /// <returns></returns>
        public string FormatAttachments()
        {
            StringBuilder sb = new StringBuilder();

            foreach (EmailAttachment value in Content.Attachments)
            {
                if (sb.Length != 0)
                {
                    sb.Append("; ");
                }
                sb.Append(value.FileName);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Format a recepient of the email and insert it into a formated list of recepients
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="value"></param>
        internal void AddEmailAddress(StringBuilder sb, EmailAddress value)
        {
            if (sb.Length != 0)
            {
                sb.Append("; ");
            }
            sb.AppendFormat("{0}, {1}", value.LastName, value.FirstName);
        }
        #endregion

    }
}