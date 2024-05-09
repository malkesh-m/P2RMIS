using Sra.P2rmis.Bll.Setup.Blocks;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///      - a MechanismTemplate can only have one MechanismTemplateElement that is marked "overall".
    /// </summary>
    internal class SingleOverallCriterionRule : RuleBase<MechanismTemplateElement>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public SingleOverallCriterionRule(IUnitOfWork unitOfWork, MechanismTemplateElement entity)
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
            EvaluationCriteriaBlock criteriaBlock = block as EvaluationCriteriaBlock;
            //
            // We only need to check if the criteria is marked as overall
            //
            if (criteriaBlock.OverallFlag)
            {
                bool result = false;

                MechanismTemplate entity = UnitOfWork.MechanismTemplateRepository.GetByID(criteriaBlock.MechanismTemplateId);
                MechanismTemplateElement elementEntity = entity.MechanismTemplateElements.FirstOrDefault(x => x.OverallFlag);
                //
                // There is two cases:
                //  1) we are doing an add.  In which case there cannot be another.
                //  2) we are doing a modify.  In which case if there is a second one it better be the one we are modifying.
                //
                if (elementEntity != null)
                {
                    if (
                        ((criteriaBlock.IsAdd) & (elementEntity != null)) ||
                        ((criteriaBlock.IsModify) & (criteriaBlock.MechanismTemplateElementId != elementEntity.MechanismTemplateElementId))
                        )
                    {
                        result = true;
                    }
                }
                //
                // If it is not unique say we are broken & return a message.
                //
                if (result)
                {
                    this.IsBroken = true;
                    this.Message = MessageService.SingleOverallCriterionRuleViolation;
                }
            }
        }
        #endregion
    }
}
