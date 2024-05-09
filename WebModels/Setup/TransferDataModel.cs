using System;

namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Model for data transfer information
    /// </summary>
    /// <seealso cref="Sra.P2rmis.WebModels.Setup.ITransferDataModel" />
    public class TransferDataModel : ITransferDataModel
    {
        /// <summary>
        /// Gets or sets the mechanism.
        /// </summary>
        /// <value>
        /// The mechanism name.
        /// </value>
        public string Mechanism { get; set; }

        /// <summary>
        /// Gets or sets the funding opportunity identifier.
        /// </summary>
        /// <value>
        /// The funding opportunity identifier.
        /// </value>
        public string FundingOpportunityId { get; set; }
        /// <summary>
        /// Last successful import date.
        /// </summary>
        public DateTime? LastSuccessfulImportDate { get; set; }

        /// <summary>
        /// Gets or sets the last import date.
        /// </summary>
        /// <value>
        /// The last import date.
        /// </value>
        public DateTime? LastImportDate { get; set; }
        /// <summary>
        /// Last import log identifier.
        /// </summary>
        public int? LastImportLogId { get; set; }

        /// <summary>
        /// Gets or sets the receipt date.
        /// </summary>
        /// <value>
        /// The receipt date.
        /// </value>
        public DateTime? ReceiptDate { get; set; }

        /// <summary>
        /// Gets or sets the program mechanism identifier.
        /// </summary>
        /// <value>
        /// The program mechanism identifier.
        /// </value>
        public int ProgramMechanismId { get; set; }
        /// <summary>
        /// Whether there was an error.
        /// </summary>
        public bool HasError { get; set; }
    }
}
