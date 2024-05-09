using Sra.P2rmis.Dal;
using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///      When awards have been assigned to a to a program it may not be deleted.
    /// </summary>
    internal class AwardsAssignedToProgram: RuleBase<ProgramYear>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public AwardsAssignedToProgram(IUnitOfWork unitOfWork, ProgramYear entity) 
            : base(unitOfWork, entity, ActionCollection)
        {}
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
            //
            // When an award (i.e. Mechanism) is assigned to a program  entity
            // the last program may not be deleted.
            // 
            if (Entity.IsAwardsAssigned())
            {
                this.IsBroken = true;
                this.Message = MessageService.CannotRemoveProgramIfAwardsAssigned;
            }
        }
        #endregion
    }
}
