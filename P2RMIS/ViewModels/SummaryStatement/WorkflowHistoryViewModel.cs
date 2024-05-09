using System.Collections.Generic;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Web.UI.Models
{
    public class WorkflowHistoryViewModel
    {
        #region Constructors
        /// <summary>
        /// View model for application workflow transaction history
        /// </summary>
        public WorkflowHistoryViewModel()
            : base()
        {
            //
            // Allocate a dictionary so view can display nothing.
            //
            this.ApplicationDetail = new ApplicationDetailModel();
            this.WorkflowTransactions = new List<IWorkflowTransactionModel>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Details of a single application
        /// </summary>
        public IApplicationDetailModel ApplicationDetail { get; set; }
        /// <summary>
        /// The history for an application's workflow
        /// </summary>
        public List<IWorkflowTransactionModel> WorkflowTransactions { get; set; }
        #endregion
    }
}