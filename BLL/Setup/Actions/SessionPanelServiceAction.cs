using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Setup;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.Setup.Actions
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete SessionPanel.
    /// </summary>
    internal class SessionPanelServiceAction : ServiceAction<SessionPanel>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public SessionPanelServiceAction() { }
        #endregion
        #region Attributes
        /// <summary>
        /// Information about the entity manipulated by the CRUD operation
        /// </summary>
        public IEnumerable<IEntityInfo> EntityInfo { get; private set; } = new List<IEntityInfo>();
        /// <summary>
        /// This is the CRUD'ed SessionPanel
        /// </summary>
        protected SessionPanel CRUDedSessionPanel { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// We tell the service action how to populate the entity with the data.
        /// </summary>
        /// <param name="entity">SessionPanel entity being populated</param>
        protected override void Populate(SessionPanel entity)
        {
            this.Block.Populate(this.UserId, entity);
        }
        /// <summary>
        /// Optional post modify processing.  Add any additional processing necessary after the entity object
        /// is modified.
        /// </summary>
        /// <param name="entity"></param>
        protected override void PostModify(SessionPanel entity)
        {
            entity.PanelStages.Where(x => x.WorkflowId == null).ToList().ForEach(y =>
                {
                    this.UnitOfWork.SetEntityDeleted<PanelStage>(this.UserId, y);
                });
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
        protected override void PostAdd(SessionPanel entity)
        {
            //
            // And we remember the SessionPanel we just created.
            //
            this.CRUDedSessionPanel = entity;
        }
        protected override bool IsDelete { get { return this.Block.IsDelete; } }
        /// <summary>
        /// Define the post processing here.  By definition (when the comment was written)
        /// the PostProcess method should only be called when something was added.
        /// </summary>
        public override void PostProcess()
        {
            if ((!this.RuleMachine.IsBroken) && (this.IsAdd))
            {
                this.EntityInfo = new List<IEntityInfo>() { new SessionPanelEntityInfo(this.CRUDedSessionPanel.SessionPanelId) };
            }
        }
        #endregion
    }
}
