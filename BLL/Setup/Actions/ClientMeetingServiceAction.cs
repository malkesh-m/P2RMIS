using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels;
using Sra.P2rmis.WebModels.Setup;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.Setup.Actions
{
    /// <summary>
    /// Service Action method to Add/Modify/Delete ClientMeeting.
    /// </summary>
    internal class ClientMeetingServiceAction : ServiceAction<ClientMeeting>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public ClientMeetingServiceAction() { }
        #endregion
        #region Attributes
        /// <summary>
        /// Information about the entity manipulated by the CRUD operation
        /// </summary>
        public IEnumerable<IEntityInfo> EntityInfo { get; private set; } = new List<IEntityInfo>();
        /// <summary>
        /// This is the CRUD'ed ClientMeeting
        /// </summary>
        protected ClientMeeting CRUDedProgramMechanism { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// We tell the service action how to populate the entity with the data.
        /// </summary>
        /// <param name="entity">ProgramMechanism entity being populated</param>
        protected override void Populate(ClientMeeting entity)
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
        protected override void PostAdd(ClientMeeting entity)
        {
            //
            // And we remember the ClientMeeting we just created.
            //
            this.CRUDedProgramMechanism = entity;
        }
        /// <summary>
        /// What happens after an "modify" is done.
        /// </summary>
        /// <param name="entity">The clientMeeting entity.</param>
        protected override void PostModify(ClientMeeting entity)
        {
            entity.MeetingSessions.SelectMany(x => x.SessionPanels).SelectMany(y => y.PanelUserAssignments)
                .SelectMany(z => z.MeetingRegistrations)
                .SelectMany(w => w.MeetingRegistrationHotels.Where(w2 => !w2.CancellationFlag &&
                    !w2.HotelNotRequiredFlag && w2.HotelId != entity.HotelId))
                .ToList().ForEach(v =>
                {
                    v.HotelId = entity.HotelId;
                    Helper.UpdateModifiedFields(v, UserId);
                });
        }
        protected override bool IsDelete { get { return this.Block.IsDelete; } }
        /// <summary>
        /// Pre-delete: delete associated program meetings
        /// </summary>
        /// <param name="entity">ClientMeeting entity</param>
        protected override void PreDelete(ClientMeeting entity)
        {
            var programMeetings = entity.ProgramMeetings.ToList();
            foreach (var programMeeting in programMeetings)
            {
                Helper.UpdateDeletedFields(programMeeting, this.UserId);
                UnitOfWork.ProgramMeetingRepository.Delete(programMeeting);
            }
        }
        /// <summary>
        /// Define the post processing here.  By definition (when the comment was written)
        /// the PostProcess method should only be called when something was added.
        /// </summary>
        public override void PostProcess()
        {
            if ((!this.RuleMachine.IsBroken) && (this.IsAdd))
            {
                this.EntityInfo = new List<IEntityInfo>() { new ProgramMechanismEntityInfo(this.CRUDedProgramMechanism.ClientMeetingId) };
            }
        }
        #endregion
    }
}
