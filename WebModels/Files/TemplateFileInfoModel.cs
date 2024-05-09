namespace Sra.P2rmis.WebModels.Files
{
    /// <summary>
    /// Data model for retrieving a client's possible files.
    /// </summary>
    public interface ITemplateFileInfoModel
    {
        /// <summary>
        /// Display label for file
        /// </summary>
        string DisplayLabel { get; }
        /// <summary>
        /// ProgramEmailTemplate entity identifier identifying the specific template. 
        /// </summary>
        int ProgramEmailTemplateId { get;}

        /// <summary>
        /// Gets the file location (URL).
        /// </summary>
        /// <value>
        /// The file location (URL).
        /// </value>
        string ContentUrl { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is video.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is video; otherwise, <c>false</c>.
        /// </value>
        bool IsVideo { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is link.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is link; otherwise, <c>false</c>.
        /// </value>
        bool IsLink { get; }

        /// <summary>
        /// Gets the meeting types associated with the document.
        /// </summary>
        /// <value>
        /// The meeting types.
        /// </value>
        string MeetingTypes { get; }
    }
    /// <summary>
    /// Data model for retrieving a client's possible files.
    /// </summary>
    public class TemplateFileInfoModel: ITemplateFileInfoModel
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="displayLabel">Display label on form</param>
        /// <param name="programEmailTemplateId">The email template identifier.</param>
        /// <param name="isLink">if the document is a link.</param>
        /// <param name="isVideo">if the document is a video.</param>
        /// <param name="contentUrl">The content URL the document is located.</param>
        /// <param name="meetingTypeIds">The meeting type ids.</param>
        public TemplateFileInfoModel(string displayLabel, int programEmailTemplateId, bool isLink, bool isVideo, string contentUrl, string meetingTypeIds)
        {
            this.DisplayLabel = displayLabel;
            this.ProgramEmailTemplateId = programEmailTemplateId;
            this.IsLink = isLink;
            this.IsVideo = isVideo;
            this.ContentUrl = contentUrl;
            this.MeetingTypes = meetingTypeIds;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Display label for file
        /// </summary>
        public string DisplayLabel { get; private set; }
        /// <summary>
        /// ProgramEmailTemplate entity identifier identifying the specific template.
        /// </summary>
        public int ProgramEmailTemplateId { get; private set; }

        /// <summary>
        /// Gets the file location (URL).
        /// </summary>
        /// <value>
        /// The file location (URL).
        /// </value>
        public string ContentUrl { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is video.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is video; otherwise, <c>false</c>.
        /// </value>
        public bool IsVideo { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is link.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is link; otherwise, <c>false</c>.
        /// </value>
        public bool IsLink { get; private set; }

        /// <summary>
        /// The meeting types associated with the document.
        /// </summary>
        /// <value>
        ///   The meeting types associated with the document.
        /// </value>
        public string MeetingTypes { get; private set; }
        #endregion
    }
}
