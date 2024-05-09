using System;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Exception for when a critique is saved of Add/Edit but it had been previously submitted.
    /// </summary>
    public class CritiqueSubmittalException: Exception
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationWorkflowStepContentId">ApplicationWorkflowStepContnet entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        public CritiqueSubmittalException(int applicationWorkflowStepContentId, int userId)
            : base(MessageService.FailedToSaveCritiqueBecauseCritiqueWasSubmitted(applicationWorkflowStepContentId, userId))
        {
            this.ApplicationWorkflowStepContentId = applicationWorkflowStepContentId;
            this.userId = userId;
        }
        #endregion
        #region Properties
        /// <summary>
        /// ApplicationWorkflowStepContent entity identifier
        /// </summary>
        public int ApplicationWorkflowStepContentId { get; private set; }
        /// <summary>
        /// User entity identifier
        /// </summary>
        public int userId { get; private set; }
        #endregion
    }
}
