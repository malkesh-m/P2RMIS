using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Setup;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Setup.Actions
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete MechanismTemplateElement.
    /// </summary>
    internal class EvaluationCriteriaServiceAction : ServiceAction<MechanismTemplateElement>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public EvaluationCriteriaServiceAction() { }
        #endregion
        #region Attributes
        /// <summary>
        /// Information about the entity manipulated by the CRUD operation
        /// </summary>
        public IEnumerable<IEntityInfo> EntityInfo { get; private set; } = new List<IEntityInfo>();
        /// <summary>
        /// This is the CRUD'ed MechanismTemplateElement
        /// </summary>
        protected MechanismTemplateElement CRUDedEntity { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// We tell the service action how to populate the entity with the data.
        /// </summary>
        /// <param name="entity">MechanismTemplateElement entity being populated</param>
        protected override void Populate(MechanismTemplateElement entity)
        {
            this.Block.Populate(this.UserId, entity);
        }
        /// <summary>
        /// And we tell it how to determine if we have data
        /// </summary>
        protected override bool HasData { get { return this.Block.HasData(); } }
        #endregion
        #region Optional Overrides
        /// <summary>
        /// What happens after an add is done.
        /// </summary>
        protected override void PostAdd(MechanismTemplateElement entity)
        {
            //
            // We just remember the MechanismTemplateElement we just created.
            //
            this.CRUDedEntity = entity;
        }
        /// <summary>
        /// How to tell if the operation is a delete operation.
        /// </summary>
        protected override bool IsDelete { get { return this.Block.IsDelete; } }
        /// <summary>
        /// Define any post processing here.  By definition (when the comment was written)
        /// the this PostProcess method should only be called when something was added.
        /// </summary>
        public override void PostProcess()
        {
            if ((!this.RuleMachine.IsBroken) && (this.IsAdd))
            {
                this.EntityInfo = new List<IEntityInfo>() { new MechanismTemplateElementEntityInfo(this.CRUDedEntity.MechanismTemplateElementId, this.CRUDedEntity) };
            }
        }
        #endregion
    }
}