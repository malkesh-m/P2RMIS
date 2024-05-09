using Sra.P2rmis.WebModels.HotelAndTravel;

namespace Sra.P2rmis.Web.UI.Models
{
    public class FactSheetViewModel
    {
        public FactSheetViewModel(IFactSheetModel factSheet)
        {
            DocumentName = factSheet.DocumentName;
            ProgramMeetingInformationId = factSheet.ProgramMeetingInformationId;
            ExternalLink = factSheet.ContentUrl;
            IsLink = factSheet.IsLink;
            IsVideo = factSheet.IsVideo;
        }
        #region Attributes
        /// <summary>
        /// Document name
        /// </summary>
        public string DocumentName { get; private set; }
        /// <summary>
        /// ProgramMeetingInformation entity identifier
        /// </summary>
        public int ProgramMeetingInformationId { get; private set; }
        /// <summary>
        /// External link to fact sheet.
        /// </summary>
        public string ExternalLink { get; private set; }

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