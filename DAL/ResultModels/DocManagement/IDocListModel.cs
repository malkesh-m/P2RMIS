using System;

namespace Sra.P2rmis.Dal.ResultModels.DocManagement
{
    /// <summary>
    /// Interface defining the object returned for a document list request for
    /// a specific program; fiscal year and panel.
    /// </summary>
    public interface IDocListModelx
    {
        /// <summary>
        /// Reviewer Last Name
        /// </summary>
        string RevLastName { get; set; }
        /// <summary>
        /// Reviewer first name
        /// </summary>
        string RevFirstName { get; set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        string ProgramAbrv { get; set; }
        /// <summary>
        /// Fiscal year
        /// </summary>
        string FiscalYear { get; set; }
        /// <summary>
        /// Participant type abbreviation
        /// </summary>
        string PartAbrv { get; set; }
        /// <summary>
        /// Reviewer document id
        /// </summary>
        int? RevDocId { get; set; }
        /// <summary>
        /// Type of document
        /// </summary>
        string DocType { get; set; }
        /// <summary>
        /// Date the document was signed
        /// </summary>
        DateTime? DateSigned { get; set; }
    }
}
