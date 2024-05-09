
namespace Sra.P2rmis.WebModels.HelperClasses
{
    /// <summary>
    /// web model for email attachments
    /// </summary>
    public class EmailAttachment : IEmailAttachment
    {
        public EmailAttachment(string fileName, string fileLocation)
        {
            FileName = fileName;
            FileLocation = fileLocation;
        }
        /// <summary>
        /// The name of the attachment
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// The location of the attachment
        /// </summary>
        public string FileLocation { get; set; }
    }
}
