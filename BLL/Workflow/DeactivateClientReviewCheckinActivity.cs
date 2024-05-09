using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Check-in activity for Client Review workflow steps
    /// </summary>
    internal class DeactivateClientReviewCheckinActivity: CheckinActivity
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        public DeactivateClientReviewCheckinActivity(): base() { }
        #endregion
        /// <summary>
        /// Determines the next workflow step
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="step">ApplicationWorkflowStep entity</param>
        /// <returns>Next ApplicationWorkflowStep entity if one exist; null otherwise</returns>
        protected override ApplicationWorkflowStep DetermineNextWorkflowStep(IUnitOfWork unitOfWork, ApplicationWorkflowStep step)
        {
            //
            // Here is the workflow
            //
            ApplicationWorkflow applicationWorkflowEntity = step.ApplicationWorkflow;
            //
            // and then we find the next non client review step.
            //
            return applicationWorkflowEntity.NextNoneClientReview();
        }
    }
}

