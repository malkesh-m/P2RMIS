using System.Collections.Generic;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.Reports;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for ApplicationWorkflow objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public interface IApplicationWorkflowRepository: IGenericRepository<ApplicationWorkflow>
    {
        /// <summary>
        /// Locates & returns the next active workflow step for the identified workflow after
        /// the current step.
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="currentStepOrder">Current ApplicationWorkflowStep order</param>
        /// <returns>First active workflow step after the current step</returns>
        ApplicationWorkflowStep GetNextStep(int workflowId, int currentStepOrder);
        /// <summary>
        /// Retrieves information about workflow progress for a single application workflow.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application workflow</param>
        /// <returns>Zero or more workflow steps for a given application workflow</returns>
        ResultModel<ApplicationWorkflowStepModel> GetSingleWorkflowProgress(int applicationWorkflowId);
         /// <summary>
        /// retrieves the active workflow step for an application
        /// </summary>
        /// <param name="applicationWorkflowId">the applications workflow id</param>
        /// <returns>integer of the active workflow step</returns>
        int GetActiveApplicationWorkflowStep(int applicationWorkflowId);
        /// <summary>
        /// Retrieve a list of report information for the supplied panel application ids.
        /// </summary>
        /// <param name="panelApplicationIds">the panel application ids</param>
        /// <returns>Enumerable list of report information.</returns>
        ResultModel<IReportAppInfo> GetReportAppInfoList(int[] panelApplicationIds);
        /// <summary>
        /// Retrieves the ApplicationWorkflow collection
        /// </summary>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>A collection of application workflow</returns>
        IEnumerable<ApplicationWorkflow> GetWorkflows(int panelUserAssignmentId, int panelApplicationId);

        /// <summary>
        /// Retrieves the ApplicationWorkflow collection for pre-meeting workflows
        /// </summary>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>A collection of application workflow</returns>
        IEnumerable<ApplicationWorkflow> GetPreMeetingWorkflows(int panelUserAssignmentId, int panelApplicationId);
        /// <summary>
        /// Detects if a reviewer has a step assignment associated with an application
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier</param>
        /// <param name="applicationId">The application identifier</param>
        /// <returns>True if the reviewer has a workflow associated with the application. Otherwise return false.</returns>
        bool ReviewerHasWorkflow(int panelUserAssignmentId, int panelApplicationId);
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <param name="userId">User identifier</param>
        void Delete(int id, int userId);
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <param name="userId">User identifier</param>
        void Delete(ApplicationWorkflow entity, int userId);
    }
}
