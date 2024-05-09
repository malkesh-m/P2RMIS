using System;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.SummaryStatements
{
    /// <summary>
    /// Wrapper containing the workflow/mechanism changeable values.
    /// </summary>
    public class AssignWorkflowToSave
    {
        #region Properties
        /// <summary>
        /// Mechanism Id
        /// </summary>
        internal int MechanismId { get; set; }
        /// <summary>
        /// The associated workflow Id
        /// </summary>
        internal int WorkflowId { get; set; }
        /// <summary>
        // THe review status id (is this for a Priority 1; Priority 2 or no priority
        /// </summary>
        internal int? ReviewStatusId { get; set; }
        #endregion
        #region Constructor
        public AssignWorkflowToSave() { }
        /// <summary>
        /// Helper method to set Mechanism & WorkflowId values
        /// </summary>
        /// <param name="mechanismId">Mechanism identifier</param>
        /// <param name="workflowId">Workflow identifier</param>
        private void SetWorkflow(string mechanismId, string workflowId)
        {
            this.MechanismId = Convert.ToInt32(mechanismId);
            this.WorkflowId = Convert.ToInt32(workflowId);
        }
        /// <summary>
        /// Sets the parameter values to priority one
        /// </summary>
        /// <param name="mechanismId">Mechanism identifier</param>
        /// <param name="workflowId">Workflow identifier</param>
        public void SetPriorityOneWorkflow(string mechanismId, string workflowId)
        {
            SetWorkflow(mechanismId, workflowId);
            this.ReviewStatusId = ReviewStatu.PriorityOne;
        }
        /// <summary>
        /// Sets the parameter values to priority two
        /// </summary>
        /// <param name="mechanismId">Mechanism identifier</param>
        /// <param name="workflowId">Workflow identifier</param>
        public void SetPriorityTwoWorkflow(string mechanismId, string workflowId)
        {
            SetWorkflow(mechanismId, workflowId);
            this.ReviewStatusId = ReviewStatu.PriorityTwo;
        }
        /// <summary>
        /// Sets the parameter values to no priority
        /// </summary>
        /// <param name="mechanismId">Mechanism identifier</param>
        /// <param name="workflowId">Workflow identifier</param>
        public void SetNoPriorityWorkflow(string mechanismId, string workflowId)
        {
            SetWorkflow(mechanismId, workflowId);
            this.ReviewStatusId = ReviewStatu.NoPriority;
        }
        #endregion
    }
}
