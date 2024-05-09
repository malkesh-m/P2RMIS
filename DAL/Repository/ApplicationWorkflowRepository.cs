using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.Reports;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for ApplicationWorkflow objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>  
    public class ApplicationWorkflowRepository: GenericRepository<ApplicationWorkflow>, IApplicationWorkflowRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationWorkflowRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
        #region Services provided
        /// <summary>
        /// Locates & returns the next active workflow step for the identified workflow after
        /// the current step.
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="currentStepOrder">Current ApplicationWorkflowStep order</param>
        /// <returns>First active workflow step after the current step</returns>
        public ApplicationWorkflowStep GetNextStep(int workflowId, int currentStepOrder)
        {
            ApplicationWorkflowStep result = null;
            ApplicationWorkflow workflow = this.GetByID(workflowId);

            if (workflow != null)
            {
                result = workflow.ApplicationWorkflowSteps.FirstOrDefault(x => ((x.StepOrder > currentStepOrder) && (x.Active)));
            }
            return result;
        }
        /// <summary>
        /// Retrieves information about workflow progress for a single application workflow.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application workflow</param>
        /// <returns>Zero or more workflow steps for a given application workflow</returns>
        public ResultModel<ApplicationWorkflowStepModel> GetSingleWorkflowProgress(int applicationWorkflowId)
        {
            ResultModel<ApplicationWorkflowStepModel> result = new ResultModel<ApplicationWorkflowStepModel>();
            result.ModelList = RepositoryHelpers.GetSingleWorkflowProgress(context, applicationWorkflowId);
            return result;
        }
        /// <summary>
        /// retrieves the active workflow step for an application
        /// </summary>
        /// <param name="applicationWorkflowId">the applications workflow id</param>
        /// <returns>integer of the active workflow step</returns>
        public int GetActiveApplicationWorkflowStep(int applicationWorkflowId)
        {
            int activeStep = RepositoryHelpers.GetActiveApplicationWorkflowStep(context, applicationWorkflowId);
            return activeStep;
        }
        /// <summary>
        /// Retrieve a list of report information for the supplied panel application ids.
        /// </summary>
        /// <param name="panelApplicationIds">the panel application ids</param>
        /// <returns>Enumerable list of report information.</returns>
        public ResultModel<IReportAppInfo> GetReportAppInfoList(int[] panelApplicationIds)
        {
            ResultModel<IReportAppInfo> result = new ResultModel<IReportAppInfo>();
            result.ModelList = RepositoryHelpers.GetReportAppInfo(context, panelApplicationIds);
            return result;
        }
        /// <summary>
        /// Retrieves the ApplicationWorkflow collection
        /// </summary>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>A collection of application workflow</returns>
        public IEnumerable<ApplicationWorkflow> GetWorkflows(int panelUserAssignmentId, int panelApplicationId)
        {
            var results = context.ApplicationWorkflows.Where(u => u.PanelUserAssignmentId == panelUserAssignmentId)
                .Where(v => v.ApplicationStage.PanelApplicationId == panelApplicationId);
            return results;
        }
        /// <summary>
        /// Retrieves the ApplicationWorkflow collection for pre-meeting workflows only
        /// </summary>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="panelApplicationId">Panel application identifier</param>
        /// <returns>A collection of application workflow</returns>
        public IEnumerable<ApplicationWorkflow> GetPreMeetingWorkflows(int panelUserAssignmentId, int panelApplicationId)
        {
            var results = context.ApplicationWorkflows.Where(u => u.PanelUserAssignmentId == panelUserAssignmentId)
                .Where(v => v.ApplicationStage.PanelApplicationId == panelApplicationId && v.ApplicationStage.ReviewStageId == ReviewStage.Asynchronous);
            return results;
        }
        /// <summary>
        /// Detects if a reviewer has a step assignment associated with an application
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier</param>
        /// <param name="applicationId">The application identifier</param>
        /// <returns>True if the reviewer has a workflow associated with the application. Otherwise return false.</returns>
        public bool ReviewerHasWorkflow(int panelUserAssignmentId, int panelApplicationId)
        {
            var workflows = GetWorkflows(panelUserAssignmentId, panelApplicationId);
            if (workflows != null && workflows.Count() > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <param name="userId">User identifier</param>
        public void Delete(int id, int userId)
        {
            var entity = GetByID(id);
            Helper.UpdateDeletedFields(entity, userId);
            Delete(id);
        }
        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <param name="userId">User identifier</param>
        public void Delete(ApplicationWorkflow entity, int userId)
        {
            Helper.UpdateDeletedFields(entity, userId);
            Delete(entity);
        }
        #endregion
        #region Services Not Provided
        #endregion 
    }
}
