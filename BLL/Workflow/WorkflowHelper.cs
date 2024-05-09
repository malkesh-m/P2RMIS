using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Collection of helper methods
    /// </summary>
    internal static class WorkflowHelper
    {
        /// <summary>
        /// Assign user to workflow step
        /// </summary>
        /// <param name="unitOfWork">Unit of work provides access to the entity framework</param>
        /// <param name="userId">User identifier of user being assigned and doing the assign</param>
        /// <param name="workflowStepId">Workflow step identifier</param>
        internal static void AssignTheWorkflowStep(IUnitOfWork unitOfWork, int userId, int workflowStepId)
        {
            ApplicationWorkflowStepAssignment assignment = unitOfWork.ApplicationWorkflowStepAssignmentRepository.GetStepAssignment(workflowStepId);
            if (assignment == null)
            {
                assignment = new ApplicationWorkflowStepAssignment();
                assignment.Populate(userId, workflowStepId);
                unitOfWork.ApplicationWorkflowStepAssignmentRepository.Add(assignment);
            }
            else
            {
                assignment.ChageToThisUser(userId);
                unitOfWork.ApplicationWorkflowStepAssignmentRepository.Update(assignment);
            }
        }

        /// <summary>
        /// Assign user to workflow step
        /// </summary>
        /// <param name="unitOfWork">Unit of work provides access to the entity framework</param>
        /// <param name="assignerId">User identifier of user performing the assigning</param>
        /// <param name="assigneeId">User identifier of user being assigned</param>
        /// <param name="workflowStepId">Workflow step identifier</param>
        internal static ApplicationWorkflowStepAssignment AssignTheWorkflowStep(IUnitOfWork unitOfWork, int assignerId, int? assigneeId, int workflowStepId)
        {
            ApplicationWorkflowStepAssignment assignment = unitOfWork.ApplicationWorkflowStepAssignmentRepository.GetStepAssignment(workflowStepId);
            if (assignment == null)
            {
                if (assigneeId != null)
                {
                    assignment = new ApplicationWorkflowStepAssignment();
                    assignment.Populate((int)assigneeId, assignerId, workflowStepId);
                    unitOfWork.ApplicationWorkflowStepAssignmentRepository.Add(assignment);
                }
            }
            else
            {
                if (assigneeId != null)
                {
                    assignment.ChageToThisUser((int)assigneeId, assignerId);
                    unitOfWork.ApplicationWorkflowStepAssignmentRepository.Update(assignment);
                } 
                else
                {
                    assignment.Delete(assignerId);
                }
            }
            return assignment;
        }
    }
}