using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.HelperClasses;


namespace Sra.P2rmis.Bll.Mail
{
    /// <summary>
    /// MailService provides services for constructing; formatting and sending emails.
    /// </summary>
    public class MailService: ServerBase, IMailService
    {
        #region Supporting Classes
        /// <summary>
        /// Status values returned from the service
        /// </summary>
        /// <remarks>ENUM needs to be re-factored to Message service</remarks>
        public enum MailStatus
        {
            Default,
            /// <summary>
            /// Service was completed successfully
            /// </summary>
            Success,
            /// <summary>
            /// Service failed with an unspecified error
            /// </summary>
            Failure,
            /// <summary>
            /// Total size of all attachments exceeds maximum
            /// </summary>
            AttachmentsExceedSize,
            /// <summary>
            /// Server failed to send the email - reason unspecified.
            /// </summary>
            FailedToSend,
            /// <summary>
            /// The second credential email was not sent.
            /// </summary>
            SecondCredentialNotSent
        }
        /// <summary>
        /// Status structure to record status of PanelManagement email requiest.
        /// </summary>
        internal struct EmailStatus
        {
            /// <summary>
            /// Count of emails successfully sent.
            /// </summary>
            public int Success;
            /// <summary>
            /// Count of emails that were not sent.
            /// </summary>
            public int Failure;
        }
        #endregion
        #region Constants
        private const string UseThisWhenNoComment = "None.";
        private const string FailToSendMessage = "Failed to send a {0} message.  ";
        private const string Sro = "SRO";
        private const string Rta = "RTA";
        /// <summary>
        /// Participation types used to filter email addresses
        /// </summary>
        private static readonly string[] _emailAddressParticipationTypes = { Sro, Rta };
        /// <summary>
        /// Maximum cumulative size of file attachments.  Currently 10 MB
        /// </summary>
        private const long MaximumAttachmentSize = 10000000;
        #endregion
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public MailService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion
        #region Services
        #region Panel Transfer
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
        /// <returns>Formatted body of email request</returns>
        public string TransferRequest(int fromUserId, string applicationLogNumber, string sourcePanel, string targetPanel, string fullPanelName, string transferReason, string comments)
        {
            ValidateTransferRequest(fromUserId, applicationLogNumber, sourcePanel, targetPanel,
                    fullPanelName, transferReason);

            string[] contentParas = new string[] { applicationLogNumber, sourcePanel, targetPanel, fullPanelName, transferReason, comments, fullPanelName };

            string content = GetTransferRequestContent(SystemTemplate.RequestTransfer, fromUserId, new string[] {}, contentParas, comments, "TransferRequest");
            return content;
        }
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
        public string CritiqueReset(int fromUserId, string reviewerName, string reviewerEmail, string programYear,
            string programFullName, string panelFullName, string panelAbbreviation, string applicationLogNumber, string sroEmail)
        {
            ValidateCritiqueReset(fromUserId, reviewerName, reviewerEmail, programYear, programFullName, panelFullName, panelAbbreviation,
                applicationLogNumber, sroEmail);

            string[] contentParas = new string[] { reviewerName, programYear, programFullName, panelFullName, panelAbbreviation, applicationLogNumber, sroEmail };

            string content = GetCritiqueResetContent(SystemTemplate.CritiqueReset, fromUserId, reviewerEmail, new string[] { }, contentParas, "CritiqueReset");
            return content;
        }
        
        /// <summary>
        /// Retrieves lists of primary email addresses.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Container of IEmailAddress models</returns>
        public Container<IEmailAddress> ListPanelUserEmailAddresses(int sessionPanelId)
        {
            ValidateListPanelUserEmailAddressesParameters(sessionPanelId);

            Container<IEmailAddress> container = new Container<IEmailAddress>();
            var results = UnitOfWork.SessionPanelRepository.ListPanelUserEmailAddresses(sessionPanelId);

            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Retrieves lists of a panel reviewer's email addresses.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Container of IEmailAddress models of reviewer emails</returns>
        public Container<IEmailAddress> ListPanelReviewersEmailAddresses(int sessionPanelId)
        {
            ValidateListPanelReviewersEmailAddressesParameters(sessionPanelId);

            Container<IEmailAddress> container = new Container<IEmailAddress>();
            var results = UnitOfWork.SessionPanelRepository.ListPanelUserEmailAddresses(sessionPanelId);

            container.ModelList = results.ModelList.Where(r => !_emailAddressParticipationTypes.Contains(r.ParticipantTypeAbbreviation));

            return container;
        }
        /// <summary>
        /// Lists the panel sro email addresses.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        public Container<IEmailAddress> ListPanelSroEmailAddresses(int sessionPanelId)
        {
            ValidateListPanelSroRtaEmailAddressesAddressesParameters(sessionPanelId);

            Container<IEmailAddress> container = new Container<IEmailAddress>();
            var results = UnitOfWork.SessionPanelRepository.ListPanelUserEmailAddresses(sessionPanelId);
            container.ModelList = results.ModelList.Where(r => r.ParticipantTypeAbbreviation == Sro);

            return container;
        }
        /// <summary>
        /// Retrieves lists of a panel reviewer's email addresses.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier of Sro & Rta emails</param>
        public Container<IEmailAddress> ListPanelSroRtaEmailAddresses(int sessionPanelId)
        {
            ValidateListPanelSroRtaEmailAddressesAddressesParameters(sessionPanelId);

            Container<IEmailAddress> container = new Container<IEmailAddress>();
            var results = UnitOfWork.SessionPanelRepository.ListPanelUserEmailAddresses(sessionPanelId);
            container.ModelList = results.ModelList.Where(r => _emailAddressParticipationTypes.Contains(r.ParticipantTypeAbbreviation));

            return container;
        }
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
        public MailStatus SendEmail(int sessionPanelId, ICollection<int> panelUserAssignmentIds, string senderEmail, 
                                      string recipientEmail, ICollection<int> panelUserAssignmentIdOfCcEmail, string ccEmail,                              
                                      string bccEmailAddresses, string subject, string message, ICollection<AttachmentToSend> attachments,
                                      ICollection<string> recipientAddresses, ICollection<string> ccEmailAddresses, int userId)
        {
            ValidateSendEmailParameters(sessionPanelId, panelUserAssignmentIds, senderEmail, recipientEmail, ccEmail, bccEmailAddresses, subject, message, attachments, userId);
            MailStatus result = MailStatus.Failure;
            //
            // In addition to the general validation or parameters we need to ensure the total size of all attachments
            // do not exceed a maximum value.  
            //
            result = CheckAttachmentsSizeDoNotValidateMaximum(attachments);
            if (result == MailStatus.Default)
            {
                //
                // This gives us the basic builder from which we build the emails
                //
                LetterBuilder builder = new LetterBuilder(senderEmail, subject, message, attachments);
                EmailStatus successCounter = new EmailStatus();
                //
                // Now send out individual emails for the recipients, the cc recipients & the bcc recipients
                //
                MakeEmails(builder, recipientAddresses, ref successCounter);
                builder.SetAdditionalRecipients(recipientAddresses);
                MakeEmails(builder, ccEmailAddresses, ref successCounter);
                MakeEmails(builder, bccEmailAddresses, ref successCounter);
                //
                // If at least 1 email was sent out we consider it a success.  If so we create a log entry
                //
                if (successCounter.Success > 0)
                {
                    //
                    // Since we have successfully sent the letter we can log it
                    //
                    FileLetter(sessionPanelId, panelUserAssignmentIds, senderEmail, recipientEmail, panelUserAssignmentIdOfCcEmail, ccEmail, bccEmailAddresses, subject, message, attachments, userId);
                    result = MailStatus.Success;
                }
                else
                {
                    result = MailStatus.FailedToSend;
                }
            }
            return result;
        }
        /// <summary>
        /// Build & send an email to one or more recipient.
        /// </summary>
        /// <param name="builder">Email builder</param>
        /// <param name="recipientAddresses">Collection of recipient addresses</param>
        /// <param name="successCounter">Success counter </param>
        internal void MakeEmails(LetterBuilder builder, ICollection<string> recipientAddresses, ref EmailStatus successCounter)
        {
            //
            // Since we have a builder which has all the respective information for the email, all we
            // need to do is change the recipient address.  Once we do that we build & send.  Based upon 
            // the outcome update the appropriate success counter.
            //
            foreach(string address in recipientAddresses)
            {
                builder.Reset(address);
                builder.Build();
                Mailer letter = builder.Letter;

                if (letter.Send())
                {
                    successCounter.Success++;
                }
                else
                {
                    successCounter.Failure++;
                }
            }
        }
        /// <summary>
        /// Send an email to one or more addresses.  Overload for the BCC address which is received as a string not a collection
        /// of individual recipient addresses.  Separate the address then make the emails.
        /// </summary>
        /// <param name="builder">Email builder</param>
        /// <param name="recipientAddresses">Semi colon separated string of recipient addresses</param>
        /// <param name="successCounter">Success counter </param>
        internal void MakeEmails(LetterBuilder builder, string addressesAsString, ref EmailStatus successCounter)
        {
            ICollection<string> recipientAddresses = addressesAsString.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            MakeEmails(builder, recipientAddresses, ref successCounter);
        }
        /// <summary>
        /// Sends an email to a user informing them that they have been assigned to this panel as a reviewer
        /// </summary>
        /// <param name="reviewerId">The user identifier for the reviewer</param>
        /// <param name="sessionPanelId">The session panel identifier</param>
        /// <param name="userId">The user identifier for the user making the assignment</param>
        /// <returns>MailStatus value indicating the status of the service request </returns>
        public MailStatus SendPanelAssignmentEmail(int reviewerId, int sessionPanelId, int userId)
        {
            return SendPanelAssignmentEmail(SystemTemplate.Indexes.SYSTEM_TEMPLATE_REVIEWER_ASSIGNMENT_NOTIFICATION, reviewerId, sessionPanelId, userId);
        }
        /// <summary>
        /// Create the necessary entities to log the email.
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
        /// <param name="userId">User identifier of the user sending the email</param>
        internal virtual void FileLetter(int sessionPanelId, ICollection<int> panelUserAssignmentIds, string senderEmail, string recipientEmail, ICollection<int> panelUserAssignmentIdOfCcEmail, string ccEmail, string bccEmailAddresses, string subject, string message, ICollection<AttachmentToSend> attachments, int userId)
        {
            //
            // First create the main log entry
            //
            CommunicationLog log = new CommunicationLog();
            log.Popolate(sessionPanelId, subject, message, bccEmailAddresses, userId);
            UnitOfWork.CommunicationLogRepository.Add(log);
            //
            // Now one needs to create a log entry for each recipient (To & CC)
            //
            panelUserAssignmentIds.ToList().ForEach(x => FileRecipient(CommunicationLogRecipientType.ToRecipientType, x, log, userId));
            panelUserAssignmentIdOfCcEmail.ToList().ForEach(x => FileRecipient(CommunicationLogRecipientType.CCRecipientType, x, log, userId));
            //
            // And finally we deal with the attachments (if there are any there)
            //
            attachments.ToList().ForEach(s => FileAttachment(s, log, userId));
            //
            // After all that we save the puppy.
            //
            UnitOfWork.Save();
        }
        /// <summary>
        /// Create the message attachment log entry and add it to the CommunicationLog entity.
        /// </summary>
        /// <param name="attachment">Description of the attachment to the email</param>
        /// <param name="log">CommunicationLog entity object</param>
        /// <param name="userId">User identifier of the user sending the email</param>
        internal virtual void FileAttachment(AttachmentToSend attachment, CommunicationLog log, int userId)
        {
            CommunicationLogAttachment attachmentLog = new CommunicationLogAttachment();
            attachmentLog.Populate(attachment.FileName, userId);
            log.CommunicationLogAttachments.Add(attachmentLog);
        }
        /// <summary>
        /// Create the message recipient log entry and add it to the CommunicationLog entity.
        /// </summary>
        /// <param name="communicationLogRecipientTypeId">Type of the email recipient (TO or CC)</param>
        /// <param name="panelUserAssignmentId">PanelUserAssignment identifier</param>
        /// <param name="log">CommunicationLog entity object</param>
        /// <param name="userId">User identifier of the user sending the email</param>
        internal virtual void FileRecipient(int communicationLogRecipientTypeId, int panelUserAssignmentId, CommunicationLog log, int userId)
        {
            CommunicationLogRecipient recipient = new CommunicationLogRecipient();
            recipient.Populate(communicationLogRecipientTypeId, panelUserAssignmentId, userId);
            log.CommunicationLogRecipients.Add(recipient);
        }
        /// <summary>
        /// Constructs a Mailer object populated with addresses (From, To, CC, BCC); subject,
        /// body and attachments.
        /// </summary>
        /// <param name="senderEmail">From address</param>
        /// <param name="recipientEmail">Semicolon separated list of recipient addresses </param>
        /// <param name="ccEmail">Semicolon separated list of recipient CC addresses</param>
        /// <param name="bccEmailAddresses">Single BCC address</param>
        /// <param name="subject">Subject text</param>
        /// <param name="message">Body text</param>
        /// <param name="attachments">Representation of attachments to send</param>
        /// <returns><Mailer object representing email message/returns>
        internal virtual Mailer WriteLetter(string senderEmail, string recipientEmail, string ccEmail, string bccEmailAddresses, string subject, string message, ICollection<AttachmentToSend> attachments)
        {
            //
            // Create the mailer object that does all the heavy lifting for us
            //
            Mailer letter = new Mailer { IsBodyHtml = true };
            //
            // Now we add things
            //
            letter.FromAddress = senderEmail;
            letter.AddToAddress(recipientEmail);
            //
            // if we have a CC address, add it to the list
            //
            if (!string.IsNullOrWhiteSpace(ccEmail))
            {
            letter.AddCCAddress(ccEmail);
            }
            //
            // Same thing with the BCC addresses
            //
            if (!string.IsNullOrWhiteSpace(bccEmailAddresses))
            {
            letter.AddBccAddress(bccEmailAddresses);
            }
            SetContent(letter, subject, message);
            //
            // If there are any attachments add them now
            //
            attachments.ToList().ForEach(x => letter.AddAttachment(x.FileStream, x.FileName));

            return letter;
        }
        /// <summary>
        /// Create an email message for the transfer reviewer request & mail it.
        /// </summary>
        /// <param name="fromUserId">User identifier of the SRO who is initiating the transfer</param>        
        /// <param name="reviewerName">Reviewer's name</param>
        /// <param name="sourcePanelName">Source panel name</param>
        /// <param name="sourcePanelAbbr">Source panel abbreviation</param>
        /// <param name="targetPanelName">Target panel name</param>
        /// <param name="targetPanelAbbr">Target panel abbreviation</par
        /// <param name="programFY">Program's fiscal year</param>
        /// <param name="programDescription">Program's description</param>
        /// <param name="comments">Transfer comments</param>
        /// <returns>Formatted body of email request</returns>
        public string TransferReviewerRequest(int fromUserId, string reviewerName, string sourcePanelName, string sourcePanelAbbr, 
            string targetPanelName, string targetPanelAbbr, string programFY, string programDescription, string comments)
        {
            ValidateTransferReviewerRequest(fromUserId, reviewerName, sourcePanelName, sourcePanelAbbr,
                    targetPanelName, targetPanelAbbr, programFY, programDescription);

            string[] subjectParas = new string[] { reviewerName, sourcePanelAbbr, targetPanelAbbr };
            string[] contentParas = new string[] { reviewerName, sourcePanelName, targetPanelName, programFY, programDescription, comments };

            string content = GetTransferRequestContent(SystemTemplate.RequestReviewerTransfer, fromUserId, subjectParas, contentParas, comments, "TransferReviewerRequest");
            return content;
        }
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
        public string ReleaseReviewerRequest(int fromUserId, int sourcePanelId, string reviewerName, string sourcePanelName, string sourcePanelAbbr, string programFY, string programDescription, string comments)
        {
            ValidateReleaseReviewerRequest(fromUserId, reviewerName, sourcePanelName, sourcePanelAbbr, programFY, programDescription);

            string[] subjectParas = new string[] { reviewerName, sourcePanelAbbr};
            string[] contentParas = new string[] { string.Empty, reviewerName, sourcePanelName, programFY, comments, string.Empty };

            string content = ProcessAndSendReleaseRequest(SystemTemplate.RequestReviewerRelease, fromUserId, sourcePanelId, subjectParas, contentParas, comments, "TransferReviewerRequest");
            return content;
        }

        /// <summary>
        /// Create an email message for the release reviewer request & mail it.
        /// </summary>
        /// <param name="fromUserId">User identifier of the SRO who is initiating the release</param>        
        /// <param name="reviewerName">Reviewer's name</param>
        /// <param name="sourcePanelName">Source panel name</param>
        /// <param name="sourcePanelAbbr">Source panel abbreviation</param>
        /// <param name="programFY">Program's fiscal year</param>
        /// <param name="programDescription">Program's description</param>
        /// <param name="comments">Release comments</param>
        /// <returns>Formatted body of email request</returns>
        public string ReleaseReviewerRequest(int fromUserId, string reviewerName, string sourcePanelName, string sourcePanelAbbr,
            string programFY, string programDescription, string comments)
        {
            ValidateReleaseReviewerRequest(fromUserId, reviewerName, sourcePanelName, sourcePanelAbbr,
                programFY, programDescription);

            string[] subjectParas = new string[] { reviewerName, sourcePanelAbbr };
            string[] contentParas = new string[] { reviewerName, sourcePanelName, programFY, programDescription, comments };

            string content = GetTransferRequestContent(SystemTemplate.RequestReviewerTransfer, fromUserId, subjectParas, contentParas, comments, nameof(ReleaseReviewerRequest));
            return content;
        }
        #endregion
        #region Send Credentials
        /// <summary>
        /// Send a credential email without a password.
        /// </summary>
        /// <param name="templateName">Template name</param>
        /// <param name="user">Email addressee</param>
        /// <returns>Status of mail request</returns>
        public virtual MailStatus SendCredential(string templateName, User user)
        {
            return SendCredential(templateName, string.Empty, user);
        }
        /// <summary>
        /// Send a credential email with a password.
        /// </summary>
        /// <param name="templateName">Template name</param>
        /// <param name="password">Clear text password</param>
        /// <param name="user">Email addressee</param>
        /// <returns>Status of mail request</returns>
        public virtual MailStatus SendCredential(string templateName, string password, User user)
        {
            SystemTemplateVersion template = ManageSystemTemplates.GetSystemTemplateVersion(templateName);

            MailMergeFieldBuilder mergeFieldsBldr = new MailMergeFieldBuilder(user, null);

            //
            // populate the template
            //
            string mailbody = template.Body;
            mergeFieldsBldr.SetTemporaryPassword(password);

            string finalMailBody = mergeFieldsBldr.MergeFieldProcesser(mailbody);
            //
            // Now mail the message
            //
            return PostLetter(ConfigManager.SystemEmailAddress, user.PrimaryUserEmailAddress(), template.Subject, finalMailBody);
        }
        /// <summary>
        /// Send a email.
        /// </summary>
        /// <param name="senderEmail">From address</param>
        /// <param name="recipientEmail">Semicolon separated list of recipient addresses </param>
        /// <param name="subject">Email subject</param>
        /// <param name="message">Email message</param>
        /// <returns>Status of mail request</returns>
        internal virtual MailStatus PostLetter(string senderEmail, string recipientEmail, string subject, string message)
        {
            return PostLetter(senderEmail, recipientEmail, null, null, subject, message, new List<AttachmentToSend>());
        }
        /// <summary>
        /// Send a email.
        /// </summary>
        /// <param name="senderEmail">From address</param>
        /// <param name="recipientEmail">Semicolon separated list of recipient addresses </param>
        /// <param name="ccEmail">Semicolon separated list of recipient CC addresses</param>
        /// <param name="bccEmailAddresses">Single BCC address</param>
        /// <param name="subject">Email subject</param>
        /// <param name="message">Email message</param>
        /// <param name="attachments">Collection of attachments</param>
        /// <returns>Status of mail request</returns>
        internal virtual MailStatus PostLetter(string senderEmail, string recipientEmail, string ccEmail, string bccEmailAddresses, string subject, string message, ICollection<AttachmentToSend> attachments)
        {
            MailStatus result = MailStatus.Failure;
            //
            // In addition to the general validation or parameters we need to ensure the total size of all attachments
            // do not exceed a maximum value.  Test that here.
            //
            result = CheckAttachmentsSizeDoNotValidateMaximum(attachments);
            if (result == MailStatus.Default)
            {
                //
                // Create & format the letter, but first we need to deal with any attachments. 
                // 
                Mailer letter = WriteLetter(senderEmail, recipientEmail, ccEmail, bccEmailAddresses, subject, message, attachments);
                result = (letter.Send()) ? MailStatus.Success : MailStatus.FailedToSend;
            }

            return result;
        }
        /// <summary>
        /// Sends an email to a user informing them that they have been assigned to this panel as a reviewer
        /// </summary>
        /// <param name="systemTemplateNAme">The system template name</param>
        /// <param name="reviewerId">The user identifier for the reviewer</param>
        /// <param name="sessionPanelId">The session panel identifier</param>
        /// <param name="userId">The user identifier for the user making the assignment</param>
        /// <returns>MailStatus value indicating the status of the service request </returns>
        internal MailStatus SendPanelAssignmentEmail(string systemTemplateName, int reviewerId, int sessionPanelId, int userId)
        {
            //
            // Get the user who is sending this email
            //
            User reviewer = UnitOfWork.UofwUserRepository.GetUserByID(reviewerId);

            MailMergeFieldBuilder mergeFieldsBldr = new MailMergeFieldBuilder(reviewer, null);

            SessionPanel panel = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            ProgramYear py = panel.GetProgramYear();

            mergeFieldsBldr.SetParticipantType(panel.GetUsersParticipantType(reviewerId));
            mergeFieldsBldr.SetPanelName(panel.PanelName);
            mergeFieldsBldr.SetPanelAbbrev(panel.PanelAbbreviation);

            string year = py != null ? py.Year : string.Empty;
            string programDescription = py != null ? py.ClientProgram.ProgramDescription : string.Empty;
            mergeFieldsBldr.SetFY(year);
            mergeFieldsBldr.SetProgramName(programDescription);

            // from inside address
            mergeFieldsBldr.SetCompanyName(ConfigurationManager.AppSettings["CompanyName"]);
            mergeFieldsBldr.SetCompanyDivision(ConfigurationManager.AppSettings["CompanyDivision"]);
            mergeFieldsBldr.SetCompanyAddress1(ConfigurationManager.AppSettings["CompanyAddress1"]);
            mergeFieldsBldr.SetCompanyAddress2(ConfigurationManager.AppSettings["CompanyAddress2"]);
            mergeFieldsBldr.SetCompanyCity(ConfigurationManager.AppSettings["CompanyCity"]);
            mergeFieldsBldr.SetCompanyPhone(ConfigurationManager.AppSettings["CompanyPhone"]);
            mergeFieldsBldr.SetCompanyFax(ConfigurationManager.AppSettings["CompanyFax"]);

            SystemTemplateVersion template = ManageSystemTemplates.GetSystemTemplateVersion(systemTemplateName);
            //
            // populate the template
            //
            string mailbody = template.Body;
            string finalMailBody = mergeFieldsBldr.MergeFieldProcesser(mailbody);
            string subject = string.Format(template.Subject, year, programDescription);


            // obtain email addresses for this sessionPanelId and the collection of selected panel user assignments
            Container<IEmailAddress> cc = ListPanelSroRtaEmailAddresses(sessionPanelId);

            List<string> ccEmail = cc.ModelList.Select(x => x.UserEmailAddress).ToList();
            string formatedCcEmailAddresses = FormatCommunicationsList(ccEmail);

            return PostLetter(ConfigManager.HelpDeskEmailAddress, reviewer.PrimaryUserEmailAddress(), formatedCcEmailAddresses, null, subject, finalMailBody, new List<AttachmentToSend>());
        }

        #endregion
        #region MOD Discussion Notification

        /// <summary>
        /// Sends the discussion notification email.
        /// </summary>
        /// <param name="templateData">The template data.</param>
        /// <returns>MailStatus object indicating if email was successful</returns>
        public MailStatus SendDiscussionNotification(IDiscussionNotificationModel templateData)
        {
            //
            // Get the template for the email
            //
            int discussionTemplateId = 0;
            ManageSystemTemplates sysTemplateMgr = new ManageSystemTemplates();
            if(templateData.IsNewOnlineDiscussion == true)
            {
                discussionTemplateId = SystemTemplate.Indexes.StartingNewDiscussion;
            }
            else
            {
                discussionTemplateId = SystemTemplate.Indexes.DiscussionBoardCommentTemplate;
            }
            var theTemplate = sysTemplateMgr.GetSystemTemplateVersionById(discussionTemplateId);
            // Populate the subject
            string subject = FormatDiscussionEmailSubject(theTemplate.Subject, templateData.FiscalYear,
                templateData.ProgramAbbreviation, templateData.PanelAbbreviation, templateData.LogNumber);
            // Populate the template
            string body = FormatDiscussionEmailBody(theTemplate.Body, templateData.ParticipantPrefix,
                templateData.ParticipantFirstName, templateData.ParticipantLastName,
                FormatDiscussionAssignment(templateData.AuthorParticipantType, templateData.AuthorAssignmentOrder,
                    templateData.AuthorParticipantRole, templateData.AuthorSystemRole), ViewHelpers.FormatDiscussionDateTime(templateData.CommentDateTime),
                    templateData.LogNumber, templateData.ApplicationTitle, templateData.FiscalYear, templateData.ProgramAbbreviation, templateData.PanelAbbreviation,
                    ViewHelpers.ConstructSiteUrl(ConfigManager.UrlScheme, ConfigManager.UrlHost), ViewHelpers.FormatDiscussionDateTime(templateData.PhaseEndDateTime), ConfigManager.HelpDeskEmailAddress,
                    ConfigManager.HelpDeskPhoneNumber);
            // Send
            return PostLetter(ConfigManager.HelpDeskEmailAddress, templateData.ParticipantEmail, subject, body);
        }

        #endregion
        #region Task tracking notification

        /// <summary>
        /// Sends a ticket notification email upon ticket creation.
        /// </summary>
        /// <param name="requestorEmail">The requestor email.</param>
        /// <param name="newTicketId">The new ticket identifier.</param>
        /// 
        /// <returns>MailStatus regarding whether the email was sent successfully.</returns>
        public MailStatus SendTicketNotification(string requestorEmail, string newTicketId)
        {
            ManageSystemTemplates sysTemplateMgr = new ManageSystemTemplates();
            var theTemplate = sysTemplateMgr.GetSystemTemplateVersionById(SystemTemplate.Indexes.NewTicketCreated);
            string subject = string.Format(theTemplate.Subject, newTicketId);
            string ticketUrl = string.Concat(ConfigManager.UrlScheme, ConfigManager.UrlHost, "/TaskTracking/EditRequest/", newTicketId);
            string body = string.Format(theTemplate.Body, newTicketId, ticketUrl);

            return PostLetter(ConfigManager.SystemEmailAddress, requestorEmail, subject, body);
        }
        #endregion

        #region Password notification

        /// <summary>
        /// Sends notification email upon passsord change.
        /// </summary>
        /// <param name="templateName">Template Name to be used for email.</param>
        /// <param name="user">Current user details.</param>
        /// 
        /// <returns>MailStatus regarding whether the email was sent successfully.</returns>
        public virtual MailStatus SendPasswordChangeNotification(string templateName, User user)
        {
            SystemTemplateVersion template = ManageSystemTemplates.GetSystemTemplateVersion(templateName);

            MailMergeFieldBuilder mergeFieldsBldr = new MailMergeFieldBuilder(user, null);
            //Set Date password will expire
            mergeFieldsBldr.SetPasswordExpirationDate(ViewHelpers.FormatDate(GlobalProperties.P2rmisDateTimeNow.AddDays(ConfigManager.PwdNumberDaysBeforeExpire)));
            //
            // populate the template
            //            
            string finalMailBody = mergeFieldsBldr.MergeFieldProcesser(template.Body);            
            // Now mail the message
            //
            return PostLetter(ConfigManager.SystemEmailAddress, user.PrimaryUserEmailAddress(), template.Subject, finalMailBody);
        }
        #endregion
        #endregion
        #region Helpers

        private string GetTransferRequestContent(string templateName, int fromUserId, string[] subjectParas, string[] contentParas, string comments, string emailType)
        {
            //
            // Get the user who is sending this email
            //
            User user = UnitOfWork.UofwUserRepository.GetUserByID(fromUserId); 
            //
            // Get the template for the email
            //
            ManageSystemTemplates sysTemplateMgr = new ManageSystemTemplates();
            var templateToSend = sysTemplateMgr.GetSystemTemplateByName(templateName);
            SystemTemplateVersion verToSend = sysTemplateMgr.GetActiveVersion(templateToSend);
            //
            // Now gather the information to be placed in the template
            //
            string userName = user.UserInfoes.Single().FullUserName;
            comments = (string.IsNullOrWhiteSpace(comments)) ? UseThisWhenNoComment : comments.Trim();

            string subject = string.Format(verToSend.Subject, subjectParas);
            string[] newContentParas = new string[contentParas.Length + 1];
            newContentParas[0] = userName;
            contentParas[5] = (string.IsNullOrWhiteSpace(comments)) ? UseThisWhenNoComment : comments.Trim();
            Array.Copy(contentParas, 0, newContentParas, 1, contentParas.Length);

            string content = string.Format(verToSend.Body, newContentParas);
            string from = user.UserInfoes.Single().UserEmails.FirstOrDefault(e => e.PrimaryFlag == true).Email;
            //
            // Send the email as HTML
            //
            SentHtmlMessage(from, ConfigManager.HelpDeskEmailAddress, subject, content, emailType);
		
            return content;
        }
        /// <summary>
        /// Gets the content of the critique reset.
        /// </summary>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="fromUserId">From user identifier.</param>
        /// <param name="reviewerEmail">The reviewer email.</param>
        /// <param name="subjectParas">The subject paras.</param>
        /// <param name="contentParas">The content paras.</param>
        /// <param name="emailType">Type of the email.</param>
        /// <returns></returns>
        private string GetCritiqueResetContent(string templateName, int fromUserId, string reviewerEmail, string[] subjectParas, string[] contentParas, string emailType)
        {
            //
            // Get the user who is sending this email
            //
            User user = UnitOfWork.UofwUserRepository.GetUserByID(fromUserId);
            //
            // Get the template for the email
            //
            ManageSystemTemplates sysTemplateMgr = new ManageSystemTemplates();
            var templateToSend = sysTemplateMgr.GetSystemTemplateByName(templateName);
            SystemTemplateVersion verToSend = sysTemplateMgr.GetActiveVersion(templateToSend);
            //
            // Now gather the information to be placed in the template
            //
            string subject = string.Format(verToSend.Subject, subjectParas);
            string content = string.Format(verToSend.Body, contentParas);
            string from = user.UserInfoes.Single().UserEmails.FirstOrDefault(e => e.PrimaryFlag == true).Email;
            //
            // Send the email as HTML
            //
            SentHtmlMessage(from, reviewerEmail, subject, content, emailType);

            return content;
        }
        /// <summary>
        /// Populates the release request template with specifics and sends the message to the help desk
        /// </summary>
        /// <param name="templateName">Template name</param>
        /// <param name="fromUserId">User entity identifier of the sender</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier of the source panel</param>
        /// <param name="subjectParas">Subject line content parameters</param>
        /// <param name="contentParas">Body content parameters</param>
        /// <param name="comments">Any comments entered by the requestor</param>
        /// <param name="emailType">Email type (for error message)</param>
        /// <returns></returns>
        private string ProcessAndSendReleaseRequest(string templateName, int fromUserId, int sessionPanelId, string[] subjectParas, string[] contentParas, string comments, string emailType)
        {
            //
            // Get the user who is sending this email
            //
            User user = UnitOfWork.UofwUserRepository.GetUserByID(fromUserId);
            //
            // Get the template for the email
            //
            ManageSystemTemplates sysTemplateMgr = new ManageSystemTemplates();
            var templateToSend = sysTemplateMgr.GetSystemTemplateByName(templateName);
            SystemTemplateVersion verToSend = sysTemplateMgr.GetActiveVersion(templateToSend);
            //
            // Now gather the information to be placed in the template
            //

            string subject = string.Format(verToSend.Subject, subjectParas);
            //
            // Several of the message parameters need to be pre-processed
            //
            contentParas[0] = user.FullName();
            contentParas[4] = (string.IsNullOrWhiteSpace(comments)) ? UseThisWhenNoComment : comments.Trim();
            contentParas[5] = user.RetrieveSessionPanelByPanelid(sessionPanelId)?.ClientParticipantType.ParticipantTypeAbbreviation;
            //
            // Populate the templates with the content
            //
            string content = string.Format(verToSend.Body, contentParas);
            string from = user.UserInfoes.Single().UserEmails.FirstOrDefault(e => e.PrimaryFlag == true).Email;
            //
            // Send the email as HTML
            //
            SentHtmlMessage(from, ConfigManager.HelpDeskEmailAddress, subject, content, emailType);

            return content;
        }
        /// <summary>
        /// Checks to see if the total size of all attachments exceeds a configurable maximum.
        /// </summary>
        /// <param name="files">Attachments to send</param>
        /// <returns>MailStatus value indicating if the attachments exceeded the size</returns>
        internal virtual MailStatus CheckAttachmentsSizeDoNotValidateMaximum(ICollection<AttachmentToSend> attachments)
        {
            //
            // Go through each attachment and add the stream length to the total
            //
            long totalSize = 0;
            attachments.ToList().ForEach(q => totalSize += q.Length);
            return (totalSize > MaximumAttachmentSize) ? MailStatus.AttachmentsExceedSize : MailStatus.Default;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Sets the mailer object addresses.
        /// </summary>
        /// <param name="letter">Mailer object</param>
        /// <param name="from">From address</param>
        /// <param name="to">To address</param>
        private void SetAddresses(Mailer letter, string from, string to)
        {
            letter.FromAddress = from;
            letter.AddToAddress(to);
        }
        /// <summary>
        /// Sets the mailer object content.
        /// </summary>
        /// <param name="letter">Mailer object</param>
        /// <param name="subject">Email subject</param>
        /// <param name="content">Email content</param>
        private void SetContent(Mailer letter, string subject, string content)
        {
            letter.Subject = subject;
            letter.Body = content;
        }
        /// <summary>
        /// Constructs & Sends an email as HTML.
        /// </summary>
        /// <param name="from">From address</param>
        /// <param name="to">To address</param>
        /// <param name="subject">Email subject</param>
        /// <param name="content">Email content</param>
        /// <param name="typeOfEmail">Readable type of email for exception message</param>
        /// <exception cref="Exception">Exception is thrown if mailer returned indication that it failed to send the email</exception>
        public virtual void SentHtmlMessage(string from, string to, string subject, string content, string typeOfEmail)
        {
            //
            // Create the mailer to send the email and set the addresses & content
            //
            Mailer letter = new Mailer { IsBodyHtml = true };
            SetAddresses(letter, from, to);
            SetContent(letter, subject, content);
            //
            // Try to send the email
            //
            string errorMessage = string.Empty;
            errorMessage = letter.SendReturnMsg();
            if (errorMessage != string.Empty)
            {
                throw new Exception(string.Format(FailToSendMessage + errorMessage, typeOfEmail) );
            }
            letter = null;
        }
        /// <summary>
        /// Validates the parameters for ListPanelUserEmailAddresses.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <exception cref="ArgumentException">Thrown if sessionPanelId is invalid (<0)</exception>
        private void ValidateListPanelUserEmailAddressesParameters(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "MailService.ListPanelUserEmailAddresses", "sessionPanelId");
        }
        /// <summary>
        /// Validates the parameters for ListPanelReviewersEmailAddresses.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <exception cref="ArgumentException">Thrown if sessionPanelId is invalid (<0)</exception>
        private void ValidateListPanelReviewersEmailAddressesParameters(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "MailService.ListPanelReviewersEmailAddresses", "sessionPanelId");
        }
        /// <summary>
        /// Validates the parameters for ListPanelSroRtaEmailAddresses.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <exception cref="ArgumentException">Thrown if sessionPanelId is invalid (<0)</exception>
        private void ValidateListPanelSroRtaEmailAddressesAddressesParameters(int sessionPanelId)
        {
            this.ValidateInteger(sessionPanelId, "MailService.ListPanelSroRtaEmailAddresses", "sessionPanelId");
        }
        /// <summary>
        /// Validates the parameters for TransferRequest
        /// </summary>
        /// <param name="fromUserId">User identifier of the SRO who is initiating the transfer</param>
        /// <param name="applicationLogNumber">Application log number</param>
        /// <param name="sourcePanel">Source panel</param>
        /// <param name="targetPanel">Target panel</param>
        /// <param name="fullPanelName">The full source panel name (drop down selection)</param>
        /// <param name="transferReason">Why the panel is being transferred</param>
        private void ValidateTransferRequest(int fromUserId, string applicationLogNumber, string sourcePanel, string targetPanel,
                   string fullPanelName, string transferReason)
        {
            this.ValidateInteger(fromUserId, "MailService.TransferRequest", "fromUserId");
            this.ValidateString(applicationLogNumber, "MailService.TransferRequest", "applicationLogNumber");
            this.ValidateString(sourcePanel, "MailService.TransferRequest", "sourcePanel");
            this.ValidateString(targetPanel, "MailService.TransferRequest", "targetPanel");
            this.ValidateString(fullPanelName, "MailService.TransferRequest", "fullPanelName");
            this.ValidateString(transferReason, "MailService.TransferRequest", "transferReason");
        }
        /// <summary>
        /// Validates the critique reset.
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
        private void ValidateCritiqueReset(int fromUserId, string reviewerName, string reviewerEmail, string programYear, string programFullName, 
                string panelFullName, string panelAbbreviation, string applicationLogNumber, string sroEmail)
        {
            this.ValidateInteger(fromUserId, "MailService.CritiqueReset", "fromUserId");
            this.ValidateString(reviewerName, "MailService.CritiqueReset", "reviewerName");
            this.ValidateString(reviewerEmail, "MailService.CritiqueReset", "reviewerEmail");
            this.ValidateString(programYear, "MailService.CritiqueReset", "programYear");
            this.ValidateString(programFullName, "MailService.CritiqueReset", "programFullName");
            this.ValidateString(panelFullName, "MailService.CritiqueReset", "panelFullName");
            this.ValidateString(panelAbbreviation, "MailService.CritiqueReset", "panelAbbreviation");
            this.ValidateString(applicationLogNumber, "MailService.CritiqueReset", "applicationLogNumber");
            this.ValidateString(sroEmail, "MailService.CritiqueReset", "sroEmail");
        }
        /// <summary>
        /// Validates the parameters for TransferReviewerRequest
        /// </summary>
        /// <param name="fromUserId">User identifier of the SRO who is initiating the transfer</param>        
        /// <param name="reviewerName">Reviewer's name</param>
        /// <param name="sourcePanelName">Source panel name</param>
        /// <param name="sourcePanelAbbr">Source panel abbreviation</param>
        /// <param name="targetPanelName">Target panel name</param>
        /// <param name="targetPanelAbbr">Target panel abbreviation</par
        /// <param name="programFY">Program's fiscal year</param>
        /// <param name="programDescription">Program's description</param>
        private void ValidateTransferReviewerRequest(int fromUserId, string reviewerName, string sourcePanelName, string sourcePanelAbbr, 
            string targetPanelName, string targetPanelAbbr, string programFY, string programDescription)
        {
            this.ValidateInteger(fromUserId, "MailService.TransferReviewerRequest", "fromUserId");
            this.ValidateString(reviewerName, "MailService.TransferReviewerRequest", "reviewerName");
            this.ValidateString(sourcePanelName, "MailService.TransferReviewerRequest", "sourcePanelName");
            this.ValidateString(sourcePanelAbbr, "MailService.TransferReviewerRequest", "sourcePanelAbbr");
            this.ValidateString(targetPanelName, "MailService.TransferReviewerRequest", "targetPanelName");
            this.ValidateString(targetPanelAbbr, "MailService.TransferReviewerRequest", "targetPanelAbbr");
            this.ValidateString(programFY, "MailService.TransferReviewerRequest", "programFY");
            this.ValidateString(programDescription, "MailService.TransferReviewerRequest", "programDescription");
        }
        /// <summary>
        /// Validates the parameters for TransferReviewerRequest
        /// </summary>
        /// <param name="fromUserId">User identifier of the SRO who is initiating the transfer</param>        
        /// <param name="reviewerName">Reviewer's name</param>
        /// <param name="sourcePanelName">Source panel name</param>
        /// <param name="sourcePanelAbbr">Source panel abbreviation</param>
        /// <param name="programFY">Program's fiscal year</param>
        /// <param name="programDescription">Program's description</param>
        private void ValidateReleaseReviewerRequest(int fromUserId, string reviewerName, string sourcePanelName, string sourcePanelAbbr,
            string programFY, string programDescription)
        {
            this.ValidateInteger(fromUserId, "MailService.TransferReviewerRequest", "fromUserId");
            this.ValidateString(reviewerName, "MailService.TransferReviewerRequest", "reviewerName");
            this.ValidateString(sourcePanelName, "MailService.TransferReviewerRequest", "sourcePanelName");
            this.ValidateString(sourcePanelAbbr, "MailService.TransferReviewerRequest", "sourcePanelAbbr");
            this.ValidateString(programFY, "MailService.TransferReviewerRequest", "programFY");
            this.ValidateString(programDescription, "MailService.TransferReviewerRequest", "programDescription");
        }
        /// <summary>
        /// Validates the parameters for SendEmail
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="senderEmail">From address</param>
        /// <param name="recipientEmail">Semicolon separated list of recipient addresses </param>
        /// <param name="ccEmail">Semicolon separated list of recipient CC addresses</param>
        /// <param name="bccEmailAddresses">Single BCC address</param>
        /// <param name="subject">Subject text</param>
        /// <param name="message">Body text</param>
        /// <param name="attachments">Representation of attachments to send</param>
        /// <param name="userId">User identifier</param>
        /// <exception cref="Exception">Exception is thrown if any parameter is invalid</exception>
        private void ValidateSendEmailParameters(int sessionPanelId, ICollection<int> panelUserAssignmentId, string senderEmail, string recipientEmail, string ccEmail, string bccEmailAddresses, string subject, string message, ICollection<AttachmentToSend> attachments, int userId)
        {
            this.ValidateInteger(sessionPanelId, "MailService.SendEmail", "sessionPanelId");
            //
            // still need need to validate the collection:
            // same number of recipient addresses
            //
            panelUserAssignmentId.ToList().ForEach(x => this.ValidateInteger(x, "MailService.SendEmail", "panelUserAssignmentId"));
            this.ValidateString(senderEmail, "MailService.SendEmail", "senderEmail");
            this.ValidateString(recipientEmail, "MailService.SendEmail", "recipientEmail");
            this.ValidateString(subject, "MailService.SendEmail", "subject");
            this.ValidateString(message, "MailService.SendEmail", "message");
            this.ValidateInteger(userId, "MailService.SendEmail", "sessionPanelId");
        }

        /// <summary>
        /// Formats a list of string into a semi colin delinerated string
        /// </summary>
        /// <param name="list">The list of strings</param>
        /// <returns>A semi colin delinerated string</returns>
        private string FormatCommunicationsList(List<string> list)
        {
            StringBuilder sb = new StringBuilder();
            list.ForEach(x => { if (string.IsNullOrWhiteSpace(x)) { list.Remove(x); } else { sb.AppendFormat("{0};", x); } } );

            // remove trailing ';'
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Formats the discussion email subject with data.
        /// </summary>
        /// <param name="templateSubject">The template subject.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programAbbreviation">The program abbreviation.</param>
        /// <param name="panelAbbreviation">The panel abbreviation.</param>
        /// <param name="logNumber">The log number.</param>
        /// <returns>Formatted subject</returns>
        internal string FormatDiscussionEmailSubject(string templateSubject, string fiscalYear, string programAbbreviation, string panelAbbreviation, string logNumber)
        {
            return string.Format(templateSubject, fiscalYear, programAbbreviation, panelAbbreviation, logNumber);
        }

        /// <summary>
        /// Formats the discussion email body template with data.
        /// </summary>
        /// <param name="templateBody">The template body.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="reviewerAssignment">The reviewer assignment.</param>
        /// <param name="commentModifiedDateTime">The comment modified date time.</param>
        /// <param name="logNumber">The log number.</param>
        /// <param name="applicationTitle">The application title.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programAbbreviation">The program abbreviation.</param>
        /// <param name="panelAbbreviation">The panel abbreviation.</param>
        /// <param name="siteUrl">The site URL.</param>
        /// <param name="phaseEndDate">The phase end date.</param>
        /// <param name="helpdeskEmail">The helpdesk email.</param>
        /// <param name="helpdeskPhoneNumber">The helpdesk phone number.</param>
        /// <returns>Formatted email body</returns>
        internal string FormatDiscussionEmailBody(string templateBody, string prefix, string firstName, string lastName,
                    string reviewerAssignment, string commentModifiedDateTime, string logNumber, string applicationTitle,
                    string fiscalYear, string programAbbreviation, string panelAbbreviation, string siteUrl, string phaseEndDate,
                    string helpdeskEmail, string helpdeskPhoneNumber)
        {
            return string.Format(templateBody, prefix, firstName, lastName, reviewerAssignment, commentModifiedDateTime,
                logNumber, applicationTitle,
                fiscalYear, programAbbreviation, panelAbbreviation, siteUrl, phaseEndDate, helpdeskEmail,
                helpdeskPhoneNumber);
        }
        /// <summary>
        /// Formats the discussion assignment string based on the context of the assignment.
        /// </summary>
        /// <param name="authorParticipantType">Type of the author participant.</param>
        /// <param name="authorAssignmentOrder">The author assignment order.</param>
        /// <param name="authorParticipantRole">The author participant role.</param>
        /// <param name="authorSystemRole">The author system role.</param>
        /// <returns>Formatted assignment string</returns>
        internal string FormatDiscussionAssignment(string authorParticipantType, int? authorAssignmentOrder, string authorParticipantRole, string authorSystemRole)
        {
            string assignmentOrderFormatted = authorAssignmentOrder != null ? $"({authorAssignmentOrder})" : string.Empty;
            string participantRoleFormatted = authorParticipantRole != null ? $" - {authorParticipantRole}" : string.Empty;
            return $"{authorParticipantType ?? authorSystemRole}{assignmentOrderFormatted}{participantRoleFormatted}";
        }
        #endregion
    }
}
