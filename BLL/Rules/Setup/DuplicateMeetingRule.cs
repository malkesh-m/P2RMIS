using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Setup.Blocks;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///      There cannot be two meetings with the same meeting name or meeting abbreviation
    /// </summary>
    internal class DuplicateMeetingRule : RuleBase<ClientMeeting>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public DuplicateMeetingRule(IUnitOfWork unitOfWork, ClientMeeting entity)
            : base(unitOfWork, entity, ActionCollection)
        { }
        #endregion
        #region Attributes 
        /// <summary>
        /// This rule applies to these CRUD actions
        /// </summary>
        protected static List<CrudAction> ActionCollection { get; } = new List<CrudAction> { CrudAction.Add, CrudAction.Modify };
        #endregion
        #region Rule implemenation
        /// <summary>
        /// Apply the rule and update the rule state.
        /// </summary>
        public override void Apply(ICrudBlock block)
        {
            MeetingBlock meetingBlock = block as MeetingBlock;
            IEnumerable<ClientMeeting> searchResults = DuplicateSearch(meetingBlock);
            //
            // If the rule is being executed for an 'add' operation then one cannot exist 
            // with the same values that were passed into the rule
            //
            if (meetingBlock.IsAdd & searchResults.Count() != 0)
            {
                this.IsBroken = true;
                this.Message = MessageService.AddDuplicateMeeting;
            }
            //
            // Otherwise if the rule is being executed for an 'add' operation, the ProgramMechanism
            // located cannot be the same as the entity being modified.  In which case we have a problem.
            //
            else if (meetingBlock.IsModify && searchResults.Count() != 0)
            {
                this.IsBroken = true;
                this.Message = MessageService.ModifyDuplicateMeeting;
            }
        }
        /// <summary>
        /// Searches for duplicate values
        /// </summary>
        /// <returns>Collection of duplicate values</returns>
        protected virtual IEnumerable<ClientMeeting> DuplicateSearch(MeetingBlock meetingBlock)
        {
            IEnumerable<ClientMeeting> result = UnitOfWork.ClientMeetingRepository.Select()
                                                //
                                                // First we retrieve the client id
                                                //
                                                .Where(x => x.ClientId == meetingBlock.ClientId)
                                                //
                                                // then filter them by a specific MeetingAbbreviation
                                                //
                                                .Where(x => x.ClientMeetingId != meetingBlock.ClientMeetingId &&
                                                x.MeetingAbbreviation == meetingBlock.MeetingAbbreviation);
            //
            // And now we execute it
            //
            return result.ToList();
        }
        #endregion
    }
}
