using Sra.P2rmis.Bll.Setup.Blocks;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///      Cannot edit an award (change the ClientAwardType) if there is an associated Pre-App award.
    /// </summary>
    internal class DeleteAwardCannotHavePreAppRule: RuleBase<ProgramMechanism>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public DeleteAwardCannotHavePreAppRule(IUnitOfWork unitOfWork, ProgramMechanism entity)
            : base(unitOfWork, entity, ActionCollection)
        { }
        #endregion
        #region Attributes 
        /// <summary>
        /// This rule applies to these CRUD actions
        /// </summary>
        protected static List<CrudAction> ActionCollection { get; } = new List<CrudAction> { CrudAction.Modify, CrudAction.Delete };
        #endregion
        #region Services
        /// <summary>
        /// Apply the rule and update the rule state.
        /// </summary>
        public override void Apply(ICrudBlock block)
        {
            AwardSetupBlock awardBlock = block as AwardSetupBlock;
            //
            // We only want to apply this rule to non pre-app blocks that are changing the ClientAwardType
            //
            if (!awardBlock.IsPreAppAdd() & (awardBlock.ClientAwardTypeId != this.Entity.ClientAwardTypeId))
            {
                //
                // We search for any children
                //
                ProgramMechanism entity = UnitOfWork.ProgramMechanismRepository.Select()
                                                                               .FirstOrDefault(x => x.ParentProgramMechanismId == this.Entity.ProgramMechanismId);
                //
                // If there is one then we do not allow the deletion
                //
                if (entity != null)
                {
                    this.IsBroken = true;
                    this.Message = MessageService.PreAppRuleViolation;
                }
            }
        }
        #endregion
    }
}
