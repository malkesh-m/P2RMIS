using Sra.P2rmis.Bll.Setup.Blocks;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.Rules.Setup
{

    /// <summary>
    /// Rule implementation of:
    ///      Cannot Add or edit an evaluation criteria once assignments have been released on any panel that has the award assigned.
    /// </summary>
    internal class ReleasedAssignmentsForAwardsRule : RuleBase<MechanismTemplateElement>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public ReleasedAssignmentsForAwardsRule(IUnitOfWork unitOfWork, MechanismTemplateElement entity)
            : base(unitOfWork, entity, ActionCollection)
        { }
        #endregion
        #region Attributes 
        /// <summary>
        /// This rule applies to these CRUD actions
        /// </summary>
        protected static List<CrudAction> ActionCollection { get; } = new List<CrudAction> { CrudAction.Add, CrudAction.Modify };
        #endregion
        #region Services
        /// <summary>
        /// Apply the rule and update the rule state.
        /// </summary>
        public override void Apply(ICrudBlock block)
        {
            EvaluationCriteriaBlock awardBlock = block as EvaluationCriteriaBlock;
            MechanismTemplate templateElement = UnitOfWork.MechanismTemplateRepository.GetByID(awardBlock.MechanismTemplateId);
            //
            // Now we navigate back through the properties:
            //
            bool result = UnitOfWork.ProgramMechanismRepository.Select()
                               //
                               // We get the specific award we are working with
                               //
                               .Where(x => x.ProgramMechanismId == templateElement.ProgramMechanismId)
                               //
                               // then we want any (or all) the applications associated with it
                               //
                               .SelectMany(x => x.Applications)
                               //
                               // then collect their PanelApplications
                               //
                               .SelectMany(x => x.PanelApplications)
                               //
                               // and collect their ApplicationStages
                               //
                               .SelectMany(x => x.ApplicationStages)
                               //
                               // To finally check if there are any stages with the AssignmentVisibilityFlag set
                               //
                               .Any(x => x.AssignmentVisibilityFlag);

            if (result)
            {
                this.IsBroken = true;
                this.Message = MessageService.ReleasedAssignmentsForAwardsRuleViolation;
            }
        }
        #endregion
    }
}

