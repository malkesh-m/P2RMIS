using System;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for Email Content in PanelManagement
    /// </summary>
    public interface IEmailContent
    {
        /// <summary>
        /// List of the email address of the email's 'To' recepients
        /// </summary>
        ICollection<IEmailAddress> To { get; set; }
        /// <summary>
        /// List of the email address of the email's 'cc' recepients
        /// </summary>
        ICollection<IEmailAddress> Cc { get; set; }
        /// <summary>
        /// List of the email address of the email's 'bcc' recepients
        /// </summary>
        string Bcc { get; set; }
        /// <summary>
        /// Email address of the email's sender
        /// </summary>
        EmailAddress From { get; set; }
        /// <summary>
        /// The email's subject
        /// </summary>
        string Subject { get; set; }
        /// <summary>
        /// The email's message
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// List of the email's attachments
        /// </summary>
        ICollection<IEmailAttachment> Attachments { get; set; }
        /// <summary>
        /// The date the email was sent
        /// </summary>
        DateTime? Date { get; set; }
    }
}
