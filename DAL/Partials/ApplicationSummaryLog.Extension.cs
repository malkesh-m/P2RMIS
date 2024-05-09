using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ApplicationSummaryLog object.
    /// </summary>
    public partial class ApplicationSummaryLog: IDateFields
    {
        /// <summary>
        /// Populate an instance of the ApplicationSummaryLog entity object.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="workflowStepId">Workflow step identifier</param>
        /// <param name="isComplete">Is the workflow complete?  If it is it will not have a current step identifier</param>
        public void Populate(int userId, int? workflowStepId, bool isComplete)
        {
            this.UserId = userId;
            this.ApplicationWorkflowStepId = workflowStepId;
            this.CompletedFlag = isComplete;
        }
    }
}
