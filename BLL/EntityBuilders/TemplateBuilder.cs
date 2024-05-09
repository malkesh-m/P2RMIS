using Sra.P2rmis.CrossCuttingServices;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;
using EntityWorkflow = Sra.P2rmis.Dal.Workflow;

namespace Sra.P2rmis.Bll.EntityBuilders
{
    /// <summary>
    /// Builds/updates an entity Template object from the view model.
    /// </summary>
    public class TemplateBuilder : EntityBuilder<IWorkflowTemplateModel, EntityWorkflow>
    {
        #region Internal Classes
        internal class Messages
        {
            public const string STEP_COUNT = "Invalid number of steps defined.  Must create at least two steps.";
            public const string FIRST_STEP_NOT_ACTIVE = "The first step must be active.";
            public const string LAST_STEP_NOT_ACTIVE = "The last step must be active.";
            public const string EMPTY_TEMPLATE_NAME = "Valid template name not supplied.";
            public const string EMPTY_DESCRIPTION = "Valid template description not supplied.";
            public const string EMPTY_STEP_NAME = "Valid step name not supplied for step {0}.";
            public const string DUPLICATE_STEPS_DEFINED = "Duplicate step ordering detected for step {0}.";
        }
        #endregion
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">Workflow view model</param>
        /// <param name="userId">User id</param>
        public TemplateBuilder(IWorkflowTemplateModel model, int userId)
            : base(model, userId)
        {
        }
        #endregion

        #region BuilderServices
        /// <summary>
        /// Specifics for constructing an entity workflow from an WorkflowTemplateModel.
        /// </summary>
        /// <returns>Entity workflow object</returns>
        public override EntityWorkflow Build()
        {
            EntityWorkflow entityObject = new EntityWorkflow();
            //
            // Copy over the entity information
            //
            /// NOTE: THIS HAS NOW BEEN TESTE YET.
            ///
            entityObject.WorkflowId = this._model.WorkflowId;
            entityObject.ClientId = this._model.ClientId;
            entityObject.WorkflowName = this._model.WorkflowName;
            entityObject.WorkflowDescription = this._model.WorkflowDescription;

            this.SetCreateBy(entityObject, this._userId);
            this.SetModifiedBy(entityObject, this._userId);

            //
            // The steps also need built. Do them here
            //
            foreach (WorkflowStepModel step in this._model.Steps)
            {
                TemplateStepBuilder builder = new TemplateStepBuilder(step, this._userId);
                entityObject.WorkflowSteps.Add(builder.Build());
            }
            //
            // We are definitely dirty since we are building it
            //
            IsDirty = true;

            return entityObject;
        }
        /// <summary>
        /// Build method takes the model object & updates an entity object.  All 
        /// instances of the builder must override this.
        /// </summary>
        /// <returns>Updated entity object</returns>
        public override EntityWorkflow Update(EntityWorkflow entityObject)
        {
            //
            // Update the properties if they changed.  Using the EntityBuilder method will change the "dirty" flag
            //
            UpdatePropertyIfChanged(entityObject.WorkflowName, this._model.WorkflowName, value => entityObject.WorkflowName = value);
            UpdatePropertyIfChanged(entityObject.WorkflowDescription, this._model.WorkflowDescription, value => entityObject.WorkflowDescription = value);

            foreach (WorkflowStepModel step in this._model.Steps)
            {
                // find the matching entity object for this step in entity model
                WorkflowStep matchingStep = entityObject.WorkflowSteps.FirstOrDefault(x => x.WorkflowStepId == step.WorkflowStepId);
                if (matchingStep != null)
                {
                    TemplateStepBuilder builder = new TemplateStepBuilder(step, this._userId);
                    builder.Update(matchingStep);
                    IsDirty = IsDirty | builder.IsDirty;
                }
                else
                {
                    //
                    // must have added this one, now what ?????
                    // 
                }
            }
            //
            // If something changed then the timestamps is set
            //
            if (IsDirty)
            {
                SetModifiedBy(entityObject, this._userId);
            }
            return entityObject;
        }
        /// <summary>
        /// Sets the Entity Workflow objects modified by fields.
        /// </summary>
        /// <param name="workflow">Entity workflow object</param>
        /// <param name="userId">User identification</param>
        private EntityWorkflow SetModifiedBy(EntityWorkflow workflow, int userId)
        {
            workflow.ModifiedBy = userId;
            workflow.ModifiedDate = GlobalProperties.P2rmisDateToday;
            return workflow;
        }
        /// <summary>
        /// Sets the Entity Workflow objects created by fields.
        /// </summary>
        /// <param name="workflow">Entity workflow object</param>
        /// <param name="userId">User identification</param>
        private EntityWorkflow SetCreateBy(EntityWorkflow workflow, int userId)
        {
            workflow.CreatedBy = userId;
            workflow.CreatedDate = GlobalProperties.P2rmisDateToday;
            return workflow;
        }
        /// <summary>
        /// Validates the built entity object
        /// </summary>
        /// <param name="entityObject">Template entity object</param>
        /// <returns></returns>
        public override List<string> Validate(EntityWorkflow entityObject)
        {
            if (entityObject != null)
            {
                //
                // Name cannot be white space
                //
                TestStringForWhiteSpace(entityObject.WorkflowName, Messages.EMPTY_TEMPLATE_NAME, Errors);
                ///
                /// Description cannot be white space
                /// 
                TestStringForWhiteSpace(entityObject.WorkflowDescription, Messages.EMPTY_DESCRIPTION, Errors);
                //
                // Must have a start & stop step
                //
                if (entityObject.WorkflowSteps.Count < 2)
                {
                    Errors.Add(Messages.STEP_COUNT);
                }
                //
                // Start & stop steps must be active
                //
                if (entityObject.WorkflowSteps.Count >= 2)
                {
                    if (entityObject.WorkflowSteps.ElementAt(0).ActiveDefault == false)
                    {
                        Errors.Add(Messages.FIRST_STEP_NOT_ACTIVE);
                    }
                    int last = entityObject.WorkflowSteps.Count - 1;
                    if (entityObject.WorkflowSteps.ElementAt(last).ActiveDefault == false)
                    {
                        Errors.Add(Messages.LAST_STEP_NOT_ACTIVE);
                    }
                }
                ///
                /// Validate each step's definition (may want to move this to step builder.
                /// 
                int index = 0;
                List<int> ordering = new List<int>();
                foreach (WorkflowStep step in entityObject.WorkflowSteps)
                {
                    index++;
                    TestStringForWhiteSpace(step.StepName, string.Format(Messages.EMPTY_STEP_NAME, index), Errors);
                    ///
                    /// check to make sure no two steps have the same number
                    ///
                    if (ordering.Contains(step.StepOrder))
                    {
                        Errors.Add(string.Format(string.Format(Messages.DUPLICATE_STEPS_DEFINED, step.StepOrder)));
                    }
                    else
                    {
                        ordering.Add(step.StepOrder);
                    }
                }

            }
            return Errors;
        }
        #endregion
    }
}
