using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Rules
{
    /// <summary>
    /// Rule implementation of:
    ///      - stale data check
    /// At the time it was decided to not include this rule in any RuleEngine.  
    /// Left it in for future use.
    /// </summary>
    internal class StaleEntityRule : RuleBase<IStandardDateFields>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public StaleEntityRule(IUnitOfWork unitOfWork, IStandardDateFields entity)
            : base(unitOfWork, entity, ActionCollection)
        { }
        #endregion
        #region Attributes 
        /// <summary>
        /// This rule applies to these CRUD actions
        /// </summary>
        protected static List<CrudAction> ActionCollection { get; } = new List<CrudAction> { CrudAction.Delete, CrudAction.Modify };
        #endregion
        #region Services
        /// <summary>
        /// Apply the rule and update the rule state.
        /// </summary>
        public override void Apply(ICrudBlock block)
        {
            //
            // There are four cases that could possibly happen
            //     1) both dates are null (no test needed)
            //     2) entity does not have a value and block has a value  (case is not possible but no test needed)
            //     3) the entity has a value, but the block does not
            //     4) both have a value (in which case we compare the values)
            if (
                ((!Entity.ModifiedDate.HasValue) & (!block.ModifiedDate.HasValue))
                || ((!Entity.ModifiedDate.HasValue) & (block.ModifiedDate.HasValue))
                )
            {
                // all is ok,
            }
            else if ((Entity.ModifiedDate.HasValue) & (!block.ModifiedDate.HasValue))
            {
                this.IsBroken = true;
            }
            else
            {
                this.IsBroken = Entity.ModifiedDate.Value != block.ModifiedDate.Value;
            }
            //
            // Now we just return a message if necessary.
            //
            if (this.IsBroken)
            {
                this.Message = "message for stale data";  //MessageService.AwardCannotBeChanged;
            }
        }

        #endregion
    }
}
