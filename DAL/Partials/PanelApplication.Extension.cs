using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.CrossCuttingServices;
using System;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PanelApplication object. 
    /// </summary>	
    public partial class PanelApplication: IStandardDateFields
    {
        /// <summary>
        /// Minimum number of summary workflows to be considered started.
        /// </summary>
        public static int MinimumNumberSummaryWorkflows = 1;		
        /// <summary>
        /// Set the application's review order.
        /// </summary>
        /// <param name="newOrder">Application review order; Null indicates the application is triaged</param>
        /// <param name="userId">User requesting the action</param>
        public void Reorder(int? newOrder, int userId)
        {
            this.ReviewOrder = newOrder;
            Helper.UpdateModifiedFields(this, userId);
        }
        /// <summary>
        /// Sets the application's review status.
        /// </summary>
        /// <param name="isTriaged">Flag indicating if the application is triaged</param>
        /// <param name="userId">User requesting the action</param>
        public void SetReviewStatus(bool isTriaged, int userId)
        {
            ApplicationReviewStatu reviewStatus = this.ApplicationReviewStatus.AsQueryable().FirstOrDefault(x => x.ReviewStatusId == ReviewStatu.Triaged);

            if (isTriaged)
            {
                //
                // Application is currently triaged so do not need to change it
                //
                if (reviewStatus != null)
                {
                }
                // 
                // Application is not currently triaged, change the first one and
                // delete the rest.
                else
                {
                    //
                    // Change the first entry
                    //
                    reviewStatus = this.ApplicationReviewStatus.ElementAt(0);
                    reviewStatus.ReviewStatusId = ReviewStatu.Triaged;
                    Helper.UpdateModifiedFields(reviewStatus, userId);
                    //
                    // Now what to do with the others?
                    //
                    List<ApplicationReviewStatu> statusToRemove = new List<ApplicationReviewStatu>(this.ApplicationReviewStatus.AsQueryable().Count());

                    for (int i = 1; i < this.ApplicationReviewStatus.AsQueryable().Count(); i++)
                    {
                        statusToRemove.Add(this.ApplicationReviewStatus.AsQueryable().ElementAt(i));
                    }
                    //
                    // Now we remove them
                    //
                    foreach (var item in statusToRemove)
                    {
                        this.ApplicationReviewStatus.Remove(item);
                    }
                }
            }
            // 
            // Application is not to be triaged
            //
            else
            {
                if (reviewStatus == null)
                {
                    //
                    // Application is not triaged so we do not need to do anything
                    //
                }
                else
                {
                    //
                    // Else we change the only ReviewStatus entry to have a Triaged review status
                    //
                    reviewStatus.ReviewStatusId = ReviewStatu.FullReview;
                    Helper.UpdateModifiedFields(reviewStatus, userId);
                }
            }
        }
        /// <summary>
        /// Determines the current reviewer presentation orders for this PanelApplication.
        /// </summary>
        /// <returns>List comprised of current presentation orders.</returns>
        public List<int> CurrentPresentationOrders()
        {
            List<int> results = new List<int>();
            //
            // Stuff the current presentation order values into a list then sort it
            //
            this.PanelApplicationReviewerAssignments.ToList().ForEach(x => { if (x.SortOrder.HasValue) results.Add(x.SortOrder.Value); });
            results.Sort();

            return results;
        }
        /// <summary>
        /// Determines if the panel application has been released.  An application has been released
        /// if at least one stage is visible.  When an application is released it sets all of it's stages
        /// to visible.  So if we have one visible stage it is released.
        /// </summary>
        /// <returns>True if the panel application is released; false otherwise</returns>
        public bool IsReleased()
        {
            return this.ApplicationStages.FirstOrDefault(s => s.IsReleased()) != null;
        }
        /// <summary>
        /// Determines if the date the panel application was released.  
        /// </summary>
        /// <returns>True if the panel application is released; false otherwise</returns>
        public DateTime? ReleaseDate()
        {
            return this.ApplicationStages.DefaultIfEmpty(new ApplicationStage()).First().AssignmentReleaseDate;
        }
        /// <summary>
        /// Determines the number of actual reviewers on a panel application.
        /// </summary>
        /// <returns>Count of the panel's reviewers</returns>
        public int CountOfReviewers()
        {
            return this.PanelApplicationReviewerAssignments.Count(q => AssignmentType.CritiqueAssignments.Contains(q.ClientAssignmentType.AssignmentTypeId));
        }
        /// <summary>
        /// Indicates if the summary statement processing has been started for this PanelApplication.
        /// </summary>
        /// <returns>True if statement processing has been started; false otherwise</returns>
        public bool IsSummaryStarted()
        {
            return !(this.ApplicationStages.Count(x => x.ReviewStageId == ReviewStage.Summary) < MinimumNumberSummaryWorkflows);
        }
        /// <summary>
        /// Determines whether the panel has been started
        /// </summary>
        /// <returns>True if the panel has been started; false otherwise</returns>
        /// <remarks>It enters the "Meeting/Final" phase if the panel has been started</remarks>
        public bool IsPanelStarted()
        {
            return this.SessionPanel.StartDate <= GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Retrieve the users Assignment on this panel.
        /// </summary>
        /// <param name="userId">UserAssignmentTYpe</param>
        /// <returns></returns>
        public PanelApplicationReviewerAssignment UsersAssignmentType(int userId)
        {
            return this.PanelApplicationReviewerAssignments.FirstOrDefault(x => x.PanelUserAssignment.UserId == userId);
        }
        /// <summary>
        /// Retrieve the users Assignment on this panel based on panelUserAssignmentId
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <returns>PanelApplicationReviewerAssignment</returns>
        public PanelApplicationReviewerAssignment GetPanelApplicationReviewerAssignment(int panelUserAssignmentId)
        {
            return this.PanelApplicationReviewerAssignments.FirstOrDefault(x => x.PanelUserAssignment.PanelUserAssignmentId == panelUserAssignmentId);
        }
        /// <summary>
        /// Determines the value of the application's status.
        /// </summary>
        /// <returns>Application status</returns>
        /// <remarks>PanelApplication should always have at least 1 ApplicationReviewStatus associated</remarks>
        public string DetermineApplicationStatus()
        {
            return
                this.ApplicationReviewStatus.AsQueryable().Where(x => x.ReviewStatu.ReviewStatusTypeId == ReviewStatusType.Review)
                    .DefaultIfEmpty(new ApplicationReviewStatu())
                    .First()
                    .ReviewStatu.ReviewStatusLabel;
        }
        /// <summary>
        /// Returns the ReviewStatusLabel.  Special transform applied to the label for applications that are
        /// triaged.
        /// </summary>
        /// <returns>ReviewStatusLabel</returns>
        public string ReviewStatusLabel()
        {
            ReviewStatu reviewStatusEntity = this.ApplicationReviewStatus.AsQueryable().Where(x => x.ReviewStatu.ReviewStatusTypeId == ReviewStatusType.Review)
                .DefaultIfEmpty(new ApplicationReviewStatu())
                .First()
                .ReviewStatu;

            return (reviewStatusEntity == null)? string.Empty: (reviewStatusEntity.IsTriaged()) ? MessageService.TriagedLabel : reviewStatusEntity.ReviewStatusLabel;
       }
        /// <summary>
        /// Gets the ordered list of reviewers for these panel application reviewer assignments
        /// </summary>
        /// <returns>IEnumerable list of critique reviewer ordered models</returns>
        public IEnumerable<ICritiqueReviewerOrderedModel> GetCritiqueReviewerOrder()
        {
            List<ICritiqueReviewerOrderedModel> results = new List<ICritiqueReviewerOrderedModel>();

            var reviewerAssignments = this.PanelApplicationReviewerAssignments.OrderBy(x => x.SortOrder);

            foreach (PanelApplicationReviewerAssignment assignment in reviewerAssignments)
            {
                ICritiqueReviewerOrderedModel result = new CritiqueReviewerOrderedModel();

                result.ReviewerFirstName = assignment.PanelUserAssignment.User.UserInfoes.FirstOrDefault().FirstName;
                result.ReviewerLastName = assignment.PanelUserAssignment.User.UserInfoes.FirstOrDefault().LastName;
                result.clientAssignmentTypeAbbreviation = assignment.ClientAssignmentType.AssignmentAbbreviation;
                result.PanelApplicationReviewerAssignmentId = assignment.PanelApplicationReviewerAssignmentId;
                result.ReviewerId = assignment.PanelUserAssignment.UserId;

                results.Add(result);
            }
            return results;
        }

        /// <summary>
        /// Retrieves the synchronous (review) ApplicationStage entity if one exists.
        /// </summary>
        /// <returns>ApplicationStage entity or null if none exists</returns>
        public ApplicationStage SynchronousReviewStage()
        {
            return this.ApplicationStages.FirstOrDefault(x => x.ReviewStageId == ReviewStage.Synchronous);
        }
        /// <summary>
        /// Indicates if the application is scored.
        /// </summary>
        /// <param name="stage">ApplicationStage entity</param>
        /// <returns>True if the application is scored; false otherwise</returns>
        public bool IsStageScored(ApplicationStage stage)
        {
            return (stage != null) ? stage.ApplicationWorkflows.SelectMany(x => x.ApplicationWorkflowSteps).SelectMany(x => x.ApplicationWorkflowStepElements).SelectMany(x => x.ApplicationWorkflowStepElementContents).Any(x => x.Score != null || x.Abstain) : false;
        }
        /// <summary>
        /// Indicates if the application has been disapproved.
        /// </summary>
        /// <returns>True if the application has been disapproved; false otherwise</returns>
        public bool IsDisapprovedStatus()
        {
            return this.ApplicationReviewStatus.AsQueryable().FirstOrDefault(x => x.ReviewStatusId == ReviewStatu.Disapproved) != null;
        }
        /// <summary>
        /// Indicates if the unassigned reviewer comments should be enabled.  Comments should be enabled any time after the application
        /// becomes active and before the session ends. The Session check is not implemented here.
        /// </summary>
        /// <returns>True if comments should be available; false otherwise</returns>
        public bool CommentsAvailable()
        {
            return (this.ApplicationReviewStatus.AsQueryable().FirstOrDefault(x => x.ReviewStatusId == ReviewStatu.Scored) != null) ||
                   (this.IsActiveStatus());
        }
        /// <summary>
        /// Indicates if the application has active status.
        /// </summary>
        /// <returns>True if the application has an active status; false otherwise</returns>
        public bool IsActiveStatus()
        {
            return this.ApplicationReviewStatus.AsQueryable().FirstOrDefault(x => (x.ReviewStatusId == ReviewStatu.Active) | (x.ReviewStatusId == ReviewStatu.Scoring)) != null;
        }
        /// <summary>
        /// Indicates if the application is Ready to Score
        /// </summary>
        /// <returns>True if the application is Ready to Score; false otherwise</returns>
        public bool IsReadyToReview()
        {
            return this.ApplicationReviewStatus.AsQueryable().FirstOrDefault(x => (x.ReviewStatusId == ReviewStatu.ReadyToScore)) != null;
        }
        /// <summary>
        /// List the PanelReviewerAssignment's PanelApplicationReviewerAssignmentId
        /// </summary>
        /// <returns>List of the PanelUserAssignmentIds</returns>
        public List<int> ListReviewers()
        {
            List<int> result = new List<int>(5);

            foreach (var p in this.PanelApplicationReviewerAssignments)
            {
                result.Add(p.PanelUserAssignmentId);
            }
            return result;
        }
        /// <summary>
        /// Gets the current review stage id for a panel application based on review status
        /// </summary>
        /// <returns>Id for the current review stage</returns>
        public int GetCurrentReviewStage()
        {
            return ReviewStatu.ActiveScoringStatuses.Contains(
            this.ApplicationReviewStatus.Where(x => x.ReviewStatu.ReviewStatusTypeId == ReviewStatusType.Review)
                    .DefaultIfEmpty(new ApplicationReviewStatu())
                    .First()
                    .ReviewStatusId) ? ReviewStage.Synchronous : ReviewStage.Asynchronous;
        }
        /// <summary>
        /// Whether the current review stage is asynchronous
        /// </summary>
        /// <returns>True if asynchronous; false otherwise.</returns>
        public bool IsCurrentReviewStageAsynchronous()
        {
            return GetCurrentReviewStage() == ReviewStage.Asynchronous;
        }
        /// <summary>
        /// Gets the current application stage.
        /// </summary>
        /// <returns>The current application stage for an application</returns>
        public ApplicationStage GetCurrentApplicationStage()
        {
            return this.ApplicationStages.FirstOrDefault(x => x.ReviewStageId == GetCurrentReviewStage());
        }
        /// <summary>
        /// Determines if the application is currently available for scoring
        /// </summary>
        /// <returns>True if in scoring; otherwise false</returns>
        public bool IsCurrentlyScoring()
        {
            return this.ApplicationReviewStatus.AsQueryable().Where(x => x.ReviewStatu.ReviewStatusTypeId == ReviewStatusType.Review).FirstOrDefault().ReviewStatusId == ReviewStatu.Scoring;
        }
        /// <summary>
        /// Determines if the application is currently being discussed by a panel
        /// </summary>
        /// <returns>True if in discussion; otherwise false</returns>
        public bool IsCurrentDiscussion()
        {
            return this.ApplicationReviewStatus.AsQueryable().Where(x => x.ReviewStatu.ReviewStatusTypeId == ReviewStatusType.Review).FirstOrDefault().ReviewStatusId == ReviewStatu.Active;
        }
        /// <summary>
        /// Returns the current ApplicationReviewStage id.
        /// </summary>
        /// <returns>Current ApplicaiotnReviewStageId</returns>
        /// <remarks>
        ///     This assumes a ApplicationReviewStage exists.
        ///     TODO: This needs renamed! It's getting the current review status rather than stage
        /// </remarks>
        public int CurrentReviewStageId()
        {
            return this.ApplicationReviewStatus.AsQueryable().First(x => x.ReviewStatu.ReviewStatusTypeId == ReviewStatusType.Review).ApplicationReviewStatusId;
        }
        /// <summary>
        /// Returns the Client entity identifier 
        /// </summary>
        /// <returns>Client entity identifier</returns>
        public int ClientId()
        {
            return this.SessionPanel.ClientId();
        }
        /// <summary>
        /// Returns the review workflow step for this reviewer.
        /// </summary>
        /// <param name="reviewerUserId">reviewer user entity identifier</param>
        /// <remarks>
        ///   This method makes several assumptions:
        ///   - There must be a synchronous review stage
        ///   - there is only a single workflow for this user
        ///   - the workflow has at least one step
        /// </remarks>
        public ApplicationWorkflowStep ReviewWorkflowStepForThisReviewer(int reviewerUserId)
        {
            //
            // Since this is for the review, get the synchronous stage
            //
            ApplicationStage aps = this.SynchronousReviewStage();
            //
            // There we get the workflow for this reviewer.  There should be only one & we expect only one.
            //
            ApplicationWorkflow aw = aps.ApplicationWorkflows.First(x => x.PanelUserAssignmentId == reviewerUserId);
            //
            // And finally we get the steps
            //
            ApplicationWorkflowStep awfs = aw.ApplicationWorkflowSteps.First();

            return awfs;
        }
        /// <summary>
        /// Returns the user's PanelApplicationReviewerExpertise on this panel application if one exists.
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <returns>PanelApplicationReviewerExpertise for the specified user if one exists; null otherwise</returns>
        public PanelApplicationReviewerExpertise GetUsersExpertiseOnApplication(int panelUserAssignmentId)
        {
            return this.PanelApplicationReviewerExpertises.FirstOrDefault(x => x.PanelUserAssignmentId == panelUserAssignmentId);
        }
        /// <summary>
        /// Determines the current phase of this SessionPanel.
        /// </summary>
        /// <returns>Current Phase indicator</returns>
        /// <remarks>
        ///      This assumes that the calling sequence started with PanelApplication.CurrentPhase();
        ///      This assumes that the panel application has been released.
        ///      Unit tests not written
        /// </remarks>
        public PanelStageStep CurrentPhase()
        {
            return this.SessionPanel.CurrentPhase();
        }
        /// <summary>
        /// the current state of the critique.
        /// </summary>
        /// <param name="stepTypeId">The step type identifier.</param>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns>true if critique is submitted; otherwise false</returns>
        public bool CurrentCritiqueState(int stepTypeId, int panelUserAssignmentId)
        {
            //
            // First thing we need is the application stage for the reviews.  (Assuming here that the structures are set up)
            //
            var a = this.ApplicationStages.FirstOrDefault(x => x.ReviewStageId == ReviewStage.Asynchronous);
            //
            // Now that we have that we want a specific workflow identified for a specific user
            //
            var b = a.ApplicationWorkflows.Where(x => (x.PanelUserAssignmentId == panelUserAssignmentId)).
                //
                // Then we get the specific step
                //
                SelectMany(x => x.ApplicationWorkflowSteps).FirstOrDefault(x => (x.StepTypeId == stepTypeId));
            //
            // Can we always assume there there will be one? =>> need a test above to check that the user is assigned.  Otherwise there is none.
            //
            return b == null ? false : b.Resolution;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="panelUserAssignmentid"></param>
        /// <returns></returns>
        public IEnumerable<PanelApplication> ReviewersApplications(int panelUserAssignmentid)
        {
            IEnumerable<PanelApplication> result = this.SessionPanel.PanelApplications.SelectMany(x => x.PanelApplicationReviewerAssignments).
                //
                //
                //
                Where(x => (x.PanelUserAssignmentId == panelUserAssignmentid) && (!x.ClientAssignmentType.IsCoi)).
                //
                //
                //
                Select(x => x.PanelApplication);

            return result;
        }

        /// <summary>
        /// Gets the current application workflow step for a specified user.
        /// </summary>
        /// <remarks>Current is defined as the first incomplete step or if nothing is incomplete the last completed step</remarks>
        /// <returns>Current ApplicationWorkflowStep entity</returns>
        public ApplicationWorkflowStep GetCurrentApplicationWorkflowStep(int panelUserAssignmentId)
        {
            return this.GetCurrentApplicationStage()
                .ApplicationWorkflows.Where(x => x.PanelUserAssignmentId == panelUserAssignmentId)
                .DefaultIfEmpty(new ApplicationWorkflow())
                .First()
                .ApplicationWorkflowSteps
                .DefaultIfEmpty(new ApplicationWorkflowStep()) 
                .OrderBy(x => x.Resolution)
                .ThenBy(x => x.Resolution ? 0 : x.StepOrder)
                .ThenByDescending(x => x.Resolution ? x.StepOrder : 0)
                .First();
        }
        /// <summary>
        /// Gets the current application workflow step for a specified user.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier</param>
        /// <param name="currentPhase">The current phase</param>
        /// <remarks>Current is defined as the first incomplete step or if nothing is incomplete the last completed step</remarks>
        /// <returns>Current ApplicationWorkflowStep entity</returns>
        public ApplicationWorkflowStep GetCurrentApplicationWorkflowStep(int panelUserAssignmentId, int currentPhase)
        {
            return this.GetCurrentApplicationStage()
                .ApplicationWorkflows.Where(x => x.PanelUserAssignmentId == panelUserAssignmentId)
                .DefaultIfEmpty(new ApplicationWorkflow())
                .First()
                .ApplicationWorkflowSteps.Where(x => x.StepTypeId == currentPhase)
                .DefaultIfEmpty(new ApplicationWorkflowStep())
                .OrderBy(x => x.Resolution)
                .ThenBy(x => x.Resolution ? 0 : x.StepOrder)
                .ThenByDescending(x => x.Resolution ? x.StepOrder : 0)
                .First();
        }
        /// <summary>
        /// Gets the critique stage step
        /// </summary>
        /// <returns>The panel stage step for the current review stage</returns>
        public PanelStageStep CritiqueStageStep()
        {
            var currentReviewStage = this.GetCurrentReviewStage();

            var currentStageStep = this.SessionPanel.PanelStages.Where(x => x.ReviewStageId == currentReviewStage).FirstOrDefault().PanelStageSteps
                .Where(x => (x.StartDate <= GlobalProperties.P2rmisDateTimeNow && x.EndDate > GlobalProperties.P2rmisDateTimeNow) || (x.ReOpenDate <= GlobalProperties.P2rmisDateTimeNow && x.ReCloseDate > GlobalProperties.P2rmisDateTimeNow))
                .OrderByDescending(x => x.StepOrder).FirstOrDefault();

            return (currentStageStep) ?? this.SessionPanel.PanelStages.Where(x => x.ReviewStageId == currentReviewStage).FirstOrDefault().PanelStageSteps
                .OrderByDescending(x => x.StepOrder).FirstOrDefault();
        }

        /// <summary>
        /// Gets the type of the application workflow step for a specified step type id.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="stepTypeId">The step type identifier.</param>
        /// <returns>ApplicationWorkflowStep for the specified step</returns>
        public ApplicationWorkflowStep GetApplicationWorkflowStepForStepType(int panelUserAssignmentId, int stepTypeId)
        {
            return this.GetCurrentApplicationStage()
                .ApplicationWorkflows.Where(x => x.PanelUserAssignmentId == panelUserAssignmentId)
                .DefaultIfEmpty(new ApplicationWorkflow())
                .First()
                .ApplicationWorkflowSteps
                .Where(x => x.StepTypeId == stepTypeId)
                .DefaultIfEmpty(new ApplicationWorkflowStep())
                .First();
        }

        /// <summary>
        /// All the review workflow steps for a panel user.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns>Collection of ApplicationWorkflowSteps</returns>
        public IEnumerable<ApplicationWorkflowStep> AllReviewWorkflowStepsForPanelUser(int panelUserAssignmentId)
        {
            var result = this.ApplicationStages
                .Where(x => x.ReviewStageId == ReviewStage.Asynchronous || x.ReviewStageId == ReviewStage.Synchronous)
                .SelectMany(x => x.ApplicationWorkflows)
                .Where(x => x.PanelUserAssignmentId == panelUserAssignmentId)
                .SelectMany(x => x.ApplicationWorkflowSteps)
                .OrderBy(x => x.StepOrder);
            return result;
        }

        /// <summary>
        /// Retrieves the Asynchronous (review) ApplicationStage entity if one exists.
        /// </summary>
        /// <returns>ApplicationStage entity or null if none exists</returns>
        public ApplicationStage AsynchronousReviewStage()
        {
            return this.ApplicationStages.FirstOrDefault(x => x.ReviewStageId == ReviewStage.Asynchronous);
        }

        /// <summary>
        /// Gets all asynchronous workflow steps.
        /// </summary>
        /// <returns>Collection of application workflow step entities</returns>
        public IEnumerable<ApplicationWorkflowStep> GetAllAsyncWorkflowSteps()
        {
            return this.AsynchronousReviewStage().ApplicationWorkflows.SelectMany(x => x.ApplicationWorkflowSteps);
        }
        /// <summary>
        /// Gets all asynchronous panel steps.
        /// </summary>
        /// <returns>Collection of panel stage step entities</returns>
        public IEnumerable<PanelStageStep> GetAllAsyncPanelSteps()
        {
            return (this.SessionPanel)?.AsyncStage().PanelStageSteps;
        }
        /// <summary>
        /// Gets the synchronize panel step.
        /// </summary>
        /// <returns></returns>
        public PanelStageStep GetSyncPanelStep()
        {
            return (this.SessionPanel)?.SyncStage()?.PanelStageSteps.FirstOrDefault();
        }

        /// <summary>
        /// Gets the last step a specified user has submitted.
        /// </summary>
        /// <param name="panelUserAssignmentId">The user identifier for a particular panel.</param>
        /// <returns></returns>
        /// <remarks>Requires ApplicationStage, ApplicationReviewStatus, ApplicationWorkflow, ApplicationWorkflowStep entities</remarks>
        public int GetLastCompletedStepOrder(int panelUserAssignmentId)
        {

            return this.GetCurrentApplicationStage()
                .ApplicationWorkflows.Where(x => x.PanelUserAssignmentId == panelUserAssignmentId)
                .DefaultIfEmpty(new ApplicationWorkflow())
                .First()
                .ApplicationWorkflowSteps
                .Where(x => x.Resolution)
                //
                // Only pull the active workflow steps.  This prevents MOD phases from 
                // being pulled out if the MOD was not started.  As background for session panels
                // that are configured to have a MOD when the content is promoted the workflows steps
                // are marked as "resolved" but are marked as Active = false.  Then if MOD is started on 
                // the application the workflow step is marked as Active = true & Resolved = false.
                //
                .Where(x => x.Active)
                .OrderByDescending(x => x.StepOrder)
                .DefaultIfEmpty(new ApplicationWorkflowStep())
                .First().StepOrder;
        }
        /// <summary>
        /// Determines if the PanelApplication has a specific ReviewStatu value.
        /// </summary>
        /// <param name="reviewStatusId">ReviewStatu entity identifier</param>
        /// <returns>ReviewStatu entity if the PanelApplication has a the ReviewStatu value; null otherwise</returns>
        public ApplicationReviewStatu HasReviewStatus(int reviewStatusId)
        {
            return this.ApplicationReviewStatus.AsQueryable().FirstOrDefault(x => x.ReviewStatusId == reviewStatusId);
        }
        /// <summary>
        /// Indicates if the summary statement workflow has been started for this PanelApplication.
        /// </summary>
        /// <returns>True if the workflow has been started; false otherwise</returns>
        public ApplicationWorkflow GetSummaryWorkflow()
        {
            return this.ApplicationStages.FirstOrDefault(x => x.ReviewStageId == ReviewStage.Summary)?.ApplicationWorkflows.FirstOrDefault(x => (
                                                                    //
                                                                    // and is not complete
                                                                    //
                                                                    !(x.IsComplete())
                                                                    )
                                                                    );
        }
        /// <summary>
        /// Indicates if the PanelApplication ReviewStatu value has changed.
        /// </summary>
        /// <param name="reviewStatuId">ReviewStatu entity identifier</param>
        /// <param name="state">New state value</param>
        /// <returns>True if the ReviewStatu value has changed; false otherwise</returns>
        public bool HasReviewStatusChanged(int reviewStatuId, bool state)
        {
            //
            // find out if the status exists
            //
            bool reviewStatusExist = (HasReviewStatus(reviewStatuId) != null);

            return ( 
                       //
                       // status exists and new state is not exist
                       //
                       ((reviewStatusExist) && (!state)) ||
                       //
                       // status does not  exists and new state is exist
                       //
                       ((!reviewStatusExist) && (state))
                   )
                 ;
        }
        /// <summary>
        /// Determines if the MOD phase is active.
        /// </summary>
        /// <param name="panelStageStepEntityId">PanelStageStep entity identifier</param>
        /// <remarks>
        /// This method assumes that the parameter panelStageStepEntityId identifies a PanelStageStep
        /// entity that is a MOD.
        /// </remarks>
        /// <returns>True if the MOD is active; false otherwise</returns>
        public bool IsModActive(int panelStageStepEntityId)
        {
            //
            // For each of the application stages
            //
            ApplicationStageStep result = this.ApplicationStages.
                          //
                          // We gather the ApplicationStageSteps
                          //
                          SelectMany(x => x.ApplicationStageSteps).
                          //
                          // Where the PanelStageStepId equals a specific panel stage step Id.  
                          // There should be only 1.
                          //
                          FirstOrDefault(x => x.PanelStageStepId == panelStageStepEntityId);
            //
            // And finally we check if the discussion container contains any thing.  If it does 
            // then by definition we are an active discussion.
            //
            return (result != null) ? result.ApplicationStageStepDiscussions.Count() > 0: false;
        }
        /// <summary>
        /// Locates the PanelStageStep for the Final phase.
        /// </summary>
        /// <returns>PanelStageStep for the final phase; null if one does not exist</returns>
        public PanelStageStep LocateFinalPanelStageStep()
        {
            //
            // From the ApplicationStages
            //
            return this.ApplicationStages.
                        //
                        // Insure the MOD phase is active (for the phase) & is active for the application
                        //
                        Where(z => z.ActiveFlag && z.ReviewStageId == ReviewStage.Indexes.Asynchronous).
                        //
                        // Collect all the ApplicationStageSteps
                        //
                        SelectMany(z => z.ApplicationStageSteps).
                        //
                        // Then filter those by their PanelStageStep.  We only 
                        // want the one that is for the Final phase
                        //
                        FirstOrDefault(z => z.PanelStageStep.StepTypeId == StepType.Indexes.Final)?.
                        //
                        // And finally get the identified PanelStageStep which may
                        // or may not exist
                        //
                        PanelStageStep;
        }

        /// <summary>
        /// Gets a panel application reviewer assignment for the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>PanelApplicationReviewerAssignment entity object</returns>
        public PanelApplicationReviewerAssignment PanelApplicationReviewerAssignment(int userId)
        {
            return
                this.PanelApplicationReviewerAssignments.Where(x => x.PanelUserAssignment.UserId == userId)
                    .DefaultIfEmpty(new PanelApplicationReviewerAssignment())
                    .First();
        }
        /// <summary>
        /// Retrieves the PanelUserAssignment entity for the specified userId
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>PanelUserAssignment entity</returns>
        public PanelUserAssignment GetPanelUserAssignmentId(int userId)
        {
            return this.SessionPanel.PanelUserAssignment(userId);
        }

        /// <summary>
        /// Critiques the exists for specific reviewer.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns>True if critique exists for the indicated user assignment; otherwise false</returns>
        public bool CritiqueExistsForSpecificReviewer(int panelUserAssignmentId)
        {
            return this.AsynchronousReviewStage().ApplicationWorkflows.Any(x => x.PanelUserAssignmentId == panelUserAssignmentId);
        }
        /// <summary>
        /// Sets the panel application.
        /// </summary>
        /// <param name="panelId">The panel identifier.</param>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void SetPanelApplication(int panelId, int applicationId, int userId)
        {
            this.SessionPanelId = panelId;
            this.ApplicationId = applicationId;

            Helper.UpdateCreatedFields(this, userId);
            Helper.UpdateModifiedFields(this, userId);
        }
        /// <summary>
        /// Determines whether [has summary text]. Checks if Summary Statement exists/started. 
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has summary text]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasSummaryText()
        {
            return this.ApplicationStages.Where(x => x.ReviewStageId == ReviewStage.Summary).Count() >= MinimumNumberSummaryWorkflows;
        }
 
        /// <summary>
        /// Determines whether [has overview text]. This method is used to check for the Application Overview text
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has overview text]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasOverviewText()
        {
            return this.PanelApplicationSummaries.Any(x => x.PanelApplicationId == this.PanelApplicationId);    
                
        }

        /// <summary>
        /// Determines whether [has user application comments].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has user application comments]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasUserApplicationComments()
        {
            return this.UserApplicationComments.Count(x => x.CommentTypeID != LookupCommentType.SummaryNoteTypeId) > 0;
        }
    }
}
