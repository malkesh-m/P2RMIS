
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ApplicationStageStep object. 
    /// </summary>
    public partial class ApplicationStageStep : IStandardDateFields
    {
        /// <summary>
        /// the application's log number.
        /// </summary>
        /// <returns>string log number</returns>
        public string LogNumber()
        {
            return this.ApplicationStage.PanelApplication.Application.LogNumber;
        }
        /// <summary>
        /// Retrieves the workflows associated with this ApplicationStageStep;
        /// </summary>
        /// <returns>Enumeration of ApplicationWorkflows</returns>
        public IEnumerable<ApplicationWorkflow> RetrieveStepWorkflows()
        {
            return this.ApplicationStage.ApplicationWorkflows;
        }
        /// <summary>
        /// Retrieve the discussion associated with the ApplicationStageStep.
        /// </summary>
        /// <remarks>
        /// Even though the relationship between ApplicationStageStep & ApplicationStageStepDiscussion
        /// is shown as a 1 : Many, it is really a 1:1.  The 1:Many is necessary because of "soft deletes"
        /// </remarks>
        /// <returns>ApplicationStageStepDiscussion entity if one exists; null otherwise</returns>
        public ApplicationStageStepDiscussion RetrieveDiscussion()
        {
            return this.ApplicationStageStepDiscussions.FirstOrDefault();
        }
        /// <summary>
        /// Determines if a discussion has already been started.  Even though the relationship between
        /// the ApplicationStageStep is shown as 1:Many it is really 1:1.  (It is this way because of the
        /// soft delete).  We just check if there is an ApplicationStageStepDiscussion already.
        /// <param name="applicationStageStepEntityId">ApplicationStageStep entity identifier</param>
        /// <returns>True if the on-line discussion has been start; false otherwise.</returns>
        public bool IsDiscussionNotStarted()
        {
            return this.RetrieveDiscussion() == null;
        }
        /// <summary>
        /// Determine if any critique is not submitted for a panel application.
        /// </summary>
        /// <param name="applicationStageStepEntityId">ApplicationStageStep entity identifier</param>
        /// <returns>True if all critiques are submitted; false otherwise</returns>
        public virtual bool AreAllReviewersCritiquesSubmitted()
        {  
            //
            // From the ApplicationStage access all the reviewers workflows for this stage
            //
            bool result = this.ApplicationStage.ApplicationWorkflows.
                          //
                          // And select all of the workflow steps and order them by their step order.
                          //
                          SelectMany(x => x.ApplicationWorkflowSteps).OrderBy(x => x.StepOrder).
                          //
                          // Then select all workflow steps that are before the Final (MOD) phase step.  If
                          // the Resolution of any of these steps are false then the critique is not submitted.
                          //
                          TakeWhile(x => x.StepTypeId != StepType.Indexes.Final).Any(x => !x.Resolution);
            return !result;
        }

        /// <summary>
        /// Populates the specified panel stage step identifier.
        /// </summary>
        /// <param name="panelStageStepId">The panel stage step identifier.</param>
        public virtual void Populate(int panelStageStepId)
        {
            this.PanelStageStepId = panelStageStepId;
        }
    }
}
