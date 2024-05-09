using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.SummaryStatement;

namespace Sra.P2rmis.Bll.Builders
{
    public class TemplateStepModelBuilder : Builder<WorkflowStep, WorkflowStepModel>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entity">Entity workflow object</param>
        public TemplateStepModelBuilder(WorkflowStep entity) : base(entity) { }
        #endregion
        #region BuilderServices
        /// <summary>
        /// Specifics for constructing a WorkflowTemplateModel from an entity model.
        /// </summary>
        /// <returns>WorkflowTemplateModel</returns>
        public override WorkflowStepModel Build()
        {
            WorkflowStepModel model = new WorkflowStepModel();
            //
            // Copy over the entity information
            //
            model.WorkflowStepId = this._entity.WorkflowStepId;
            model.WorkflowId = this._entity.WorkflowId;
            model.StepName = this._entity.StepName;
            model.StepOrder = this._entity.StepOrder;
            model.ActiveDefault = this._entity.ActiveDefault;            
 
            return model;
        }
        #endregion 
    }
}
