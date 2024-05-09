using System;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// Model for Mechanism Transfer (Import) info
    /// </summary>
    public class MechanismTransferDetailViewModel
    {
        public MechanismTransferDetailViewModel(ITransferDataModel dataModel)
        {
            Mechanism = dataModel.Mechanism;
            FundingOpportunityId = dataModel.FundingOpportunityId;
            LastSuccessfulImportDate = ViewHelpers.FormatDate(dataModel.LastSuccessfulImportDate);
            LastImportDate = ViewHelpers.FormatDate(dataModel.LastImportDate);
            LastImportLogId = dataModel.LastImportLogId;
            ReceiptDate = ViewHelpers.FormatDate(dataModel.ReceiptDate);
            ProgramMechanismId = dataModel.ProgramMechanismId;
            HasError = dataModel.HasError;
        }
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
        /// Gets or sets the last successful import date.
        /// </summary>
        public string LastSuccessfulImportDate { get; set; }

        /// <summary>
        /// Gets or sets the last import date.
        /// </summary>
        /// <value>
        /// The last import date.
        /// </value>
        public string LastImportDate { get; set; }
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
        public string ReceiptDate { get; set; }

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