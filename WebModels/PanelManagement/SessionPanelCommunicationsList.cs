using System;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for displaying the session panel communications on the panel management
    /// views.
    /// </summary>
    public class SessionPanelCommunicationsList : ISessionPanelCommunicationsList
    {
        public SessionPanelCommunicationsList(DateTime? date, int userId, string subject, int logId)
        {
            this.SendDate = date;
            this.FromUserId = userId;
            this.Subject = subject;
            this.CommunicationLogId = logId;
        }
        /// <summary>
        /// The date the email was sent
        /// </summary>
        public DateTime? SendDate { get; set; }
        /// <summary>
        /// The address the email was sent from
        /// </summary>
        public string FromEmailAddress { get; set; }
        /// <summary>
        /// The email's subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// The communication log identifer of the email
        /// </summary>
        public int CommunicationLogId { get; set; }
        /// <summary>
        /// The user identifier the email was sent from
        /// </summary>
        public int FromUserId { get; set; }
    }
}
