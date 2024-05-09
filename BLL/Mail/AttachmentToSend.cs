using System.IO;

namespace Sra.P2rmis.Bll.Mail
{
    /// <summary>
    /// Used to pass attached file information to the mailer service.
    /// </summary>
    public class AttachmentToSend
    {
        #region Properties
        /// <summary>
        /// Name of file to attach
        /// </summary>
        public string FileName { get; private set; }
        /// <summary>
        /// Attached file
        /// </summary>
        public Stream FileStream { get; private set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName"><Name of uploaded file to send/param>
        /// <param name="fileStream">Uploaded file</param>
        public AttachmentToSend (string fileName, Stream fileStream)
        {
            this.FileName = fileName;
            this.FileStream = fileStream;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Returns the stream size.
        /// </summary>
        public long Length
        {
            get {
                return FileStream.Length;
                }
        }
        #endregion
    }
}
