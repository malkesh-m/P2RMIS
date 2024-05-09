using System;

namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Interface for data transfer information
    /// </summary>
    public interface ITransferDataModel
    {
        /// <summary>
        /// Gets or sets the funding opportunity identifier.
        /// </summary>
        /// <value>
        /// The funding opportunity identifier.
        /// </value>
        string FundingOpportunityId { get; set; }
        /// <summary>
        /// Last successful import date.
        /// </summary>
        DateTime? LastSuccessfulImportDate { get; set; }
        /// <summary>
        /// Gets or sets the last import date.
        /// </summary>
        /// <value>
        /// The last import date.
        /// </value>
        DateTime? LastImportDate { get; set; }
        /// <summary>
        /// The last import log identifier.
        /// </summary>
        int? LastImportLogId { get; set; }
        /// <summary>
        /// Gets or sets the mechanism.
        /// </summary>
        /// <value>
        /// The mechanism.
        /// </value>
        string Mechanism { get; set; }
        /// <summary>
        /// Gets or sets the program mechanism identifier.
        /// </summary>
        /// <value>
        /// The program mechanism identifier.
        /// </value>
        int ProgramMechanismId { get; set; }
        /// <summary>
        /// Gets or sets the receipt date.
        /// </summary>
        /// <value>
        /// The receipt date.
        /// </value>
        DateTime? ReceiptDate { get; set; }
        /// <summary>
        /// Whether there was an error.
        /// </summary>
        bool HasError { get; set; }
    }
}