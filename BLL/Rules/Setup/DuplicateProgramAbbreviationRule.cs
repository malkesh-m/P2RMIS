using Sra.P2rmis.Bll.Setup.Blocks;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///      - The program abbreviation (for individual clients) must be unique.
    /// </summary>
    internal class DuplicateProgramAbbreviationRule : RuleBase<ProgramYear>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public DuplicateProgramAbbreviationRule(IUnitOfWork unitOfWork, ProgramYear entity) 
            : base(unitOfWork, entity, ActionCollection)
        { }
        #endregion
        #region Attributes 
        /// <summary>
        /// This rule applies to these CRUD actions
        /// </summary>
        protected static List<CrudAction> ActionCollection { get; } = new List<CrudAction> { CrudAction.Add };
        #endregion
        #region Services
        /// <summary>
        /// Apply the rule and update the rule state.
        /// </summary>
        public override void Apply(ICrudBlock block)
        {
            ProgramSetupBlock programBlock = block as ProgramSetupBlock;
            //
            // Check if the ProgramAbbreviation is not unique for this client
            //
            bool result = UnitOfWork.ClientRepository.Select()
                                                .Where(x => x.ClientID == programBlock.ClientId)
                                                .SelectMany(x => x.ClientPrograms1)
                                                .Any(x => x.ProgramAbbreviation == programBlock.ProgramAbbreviation);
            //
            // If it is not unique say we are broken & return a message.
            //
            if (result)
            {
                this.IsBroken = true;
                this.Message = MessageService.AddProgramWithDuplicateAbbreviation;
            }
        }
        #endregion
    }
}
