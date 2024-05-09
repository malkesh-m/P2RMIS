using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's WorkflowStep object.
    /// </summary>
    public partial class WorkflowStep
    {
        private const string COPY_ID = "- Copy";

        /// <summary>
        /// Copy a workflow step into a new workflow step.
        /// 
        /// An assumption is that the workflowstep is being copied into a newly
        /// created workflow and the id does not exist yet.
        /// </summary>
        /// <param name="userId">User id of user copying the workflow</param>
        /// <returns>Workflow step created</returns>
        public WorkflowStep Copy(int userId)
        {
            //
            // Create a new WorkflowStep object ....
            //
            WorkflowStep result = new WorkflowStep();
            //
            // and copy over the data
            //
            result.StepName = this.StepName;
            result.StepOrder = this.StepOrder;
            result.ActiveDefault = this.ActiveDefault;
            //
            // and set the unique values
            //
            result.CreatedBy = userId;
            result.CreatedDate = GlobalProperties.P2rmisDateTimeNow;
            result.ModifiedBy = userId;
            result.ModifiedDate = GlobalProperties.P2rmisDateTimeNow;
            //
            return result;
        }
    }
}
