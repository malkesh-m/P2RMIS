using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Bll.EntityBuilders
{
    /// <summary>
    /// Builds/updates an entity TemplateStep object from the view model.
    /// </summary>
    public class TemplateStepBuilder : EntityBuilder<WorkflowStepModel, WorkflowStep>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">Workflow view model</param>
        /// <param name="userId">User id</param>
        public TemplateStepBuilder(WorkflowStepModel model, int userId) : base(model, userId) { }
        #endregion
        #region BuilderServices
        /// <summary>
        /// Specifics for constructing a workflow step from an WorkflowTemplateModel.
        /// </summary>
        /// <returns>Entity workflow object</returns>
        public override WorkflowStep Build()
        {
            WorkflowStep entityObject = new WorkflowStep();
            //
            // Copy over the entity information
            //
            entityObject.StepName = this._model.StepName;
            entityObject.StepOrder = this._model.StepOrder;
            entityObject.ActiveDefault = this._model.ActiveDefault;
            //
            // We are definitely dirty since we are building it
            //
            IsDirty = true;
            //
            // Now set the creation & modified by fields
            //
            SetCreateBy(entityObject, this._userId);
            SetModifiedBy(entityObject, this._userId);

            return entityObject;
        }
        /// <summary>
        /// Build method takes the model object & updates an entity object.  All 
        /// instances of the builder must override this.
        /// </summary>
        /// <param name="model">Entity model to update</param>
        /// <returns>Updated entity object</returns>
        public override WorkflowStep Update(WorkflowStep entityObject)
        {
            //
            // Copy over the entity information
            //
            UpdatePropertyIfChanged(entityObject.StepName, this._model.StepName, value => entityObject.StepName = value);
            UpdatePropertyIfChanged(entityObject.StepOrder, this._model.StepOrder, value => entityObject.StepOrder = value);
            UpdatePropertyIfChanged(entityObject.ActiveDefault, this._model.ActiveDefault, value => entityObject.ActiveDefault = value);
            //
            // if something was changed then we need to update the appropriate fields
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
        /// <param name="entityObject">Entity workflow object</param>
        private WorkflowStep SetModifiedBy(WorkflowStep entityObject, int userId)
        {
            entityObject.ModifiedBy = userId;
            entityObject.ModifiedDate = GlobalProperties.P2rmisDateToday;
            return entityObject;
        }
        /// <summary>
        /// Sets the Entity Workflow objects created by fields.
        /// </summary>
        /// <param name="entityObject">Entity workflow object</param>
        private WorkflowStep SetCreateBy(WorkflowStep entityObject, int userId)
        {
            entityObject.CreatedBy = userId;
            entityObject.CreatedDate = GlobalProperties.P2rmisDateToday;
            return entityObject;
        }
        #endregion
    }
}
