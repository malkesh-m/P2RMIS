using System;
using System.IO;
using System.Activities;
using System.Collections;
using System.Collections.Generic;
using Sra.P2rmis.Bll.Builders;
using Sra.P2rmis.Bll.EntityBuilders;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.Bll.SummaryStatements;
using Sra.P2rmis.CrossCuttingServices.FileServices;
using EntityWorkflow = Sra.P2rmis.Dal.Workflow;

namespace Sra.P2rmis.Bll.Workflow
{
    /// <summary>
    /// Provides functionality to manipulate work flow templates & create instance of work flow templates.
    /// </summary>
    public partial class WorkflowService: ServerBase, IWorkflowService
    {
        #region Attributes
        /// <summary>
        /// Error message when Execute receives invalid parameters
        /// </summary>
        private const string ExecuteErrorMessage = "WorkflowService.Execute received invalid parameters: workflowId [{0}]; userId [{1}]";
        private const string ExecuteOnlyErrorMessage = "WorkflowService.ExecuteOnly received invalid parameters: workflowId [{0}]; userId [{1}]";
        private const string ExecuteExecuteAssignWorkflowMessage = "WorkflowService.ExecuteAssignWorkflow received invalid parameters: collection is null";
        #endregion
        #region Properties
        /// <summary>
        /// Defines a null object for activities without specific parameters.
        /// </summary>
        private readonly IDictionary nullActivitySpecificParameters = null;
        /// <summary>
        /// Defines a null object for activities that do not have specific results.
        /// </summary>
        private readonly IDictionary nullResults = null;
        #endregion        
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public WorkflowService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion
        #region Template Services
        /// <summary>
        /// Adds a template to the database.
        /// </summary>
        /// <param name="model">Template view model</param>
        /// <param name="userId">User identifier</param>
        /// <returns>TBD ?</returns>
        /// <exception cref=" cref=""> </exception>
        public bool Add(IWorkflowTemplateModel model, int userId)
        {
            bool result = false;
            ///
            /// Get a builder and build
            /// 
            if (IsAddParametersValid(model, userId))
            {
                TemplateBuilder builder = new TemplateBuilder(model, userId);
                EntityWorkflow entityObject = builder.Build();
                //
                // We have built the entity but now need to make sure it is a valid entity object.
                //
                if (builder.Validate(entityObject).Count == 0)
                {
                    //
                    // Now we can add the entity to the repository and save.
                    //
                    UnitOfWork.WorkflowTemplateRepository.Add(entityObject);
                    UnitOfWork.Save();
                    result = true;
                }
                else
                {
                    //
                    // some how need to get the error strings back
                    //
                }
                foreach (string s in builder.Validate(entityObject)) Console.WriteLine(s);
            }
            return result;
        }
        /// <summary>
        /// Retrieves the template identified by the specific id value.
        /// </summary>
        /// <param name="templateId">Template identifier</param>
        /// <returns>Work flow template model</returns>
        public WorkflowTemplateModel GetTemplateModel(int templateId)
        {
            WorkflowTemplateModel result = null;

            if (IsGetTemplateParameterValid(templateId))
            {
                var entity = this.UnitOfWork.WorkflowTemplateRepository.GetByID(templateId);
                TemplateModelBuilder builder = new TemplateModelBuilder(entity);
                result = builder.Build();
            }
            return result;
        }
        /// <summary>
        /// Update the workflow.
        /// </summary>
        /// <param name="model">WorkflowTemplateModel</param>
        /// <returns>True if the action was successful; false otherwise</returns>
        public bool Update(WorkflowTemplateModel model, int userId)
        {
            bool result = false;
            // 
            // Find the entity object that matches the model.  If one is located then update the contents.
            //
            var entity = this.UnitOfWork.WorkflowTemplateRepository.GetByID(model.WorkflowId);
            if (entity != null)
            {
                //
                // Create a builder and update the template.  The template takes care of updating
                // the individual steps.
                //
                TemplateBuilder builder = new TemplateBuilder(model, userId);
                entity = builder.Update(entity);
                //
                //  Update the datetime stamp & user id of the entity or workflow steps that changed.
                //
                if (builder.IsDirty)
                {
                    //
                    // Update the repository and finally save the workflow
                    //
                    this.UnitOfWork.WorkflowTemplateRepository.Update(entity);
                    this.UnitOfWork.Save();
                }

                //
                // Send back a success indicator.
                //
                result = true;
            }
            return result;
        }
        /// <summary>
        /// Determines the action to perform on the current workflow step.
        /// </summary>
        /// <param name="applicationWorkflowId">ApplicationWorkflow identifier</param>
        /// <param name="buttonAction">Action to perform</param>
        /// <param name="userId">User id of user performing the buttonAction</param>
        /// <param name="parameterList">List of activity specific parameters.  Keys & values are specific to each activity.</param>
        public virtual void Execute(int applicationWorkflowId, P2rmisActions buttonAction, int userId, IDictionary parameterList)
        {
            this.Execute(applicationWorkflowId, buttonAction, userId, parameterList, nullResults);
        }
        /// <summary>
        /// Determines the action to perform on the current workflow step.
        /// </summary>
        /// <param name="applicationWorkflowId">ApplicationWorkflow identifier</param>
        /// <param name="buttonAction">Action to perform</param>
        /// <param name="userId">User id of user performing the buttonAction</param>
        /// <param name="parameterList">List of activity specific parameters.  Keys & values are specific to each activity.</param>
        /// <param name="resultsList">List of activity specific results.  Keys & values are specific to each activity</param>
        public virtual void Execute(int applicationWorkflowId, P2rmisActions buttonAction, int userId, IDictionary parameterList, IDictionary resultsList)
        {
            if (IsExecuteParametersValid(applicationWorkflowId, userId))
            {
                //
                // Create the Activity that we should execute based on what the current workflow
                // step and what button was pushed.
                //
                P2rmisActivity theActivity = CreateActivity(buttonAction);
                //
                // Next Locate the workflow 
                //
                ApplicationWorkflow theWorkflow = UnitOfWork.ApplicationWorkflowRepository.GetByID(applicationWorkflowId);
                ApplicationWorkflowStep currentStep = theActivity.GetCurrentWorkflowStep(theWorkflow);

                //
                // Set the parameters
                //
                theActivity.UnitOfWork = new InArgument<Dal.IUnitOfWork>(ctx => this.UnitOfWork);
                theActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => currentStep);
                theActivity.UserId = userId;
                //
                // Set any activity specific parameters
                //
                theActivity.SetParameters(parameterList);

                var workflowResults = WorkflowInvoker.Invoke(theActivity);

                WorkflowState outState = (WorkflowState)workflowResults[P2rmisActivity.OutArgumentNames.OutState.ToString()];
                //
                // Deal with the change of state for this step
                //
                SetStepState(outState, currentStep, userId);
                //
                //  Now deal with the workflow itself
                //
                CompleteWorkflow(theWorkflow, userId);
                //
                // Now save any updated framework object
                //
                UnitOfWork.Save();
                //
                // And finally get any returned results
                //
                theActivity.GetResults(workflowResults, resultsList);
            }
            else
            {
                string message = string.Format(ExecuteErrorMessage, applicationWorkflowId, userId);
                throw new ArgumentException(message);
            }
        }
        /// <summary>
        /// Sets up and executes a workflow step without performing a save to the database.  Save is the responsibility of the calling
        /// site.
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="buttonAction">Action to perform</param>
        /// <param name="userId">User id of user performing the buttonAction</param>
        /// <param name="parameterList">List of activity specific parameters.  Keys & values are specific to each activity.</param>
        /// <param name="resultsList">List of activity specific results.  Keys & values are specific to each activity</param>
        public virtual void ExecuteOnly(int workflowId, P2rmisActions buttonAction, int userId, IDictionary parameterList, IDictionary resultsList)
        {
            if (IsExecuteParametersValid(workflowId, userId))
            {
                //
                // Locate the workflow 
                //
                ApplicationWorkflow theWorkflow = UnitOfWork.ApplicationWorkflowRepository.GetByID(workflowId);
                ApplicationWorkflowStep currentStep = theWorkflow.CurrentStep();
                //
                // Next create the Activity that we should execute based on what the current workflow
                // step and what button was pushed.
                //
                P2rmisActivity theActivity = CreateActivity(buttonAction);
                //
                // Set the parameters
                //
                theActivity.UnitOfWork = new InArgument<Dal.IUnitOfWork>(ctx => this.UnitOfWork);
                theActivity.WorkflowStep = new InArgument<ApplicationWorkflowStep>(ctx => currentStep);
                theActivity.UserId = userId;
                //
                // Set any activity specific parameters
                //
                theActivity.SetParameters(parameterList);

                var workflowResults = WorkflowInvoker.Invoke(theActivity);

                WorkflowState outState = (WorkflowState)workflowResults[P2rmisActivity.OutArgumentNames.OutState.ToString()];
                //
                // Deal with the change of state for this step
                //
                SetStepState(outState, currentStep, userId);
                //
                //  Now deal with the workflow itself
                //
                CompleteWorkflow(theWorkflow, userId);
                //
                // And finally get any returned results
                //
                theActivity.GetResults(workflowResults, resultsList);
            }
            else
            {
                string message = string.Format(ExecuteOnlyErrorMessage, workflowId, userId);
                throw new ArgumentException(message);
            }
        }
        /// <summary>
        /// Sets the state of the workflow step depending upon the outcome (outState)
        /// of the workflow's step activity.
        /// </summary>
        /// <param name="outState">New state of the workflow step</param>
        /// <param name="currentStep">Current step in the workflow</param>
        /// <param name="userId">User id of person copying the workflow</param>
        private void SetStepState(WorkflowState outState, ApplicationWorkflowStep currentStep, int userId)
        {
            if ((outState != WorkflowState.Default) && (outState != WorkflowState.Fail))
            {
                currentStep.SetResolution(outState == WorkflowState.Complete, userId);
                this.UnitOfWork.ApplicationWorkflowStepRepository.Update(currentStep);
            }
        }
        /// <summary>
        /// Determines if the workflow has completed all steps.  If the workflow has completed all
        /// steps the workflow is marked as "resolved".
        /// </summary>
        /// <param name="Workflow">ApplicationWorkflow object</param>
        /// <param name="userId">User id of person copying the workflow</param>
        private void CompleteWorkflow(ApplicationWorkflow workflow, int userId)
        {
            //
            // Ask the workflow if all of it's steps are complete.  If they are
            // mark the workflow as resolved.
            //
            if (workflow.IsComplete())
            {
                workflow.Complete(userId);
                this.UnitOfWork.ApplicationWorkflowRepository.Update(workflow);
            }
        }
        /// <summary>
        /// Create a copy of the workflow.
        /// </summary>
        /// <param name="workflowId">Id of workflow to copy</param>
        /// <param name="userId">User id of person copying the workflow</param>
        /// <returns>Newly created copy of template; Empty workflow returned if error occurred</returns>
        public bool Copy(int workflowId, int userId)
        {
            bool result = false;

            if (IsCopyValidParameters(workflowId, userId))
            {
                //
                // Locate the workflow to be copied & tell it to make a copy of itself.  This
                // will copy the workflow steps also.
                //
                EntityWorkflow templateToCopy = this.UnitOfWork.WorkflowTemplateRepository.GetByID(workflowId);
                if (templateToCopy != null)
                {
                    EntityWorkflow templateCopy = templateToCopy.Copy(userId);
                    UnitOfWork.WorkflowTemplateRepository.Add(templateCopy);
                    //
                    // Now save the new copy
                    //
                    this.UnitOfWork.Save();
                    //
                    // And we are done.
                    //
                    result = true;
                }
            }
            return result;
        }
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
        public bool ExecuteCheckoutWorkflow(int applicationWorkflowId, int userId)
        {
            //
            // The Checkout activity has not activity specific parameters
            // Now execute the workflow.
            //
            IDictionary results = new Hashtable(CheckoutActivity.ResultCount);
            this.Execute(applicationWorkflowId, P2rmisActions.Checkout, userId, nullActivitySpecificParameters, results);
            //
            //  Need to actually check if it was there.  Otherwise unit test will error.
            //  Checking into stubs & doing something
            //
            return (results.Contains(CheckoutActivity.OutArgumentNames.WasCheckedOut)) ? (bool)results[CheckoutActivity.OutArgumentNames.WasCheckedOut] : true;
        }
        /// <summary>
        /// Execute the workflow with the appropriate activity.  This is merely a wrapper
        /// to construct any activity specific parameters and indicate the activity.
        /// 
        /// Parameters are not validated here.  Parameters validation is performed Execute().
        /// </summary>
        /// <param name="applicationWorkflowId">ApplicationWorkflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <exception cref="ArgumentException">Thrown in Execute if arguments are invalid</exception>
        public void ExecuteCheckinWorkflow(int applicationWorkflowId, int userId)
        {
            //
            // The Checkout activity has not activity specific parameters
            // Now execute the workflow.
            //
            this.Execute(applicationWorkflowId, P2rmisActions.Checkin, userId, nullActivitySpecificParameters);
        }
        /// <summary>
        /// Execute the workflow with the appropriate activity.  This is merely a wrapper
        /// to construct any activity specific parameters and indicate the activity.
        /// 
        /// Parameters are not validated here.  Parameters validation is performed Execute().
        /// </summary>
        /// <param name="workflowId">Workflow entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <exception cref="ArgumentException">Thrown in Execute if arguments are invalid</exception>
        public void ExecuteDeactivateClientReviewCheckinActivity(int workflowId, int userId)
        {
            //
            // The Checkout activity has not activity specific parameters
            // Now execute the workflow.
            //
            this.Execute(workflowId, P2rmisActions.DeactivateClientReviewCheckinActivity, userId, nullActivitySpecificParameters);
        }
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
        public void ExecuteSubmitWorkflow(int workflowId, int userId)
        {
            //
            // The Checkout activity has not activity specific parameters
            // Now execute the workflow.
            //
            this.Execute(workflowId, P2rmisActions.Submit, userId, nullActivitySpecificParameters);
        }
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
        public void ExecuteSaveWorkflow(int workflowId, int userId, int contentToSaveId, string contentToSave, int stepElementId)
        {
            //
            // The Save activity has two specific parameters; the content identifier & content to save.
            //
            IDictionary activitySpecificParameters = new Hashtable(SaveActivity.ActivityArgumentCount);
            activitySpecificParameters[SaveActivity.SaveParameters.ContentId] = contentToSaveId;
            activitySpecificParameters[SaveActivity.SaveParameters.Content] = contentToSave;
            activitySpecificParameters[SaveActivity.SaveParameters.ElementId] = stepElementId;
            //
            // Now execute the workflow.
            //
            this.Execute(workflowId, P2rmisActions.Save, userId, activitySpecificParameters);
        }
        /// <summary>
        /// Assigns a user to an workflow
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <exception cref="ArgumentException">Thrown in Execute if arguments are invalid</exception>
        public void ExecuteAssignUser(ICollection<AssignUserToApplication> collection)
        {

            // Now execute the workflow.
            //
            foreach (var assignment in collection)
            {
                //
                // The Assign User activity has one specific parameters; the assignee's user id,
                //
                IDictionary activitySpecificParameters = new Hashtable(AssignUserActivity.ActivityArgumentCount);
                activitySpecificParameters[AssignUserActivity.AssignUserParameters.AssigneeId] = assignment.AssigneeId;
                //
                // Now execute the workflow
                //
                this.Execute(assignment.WorkflowId, P2rmisActions.AssignUser, assignment.UserId, activitySpecificParameters);
            }
        }
        /// <summary>
        /// Assigns the content from the current workflow to the specified workflow step
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="targetWorkflowStepId">Target Workflow Step identifier</param>
        /// <exception cref="ArgumentException">Thrown in Execute if arguments are invalid</exception>
        public void ExecuteAssignWorkflow(int workflowId, int userId, int targetWorkflowStepId)
        {
            //
            // The Save activity has two specific parameters; the content identifier & content to save.
            //
            IDictionary activitySpecificParameters = new Hashtable(1);
            activitySpecificParameters[AssignWorkflowStepActivity.SaveParameters.TargetStepId] = targetWorkflowStepId;
            //
            // Now execute the workflow.
            //
            this.Execute(workflowId, P2rmisActions.AssignWorkflowStep, userId, activitySpecificParameters);
        }
        /// <summary>
        /// Executes the assign workflow with file.
        /// </summary>
        /// <param name="workflowId">The workflow identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="targetWorkflowStepId">The target workflow step identifier.</param>
        /// <param name="backupData">The backup file.</param>
        public void ExecuteAssignWorkflowWithFile(int workflowId, int userId, int targetWorkflowStepId, byte[] backupData)
        {
            //
            // The Save activity has two specific parameters; the content identifier & content to save.
            //
            IDictionary activitySpecificParameters = new Hashtable(1);
            activitySpecificParameters[AssignWorkflowStepActivity.SaveParameters.TargetStepId] = targetWorkflowStepId;
            activitySpecificParameters[AssignWorkflowStepActivity.SaveParameters.BackupFile] = backupData;
            //
            // Now execute the workflow.
            //
            this.Execute(workflowId, P2rmisActions.AssignWorkflowStep, userId, activitySpecificParameters);
        }
        /// <summary>
        /// Assigns the content from the current workflow to the specified workflow step
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="targetWorkflowStepId">Target Workflow Step identifier</param>
        /// <exception cref="ArgumentException">Thrown in ExecuteOnly if arguments are invalid</exception>
        public void ExecuteAssignWorkflow(ICollection<AssignWorkflowStep> collection, int userId)
        {
            ValidateExecuteAssignWorkflowParameters(collection);
            foreach (var item in collection)
            {
                //
                // The assign activity has a specific parameters the target workflow step identifier.
                // While it may seem inefficient to re-allocate the dictionary each time through the loop
                // this is necessary to support the Rhino mocks test.  See the comment in the multiple 
                // test method or the rational.
                //
                IDictionary activitySpecificParameters = new Hashtable(AssignWorkflowStepActivity.AssignArgumentCount);
                activitySpecificParameters[AssignWorkflowStepActivity.SaveParameters.TargetStepId] = item.TargetWorkflowStepId;
                ExecuteOnly(item.WorkflowId, P2rmisActions.AssignWorkflowStep, userId, activitySpecificParameters, nullResults);
            }
            //
            // Now save any updated or new framework object
            //
            UnitOfWork.Save();
        }
        /// <summary>
        /// Rests the specified workflow step to edit
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="targetWorkflowStepId">Target Workflow Step identifier</param>
        /// <param name="userId">User identifier</param>
        /// <exception cref="ArgumentException">Thrown in Execute if arguments are invalid</exception>
        public void ExecuteResetToEditWorkflow(int workflowId, int targetWorkflowStepId, int userId)
        {
            //
            // Locate the last workflowstep 
            //
            ApplicationWorkflow theWorkflow = UnitOfWork.ApplicationWorkflowRepository.GetByID(workflowId);
            ApplicationWorkflowStep lastStep = theWorkflow.LastStep();

            //
            // The Reset to edit activity has two specific parameters; the content identifier & content to save.
            //
            IDictionary activitySpecificParameters = new Hashtable(2);
            activitySpecificParameters[ResetToEditWorkflowStepActivity.SaveParametersResetToEditWorkflowActivity.TargetStepId] = targetWorkflowStepId;
            if (lastStep != null)
                activitySpecificParameters[ResetToEditWorkflowStepActivity.SaveParametersResetToEditWorkflowActivity.LastStepId] = lastStep.ApplicationWorkflowStepId;
            //
            // Now execute the workflow.
            //
            this.Execute(workflowId, P2rmisActions.ResetToEditApplicationWorkflow, userId, activitySpecificParameters);
        }

        #endregion
        #region Helpers
        /// <summary>
        /// Validates the parameters for method are valid.  Validations are
        ///   - template model is not null
        /// </summary>
        /// <param name="templateModel">Template web model from entity framework</param>
        /// <param name="userId">User identifier</param>
        /// <returns>True if parameters are valid; false otherwise</returns>
        private bool IsAddParametersValid(IWorkflowTemplateModel templateModel, int userId)
        {
            return ((templateModel != null) &&
                    (userId > 0)
                   );
        }
        /// <summary>
        /// Validates the parameters for method are valid.  Validations are
        ///   - id points to existing template
        /// </summary>
        /// <param name="templateId">Template identifier</param>
        /// <returns>True if the identified template exists; false otherwise</returns>
        private bool IsGetTemplateParameterValid(int templateId)
        {
            return (this.UnitOfWork.WorkflowTemplateRepository.IsTemplateValid(templateId));

        }
        /// <summary>
        /// Validates the parameters for Execute are valid.  Validations are
        ///  - applicationId can be converted to an integer
        ///  - userId is greater than 0
        /// </summary>
        /// <param name="applicationId">ApplicationWorkflowId (format is 12345)</param>
        /// <param name="userId">User identifier</param>
        /// <returns>True if parameters are valid; false otherwise</returns>
        private bool IsExecuteParametersValid(int applicationId, int userId)
        {
            return (
                   (applicationId > 0) &&
                   (userId > 0)
                   );
        }
        /// <summary>
        /// Calls the ActivityFactory and returns the activity for the action.  This
        /// wrapper is primarily to support unit testing and mocking the activity.
        /// </summary>
        /// <param name="buttonAction">Requested activity</param>
        /// <returns><Requested Activity/returns>
        public virtual P2rmisActivity CreateActivity(P2rmisActions buttonAction)
        {
            return ActivityFactory.CreateActivity(buttonAction);
        }
        /// <summary>
        /// Validates the parameters for Copy are valid.  Validations are
        ///   - workflowId is greater than 0
        ///   - userId is greater than 0
        /// </summary>
        /// <param name="workflowId">Workflow identifier</param>
        /// <param name="userId">User identifier</param>
        /// <returns>True if parameters are valid; false otherwise</returns>
        private bool IsCopyValidParameters(int workflowId, int userId)
        {
            return (
                (workflowId > 0) &&
                (userId > 0)
                );
        }
        /// <summary>
        /// Validates the parameters for ExecuteAssignWorkflow are valid.  Validations are:
        ///   - collection cannot equal null
        /// Specif values of the contained objects are validated in the corresponding activity.
        /// </summary>
        /// <param name="collection">Collection of individual parameters</param>
        /// <exception cref="ArgumentException">Thrown if collection parameter is null</exception>
        private void ValidateExecuteAssignWorkflowParameters(ICollection<AssignWorkflowStep> collection)
        {
            if (collection == null)
            {
                throw new ArgumentException(ExecuteExecuteAssignWorkflowMessage);
            }
        }

        #endregion
        #region Client Review Workflow step manipulation
        /// <summary>
        /// Saves changes (priority; workflow and templates) for one or more PanelApplications from the Staged SS tab 
        /// of Summary Management        
        /// </summary>
        /// <param name="collection">Collection of changes to save</param>
        public void SaveChanges(ICollection<ChangeToSave> collection)
        {
            //
            // We have a bunch of changes so iterate over the collection and 
            // make the changes for each entry.
            //
            foreach (var item in collection)
            {
                Application application = UnitOfWork.ApplicationRepository.FindApplicationByPanelApplicationId(item.PanelApplicationId);
                ManipulateTheWorkflowForPriority1(application, item);
                SaveWorkflowChanges(application, item.WorkflowId, item.UserId);
            }
            UnitOfWork.Save();
        }
        /// <summary>
        /// Saves changes (priority; workflow and templates) for one or more PanelApplications from the Progress tab 
        /// of Summary Management
        /// </summary>
        /// <param name="collection">Collection of changes to save</param>
        public void ProgressSaveChanges(ICollection<ChangeToSave> collection)
        {
            //
            // We have a bunch of changes so iterate over the collection and 
            // make the changes for each entry.
            //
            foreach (var item in collection)
            {
                Application application = UnitOfWork.ApplicationRepository.FindApplicationByPanelApplicationId(item.PanelApplicationId);
                ManipulateTheWorkflowForPriority1(application, item);
            }
            UnitOfWork.Save();
        }

        /// <summary>
        /// Processes a single change for priority 1 change.
        /// </summary>
        /// <param name="item">Single change to save</param>
        internal virtual void ManipulateTheWorkflowForPriority1(Application application, ChangeToSave item)
        {
            int a = application.PanelApplicationId().Value;
            PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(a);
            ApplicationWorkflow applicationWorkflowEntity = panelApplicationEntity.GetSummaryWorkflow();
            bool item1StateChange = panelApplicationEntity.HasReviewStatusChanged(ReviewStatu.PriorityOne, item.Priority1);
            //
            // Now that we have all of the data update the priorities, manipulate the workflow & make
            // any changes to the ApplicationDefaultWorkflow
            //
            UpdatePriorities(item);
            ManipulateTheWorkflow(panelApplicationEntity, applicationWorkflowEntity, item, item1StateChange);
        }
        /// <summary>
        /// Manipulates the SummaryStatement workflow assigned to the application for Priority 1 (client review)
        /// functionality.
        /// </summary>
        /// <param name="panelApplicationEntity">PanelApplication entity</param>
        /// <param name="applicationWorkflowEntity">ApplicationWorkflow entity</param>
        /// <param name="item">ChangeToSave object describing the changes</param>
        /// <param name="hasStatusChanged">Indicates if the Priority 1 status has changed</param>
        internal virtual void ManipulateTheWorkflow(PanelApplication panelApplicationEntity, ApplicationWorkflow applicationWorkflowEntity, ChangeToSave item, bool hasStatusChanged)
        {
            //
            // If summary statement processing has not started or the Priority 1 status has not changed
            // we do not need to do anything.
            //
            if (!panelApplicationEntity.IsSummaryStarted() || (!hasStatusChanged))
            {
            }
            // If an active summary workflow doesn't exist
            // We do not need to do anything
            else if (applicationWorkflowEntity == null)
            {                
            }
            //
            // Since the summary statement workflow has started then we determine 
            // where the workflow is in relationship to the current step. 
            //
            // Note: the 'if' blocks position is significant and assumes that any previous test is false.
            //
            // If it is before the first client review step all we need to do is 
            // activate/deactivate the workflow steps.
            //
            else if (applicationWorkflowEntity.IsWorkflowBeforeFirstClientReview())
            {
                applicationWorkflowEntity.ActivateClientReviewSteps(item.Priority1, item.UserId);
            }
            //
            // If the workflow is at the first client review step then we mark the step as resolved
            // and complete the step
            //
            else if (applicationWorkflowEntity.IsWorkflowAtAClientReviewStep())
            {
                //
                // Activate (or deactivate) the steps & promote the workflow
                //
                //ExecuteCheckinWorkflow(applicationWorkflowEntity.ApplicationWorkflowId, item.UserId);
                ExecuteDeactivateClientReviewCheckinActivity(applicationWorkflowEntity.ApplicationWorkflowId, item.UserId);
                applicationWorkflowEntity.ActivateClientReviewSteps(item.Priority1, item.UserId);
            }
            //
            // If there is a client review workflow step ahead then just activate the steps
            //
            else if (applicationWorkflowEntity.IsClientReviewWorkflowStepAhead())
            {
                applicationWorkflowEntity.ActivateClientReviewSteps(item.Priority1, item.UserId);
            }
            //
            // Just resolve the steps in the past.
            //
            else if (!applicationWorkflowEntity.IsClientReviewWorkflowStepAhead())
            {
                applicationWorkflowEntity.ActivateClientReviewSteps(item.Priority1, item.UserId);
            }
        }

        /// <summary>
        /// Updated the ReviewStatu Priority One & Priority Two values of the application.
        /// </summary>
        /// <param name="item">ChangeToSave object describing the changes</param>
        internal virtual void UpdatePriorities(ChangeToSave item)
        {
            Application application = UnitOfWork.ApplicationRepository.FindApplicationByPanelApplicationId(item.PanelApplicationId);
            int panelApplicationEntityId = application.PanelApplicationId().Value;

            SetPriority(application, panelApplicationEntityId, ReviewStatu.PriorityOne, item.Priority1, item.UserId);
            SetPriority(application, panelApplicationEntityId, ReviewStatu.PriorityTwo, item.Priority2, item.UserId);
        }
        /// <summary>
        /// Set or remove a priority of the application.
        /// </summary>
        /// <param name="application">Application entity</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="statusToChange">Identifies the priority</param>
        /// <param name="state">Change state</param>
        /// <param name="userId">User requesting the change</param>
        internal virtual void SetPriority(Application application, int panelApplicationId, int statusToChange, bool state, int userId)
        {
            //
            // clear
            //
            PanelApplication a = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            //
            // If the Status indicator (Priority One or Priority Two) is not set and the status indicator is there, it needs to be deleted
            //
            ApplicationReviewStatu b = a.HasReviewStatus(statusToChange);
            if ((!state) & (b != null))
            {
                //
                // There is no review status for the specified value but there is one in the database.
                // So delete it.
                // 
                var editAction = new PriorityChangeServiceAction();
                editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationReviewStatuGenericRepository, ServiceAction<ApplicationReviewStatu>.DoNotUpdate, b.ApplicationReviewStatusId, userId);
                editAction.Execute();
            }
            else if ((state) & (b == null))
            {
                //
                // There is no ReviewStatu entity for this state, but we need one.  So
                // create an editor and add it.
                //
                var editAction = new PriorityChangeServiceAction();
                editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationReviewStatuGenericRepository, ServiceAction<ApplicationReviewStatu>.DoNotUpdate, userId);
                editAction.Populate(a.ApplicationId, statusToChange, panelApplicationId);
                editAction.Execute();
            }
        }
        /// <summary>
        /// Save the change to an application's default workflow.
        /// </summary>
        /// <param name="application">Application entity</param>
        /// <param name="newWorkflowId">Workflow identifier</param>
        /// <param name="userId">User requesting the change</param>
        internal virtual void SaveWorkflowChanges(Application application, int newWorkflowId, int userId)
        {
            ApplicationDefaultWorkflow workflow = application.FindDefaultWorkflow();

            if (workflow != null)
            {
                workflow.Update(newWorkflowId, userId);
                UnitOfWork.ApplicationDefaultWorkflowRepository.Update(workflow);
            }
            else
            {
                workflow = application.AddDefaultWorkflow(newWorkflowId, userId);
                UnitOfWork.ApplicationDefaultWorkflowRepository.Add(workflow);
            }
        }
        #endregion
    }
}
