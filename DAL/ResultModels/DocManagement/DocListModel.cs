using System;

namespace Sra.P2rmis.Dal.ResultModels.DocManagement
{
    /// <summary>
    /// Interface defining the object returned for a document list request for
    /// a specific panel, person and/or document type.
    /// </summary>
    public class DocListModelx : IDocListModelx
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
    }
}
