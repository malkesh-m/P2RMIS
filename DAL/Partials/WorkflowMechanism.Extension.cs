using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's WorkflowMechanism object.
    /// </summary>
    public partial class WorkflowMechanism: IDateFields
    {
        /// <summary>
        /// Assigning a workflow to a mechanism.
        /// </summary>
        /// <param name="mechanismId">Mechanism identifier</param>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="userId">User identifier making the change</param>
        public void Populate(int mechanismId, int workflowId, int? reviewId, int userId)
        {
            this.MechanismId = mechanismId;
            this.WorkflowId = workflowId;
            this.ReviewStatusId = reviewId;
            Helper.UpdateModifiedFields(this, userId);
        }
        /// <summary>
        /// Assigning a workflow to a mechanism when entity object created.
        /// </summary>
        /// <param name="mechanismId">Mechanism identifier</param>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="userId">User identifier making the change</param>
        public void Create(int mechanismId, int workflowId, int? reviewId, int userId)
        {
            Populate(mechanismId, workflowId, reviewId, userId);
            Helper.UpdateCreatedFields(this, userId);
        }
        /// <summary>
        /// Wrapper indicating entity object exists
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <returns>True if entity object is not null; false otherwise</returns>
        public static bool Exists(WorkflowMechanism entity)
        {
            return (entity != null);
        }
        /// <summary>
        /// Tests if the relationship between mechanism & workflow has changed.
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <returns>True if relationship has changed; false otherwise</returns>
        public bool Changed(int workflowId, int? reviewStatusId)
        {
            return ((this.WorkflowId != workflowId) || ((reviewStatusId.HasValue) && (this.ReviewStatusId != reviewStatusId)));
        }

    }
}
