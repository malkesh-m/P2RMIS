namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for WorkflowInstance objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public interface IWorkflowInstanceRepository
    {
        /// <summary>
        /// Determines if the instance id is valid
        /// </summary>
        /// <param name="instanceId">Instance Id</param>
        /// <returns></returns>
        bool IsInstanceValid(int instanceId);

    }
}
