using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{

    /// <summary>
    /// Custom methods for Entity Framework's CommunicationLog  object. 
    /// </summary>
    public partial class CommunicationLog: IStandardDateFields
    {
        /// <summary>
        /// Populates a new CommunicationLog entity object
        /// </summary>
        /// <param name="sessionPanelId"><Session panel identifier/param>
        /// <param name="subject">Message subject</param>
        /// <param name="message">Message body text</param>
        /// <param name="Bcc">Bcc address</param>
        /// <param name="userId">User identifier of the user creating the email</param>
        /// <returns>CommunicationLog entity</returns>
        public CommunicationLog Popolate(int sessionPanelId, string subject, string message, string Bcc, int userId)
        {
            this.SessionPanelId = sessionPanelId;
            this.Subject = subject;
            this.Message = message;
            this.BCC = Bcc;
            Helper.UpdateCreatedFields(this, userId);
            Helper.UpdateModifiedFields(this, userId);

            return this;
        }

        /// <summary>
        /// Obtain sender's first name
        /// </summary>
        /// <returns>sender's first name</returns>
        public string FirstName()
        {
            string firstName = string.Empty;
            if (this.CreatedBy.HasValue)
            {
                firstName = this.SessionPanel.FirstName(this.CreatedBy.Value);
            }
            return firstName;
        }
        /// <summary>
        /// Obtain sender's last name
        /// </summary>
        /// <returns>senders last name</returns>
        public string LastName()
        {
            string lastName = string.Empty;
            if (this.CreatedBy.HasValue)
            {
                lastName = SessionPanel.LastName(this.CreatedBy.Value);
            }
            return lastName;
        }
        /// <summary>
        /// Obtain sender's panel assignment identifier
        /// </summary>
        /// <returns>Panel user assignment identifier</returns>
        public int? PanelUserAssignmentId()
        {
            int? panelUserAssignmentId = null;
            if (this.CreatedBy.HasValue)
            {
                panelUserAssignmentId = this.SessionPanel.PanelUserAssignmentId(this.CreatedBy.Value);
            }
            return panelUserAssignmentId;
        }
        /// <summary>
        /// Obtain sender's  email address
        /// </summary>
        /// <returns>Sender's primary email address</returns>
        public string UserPrimaryEmailAddress()
        {
            string emailAddress = string.Empty;
            if (this.CreatedBy.HasValue)
            {
                emailAddress = this.SessionPanel.UserEmailAddress(this.CreatedBy.Value);
            }
            return emailAddress;
        }
        /// <summary>
        /// Obtain senders's participant type abbreviation
        /// </summary>
        /// <returns>Senders participant type abbreviation</returns>
        public string ParticipantTypeAbbreviation()
        {
            string participantTypeAbbreviation = string.Empty;
            if (this.CreatedBy.HasValue)
            {
                participantTypeAbbreviation = this.SessionPanel.ParticipantTypeAbbreviation(this.CreatedBy.Value);
            }
            return participantTypeAbbreviation;
        }
        /// <summary>
        /// Returns the User entity identifier of the user who created the email.
        /// </summary>
        /// <returns>User entity identifier</returns>
        /// <remarks>
        /// The assumption here is that there will always be a value for the user identifier who created the log record.
        /// </remarks>
        public int UserId()
        {
            return this.CreatedBy.Value;
        }
    }
}
