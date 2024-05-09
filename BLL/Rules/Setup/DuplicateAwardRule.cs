using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Setup.Blocks;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///      There cannot be two awards with the same ProgramYear; ClientAwardType & Receipt Cycle
    /// </summary>
    internal class DuplicateAwardRule : RuleBase<ProgramMechanism>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public DuplicateAwardRule(IUnitOfWork unitOfWork, ProgramMechanism entity)
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
            AwardSetupBlock awardBlock = block as AwardSetupBlock;
            IEnumerable<ProgramMechanism> searchResults = DuplicateSearch(awardBlock);
            //
            // If the rule is being executed for an 'add' operation then one cannot exist 
            // with the same values that were passed into the rule
            //
            if ((awardBlock.IsAdd) & (searchResults.Count() != 0))
            {
                this.IsBroken = true;
                this.Message = MessageService.AddDuplicateAward; 
            }
            //
            // Otherwise if the rule is being executed for an 'add' operation, the ProgramMechanism
            // located cannot be the same as the entity being modified.  In which case we have a problem.
            //
            else if (awardBlock.IsModify)
            {
                ProgramMechanism entity = searchResults.FirstOrDefault();

                if ((entity != null) && (entity.ProgramMechanismId != Entity.ProgramMechanismId))
                {
                    this.IsBroken = true;
                    this.Message = MessageService.ModifyDuplicateAward;
                }
            }
        }
        /// <summary>
        /// Searches for duplicate values
        /// </summary>
        /// <returns>Collection of duplicate values</returns>
        protected virtual IEnumerable<ProgramMechanism> DuplicateSearch(AwardSetupBlock awardBlock)
        {
            IEnumerable<ProgramMechanism> result = UnitOfWork.ProgramYearRepository.Select()
                                                          //
                                                          // First we retrieve the program year
                                                          //
                                                          .Where(x => x.ProgramYearId == awardBlock.ProgramYearId)
                                                          //
                                                          // Then pull all of it's ProramMechanism
                                                          //
                                                          .SelectMany(x => x.ProgramMechanism)
                                                          //
                                                          // then filter them by a specific ClientAwardType & ReceiptCycle
                                                          //
                                                          .Where(x => x.ClientAwardTypeId == awardBlock.ClientAwardTypeId)
                                                          .Where(x => x.ReceiptCycle.Value == awardBlock.ReceiptCycle);
            //
            // And now we execute it
            //
            return result.ToList();
        }
        #endregion
    }
}
