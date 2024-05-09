using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Setup.Blocks;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///      There cannot be two panels with the same panel name or panel abbreviation
    /// </summary>
    internal class DuplicatePanelRule : RuleBase<SessionPanel>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public DuplicatePanelRule(IUnitOfWork unitOfWork, SessionPanel entity)
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
            PanelBlock panelBlock = block as PanelBlock;
            IEnumerable<SessionPanel> searchResults = DuplicateSearch(panelBlock);
            //
            // If the rule is being executed for an 'add' operation then one cannot exist 
            // with the same values that were passed into the rule
            //
            if ((panelBlock.IsAdd) & (searchResults.Count() != 0))
            {
                this.IsBroken = true;
                this.Message = MessageService.AddDuplicatePanel;
            }
            //
            // Otherwise if the rule is being executed for an 'add' operation, the SessionPanel
            // located cannot be the same as the entity being modified.  In which case we have a problem.
            //
            else if (panelBlock.IsModify)
            {
                SessionPanel entity = searchResults.FirstOrDefault();

                if ((entity != null) && (entity.SessionPanelId != Entity.SessionPanelId))
                {
                    this.IsBroken = true;
                    this.Message = MessageService.ModifyDuplicatePanel;
                }
            }
        }
        /// <summary>
        /// Searches for duplicate values
        /// </summary>
        /// <returns>Collection of duplicate values</returns>
        protected virtual IEnumerable<SessionPanel> DuplicateSearch(PanelBlock panelBlock)
        {
            IEnumerable<SessionPanel> result = UnitOfWork.SessionPanelRepository.Select()
                                                //
                                                // First we retrieve the client id
                                                //
                                                .Where(x => x.ProgramPanels.FirstOrDefault().ProgramYear.ProgramYearId == panelBlock.ProgramYearId)
                                                //
                                                // then filter them by a specific PanelName & PanelAbbreviation
                                                //
                                                .Where(x => (x.PanelName == panelBlock.PanelName
                                                || x.PanelAbbreviation == panelBlock.PanelAbbreviation) &&
                                                x.SessionPanelId != panelBlock.SessionPanelId);
                                                //
                                                // check to see if any of the panel name or abbreviation are differnt
                                                //
            //
            // And now we execute it
            //
            return result.ToList();
        }
        #endregion
    }
}
