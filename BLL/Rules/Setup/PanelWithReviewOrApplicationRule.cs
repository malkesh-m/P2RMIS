using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Setup.Blocks;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///      Cannot remove panel if it has reviews or applications associated.
    /// </summary>
    internal class PanelWithRevieworApplicationRule : RuleBase<SessionPanel>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public PanelWithRevieworApplicationRule(IUnitOfWork unitOfWork, SessionPanel entity)
            : base(unitOfWork, entity, ActionCollection)
        { }
        #endregion
        #region Attributes 
        /// <summary>
        /// This rule applies to these CRUD actions
        /// </summary>
        protected static List<CrudAction> ActionCollection { get; } = new List<CrudAction> { CrudAction.Delete };
        #endregion
        #region Rule implemenation
        /// <summary>
        /// Apply the rule and update the rule state.
        /// </summary>
        public override void Apply(ICrudBlock block)
        {
            if (Entity.PanelApplications.Count > 0 ||
                Entity.AssignedReviewers().Count > 0)
            {
                this.IsBroken = true;
                this.Message = MessageService.ReviewsOrApplicationsAssignedToPanelViolation;
            }
        }
        #endregion
    }
}
