using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's Workflow object.
    /// </summary>
    public partial class Workflow
    {
        private const string COPY_ID = "- Copy";
        /// <summary>
        /// Copy a workflow into a newly created workflow. Fields that are not copied
        /// are:
        ///     - Name: name is copied but an identifier string is appended
        ///     - Created: new user id and current time used
        ///     - Modified: new user id and current time used
        ///     - workflow steps: new copies created
        /// </summary>
        /// <param name="userId">User id of user copying the workflow</param>
        /// <returns>New workflow returned</returns>
        public Workflow Copy(int userId)
        {
            //
            // Create a new workflow object ....
            //
            Workflow result = new Workflow();
            //
            // and copy over the data
            //
            result.ClientId = this.ClientId;
            result.WorkflowName = this.WorkflowName + COPY_ID;
            result.WorkflowDescription = this.WorkflowDescription;
            //
            // and set the unique values
            //
            result.CreatedBy = userId;
            result.CreatedDate = GlobalProperties.P2rmisDateTimeNow;
            result.ModifiedBy = userId;
            result.ModifiedDate = GlobalProperties.P2rmisDateTimeNow;
            //
            // Now replicate the steps
            //
            foreach (WorkflowStep step in WorkflowSteps)
            {
                WorkflowStep copy = step.Copy(userId);
                result.WorkflowSteps.Add(copy);
            }
            return result;
        }
    }
}
