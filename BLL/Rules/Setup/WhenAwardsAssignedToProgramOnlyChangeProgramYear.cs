using Sra.P2rmis.Bll.Setup.Blocks;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///     - only the program year can be changed when awards have been assigned.
    /// </summary>
    internal class WhenAwardsAssignedToProgramOnlyChangeProgramYear : RuleBase<ProgramYear>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public WhenAwardsAssignedToProgramOnlyChangeProgramYear(IUnitOfWork unitOfWork, ProgramYear entity)
            : base(unitOfWork, entity, ActionCollection)
        { }
        #endregion
        #region Attributes 
        /// <summary>
        /// This rule applies to these CRUD actions
        /// </summary>
        protected static List<CrudAction> ActionCollection { get; } = new List<CrudAction> { CrudAction.Modify };
        #endregion
        #region Services
        /// <summary>
        /// Apply the rule and update the rule state.
        /// </summary>
        public override void Apply(ICrudBlock block)
        {
            //
            // When an award (i.e. Mechanism) is assigned to a program  entity
            // is linked to the ProgramYear.  So all we need to do is to check the count.
            // 
            if ((Entity.ProgramMechanism.Count > 0) & (IsDataChangedOtherThanProgramYear(block)))
            {
                this.IsBroken = true;
                this.Message = MessageService.OnlyProgramYearMaybeChangedBecauseAwardsAssigned;
            }
        }
        /// <summary>
        /// Verify that no other data has change other than ProgramYear.
        /// </summary>
        /// <param name="block">CrudBlock for the request</param>
        /// <returns>True if data other than the ProgramYear has changed; false otherwise</returns>
        private bool IsDataChangedOtherThanProgramYear(ICrudBlock block)
        {
            ProgramSetupBlock programBlock = block as ProgramSetupBlock;
            return (Entity.ClientProgramId != programBlock.ClientProgramId);
        }
        #endregion
    }
}
