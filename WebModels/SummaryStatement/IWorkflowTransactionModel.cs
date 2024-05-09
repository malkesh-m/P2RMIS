using System;

namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Class representing transaction history details for an application's workflow
    /// </summary>
    public interface IWorkflowTransactionModel
    {
        /// <summary>
        /// Gets or sets the application workflow step work log identifier.
        /// </summary>
        /// <value>
        /// The application workflow step work log identifier.
        /// </value>
        int ApplicationWorkflowStepWorkLogId { get; set; }
        /// <summary>
        /// The resulting action that takes place for a transaction
        /// </summary>
        string Action { get; set; }
        /// <summary>
        /// Name of the phase or step the transaction takes place on
        /// </summary>
        string PhaseName { get; set; }
        /// <summary>
        /// Date the transaction takes place
        /// </summary>
        DateTime? TransactionDate { get; set; }
        /// <summary>
        /// Last name of the user initiating the transaction
        /// </summary>
        string UserLastName { get; set; }
        /// <summary>
        /// First name of the user initiating the transaction
        /// </summary>
        string UserFirstName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [summary file exists].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [summary file exists]; otherwise, <c>false</c>.
        /// </value>
        bool BackupFileExists { get; set; }
    }
}
