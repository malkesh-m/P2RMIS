using Sra.P2rmis.Bll.Setup.Blocks;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.Rules.Setup
{

    /// <summary>
    /// Rule implementation of:
    ///      Cannot Add or edit an evaluation criteria once scoring has been set up.
    /// </summary>
    internal class AwardsCriteriaRule : RuleBase<MechanismTemplateElement>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public AwardsCriteriaRule(IUnitOfWork unitOfWork, MechanismTemplateElement entity)
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
            // If template elements exist then we need to check the MechanismTemplateElements in the synchronous review stage.
            // 
            if (templateElement.MechanismTemplateElements.Count() > 0)
            {
                //
                // Navigate back through the properties to get to the elements in the synchronous stage.
                //
                bool result = templateElement.ProgramMechanism.MechanismTemplates
                    .Where(x => x.ReviewStageId == ReviewStage.Synchronous)
                    //
                    // We select all of the MechanismTemplateElements
                    //
                    .SelectMany(x => x.MechanismTemplateElements)
                    //
                    // Then check if any MechanismTemplateElementScorings exist.  If they have been then scoring has been set up.
                    //
                    .SelectMany(x => x.MechanismTemplateElementScorings)
                    //
                    // And now we count them
                    //
                    .Count() != 0;

                if (result)
                {
                    this.IsBroken = true;
                    this.Message = MessageService.EvaluationCriteriaScoringViolation;
                }
            }
        }
        #endregion
    }
}
