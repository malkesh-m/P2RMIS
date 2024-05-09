
using System;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for Email Content in PanelManagement
    /// </summary>
    public class EmailContent : IEmailContent
    {
        #region Contructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public EmailContent()
        {
            To = new List<IEmailAddress>();
            Cc = new List<IEmailAddress>();
            Attachments = new List<IEmailAttachment>();
        }
        #endregion
        /// <summary>
        /// List of the email address of the email's 'To' recepients
        /// </summary>
        public ICollection<IEmailAddress> To { get; set; }
        /// <summary>
        /// List of the email address of the email's 'cc' recepients
        /// </summary>
        public ICollection<IEmailAddress> Cc { get; set; }
        /// <summary>
        /// List of the email address of the email's 'bcc' recepients
        /// </summary>
        public string Bcc { get; set; }
        /// <summary>
        /// Email address of the email's sender
        /// </summary>
        public EmailAddress From { get; set; }
        /// <summary>
        /// The email's subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// The email's message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// List of the email's attachments
        /// </summary>
        public ICollection<IEmailAttachment> Attachments { get; set; }
        /// <summary>
        /// The date the email was sent
        /// </summary>
        public DateTime? Date { get; set; }
    }
}
