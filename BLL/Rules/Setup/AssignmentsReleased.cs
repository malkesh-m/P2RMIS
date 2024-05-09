using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.Bll.Rules.Setup
{
    /// <summary>
    /// Rule implementation of:
    ///      When assignments no edits are allowed
    /// </summary>
    internal class AssignmentsReleased : RuleBase<ProgramYear>
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        public AssignmentsReleased(IUnitOfWork unitOfWork, ProgramYear entity) 
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
            if (ApplicationsAreReleased())
            {
                this.IsBroken = true;
                this.Message = MessageService.EditingNotPermittedBecauseApplicationsReleased;
            }
        }
        /// <summary>
        /// Determines if applications have been released.
        /// </summary>
        /// <returns>True if applications are released; false otherwise</returns>
        protected bool ApplicationsAreReleased()
        {
            bool results = Entity.ProgramPanels.AsQueryable()
                            //
                            // The indication that an application has been released is located in 
                            // the ApplicationStage.  Go and get all the stages for this ProgramYear
                            //
                            .Select(x => x.SessionPanel)
                            .SelectMany(x => x.PanelApplications)
                            .SelectMany(x => x.ApplicationStages)
                            //
                            // by definition we only need to look at the first stage
                            //
                            .Where(x => x.ReviewStageId == ReviewStage.Asynchronous)
                            //
                            // we just see if any assignments have been released.
                            //
                            .Any(x => x.AssignmentVisibilityFlag);
            //
            // Now all we do is execute it and check the results.
            //
            return results;
        }
        #endregion
    }
}
