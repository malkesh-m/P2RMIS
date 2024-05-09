
namespace Sra.P2rmis.WebModels.HelperClasses
{
    /// <summary>
    /// web model for email attachments
    /// </summary>
    public interface IEmailAttachment
    {
        /// <summary>
        /// The name of the attachment
        /// </summary>
        string FileName { get; set; }
        /// <summary>
        /// The location of the attachment
        /// </summary>
        string FileLocation { get; set; }
    }
}
