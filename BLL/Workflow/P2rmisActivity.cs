using System;
using System.Activities;
using System.Collections;
using System.Collections.Generic;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Base for all P2RMIS activities
    /// </summary>
    public class P2rmisActivity : CodeActivity, IP2rmisActivity
    {
        #region Constructor and set up
        /// <summary>
        /// Constructor
        /// </summary>
        internal P2rmisActivity()
        {

        }
        #endregion
        #region Properties
        /// <summary>
        /// Input argument names
        /// </summary>
        public enum InArgumentNames
        {
            /// <summary>
            /// All enums should have a default
            /// </summary>
            Default = 0,
            /// <summary>
            /// The in parameter name for the EntityFramework
            /// </summary>
            EntityFramework = 1,
            /// <summary>
            /// The in parameter name for the workflow step state
            /// </summary>
            InState = 2,
            /// <summary>
            /// The in parameter name for the ApplicationWorkflowStep
            /// </summary>
            WorkflowStep = 3,
            /// <summary>
            /// The UserId
            /// </summary>
            UserId = 4
        }
        /// <summary>
        /// Entity Framework
        /// </summary>
        public InArgument<IUnitOfWork> UnitOfWork { get; set; }
        /// <summary>
        /// Current workflow step object
        /// </summary>
        public InArgument<ApplicationWorkflowStep> WorkflowStep { get; set; }
        /// <summary>
        /// User Id
        /// </summary>
        public InArgument<int> UserId { get; set; }
        /// <summary>
        /// Output argument names
        /// </summary>
        public enum OutArgumentNames
        {
            /// <summary>
            /// All enums should have a default
            /// </summary>
            Default = 0,
            /// <summary>
            /// State of workflow after the Activity completes.
            /// </summary>
            OutState = 1
        }
        /// <summary>
        /// State of workflow after the Activity completes.
        /// </summary>
        public OutArgument<WorkflowState> OutState { get; set; }
        #endregion
        #region Activity Methods
        /// <summary>
        /// Execute Activity Specific code here.  All derived classes
        /// must override this method and supply Activity specific actions.
        /// </summary>
        /// <param name="context">Workflow Activity Context object</param>
        protected override void Execute(CodeActivityContext context)
        {
            throw new NotImplementedException("Execute method is not implemented in P2rmisActivity base class.");
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Determines if the Activity parameters are valid
        ///   - workflow step is not null
        ///   - user id is greater than 0
        ///   - unit of work is not null
        /// </summary>
        /// <param name="workflowStepId">Workflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="unitOfWork">Unit of work provides access to the entity framework</param>
        /// <returns>True if all parameters are valid; false otherwise</returns>
        protected bool IsActivityParametersValid(ApplicationWorkflowStep step, int userId, IUnitOfWork unitOfWork)
        {
            return (
                    (step != null) &&
                    (userId > 0) &&
                    (unitOfWork != null)
                   );
        }
        /// <summary>
        /// Initializes any Activity specific parameters.
        /// </summary>
        /// <param name="list">List of activity specific parameters</param>
        public virtual void SetParameters(IDictionary list)
        {
            //
            // Default behavior is to do nothing.  If an activity need any parameters
            // other than the general parameters they should be set here.
            //
        }
        /// <summary>
        /// Initializes any Activity specific results for return.
        /// </summary>
        /// <param name="results">Activity results</param>
        /// <param name="resultsList">List for activity specific results</param>
        public virtual IDictionary GetResults(IDictionary<string, object> results, IDictionary resultsList)
        {
            //
            // Default behavior is to return an empty IDictionary object.  If an activity needs to
            // return specific values other than state it should be set here.
            //
            return resultsList;
        }
        /// <summary>
        /// Gets the current workflow step for the current workflow
        /// </summary>
        /// <param name="theWorkflow">The application workflow entity</param>
        /// <returns></returns>
        public virtual ApplicationWorkflowStep GetCurrentWorkflowStep(ApplicationWorkflow theWorkflow)
        {
            return theWorkflow.CurrentStep();
        }
        #endregion
    }
}
