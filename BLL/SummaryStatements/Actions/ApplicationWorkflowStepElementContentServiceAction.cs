using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Setup;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.Setup.Blocks
{
    /// <summary>
    /// Crud block to use for ClientMeeting (Meeting) setup.
    /// </summary>
    internal class ApplicationWorkflowStepElementContentServiceAction : ServiceAction<ApplicationWorkflowStepElementContent>
    {
        #region Construction & Setup

        public ApplicationWorkflowStepElementContentServiceAction()
        {
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Information about the entity manipulated by the CRUD operation
        /// </summary>
        public IEnumerable<IEntityInfo> EntityInfo { get; private set; } = new List<IEntityInfo>();
        /// <summary>
        /// This is the CRUD'ed MeetingSession
        /// </summary>
        protected ApplicationWorkflowStepElementContent CRUDedElementContent { get; set; }
        #endregion
        #region Required Overrides
        protected override void Populate(ApplicationWorkflowStepElementContent entity)
        {
            this.Block.Populate(this.UserId, entity);
        }
        /// And we tell it how to determine if we have data
        /// </summary>
        protected override bool HasData { get { return this.Block.HasData(); } }
        #endregion
        #region Optional Overrides
        /// <summary>
        /// What happens after an add is done.
        /// </summary>
        protected override void PostAdd(ApplicationWorkflowStepElementContent entity)
        {
            //
            // And we remember the MeetingSession we just created.
            //
            this.CRUDedElementContent = entity;
        }
        /// <summary>
        /// Define the post processing here.  By definition (when the comment was written)
        /// the PostProcess method should only be called when something was added.
        /// </summary>
        public override void PostProcess()
        {
            if ((!this.RuleMachine.IsBroken) && (this.IsAdd))
            {
                this.EntityInfo = new List<IEntityInfo>() { new BaseEntityInfo(this.CRUDedElementContent.ApplicationWorkflowStepElementContentId) };
            }
        }
        #endregion
    }
}
