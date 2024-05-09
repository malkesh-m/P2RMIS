using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Setup.Blocks;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///      Cannot edit or delete an award with criteria.
    /// </summary>
    internal class AwardWithCriteriaRule : RuleBase<ProgramMechanism>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public AwardWithCriteriaRule(IUnitOfWork unitOfWork, ProgramMechanism entity)
            : base(unitOfWork, entity, ActionCollection)
        { }
        #endregion
        #region Attributes 
        /// <summary>
        /// This rule applies to these CRUD actions
        /// </summary>
        protected static List<CrudAction> ActionCollection { get; } = new List<CrudAction> { CrudAction.Modify, CrudAction.Delete };
        #endregion
        #region Rule implemenation
        /// <summary>
        /// Apply the rule and update the rule state.
        /// </summary>
        public override void Apply(ICrudBlock block)
        {
            //
            // All this is is just checking the number of evaluation criteria on the ProgramMechanism
            // 
            int cnt = UnitOfWork.MechanismTemplateElementRepository.Get(x =>
                x.MechanismTemplate.ProgramMechanismId == Entity.ProgramMechanismId).Count();
            if (cnt > 0)
            {
                AwardSetupBlock awardBlock = block as AwardSetupBlock;
                this.IsBroken = true;
                this.Message = (awardBlock.IsModify) ? MessageService.CannotModifyAwardWithCriteria : MessageService.CannotDeleteAwardWithCriteria;
            }
        }
        #endregion
    }
}
