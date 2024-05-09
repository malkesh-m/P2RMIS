namespace Sra.P2rmis.CrossCuttingServices.MessageServices
{
    /// <summary>
    /// Message container
    /// </summary>
    public class ServiceMessage
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="messageId">Message identifier</param>
        /// <param name="messageText">Message text</param>
        public ServiceMessage(int messageId, string messageText)
        {
            this.MessaageId = messageId;
            this.MessageText = messageText;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// The message identifier
        /// </summary>
        public int MessaageId { get; private set; }
        /// <summary>
        /// The message text
        /// </summary>
        public string MessageText { get; private set; }
        #endregion
    }
}
