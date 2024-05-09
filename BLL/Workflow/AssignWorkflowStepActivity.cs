using System;
using System.Activities;
using System.Collections;
using System.Collections.Generic;
using Sra.P2rmis.Dal;
using Sra.P2rmis.CrossCuttingServices.DocumentServices;
using System.Linq;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Business rules for the Assign (Rollback) function
    /// </summary>
    public class AssignWorkflowStepActivity : P2rmisActivity 
    {
        #region Constants
        protected const string ParameterErrorMessage = "SetParameters detected invalid arguments: list is null [{0}] list entries count [{1}]";
        #endregion
        #region Classes
        //
        // Identify the activity specific parameters
        //
        public enum SaveParameters
        {
            Default = 0,
            //
            // This is the target ApplicationWorkflowStepIdt value
            //
            TargetStepId = 1,
            BackupFile = 2
        }
        /// <summary>
        /// Number of activity specific arguments.  Used to size the hashset.
        /// </summary>
        public static readonly int AssignArgumentCount = 1;
        #endregion
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        public AssignWorkflowStepActivity() : base() 
        {
            ErrorMessage = "AssignWorkflowStepActivity detected invalid arguments: ApplicationWorkflowStep is null [{0}] userId [{1}] UnitOfWork is null [{2}] TargetStepId [{3}]";
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
                this.TargetWorkflowId = (int)values[SaveParameters.TargetStepId];
                this.BackupFile =  new InArgument<byte[]>(ctx => (byte[])values[SaveParameters.BackupFile]);
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
        /// Parameter validation error message format
        /// </summary>
        protected string ErrorMessage { get; set; }
        /// <summary>
        /// Target workflow step id
        /// </summary>
        public InArgument<int> TargetWorkflowId { get; set; }
        /// <summary>
        /// Gets or sets the backup file.
        /// </summary>
        /// <value>
        /// The backup file.
        /// </value>
        public InArgument<byte[]> BackupFile { get; set; }
        #endregion
        #region Business Rule Execution
        /// <summary>
        /// Executes the Assign specific business rules.  The Assign activity moves a workflow from
        /// it's current step to a user selected step in the workflow.
        /// </summary>
        /// <param name="context">Workflow Activity Context object</param>
        protected override void Execute(CodeActivityContext context)
        {
            //
            // Retrieve the parameters
            //
            ApplicationWorkflowStep step = GetCurrentWorkflowStep(context);
            int userId = this.UserId.Get(context);
            IUnitOfWork unitOfWork = this.UnitOfWork.Get(context);
            int targetworkflowStepId = this.TargetWorkflowId.Get(context);
            //
            // Ensure the parameters make sense
            //
            if (!IsActivityParametersValid(step, userId, unitOfWork, targetworkflowStepId))
            {
                String message = string.Format(ErrorMessage, (step == null), userId, (unitOfWork == null), targetworkflowStepId);
                throw new ArgumentException(message);
            }
            else if (targetworkflowStepId == ApplicationWorkflowStep.CompleteWorkflow)
            {
                MoveToLastPhase(context);
                CompleteWorkflow(context, step.ApplicationWorkflow.LastStep());
            }
            //
            // If the target WorkflowStep is not the same as the current WorkflowStep then
            // we manipulate the workflow's step content.
            //
            else if (targetworkflowStepId != step.ApplicationWorkflowStepId)
            {
                ReassignToSpecificWorkflowStep(context, targetworkflowStepId);
                RevertSubsequentWorkflowSteps(context, targetworkflowStepId);
                UpdateStepModifiedDates(step, targetworkflowStepId, userId);
            }
            //
            // Well if they are not not equal then they are the same.  In which case we do
            // not need to do much.
            //
            else
            {
                AssignToSameWorkflowStep(context);
                RevertSubsequentWorkflowSteps(context, targetworkflowStepId);
                //
                // And just do the paper work
                //
                Helper.UpdateModifiedFields(step, userId);
            }
        }
        /// <summary>
        /// Updates the ModifiedBy fields of the current and target 
        /// ApplicationWorkflowSteps
        /// </summary>
        /// <param name="current">Current ApplicationWorkflowStep</param>
        /// <param name="target">Target ApplicationWorkflowStep</param>
        /// <param name="userId">User entity id of user making the change</param>
        protected virtual void UpdateStepModifiedDates(ApplicationWorkflowStep current, int targetworkflowStepId, int userId)
        {
            var target = current.ApplicationWorkflow.GetThisStep(targetworkflowStepId);

            Helper.UpdateModifiedFields(current, userId);
            Helper.UpdateModifiedFields(target, userId);
        }       /// <summary>
                /// The "Completion" action is identified by a non-existent Workflow step
                /// inserted into the submit dropdown selection list.  When this is selected
                /// the workflow is marked as complete.
                /// </summary>
                /// <param name="context">Workflow Activity Context object</param>
                /// <param name="step">Final ApplicationWorkflowStep entity</param>
        protected virtual void CompleteWorkflow(CodeActivityContext context, ApplicationWorkflowStep step)
        {
            //
            // Since we are assigning to the same step we don't need to do go through
            // content promotion.  All we need to do is just
            // set the WorkLog to completed.
            //
            // Retrieve the parameters update the WorkLog.
            //
            int userId = this.UserId.Get(context);
            byte[] backupFile = this.BackupFile.Get(context);
            IUnitOfWork unitOfWork = this.UnitOfWork.Get(context);


            UpdateWorkLog(unitOfWork, step, backupFile, userId);

            step.SetResolution(true, userId);
            //
            // And set the state machine's state
            //
            this.OutState.Set(context, WorkflowState.Complete);
        }
        /// <summary>
        /// Check-in a summary statement into the same workflow step.
        /// </summary>
        /// <param name="context">Workflow Activity Context object</param>
        protected virtual void AssignToSameWorkflowStep(CodeActivityContext context)
        {
            //
            // Since we are assigning to the same step we don't need to do go through
            // content promotion, Resolved calculation etc.  All we need to do is just
            // set the WorkLog to completed.
            //
            // Retrieve the parameters update the WorkLog.
            //
            ApplicationWorkflowStep step = GetCurrentWorkflowStep(context);
            int userId = this.UserId.Get(context);
            byte[] backupFile = this.BackupFile.Get(context);
            IUnitOfWork unitOfWork = this.UnitOfWork.Get(context);

            ChangeResolveState(step, step, userId);
            UpdateWorkLog(unitOfWork, step, backupFile, userId);
            //
            // And set the state machine's state
            //
            this.OutState.Set(context, WorkflowState.Default);
        }
        /// <summary>
        /// Executes the Assign specific business rules.  The Assign activity moves a workflow from
        /// it's current step to a user selected step in the workflow.
        /// </summary>
        /// <param name="context">Workflow Activity Context object</param>
        /// <param name="targetworkflowStepId">Target ApplicationWorkflowStep entity identifier</param>
        protected virtual void ReassignToSpecificWorkflowStep(CodeActivityContext context, int targetworkflowStepId)
        {
            //
            // Retrieve the parameters
            //
            ApplicationWorkflowStep step = GetCurrentWorkflowStep(context);
            int userId = this.UserId.Get(context);
            byte[] backupFile = this.BackupFile.Get(context);
            IUnitOfWork unitOfWork = this.UnitOfWork.Get(context);

            //
            // Moving to a previous work flow step
            // 1. move previous and current workflow step data to ApplicationWorkflowStepElementContentHistory
            // 2  move current content step data to the target workflow step
            // 3. delete previous workflow step data from ApplicatinWorkflowStepElementContent
            //
            ApplicationWorkflowStep targetStep = step.ApplicationWorkflow.GetThisStep(targetworkflowStepId);

            List<ApplicationWorkflowStepElementContent> promptedContent = step.Promote(targetStep, userId);
            unitOfWork.ApplicationWorkflowStepElementContentRepository.Add(promptedContent);

            // Manipulate document data
            backupFile = ManipulateDocumentData(targetStep, backupFile);
            UpdateWorkLog(unitOfWork, step, backupFile, userId);

            // Delete previous workflow step data from ApplicatinWorkflowStepElementContent
            unitOfWork.ApplicationWorkflowStepElementContentRepository.DeletePostTargetStepElementContent(targetStep.ApplicationWorkflowId, targetworkflowStepId, userId);
            ChangeResolveState(step, targetStep, userId);
            //
            // Overridden method to enable derived classes to perform post assign processing, parent class does nothing
            //
            WorkflowPostAssignProcessing(step.ApplicationWorkflow, userId); 
            //
            // This is the state machine's state now.
            //
            this.OutState.Set(context, WorkflowState.Default);

            return;
        }
        /// <summary>
        /// Update the WorkLog entry
        /// </summary>
        /// <param name="unitOfWork">Unit of work provides access to the entity framework</param>
        /// <param name="step">ApplicationWorkflowStep entity</param>
        /// <param name="backupFile">Bakcup file</param>
        /// <param name="userId">User entity identifier</param>
        private void UpdateWorkLog(IUnitOfWork unitOfWork, ApplicationWorkflowStep step, byte[] backupFile, int userId)
        {
            ApplicationWorkflowStepWorkLog entity = unitOfWork.ApplicationWorkflowStepWorkLogRepository.FindInCompleteWorkLogEntryByWorkflowStep(step.ApplicationWorkflowStepId);

            if (entity != null)
            {
                //
                // Update it as completed
                //
                entity.Complete(userId, backupFile);
            }
        }
        /// <summary>
        /// Manipulates the document data.
        /// </summary>
        /// <param name="targetStep">The target step.</param>
        /// <param name="backupFile">The backup file.</param>
        /// <returns></returns>
        private byte[] ManipulateDocumentData(ApplicationWorkflowStep targetStep, byte[] backupFile)
        {
            if (targetStep.StepTypeId == StepType.Indexes.Review)
            {
                backupFile = WordServices.AcceptTrackChangesAndRemoveComments(backupFile);
            }
            return backupFile;
        }
        /// <summary>
        /// Set or reset the Resolved attribute of all states between the current ApplicationWorkflowStep
        /// and the target ApplicationWorkflowStep.
        /// </summary>
        /// <param name="currentWorkflowStepEntity">Current ApplicationWorkflowStep entity</param>
        /// <param name="targetWorkflowStepEntity">Target ApplicationWorkflowStep entity</param>
        /// <param name="userId">User entity identifier</param>
        protected virtual void ChangeResolveState(ApplicationWorkflowStep currentWorkflowStepEntity, ApplicationWorkflowStep targetWorkflowStepEntity, int userId)
        {
            //
            // Change the workflow's steps state to not resolved all the way back to the target workflow step
            //
            currentWorkflowStepEntity.ApplicationWorkflow.SetResetResolved(currentWorkflowStepEntity, targetWorkflowStepEntity, userId);
        }
        /// <summary>
        /// Copies the content between step elements (source to target).
        /// </summary>
        /// <param name="sourceStep">Source workflow step</param>
        /// <param name="targetStep">Target workflow step</param>
        /// <param name="contentList">List of content element to copy</param>
        /// <param name="userId">User identifier</param>
        private void CopyContentToSpecificStep(ApplicationWorkflowStep sourceStep, ApplicationWorkflowStep targetStep, List<ApplicationWorkflowStepElementContent> contentList, int userId)
        {
            foreach (var item in contentList)
            {
                targetStep.CopyContentFromOtherWorkflowStep(sourceStep, userId);
            }
        }
        /// <summary>
        /// Virtual method,that when overridden, can be used for workflow post assign processing 
        /// such as resetting date closed to null
        /// </summary>
        /// <param name="applicationWorkflow">Application workflow</param>
        /// <param name="userId">applicationWorkflow</param>
        protected virtual void WorkflowPostAssignProcessing(ApplicationWorkflow applicationWorkflow, int userId) {}
        /// <summary>
        /// Gets current workflow step object
        /// </summary>
        /// <param name="context">Workflow Activity Context object</param>
        /// <returns>Current Work flow step object</returns>
        protected virtual ApplicationWorkflowStep GetCurrentWorkflowStep(CodeActivityContext context)
        {
            return this.WorkflowStep.Get(context);
        }
        /// <summary>
        /// Promotes the current step to the last phase.  If the ApplicationWorkflow
        /// is already on the last phase no promotion is performed.
        /// </summary>
        /// <param name="context">Workflow Activity Context object</param>
        protected virtual void MoveToLastPhase(CodeActivityContext context)
        {
            ApplicationWorkflowStep step = GetCurrentWorkflowStep(context);
            int userId = this.UserId.Get(context);
            IUnitOfWork unitOfWork = this.UnitOfWork.Get(context);
            int targetworkflowStepId = this.TargetWorkflowId.Get(context);

            ApplicationWorkflowStep lastStep  = step.ApplicationWorkflow.LastStep();
            //
            // if the workflow is being completed from a step other than the last step
            // the content needs to be promoted to the last step.
            //
            if (step.ApplicationWorkflowStepId != lastStep.ApplicationWorkflowStepId)
            {
                ReassignToSpecificWorkflowStep(context, lastStep.ApplicationWorkflowStepId);
            }
        }
        /// <summary>
        /// Hook to deal with any subsequent workflow steps.
        /// </summary>
        /// <param name="context">Workflow Activity Context object</param>
        /// <param name="currentApplicationWorkflowStepId">Current ApplicationWorkflowStep entity identifier</param>
        protected virtual void RevertSubsequentWorkflowSteps(CodeActivityContext context, int currentApplicationWorkflowStepId)
        {
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Determines if the Activity parameters are valid
        ///   - workflow step is not null
        ///   - user id is greater than 0
        ///   - unit of work is not null
        ///   - target WorkflowStep entity identifier is not less than ApplicationWorkflowStep.CompleteWorkflow
        /// </summary>
        /// <param name="workflowStepId">Workflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="unitOfWork">Unit of work provides access to the entity framework</param>
        /// <returns>True if all parameters are valid; false otherwise</returns>
        protected bool IsActivityParametersValid(ApplicationWorkflowStep step, int userId, IUnitOfWork unitOfWork, int targetworkflowStepId)
        {
            return (IsActivityParametersValid(step, userId, unitOfWork) &&
                   (targetworkflowStepId >= ApplicationWorkflowStep.CompleteWorkflow)
                   );
        }
        #endregion
    }
}
