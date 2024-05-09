using System;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Wrapper containing the parameters for assigning a user to an application for review purposes.
    /// </summary>
    public class AssignUserToApplication
    {
        #region Properties
        /// <summary>
        /// User Id of assignee.
        /// </summary>
        internal int UserId { get; set; }
        /// <summary>
        /// User Id of assignee.
        /// </summary>
        internal int? AssigneeId { get; set; }
        /// <summary>
        /// The associated workflow Id
        /// </summary>
        internal int WorkflowId { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userId">Assignee identifier</param>
        /// <param name="workflowId">Associated workflow identifier</param>
        public AssignUserToApplication(int userId, string assigneeId, string workflowId)
        {
            this.UserId = userId;
            this.AssigneeId = assigneeId != null && !string.IsNullOrWhiteSpace(assigneeId) ? Convert.ToInt32(assigneeId) : (int?)null;
            this.WorkflowId = Convert.ToInt32(workflowId);
        }
        #endregion
    }
}
