using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Setup;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Setup.Actions
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete ProgramMeeting.
    /// </summary>
    internal class ProgramMeetingServiceAction : ServiceAction<ProgramMeeting>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public ProgramMeetingServiceAction() { }
        #endregion
        #region Attributes
        /// <summary>
        /// Information about the entity manipulated by the CRUD operation
        /// </summary>
        public IEnumerable<IEntityInfo> EntityInfo { get; private set; } = new List<IEntityInfo>();
        /// <summary>
        /// This is the CRUD'ed ProgramMeeting
        /// </summary>
        protected ProgramMeeting CRUDedProgramMeeting { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// We tell the service action how to populate the entity with the data.
        /// </summary>
        /// <param name="entity">ProgramMeeting entity being populated</param>
        protected override void Populate(ProgramMeeting entity)
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
        protected override void PostAdd(ProgramMeeting entity)
        {
            //
            // And we remember the ProgramMeeting we just created.
            //
            this.CRUDedProgramMeeting = entity;
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
                this.EntityInfo = new List<IEntityInfo>() { new ProgramMechanismEntityInfo(this.CRUDedProgramMeeting.ProgramMeetingId) };
            }
        }
        #endregion
    }
}
