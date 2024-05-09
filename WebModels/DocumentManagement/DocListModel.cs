using System;

namespace Sra.P2rmis.WebModels.DocumentManagement
{
    /// <summary>
    /// Interface defining the object returned for a document list request for
    /// a specific program; fiscal year and panel.
    /// </summary>
    public class DocListModel : IDocListModel
    {
        /// <summary>
        /// Reviewer Last Name
        /// </summary>
        public string RevLastName { get; set; }
        /// <summary>
        /// Reviewer first name
        /// </summary>
        public string RevFirstName { get; set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        public string ProgramAbrv { get; set; }
        /// <summary>
        /// Fiscal year
        /// </summary>
        public string FiscalYear { get; set; }
        /// <summary>
        /// Participant type abbreviation
        /// </summary>
        public string PartAbrv { get; set; }
        /// <summary>
        /// Reviewer document id
        /// </summary>
        public int? RevDocId { get; set; }
        /// <summary>
        /// Type of document
        /// </summary>
        public string DocType { get; set; }
        /// <summary>
        /// Date the document was signed
        /// </summary>
        public DateTime? DateSigned { get; set; }
        /// <summary>
        /// panels abbreviation
        /// </summary>
        public string PanelAbbrv { get; set; }
        /// <summary>
        /// Document name
        /// </summary>
        public string DocName { get { return RevLastName + RevFirstName + "-" + DocType + ".pdf"; } }
        /// <summary>
        /// Document Link
        /// </summary>
        public string DocLink { get { return "/DocumentManagement/DownloadRevDocument?docId=" + RevDocId + "&docName=" + DocName; } }
    }
}
