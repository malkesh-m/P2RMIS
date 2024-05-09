using Sra.P2rmis.Bll.Setup.Blocks;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///      Cannot delete an award if there are applications associated with its child award
    /// </summary>
    internal class AwardWithChildAwardApplicationRule : RuleBase<ProgramMechanism>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public AwardWithChildAwardApplicationRule(IUnitOfWork unitOfWork, ProgramMechanism entity)
            : base(unitOfWork, entity, ActionCollection)
        { }
        #endregion
        #region Attributes 
        /// <summary>
        /// This rule applies to these CRUD actions
        /// </summary>
        protected static List<CrudAction> ActionCollection { get; } = new List<CrudAction> { CrudAction.Delete };
        #endregion
        #region Services
        /// <summary>
        /// Apply the rule and update the rule state.
        /// </summary>
        public override void Apply(ICrudBlock block)
        {
            AwardSetupBlock awardBlock = block as AwardSetupBlock;
            var childEntities = UnitOfWork.ProgramMechanismRepository.Select()
                                          .Where(x => x.ParentProgramMechanismId == this.Entity.ProgramMechanismId);
            //
            // Check if any of the children has assigned applications
            // 
            if (childEntities.Where(x => x.Applications.Count() > 0).Count() > 0)
            {
                this.IsBroken = true;
                this.Message = MessageService.CannotDeleteAwardWithChildAwardApps;
            }
        }
        #endregion
    }
}
