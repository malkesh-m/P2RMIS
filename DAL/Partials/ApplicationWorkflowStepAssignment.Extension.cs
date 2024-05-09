using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
     /// <summary>
    /// Custom methods for Entity Framework's ApplicationWorkflowStepAssignment object.
    /// </summary>
    public partial class ApplicationWorkflowStepAssignment: IStandardDateFields
    {
        /// <summary>
        /// Populate the ApplicationWorkflowStepAssignment.  This version is for when the assigner and assignee are different.
        /// </summary>
        /// <param name="assigneeUserId">Assignee identifier</param>
        /// <param name="assignerUserId">Assigner identifier</param>
        /// <param name="ApplicationWorkflowStepId">Workflow step identifier</param>
        /// <returns>ApplicationWorkflowStepAssignment entity object</returns>
        public virtual ApplicationWorkflowStepAssignment Populate(int assigneeUserId, int assignerUserId, int ApplicationWorkflowStepId)
        {
            this.ApplicationWorkflowStepId = ApplicationWorkflowStepId;
            this.UserId = assigneeUserId;
            this.AssignmentId = 1;

            Helper.UpdateCreatedFields(this, assignerUserId);
            Helper.UpdateModifiedFields(this, assignerUserId);

            return this;
        }
        /// <summary>
        /// Populate the ApplicationWorkflowStepAssignment.  This version is for when the assigner and assignee are the same.
        /// </summary>
        /// <param name="assigneeUserId">User identifier</param>
        /// <param name="ApplicationWorkflowStepId">Workflow step identifier</param>
        /// <returns>ApplicationWorkflowStepAssignment entity object</returns>
        public virtual ApplicationWorkflowStepAssignment Populate(int userId, int applicationWorkflowStepId)
        {
            return Populate(userId, userId, applicationWorkflowStepId);
        }
        /// <summary>
        /// Change the assignment to a another user. This version is for when the assigner and assignee are different.
        /// </summary>
        /// <param name="assigneeUserId">Assignee identifier</param>
        /// <param name="assignerUserId">AssignerUser identifier</param>
        /// <returns>ApplicationWorkflowStepAssignment entity object</returns>
        public virtual ApplicationWorkflowStepAssignment ChageToThisUser(int assigneeUserId, int assignerUserId)
        {
            this.UserId = assigneeUserId;
            Helper.UpdateModifiedFields(this, assignerUserId);
            return this;
        }
        /// <summary>
        /// Change the assignment to a another user. This version is for when the assigner and assignee are the same.
        /// </summary>
        /// <param name="userId">Assignee identifier</param>
        /// <param name="assignerUserId">AssignerUser identifier</param>
        /// <returns>ApplicationWorkflowStepAssignment entity object</returns>
        public virtual ApplicationWorkflowStepAssignment ChageToThisUser(int userId)
        {
            this.UserId = userId;
            Helper.UpdateModifiedFields(this, userId);
            return this;
        }
        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public virtual void Delete(int userId)
        {
            Helper.UpdateDeletedFields(this, userId);
        }
    }
}
