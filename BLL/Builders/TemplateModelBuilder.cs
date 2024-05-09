using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;
using EntityWorkflow = Sra.P2rmis.Dal.Workflow;

namespace Sra.P2rmis.Bll.Builders
{
    /// <summary>
    /// Builder constructs a TemplateModel from an entity template object.
    /// </summary>
    public class TemplateModelBuilder: Builder<EntityWorkflow, WorkflowTemplateModel>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entity">Entity workflow object</param>
        public TemplateModelBuilder(EntityWorkflow entity) : base(entity) { }
        #endregion
        #region BuilderServices
        /// <summary>
        /// Specifics for constructing a WorkflowTemplateModel from an entity model.
        /// </summary>
        /// <returns>WorkflowTemplateModel</returns>
        public override WorkflowTemplateModel Build()
        {
            WorkflowTemplateModel model = new WorkflowTemplateModel();
            //
            // Copy over the entity information
            //
            model.WorkflowId = this._entity.WorkflowId;
            model.ClientId = this._entity.ClientId;
            model.WorkflowName = this._entity.WorkflowName;
            model.WorkflowDescription = this._entity.WorkflowDescription;
            //
            // The steps also need built. Do them here
            //
            foreach (WorkflowStep step in this._entity.WorkflowSteps)
            {
                TemplateStepModelBuilder builder = new TemplateStepModelBuilder(step);
                model.Steps.Add(builder.Build());
            }
            return model;
        }
        #endregion
    }
}
