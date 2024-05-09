using System;
using System.Activities;
using System.Collections.Generic;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Activity performs the business rules associated with the summary
    /// statement check-in.
    /// </summary>
    internal class CheckinActivity: P2rmisActivity 
    {
        #region Constants
        private const string ErrorMessage = "CheckinActivity detected invalid arguments: ApplicationWorkflowStep is null [{0}] userId [{1}] UnitOfWork is null [{2}]";
        #endregion
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        public CheckinActivity (): base() {}
        #endregion
        #region Business Rule Execution
        /// <summary>
        /// Executes the Check-in specific business rules.
        /// </summary>
        /// <param name="context">Workflow Activity Context object</param>
        protected override void Execute(CodeActivityContext context)
        {
            ApplicationWorkflowStep step = this.WorkflowStep.Get(context);
            int userId = this.UserId.Get(context);
            IUnitOfWork unitOfWork = this.UnitOfWork.Get(context);
            if (IsActivityParametersValid(step, userId, unitOfWork))
            {
                //
                // Already have the workflow step we are on.  So find the next step
                //
                ApplicationWorkflowStep nextStep = DetermineNextWorkflowStep(unitOfWork, step);
                //
                // If there is a next step then we need to do some work.  Otherwise we can just skip it.
                //
                PreCheckinProcessing(context);
                if (nextStep != null)
                {
                    //
                    // Next promote the steps content to the next step & add it.
                    //
                    List<ApplicationWorkflowStepElementContent> promptedContent = step.Promote(nextStep, userId);
                    unitOfWork.ApplicationWorkflowStepElementContentRepository.Add(promptedContent);
                    //
                    // Find the work log entry and update it as completed.
                    //
                    UpdateWorklog(unitOfWork, step, userId);
                    //
                    // Now we update the target workflow & workflow step as necessary.
                    //
                    CompleteWorkflowStepAndWorkflowIfMOD(nextStep, userId);
                }
                PostCheckinProcessing(context);
                //
                // This is the state machine's state now.
                //
                this.OutState.Set(context, WorkflowState.Complete);
            }
            else
            {
                String message = string.Format(ErrorMessage, (step == null), userId, (unitOfWork == null));
                throw new ArgumentException(message);
            }
            return;
        }
        /// <summary>
        /// Update the work log
        /// </summary>
        /// <param name="unitOfWork">Unit Of Work object implementing pattern for transactional access to database</param>
        /// <param name="step">ApplicationWorkflowStep entity</param>
        /// <param name="userId">User entity identifier</param>
        /// <remarks>
        ///    These processing steps were re-factored to support submittal of Critiques by reviewers which did not have
        ///    an ApplicationWorkflowStepWorkLog entry because they were not checked out.
        /// </remarks>
        protected virtual void UpdateWorklog(IUnitOfWork unitOfWork, ApplicationWorkflowStep step, int userId)
        {
            ApplicationWorkflowStepWorkLog entity = unitOfWork.ApplicationWorkflowStepWorkLogRepository.FindInCompleteWorkLogEntryByWorkflowStep(step.ApplicationWorkflowStepId);
            entity?.Complete(userId);
        }
        /// <summary>
        /// Determines the next workflow step
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="step">ApplicationWorkflowStep entity</param>
        /// <returns>Next ApplicationWorkflowStep entity if one exist; null otherwise</returns>
        protected virtual ApplicationWorkflowStep DetermineNextWorkflowStep(IUnitOfWork unitOfWork, ApplicationWorkflowStep step)
        {
            return unitOfWork.ApplicationWorkflowRepository.GetNextStep(step.ApplicationWorkflowId, step.StepOrder);
        }
        /// <summary>
        /// Certain workflows need additional processing.  
        /// </summary>
        /// <param name="applicationWorkflowStepEntity">The next ApplicationWorkflowStep entity</param>
        /// <param name="userId">User entity identifier</param>
        protected virtual void CompleteWorkflowStepAndWorkflowIfMOD(ApplicationWorkflowStep applicationWorkflowStepEntity, int userId)
        {
            // By default we do nothing.
        }
        /// <summary>
        /// Hook for derived classes to perform any workflow content specific pre-processing.  
        /// Really wanted to keep the Activities workflow specific to workflows entities only.
        /// However the desire to do things transactional got the better of me.
        /// </summary>
        protected virtual void PreCheckinProcessing(CodeActivityContext context)
        {
            // By default we do nothing.
        }
        /// <summary>
        /// Hook for derived classes to perform any workflow content specific post-processing.
        /// </summary>
        protected virtual void PostCheckinProcessing(CodeActivityContext context)
        {
            // By default we do nothing.
        }
        #endregion
    }
}
