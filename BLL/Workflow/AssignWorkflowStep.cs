using System;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Container for assigning one workflow to a previous step when 
    /// multiple assignments are specified.
    /// </summary>
    public class AssignWorkflowStep
    {
        #region Attributes
        /// <summary>
        /// Workflow identifier
        /// </summary>
        public int WorkflowId { get; private set; }
        /// <summary>
        /// Target workflow step identifier
        /// </summary>
        public int TargetWorkflowStepId { get; private set; }
        #endregion
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="targetWorkflowStepId">Target workflow step identifier</param>
        public AssignWorkflowStep(int workflowId, int targetWorkflowStepId)
        {
            this.WorkflowId = workflowId;
            this.TargetWorkflowStepId = targetWorkflowStepId;
        }
        /// <summary>
        /// Creates an AssignWorkflowStep object if the currentStepId is different than
        /// the targetStepId.
        /// </summary>
        /// <param name="currentWorkflowId">Current workflow step identifier</param>
        /// <param name="targetWorkflowId">Target workflow step identifier</param>
        /// <returns><Newly created parameter object or null if the currentStep is the same as the targetStep/returns>
        public static AssignWorkflowStep Create(string workflowId, string currentWorkflowStepId, string targetWorkflowStepId)
        {
            AssignWorkflowStep result = null;

            int targetStep = Convert.ToInt32(targetWorkflowStepId);
            int currentStep = Convert.ToInt32(currentWorkflowStepId);
            if (targetStep != currentStep)
            {
                int workflow = Convert.ToInt32(workflowId);
                result = new AssignWorkflowStep(workflow, targetStep);
            }

            return result;
        }
        #endregion
    }
}
