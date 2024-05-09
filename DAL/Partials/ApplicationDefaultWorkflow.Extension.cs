using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's Application object.
    /// </summary>
    public partial class ApplicationDefaultWorkflow : IDateFields
    {
        /// <summary>
        /// Populate the ApplicationDefaultWorkflow
        /// </summary>
        /// <param name="applicationId">Application Identifier of application</param>
        /// <param name="workflowId">Workflow Identifier of workflow to use as default</param>
        /// <param name="userId">User Identifier</param>
        public void Populate(int applicationId, int workflowId, int userId)
        {
            this.ApplicationId = applicationId;
            this.WorkflowId = workflowId;
            Helper.UpdateCreatedFields(this, userId);
            Helper.UpdateModifiedFields(this, userId);
        }
        /// <summary>
        /// Updates an existing default workflow with the new workflow value and
        /// updates the modify time.
        /// </summary>
        /// <param name="workflowId">Workflow Identifier of workflow to use as default</param>
        /// <param name="userId">User Identifier</param>
        public void Update(int workflowId, int userId)
        {
            this.WorkflowId = workflowId;
            Helper.UpdateModifiedFields(this, userId);
        }
    }
}
