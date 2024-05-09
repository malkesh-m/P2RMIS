using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.Bll.SummaryStatements;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Defines the services provided by the Workflow service.
    /// </summary>
    public interface IWorkflowService
    {
        #region Template Services
        /// <summary>
        /// Adds a template to the database.
        /// </summary>
        /// <param name="model">Template view model</param>
        /// <param name="userId">User identifier</param>
        /// <returns>TBD ?</returns>
        /// <exception cref=" cref=""> </exception>
        bool Add(IWorkflowTemplateModel model, int userId);
        /// Retrieves the template identified by the specific id value.
        /// </summary>
        /// <param name="templateId">Template identifier</param>
        /// <returns>Work flow template model</returns>
        WorkflowTemplateModel GetTemplateModel(int templateId);
        /// <summary>
        /// Update the workflow.
        /// </summary>
        /// <param name="model">WorkflowTemplateModel</param>
        /// <returns>True if the action was successful; false otherwise</returns>
        bool Update(WorkflowTemplateModel model, int userId);
        /// <summary>
        /// Create a copy of the workflow.
        /// </summary>
        /// <param name="workflowId">Id of workflow to copy</param>
        /// <param name="userId">User id of person copying the workflow</param>
        /// <returns>Newly created copy of template</returns>
        bool Copy(int workflowId, int userId);
        #endregion
        #region Instance Services
        /// <summary>
        /// Retrieves the instance identified by the specific id value.
        /// </summary>
        /// <param name="instanceId">Template instance identifier</param>
        /// <returns>Instance object identified by id</returns>
        object GetInstance(int instanceId);
        /// <summary>
        /// Populates an existing Instance object and updates it with the user entered values.
        /// </summary>
        /// <param name="templateModel">Template web model from entity framework</param>
        /// <param name="instanceId">Template instance identifier</param>
        /// <param name="userId">User identifier</param>
        /// <returns>TBD ?</returns>
        bool PopulateInstance(IWorkflowTemplateModel templateModel, int instanceId, int userId);
        /// <summary>
        /// Determines the action to perform on the current workflow step.
        /// </summary>
        /// <param name="applicationWorkflowId">ApplicationWorkflowId identifier</param>
        /// <param name="buttonAction">Action to perform</param>
        /// <param name="userId">User id of user performing the buttonAction</param>
        /// <param name="parameterList">List of activity specific parameters.  Keys & values are specific to each activity.</param>
        void Execute(int applicationWorkflowId, P2rmisActions buttonAction, int userId, IDictionary parameterList);
        /// <summary>
        /// Determines the action to perform on the current workflow step.
        /// </summary>
        /// <param name="applicationWorkflowId">ApplicationWorkflowId identifier</param>
        /// <param name="buttonAction">Action to perform</param>
        /// <param name="userId">User id of user performing the buttonAction</param>
        /// <param name="inParameterList">List of activity specific parameters.  Keys & values are specific to each activity.</param>
        /// <param name="outResultsList">List of activity specific results.  Keys & values are specific to each activity</param>
        void Execute(int applicationWorkflowId, P2rmisActions buttonAction, int userId, IDictionary inParameterList, IDictionary outResultsList);
        /// <summary>
        /// Retrieves information about workflow progress for a single application.
        /// </summary>
        /// <param name="applicationWorkflowId">Identifier for an application workflow</param>
        /// <returns>Zero or more workflow steps progress for an application's workflow</returns>
        Container<ApplicationWorkflowStepModel> GetSingleWorkflowProgress(int applicationWorkflowId);
        /// <summary>
        /// retrieves the active workflow step for an application
        /// </summary>
        /// <param name="applicationWorkflowId">the applications workflow id</param>
        /// <returns>integer of the active workflow step</returns>
        int GetActiveApplicationWorkflowStep(int applicationWorkflowId);
        #endregion
        #region Execute Workflow Activity Services 
        /// <summary>
        /// Execute the workflow with the appropriate activity.  This is merely a wrapper
        /// to construct any activity specific parameters and indicate the activity.
        /// 
        /// Parameters are not validated here.  Parameters validation is performed Execute().
        /// </summary>
        /// <param name="applicationWorkflowId">ApplicationWorkflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <exception cref="ArgumentException">Thrown in Execute if arguments are invalid</exception>
        /// <returns>true if the workflow is already checked out, false otherwise</returns>
        bool ExecuteCheckoutWorkflow(int applicationWorkflowId, int userId);
        /// <summary>
        /// Execute the workflow with the appropriate activity.  This is merely a wrapper
        /// to construct any activity specific parameters and indicate the activity.
        /// 
        /// Parameters are not validated here.  Parameters validation is performed Execute().
        /// </summary>
        /// <param name="applicationWorkflowId">ApplicationWorkflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <exception cref="ArgumentException">Thrown in Execute if arguments are invalid</exception>
        void ExecuteCheckinWorkflow(int applicationWorkflowId, int userId);
        /// <summary>
        /// Execute the workflow with the appropriate activity.  This is merely a wrapper
        /// to construct any activity specific parameters and indicate the activity.
        /// 
        /// The difference between this and the Check-in workflow is the worklog manipulation.  Submit is
        /// derived from Check-in, but the worklog is not manipulated.
        /// 
        /// Parameters are not validated here.  Parameters validation is performed Execute().
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <exception cref="ArgumentException">Thrown in Execute if arguments are invalid</exception>
        void ExecuteSubmitWorkflow(int workflowId, int userId);
        /// <summary>
        /// Execute the workflow with the appropriate activity.  This is merely a wrapper
        /// to construct any activity specific parameters and indicate the activity.
        /// 
        /// Parameters are not validated here.  Parameters validation is performed Execute().
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="contentToSaveId">Content identifier</param>
        /// <param name="contentToSave">Content to save</param>
        /// <param name="stepElementId">ApplicationWorkflowStepElement identifier</param>
        /// <exception cref="ArgumentException">Thrown in Execute if arguments are invalid</exception>
        void ExecuteSaveWorkflow(int workflowId, int userId, int contentToSaveId, string contentToSave, int stepElementId);
        /// <summary>
        /// Assigns a user to an workflow
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <exception cref="ArgumentException">Thrown in Execute if arguments are invalid</exception>
        void ExecuteAssignUser(ICollection<AssignUserToApplication> collection);
        /// <summary>
        /// Assigns the content from the current workflow to the specified workflow step
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="targetWorkflowStepId">Target Workflow Step identifier</param>
        /// <exception cref="ArgumentException">Thrown in Execute if arguments are invalid</exception>
        void ExecuteAssignWorkflow(int workflowId, int userId, int targetWorkflowStepId);
        /// <summary>
        /// Executes the assign workflow with file.
        /// </summary>
        /// <param name="workflowId">The workflow identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="targetWorkflowStepId">The target workflow step identifier.</param>
        /// <param name="backupData">The backup stream.</param>
        void ExecuteAssignWorkflowWithFile(int workflowId, int userId, int targetWorkflowStepId, byte[] backupData);
        /// <summary>
        /// Assigns the content from the current workflow to the specified workflow step
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="targetWorkflowStepId">Target Workflow Step identifier</param>
        /// <exception cref="ArgumentException">Thrown in ExecuteOnly if arguments are invalid</exception>
        void ExecuteAssignWorkflow(ICollection<AssignWorkflowStep> collection, int userId);
        /// <summary>
        /// Sets up and executes a workflow step without performing a save to the database.  Save is the responsibility of the calling
        /// site.
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="buttonAction">Action to perform</param>
        /// <param name="userId">User id of user performing the buttonAction</param>
        /// <param name="parameterList">List of activity specific parameters.  Keys & values are specific to each activity.</param>
        /// <param name="resultsList">List of activity specific results.  Keys & values are specific to each activity</param>
        void ExecuteOnly(int workflowId, P2rmisActions buttonAction, int userId, IDictionary parameterList, IDictionary resultsList);
        /// <summary>
        /// Rests the specified workflow step to edit
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="targetWorkflowStepId">Target Workflow Step identifier</param>
        /// <param name="userId">User identifier</param>
        /// <exception cref="ArgumentException">Thrown in Execute if arguments are invalid</exception>
        void ExecuteResetToEditWorkflow(int workflowId, int targetWorkflowStepId, int userId);
        /// <summary>
        /// Saves changes (priority; workflow and templates) for one or more PanelApplications from the Staged SS tab 
        /// of Summary Management        
        /// </summary>
        /// <param name="collection">Collection of changes to save</param>
        void SaveChanges(ICollection<ChangeToSave> collection);
        /// <summary>
        /// Saves changes (priority; workflow and templates) for one or more PanelApplications from the Progress tab 
        /// of Summary Management
        /// </summary>
        /// <param name="collection">Collection of changes to save</param>
        void ProgressSaveChanges(ICollection<ChangeToSave> collection);
        #endregion
    }
}
