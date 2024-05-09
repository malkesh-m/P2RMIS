using Sra.P2rmis.Bll.ApplicationScoring;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.ApplicationScoring;
using System.Activities;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// 
    /// </summary>
    internal class SubmitActivity: CheckinActivity
    {
        /// <summary>
        /// Update the worklog.
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork entity to access P2RMIS repositories</param>
        /// <param name="step">ApplicationWorkflowStep entity</param>
        /// <param name="userId">User entity identifier</param>
        /// <remarks>
        ///    These processing steps were re-factored to support submittal of Critiques by reviewers which did not have
        ///    an ApplicationWorkflowStepWorkLog entry because they were not checked out.
        /// </remarks>
        protected override void UpdateWorklog(IUnitOfWork unitOfWork, ApplicationWorkflowStep step, int userId)
        {
        }
        /// <summary>
        /// Determines the next workflow step.  We override the default because the next step does not have to be a 
        /// active if it is a MOD step (stepTypeId = 7).
        /// </summary>
        /// <param name="unitOfWork">Unit of work (Not used)</param>
        /// <param name="step">ApplicationWorkflowStep entity</param>
        /// <returns>Next ApplicationWorkflowStep entity if one exist; null otherwise</returns>
        protected override ApplicationWorkflowStep DetermineNextWorkflowStep(IUnitOfWork unitOfWork, ApplicationWorkflowStep step)
        {
            //
            // First we get the workflow from the step
            //
            return step.GetNextMODStep();
        }
        /// <summary>
        /// Certain workflows need additional processing.  By default we do nothing.
        /// </summary>
        /// <param name="applicationWorkflowStepEntity">The next ApplicationWorkflowStep entity</param>
        protected override void CompleteWorkflowStepAndWorkflowIfMOD(ApplicationWorkflowStep applicationWorkflowStepEntity, int userId)
        {
            //
            // If the next step is MOD
            //
            if (applicationWorkflowStepEntity.StepTypeId == StepType.Indexes.Final)
            {
                //
                // Resolve the step & complete the workflow if necessary
                //
                applicationWorkflowStepEntity.SetResolution(true, userId);
                applicationWorkflowStepEntity.ApplicationWorkflow.Complete(userId);
            }
        }
        /// <summary>
        /// Hook for derived classes to perform any workflow content specific pre-processing.  
        /// Really wanted to keep the Activities workflow specific to workflows entities only.
        /// However the desire to do things transactional got the better of me.
        /// </summary>
        protected override void PreCheckinProcessing(CodeActivityContext context)
        {
            ApplicationWorkflowStep step = this.WorkflowStep.Get(context);
            IUnitOfWork unitOfWork = this.UnitOfWork.Get(context);
            int userId = this.UserId.Get(context);
            //
            // Now we ask the workflow step to provide a list of the incomplete criteria.
            //
            List<IIncompleteCriteriaNameModel> incompleteCriteriaList = step.CanSubmit();
            //
            // Now that we have the list we need to abstain them.
            //
            ApplicationWorkflowStepElementContentServiceActionPostAssignment editAction = new ApplicationWorkflowStepElementContentServiceActionPostAssignment();
            //
            // Now it is just as simple as iterating over the ApplicationWorkflowStepElementIds
            //
            foreach (IIncompleteCriteriaNameModel model in incompleteCriteriaList)
                {
                int applicationWorkflowStepElementId = model.ApplicationWorkflowStepElementId;
                ApplicationWorkflowStepElement applicationWorkflowStepElementEntity = unitOfWork.ApplicationWorkflowStepElementRepository.GetByID(applicationWorkflowStepElementId);
                int applicationWorkflowStepElemenContentId = applicationWorkflowStepElementEntity.ApplicationWorkflowStepElementContentId();
                string contentText = applicationWorkflowStepElementEntity.ApplicationWorkflowStepElementContent().ContentText;

                editAction.InitializeAction(unitOfWork, unitOfWork.ApplicationWorkflowStepElementContentRepository, ServiceAction<ApplicationWorkflowStepElementContent>.DoNotUpdate, applicationWorkflowStepElemenContentId, userId);
                editAction.Populate(unitOfWork.ApplicationWorkflowStepElementRepository, contentText, null, true, applicationWorkflowStepElementId);
                editAction.Execute();
            }
            //
            // We rely upon the caller to do a save since we want it to be transactional. 
            //
        }
    }
}
