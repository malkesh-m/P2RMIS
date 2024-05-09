using System;
using System.Activities;
using System.Collections;
using System.Collections.Generic;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Activity performs the business rules associated with the summary
    /// statement check-out.
    /// </summary>
    internal class CheckoutActivity : P2rmisActivity
    {
        #region Constants
        private const string ErrorMessage = "CheckoutActivity detected invalid arguments: ApplicationWorkflowStep is null [{0}] userId [{1}] UnitOfWork is null [{2}]";
        private const string ResultErrorMessage = "AssignUserActivity.GetResults() detected invalid arguments: list is null? [{0}]";
        /// <summary>
        /// Number of out results
        /// </summary>
        public static readonly int ResultCount = 1;
        #endregion
        #region Classes
        //
        // Identify the activity specific parameters
        //
        public new enum OutArgumentNames
        {
            Default = 0,
            //
            // Indicates if the summary statement was already checked out
            //
            WasCheckedOut
        }
        #endregion
        #region Properties
        /// <summary>
        /// If true checkout was not performed because another user had the summary statement checked out.
        /// </summary>
        public OutArgument<bool> WasCheckedOut { get; set; }
        #endregion
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        public CheckoutActivity (): base() {}
        /// <summary>
        /// Initializes any Activity specific results for return.
        /// </summary>
        /// <param name="results">Activity results</param>
        /// <param name="resultsList">List for activity specific results</param>
        public override IDictionary GetResults(IDictionary<string, object> results, IDictionary resultsList)
        {
            //
            // just try setting the results.  If anything is wrong throw
            // and exception.
            //
            try
            {
                bool value = (bool)results[CheckoutActivity.OutArgumentNames.WasCheckedOut.ToString()];
                resultsList.Add(CheckoutActivity.OutArgumentNames.WasCheckedOut.ToString(), value);
            }
            catch (Exception ex)
            {
                string message = string.Format(ResultErrorMessage, (resultsList == null));
                throw new ArgumentException(message, ex);
            }
            return resultsList;
        }
        #endregion
        #region Business Rule Execution
        /// <summary>
        /// Executes the Checkout specific business rules.
        /// </summary>
        /// <param name="context">Workflow Activity Context object</param>
        protected override void Execute(CodeActivityContext context)
        {
            //
            // Set up the return value
            ///
            WasCheckedOut.Set(context, false);
            //
            // Get the incoming parameter values
            //
            ApplicationWorkflowStep step = this.WorkflowStep.Get(context);
            int userId = this.UserId.Get(context);
            IUnitOfWork unitOfWork = this.UnitOfWork.Get(context);
            if (IsCheckoutActivityParametersValid(step, userId, unitOfWork))
            {
                int workflowStepId = this.WorkflowStep.Get(context).ApplicationWorkflowStepId;
                //
                // First one needs to make sure someone else does not have the summary statement checked out.
                //
                if (unitOfWork.SummaryManagementRepository.IsSsCheckedOut(step.ApplicationWorkflowId))
                {
                    //
                    // Someone has it checked out and we do not want to change the workflow state.
                    //
                    WasCheckedOut.Set(context, true);
                    this.OutState.Set(context, WorkflowState.Default);
                }
                else
                {
                    //
                    // The only thing that happens when a summary statement is checked out
                    // is an entry is added into the ApplicationWorkflowStepWorkLog.
                    //
                    ApplicationWorkflowStepWorkLog entity = new ApplicationWorkflowStepWorkLog();
                    entity.Initialize(workflowStepId, userId, this.WorkflowStep.Get(context).GetCurrentDocument());
                    unitOfWork.ApplicationWorkflowStepWorkLogRepository.Add(entity);
                    //
                    // Now deal with the assignment
                    //
                    WorkflowHelper.AssignTheWorkflowStep(unitOfWork, userId, workflowStepId);
                    //
                    // This is the state machine's state now.
                    //
                    this.OutState.Set(context, WorkflowState.Started);
                }
            }
            else
            {
                String message = string.Format(ErrorMessage, (step == null), userId, (unitOfWork == null));
                throw new ArgumentException(message);
            }
        }        
        #endregion
        #region Helpers
        /// <summary>
        /// Determines if the CheckoutActivity parameters are valid
        ///  - workflow id is greater than 0
        ///   - user id is greater than 0
        ///   - unit of work is not null
        /// </summary>
        /// <param name="workflowStepId">Workflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="unitOfWork">Unit of work provides access to the entity framework</param>
        /// <returns></returns>
        private bool IsCheckoutActivityParametersValid(ApplicationWorkflowStep step, int userId, IUnitOfWork unitOfWork)
        {
            return (
                    (step != null) &&
                    (userId > 0) &&
                    (unitOfWork != null)
                   );
        }
        #endregion
    }
}
