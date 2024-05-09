using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Setup.Blocks;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///      Cannot move panel if the application assignments have been released
    /// </summary>
    internal class ReleasedAssignmentsForPanelRule : RuleBase<SessionPanel>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public ReleasedAssignmentsForPanelRule(IUnitOfWork unitOfWork, SessionPanel entity)
            : base(unitOfWork, entity, ActionCollection)
        { }
        #endregion
        #region Attributes 
        /// <summary>
        /// This rule applies to these CRUD actions
        /// </summary>
        protected static List<CrudAction> ActionCollection { get; } = new List<CrudAction> { CrudAction.Modify };
        #endregion
        #region Rule implemenation
        /// <summary>
        /// Apply the rule and update the rule state.
        /// </summary>
        public override void Apply(ICrudBlock block)
        {
            PanelBlock panelBlock = block as PanelBlock;
            //
            // Now we navigate back through the properties:
            //
            bool result = UnitOfWork.SessionPanelRepository.Select()
                               //
                               // We get the specific award we are working with
                               //
                               .Where(x => x.SessionPanelId == panelBlock.SessionPanelId)
                               //
                               // then collect their PanelApplications
                               //
                               .SelectMany(x => x.PanelApplications)
                               //
                               // and collect their ApplicationStages
                               //
                               .SelectMany(x => x.ApplicationStages)
                               //
                               // To finally check if there are any stages with the AssignmentVisibilityFlag set
                               //
                               .Any(x => x.AssignmentVisibilityFlag);
            if (result)
            {
                this.IsBroken = true;
                this.Message = MessageService.ReleasedAssignmentsForPanelViolation;
            }
        }
        #endregion
    }
}
