using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Bll.Setup.Blocks;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///    - The Fiscal Year for an added program must be unique
    /// </summary>
    internal class UniqueProgramFiscalYearRule : RuleBase<IStandardDateFields>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public UniqueProgramFiscalYearRule(IUnitOfWork unitOfWork, IStandardDateFields entity)
            : base(unitOfWork, entity, ActionCollection)
        { }
        #endregion
        #region Attributes 
        /// <summary>
        /// This rule applies to these CRUD actions
        /// </summary>
        protected static List<CrudAction> ActionCollection { get; } = new List<CrudAction> { CrudAction.Add};
        #endregion
        #region Services
        /// <summary>
        /// Apply the rule and update the rule state if necessary.
        /// </summary>
        public override void Apply(ICrudBlock block)
        {
            var programBlock = block as ProgramSetupBlock;
            //
            // If the client program does not exist then then there is no need to run the rule
            //
            if (programBlock.ClientProgramId.HasValue)
            {
                CheckIfDuplicateYearForProgram(programBlock);
            }
        }
        /// <summary>
        /// Actual rule implementation
        /// </summary>
        /// <param name="programBlock">The rule's block as type specific</param>
        protected virtual void CheckIfDuplicateYearForProgram(ProgramSetupBlock programBlock)
        {
            //
            // First we locate the specific ClientProgram
            //
            ClientProgram entity = UnitOfWork.ClientProgramRepository.GetByID(programBlock.ClientProgramId);
            //
            // Then check if any of it's FiscalYears (i.e. the ProgramYear)
            // has the same year value.
            //
            bool result = entity.ProgramYears.Any(x => x.Year == programBlock.ProgramYear);
            //
            // If there is an existing fiscal year then the rule is broken.
            //
            if (result)
            {
                this.IsBroken = true;
                this.Message = MessageService.AddDuplicateFiscalYear(entity.ProgramAbbreviation);
            }
        }

        #endregion
    }
}
