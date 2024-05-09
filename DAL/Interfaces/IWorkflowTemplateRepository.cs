namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for WorkflowTepmplate objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public interface IWorkflowTemplateRepository: IGenericRepository<Workflow>
    {
        /// <summary>
        /// Returns the Workflow Template identified by the specific index
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// !!!!!!!!!!!!!!!!!!!!!!!!  TYPES NEED TO BE CHANGED WHEN CRAIG DEFINES THE OBJECTS
        //Workflow GetByID(object id);
        /// <summary>
        /// Determines if the temple id is valid
        /// </summary>
        /// <param name="templateId">Template Id</param>
        /// <returns></returns>
        bool IsTemplateValid(int templateId);

    }
}
