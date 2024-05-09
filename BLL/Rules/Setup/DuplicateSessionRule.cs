using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Setup.Blocks;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///      There cannot be two sessions with the same session name or session abbreviation
    /// </summary>
    internal class DuplicateSessionRule : RuleBase<MeetingSession>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public DuplicateSessionRule(IUnitOfWork unitOfWork, MeetingSession entity)
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
            SessionBlock sessionBlock = block as SessionBlock;
            IEnumerable<MeetingSession> searchResults = DuplicateSearch(sessionBlock);

            //
            // If the rule is being executed for an 'add' operation then one cannot exist 
            // with the same values that were passed into the rule
            //
            if ((sessionBlock.IsAdd) & (searchResults.Count() != 0))
            {
                this.IsBroken = true;
                this.Message = MessageService.AddDuplicateSession;
            }
            //
            // Otherwise if the rule is being executed for an 'add' operation, the MeetingSession
            // located cannot be the same as the entity being modified.  In which case we have a problem.
            //
            else if (sessionBlock.IsModify)
            {
                MeetingSession entity = searchResults.FirstOrDefault();

                if ((entity != null) && (searchResults.Count() != 0))
                {
                    this.IsBroken = true;
                    this.Message = MessageService.ModifyDuplicateSession;
                }
            }
        }
        /// <summary>
        /// Searches for duplicate values
        /// </summary>
        /// <returns>Collection of duplicate values</returns>
        protected virtual IEnumerable<MeetingSession> DuplicateSearch(SessionBlock sessionBlock)
        {
            IEnumerable<MeetingSession> result = UnitOfWork.MeetingSessionRepository.Select()
                                                //
                                                // First we retrieve the client meeting id
                                                //
                                                .Where(x => x.ClientMeetingId == sessionBlock.ClientMeetingId && 
                                                 x.MeetingSessionId != sessionBlock.MeetingSessionId)
                                                //
                                                // then filter them by a specific SessionDescription & SessionAbbreviation
                                                //
                                                .Where(x => x.SessionDescription == sessionBlock.SessionDescription
                                                || x.SessionAbbreviation == sessionBlock.SessionAbbreviation);
            //
            // And now we execute it
            //
            return result.ToList();
        }
        #endregion
    }
}
