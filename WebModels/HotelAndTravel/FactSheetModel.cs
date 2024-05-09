using System;

namespace Sra.P2rmis.WebModels.HotelAndTravel
{
    public interface IFactSheetModel
    {
        #region Attributes
        /// <summary>
        /// Document name
        /// </summary>
        string DocumentName { get; }
        /// <summary>
        /// ProgramMeetingInformation PeerReviewDocument identifier
        /// </summary>
        int ProgramMeetingInformationId { get; }
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
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    public class FactSheetModel: IFactSheetModel
    {
        #region Constructor & Setup
        public FactSheetModel(int programMeetingInformationId, string documentName, string contentUrl, bool isVideo, bool isLink)
        {
            ProgramMeetingInformationId = programMeetingInformationId;
            DocumentName = documentName;
            ContentUrl = contentUrl;
            IsVideo = isVideo;
            IsLink = isLink;
        }


        #endregion
        #region Attributes
        /// <summary>
        /// Document name
        /// </summary>
        public string DocumentName { get; private set; }

        /// <summary>
        /// ProgramMeetingInformation PeerReviewDocument identifier
        /// </summary>
        public int ProgramMeetingInformationId { get; private set; }
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
        #endregion
    }
}
