using System;
using System.Activities;
using System.Collections;
using System.Collections.Generic;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Business rules for the Reset To Edit Workflow function
    /// </summary>
    public class ResetToEditWorkflowStepActivity : AssignWorkflowStepActivity
    {
        #region Constructor and setup
        public ResetToEditWorkflowStepActivity() : base()
        {
            ErrorMessage = "ResetToEditWorkflowStepActivity detected invalid arguments: ApplicationWorkflowStep is null [{0}] userId [{1}] UnitOfWork is null [{2}] TargetStepId [{3}]";
        }
        //
        // Identify the activity specific parameters
        //
        public enum SaveParametersResetToEditWorkflowActivity
        {
            Default = 0,
            //
            // This is the target ApplicationWorkflowStepId value
            // 

            TargetStepId = AssignWorkflowStepActivity.SaveParameters.TargetStepId + 1,
            //
            // This is the last ApplicationWorkflowStep
            //
            LastStepId
        }
        /// <summary>
        /// Initializes any Activity specific parameters.
        /// </summary>
        /// <param name="values">List of activity specific parameters</param>
        public override void SetParameters(IDictionary values)
        {
            //
            // just try setting the parameters.  If anything is wrong throw
            // and exception.
            //
            try
            {
                this.TargetWorkflowId = (int)values[SaveParametersResetToEditWorkflowActivity.TargetStepId];
                this.LastWorkflowStepId = (int)values[SaveParametersResetToEditWorkflowActivity.LastStepId];

            }
            catch
            {
                string message = string.Format(ParameterErrorMessage, (values == null), ((values != null) ? values.Count.ToString() : string.Empty));
                throw new ArgumentException(message);
            }
        }
    #endregion
        #region Properties
        /// <summary>
        /// Last workflow step
        /// </summary>
        public InArgument<int> LastWorkflowStepId { get; set; }
        #endregion

        /// <summary>
        /// Virtual method overridden to reset date closed to null
        /// </summary>
        /// <param name="applicationWorkflow">Application workflow</param>
        /// <param name="userId">User identifier</param>
        protected override void WorkflowPostAssignProcessing(ApplicationWorkflow applicationWorkflow, int userId) 
        {
            if (applicationWorkflow.DateClosed != null)
            {
                applicationWorkflow.ReOpen(userId);
            }
        }
        /// <summary>
        /// Gets current or last workflow step object
        /// </summary>
        /// <param name="context">Workflow Activity Context object</param>
        /// <returns>Current Work flow step object if exists, otherwise last work flow step object</returns>
        protected override ApplicationWorkflowStep GetCurrentWorkflowStep(CodeActivityContext context)
        {
            var currentStep = this.WorkflowStep.Get(context);
            if (currentStep != null)
                return currentStep;
            else
            {
                IUnitOfWork unitOfWork = this.UnitOfWork.Get(context);
                return unitOfWork.ApplicationWorkflowStepRepository.GetByID(this.LastWorkflowStepId.Get(context));
            }
        }
        /// <summary>
        /// Set or reset the Resolved attribute of all states between the current ApplicationWorkflowStep
        /// and the target ApplicationWorkflowStep.
        /// </summary>
        /// <param name="currentWorkflowStepEntity">Current ApplicationWorkflowStep entity</param>
        /// <param name="targetWorkflowStepEntity">Target ApplicationWorkflowStep entity</param>
        /// <param name="userId">User entity identifier</param>
        protected override void ChangeResolveState(ApplicationWorkflowStep currentWorkflowStepEntity, ApplicationWorkflowStep targetWorkflowStepEntity, int userId)
        {
            targetWorkflowStepEntity.ResetResolution();
            Helper.UpdateModifiedFields(targetWorkflowStepEntity, userId);
        }
        /// <summary>
        /// Gets the current or last workflow step for the current workflow
        /// </summary>
        /// <param name="theWorkflow">The application workflow entity</param>
        /// <returns></returns>
        public override ApplicationWorkflowStep GetCurrentWorkflowStep(ApplicationWorkflow theWorkflow)
        {
            return theWorkflow.CurrentStep() ?? theWorkflow.LastStep();
        }
        /// <summary>
        /// Logically delete the subsequent workflow step content & mark it as open.
        /// </summary>
        /// <param name="context">Workflow Activity Context object</param>
        /// <param name="currentApplicationWorkflowStepId">Current ApplicationWorkflowStep entity identifier</param>
        protected override void RevertSubsequentWorkflowSteps(CodeActivityContext context, int currentApplicationWorkflowStepId)
        {
            int userId = this.UserId.Get(context);
            IUnitOfWork unitOfWork = this.UnitOfWork.Get(context);
            ApplicationWorkflowStep currentStep = unitOfWork.ApplicationWorkflowStepRepository.GetByID(currentApplicationWorkflowStepId);
            //
            // This is going to give the next step even for MOD
            //
            ApplicationWorkflowStep nextStep = currentStep.GetNextMODStep();
            //
            // Now we just delete the contents
            //
            if (nextStep != null)
            {
                unitOfWork.ApplicationWorkflowStepElementContentRepository.DeletePostTargetStepElementContent(currentStep.ApplicationWorkflowId, nextStep.ApplicationWorkflowStepId, userId);
                //
                // and reset the next workflow then Bob is your uncle.
                //
                nextStep.UnResolve();
                Helper.UpdateModifiedFields(nextStep, userId);
                unitOfWork.ApplicationWorkflowStepRepository.Update(nextStep);
            }
        }
    }
}
