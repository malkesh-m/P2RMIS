using System;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for displaying the session panel communications on the panel management
    /// views.
    /// </summary>
    public interface ISessionPanelCommunicationsList
    {
        /// <summary>
        /// The date the email was sent
        /// </summary>
        DateTime? SendDate { get; set; }
        /// <summary>
        /// The address the email was sent from
        /// </summary>
        string FromEmailAddress { get; set; }
        /// <summary>
        /// The email's subject
        /// </summary>
        string Subject { get; set; }
        /// <summary>
        /// The communication log identifer of the email
        /// </summary>
        int CommunicationLogId { get; set; }
    }
}
