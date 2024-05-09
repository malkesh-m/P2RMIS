using System.Collections.Generic;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Bll.Mail
{
    /// <summary>
    /// MailService provides services for constructing; formatting and sending emails.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Create an email message for the transfer request & mail it.
        /// </summary>
        /// <param name="fromUserId">User identifier of the SRO who is initiating the transfer</param>
        /// <param name="applicationLogNumber">Application log number</param>
        /// <param name="sourcePanel">Source panel</param>
        /// <param name="targetPanel">Target panel</param>
        /// <param name="fullPanelName">The full source panel name (drop down selection)</param>
        /// <param name="transferReason">Why the panel is being transferred</param>
        /// <param name="comments">Transfer comments</param>
        /// <returns>Formated body of email request</returns>
        string TransferRequest(int fromUserId, string applicationLogNumber, string sourcePanel, string targetPanel, string fullPanelName, string transferReason, string comments);
        /// <summary>
        /// Critiques the reset.
        /// </summary>
        /// <param name="fromUserId">From user identifier.</param>
        /// <param name="reviewerName">Name of the reviewer.</param>
        /// <param name="reviewerEmail">The reviewer email.</param>
        /// <param name="programYear">The program year.</param>
        /// <param name="programFullName">Full name of the program.</param>
        /// <param name="panelFullName">Full name of the panel.</param>
        /// <param name="panelAbbreviation">The panel abbreviation.</param>
        /// <param name="applicationLogNumber">The application log number.</param>
        /// <param name="sroEmail">The sro email.</param>
        /// <returns></returns>
        string CritiqueReset(int fromUserId, string reviewerName, string reviewerEmail, string programYear, string programFullName, string panelFullName, string panelAbbreviation, string applicationLogNumber, string sroEmail);
        /// <summary>
        /// Create an email message for the transfer reviewer request & mail it.
        /// </summary>
        /// <param name="fromUserId">User identifier of the SRO who is initiating the transfer</param>        
        /// <param name="reviewerName">Reviewer's name</param>
        /// <param name="sourcePanelName">Source panel name</param>
        /// <param name="sourcePanelAbbr">Source panel abbreviation</param>
        /// <param name="targetPanelName">Target panel name</param>
        /// <param name="targetPanelAbbr">Target panel abbreviation</param>
        /// <param name="programFY">Program's fiscal year</param>
        /// <param name="ProgramDescription">Program's description</param>
        /// <param name="comments">Transfer comments</param>
        /// <returns>Formated body of email request</returns>
        string TransferReviewerRequest(int fromUserId, string reviewerName, string sourcePanelName, string sourcePanelAbbr, 
            string targetPanelName, string targetPanelAbbr, string programFY, string programDescription, string comments);
        /// <summary>
        /// Retrieves lists of primary email addresses.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Container of IEmailAddress models</returns>
        Container<IEmailAddress> ListPanelUserEmailAddresses(int sessionPanelId);
        /// <summary>
        /// Retrieves lists of a panel reviewer's email addresses.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Container of IEmailAddress models</returns>
        Container<IEmailAddress> ListPanelReviewersEmailAddresses(int sessionPanelId);
        /// <summary>
        /// Lists the panel sro email addresses.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        Container<IEmailAddress> ListPanelSroEmailAddresses(int sessionPanelId);
        /// <summary>
        /// Retrieves lists of a panel reviewer's email addresses.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        Container<IEmailAddress> ListPanelSroRtaEmailAddresses(int sessionPanelId);
        /// <summary>
        /// Send a credential email without a password.
        /// </summary>
        /// <param name="templateName">Template name</param>
        /// <param name="user">Email addressee</param>
        /// <returns>Status of mail request</returns>
        MailService.MailStatus SendCredential(string templateName, User user);
        /// <summary>
        /// Send a credential email with a password.
        /// </summary>
        /// <param name="templateName">Template name</param>
        /// <param name="password">Clear text password</param>
        /// <param name="user">Email addressee</param>
        /// <returns>Status of mail request</returns>
        MailService.MailStatus SendCredential(string templateName, string password, User user);
        /// <summary>
        /// Sends an email to a user informing them that they have been assigned to this panel as a reviewer
        /// </summary>
        /// <param name="reviewerId">The user identifier for the reviewer</param>
        /// <param name="sessionPanelId">The session panel identifier</param>
        /// <param name="userId">The user identifier for the user making the assignment</param>
        /// <returns>MailStatus value indicating the status of the service request </returns>
        MailService.MailStatus SendPanelAssignmentEmail(int reviewerId, int sessionPanelId, int userId);

        /// <summary>
        /// Sends the discussion notification email.
        /// </summary>
        /// <param name="templateData">The template data.</param>
        /// <returns>MailStatus object indicating if email was successful</returns>
        MailService.MailStatus SendDiscussionNotification(IDiscussionNotificationModel templateData);
        /// <summary>
        /// Create an email message for the transfer reviewer request & mail it.
        /// </summary>
        /// <param name="fromUserId">User identifier of the SRO who is initiating the transfer</param>  
        /// <param name="sourcePanelId">SourcePanel entity identifier</param>
        /// <param name="reviewerName">Reviewer's name</param>
        /// <param name="sourcePanelName">Source panel name</param>
        /// <param name="sourcePanelAbbr">Source panel abbreviation</param>
        /// <param name="targetPanelName">Target panel name</param>
        /// <param name="targetPanelAbbr">Target panel abbreviation</par
        /// <param name="programFY">Program's fiscal year</param>
        /// <param name="programDescription">Program's description</param>
        /// <param name="comments">Transfer comments</param>
        /// <returns>Formatted body of email request</returns>
        string ReleaseReviewerRequest(int fromUserId, int sourcePanelId, string reviewerName, string sourcePanelName, string sourcePanelAbbr, string programFY, string programDescription, string comments);
        /// <summary>
        /// Send an email to one or more recipients.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel identifier</param>
        /// <param name="panelUserAssignmentIds">PanelUserAssignment identifiers of To recipients</param>
        /// <param name="senderEmail">From address</param>
        /// <param name="recipientEmail">Semicolon separated list of recipient addresses </param>
        /// <param name="panelUserAssignmentIdOfCcEmail">PanelUserAssignment identifier of CC recipients</param>
        /// <param name="ccEmail">Semicolon separated list of recipient CC addresses</param>
        /// <param name="bccEmailAddresses">Single BCC address</param>
        /// <param name="subject">Subject text</param>
        /// <param name="message">Body text</param>
        /// <param name="attachments">Representation of attachments to send</param>
        /// <param name="recipientAddresses">Collection of individual recipient addresses</param>
        /// <param name="ccEmailAddresses">Collection of individual cc recipient addresses</param>
        /// <param name="userId">User identifier of the user sending the email</param>
        /// <returns>MailStatus value indicating the status of the service request </returns>
        MailService.MailStatus SendEmail(int sessionPanelId, ICollection<int> panelUserAssignmentIds, string senderEmail, string recipientEmail, ICollection<int> panelUserAssignmentIdOfCcEmail, string ccEmail, string bccEmailAddresses, string subject, string message, ICollection<AttachmentToSend> attachments, ICollection<string> recipientAddresses, ICollection<string> ccEmailAddresses, int userId);
        /// <summary>
        /// Sends a ticket notification email upon ticket creation.
        /// </summary>
        /// <param name="requestorEmail">The requestor email.</param>
        /// <param name="newTicketId">The new ticket identifier.</param>
        /// 
        /// <returns>MailStatus regarding whether the email was sent successfully.</returns>
        MailService.MailStatus SendTicketNotification(string requestorEmail, string newTicketId);
        /// <summary>
        /// Send password notification email to user
        /// </summary>
        /// <param name="templateName">Template name</param>
        /// <param name="user">Email addressee</param>
        /// <returns>Status of mail request</returns>
        MailService.MailStatus SendPasswordChangeNotification(string templateName, User user);
    }
}
