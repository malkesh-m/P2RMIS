using Sra.P2rmis.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ApplicationWorkflow object.
    /// </summary>
    public partial class ApplicationWorkflow: IStandardDateFields
    {
        /// <summary>
        /// Locates the current workflow step which is defined
        /// as the first step which is active and is not completed.
        /// </summary>
        /// <returns>Current workflow step</returns>
        public virtual ApplicationWorkflowStep CurrentStep()
        {
            return this.ApplicationWorkflowSteps.OrderBy(y => y.StepOrder).FirstOrDefault(x => (x.Active == true) && (x.Resolution == false));
        }
        /// <summary>
        /// Locates the last active workflow step.
        /// </summary>
        /// <returns>Last workflow step</returns>
        public virtual ApplicationWorkflowStep LastStep()
        {
            return this.ApplicationWorkflowSteps.OrderByDescending(y => y.StepOrder).FirstOrDefault(x => (x.Active == true));
        }
        /// <summary>
        /// Determines if the ApplicationWorkflow is complete.  Complete is defined as all
        /// active steps have a Resolution value of true.
        /// </summary>
        /// <returns>True if the workflow is complete; false otherwise.</returns>
        public virtual bool IsComplete()
        {
            return (this.ApplicationWorkflowSteps.OrderBy(y => y.StepOrder).Count(x => (x.Active == true) && (x.Resolution == false)) == 0);
        }
        /// <summary>
        /// Completes the workflow by:
        ///  - setting the Modified by & DateTime
        ///  - setting the DateClosed 
        /// </summary>
        /// <param name="userId"></param>
        public void Complete(int userId)
        {
            Helper.UpdateModifiedFields(this, userId);
            this.DateClosed = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Returns the workflow step identified by the id.
        /// </summary>
        /// <param name="workflowStepId">Workflow step identifier</param>
        /// <returns>Workflow step</returns>
        public virtual ApplicationWorkflowStep GetThisStep(int workflowStepId)
        {
            return this.ApplicationWorkflowSteps.FirstOrDefault(x => x.ApplicationWorkflowStepId == workflowStepId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentStep"></param>
        /// <param name="targetStep"></param>
        public virtual void ResetResolved(ApplicationWorkflowStep currentStep, ApplicationWorkflowStep targetStep)
        {
            this.ApplicationWorkflowSteps.Where(x => ((x.StepOrder >= targetStep.StepOrder) && (x.StepOrder <= currentStep.StepOrder))).ToList().ForEach(delegate(ApplicationWorkflowStep aStep) { aStep.Resolution = false; aStep.ResolutionDate = null; });
        }
        /// <summary>
        /// Set or Clear the Resolution state and date.  Setting or clearing the Resolution
        /// state is determined by if we are moving ahead (setting) or backward (resetting).
        /// </summary>
        /// <param name="currentStep">Current ApplicationWorkflowStep</param>
        /// <param name="targetStep">Target ApplicationWorkflowStep</param>
        /// <param name="userId">User entity identifier</param>
        public virtual void SetResetResolved(ApplicationWorkflowStep currentStep, ApplicationWorkflowStep targetStep, int userId)
        {
            //
            // Then decide if the resolution is TRUE or FALSE and the date falls out from that.
            //
            bool unresolved = currentStep.StepOrder >= targetStep.StepOrder;
            DateTime? resolutionDate = (unresolved) ? null : (DateTime?)GlobalProperties.P2rmisDateTimeNow;
            //
            // Since this can be used to do forward as well as backward movement, standardize the limits
            // and make a list of the steps.  Note that in the case where we are going forward the upper limit
            // workflow step is not retrieved. 
            //
            int upperLimit = ((unresolved) || (currentStep.StepOrder == targetStep.StepOrder)) ? Math.Max(currentStep.StepOrder, targetStep.StepOrder) : Math.Max(currentStep.StepOrder, targetStep.StepOrder) - 1;
            int lowerLimit = Math.Min(currentStep.StepOrder, targetStep.StepOrder);
            List<ApplicationWorkflowStep> list = this.ApplicationWorkflowSteps.Where(x => 
                (
                    //
                    // Within the upper & lower limits
                    //
                    (x.StepOrder >= lowerLimit) && (x.StepOrder <= upperLimit)) &&
                    //
                    // And the step is active
                    //
                    (x.Active)
                )
                    //
                    // And then we make a list of these steps
                    //
                    .ToList();
            //
            // Now we just change each accordingly
            //
            list.ForEach(delegate (ApplicationWorkflowStep aStep) { aStep.Resolution = !unresolved; aStep.ResolutionDate = resolutionDate; Helper.UpdateModifiedFields(aStep, userId); });
        }
        /// <summary>
        /// Reopen the Application workflow by setting its date to null
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>ApplicationWorkflow</returns>
        public ApplicationWorkflow ReOpen(int userId)
        {
            this.DateClosed = null;
            Helper.UpdateModifiedFields(this, userId);

            return this;
        }
        /// <summary>
        /// Obtain assigned user's first name
        /// </summary>
        /// <returns>assigned user's first name</returns>
        public string FirstName()
        {
            return this.PanelUserAssignment.FirstName();
        }
        /// <summary>
        /// Obtain assigned user's last name
        /// </summary>
        /// <returns>assigned user's last name</returns>
        public string LastName()
        {
            return this.PanelUserAssignment.LastName();
        }
        /// <summary>
        /// Obtain the participant type's name
        /// </summary>
        /// <returns>Assigned user's participant type name</returns>
        public string ParticipantTypeName()
        {
            return this.PanelUserAssignment.ClientParticipantType.ParticipantTypeName;
        }
        /// <summary>
        /// Reviewer Assignment Order
        /// </summary>
        /// <returns>Assigned user's presentation order</returns>
        public int ReviewerAssignmentOrder()
        {
            return this.ApplicationStage.PanelApplication.PanelApplicationReviewerAssignments.FirstOrDefault(x => x.PanelUserAssignmentId == this.PanelUserAssignmentId).SortOrder ?? 0;
        }
        /// <summary>
        /// Client role name
        /// </summary>
        /// <returns>Client Role Name if a client role exists; empty string otherwise</returns>
        public string RoleName()
        {
            return (this.PanelUserAssignment.ClientRoleId != null)? this.PanelUserAssignment.ClientRole.RoleName: string.Empty;
        }
        /// <summary>
        /// Identifies the current user (by the identifier)
        /// </summary>
        /// <returns>User entity identifier</returns>
        public int CurrentUser()
        {
            return this.PanelUserAssignment.UserId;
        }
        /// <summary>
        /// Returns the ApplicationWorkflowStep for the specified StepTypeId.
        /// </summary>
        /// <param name="stepTypeId">StepType entity identifier</param>
        /// <returns>ApplicationWorkflowStep for the requested StepType</returns>
        public ApplicationWorkflowStep GetSpecificWorkflowStepByStepType(int stepTypeId)
        {
            return this.ApplicationWorkflowSteps.DefaultIfEmpty(new ApplicationWorkflowStep()).First(x => x.StepTypeId == stepTypeId);
        }
        /// <summary>
        /// Get the ApplicationWorkflowStepElement for the overall rating.
        /// </summary>
        /// <returns>ApplicationWorkflowStepElement for the overall rating; null if there is none</returns>
        public ApplicationWorkflowStepElement GetOverallStepElement()
        {
            ApplicationWorkflowStep applicationWorkflowStepEntity = CurrentStep();
            return (applicationWorkflowStepEntity == null) ? null : applicationWorkflowStepEntity.ApplicationWorkflowStepElements.FirstOrDefault(x => x.ApplicationTemplateElement.MechanismTemplateElement.OverallFlag);
        }
        /// <summary>
        /// Indicates if the workflow is before the first client review workflow step.
        /// </summary>
        /// <returns>True if the summary workflow is before the first client review step; false otherwise</returns>
        public bool IsWorkflowBeforeFirstClientReview()
        {
            //
            // Locate the first client review workflow step.
            //
            ApplicationWorkflowStep applicationWorkflowStepEntity = this.ApplicationWorkflowSteps.
                //
                // Ensure the order 1-n ...
                //
                OrderBy(x => x.StepOrder).
                //
                // Then find the first entry that is a Review or ReviewSupport
                //
                FirstOrDefault(x => (x.StepTypeId == StepType.Indexes.Review) || (x.StepTypeId == StepType.Indexes.ReviewSupport));
            //
            // If there is no client review steps (does not need to be) just return false;  Otherwise
            // check the step order against the current step.
            //
            return (applicationWorkflowStepEntity != null) ? (applicationWorkflowStepEntity.StepOrder > this.CurrentStep().StepOrder) : false;
        }
        /// <summary>
        /// Indicates if the workflow is before the at a client review workflow step.
        /// </summary>
        /// <returns>True if the current step is a ClientReviewStep; False otherwise</returns>
        public bool IsWorkflowAtAClientReviewStep()
        {
            return this.CurrentStep().StepType.IsClientReviewStepType();                                                        
        }
        /// <summary>
        /// Indicates if there is a client review step after the current workflow step.
        /// </summary>
        /// <returns>True if there is a client review step ahead; False otherwise </returns>
        public bool IsClientReviewWorkflowStepAhead()
        {
            ApplicationWorkflowStep applicationWorkflowStepCurrent = this.CurrentStep();
            return this.ApplicationWorkflowSteps.Where(x => x.StepOrder > applicationWorkflowStepCurrent.StepOrder).
                //
                // then order them
                //
                OrderBy(x => x.StepOrder).
                //
                // then get the first one that is a client review
                //
                Any(x => x.IsClientReviewStep());
        }
        /// <summary>
        /// Modifies the Active state (True/False) of a specific client review ApplicaitonWorkflowSteps.
        /// </summary>
        /// <param name="stepTypeId">StepType entity identifier</param>
        public void ActivateAClientReviweWorkflowStep(int stepTypeId, bool state, int userId)
        {
            var currentStepStepOrder = this.CurrentStep().StepOrder;
            ApplicationWorkflowSteps.Where(x => x.StepTypeId == stepTypeId).ToList().ForEach(x => x.ActivateAClientReviweWorkflowStep(state, currentStepStepOrder, userId));
        }
        /// <summary>
        /// Modifies the Active state (True/False) of the client review ApplicaitonWorkflowSteps.
        /// </summary>
        /// <param name="state">New ClientReview ApplicationWorkflowStep entity Active state</param>
        /// <param name="userId">User entity identifier</param>
        public void ActivateClientReviewSteps(bool state, int userId)
        {
            ActivateAClientReviweWorkflowStep(StepType.Indexes.Review, state, userId);
            ActivateAClientReviweWorkflowStep(StepType.Indexes.ReviewSupport, state, userId);
        }
        /// <summary>
        /// Locates the next non client review step after the current.  
        /// </summary>
        /// <remarks>
        /// The assumption is that the workflow is currently sitting on a Client Review step.
        /// </remarks>
        /// <returns>ApplicationWorkflowStep</returns>
        public ApplicationWorkflowStep NextNoneClientReview()
        {
            var currentStepOrder = CurrentStep().StepOrder;
            //
            // We want the next ApplicationWorkflowStep that is:
            //  1) after the current step 
            //  2) is active
            //  3) and is not a Client Review step
            //
            return this.ApplicationWorkflowSteps.FirstOrDefault(x => ((x.StepOrder > currentStepOrder) && 
                                                                        (x.Active) && 
                                                                        (!x.StepType.IsClientReviewStepType())));
        }
    }
}
