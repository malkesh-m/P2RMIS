using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.Mail
{
    /// <summary>
    /// Constructs a PanelManagement letter
    /// </summary>
    internal class LetterBuilder
    {
        #region Constructors & setup
        /// <summary>
        /// Base constructor initializes properties to a know state.
        /// </summary>
        private LetterBuilder()
        {
            this.FromAddress = string.Empty;
            this.ToAddress = string.Empty;
            this.Subject = string.Empty;
            this.Content = string.Empty;
            this.AdditionalRecipentsLine = string.Empty;
            this.Attachments = new List<AttachmentToSend>();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fromAddress">Sender email address</param>
        /// <param name="subject">Subject of email</param>
        /// <param name="content">Email content</param>
        /// <param name="attachments">Attachments</param>
        public LetterBuilder(string fromAddress, string subject, string content, ICollection<AttachmentToSend> attachments) : this()
        {
            this.FromAddress = fromAddress;
            this.Subject = subject;
            this.Content = content;
            this.Attachments = attachments;
        }
        /// <summary>
        /// Resets the builder contents to be used with a different address.
        /// </summary>
        /// <param name="toAddress">New recipient address</param>
        public void Reset(string toAddress)
        {
            this.ToAddress = toAddress;
        }
        /// <summary>
        /// Set/Reset the list of additional recipients.
        /// </summary>
        /// <param name="additionalRecipents">Collection of additional recipients</param>
        public void SetAdditionalRecipients(ICollection<string> additionalRecipents)
        {
            string listOfRecipients = string.Join(", ", additionalRecipents);
            this.AdditionalRecipentsLine = string.Concat(HtmlServices.SurroundWithParagraphMarkers(MessageService.RecipientListMessage),
                                                         HtmlServices.SurroundWithParagraphMarkers(listOfRecipients));
        }
        #endregion
        #region Properties
        /// <summary>
        /// Sender email address
        /// </summary>
        public string FromAddress { get; private set; }
        /// <summary>
        /// Recipient address<
        /// </summary>
        public string ToAddress { get; private set; }
        /// <summary>
        /// Email content
        /// </summary>
        public string Content { get; private set; }
        /// <summary>
        /// Subject of email
        /// </summary>
        public string Subject { get; private set; }
        /// <summary>
        /// Attachments
        /// </summary>
        public ICollection<AttachmentToSend> Attachments { get; private set; }
        /// <summary>
        /// Resulting letter
        /// </summary>
        public Mailer Letter { get; private set; }
        /// <summary>
        /// Concatenation of additional recreants email addresses
        /// </summary>
        private string AdditionalRecipentsLine { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// Constructs a letter
        /// </summary>
        public void Build()
        {
            //
            // Create the mailer object that does all the heavy lifting for us
            //
            Letter = new Mailer { IsBodyHtml = true };
            //
            // Now we add things
            //
            Letter.FromAddress = FromAddress;
            Letter.AddToAddress(ToAddress);
            Letter.Subject = Subject;
            Letter.Body = string.Concat(this.Content, AdditionalRecipentsLine);
            //
            // If there are any attachments add them now
            //
            Attachments.ToList().ForEach(x =>
            {
                x.FileStream.Position = 0;
                Letter.AddAttachment(x.FileStream, x.FileName);
            });
        }
        #endregion
    }
}