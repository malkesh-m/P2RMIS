using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Net.Mail;
using System.Runtime.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Sra.P2rmis.CrossCuttingServices
{
    /// <summary>
    /// An exception indicating that the caller supplied an email address in an invalid format
    /// </summary>
    [Serializable]
    public class InvalidEmailAddressException : Exception
    {
        const string message = "The supplied email address {0} is in an invalid format.  See InnerException property for more details.";

        /// <summary>
        /// Standard constructor for Exception classes
        /// </summary>
        public InvalidEmailAddressException() : base(message, null) { }

        /// <summary>
        /// Standard constructor for Exception classes
        /// </summary>
        /// <param name="msg"></param>
        public InvalidEmailAddressException(string msg) : base(msg, null) {}

        /// <summary>
        /// Standard constructor for Exception classes
        /// </summary>
        /// <param name="e">The .NET-generated exception that occurred when validating the email address</param>
        public InvalidEmailAddressException(Exception e) : base(message, e) { }

        /// <summary>
        /// Constructor for the exception
        /// </summary>
        /// <param name="invalidEmail">The text of the invalid email address</param>
        /// <param name="e">The .NET-generated exception that occurred when validating the email address</param>
        public InvalidEmailAddressException(string invalidEmail, Exception e) : base(string.Format(CultureInfo.CurrentCulture, message, invalidEmail), e) {}

        /// <summary>
        /// Standard constructor for Exception classes
        /// </summary>
        /// <param name="info">Serialization information for this class</param>
        /// <param name="sc">Contains contextual information about the source or destination</param>
        protected InvalidEmailAddressException(SerializationInfo info, StreamingContext sc) : base(info, sc) { }

        /// <summary>
        /// Required method for Serializable object
        /// </summary>
        /// <param name="info">Serialization information for this class</param>
        /// <param name="context">Contains contextual information about the source or destination</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }

    /// <summary>
    /// A class that can construct and send an email message
    /// </summary>
    public class Mailer
    {
        #region Private members
        private MailAddress _fromAddress;
        private MailAddressCollection _toAddresses;
        private MailAddressCollection _ccAddresses;
        private MailAddressCollection _bccAddresses;
        private MailAddressCollection _replyToAddresses;
        private string _subject;
        private string _body;
        private bool _isBodyHtml;  // default is false
        private char[] AddressListDelimiters = new char[2] { ';', ',' };
        private Collection<Attachment> _attachments;
        const String OverrideAddressKey = "recipient-override";
        private static LogEntry logMsg;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes static members of the class
        /// </summary>
        static Mailer()
        {
            logMsg = new LogEntry();
            logMsg.Priority = 100;
            logMsg.Severity = TraceEventType.Error;
            logMsg.EventId = 102;
        }

        /// <summary>
        /// Initializes an empty instance of the Mailer class
        /// </summary>
        public Mailer()
        {
            _toAddresses = new MailAddressCollection();
            _ccAddresses = new MailAddressCollection();
            _bccAddresses = new MailAddressCollection();
            _replyToAddresses = new MailAddressCollection();
            _attachments = new Collection<Attachment>();
        }
        #endregion

        #region Public properties
        /// <summary>
        /// The email address of the sender
        /// </summary>
        public string FromAddress
        {
            get { return _fromAddress.ToString(); }
            set { _fromAddress = CreateMailAddress(value); }
        }

        /// <summary>
        /// The subject line of the email message
        /// </summary>
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        /// <summary>
        /// The body of the email message
        /// </summary>
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        /// <summary>
        /// True if the message body is in HTML, otherwise false.  Default is false.
        /// </summary>
        public bool IsBodyHtml
        {
            get { return _isBodyHtml; }
            set { _isBodyHtml = value; }
        }

        /// <summary>
        /// Returns the email address used as the From address for emails coming from the PRIMO system.  Set in the application configuration file.
        /// </summary>
        public static string SystemFromAddress
        {
            get 
            { 
                try
                {
                    string fromAddress = ConfigurationManager.AppSettings["system-email-address"];
                    if (string.IsNullOrEmpty(fromAddress))
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return fromAddress;
                    }
                }
                catch (ConfigurationErrorsException e)
                {
                    logMsg.Message = e.Message;
                    Logger.Write(logMsg);
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the email address used as the Reply-To address for emails coming from the PRIMO system.  Set in the application configuration file.
        /// </summary>
        public static string SystemReplyToAddress
        {
            get
            {
                try
                {
                    string replyAddress = ConfigurationManager.AppSettings["system-reply-to-email-address"];
                    if (string.IsNullOrEmpty(replyAddress))
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return replyAddress;
                    }
                }
                catch (ConfigurationErrorsException e)
                {
                    logMsg.Message = e.Message;
                    Logger.Write(logMsg);
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the email address used to override any email message recipient list
        /// </summary>
        public static string RecipientOverrideAddress
        {
            get
            {
                try
                {
                    string OverrideAddress = ConfigurationManager.AppSettings[OverrideAddressKey];
                    if (string.IsNullOrEmpty(OverrideAddress))
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return OverrideAddress;
                    }
                }
                catch (ConfigurationErrorsException e)
                {
                    logMsg.Message = e.Message;
                    Logger.Write(logMsg);
                    return string.Empty;
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates a MailAddress object from a string representing an email address
        /// </summary>
        /// <param name="address">A string representing an email address</param>
        /// <returns>A MailAddress object containing the desired email address</returns>
        private static MailAddress CreateMailAddress(string address)
        {
            MailAddress newAddress;
            try
            {
                newAddress = new MailAddress(address);
                return newAddress;
            }
            catch (ArgumentNullException e)
            {
                logMsg.Message = "Address is null. " + e.Message;
                Logger.Write(logMsg);
                throw new InvalidEmailAddressException(address, e);
            }
            catch (ArgumentException e)
            {
                logMsg.Message = "Address is empty. " + e.Message;
                Logger.Write(logMsg);
                throw new InvalidEmailAddressException(address, e);
            }
            catch (FormatException e)
            {
                logMsg.Message = "Address is not in a recognized format. " + e.Message;
                Logger.Write(logMsg);
                throw new InvalidEmailAddressException(address, e);
            }
        }

        /// <summary>
        /// Adds an email address to a MailAddressCollection
        /// </summary>
        /// <param name="addresses">The collection where the new address will be added</param>
        /// <param name="newAddress">The email address to be added</param>
        private static void AddAddressToCollection(MailAddressCollection addresses, string newAddress)
        {
            try
            {
                MailAddress newMailAddress = CreateMailAddress(newAddress);
                if (newMailAddress != null)
                {
                    addresses.Add(newMailAddress);
                }
            }
            catch (InvalidEmailAddressException e)
            {
                logMsg.Message = "An invalid email address was used. " + e.Message;
                Logger.Write(logMsg);
                throw; 
            }
        }

        /// <summary>
        /// Uses SMTP to send an email message
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <returns>True if the message was successfully sent, false otherwise</returns>
        private static bool SendMessage(MailMessage message)
        {
            string CrLf = "\r\n";
            bool retval = false;
            // uses settings from <mailSettings> in application configuration file
            SmtpClient client = new SmtpClient();  

            try
            {
                // used in system testing to ensure we can test email functionality
                if (!string.IsNullOrEmpty(RecipientOverrideAddress))
                {
                    // replace the "to" addresses with the override address
                    message.To.Clear();
                    message.To.Add(RecipientOverrideAddress);
                }
                client.Send(message);
                retval = true;
            }
            catch (NullReferenceException)
            {
                logMsg.Message = "The mail message is null";
            }
            catch (ObjectDisposedException e)
            {
                // can't really happen, but included just to be complete
                logMsg.Message = "The object has already been disposed. " + e.Message;
            }
            catch (ArgumentNullException e)
            {
                // also can't really happen, but included just to be complete
                logMsg.Message = "Message is null or \"from\" address is null. " + e.Message;
            }
            catch (InvalidOperationException e)
            {
                logMsg.Message = "This SmtpClient has a SendAsync call in progress, or there are no recipients specified in MailMessage.To, MailMessage.CC, and MailMessage.Bcc properties, or the DeliveryMethod properties are invalid." + e.Message;
            }
            catch (SmtpFailedRecipientsException e)
            {
                logMsg.Message = "The message could not be delivered to one or more of the recipients in MailMessage.To, MailMessage.CC, or MailMessage.Bcc. " + e.Message;
            }
            catch (SmtpException e)
            {
                string status = e.StatusCode.ToString();
                string errMsg = e.Message + CrLf;
                Exception exc = e.InnerException;
                while (exc != null)
                {
                    errMsg += exc.Message + CrLf;
                    exc = exc.InnerException;
                }
                logMsg.Message = "The connection to the SMTP server failed, or ";
                logMsg.Message += "Authentication failed, or ";
                logMsg.Message += "The operation timed out, or ";
                logMsg.Message += "EnableSsl is set to true but the DeliveryMethod property is set to SpecifiedPickupDirectory or PickupDirectoryFromIis, or ";
                logMsg.Message += "EnableSsl is set to true, but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command, or ";
                logMsg.Message += "The message could not be delivered to one or more of the recipients in MailMessage.To, MailMessage.CC, or MailMessage.Bcc.. " + errMsg;
            }
            finally
            {
            //    Logger.Write(logMsg);
                client.Dispose();
            }
            return retval;
        }

        /// <summary>
        /// Uses SMTP to send an email message
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <returns>The error message if problem, empty string if all is good.</returns>
        private static string SendMessageString(MailMessage message)
        {
            string CrLf = "\r\n";
            // uses settings from <mailSettings> in application configuration file
            SmtpClient client = new SmtpClient();

            try
            {
                // used in system testing to ensure we can test email functionality
                if (!string.IsNullOrEmpty(RecipientOverrideAddress))
                {
                    // replace the "to" addresses with the override address
                    message.To.Clear();
                    message.To.Add(RecipientOverrideAddress);
                }
                client.Send(message);
            }
            catch (NullReferenceException)
            {
                logMsg.Message = "The mail message is null";
            }
            catch (ObjectDisposedException e)
            {
                // can't really happen, but included just to be complete
                logMsg.Message = "The object has already been disposed. " + e.Message;
            }
            catch (ArgumentNullException e)
            {
                // also can't really happen, but included just to be complete
                logMsg.Message = "Message is null or \"from\" address is null. " + e.Message;
            }
            catch (InvalidOperationException e)
            {
                logMsg.Message = "This SmtpClient has a SendAsync call in progress, or there are no recipients specified in MailMessage.To, MailMessage.CC, and MailMessage.Bcc properties, or the DeliveryMethod properties are invalid." + e.Message;
            }
            catch (SmtpFailedRecipientsException e)
            {
                logMsg.Message = "The message could not be delivered to one or more of the recipients in MailMessage.To, MailMessage.CC, or MailMessage.Bcc. " + e.Message;
            }
            catch (SmtpException e)
            {
                string status = e.StatusCode.ToString();
                string errMsg = e.Message + CrLf;
                Exception exc = e.InnerException;
                while (exc != null)
                {
                    errMsg += exc.Message + CrLf;
                    exc = exc.InnerException;
                }
                logMsg.Message = "The connection to the SMTP server failed, or ";
                logMsg.Message += "Authentication failed, or ";
                logMsg.Message += "The operation timed out, or ";
                logMsg.Message += "EnableSsl is set to true but the DeliveryMethod property is set to SpecifiedPickupDirectory or PickupDirectoryFromIis, or ";
                logMsg.Message += "EnableSsl is set to true, but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command, or ";
                logMsg.Message += "The message could not be delivered to one or more of the recipients in MailMessage.To, MailMessage.CC, or MailMessage.Bcc.. " + errMsg;
            }
            finally
            {
              //  Logger.Write(logMsg);
                client.Dispose();
            }
            return logMsg.Message;
        }





        /// <summary>
        /// Send an email message to a single recipient based on the values of the supplied parameters regardless of any previous property assignments
        /// </summary>
        /// <param name="from">The email address of the sender</param>
        /// <param name="to">The email address of the recipient</param>
        /// <param name="subject">The subject of the email message</param>
        /// <param name="body">The body of the email message</param>
        /// <param name="replyTo">The email address that a recipient should reply to instead of the sender</param>
        /// <returns>True if message was sent successfully, false otherwise</returns>
        private static bool ProcessSend(string from, string to, string subject, string body, string replyTo)
        {
            bool retval = false;
            bool validAddresses = false;
            MailAddress fromAddress = null;
            MailAddress toAddress = null;
            MailAddress replyToAddress = null;

            try
            {
                fromAddress = CreateMailAddress(from);
                toAddress = CreateMailAddress(to);
                replyToAddress = CreateMailAddress(replyTo);
                validAddresses = true;
            }
            catch (InvalidEmailAddressException e)
            {
                logMsg.Message = "An invalid email address was supplied. " + e.Message;
                Logger.Write(logMsg);
                retval = false;
            }

            if (validAddresses)
            {
                MailMessage message = new MailMessage(fromAddress, toAddress);
                message.ReplyToList.Add(replyToAddress);
                message.Subject = subject;
                message.Body = body;
                
                retval = SendMessage(message);
                message.Dispose();
            }
            return retval;
        }

        /// <summary>
        /// Adds an address list to a MailAddress collection
        /// </summary>
        /// <param name="addressCollection">The collection that the addresses should be added to</param>
        /// <param name="addressList">The list of addresses to add</param>
        /// <exception cref="InvalidEmailAddressException">This exception is thrown when one of the addresses in the address list is in an invalid format</exception>
        private void AddAddressListToCollection(MailAddressCollection addressCollection, string addressList)
        {
            if (!string.IsNullOrEmpty(addressList))
            {
                try
                {
                    string[] addresses = addressList.Split(AddressListDelimiters);
                    foreach (string address in addresses)
                    {
                        AddAddressToCollection(addressCollection, address);
                    }
                }
                catch (InvalidEmailAddressException e)
                {
                    logMsg.Message = "An invalid email address was supplied. " + e.Message;
                    Logger.Write(logMsg);
                    throw;
                }
            }
            else
            {
                throw new InvalidEmailAddressException("Cannot use a null or empty address list");
            }
        }

        /// <summary>
        /// Copies mail addresses from one MailAddressCollection to another
        /// </summary>
        /// <param name="source">The MailAddressCollection to copy from</param>
        /// <param name="target">The MailAddressCollection to copy to</param>
        /// <remarks>Needed because the MailAddressCollection cannot be directly assigned</remarks>
        private static void TransferAddressesToMessage(MailAddressCollection source, MailAddressCollection target)
        {
            if (source != null && target != null)
            {
                foreach (MailAddress addr in source)
                {
                    target.Add(addr);
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a list of addressees to the To addresses of an email message
        /// </summary>
        /// <param name="addressList">The list of addressees to add.  Addressees should be separated by a comma or a semi-colon.</param>
        /// <exception cref="InvalidEmailAddressException">Will be thrown if any email address in not in a valid format</exception>
        public void AddToAddress(string addressList)
        {
            AddAddressListToCollection(_toAddresses, addressList);
        }

        /// <summary>
        /// Adds a list of addressees to the CC addresses of an email message
        /// </summary>
        /// <param name="addressList">The list of addressees to add.  Addressees should be separated by a comma or a semi-colon.</param>
        /// <exception cref="InvalidEmailAddressException">Will be thrown if any email address in not in a valid format</exception>
        public void AddCCAddress(string addressList)
        {
            AddAddressListToCollection(_ccAddresses, addressList);
        }

        /// <summary>
        /// Adds a list of addressees to the BCC addresses of an email message
        /// </summary>
        /// <param name="addressList">The list of addressees to add.  Addressees should be separated by a comma or a semi-colon.</param>
        /// <exception cref="InvalidEmailAddressException">Will be thrown if any email address in not in a valid format</exception>
        public void AddBccAddress(string addressList)
        {
            AddAddressListToCollection(_bccAddresses, addressList);
        }

        /// <summary>
        /// Adds a list of addressees to the Reply-To addresses of an email message
        /// </summary>
        /// <param name="addressList">The list of addressees to add.  Addressees should be separated by a comma or a semi-colon.</param>
        /// <exception cref="InvalidEmailAddressException">Will be thrown if any email address in not in a valid format</exception>
        public void AddReplyToAddress(string addressList)
        {
            AddAddressListToCollection(_replyToAddresses, addressList);
        }

        /// <summary>
        /// Adds an attachment to the current email message
        /// </summary>
        /// <param name="fileName">The filename of the file to be used as an attachment</param>
        /// <exception cref="ArgumentNullException">Thrown if filename is null</exception>
        /// <exception cref="ArgumentException">Thrown if filename is empty</exception>
        public void AddAttachment(string fileName)
        {
            _attachments.Add(new Attachment(fileName));
        }
        /// <summary>
        /// Adds an attachment to the current email message
        /// </summary>
        /// <param name="contentStream">A readable Stream that contains the content for this attachment</param>
        /// <param name="fileName">The filename of the file to be used as an attachment</param>
        /// <exception cref="ArgumentNullException">Thrown if stream is null</exception>
        public void AddAttachment(System.IO.Stream contentStream, string fileName)
        {
            _attachments.Add(new Attachment(contentStream, fileName));
        }
        /// <summary>
        /// Send an email message from the system email address to a single recipient based on the values of the supplied parameters regardless of any previous property assignments
        /// </summary>
        /// <param name="to">The email address of the recipient</param>
        /// <param name="subject">The subject of the email message</param>
        /// <param name="body">The body of the email message</param>
        /// <returns>True if message was sent successfully, false otherwise</returns>
        public static bool QuickSend(string to, string subject, string body)
        {
            return ProcessSend(SystemFromAddress, to, subject, body, SystemReplyToAddress);
        }

        /// <summary>
        /// Send an email message to a single recipient based on the values of the supplied parameters regardless of any previous property assignments
        /// </summary>
        /// <param name="from">The email address of the sender</param>
        /// <param name="to">The email address of the recipient</param>
        /// <param name="subject">The subject of the email message</param>
        /// <param name="body">The body of the email message</param>
        /// <returns>True if message was sent successfully, false otherwise</returns>
        public static bool QuickSend(string from, string to, string subject, string body)
        {
            return ProcessSend(from, to, subject, body, from);
        }

        /// <summary>
        /// Sends an email message using properties set in earlier calls to this object
        /// </summary>
        /// <returns>True if message was sent successfully, otherwise false</returns>
        public bool Send()
        {
            bool retval = false;
            MailMessage message = new MailMessage();
            message.From = CreateMailAddress(FromAddress);
            TransferAddressesToMessage(_toAddresses, message.To);
            TransferAddressesToMessage(_ccAddresses, message.CC);
            TransferAddressesToMessage(_bccAddresses, message.Bcc);
            TransferAddressesToMessage(_replyToAddresses, message.ReplyToList);
            message.Subject = Subject;
            message.Body = Body;
            message.IsBodyHtml = IsBodyHtml;
            foreach (Attachment att in _attachments)
            {
                message.Attachments.Add(att);
            }

            retval = SendMessage(message);
            return retval;
        }

        /// <summary>
        /// Sends an email message using properties set in earlier calls to this object
        /// </summary>
        /// <returns>Error message was not sent successfully, otherwise empty string.</returns>
        public string SendReturnMsg()
        {
            string retMessage = string.Empty;
            MailMessage message = new MailMessage();
            message.From = CreateMailAddress(FromAddress);
            TransferAddressesToMessage(_toAddresses, message.To);
            TransferAddressesToMessage(_ccAddresses, message.CC);
            TransferAddressesToMessage(_bccAddresses, message.Bcc);
            TransferAddressesToMessage(_replyToAddresses, message.ReplyToList);
            message.Subject = Subject;
            message.Body = Body;
            message.IsBodyHtml = IsBodyHtml;
            foreach (Attachment att in _attachments)
            {
                message.Attachments.Add(att);
            }

            retMessage = SendMessageString(message);
            return retMessage;
        }


        #endregion
    }
}
