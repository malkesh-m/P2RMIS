using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.SummaryStatement;
using Entity = Sra.P2rmis.Dal;
using Sra.P2rmis.Bll.ModelBuilders.ApplicationScoring;
using Sra.P2rmis.Dal;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// ApplicationScoringService provides services to perform business related functions for
    /// the ApplicationScoring Application.
    /// </summary>
    public partial class ApplicationScoringService: ServerBase, IApplicationScoringService
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationScoringService()
        {
            UnitOfWork = new Entity.UnitOfWork();
        }
        #endregion
        /// <summary>
        /// Retrieve the information to populate the Reviewer "Ready to Review" application scoring grid.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of IReviewerApplicationScoring from the identified SessionPanel</returns>
        public Container<IReviewerApplicationScoring> ListReviewerReadyForReview(int sessionPanelId, int userId)
        {
            string name = FullName(nameof(ApplicationScoringService), nameof(ListReviewerReadyForReview));
            ValidateInt(sessionPanelId, name, nameof(sessionPanelId));
            ValidateInt(userId, name, nameof(userId));
            //
            // Retrieve the SessionPanel entity.  All the data is determined from it's contents
            //
            Entity.SessionPanel sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);

            return ListReviewerReadyForReview(sessionPanelEntity, userId);
        }
        /// <summary>
        /// Gets active or scoring application
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier</param>
        /// <returns></returns>
        public KeyValuePair<int, string> GetActiveOrScoringApplication(int sessionPanelId)
        {
            //
            // Create the model & populate it
            //
            var appWithStatus = new KeyValuePair<int, string>();
            //
            // Get active or scoring PanelApplication with app id and status
            //
            var panelApplication = UnitOfWork.PanelApplicationRepository.Select()
                .Where(x => x.SessionPanelId == sessionPanelId
                    && x.ApplicationReviewStatus.Any(y => ReviewStatu.ActiveScoringStatuses.Contains(y.ReviewStatusId)))
                .Select(x => new 
                {
                    x.ApplicationId,
                    x.ApplicationReviewStatus.FirstOrDefault(y => y.ReviewStatu.ReviewStatusTypeId == ReviewStatusType.Review).ReviewStatu.ReviewStatusLabel
                }).FirstOrDefault();
            if (panelApplication != null)
                appWithStatus = new KeyValuePair<int, string>(panelApplication.ApplicationId, panelApplication.ReviewStatusLabel);
            return appWithStatus;
        }

        /// <summary>
        /// Populate the model to return data for the reviewer Application Scoring 
        /// </summary>
        /// <param name="sessionPanelEntity">SessionPanel entity</param>
        /// <param name="userId">UserId of user making the call</param>
        /// <returns>Populate model</returns>
        internal virtual Container<IReviewerApplicationScoring> ListReviewerReadyForReview(Entity.SessionPanel sessionPanelEntity, int userId)
        {
            Container<IReviewerApplicationScoring> result = new Container<IReviewerApplicationScoring>();
            List<IReviewerApplicationScoring> list = new List<IReviewerApplicationScoring>();
            result.ModelList = list;
            bool isChairPerson = sessionPanelEntity.PanelUserAssignment(userId).IsChair();
            bool isRestrictedToOnlyAssignedApps = sessionPanelEntity.PanelUserAssignment(userId).RestrictedAssignedFlag;
            var panelApplications = UnitOfWork.PanelApplicationRepository.GetPanelApplicationsForScoring(sessionPanelEntity.SessionPanelId);
            
            //if the reviewer is restricted, we only want to include apps they are assigned to
            foreach (var panelApplication in panelApplications.Where(x => !isRestrictedToOnlyAssignedApps || x.PanelApplicationReviewerAssignment(userId)?.ClientAssignmentType != null))
            {
                //
                //  Check to make sure this user has an assignment on this panel.
                //
                var panelApplicationReviewerEntity = panelApplication.UsersAssignmentType(userId);
                //
                // For convenience define these locals
                //
                var applicationEntity = panelApplication.Application;
                var applicationPersonnelEntity = applicationEntity.PrimaryInvestigator();
                //
                // Create the model & populate it
                //
                var model = new ReviewerApplicationScoring();
                //
                // Determine the state of the MOD phase (if there is one)
                //
                Entity.PanelStageStep panelStageStepEntity = panelApplication.LocateFinalPanelStageStep(); 
                bool modIsActive = IsModActive(panelApplication, panelStageStepEntity);
                bool panelStageStepIsOpen = IsStageStepOpen(panelStageStepEntity);
                Entity.ApplicationStageStep applicationStageStepEntity = panelStageStepEntity?.RetrieveApplicationStageStep(panelApplication.PanelApplicationId);

                model.Populate(panelApplication.ReviewOrder, DetermineAppAssign(panelApplicationReviewerEntity),
                                applicationEntity.LogNumber, applicationEntity.ApplicationTitle,
                                applicationPersonnelEntity.FirstName, applicationPersonnelEntity.LastName,
                                applicationEntity.ProgramMechanism.ClientAwardType.AwardAbbreviation, panelApplication.ReviewStatusLabel(), //panelApplication.DetermineApplicationStatus(), 
                                panelApplication.Application.ApplicationId, panelApplication.PanelApplicationId, 
                                sessionPanelEntity.SessionPanelId,
                                applicationEntity.ProgramMechanism.BlindedFlag,
                                Entity.PanelStageStep.IsModPhase(panelStageStepEntity), 
                                Entity.PanelStageStep.IsModReady(panelStageStepEntity, panelStageStepIsOpen, modIsActive), 
                                Entity.PanelStageStep.IsModActive(panelStageStepEntity, panelStageStepIsOpen, modIsActive), 
                                Entity.PanelStageStep.IsModClosed(panelStageStepEntity, panelStageStepIsOpen),
                                DetermineApplicationStageStepId(applicationStageStepEntity),
                                applicationStageStepEntity?.RetrieveDiscussion()?.ApplicationStageStepDiscussionId, modIsActive, isChairPerson); 
                //
                // Now take care of the application properties.
                //
                bool hasApplicationComments = panelApplication.HasUserApplicationComments();
                model.PopulateActions(DetermineReviewerFlag(panelApplicationReviewerEntity),
                                DetermineCoi(panelApplicationReviewerEntity),
                                !sessionPanelEntity.IsActive(),
                                hasApplicationComments,
                                panelApplicationReviewerEntity != null && panelApplicationReviewerEntity.IsPreMeetingCritiqueSubmitted(),
                                panelApplication.IsCurrentlyScoring(),
                                panelApplication.IsCurrentDiscussion()
                                );
                list.Add(model);

            }
            result.ModelList = result.ModelList.OrderBy(x => x.ReviewOrder ?? 99).ThenBy(o => o.ApplicationLogNumber);
            return result;
        }
        /// <summary>
        /// Determines the ApplicationStageStep entity identifier
        /// </summary>
        /// <param name="applicationStageStepEntity">ApplicationStageStep entity identifier</param>
        /// <returns>ApplicationStageStep entity identifier; 0 if no ApplicationStageStep exists</returns>
        internal int DetermineApplicationStageStepId(Entity.ApplicationStageStep applicationStageStepEntity)
        {
            return (applicationStageStepEntity != null) ? applicationStageStepEntity.ApplicationStageStepId : 0;
        }
        /// <summary>
        /// List the panel application's critiques.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">Current user entity identifier</param>
        /// <returns>Container of IReviewerCritiques</returns>
        public Container<ReviewerScores> ListApplicationScores(int panelApplicationId, int userId)
        {
            ValidateInt(panelApplicationId, "ApplicationScoringService.ListApplicationScores", "panelApplicationId");
            ValidateInt(userId, "ApplicationScoringService.ListApplicationScores", "userId");
            Container<ReviewerScores> container = new Container<ReviewerScores>();
            List<ReviewerScores> list = new List<ReviewerScores>();
            //
            // Get the application workflow step entity
            // 
            var applicationWorkflowStepEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId)
                .ApplicationStages.FirstOrDefault(x => x.ReviewStageId == Entity.ReviewStage.Synchronous)
                .ApplicationWorkflows.FirstOrDefault(x => x.PanelUserAssignment.UserId == userId)
                .ApplicationWorkflowSteps.FirstOrDefault();

            foreach (var applicationWorkflowStepElement in applicationWorkflowStepEntity.ApplicationWorkflowStepElements.Where(x => x.ApplicationTemplateElement.MechanismTemplateElement.ScoreFlag))
            {
                //
                // Retrieve the list of application workflow step element entities
                //
                var appWorkflowStepElementEntity = UnitOfWork.ApplicationWorkflowStepElementRepository.GetByID(applicationWorkflowStepElement.ApplicationWorkflowStepElementId);
                //
                // Populate the ReviewerScores object off of the ApplicationWorkflowStepElementEntity object
                //
                var model = new ReviewerScores();
                model.Populate(appWorkflowStepElementEntity.GetCriterionScoringLegend(), appWorkflowStepElementEntity.ClientScoringScale.ClientScoringScaleLegendId != null ? appWorkflowStepElementEntity.ClientScoringScale.ClientScoringScaleLegend.LegendName : String.Empty,
                    appWorkflowStepElementEntity.ApplicationTemplateElement.MechanismTemplateElement.OverallFlag, appWorkflowStepElementEntity.ClientScoringScale.LowValue,
                    appWorkflowStepElementEntity.ClientScoringScale.HighValue, appWorkflowStepElementEntity.ApplicationWorkflowStepElementContents.DefaultIfEmpty(new Entity.ApplicationWorkflowStepElementContent()).First().Score,
                    appWorkflowStepElementEntity.ApplicationWorkflowStepElementContents.DefaultIfEmpty(new Entity.ApplicationWorkflowStepElementContent()).First().Abstain,
                    appWorkflowStepElementEntity.ClientScoringScale.ScoreType, appWorkflowStepElementEntity.ApplicationWorkflowStepElementId,
                    appWorkflowStepElementEntity.ApplicationTemplateElement.MechanismTemplateElement.ClientElement.ElementDescription, appWorkflowStepElementEntity.GetAdjectivalScoringScale(), appWorkflowStepElementEntity.ApplicationTemplateElement.MechanismTemplateElement.SortOrder);
                list.Add(model);
            }
            
            container.ModelList = list.OrderBy(x => x.SortOrder);
            return container;
        }
        /// <summary>
        /// Get application status
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>The status in string format</returns>
        public string GetApplicationStatus(int panelApplicationId)
        {

            ValidateInt(panelApplicationId, "ApplicationScoringService.GetApplicationStatus", "panelApplicationId");
            ///
            /// Get the panel application
            /// 
            var panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            string status = panelApplicationEntity.DetermineApplicationStatus();
            return status;
        }
        /// <summary>
        /// Save or update a reviewer's critique.
        /// </summary>
        /// <param name="applicationWorkflowStepElemenContentId">ApplicationWorkflowStepElementContent entity identifier</param>
        /// <param name="contentText">Critique text</param>
        /// <param name="userId">User entity identifier</param>
        public void SaveReviewersCritique(int applicationWorkflowStepElemenContentId, string contentText, int userId)
        {
            ValidateSaveReviewersCritique(applicationWorkflowStepElemenContentId, userId);

            ApplicationWorkflowStepElementContentServiceAction editAction = new ApplicationWorkflowStepElementContentServiceAction();
            editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationWorkflowStepElementContentRepository, ServiceAction<Entity.ApplicationWorkflowStepElementContent>.DoUpdate, applicationWorkflowStepElemenContentId, userId);
            editAction.Populate(contentText);
            editAction.Execute();
        }
        /// <summary>
        /// Add a reviewers comment
        /// </summary>
        /// <param name="applicationId">Application entity identifier</param>
        /// <param name="comment">Application comment</param>
        /// <param name="userId">User entity identifier</param>
        /// <remarks>TODO: consolidate with ApplicationManagementService.AddComment (add type as parameter)</remarks>
        public void AddComment(int panelApplicationId, string comment,int userId)
        {
            ValidateSaveCommentParameters(panelApplicationId, userId);
            //
            // Now save it.
            //
            UserApplicationCommentServiceAction editAction = new UserApplicationCommentServiceAction();
            editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.UserApplicationCommentRepository, ServiceAction<Entity.UserApplicationComment>.DoUpdate, 0, userId);
            editAction.Populate(panelApplicationId, comment, Entity.CommentType.Indexes.ReviewerNote);
            editAction.Execute();
        }
        /// <summary>
        /// Save or update a reviewers comment
        /// </summary>
        /// <param name="commentId">Comment entity identifier</param>
        /// <param name="comment">Application comment</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>True if the update was successful; false otherwise.</returns>
        /// <remarks>
        /// The only reason for Edit to return false is if the a user other than the original comment owner 
        /// was trying to edit the comment.
        /// </remarks>
        public bool EditComment(int commentId, string comment, int userId)
        {
            ValidateEditCommentParameters(commentId, userId);
            //
            // Now save it.
            //
            UserApplicationCommentServiceAction editAction = new UserApplicationCommentServiceAction();
            editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.UserApplicationCommentRepository, ServiceAction<Entity.UserApplicationComment>.DoUpdate, commentId, userId);
            editAction.Populate(comment);
            editAction.Execute();

            return editAction.WasActionValid;
        }
        /// <summary>
        /// Delete a reviewers comment
        /// </summary>
        /// <param name="commentId">Comment entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        public void DeleteComment(int commentId, int userId)
        {
            ValidateDeleteCommentParameters(commentId, userId);
            //
            // Now save it.
            //
            UserApplicationCommentServiceAction editAction = new UserApplicationCommentServiceAction();
            editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.UserApplicationCommentRepository, ServiceAction<Entity.UserApplicationComment>.DoUpdate, commentId, userId);
            editAction.Execute();
        }
        /// <summary>
        /// Retrieves user application comments for a specified application.
        /// </summary>
        /// <param name="panelApplicationId">Identifier for a Panel Application</param>
        /// <returns>Zero or more comments related to a panel application</returns>
        public Container<ISummaryCommentModel> GetApplicationComments(int panelApplicationId)
        {
            this.ValidateInteger(panelApplicationId, "ApplicationScoringService.GetApplicationComments", "panelApplicationId");
            ///
            /// Set up default return
            /// 
            Container<ISummaryCommentModel> container = new Container<ISummaryCommentModel>();
            //
            // Call the DL and retrieve the summary comments for this application
            //
            var results = UnitOfWork.ApplicationScoringRepository.GetApplicationComments(panelApplicationId, Entity.CommentType.Indexes.ReviewerNote);
            //
            // Then create the view to return to the PI layer & return
            //
            container.SetModelList(results);

            return container;
        }
        /// <summary>
        /// Save a reviewer's score
        /// </summary>
        /// <param name="reviewerScores">Collection of reviewer scores</param>
        /// <param name="overallScores">Collection of reviewers overall scores (there will be only one)</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier for the application being scored</param>
        /// <param name="userId">User entity identifier</param>
        /// <param name="canUserEditAnyScoreCard">Whether the user can edit another user's final score card</param>
        public void SaveScore(List<ReviewerScores> reviewerScores, List<ReviewerScores> overallScores, int panelApplicationId, int userId, bool canUserEditAnyScoreCard)
        {
            ValidateSaveScoreParameters(reviewerScores, panelApplicationId, userId);
            //
            // First find out if we score the application
            //
            Entity.PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            //
            // A user who has adjust scorecard permission can enter a reviewers scores on their behalf regardless or scoring status 
            //
            if (canUserEditAnyScoreCard || panelApplicationEntity.IsCurrentlyScoring())
            {
                //
                // First we do the criterion scores
                //
                ScoringServiceAction editAction = new ScoringServiceAction();
                if (reviewerScores != null)
                {
                    foreach (var reviewerScore in reviewerScores)
                    {
                        editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationWorkflowStepElementContentRepository, this.UnitOfWork.ApplicationWorkflowStepElementRepository, ServiceAction<Entity.ApplicationWorkflowStepElementContent>.DoNotUpdate, userId, reviewerScore);
                        editAction.Execute();
                    }
                }
                //
                // Now the overall score
                //
                ReviewerScores overallScore = overallScores?.First();

                editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationWorkflowStepElementContentRepository, this.UnitOfWork.ApplicationWorkflowStepElementRepository, ServiceAction<Entity.ApplicationWorkflowStepElementContent>.DoNotUpdate, userId, overallScore);
                editAction.Execute();
                //
                // We manually call the save since we are updating multiple records at once.
                //
                UnitOfWork.Save();
                ///
                /// Now we update the cache with the reviewers scores & overall score.
                /// 
                Entity.PanelUserAssignment panelUserAssignmentEntity = panelApplicationEntity.GetPanelUserAssignmentId(userId);
                UpdateCache(reviewerScores, overallScores, panelApplicationEntity.ApplicationId, panelApplicationEntity.SessionPanelId, panelUserAssignmentEntity.PanelUserAssignmentId);
            }
            else
                throw new ScoringException();
        }
        /// <summary>
        /// Checks whether the user can access their panel assignment due to having incomplete assignments
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sessionPanelId"></param>
        /// <returns>True if access is permitted; otherwise false;</returns>
        public bool CanUserAccessPanelAssignment(int userId, int sessionPanelId)
        {
            var panelUserAssignment = UnitOfWork.PanelUserAssignmentRepository.Get(x => x.SessionPanelId == sessionPanelId && x.UserId == userId).DefaultIfEmpty(new Entity.PanelUserAssignment()).First();
            return !panelUserAssignment.IsRegistrationIncomplete();
        }
        /// <summary>
        /// Updates the cache with the score
        /// </summary>
        /// <param name="reviewerScores">The reviewer's score</param>
        /// <param name="overallScores"></param>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="sessionPanelId"></param>
        /// <param name="key">Key value used to identify the reviewer on the client</param>
        internal virtual void UpdateCache(List<ReviewerScores> reviewerScores, List<ReviewerScores> overallScores, int applicationId, int sessionPanelId, int key)
        {
            //
            // Get the cache entry for this reviewer's score & reset the list
            //
            ScoreCacheEntry sce = OnLineScoringCacheService.GetOrCreateEntry(applicationId, sessionPanelId, key);
            sce.Reset();

            reviewerScores?.ForEach(r => sce.Scores.Add(r.Score));
            overallScores?.ForEach(r => sce.OverallScore = r.Score);
        }
        /// <summary>
        /// Retrieves any scores that have changed since the last polling request.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        public List<ScoreCacheEntry> PollScore(int applicationId)
        {
            List<ScoreCacheEntry> result = new List<ScoreCacheEntry>();
            //
            // Get the SessionPanelId (this was a retro fit, so it looks odd)
            //
            Entity.Application applicationEntity = UnitOfWork.ApplicationRepository.GetByID(applicationId);
            Entity.PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(applicationEntity.PanelApplicationId());

            if (OnLineScoringCacheService.IsCurrentApplication(applicationId, panelApplicationEntity.SessionPanelId))
            {
                //
                // Query the cache for entries for this application
                //
                var listOfkeys = OnLineScoringCacheService.ScoreCacheKeyList(panelApplicationEntity.SessionPanelId);
                var cacheEntries = OnLineScoringCacheService.GetValues(listOfkeys);
                //
                // If the entry is updated, added it to the list of values to return then
                // mark it as not updated.
                //
                foreach (ScoreCacheEntry cacheEntry in cacheEntries.Values)
                {
                    result.Add(cacheEntry);
                }
            }

            return result;
        }
        /// <summary>
        /// Retrieves the pre-meeting scores for the indicated applicication, session panel and reviewer.
        /// </summary>
        /// <param name="applicationId">Identifier for an application's instance of a workflow</param>
        /// <param name="sessionPanelId">Identifier for an application's instance of a workflow</param>
        /// <param name="reviewerId">Identifier for an application's instance of a workflow</param>
        /// <returns>Zero or more transactions of an application workflow</returns>
        public ReviewerApplicationPremeetingScoresModel GetApplicationPreMtgScoresForReviewer(int panelApplicationId)
        {
            this.ValidateInteger(panelApplicationId, "ApplicationScoringService.GetApplicationPreMtgScoresForReviewer", "panelApplicationId");

            ///
            /// Set up default return
            /// 
            ReviewerApplicationPremeetingScoresModel result = new ReviewerApplicationPremeetingScoresModel();

            var reviewers = UnitOfWork.ApplicationScoringRepository.GetPreMtgReviewers(panelApplicationId);
            var criteria = UnitOfWork.ApplicationScoringRepository.GetPreMtgCriteria(panelApplicationId);
            result.Reviewers = new List<IPreMeetingReviewerModel>(reviewers.ModelList);
            result.Criteria = new List<IPreMeetingCriteriaModel>(criteria.ModelList);
            return result;
        }
        /// <summary>
        /// Gets the critique stage step
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>The edit critique phase model</returns>
        public EditCritiquePhaseModel GetCritiqueStageStep(int panelApplicationId)
        {
            Entity.PanelApplication panelApplication = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            Entity.PanelStageStep step = panelApplication.CritiqueStageStep();

            EditCritiquePhaseModel model = new EditCritiquePhaseModel(step.PanelStageStepId, step.StepName, step.EndDate);

            return model;
        }
        /// <summary>
        /// Gets the critique review order
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>List of critique review ordered model objects</returns>
        public List<ICritiqueReviewerOrderedModel> GetCritiqueReviewerOrder(int panelApplicationId)
        {
            //
            // Set up default return
            // 
            List<ICritiqueReviewerOrderedModel> result = new List<ICritiqueReviewerOrderedModel>();

            Entity.PanelApplication panelApplication = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);

            result = panelApplication.GetCritiqueReviewerOrder().ToList();

            return result;
        }
        #region Status changes
        /// <summary>
        /// Deactivate an application
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        public void Deactivate(int panelApplicationId, int userId)
        {
            ValidateInt(panelApplicationId, "ApplicationScoringService.Deactivate", "panelApplicationId");
            ValidateInt(userId, "ApplicationScoringService.Deactivate", "userId");

            Entity.PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);

            int newReviewStatusId = DeactivateStatus(panelApplicationEntity);

            ApplicationReviewStatusesServiceAction editAction = new ApplicationReviewStatusesServiceAction();
            editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationReviewStatuGenericRepository, ServiceAction<Entity.ApplicationReviewStatu>.DoUpdate, panelApplicationEntity.CurrentReviewStageId(), userId);
            editAction.Populate(newReviewStatusId);
            editAction.Execute();
            //
            // Since we have successfully change the database we can now trash the cache
            //
            ClearCacheForThisApplication(panelApplicationEntity.ApplicationId, panelApplicationEntity.SessionPanelId);
        }
        /// <summary>
        /// Checks if the application identified by the entity identifier is contained in the 
        /// cache.  If the application is contained in the cache, the cache is invalidated.
        /// </summary>
        /// <param name="applicationId">Application entity identifier</param>
        /// <param name="sessionPanelId"></param>
        internal virtual void ClearCacheForThisApplication(int applicationId, int sessionPanelId)
        {
            if (OnLineScoringCacheService.IsCurrentApplication(applicationId, sessionPanelId))
            {
                OnLineScoringCacheService.InvalidateCache(sessionPanelId, applicationId);
            }
        }
        /// <summary>
        /// Determine the new ReviewStatus for the Deactivate command
        /// </summary>
        /// <param name="panelApplicationEntity">PanelApplication entity identifier</param>
        /// <returns>Deactivate status</returns>
        internal int DeactivateStatus(Entity.PanelApplication panelApplicationEntity)
        {
            //
            // By definition the next Review Status will be scored
            //
            int result = Entity.ReviewStatu.Scored;
            //
            // If the application is Active (Review status of Active or Scoring) & it does not have scores
            // reset the state back to Full Review.  
            //
            if (panelApplicationEntity.IsActiveStatus())
            {
                Entity.ApplicationStage applicationStageEntity = panelApplicationEntity.SynchronousReviewStage();
                if (!panelApplicationEntity.IsStageScored(applicationStageEntity))
                {
                    result = Entity.ReviewStatu.FullReview;
                }
            }

            return result;
        }
        /// <summary>
        /// Change the applications status to ready to score
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        public void ReadyToScore(int panelApplicationId, int userId)
        {
            ValidateInt(panelApplicationId, "ApplicationScoringService.ReadyToScore", "panelApplicationId");
            ValidateInt(userId, "ApplicationScoringService.ReadyToScore", "userId");

            ChangeStatus(panelApplicationId, Entity.ReviewStatu.ReadyToScore, userId);
        }
        /// <summary>
        /// Change the applications status to active
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        public void Active(int panelApplicationId, int userId)
        {
            ValidateInt(panelApplicationId, "ApplicationScoringService.ReadyToScore", "panelApplicationId");
            ValidateInt(userId, "ApplicationScoringService.ReadyToScore", "userId");
            ValidateNoPanelActive(panelApplicationId);

            ChangeStatus(panelApplicationId, Entity.ReviewStatu.Active, userId);
            InitializeCache(panelApplicationId);
        }
        /// <summary>
        /// Initialize the cache when the application is started.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        internal virtual void InitializeCache(int panelApplicationId)
        {
            Entity.PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            OnLineScoringCacheService.InitializeCache(panelApplicationEntity.ApplicationId, panelApplicationEntity.SessionPanelId);
        }
        /// <summary>
        /// Change the applications status to triage
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        public void Triage(int panelApplicationId, int userId)
        {
            ValidateInt(panelApplicationId, "ApplicationScoringService.Triage", "panelApplicationId");
            ValidateInt(userId, "ApplicationScoringService.Triage", "userId");

            ChangeStatus(panelApplicationId, Entity.ReviewStatu.Triaged, userId);
        }
        /// <summary>
        /// Change the applications status to Disapproved
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        public void Disapproved(int panelApplicationId, int userId)
        {
            ValidateInt(panelApplicationId, "ApplicationScoringService.Disapproved", "panelApplicationId");
            ValidateInt(userId, "ApplicationScoringService.Disapproved", "userId");

            ChangeStatus(panelApplicationId, Entity.ReviewStatu.Disapproved, userId);
        }
        /// <summary>
        /// Change the applications status to Scoring
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        public void Scoring(int panelApplicationId, int userId)
        {
            ValidateInt(panelApplicationId, "ApplicationScoringService.Scoring", "panelApplicationId");
            ValidateInt(userId, "ApplicationScoringService.Scoring", "userId");

            ChangeStatusWithoutClearingCache(panelApplicationId, Entity.ReviewStatu.Scoring, userId);
            //
            // Now start the workflow.
            //
            UnitOfWork.ApplicationScoringRepository.BeginScoringWorkflow(panelApplicationId, userId);
        }
        /// <summary>
        /// Change the applications status without clearing cache
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="newStatus">New status value</param>
        /// <param name="userId">User entity identifier</param>
        internal virtual void ChangeStatusWithoutClearingCache(int panelApplicationId, int newStatus, int userId)
        {
            Entity.PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            ChangeStatusWithoutClearingCache(panelApplicationEntity, newStatus, userId);
        }
        /// <summary>
        /// Change the applications status without clearing cache
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="newStatus">New status value</param>
        /// <param name="userId">User entity identifier</param>
        internal virtual void ChangeStatusWithoutClearingCache(Entity.PanelApplication panelApplicationEntity, int newStatus, int userId)
        {
            ApplicationReviewStatusesServiceAction editAction = new ApplicationReviewStatusesServiceAction();
            editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationReviewStatuGenericRepository, ServiceAction<Entity.ApplicationReviewStatu>.DoUpdate, panelApplicationEntity.CurrentReviewStageId(), userId);
            editAction.Populate(newStatus);
            editAction.Execute();
        }
        /// <summary>
        /// Change the applications status
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="newStatus">New status value</param>
        /// <param name="userId">User entity identifier</param>
        internal virtual void ChangeStatus(int panelApplicationId, int newStatus, int userId)
        {
            Entity.PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            ChangeStatusWithoutClearingCache(panelApplicationEntity, newStatus, userId);
            //
            // Since we have successfully change the database & the application has finished scoring we can now trash the cache
            //
            ClearCacheForThisApplication(panelApplicationEntity.ApplicationId, panelApplicationEntity.SessionPanelId);
        }
        /// <summary>
        /// Determines if any application is actively scoring for the session of the supplied panel.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <returns>If an application is scoring the log number is returned; null otherwise</returns>
        public ActiveApplicationModel IsAnyApplicationBeingScored(int panelApplicationId)
        {
            ValidateInteger(panelApplicationId, "ApplicationScoringService.IsAnyApplicationBeingScored", "panelApplicationId");

            Entity.PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            //
            // This returns a specific session panel (re: meeting)
            //
            var result = panelApplicationEntity.SessionPanel.
            //
            // Now for all the panels in the session
            //
            PanelApplications.
            //
            // Locate the ones that are not active (status of "Active" or "Scoring".
            //
            SelectMany(x => x.ApplicationReviewStatus.Where(y => (y.ReviewStatusId == Entity.ReviewStatu.Active) || (y.ReviewStatusId == Entity.ReviewStatu.Scoring))).
            //
            // And then we create something to return.
            //
            Select(x => new ActiveApplicationModel(x.PanelApplicationId, x.PanelApplication.Application.ApplicationTitle, x.PanelApplication.Application.LogNumber));

            return result.FirstOrDefault();
        }
        /// <summary>
        /// Determines if any application is actively scoring for the session.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <returns>If an application is scoring the log number is returned; null otherwise</returns>
        public ActiveApplicationModel IsAnyApplicationBeingScoredByPanel(int sessionPanelId)
        {
            Entity.SessionPanel sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            //
            // This returns a specific session panel (re: meeting)
            //
            var result = sessionPanelEntity.
                //
                // Now for all the panels in the session
                //
            PanelApplications.
                //
                // Locate the ones that are not active (status of "Active" or "Scoring".
                //
            SelectMany(x => x.ApplicationReviewStatus.Where(y => (y.ReviewStatusId == Entity.ReviewStatu.Active) || (y.ReviewStatusId == Entity.ReviewStatu.Scoring))).
                //
                // And then we create something to return.
                //
            Select(x => new ActiveApplicationModel(x.PanelApplicationId, x.PanelApplication.Application.ApplicationTitle, x.PanelApplication.Application.LogNumber));

            return result.FirstOrDefault();
        }
        /// <summary>
        /// Abstain all criterion for the reviewer's critique.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="reviewerUserId">User entity identifier of the reviewer being abstained.</param>
        /// <param name="userId">User entity identifier</param>
        public void MarkReviewierScoresAsAbstain(int panelApplicationId, int reviewerUserId, int userId)
        {
            ValidateMarkReviewierScoresAsAbstainParameters(panelApplicationId, reviewerUserId, userId);
            //
            // Get the review workflow step
            //
            Entity.PanelApplication pa = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            Entity.ApplicationWorkflowStep awfs = pa.ReviewWorkflowStepForThisReviewer(reviewerUserId);
            //
            // Let's create the action to manipulate the entity
            //
            ApplicationWorkflowStepElementContentServiceActionPostAssignment editAction = new ApplicationWorkflowStepElementContentServiceActionPostAssignment();
            CreateAbstainedApplicationWorkflowStepElementContentServiceAction createAction = new CreateAbstainedApplicationWorkflowStepElementContentServiceAction();
            //
            // Now it is just as simple as iterating through the ApplicationWorkflowStepElements & their ApplicationWorkflowStepElementContents
            //
            foreach (var awfse in awfs.ApplicationWorkflowStepElements)
            {
                Entity.ApplicationWorkflowStepElementContent awsec = awfse.ApplicationWorkflowStepElementContents.FirstOrDefault();
                if (awsec != null)
                {
                    //
                    // Since there is an existing content all we need to do is abstain it.
                    //
                    AbstainExistingApplicationWorkflowStepElementContent(editAction, awsec, userId);
                }
                else
                {
                    //
                    // Since there is no content for this ApplicationWorkflowStepElement we need to create it and
                    // might as well set it's value to be abstained while we are at it.
                    // 
                    CreateAbstainedApplicationWorkflowStepElementContents(createAction, awfse.ApplicationWorkflowStepElementId, userId);
                }
            }
            //
            // Now that we have done all that we just commit everything
            //
            UnitOfWork.Save();
        }
        /// <summary>
        /// Modifies an existing ApplicationWorkflowStepElementContent for the ApplicationWorkflowStepElement which
        /// has "abstained" from scoring.
        /// </summary>
        /// <param name="action">Service action to process the modify of an existing content</param>
        /// <param name="awsec">ApplicationWorkflowStepElementContent entity</param>
        /// <param name="userId">User entity identifier</param>
        internal virtual void AbstainExistingApplicationWorkflowStepElementContent(ApplicationWorkflowStepElementContentServiceActionPostAssignment action, Entity.ApplicationWorkflowStepElementContent awsec, int userId)
        {
            const bool doNotReviseContent = false;

            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationWorkflowStepElementContentRepository, ServiceAction<ApplicationWorkflowStepElementContent>.DoNotUpdate, awsec.ApplicationWorkflowStepElementContentId, userId);
            action.Populate(this.UnitOfWork.ApplicationWorkflowStepElementRepository, awsec.ContentText, null, true, awsec.ApplicationWorkflowStepElementId, doNotReviseContent);
            action.Execute();
        }
        /// <summary>
        /// Creates an ApplicationWorkflowStepElementContent for the ApplicationWorkflowStepElement which
        /// has "abstained" from scoring.
        /// </summary>
        /// <param name="action">Service action to process the create</param>
        /// <param name="applicationWorkflowStepElementEntityId">ApplicationWorkflowStepElement entity identifier of parent container</param>
        /// <param name="userId">User entity identifier of creating user</param>
        internal virtual void CreateAbstainedApplicationWorkflowStepElementContents(CreateAbstainedApplicationWorkflowStepElementContentServiceAction action, int applicationWorkflowStepElementEntityId, int userId)
        {
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationWorkflowStepElementContentRepository, ServiceAction<ApplicationWorkflowStepElementContent>.DoNotUpdate, userId);
            action.Populate(applicationWorkflowStepElementEntityId, null, null, true);
            action.Execute();
        }
        /// <summary>
        /// Return a list of COIs for the PanelApplication
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <returns><PanelApplication entity identifier/returns>
        public Container<CoiModel> ListApplicationCoi(int panelApplicationId)
        {
            this.ValidateInteger(panelApplicationId, "", "panelApplicationId");
            Container<CoiModel> container = new Container<CoiModel>();

            Entity.PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            //
            // From the PanelApplication we want the reviewer assignments
            //
            var hits = panelApplicationEntity.PanelApplicationReviewerAssignments.
                //
                // then these we filter on the assignment type of COI
                //
                Where(x => x.ClientAssignmentType.AssignmentTypeId == Entity.AssignmentType.COI).
                //
                // Whatever matches we create a web model for.
                //
                Select(x => new CoiModel { FirstName = x.PanelUserAssignment.User.UserInfoes.First().FirstName, LastName = x.PanelUserAssignment.User.UserInfoes.First().LastName }).
                //
                // Then we order them by their last name
                //
                OrderBy(x => x.LastName);

            container.ModelList = hits.ToList<CoiModel>();

            return container;
        }
        /// <summary>
        /// Calculates the model to control the visibility of the icons on the Critique edit page
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        public CritiqueIconControlModel ShowCritiqueIcons(int panelApplicationId, int userId)
        {
            var a = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            return ShowCritiqueIcons(panelApplicationId, a.SessionPanelId, userId);
        }
        /// <summary>
        /// Calculates the model to control the visibility of the icons on the Critique edit page
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        public CritiqueIconControlModel ShowCritiqueIcons(int panelApplicationId, int sessionPanelId, int userId)
        {
            ValidateShowCritiqueIconsParameters(panelApplicationId, sessionPanelId, userId);

            bool canAddEdit = false;
            bool canView = true;
            //
            // Retrieve the entities we need to determine the visibility
            //
            Entity.SessionPanel sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            Entity.PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByID(panelApplicationId);
            Entity.PanelApplicationReviewerAssignment panelApplicationReviewerAssignmentEntity = panelApplicationEntity.UsersAssignmentType(userId);
            bool isPanelReviewer = sessionPanelEntity.PanelUserAssignment(userId) != null && sessionPanelEntity.PanelUserAssignment(userId).ClientParticipantType.ReviewerFlag;
            bool isCoi = ((panelApplicationReviewerAssignmentEntity != null) && (panelApplicationReviewerAssignmentEntity.IsCoi()));
            //
            // We can Add/Edit a comment if
            //  1) we are unassigned (not assigned to the application as a reviewer)
            //  2) the application is in the Final phase & the application has been put into Active mode  
            //  3) the session has not ended
            //
            if (
                (panelApplicationEntity.UsersAssignmentType(userId) == null)         &&
                (panelApplicationEntity.CommentsAvailable()) &&
                (!sessionPanelEntity.IsSessionEnded()) &&
                (isPanelReviewer)
                )
            {
                canAddEdit = true;
            }
            //
            // Comments are not shown to COI's 
            //
            if (isCoi)
            {
                canView = false;
            }
            else if (panelApplicationEntity.IsReadyToReview() & ApplicationHasComments(panelApplicationEntity.ApplicationId))
            {
            }
            else if (!panelApplicationEntity.CommentsAvailable())
            {
                canView = false;
            }

            //
            // Need to take care of the scoring icon.  Only reviewers (assigned and un-assigned) and if the panel is currently scoring.
            //
            bool isScoringEnabled = ((isPanelReviewer) && (panelApplicationEntity.IsCurrentlyScoring()));
            //
            // Finally we determine if the user is a client
            //
            Entity.User u = UnitOfWork.UserRepository.GetByID(userId);
            bool isClient = u.IsClient();
            return new CritiqueIconControlModel(canAddEdit, canView, isScoringEnabled, isPanelReviewer, isClient);
        }
        /// <summary>
        /// Retrieves the scoring status for the application.
        /// </summary>
        /// <param name="sessionPanelId"></param>
        /// <returns>Scoring status for the identified application; null if none exists</returns>
        public Container<IApplicationStatusModel> RetrieveSessionApplicationScoringStatuses(int sessionPanelId)
        {
            ValidateInt(sessionPanelId, FullName(nameof(ApplicationScoringService), nameof(RetrieveSessionApplicationScoringStatuses)), nameof(sessionPanelId));

            ApplicationStatusModelBuilder builder = new ApplicationStatusModelBuilder(this.UnitOfWork, sessionPanelId);
            builder.BuildContainer();

            return builder.Results;
        }
        /// <summary>
        /// Determines if the session panel is active
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <returns>True if now is outside of the meeting dates; false otherwise</returns>
        public bool IsSessionPanelOver(int sessionPanelId)
        {
            bool result = false;
            //
            // There are cases where the service could be called with an invalid sessionPanelId.  Test for it specifically 
            // instead of using one of the base validators.
            //
            if (sessionPanelId > 0)
            {
                Entity.SessionPanel sessionPanelEnity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
                result = !WithinDateRange(sessionPanelEnity.StartDate, sessionPanelEnity.EndDate, GlobalProperties.P2rmisDateTimeNow);
            }

            return result;
        }
        /// <summary>
        /// Wrapper method to determine if unassigned reviewer comments exist for the application.
        /// </summary>
        /// <param name="applicationId">Application entity identifier</param>
        /// <returns>True if comments exist; false otherwise</returns>
        internal virtual bool ApplicationHasComments(int applicationId)
        {
            Container<ISummaryCommentModel> a = this.GetApplicationComments(applicationId);
            return (a.ModelList.Count() > 0);
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Indicates if MOD is active
        /// </summary>
        /// <param name="panelApplicationEntity">PanelApplication entity</param>
        /// <param name="panelStageStepEntity">PanelStageStep entity</param>
        /// <returns>True if the MOD is active; false otherwise</returns>
        internal virtual bool IsModActive(Entity.PanelApplication panelApplicationEntity, Entity.PanelStageStep panelStageStepEntity)
        {
            return ((panelApplicationEntity != null) && (panelStageStepEntity != null))? panelApplicationEntity.IsModActive(panelStageStepEntity.PanelStageStepId) : false;
        }
        /// <summary>
        /// Indicates if the phase is open
        /// </summary>
        /// <param name="panelStageStepEntity">PanelStageStep entity</param>
        /// <returns>True if the phase is open; false otherwise</returns>
        internal virtual bool IsStageStepOpen(Entity.PanelStageStep panelStageStepEntity)
        {
            return ((panelStageStepEntity != null) && (panelStageStepEntity!= null))? panelStageStepEntity.IsStageStepOpen(): false;
        }
        /// <summary>
        /// Verification that no application on this panel is active at this time.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <exception cref="Exception">Thrown if another active application detected</exception>
        internal virtual void ValidateNoPanelActive(int panelApplicationId)
        {
            ActiveApplicationModel model = IsAnyApplicationBeingScored(panelApplicationId);

            if (model != null)
            {
                throw new Exception(string.Format(MessageService.ActiveApplicationFound, "ApplicationScoringService.Active", model.LogNumber, model.PanelApplicationId));
            }
        }
        /// <summary>
        /// Validate parameters for MarkReviewierScoresAsAbstain
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="reviewerUserId">User entity identifier of the reviewer being abstained.</param>
        /// <param name="userId">User entity identifier</param>
        private void ValidateMarkReviewierScoresAsAbstainParameters(int panelApplicationId, int reviewerUserId, int userId)
        {
            this.ValidateInteger(panelApplicationId, "ApplicationScoringService.MarkReviewierScoresAsAbstain", "panelApplicationId");
            this.ValidateInteger(reviewerUserId, "ApplicationScoringService.MarkReviewierScoresAsAbstain", "reviewerUserId");
            this.ValidateInteger(userId, "ApplicationScoringService.MarkReviewierScoresAsAbstain", "userId");
        }
        /// <summary>
        /// Validate parameters for SaveScore
        /// </summary>
        /// <param name="reviewerScores"></param>
        /// <param name="panelApplicationId"></param>
        /// <param name="userId">User entity identifier</param>
        private void ValidateSaveScoreParameters(List<ReviewerScores> reviewerScores, int panelApplicationId, int userId)
        {
            this.ValidateInteger(panelApplicationId, "ApplicationScoringService.SaveScore", "panelApplicationId");
            this.ValidateInteger(userId, "ApplicationScoringService.SaveScore", "userId");
        }
        /// <summary>
        /// Validate parameters for DeleteComment
        /// </summary>
        /// <param name="commentId">Application entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        private void ValidateDeleteCommentParameters(int commentId, int userId)
        {
            this.ValidateInteger(commentId, "ApplicationScoringService.EditComment", "commentId");
            this.ValidateInteger(userId, "ApplicationScoringService.EditComment", "userId");
        }
        /// <summary>
        /// Validate parameters for EditComment
        /// </summary>
        /// <param name="commentId">Application entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        private void ValidateEditCommentParameters(int commentId, int userId)
        {
            this.ValidateInteger(commentId, "ApplicationScoringService.EditComment", "commentId");
            this.ValidateInteger(userId, "ApplicationScoringService.EditComment", "userId");
        }
        /// <summary>
        /// Validate parameters for SaveComment
        /// </summary>
        /// <param name="applicationId">Application entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        private void ValidateSaveCommentParameters(int applicationId, int userId)
        {
            this.ValidateInteger(applicationId, "ApplicationScoringService.SaveComment", "applicationId");
            this.ValidateInteger(userId, "ApplicationScoringService.SaveComment", "userId");
        }
        /// <summary>
        /// Validate parameters for SaveReviewersCritique
        /// </summary>
        /// <param name="applicationWorkflowStepElemenContentId"></param>
        /// <param name="userId">User entity identifier</param>
        private void ValidateSaveReviewersCritique(int applicationWorkflowStepElemenContentId, int userId)
        {
            this.ValidateInteger(applicationWorkflowStepElemenContentId, "ApplicationScoringService.SaveReviewersCritique", "applicationWorkflowStepElemenContentId");
            this.ValidateInteger(userId, "ApplicationScoringService.SaveReviewersCritique", "userId");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="isAssignedUser"></param>
        internal virtual void SetIsAssignedUser(List<IReviewerCritiques> list, bool isAssignedUser)
        {
            list.ForEach(x => x.IsAssignedUser = isAssignedUser);
        }
        /// <summary>
        /// Determines the AppAssign value.  When the user does not have an assignment to the panel
        /// their AppAssign value should be "Unassigned".
        /// </summary>
        /// <param name="panelApplicationReviewerAssignment">User's PanelApplicationReviewerAssignment entity for this panel</param>
        /// <returns>User's assignment or "Unassigned" if not assigned to the application.</returns>
        private string DetermineAppAssign(Entity.PanelApplicationReviewerAssignment panelApplicationReviewerAssignment)
        {
            return (panelApplicationReviewerAssignment == null) ?  "Unassigned" :  panelApplicationReviewerAssignment.ClientAssignmentType.IsCoi ? panelApplicationReviewerAssignment.ClientAssignmentType.AssignmentAbbreviation : panelApplicationReviewerAssignment.ClientAssignmentType.AssignmentLabel;
        }
        /// <summary>
        /// Determine if the user is assigned to this application.
        /// </summary>
        /// <param name="panelApplicationReviewerAssignment">User's PanelApplicationReviewerAssignment entity for this panel</param>
        /// <returns>False if the user is not assigned to this application; ReviewerFlag otherwise</returns>
        private bool DetermineReviewerFlag(Entity.PanelApplicationReviewerAssignment panelApplicationReviewerAssignment)
        {
            return (panelApplicationReviewerAssignment == null) ? false : Entity.AssignmentType.CritiqueAssignments.Contains(panelApplicationReviewerAssignment.ClientAssignmentType.AssignmentTypeId);
        }
        /// <summary>
        /// Determine if the user is a COI.
        /// </summary>
        /// <param name="panelApplicationReviewerAssignment">User's PanelApplicationReviewerAssignment entity for this panel</param>
        /// <returns>False if the user is not assigned to this application; ClientAssignmentType.IsCoi otherwise</returns>
        private bool DetermineCoi(Entity.PanelApplicationReviewerAssignment panelApplicationReviewerAssignment)
        {
            return (panelApplicationReviewerAssignment == null) ? false : panelApplicationReviewerAssignment.ClientAssignmentType.IsCoi;
        }
        /// <summary>
        /// Validate the parameters for ShowCritiqueIcons
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        private void ValidateShowCritiqueIconsParameters(int panelApplicationId, int sessionPanelId, int userId)
        {
            ValidateInt(panelApplicationId, "ApplicationScoringService.ShowCritiqueIcons", "panelApplicationId");
            ValidateInt(sessionPanelId, "ApplicationScoringService.ShowCritiqueIcons", "sessionPanelId");
            ValidateInt(userId, "ApplicationScoringService.ShowCritiqueIcons", "userId");
        }
        #endregion
        #region Static methods
        /// <summary>
        /// Wrapper method to invoke the scoring scale comparer.
        /// </summary>
        /// <param name="scoreTypeOne">First ScoreType value</param>
        /// <param name="scoreTypeTwo">Second ScoreType value</param>
        /// <returns>True if the ScoreTypes are the same (case insensitive); false otherwise</returns>
        public static bool IsSameScoringScale(string scoreTypeOne, string scoreTypeTwo)
        {
            ValString(scoreTypeOne, FullName(nameof(ApplicationScoringService), nameof(IsSameScoringScale)), nameof(scoreTypeOne));
            ValString(scoreTypeTwo, FullName(nameof(ApplicationScoringService), nameof(IsSameScoringScale)), nameof(scoreTypeTwo));
            return Entity.ClientScoringScale.IsSameScale(scoreTypeOne, scoreTypeTwo);
        }
        #endregion
    }
}
