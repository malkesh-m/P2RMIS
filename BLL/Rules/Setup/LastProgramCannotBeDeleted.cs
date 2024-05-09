using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///      Deleting the last ProgramYear is not permitted
    /// </summary>
    internal class LastProgramCannotBeDeleted : RuleBase<ProgramYear>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public LastProgramCannotBeDeleted(IUnitOfWork unitOfWork, ProgramYear entity)
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
            if (Entity.ClientProgram.IsLastProgramYear())
            {
                this.IsBroken = true;
                this.Message = MessageService.CannotRemoveLastProgram;
            }
        }
        #endregion
    }
}
