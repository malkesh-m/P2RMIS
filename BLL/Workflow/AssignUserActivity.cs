using System;
using System.Activities;
using System.Collections;
using System.Collections.Generic;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Business rules for the Assign User function
    /// </summary>
    public class AssignUserActivity : P2rmisActivity
    {
        #region Constants
        /// <summary>
        /// Number of activity specific arguments.  Used to size the hashset.
        /// </summary>
        public static readonly int ActivityArgumentCount = 1; 
        
        private const string ErrorMessage = "AssignUserActivity.Execute() detected invalid arguments: ApplicationWorkflowStep is null [{0}] userId [{1}] UnitOfWork is null? [{2}]";
        private const string ParameterErrorMessage = "AssignUserActivity.SetParameters() detected invalid arguments: list is null? [{0}] list entries count [{1}]";
        
        #endregion
        #region Classes
        //
        // Identify the activity specific parameters
        //
        public enum AssignUserParameters
        {
            Default = 0,
            //
            // This is the AssigneeId value
            //
            AssigneeId
        }
        #endregion
        #region Parameters
        /// <summary>
        /// Assignee user id
        /// </summary>
        public InArgument<int?> AssigneeUserId { get; set; }
        #endregion
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        public AssignUserActivity() : base() { }
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
                this.AssigneeUserId = (int?)values[AssignUserParameters.AssigneeId];
            }
            catch
            {
                string message = string.Format(ParameterErrorMessage, (values == null), ((values != null) ? values.Count.ToString() : string.Empty));
                throw new ArgumentException(message);
            }
        }

        #endregion
        #region Business Rule Execution
        /// <summary>
        /// Executes the Assign specific business rules.  The AssignUser activity assigns a user to 
        /// a specific workflow step
        /// </summary>
        /// <param name="context">Workflow Activity Context object</param>
        protected override void Execute(CodeActivityContext context)
        {
            //
            // Retrieve the parameters
            //
            ApplicationWorkflowStep step = this.WorkflowStep.Get(context);
            int userId = this.UserId.Get(context);
            IUnitOfWork unitOfWork = this.UnitOfWork.Get(context);

            int? assigneeId = this.AssigneeUserId.Get(context);

            if (IsActivityParametersValid(step, userId, unitOfWork))
            {
                WorkflowHelper.AssignTheWorkflowStep(unitOfWork, userId, assigneeId, step.ApplicationWorkflowStepId);
                //
                // If this workflow step is currently checked out then it should be closed and the content saved.
                //
                ApplicationWorkflowStepWorkLog entity = unitOfWork.ApplicationWorkflowStepWorkLogRepository.FindInCompleteWorkLogEntryByWorkflowStep(step.ApplicationWorkflowStepId);
                if (entity != null)
                {
                    //
                    // Update it as completed
                    //
                    entity.Complete(userId);
                    //
                    // Create an entry in the history table
                    //
                    ICollection<ApplicationWorkflowStepElementContentHistory> historyEntities = step.CreateHistory(userId, entity.ApplicationWorkflowStepWorkLogId);
                    unitOfWork.ApplicationWorkflowStepElementContentHistoryRepository.AddRange(historyEntities);
                }
                //
                // Now all we need to do is check-out the summary statement for the assignee 
                // The only thing that happens when a summary statement is checked out
                // is an entry is added into the ApplicationWorkflowStepWorkLog.
                //
                if (assigneeId != null)
                {
                    ApplicationWorkflowStepWorkLog logEntity = new ApplicationWorkflowStepWorkLog();
                    logEntity.Initialize(this.WorkflowStep.Get(context).ApplicationWorkflowStepId, (int)assigneeId, this.WorkflowStep.Get(context).GetCurrentDocument());
                    unitOfWork.ApplicationWorkflowStepWorkLogRepository.Add(logEntity);
                }
                //
                // This is the state machine's state now.
                //
                this.OutState.Set(context, WorkflowState.Started);
            }
            else
            {
                String message = string.Format(ErrorMessage, (step == null), userId, (unitOfWork == null));
                throw new ArgumentException(message);
            }
            return;
        }
        #endregion
    }
}
