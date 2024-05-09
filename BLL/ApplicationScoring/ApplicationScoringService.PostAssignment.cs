using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.ApplicationScoring;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    public partial class ApplicationScoringService
    {
        #region Services
        /// <summary>
        /// Creates and populates a web model containing data for PostAssignmentApplications Current Assignment grid
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of PreAssignmentModel objects</returns>
        public Container<IPostAssignmentModel> RetrievePostAssignmentApplications(int sessionPanelId, int userId)
        {
            VerifyRetrievePostAssignmentApplicationsParameters(sessionPanelId, userId);

            Container<IPostAssignmentModel> container = new Container<IPostAssignmentModel>();
            List<PostAssignmentModel> list = new List<PostAssignmentModel>();

            SessionPanel sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            int panelUserAssignmentId = sessionPanelEntity.PanelUserAssignmentId(userId);
            bool isChairPerson = sessionPanelEntity.PanelUserAssignment(userId).IsChair();

            var assignedPanelApplicationIds = UnitOfWork.PanelUserAssignmentRepository.GetAssignedPanelApplicationIds(panelUserAssignmentId);
            var panelApplications = UnitOfWork.PanelApplicationRepository.GetPanelApplicationsForPostAssigned(sessionPanelId, assignedPanelApplicationIds);
            foreach (PanelApplication panelApplicationEntity in panelApplications)
            {
                //
                // For convenience define these locals
                //
                var applicationEntity = panelApplicationEntity.Application;
                var applicationPersonnelEntity = applicationEntity.PrimaryInvestigator();
                PanelApplicationReviewerExpertise panelApplicationReviewerExpertiseEntity = panelApplicationEntity.GetUsersExpertiseOnApplication(panelUserAssignmentId);
                //
                //  create a model & add it to the list
                //
                PostAssignmentModel model = new PostAssignmentModel(applicationEntity.Blinded(), isChairPerson);
                list.Add(model);
                //
                // now populate the model
                //
                string firstNameValue = IsBlindedValue(applicationEntity.Blinded(), string.Empty, applicationPersonnelEntity.FirstName);
                string lastNameValue = IsBlindedValue(applicationEntity.Blinded(), BlindedString, applicationPersonnelEntity.LastName);
                string organizationValue = IsBlindedValue(applicationEntity.Blinded(), string.Empty, applicationPersonnelEntity.OrganizationName);

                model.Populate(applicationEntity.LogNumber, applicationEntity.ApplicationTitle, firstNameValue, lastNameValue, applicationEntity.AwardAbbreviation(), organizationValue);
                model.PopulateEntityIdentifiers(panelApplicationEntity.PanelApplicationId, applicationEntity.ApplicationId, sessionPanelId, sessionPanelEntity.PanelAbbreviation, sessionPanelEntity.PanelName, panelUserAssignmentId);
                //
                // There may or may not be an experience
                //
                if (panelApplicationReviewerExpertiseEntity != null)
                {
                    model.PopulateExperience(panelApplicationReviewerExpertiseEntity.Conflict(), panelApplicationReviewerExpertiseEntity.Rating());
                }
                //
                // finally get data necessary for the critique status and overall score
                //
                SetCritiqueStatusAndScore(model, panelApplicationEntity, panelUserAssignmentId);

                IPanelApplicaitonModeStatus onLineStatus = this.PanelApplicationMODStatus(panelApplicationEntity.PanelApplicationId);
                model.Populate(onLineStatus);
            }
            container.ModelList = list;

            return container;
        }
        /// <summary>
        /// Retrieves the chair applications.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Container<IChairAssignmentModel> RetrieveChairApplications(int sessionPanelId, int userId)
        {
            VerifyRetrieveChairAssignmentApplicationsParameters(sessionPanelId, userId);

            Container<IChairAssignmentModel> container = new Container<IChairAssignmentModel>();
            List<ChairAssignmentModel> list = new List<ChairAssignmentModel>();

            SessionPanel sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            int panelUserAssignmentId = sessionPanelEntity.PanelUserAssignmentId(userId);
            bool isChairPerson = sessionPanelEntity.PanelUserAssignment(userId).IsCpritChair();

            var assignedPanelApplicationIds = UnitOfWork.PanelUserAssignmentRepository.GetAssignedPanelApplicationIds(panelUserAssignmentId);
            var panelApplications = UnitOfWork.PanelApplicationRepository.GetPanelApplicationsForChairs(sessionPanelId, assignedPanelApplicationIds);
            foreach (PanelApplication panelApplicationEntity in panelApplications)
            {
                //
                // For convenience define these locals
                //
                var applicationEntity = panelApplicationEntity.Application;
                var applicationPersonnelEntity = applicationEntity.PrimaryInvestigator();
                PanelApplicationReviewerExpertise panelApplicationReviewerExpertiseEntity = panelApplicationEntity.GetUsersExpertiseOnApplication(panelUserAssignmentId);
                //
                //  create a model & add it to the list
                //
                ChairAssignmentModel model = new ChairAssignmentModel(applicationEntity.Blinded(), isChairPerson);
                list.Add(model);
                //
                // now populate the model
                //
                string firstNameValue = IsBlindedValue(applicationEntity.Blinded(), string.Empty, applicationPersonnelEntity.FirstName);
                string lastNameValue = IsBlindedValue(applicationEntity.Blinded(), BlindedString, applicationPersonnelEntity.LastName);
                string organizationValue = IsBlindedValue(applicationEntity.Blinded(), string.Empty, applicationPersonnelEntity.OrganizationName);

                model.Populate(applicationEntity.LogNumber, applicationEntity.ApplicationTitle, firstNameValue, lastNameValue, applicationEntity.AwardAbbreviation(), organizationValue);
                model.PopulateEntityIdentifiers(panelApplicationEntity.PanelApplicationId, applicationEntity.ApplicationId, sessionPanelId, sessionPanelEntity.PanelAbbreviation, sessionPanelEntity.PanelName, panelUserAssignmentId);
                //
                // There may or may not be an experience
                //
                if (panelApplicationReviewerExpertiseEntity != null)
                {
                    model.PopulateExperience(panelApplicationReviewerExpertiseEntity.Conflict(), panelApplicationReviewerExpertiseEntity.Rating());
                }
                //
                // finally get data necessary for the add/edit overview status
                //
                bool isSummaryStarted = panelApplicationEntity.IsSummaryStarted();
                bool hasSummaryText = panelApplicationEntity.HasOverviewText();
                model.SetSummaryStatus(isSummaryStarted, hasSummaryText);
            }
            container.ModelList = list;

            return container;
        }
        /// <summary>
        /// Determines whether [is chair person] [the specified session panel identifier].
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is chair person] [the specified session panel identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCpritChairPerson(int sessionPanelId, int userId)
        {
            SessionPanel sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            bool isChairPerson = sessionPanelEntity.PanelUserAssignment(userId).IsCpritChair();
            return isChairPerson;
        }
        /// <summary>
        /// Sets the critique status and score.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="panelApplicationEntity">The panel application entity.</param>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <remarks>To salvage some performance, we avoid multiple unit of work calls to save trips to the db</remarks>
        internal void SetCritiqueStatusAndScore(PostAssignmentModel model, PanelApplication panelApplicationEntity, int panelUserAssignmentId)
        {
            //
            // To determine what the reviewers phase state is we need:
            //
            PanelStageStep currentPanelStageStepEntity = EstablishThePhase(panelApplicationEntity);
            // PANEL PHASE
            int currentPhase = currentPanelStageStepEntity.StepTypeId;
            // CRITIQUE PHASE
            ApplicationWorkflowStep appWorkflowCurrentStep = EstablishTheActiveStep(panelApplicationEntity,
                panelUserAssignmentId, currentPanelStageStepEntity);
            int currentCritiquePhase = appWorkflowCurrentStep.StepTypeId;
            string currentCritiquePhaseName = appWorkflowCurrentStep.StepName;
            //CRITIQUE PHASE
            bool isOpenOrReopened = IsOpenOrReopened(currentPanelStageStepEntity, currentCritiquePhase);
            bool isReopened = IsReopened(currentPanelStageStepEntity, currentCritiquePhase);
            bool isOpen = currentPanelStageStepEntity.PanelStage.IsStageStepOpenButNotReopened(currentCritiquePhase);
            //
            // This determines if the reviewer has submitted a critique for the specified application
            // CRITIQUE PHASE
            bool reviewerCritiqueSubmittal = EstablishReviewersCritiqueSubmital(panelApplicationEntity, panelUserAssignmentId, currentCritiquePhase);
            //
            // This determines if the reviewer has submitted critiques for all assigned applications for this phase
            // PANEL PHASE
            bool reviewerPhaseCritiqueState = EstablishReviewerPhaseCritiqueState(panelApplicationEntity, panelUserAssignmentId, currentPhase);
            //
            // This determines if the reviewer is assigned to the application
            // 
            bool isAssigenedToApplication = EstablishAssigenedToApplication(panelApplicationEntity, panelUserAssignmentId);
            //
            // Now we dig deeper to get scores and critique started status          
            //
            // Whether the critique has been started for the current panel step
            // CRITIQUE PHASE
            bool critiqueStarted = appWorkflowCurrentStep.HasContent();
            string overallScore = string.Empty;
            var overallElement = appWorkflowCurrentStep.GetOverallElementContent();
            if (critiqueStarted && overallElement.Score != null)
            {
                overallScore = ViewHelpers.ScoreFormatterNotCalculated(appWorkflowCurrentStep.GetOverallElementContent().Score, appWorkflowCurrentStep.GetOverallElementContent().ApplicationWorkflowStepElement.ClientScoringScale.ScoreType, appWorkflowCurrentStep.GetOverallElementContent().AdjectivalEquivalent());    
            }
            //
            //  If the user is assigned to the application then we also need the presentation order
            //
            int? presentationOrder = null;
            if (isAssigenedToApplication)
            {
                presentationOrder = panelApplicationEntity.GetPanelApplicationReviewerAssignment(panelUserAssignmentId).SortOrder;
            };
            
            //
            // Now that we have all of the information we just run the information against the state engine
            //
            StateResult critiqueStatus = DeterminePhaseState(isAssigenedToApplication, reviewerCritiqueSubmittal, reviewerPhaseCritiqueState, isOpenOrReopened, critiqueStarted);
            StateResult newCritiqueStatus = PhaseStateMachine.PhaseState(isAssigenedToApplication, reviewerCritiqueSubmittal, reviewerPhaseCritiqueState, isOpen, isReopened);

            model.PopulateOverallScore(overallScore);
            model.PopulateStatusInfo(critiqueStatus, newCritiqueStatus, reviewerCritiqueSubmittal, isAssigenedToApplication, currentCritiquePhase, currentPhase, currentCritiquePhaseName, presentationOrder, isReopened);
        }

        /// <summary>
        /// Establishes the active step for a critique.
        /// </summary>
        /// <param name="panelApplicationEntity">The panel application entity.</param>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="currentPanelStageStepEntity"></param>
        /// <returns>The active step for a critique</returns>
        internal ApplicationWorkflowStep EstablishTheActiveStep(PanelApplication panelApplicationEntity, int panelUserAssignmentId, PanelStageStep currentPanelStageStepEntity)
        {
            var currentCritiqueStep = panelApplicationEntity.GetCurrentApplicationWorkflowStep(panelUserAssignmentId);
            var panelCritiqueStep =
                panelApplicationEntity.GetApplicationWorkflowStepForStepType(panelUserAssignmentId, currentPanelStageStepEntity.StepTypeId);
            //rule: if current critique is ahead of the panel, we only pull back as far as the panel has gotten
            return currentCritiqueStep.StepOrder > currentPanelStageStepEntity.StepOrder
                ? panelCritiqueStep
                : currentCritiqueStep;
        }

        /// <summary>
        /// Determines whether a panel stage step entity is currently opened or re-opened for a specified critique phase.
        /// </summary>
        /// <param name="currentPanelStageStepEntity">The current panel stage step entity.</param>
        /// <param name="currentCritiquePhase"></param>
        /// <returns></returns>
        internal static bool IsOpenOrReopened(PanelStageStep currentPanelStageStepEntity, int currentCritiquePhase)
        {
            var currentCritiqueStageStepEntity =
                currentPanelStageStepEntity.PanelStage.PanelStageSteps.Where(x => x.StepTypeId == currentCritiquePhase)
                    .DefaultIfEmpty(new PanelStageStep())
                    .First();

            return
                (DateTime.Compare(currentCritiqueStageStepEntity.ReOpenDate ?? DateTime.MaxValue, GlobalProperties.P2rmisDateTimeNow) < 0 && DateTime.Compare(currentCritiqueStageStepEntity.ReCloseDate ?? DateTime.MaxValue, GlobalProperties.P2rmisDateTimeNow) > 0) ||
                   (DateTime.Compare(currentCritiqueStageStepEntity.StartDate ?? DateTime.MaxValue, GlobalProperties.P2rmisDateTimeNow) < 0 && DateTime.Compare(currentCritiqueStageStepEntity.EndDate ?? DateTime.MaxValue, GlobalProperties.P2rmisDateTimeNow) > 0)   ||
                   (DateTime.Compare(currentPanelStageStepEntity.PanelStage.SessionPanel.StartDate ?? DateTime.MaxValue, GlobalProperties.P2rmisDateTimeNow) < 0);
        }
        /// <summary>
        /// Determines whether a panel stage step entity is currently opened or re-opened for a specified critique phase.
        /// </summary>
        /// <param name="currentPanelStageStepEntity">The current panel stage step entity.</param>
        /// <param name="currentCritiquePhase"></param>
        /// <returns></returns>
        internal static bool IsReopened(PanelStageStep currentPanelStageStepEntity, int currentCritiquePhase)
        {
            var currentCritiqueStageStepEntity =
                currentPanelStageStepEntity.PanelStage.PanelStageSteps.Where(x => x.StepTypeId == currentCritiquePhase)
                    .DefaultIfEmpty(new PanelStageStep())
                    .First();
            return  (DateTime.Compare(currentCritiqueStageStepEntity.ReOpenDate ?? DateTime.MaxValue, GlobalProperties.P2rmisDateTimeNow) < 0 && DateTime.Compare(currentCritiqueStageStepEntity.ReCloseDate ?? DateTime.MaxValue, GlobalProperties.P2rmisDateTimeNow) > 0);
        }
        #region Status Depracated
        /// <summary>
        /// State table entry
        /// </summary>
        internal class PhaseState
        {
            /// <summary>
            /// The PhaseState to return
            /// </summary>
            public StateResult Result { get; internal set; }
            /// <summary>
            /// Whether the panel phase is open or reopened
            /// </summary>
            public bool IsOpenOrReopened { get; internal set; }
            /// <summary>
            /// Has the reviewer submitted the critique?
            /// </summary>
            public bool ApplicationCritiqueSubmitted { get; internal set; }
            /// <summary>
            /// Has the reviewer submitted all critiques for the panel phase?
            /// </summary>
            public bool PhaseCritiqueSubmitted { get; internal set; }
            /// <summary>
            /// Is the reviewer assigned to the application?
            /// </summary>
            public bool AssignedToApplication { get; internal set; }
            /// <summary>
            /// Whether the critique has been started
            /// </summary>
            public bool CritiqueStarted { get; internal set; }
            /// <summary>
            /// State as text..
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                //
                // TODO: format the state as a text string
                //
                return "This should not happen";
            }
        }

        /// <summary>
        /// Enumeration of all states as shown in Confluence
        /// </summary>
        private static readonly List<PhaseState> StateTable = new List<PhaseState>()
        {
            new PhaseState
            {
                Result = StateResult.Phase01,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = true,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase02,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = true,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase03,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = true,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase04,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = true,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase05,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = false,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase06,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = false,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase07,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = false,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase08,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = false,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase09,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = true,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase10,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = true,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase11,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = true,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase12,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = true,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase13,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = false,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase14,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = false,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase15,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = false,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase16,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = false,
                CritiqueStarted = true
            },
            new PhaseState
            {
                Result = StateResult.Phase17,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = true,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase18,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = true,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase19,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = true,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase20,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = true,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase21,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = false,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase22,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = false,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase23,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = false,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase24,
                AssignedToApplication = false,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = false,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase25,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = true,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase26,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = true,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase27,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = true,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase28,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = true,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase29,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = false,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase30,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = true,
                IsOpenOrReopened = false,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase31,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = true,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = false,
                CritiqueStarted = false
            },
            new PhaseState
            {
                Result = StateResult.Phase32,
                AssignedToApplication = true,
                ApplicationCritiqueSubmitted = false,
                PhaseCritiqueSubmitted = false,
                IsOpenOrReopened = false,
                CritiqueStarted = false
            }
        };
        /// <summary>
        /// Determines the user Phase state
        /// </summary>
        /// <returns>StateResult ENUM representing the user state in the phase.</returns>
        public static StateResult DeterminePhaseState(bool assignedToApplication, bool applicationCritiqueSubmitted, bool phaseCritiqueSubmitted, bool isOpenOrReopened, bool critiqueStarted)
        {
            //
            // Find where this entry is in the state table.  The StateTable should contain every combination of the possible parameters.
            //
            var result = StateTable.Where(x => (
                                                (x.ApplicationCritiqueSubmitted == applicationCritiqueSubmitted) &&
                                                (x.AssignedToApplication == assignedToApplication) &&
                                                (x.PhaseCritiqueSubmitted == phaseCritiqueSubmitted) &&
                                                (x.IsOpenOrReopened == isOpenOrReopened) &&
                                                (x.CritiqueStarted == critiqueStarted)
                                               ));

            return (result.Count() == 1) ? result.First().Result : StateResult.Default;
        }
        

        #endregion
        /// <summary>
        /// Determines the PanelStageStep of this application.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <returns>PanelStageStep entity representing the current phase</returns>
        internal virtual PanelStageStep EstablishThePhase(PanelApplication panelApplication)
        {
            return panelApplication.CurrentPhase();
        }
        /// <summary>
        /// Determines the PanelStageStep of this application.
        /// </summary>
        /// <param name="panelApplicationEntity">PanelApplication entity</param>
        /// <param name="reviewStageId">ReviewStage entity identifier</param>
        /// <returns>PanelStageStep entity representing the current phase</returns>
        internal virtual PanelStageStep EstablishThePhase(PanelApplication panelApplicationEntity, int reviewStageId)
        {
            PanelStage p = panelApplicationEntity.SessionPanel.GetSpecificPanelStage(reviewStageId);
            return p.CurrentPhase();
        }
        /// <summary>
        /// Determines if the reviewers critique on the application is submitted.
        /// </summary>
        /// <param name="panelApplicationEntity">PanelApplictionEntity</param>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="stepTypeId">StepType entity identifier</param>
        /// <returns>CritiqueSubmittal indicating the state of the submittal</returns>
        internal virtual bool EstablishReviewersCritiqueSubmital(PanelApplication panelApplicationEntity, int panelUserAssignmentId, int stepTypeId)
        {
            return (panelApplicationEntity.CurrentCritiqueState(stepTypeId, panelUserAssignmentId));
        }
        /// <summary>
        /// Determine if a reviewer has submitted a critique for all assigned applications within the phase for the panel.
        /// </summary>
        /// <param name="panelApplicationEntity">PanelApplictionEntity</param>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <returns>CritiqueSubmittal value indicating if the reviewer has submitted all critiques</returns>
        internal virtual bool EstablishReviewerPhaseCritiqueState(PanelApplication panelApplicationEntity, int panelUserAssignmentId, int currentStepType)
        {
            var allCritiquesForAssignedApplications = UnitOfWork.PanelApplicationReviewerAssignmentRepository.Select().Where(x => x.PanelUserAssignmentId == panelUserAssignmentId && x.ClientAssignmentType.AssignmentTypeId != AssignmentType.COI)
                .SelectMany(x => x.PanelApplication.ApplicationStages).Where(x => x.ReviewStageId == ReviewStage.Asynchronous)
                .SelectMany(y => y.ApplicationWorkflows).Where(y => y.PanelUserAssignmentId == panelUserAssignmentId)
                .SelectMany(y => y.ApplicationWorkflowSteps).Where(z => z.StepTypeId == currentStepType).ToList();
            return allCritiquesForAssignedApplications.All(x => x.Resolution);
        }
        /// <summary>
        /// Determines if the reviewer is assigned to the application.
        /// </summary>
        /// <param name="panelApplication">PanelApplication entity</param>
        /// <param name="panelUserAssignmentId">PanelUserAssignemnt entity identifier</param>
        /// <returns>True the reviewer is assigned to the application; false otherwise</returns>
        internal virtual bool EstablishAssigenedToApplication(PanelApplication panelApplicationEntity, int panelUserAssignmentId)
        {
            return panelApplicationEntity.ListReviewers().Contains<int>(panelUserAssignmentId);
        }
        /// <summary>
        /// Gets the blinded string.
        /// </summary>
        /// <value>
        /// The blinded string to mask PI information.
        /// </value>
        private static string BlindedString
        {
            get
            {
                string blindedString = ConfigManager.BlindedReplacementString;
                return blindedString;
            }
        }
        /// <summary>
        /// List the panel application's critiques.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">Current user entity identifier</param>
        /// <param name="isAdmin">Current user is an admin</param>
        /// <returns>Container of IReviewerCritiques</returns>
        public Container<IReviewerCritiques> ListApplicationCritiquesForApplicationEvaluation(int panelApplicationId, int userId, bool isAdmin)
        {
            ValidateInt(panelApplicationId, "ApplicationScoringService.ListApplicationCritiques", "panelApplicationId");
            Container<IReviewerCritiques> container = new Container<IReviewerCritiques>();
            List<IReviewerCritiques> list = new List<IReviewerCritiques>();
            //
            // Get the PanelApplication entity, the users PanelApplicationId, a list of the assigned reviewers PanelApplicationIds
            // 
            var panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByIDWithPanelCritiqueInfo(panelApplicationId);
            int panelUserAssignmentId = UnitOfWork.PanelUserAssignmentRepository.Get(x => x.SessionPanelId == panelApplicationEntity.SessionPanelId && x.UserId == userId).FirstOrDefault()?.PanelUserAssignmentId ?? 0;
            List<int> listReviewerPanelUserAssignmentId = panelApplicationEntity.ListReviewers();
            List<int> reviewerUserIds = new List<int>();
            //
            // We need to determine if the user has submitted the critique for this phase
            //
            //We check whether the current critique stage is preliminary or revised

            var currentReviewStage = panelApplicationEntity.GetCurrentReviewStage();
            //TODO: seems like duplicate call to get current review stage then check whether it's async
            var isCurrentReviewStageAsync = panelApplicationEntity.IsCurrentReviewStageAsynchronous();

            PanelStageStep currentPanelStageStepEntity = EstablishThePhase(panelApplicationEntity, currentReviewStage);
            // PANEL PHASE
            int currentPhase = currentPanelStageStepEntity.StepTypeId;
            // CRITIQUE PHASE
            ApplicationWorkflowStep appWorkflowCurrentStep = EstablishTheActiveStep(panelApplicationEntity, panelUserAssignmentId, currentPanelStageStepEntity);
            int currentCritiquePhase = appWorkflowCurrentStep.StepTypeId;
            //CRITIQUE PHASE
            bool isOpenOrReopened = IsOpenOrReopened(currentPanelStageStepEntity, currentCritiquePhase);
            //
            // PANEL PHASE -This determines if the reviewer has submitted critiques for all assigned applications for this phase
            //
            bool reviewerPhaseCritiqueState = EstablishReviewerPhaseCritiqueState(panelApplicationEntity, panelUserAssignmentId, currentPhase);
            ApplicationWorkflowStep applicationWorkflowStepEntityForReviewer = panelApplicationEntity.GetCurrentApplicationWorkflowStep(panelUserAssignmentId, currentPhase);
            //
            // Now that we have the workflow step we can answer the question if they have submitted a critique for it
            //
            bool hasSubmitted = applicationWorkflowStepEntityForReviewer.Resolution;
            //
            // From the start we assume that the user cannot edit the critiques.  Where they can it is recalculated & reset
            //
            bool canEdit = false;
            //
            // if the current phase step is meeting then we send back all of the critiques & allow them to edit
            //
            if (currentPanelStageStepEntity.IsMeetingPhase())
            {
                //user can edit for panel if dates are within range
                canEdit = WithinDateRange(panelApplicationEntity.SessionPanel.StartDate, panelApplicationEntity.SessionPanel.EndDate, GlobalProperties.P2rmisDateTimeNow);
                list = ConstructAllCritiquesForReview(panelApplicationEntity, userId, isCurrentReviewStageAsync, currentReviewStage, StepType.Indexes.Meeting, listReviewerPanelUserAssignmentId, reviewerUserIds, canEdit, isAdmin);
            }
            //
            //
            // For the first phase
            //
            else if ((applicationWorkflowStepEntityForReviewer.IsFirstWorkflowStep()) || (appWorkflowCurrentStep.IsFirstWorkflowStep()) || (!hasSubmitted))
            {
                if (listReviewerPanelUserAssignmentId.Contains(panelUserAssignmentId))
                {
                    //
                    // If the user has not submitted the critique then we send back only their critique.  If they can edit it is indicated by
                    // the step open/reopen state
                    //
                    if (!hasSubmitted)
                    {
                        //
                        // Retrieve the workflow for a specific phase.  The phase is determined by the user's phase within the review process.
                        // 
                        ApplicationWorkflowStep applicationWorkflowStepEntity = panelApplicationEntity.GetCurrentApplicationWorkflowStep(panelUserAssignmentId);
                        IReviewerCritiques reviewerCritique = ConstructCritiqueModel(applicationWorkflowStepEntity.ApplicationWorkflow, applicationWorkflowStepEntity, userId, isCurrentReviewStageAsync, currentReviewStage, isOpenOrReopened, panelUserAssignmentId, panelApplicationEntity, isAdmin);
                        list.Add(reviewerCritique);
                        //
                        // In addition to this we need to update the list of reviewers userIds to only the user
                        //
                        reviewerUserIds.Add(userId);
                    }
                    //
                    // Otherwise we send back all critiques for the user's current phase and we only let them view it.
                    //
                    else
                    {
                        list = ConstructAllCritiquesForReview(panelApplicationEntity, userId, isCurrentReviewStageAsync, currentReviewStage, StepType.Indexes.Preliminary, listReviewerPanelUserAssignmentId, reviewerUserIds, canEdit, isAdmin);
                    }
                }
                else
                {
                    //
                    // If the reviewer has submitted all of their assigned critiques then they can see all 
                    // of the others. 
                    //
                    if (reviewerPhaseCritiqueState)
                    {
                        list = ConstructAllCritiquesForReview(panelApplicationEntity, userId, isCurrentReviewStageAsync, currentReviewStage, currentPhase, listReviewerPanelUserAssignmentId, reviewerUserIds, canEdit, isAdmin);
                    }
                    else
                    {
                        //
                        // They have not submitted all of their critiques.  In which case they cannot
                        // see any other critiques.
                        //
                    }
                }
            }
            else
            {
                //
                // We determine if the user can edit their own critique.  The can edit their own critique if: 
                //  - the phase is open or reopened and they have not submitted the critique
                //  - the session panel is active and not an online panel but scoring has not started on the application 
                //
                canEdit = (isOpenOrReopened & !hasSubmitted) || (panelApplicationEntity.SessionPanel.IsActive() && !panelApplicationEntity.SessionPanel.IsOnLineMeeting());
                //
                // Concerning the phase that we want.  If the user is assigned to review the application they
                // will have a specific phase.  Otherwise they will not, it will be 0.  In which case we use
                // the panel's phase.
                //
                int phaseToUse = (currentCritiquePhase == 0) ? currentPhase : currentCritiquePhase;

                list = ConstructAllCritiquesForReview(panelApplicationEntity, userId, isCurrentReviewStageAsync, currentReviewStage, phaseToUse, listReviewerPanelUserAssignmentId, reviewerUserIds, canEdit, isAdmin);
            }

            //
            // Now we go back & set the indicator for this user being an assigned user
            //
            SetIsAssignedUser(list, reviewerUserIds.Contains(userId));
            container.ModelList = list.OrderBy(x => x.ReviewerSlot);
            return container;
        }
        /// <summary>
        /// Retrieve the critique from the specified workflow.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <param name="applicationWorkflowStepId">ApplicationWorkflowStep entity identifier</param>
        /// <param name="isCurrentUser">Whether the current critique page belongs to the logged in user</param>
        /// <param name="hasManageCritiquesPermission">Whether the user has permission to manage other user's critiques</param>
        /// <returns>Container of the workflow's critique</returns>
        public Container<IReviewerCritiques> ListApplicationCritiqueForPanelManagement(int panelApplicationId, int userId, int applicationWorkflowStepId, bool isCurrentUser, bool hasManageCritiquesPermission, bool hasViewAllCritiquesPermission)
        {
            ValidateInt(panelApplicationId, "ApplicationScoringService.ListApplicationCritiques", "panelApplicationId");
            Container<IReviewerCritiques> container = new Container<IReviewerCritiques>();
            List<IReviewerCritiques> list = new List<IReviewerCritiques>();
            List<int> reviewerUserIds = new List<int>();
            //
            // Get the PanelApplication entity, the users PanelApplicationId, a list of the assigned reviewers PanelApplicationIds
            // 
            //include workflows, critique template and scores (uses quite a bit of memory but avoids redundant individual database calls
            PanelApplication panelApplicationEntity = UnitOfWork.PanelApplicationRepository.GetByIDWithPanelCritiqueInfo(panelApplicationId);
            ApplicationWorkflowStep applicationWorkflowStepEntity = panelApplicationEntity.ApplicationStages.SelectMany(x => x.ApplicationWorkflows).SelectMany(x => x.ApplicationWorkflowSteps).FirstOrDefault(y => y.ApplicationWorkflowStepId == applicationWorkflowStepId);
            //include stage and panel info
            SessionPanel sessionPanelEntity = panelApplicationEntity.SessionPanel;
            //include workflows, critique template and scores
            ApplicationStage applicationStageEntity = applicationWorkflowStepEntity.ApplicationWorkflow.ApplicationStage;

            //
            // We need to determine if the user has submitted the critique for this phase
            //
            //We check whether the current critique stage is preliminary or revised
            var currentReviewStage = panelApplicationEntity.GetCurrentReviewStage();
            var isCurrentReviewStageAsync = panelApplicationEntity.IsCurrentReviewStageAsynchronous();
            int stepTypeId = applicationWorkflowStepEntity.StepTypeId;
            var panelStageStep = sessionPanelEntity.GetSpecificPanelStageStep(stepTypeId);

            foreach(var applicationWorkflowEntity in applicationStageEntity.ApplicationWorkflows)
            {
                var aws = applicationWorkflowEntity.ApplicationWorkflowSteps.FirstOrDefault(x => x.StepTypeId == stepTypeId);
                bool canEdit = (hasManageCritiquesPermission || isCurrentUser) && ViewHelpers.IsAbleToEditCritique(aws.ResolutionDate, 
                    panelStageStep.StartDate.Value, panelStageStep.EndDate.Value, panelStageStep.ReOpenDate, panelStageStep.ReCloseDate);

                PanelUserAssignment panelUserAssignment = applicationWorkflowEntity.PanelUserAssignment;
                //
                // Retrieve the workflow for a specific phase.  The phase is determined by the user's phase within the review process.
                // 
                // original ApplicationWorkflowStep applicationWorkflowStepEntity = panelApplicationEntity.GetCurrentApplicationWorkflowStep(panelUserAssignmentId);
                IReviewerCritiques reviewerCritique = ConstructCritiqueModel(applicationWorkflowEntity, aws, 
                    userId, isCurrentReviewStageAsync, currentReviewStage, canEdit, panelUserAssignment.PanelUserAssignmentId, panelApplicationEntity, hasViewAllCritiquesPermission);
                list.Add(reviewerCritique);
                //
                // In addition to this we need to update the list of reviewers userIds to only the user
                //
                reviewerUserIds.Add(panelUserAssignment.UserId);                
            }
            // Now we go back & set the indicator for this user being an assigned user
            SetIsAssignedUser(list, reviewerUserIds.Contains(userId));
            container.ModelList = list.OrderBy(x => x.ReviewerSlot);
            return container;
        }
        /// <summary>
        /// Construct a ReviewerCritiques web model
        /// </summary>
        /// <param name="applicationWorkflowEntity">ApplicationWorkflow Entity</param>
        /// <param name="applicationWorkflowStepEntity">The application workflow step entity.</param>
        /// <param name="userId">User entity identifier</param>
        /// <param name="isCurrentReviewStageAsync">Indicates if the current review stage is asynchronous</param>
        /// <param name="currentReviewStage">ReviewStage entity identifier</param>
        /// <param name="canEdit">Indicates if the critique reviewer can edit the critique</param>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <param name="panelApplicationEntity">The panel application entity.</param>
        /// <param name="canViewUnsubmitted">Whether the user can view unsubmitted critiques.</param>
        /// <returns>
        /// ReviewerCritiques web model
        /// </returns>
        internal static IReviewerCritiques ConstructCritiqueModel(ApplicationWorkflow applicationWorkflowEntity, ApplicationWorkflowStep applicationWorkflowStepEntity, int userId, bool isCurrentReviewStageAsync, int currentReviewStage, bool canEdit, int panelUserAssignmentId, PanelApplication panelApplicationEntity, bool canViewUnsubmitted)
        {
            int reviewerId = applicationWorkflowEntity.CurrentUser();
            IReviewerCritiques result = new ReviewerCritiques(applicationWorkflowEntity.ApplicationWorkflowId, applicationWorkflowEntity.FirstName(), applicationWorkflowEntity.LastName(), 
                applicationWorkflowEntity.ParticipantTypeName(), applicationWorkflowEntity.ReviewerAssignmentOrder(), applicationWorkflowEntity.RoleName(),
                GetCriterionAndContent(applicationWorkflowEntity, applicationWorkflowStepEntity), applicationWorkflowStepEntity.Resolution, applicationWorkflowStepEntity.StepName,
                currentReviewStage, isCurrentReviewStageAsync, (reviewerId == userId), 
                reviewerId, canEdit, applicationWorkflowEntity.ModifiedDate, 
                panelApplicationEntity.UsersAssignmentType(reviewerId).AssignmentAbbreviation(), panelApplicationEntity.IsPanelStarted());
            //
            // Now retrieve all the reviewer's score information for each phase
            //
            var steps = panelApplicationEntity.AllReviewWorkflowStepsForPanelUser(panelUserAssignmentId);
            result.Critiques.ForEach(x => x.PhaseScores = GetPhaseScoresForReviewerCriterion(steps, x.ClientElementId,
                    canEdit, canViewUnsubmitted));
            result.PrevApplicationWorkflowSteps = GetPrevPhasesForReviewer(steps, applicationWorkflowStepEntity);
            return result;
        }
        /// <summary>
        /// Gets the content of the criterion and.
        /// </summary>
        /// <param name="applicationWorkflowEntity">The application workflow entity.</param>
        /// <param name="applicationWorkflowStepEntity">The application workflow step entity.</param>
        /// <returns></returns>
        internal static List<CritiqueContent> GetCriterionAndContent(ApplicationWorkflow applicationWorkflowEntity, ApplicationWorkflowStep applicationWorkflowStepEntity)
        {
            var cc = applicationWorkflowStepEntity.ApplicationWorkflowStepElements.Select(x => new CritiqueContent(x.ApplicationTemplateElement.MechanismTemplateElement.ClientElement.ElementDescription,
                        x.ApplicationTemplateElement.MechanismTemplateElement.ClientElement.ClientElementId,
                        (x.ApplicationWorkflowStepElementContents.FirstOrDefault() == null) ? "" : x.ApplicationWorkflowStepElementContents.FirstOrDefault().ContentText,
                        x.Score(),
                        x.ApplicationTemplateElement.MechanismTemplateElement.ScoreFlag,
                        x.ApplicationTemplateElement.MechanismTemplateElement.TextFlag,
                        (x.ApplicationWorkflowStepElementContents.FirstOrDefault() == null) ? 0 : x.ApplicationWorkflowStepElementContents.FirstOrDefault().ApplicationWorkflowStepElementContentId,
                        (x.ApplicationWorkflowStepElementContents.FirstOrDefault() == null) ? false : x.ApplicationWorkflowStepElementContents.FirstOrDefault().CritiqueRevised,
                        (x.ApplicationWorkflowStepElementContents.FirstOrDefault() == null || x.ClientScoringScale == null) ?
                            string.Empty : x.ApplicationWorkflowStepElementContents.FirstOrDefault().AdjectivalEquivalent(),
                        x.GetAdjectivalScoringScale(),
                        x.ApplicationTemplateElement.MechanismTemplateElement.InstructionText,
                        (x.ApplicationWorkflowStepElementContents.FirstOrDefault() == null) ? false : x.ApplicationWorkflowStepElementContents.FirstOrDefault().Abstain,
                        x.ApplicationWorkflowStepElementId, x.ApplicationWorkflowStepElementContent().ModifiedDate,
                        x.ScoreType(), x.ScoreHighValue(), x.ScoreLowValue(), x.IsOverall(),
                        x.ApplicationTemplateElement.MechanismTemplateElement.RecommendedWordCount,
                        x.ApplicationTemplateElement.GetCriteriaSortOrder())).OrderBy(x => x.SortOrder).ToList();
            return cc;
        }

        /// <summary>
        /// Construct a list of ReviewerCritiques web models for all reviewer assigned to the application. 
        /// </summary>
        /// <param name="panelApplicationEntity"></param>
        /// <param name="userId">User entity identifier</param>
        /// <param name="isCurrentReviewStageAsync">Indicates if the current review stage is asynchronous</param>
        /// <param name="currentReviewStage">ReviewStage entity identifier</param>
        /// <param name="currentCritiquePhase"></param>
        /// <param name="listReviewerPanelUserAssignmentId">List of PanelUserAssignment entity identifiers of reviewers assigned to review the application</param>
        /// <param name="reviewerUserIds">(out) list of assigned reviewers User entity identifiers</param>
        /// <param name="canEditForPanel">Indicates if the critique reviewer can edit critique's on this panel</param>
        /// <param name="isAdmin">Indicates if the user is an admin</param>
        /// <returns>List of ReviewerCritiques web models & populates list of reviewer User entity identifiers</returns>
        internal List<IReviewerCritiques> ConstructAllCritiquesForReview(PanelApplication panelApplicationEntity, int userId, bool isCurrentReviewStageAsync, int currentReviewStage, int currentCritiquePhase, List<int> listReviewerPanelUserAssignmentId, List<int> reviewerUserIds, bool canEditForPanel, bool isAdmin)
        {
            List<IReviewerCritiques> list = new List<IReviewerCritiques>();

            foreach (var applicationWorkflowEntity in panelApplicationEntity.ApplicationStages.Where(x => x.ReviewStageId == currentReviewStage).SelectMany(x => x.ApplicationWorkflows))
            {
                //
                // While there may be a workflow that does not guarantee that we want that workflow.  We only want the reviewer's wrokflow which are the ones in 
                // PanelApplicationReviewerAssignment (but the link to the work flow is not there).  So match up the id which are in both.  (We assume here that 
                // if there is a workflow it will have a user assigned to it.)
                int panelUserAssignmentId = applicationWorkflowEntity.PanelUserAssignmentId ?? 0;
                //
                if (listReviewerPanelUserAssignmentId.Contains(panelUserAssignmentId))
                {
                    int reviewerId = applicationWorkflowEntity.CurrentUser();
                    reviewerUserIds.Add(reviewerId);
                    //
                    // Retrieve the workflow for a specific phase.  The phase is determined by the user's phase within the review process.
                    // Which phase that should be was determined by the caller.
                    // 
                    ApplicationWorkflowStep applicationWorkflowStepEntity = applicationWorkflowEntity.GetSpecificWorkflowStepByStepType(currentCritiquePhase);
                    //
                    // Next we check if the user can edit the critique.  The calling method determined if the user could edit their critique.  However we don't want
                    // to apply that setting to all critiques.  Check if this critique is their's and let them edit it.  Otherwise they cannot.
                    //
                    var canEditThisCritique = (isAdmin && canEditForPanel) || CanEditThisCritique(applicationWorkflowStepEntity, canEditForPanel, userId);
                    IReviewerCritiques result = ConstructCritiqueModel(applicationWorkflowEntity, applicationWorkflowStepEntity, userId, isCurrentReviewStageAsync, currentReviewStage, canEditThisCritique, panelUserAssignmentId, panelApplicationEntity, isAdmin);
                    
                    list.Add(result);
                }
            }
            return list;
        }
        /// <summary>
        /// Gets the phase scores for reviewer criterion.
        /// </summary>
        /// <param name="steps">All application workflow steps for panel user.</param>
        /// <param name="clientElementId">The client element identifier.</param>
        /// <param name="canEdit">if set to <c>true</c> [can edit].</param>
        /// <param name="canViewUnsubmitted">if set to <c>true</c> [user can view unsubmitted critiques].</param>
        /// <returns>Gets a collection of all scores for a specified criterion</returns>
        internal static IEnumerable<CritiqueScore> GetPhaseScoresForReviewerCriterion(IEnumerable<ApplicationWorkflowStep> steps, 
            int clientElementId, bool canEdit, bool canViewUnsubmitted)
        {
            //retrieve all scores and filter un-submitted if user cannot edit
            var result = steps.Where(x => canViewUnsubmitted || canEdit || x.Resolution)
                .OrderBy(x => x.ApplicationWorkflow.ApplicationStage.StageOrder)
                .SelectMany(x => x.ApplicationWorkflowStepElements)
                .Where(x => x.ApplicationTemplateElement.MechanismTemplateElement.ClientElementId == clientElementId)
                .Select(
                    x =>
                        new CritiqueScore(x.ApplicationWorkflowStepId, x.ApplicationWorkflowStep.StepName,
                            ViewHelpers.ScoreFormatterNotCalculatedWithAbstain(x.ApplicationWorkflowStepElementContent().Score, x.ScoreType(), 
                            x.ApplicationWorkflowStepElementContent().AdjectivalEquivalent(), x.IsAbstained()),
                            x.ApplicationTemplateElement.MechanismTemplateElement.SortOrder,
                            x.ApplicationWorkflowStepElementContent().Abstain, 
                            x.ApplicationWorkflowStep.Resolution,
                            x.ApplicationWorkflowStep.ApplicationWorkflow.ApplicationStage.StageOrder)
                )
                //
                // And now we order it by the stage & then the sort order within the stage
                //
                .OrderBy(x => x.StageOrder).ThenBy(x => x.Order);
            return result;
        }
        /// <summary>
        /// Gets the previous phases for reviewer.
        /// </summary>
        /// <param name="steps">All application workflow steps for panel user.</param>
        /// <param name="applicationWorkflowStepEntity">The application workflow step entity.</param>
        /// <returns></returns>
        internal static IList<ApplicationWorkflowStepModel> GetPrevPhasesForReviewer(IEnumerable<ApplicationWorkflowStep> steps,
            ApplicationWorkflowStep applicationWorkflowStepEntity)
        {
            var result = steps.Where(x => x.StepTypeId < applicationWorkflowStepEntity.StepTypeId || 
                    (x.StepTypeId == applicationWorkflowStepEntity.StepTypeId && x.StepOrder < applicationWorkflowStepEntity.StepOrder))
                .Select(x => new ApplicationWorkflowStepModel()
                {
                    StepName = x.StepName,
                    ApplicationWorkflowStepId = x.ApplicationWorkflowStepId,
                    IsCompleted = x.Resolution
                }).ToList();
            return result;
        }

        /// <summary>
        /// Determines if the critique in the ApplicationWorkflowStep entity should be editable by the user.
        /// </summary>
        /// <param name="applicationWorkflowStepEntity">ApplicatinoWorkflowStep entity</param>
        /// <param name="canEdit">Indicates if the critique reviewer can edit the critique</param>
        /// <returns>True if the user can edit the critique; false otherwise</returns>
        private bool CanEditThisCritique(ApplicationWorkflowStep applicationWorkflowStepEntity, bool canEdit, int userId)
        {
            ApplicationWorkflowStepAssignment applicationWorkflowStepAssignmentEntity = applicationWorkflowStepEntity.ApplicationWorkflowStepAssignments.FirstOrDefault(x => x.UserId == userId);
            return (applicationWorkflowStepAssignmentEntity != null) ? canEdit : false;
        }
        /// <summary>
        /// Save or update a reviewer's critique during the post assignment phase.
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">ApplicationWorkflowStepElement entity identifier</param>
        /// <param name="applicationWorkflowStepElemenContentId">ApplicationWorkflowStepElementContent entity identifier</param>
        /// <param name="critiqueText">Critique text</param>
        /// <param name="score">Reviewer's score</param>
        /// <param name="abstain">Indicates if the user abstained from the critique</param>
        /// <param name="userId">User entity identifier</param>
        /// <param name="isPanelStarted">Indicates if the panel has been started</param>
        public ISaveReviewersCritiquePostAssignmentResultsModel SaveReviewersCritiquePostAssignment(int applicationWorkflowStepElementId, int applicationWorkflowStepElemenContentId, string critiqueText, decimal? score, bool abstain, bool isPanelStarted, int userId, string errorMessage)
        {
            ValidateSaveReviewersCritiquePostAssignmentParameters(applicationWorkflowStepElementId, userId);
            //
            // First thing we need to do is check if the user can update the critique.  They can if
            //  - the panel has started OR
            //  - it is not part of a submitted critique.  The marker for the submission of a critique is contained in the ApplicationWorkflowStep
            //
            ApplicationWorkflowStepElement applicationWorkflowStepElementEntity = UnitOfWork.ApplicationWorkflowStepElementRepository.GetByID(applicationWorkflowStepElementId);
            
            if ((!isPanelStarted) && (applicationWorkflowStepElementEntity.IsResolved()))
            {
                //
                // We should not get here.  The UI should enforce this, but one never knows
                //
                errorMessage = MessageService.FailedToSaveCritiqueBecauseCritiqueWasSubmitted(applicationWorkflowStepElemenContentId, userId);
                //throw new CritiqueSubmittalException(applicationWorkflowStepElemenContentId, userId);
            }
            //            
            //  They have provided a critique & score
            //  Depending upon which one it is we will need to create different ServiceActions.
            //
            else
            {
                // Get existing contentId if not provided
                if (applicationWorkflowStepElemenContentId == 0)
                {
                    ApplicationWorkflowStepElementContent contentEntity = UnitOfWork.ApplicationWorkflowStepElementContentRepository.GetByElementId(applicationWorkflowStepElementId);
                    if (contentEntity != null)
                        applicationWorkflowStepElemenContentId = contentEntity.ApplicationWorkflowStepElementContentId;
                }
                //
                // This is the happy path, save the critique
                //
                ApplicationWorkflowStepElementContentServiceActionPostAssignment editAction = new ApplicationWorkflowStepElementContentServiceActionPostAssignment();
                editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationWorkflowStepElementContentRepository, ServiceAction<ApplicationWorkflowStepElementContent>.DoUpdate, applicationWorkflowStepElemenContentId, userId);
                editAction.Populate(this.UnitOfWork.ApplicationWorkflowStepElementRepository, critiqueText, score, abstain, applicationWorkflowStepElementId);
                editAction.Execute();
            }
            //
            // The UI needs to maintain the entity id value in the underlying page.  So we send it back to them.  It could have been an add for an abstain (because of the soft delete)
            // or added for a create.  So we just send it back in all cases.
            //
            return new SaveReviewersCritiquePostAssignmentResultsModel(applicationWorkflowStepElementId, applicationWorkflowStepElementEntity.ApplicationWorkflowStepElementContentId(), abstain, errorMessage);
        }
        /// <summary>
        /// Business rule to determine if "Abstain" can be saved fro a reviewers content
        /// </summary>
        /// <param name="abstain"></param>
        /// <param name="isPanelStarted"></param>
        /// <param name="applicationWorkflowStepElementEntity"></param>
        /// <returns></returns>
        private bool Abstainable(bool abstain, bool isPanelStarted, ApplicationWorkflowStepElement applicationWorkflowStepElementEntity)
        {
            return (
                      ((abstain == true) && (!isPanelStarted)) ||
                      (
                        (abstain == true) &&
                        isPanelStarted &&
                        applicationWorkflowStepElementEntity.IsContentForPreMeetingCritique() &&
                        !applicationWorkflowStepElementEntity.IsSubmitted())
                      );
        }
        /// <summary>
        /// Checks to see if the ApplicationWorkflowStepElementContent is an abstain.
        /// </summary>
        /// <param name="applicationWorkflowStepElemenContentId">ApplicationWorkflowStepElementContent entity identifier</param>
        /// <returns>True if the content is marked Abstain; false otherwise</returns>
        private bool IsContaintAbstain(int applicationWorkflowStepElemenContentId)
        {
            return (applicationWorkflowStepElemenContentId > 0)? UnitOfWork.ApplicationWorkflowStepElementContentRepository.GetByID(applicationWorkflowStepElemenContentId).Abstain: false;
        }
        /// <summary>
        /// Determines if the critique (containing the supplied ApplicationWorkflowStepElemnt) is submittable.
        /// Submittable is defined as containing an entry for the ContextText for each criteria or an abstention.
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">ApplicationWorkflowStepElemnt entity identifier</param>
        /// <returns>Container of IIncompleteCriteriaNameModel objects listing any incomplete criteria.  Empty container list signifies all criteria have been saved.</returns>
        public Container<IIncompleteCriteriaNameModel> CanSubmit(int applicationWorkflowStepElementId)
        {
            this.ValidateInteger(applicationWorkflowStepElementId, "ApplicationScoringService.CanSubmit", "applicationWrokflowStepElemnetId");

            Container<IIncompleteCriteriaNameModel> result = new Container<IIncompleteCriteriaNameModel>();
            ApplicationWorkflowStepElement applicationWorkflowStepElementEntity = UnitOfWork.ApplicationWorkflowStepElementRepository.GetByID(applicationWorkflowStepElementId);
            result.ModelList = applicationWorkflowStepElementEntity.ApplicationWorkflowStep.CanSubmit();

            return result;
        }
        /// <summary>
        /// Determines if the critique (containing the supplied ApplicationWorkflowStepElemnt) requires an overall rating
        /// If it does it checks if one has been supplied.
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">ApplicationWorkflowStepElemnt entity identifier</param>
        /// <returns></returns>
        public bool HasOverall(int applicationWorkflowStepElementId)
        {
            this.ValidateInteger(applicationWorkflowStepElementId, "ApplicationScoringService.HasOverall", "applicationWrokflowStepElemnetId");
            ApplicationWorkflowStepElement applicationWorkflowStepElementEntity = UnitOfWork.ApplicationWorkflowStepElementRepository.GetByID(applicationWorkflowStepElementId);
            bool result = true;

            if (!applicationWorkflowStepElementEntity.IsResolved())
            {
                //
                // Get the workflow we are talking about
                //
                ApplicationWorkflow applicationWorkflowEntity = applicationWorkflowStepElementEntity.ApplicationWorkflowStep.ApplicationWorkflow;
                //
                // Now we find all the overall element
                //
                ApplicationWorkflowStepElement applicationWorkflowStepElementOverallEntity = applicationWorkflowEntity.GetOverallStepElement();
                //
                // If the step actually requires an overall evaluation then check and see if it has a text & a score.
                // HasTextData() & HasScoreData() deal with the conditions required/not required so we can call them at this level.
                //
                if (applicationWorkflowStepElementOverallEntity != null)
                {
                    ApplicationWorkflowStepElementContent applicationWorkflowStepElementContentEntity = applicationWorkflowStepElementOverallEntity.GetContents();
                    result = applicationWorkflowStepElementOverallEntity.HasScoreData(applicationWorkflowStepElementContentEntity) &
                             applicationWorkflowStepElementOverallEntity.HasTextData(applicationWorkflowStepElementContentEntity);
                }

            }
            return result;
        }
        /// <summary>
        /// Validates the value entered into the Rating control.
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">The Critique's ApplicationWorkflowStepElement entity identifier</param>
        /// <param name="rating">Rating value</param>
        /// <param name="isPanelStarted">Indicates if the panel has been started</param>
        /// <returns>True if the rating value is valid; false otherwise.</returns>
        public RatingValidationModel IsRatingValid(int applicationWorkflowStepElementId, decimal? rating, bool isPanelStarted)
        {
            this.ValidateInteger(applicationWorkflowStepElementId, "ApplicationScoringService.IsRatingValid", "applicationWorkflowStepElementId");

            RatingValidationModel result = new RatingValidationModel();
            ApplicationWorkflowStepElement applicationWorkflowStepElementEntity = UnitOfWork.ApplicationWorkflowStepElementRepository.GetByID(applicationWorkflowStepElementId);

            //
            // Do we have a scoring scale?  If not then the only value we can/should accept is null.
            //
            if (applicationWorkflowStepElementEntity.ClientScoringId == null)
            {
                if (rating.HasValue)
                {
                    result = new RatingValidationModel(MessageService.InvalidScoreValueProvided());
                }
            }
            else 
            {
                //
                // If the panel has started then we do not need to validate the rating.  Remember this service method is only 
                // supposed to be called when the criterion edit modal is saved.
                //
                if (isPanelStarted)
                {

                }
                //
                //  There is an actual scoring scale there.  So check that the rating is within the limits and 
                //  return true if it is; false if it is not.  And bob is your uncle.
                //
                else if ((!rating.HasValue) || (!applicationWorkflowStepElementEntity.ClientScoringScale.IsRatingValid(rating.Value)))
                {
                    result = new RatingValidationModel(MessageService.InvalidScoreMessage());
                }
            }
            return result;
        }
        /// <summary>
        /// Save all incomplete review criteria as abstain.
        /// </summary>
        /// <param name="applicationWorkflowStepElementIdCollection">Enumerable list of ApplicationWorkflowStepElement entity identifiers</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of ISaveReviewersCritiquePostAssignmentResultsModel representing the created ApplicationWorkflowStepElementContent entities</returns>
        public Container<ISaveReviewersCritiquePostAssignmentResultsModel> SaveReviewersIncompleteCritiquePostAssignmentAsAbstains(ICollection<int> applicationWorkflowStepElementIdCollection, int userId)
        {
            ValidateSaveReviewersIncompleteCritiquePostAssignmentAsAbstainsParameters(applicationWorkflowStepElementIdCollection, userId);
            //
            // Make a list of the ApplicationWorkflowStepElement we updated so creating the results to return will be simpler.
            //
            List<ApplicationWorkflowStepElement> list = new List<ApplicationWorkflowStepElement>(applicationWorkflowStepElementIdCollection.Count());
            Container<ISaveReviewersCritiquePostAssignmentResultsModel> container = new Container<ISaveReviewersCritiquePostAssignmentResultsModel>();
            //
            // First we create the Service action to do the CRUD operations for the Abstain
            //
            ApplicationWorkflowStepElementContentServiceActionPostAssignment editAction = new ApplicationWorkflowStepElementContentServiceActionPostAssignment();
            //
            // Now it is just as simple as iterating over the ApplicationWorkflowStepElementIds
            //
            foreach (int applicationWorkflowStepElementId in applicationWorkflowStepElementIdCollection)
            {
                ApplicationWorkflowStepElement applicationWorkflowStepElementEntity = UnitOfWork.ApplicationWorkflowStepElementRepository.GetByID(applicationWorkflowStepElementId);
                int applicationWorkflowStepElemenContentId = applicationWorkflowStepElementEntity.ApplicationWorkflowStepElementContentId();
                list.Add(applicationWorkflowStepElementEntity);
                string contentText = applicationWorkflowStepElementEntity.ApplicationWorkflowStepElementContent().ContentText;

                editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.ApplicationWorkflowStepElementContentRepository, ServiceAction<ApplicationWorkflowStepElementContent>.DoNotUpdate, applicationWorkflowStepElemenContentId, userId);
                editAction.Populate(this.UnitOfWork.ApplicationWorkflowStepElementRepository, contentText, null, true, applicationWorkflowStepElementId);
                editAction.Execute();
            }
            //
            // And finally we just save all at once within a single transaction.
            //
            UnitOfWork.Save();
            //
            // Just for grins let the caller know the ids of the newly created comments
            //
            List<ISaveReviewersCritiquePostAssignmentResultsModel> result = SaveReviewersIncompleteCritiquePostAssignmentAsAbstainsResults(list);
            container.ModelList = result;
            return container;
        }
        /// <summary>
        /// Constructs a list of the ApplicationWorkflowStepElementContents identifiers created when all remaining results were abstained.
        /// </summary>
        /// <param name="list">ApplicationWorkflowStepElement List containing entities that had comments created </param>
        /// <returns>ISaveReviewersCritiquePostAssignmentResultsModel list representing the created contents</returns>
        internal virtual List<ISaveReviewersCritiquePostAssignmentResultsModel> SaveReviewersIncompleteCritiquePostAssignmentAsAbstainsResults(List<ApplicationWorkflowStepElement> list)
        {
            List<ISaveReviewersCritiquePostAssignmentResultsModel> result = new List<ISaveReviewersCritiquePostAssignmentResultsModel>(list.Count());
            list.ForEach(x => result.Add(new SaveReviewersCritiquePostAssignmentResultsModel(x.ApplicationWorkflowStepElementId, x.ApplicationWorkflowStepElementContents.First().ApplicationWorkflowStepElementContentId, x.ApplicationWorkflowStepElementContents.First().Abstain, null)));
            return result;
        }
        /// <summary>
        /// Retrieves the assigned reviewers scores.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IReviewerCritiqueSummary RetrieveAssignedReviewersScores(int panelApplicationId, int userId)
        {
            this.ValidateInteger(panelApplicationId, "ApplicationScoringService.RetrieveAssignedReviewersScores", "panelApplicationId");
            IReviewerCritiqueSummary result = new ReviewerCritiqueSummary();
            bool isReviewer = UnitOfWork.UserRepository.IsReviewer(userId);
            PanelApplication panApp = UnitOfWork.PanelApplicationRepository.GetWithAssignments(panelApplicationId);
            //Populate the list of all assigned "critique providing" reviewers
            result.ReviewerList = PopulateReviewerList(panApp);
            //Populate the list of all pre-meeting phases for the panel
            result.PhaseList = PopulateAsyncStagePhaseList(panApp);
            var phaseOrder = DeterminePhaseOrder(userId, panApp, isReviewer);
            //Populate the list of grid score information for the panel application
            var dataList = PopulateCriteriaReviewerScoreModel(panApp, phaseOrder);
            result.CriteriaList = dataList;
            return result;
        }

        /// <summary>
        /// Determines the appropriate phase order to pull critiques from.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="panApp">The pan application.</param>
        /// <returns>Returns the last completed step type. Null if not found or not applied.</returns>
        /// <remarks>Requires SessionPanel, PanelUserAssignment, PanelApplicationReviewerAssignment, ClientAssignmentType,
        /// ApplicationStage, ApplicationReviewStatus, ApplicationWorkflow, ApplicationWorkflowStep entities</remarks>
        internal static int? DeterminePhaseOrder(int userId, PanelApplication panelApplication, bool isReviewer)
        {
            //First get the PanelUserAssignmentId for the reviewer
            int panelAssignmentId = panelApplication.SessionPanel.PanelUserAssignmentId(userId);
            var appAssignment = panelApplication.UsersAssignmentType(userId);
            int? phaseOrder = null;
            //
            // kind of dirty logic if phaseOrder is null then we pull all critiques (unassigned user), otherwise pull just up until that phase
            // Also only want to apply this logic if the PanelApplication is in the review stage.  Otherwise
            // we can just send back all phases.
            // 
            if (isReviewer && panelAssignmentId == 0)
                phaseOrder = 0; // Unpermitted reviewer
            else if (panelAssignmentId > 0 && appAssignment != null &&
                    panelApplication.GetCurrentReviewStage() == ReviewStage.Asynchronous)
            {
                if (!(appAssignment.ClientAssignmentType.IsCoi))
                    phaseOrder = panelApplication.GetLastCompletedStepOrder(panelAssignmentId);
                else
                    phaseOrder = 0; // Unpermitted COI
            }
            return phaseOrder;
        }

        /// <summary>
        /// Populates the criteria reviewer score model.
        /// </summary>
        /// <param name="panApp">The pan application.</param>
        /// <param name="phaseOrder">The order in which the reviewer has completed critiques through</param>
        /// <returns>Populated list of CriteriaReviewerScoreModel web model</returns>
        internal static List<ICriteriaReviewerScoreModel> PopulateCriteriaReviewerScoreModel(PanelApplication panApp, int? phaseOrder)
        {
            //Get the asynchronous review stage
            List<ICriteriaReviewerScoreModel> dataList = new List<ICriteriaReviewerScoreModel>();
            IEnumerable<ApplicationWorkflowStep> appWorkflowSteps =
                panApp.GetAllAsyncWorkflowSteps();
            //Loop through each step and each criteria
            foreach (var stepElement in appWorkflowSteps.SelectMany(x => x.ApplicationWorkflowStepElements))
            {
                dataList.Add(new CriteriaReviewerScoreModel()
                {
                    CriteriaName = stepElement.GetCriteriaElementDescription(),
                    CriteriaSortOrder = stepElement.GetCriteriaSortOrder(),
                    PanelUserAssignmentId = stepElement.ApplicationWorkflowStep.ApplicationWorkflow.PanelUserAssignmentId ?? 0,
                    StepTypeId = stepElement.ApplicationWorkflowStep.StepTypeId,
                    OverallFlag = stepElement.IsOverall(),
                    Score = phaseOrder == null || stepElement.ApplicationWorkflowStep.StepOrder <= phaseOrder ?
                        ViewHelpers.ScoreFormatterNotCalculatedWithAbstain(stepElement.ContentScore(), stepElement.ScoreType(),
                            stepElement.ContentAdjectivalEquivalent(), stepElement.IsAbstained(), stepElement.IsResolved()) : string.Empty,
                    ApplicationWorkflowStepId = stepElement.ApplicationWorkflowStepId
                });
            }
            return dataList;
        }

        /// <summary>
        /// Populates the phase list for the asynchronous stage of a panel appplication's panel.
        /// </summary>
        /// <param name="panApp">The pan application.</param>
        /// <returns>Collection of Phase web model objects</returns>
        internal IEnumerable<IPhaseModel> PopulateAsyncStagePhaseList(PanelApplication panApp)
        {
            List<IPhaseModel> result = new List<IPhaseModel>();
            foreach (var phase in panApp.GetAllAsyncPanelSteps())
            {
                result.Add(new PhaseModel()
                {
                    StepTypeId = phase.StepTypeId,
                    SortOrder = phase.StepOrder,
                    PhaseName = phase.StepName
                });
            }
            return result;
        }

        /// <summary>
        /// Populates the assigned reviewer list.
        /// </summary>
        /// <param name="panApp">The pan application.</param>
        /// <returns>Collection of Reviewer model</returns>
        internal IEnumerable<IReviewerModel> PopulateReviewerList(PanelApplication panApp)
        {
            List<IReviewerModel> result = new List<IReviewerModel>();
            foreach (var rev in panApp.PanelApplicationReviewerAssignments.Where(x => !x.ClientAssignmentType.IsCoi && !x.ClientAssignmentType.IsReader))
            {
                result.Add(new ReviewerModel()
                {
                    PanelUserAssignmentId = rev.PanelUserAssignmentId,
                    SortOrder = rev.SortOrder ?? Int32.MaxValue,
                    FirstName = rev.PanelUserAssignment.FirstName(),
                    LastName = rev.PanelUserAssignment.LastName(),
                    AssignmentTypeAbbreviation = rev.AssignmentAbbreviation(),
                    ParticipantRole = rev.RoleName(),
                    UserId = rev.PanelUserAssignment.UserId
                });
            }
            return result;
        }

        /// <summary>
        /// Retrieves the discussion information.
        /// </summary>
        /// <param name="applicationStageStepId">The application stage step identifier.</param>
        /// <returns>
        /// DiscussionBoardModel representing a discussion with comments and participants
        /// </returns>
        public IDiscussionBoardModel RetreiveDiscussionInfo(int applicationStageStepId)
        {
            this.ValidateInteger(applicationStageStepId, "ApplicationScoringService.RetrieveDiscussionInfo", nameof(applicationStageStepId));
            var theDiscussionStep = this.UnitOfWork.ApplicationStageStepRepository.GetByID(applicationStageStepId);
            var theComments = GetDiscussionComments(theDiscussionStep);
            var theParticipants = GetParticipantInfo(theDiscussionStep);
            IDiscussionBoardModel result = new DiscussionBoardModel()
            {
                ApplicationStageStepId = theDiscussionStep.ApplicationStageStepId,
                ApplicationStageStepDiscussionId =
                    theDiscussionStep.ApplicationStageStepDiscussions.FirstOrDefault()?.ApplicationStageStepDiscussionId,
                LogNumber = theDiscussionStep.LogNumber(),
                DiscussionExists = theDiscussionStep.ApplicationStageStepDiscussions.Any(),
                PanelApplicationId = theDiscussionStep.ApplicationStage.PanelApplicationId,
                SessionPanelId = theDiscussionStep.ApplicationStage.PanelApplication.SessionPanelId,
                DiscussionComments = theComments.OrderByDescending(x => x.CommentCreationDate),
                Participants = theParticipants,
                IsModDone = PanelStageStep.IsModDone(theDiscussionStep.PanelStageStep)
        };
            return result;
        }

        /// <summary>
        /// Gets the participant information.
        /// </summary>
        /// <param name="theDiscussionStep">The discussion step.</param>
        /// <returns>The participant object</returns>
        internal IEnumerable<IDiscussionParticipantModel> GetParticipantInfo(ApplicationStageStep theDiscussionStep)
        {
            var result = new List<IDiscussionParticipantModel>();
            //add the assigned reviewers as participants of the discussion
            foreach (
                var reviewer in
                    theDiscussionStep.ApplicationStage.PanelApplication.PanelApplicationReviewerAssignments.Where(x => AssignmentType.CritiqueAssignments.Contains(x.ClientAssignmentType.AssignmentTypeId)))
            {
                result.Add(new DiscussionParticipantModel(reviewer.PanelUserAssignment.FirstName(), reviewer.PanelUserAssignment.LastName(), reviewer.PanelUserAssignment.ClientParticipantType.ParticipantTypeAbbreviation,
                    reviewer.SortOrder, reviewer.RoleName(), reviewer.PanelUserAssignment.PrimaryPhoneNumber(), reviewer.PanelUserAssignment.IsModerator(), false));
            }
            ICollection<PanelUserAssignment> panelUserAssignments = theDiscussionStep.ApplicationStage.PanelApplication.SessionPanel.PanelUserAssignments;
            //add the moderator(s) as a participant of the discussion
            foreach ( 
                var participant in panelUserAssignments.Where(x => x.IsModerator()))
            {
                result.Add(new DiscussionParticipantModel(participant.FirstName(), participant.LastName(), participant.ClientParticipantType?.ParticipantTypeAbbreviation,
                    null, participant.ClientRole?.RoleName, participant.PrimaryPhoneNumber(), participant.IsModerator(), false));
            }
            int panelApplicationId = theDiscussionStep.ApplicationStage.PanelApplicationId;
            //
            // and finally pick up the chair(s)
            //
            foreach (var participant in panelUserAssignments.Where(x => x.IsChair(panelApplicationId)))
            {
                result.Add(new DiscussionParticipantModel(participant.FirstName(), participant.LastName(), participant.ClientParticipantType?.ParticipantTypeAbbreviation,
                    null, participant.ClientRole?.RoleName, participant.PrimaryPhoneNumber(), false, true));
            }
            return result;
        }
        /// <summary>
        /// Can user participate in discussion
        /// </summary>
        /// <param name="applicationStageStepId">The application step identifier.</param>
        /// <param name="userId">The user identifier</param>
        /// <returns>true if the user can participate in the panel discussion; false otherwise</returns>
        public bool IsUserDiscussionParticipant(int applicationStageStepId, int userId)
        {
            var theDiscussionStep = this.UnitOfWork.ApplicationStageStepRepository.GetByID(applicationStageStepId);

            return IsUserDiscussionParticipant(theDiscussionStep, userId);
        }
        /// <summary>
        /// Can user participate in discussion
        /// </summary>
        /// <param name="theDiscussionStep">The discussion step.</param>
        /// <param name="userId">The user identifier</param>
        /// <returns>The participant object</returns>
        internal bool IsUserDiscussionParticipant(ApplicationStageStep theDiscussionStep, int userId)
        {
            var result = new List<int>();
            if (theDiscussionStep != null)
            {
                //add the assigned reviewers as participants of the discussion
                foreach (
                    var reviewer in
                        theDiscussionStep.ApplicationStage.PanelApplication.PanelApplicationReviewerAssignments.Where(
                            x =>
                                AssignmentType.CritiqueAssignments.Contains(x.ClientAssignmentType.AssignmentTypeId) &&
                                x.PanelUserAssignment.UserId == userId))
                {
                    result.Add(userId);
                }
                //add the moderator(s) as a participant of the discussion
                foreach (
                    var participant in
                        theDiscussionStep.ApplicationStage.PanelApplication.SessionPanel.PanelUserAssignments.Where(
                            x => x.IsModerator() && x.UserId == userId))
                {
                    result.Add(userId);
                }
            }
            return result.Count > 0;
        }

        /// <summary>
        /// Gets the discussion comments.
        /// </summary>
        /// <param name="theDiscussionStep">The discussion step.</param>
        /// <returns>The discussion comments object</returns>
        internal IEnumerable<IDiscussionCommentModel> GetDiscussionComments(ApplicationStageStep theDiscussionStep)
        {
            var result = new List<IDiscussionCommentModel>();
            foreach (
                var comment in
                    theDiscussionStep.ApplicationStageStepDiscussions.DefaultIfEmpty(new ApplicationStageStepDiscussion()).First().ApplicationStageStepDiscussionComments)
            {
                result.Add(new DiscussionCommentModel(comment.AuthorInfo().FirstName, comment.AuthorInfo().LastName, comment.AuthorRole(), comment.AuthorPanelAssignment()?.ClientParticipantType.ParticipantTypeAbbreviation,
                    comment.AuthorPanelAssignment()?.ClientRole?.RoleName, comment.AuthorApplicationAssignment()?.SortOrder, comment.Comment, comment.CreatedDate, comment.IsAuthorModerator()));
            }
            return result;
        }
        /// <summary>
        /// Determine if critique has been submitted
        /// </summary>
        /// <param name="applicationWorkflowStepElementId"></param>
        /// <returns></returns>
        public bool IsResolved(int applicationWorkflowStepElementId)
        {
            ApplicationWorkflowStepElement applicationWorkflowStepElementEntity = UnitOfWork.ApplicationWorkflowStepElementRepository.GetByID(applicationWorkflowStepElementId);
            return applicationWorkflowStepElementEntity.IsResolved();
        }

        #endregion
        #region Helpers
        /// <summary>
        /// Verify the parameters for RetrievePreAssignmentApplications
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        private void VerifyRetrievePostAssignmentApplicationsParameters(int sessionPanelId, int userId)
        {
            ValidateInt(sessionPanelId, "ApplicationScoringService.RetrievePostAssignmentApplications", "sessionPanelId");
            ValidateInt(userId, "ApplicationScoringService.RetrievePostAssignmentApplications", "userId");
        }
        /// <summary>
        /// Verifies the retrieve chair assignment applications parameters.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        private void VerifyRetrieveChairAssignmentApplicationsParameters(int sessionPanelId, int userId)
        {
            ValidateInt(sessionPanelId, "ApplicationScoringService.RetrieveChairAssignmentApplications", "sessionPanelId");
            ValidateInt(userId, "ApplicationScoringService.RetrieveChairAssignmentApplications", "userId");
        }
        /// <summary>
        /// Validate parameters for SaveReviewersCritiquePostAssignment
        /// </summary>
        /// <param name="applicationWorkflowStepElement">ApplicationWorkflowStepElement entity identifier</param>
        /// <param name="userId">User entity identifier>User entity identifier/param>
        private void ValidateSaveReviewersCritiquePostAssignmentParameters(int applicationWorkflowStepElementId, int userId)
        {
            this.ValidateInteger(applicationWorkflowStepElementId, "ApplicationScoringService.SaveReviewersCritiquePostAssignment", "applicationWorkflowStepElementId");
            this.ValidateInteger(userId, "ApplicationScoringService.SaveReviewersCritiquePostAssignment", "userId");
        }
        /// <summary>
        /// Validate parameters for SaveReviewersIncompleteCritiquePostAssignmentAsAbstains
        /// </summary>
        /// <param name="applicationWorkflowStepElementIdCollection">Enumerable list of ApplicationWorkflowStepElement entity identifiers</param>
        /// <param name="userId">User entity identifier</param>
        private void ValidateSaveReviewersIncompleteCritiquePostAssignmentAsAbstainsParameters(ICollection<int> applicationWorkflowStepElementIdCollection, int userId)
        {
            this.ValidateCollection(applicationWorkflowStepElementIdCollection, "ApplicationScoringService.SaveReviewersIncompleteCritiquePostAssignmentAsAbstains", "applicationWorkflowStepElementIdCollection");
            this.ValidateInteger(userId, "ApplicationScoringService.SaveReviewersIncompleteCritiquePostAssignmentAsAbstains", "userId");
        }
        #endregion
 
    }
    ///// <summary>
    ///// Represents & determines a phase state for the display of critique icons
    ///// </summary>
    //internal static class PhaseStateMachine
    //{
    //    #region Interal Classes
    //    /// <summary>
    //    /// Description of a PhaseState table entry.  
    //    /// </summary>
    //    public class PhaseState2
    //    {
    //        /// <summary>
    //        /// The PhaseState to return
    //        /// </summary>
    //        public StateResult Result { get; set; }
    //        /// <summary>
    //        /// Is the reviewer assigned to the application?
    //        /// </summary>
    //        public bool AssignedToApplication { get; set; }
    //        /// <summary>
    //        /// Whether the critique has been submitted for the current phase
    //        /// </summary>
    //        public bool ApplicationCritiqueSubmitted { get; set; }
    //        /// <summary>
    //        /// Whether all assigned critiques has been submitted for the current phase
    //        /// </summary>
    //        public bool PhaseCritiqueSubmitted { get; set; }
    //        /// <summary>
    //        /// Whether the panel phase is open 
    //        /// </summary>
    //        public bool IsOpen { get; set; }
    //        /// <summary>
    //        /// Whether the panel phase is  reopened
    //        /// </summary>
    //        public bool IsReopened { get; set; }
    //        /// <summary>
    //        /// State as text.
    //        /// </summary>
    //        /// <returns></returns>
    //        public override string ToString()
    //        {
    //            //
    //            // TODO: format the state as a text string
    //            //
    //            return "This should not happen";
    //        }
    //    }
    //    #endregion
    //    #region Statics
    //    /// <summary>
    //    /// Enumeration of all states as shown in Confluence table on 
    //    /// page https://prsm-confluence.srahosting.com/display/p2rmis/My+Workspace
    //    /// </summary>
    //    private static readonly List<PhaseState2> StateTable = new List<PhaseState2>()
    //    {
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase101,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = true,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase102,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = true,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase103,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = false,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase104,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = false,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase105,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = true,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase106,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = true,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase107,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = false,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase108,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = false,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase109,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = true,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase110,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = true,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase111,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = false,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase112,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = false,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase113,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = true,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase114,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = true,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase115,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = false,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase116,
    //            AssignedToApplication = false,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = false,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase117,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = true,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase118,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = true,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase119,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = false,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase120,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = false,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase121,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = true,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase122,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = true,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase123,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = false,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase124,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = true,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = false,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase125,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = false,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase126,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = false,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase127,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = false,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase128,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = false,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase129,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = true,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase130,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = true,
    //            IsReopened = true
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase131,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = true,
    //            IsOpen = true,
    //            IsReopened = false
    //        },
    //        new PhaseState2
    //        {
    //            Result = StateResult.Phase131,
    //            AssignedToApplication = true,
    //            ApplicationCritiqueSubmitted = false,
    //            PhaseCritiqueSubmitted = false,
    //            IsOpen = true,
    //            IsReopened = false
    //        }
    //    };
    //    #endregion
    //    #region Services
    //    /// <summary>
    //    /// Determines the user's Phase state
    //    /// </summary>
    //    /// <param name="assignedToApplication">Indicates if the user is assigned to the application</param>
    //    /// <param name="applicationCritiqueSubmitted">Indicates if the user's critique for the application has been submitted</param>
    //    /// <param name="phaseCritiqueSubmitted">Indicates if the user has submitted all assigned critiques for the current phase</param>
    //    /// <param name="isOpen">Indicates if the current phase is open(but not reopened)</param>
    //    /// <param name="isReopened">Indicates if the current phase is reopened</param>
    //    /// <returns>StateResult ENUM representing the user state in the phase.</returns>
    //    public static StateResult PhaseState(bool assignedToApplication, bool applicationCritiqueSubmitted, bool phaseCritiqueSubmitted, bool isOpen, bool isReopened)
    //    {
    //        //
    //        // Find where this entry is in the state table.  The StateTable should contain every combination of the parameters that will result in a phase of interest.
    //        //
    //        var result = StateTable.Where(x => (
    //                                            (x.ApplicationCritiqueSubmitted == applicationCritiqueSubmitted) &&
    //                                            (x.AssignedToApplication == assignedToApplication) &&
    //                                            (x.PhaseCritiqueSubmitted == phaseCritiqueSubmitted) &&
    //                                            (x.IsOpen == isOpen) &&
    //                                            (x.IsReopened == isReopened)
    //                                           ));

    //        return (result.Count() == 1) ? result.First().Result : StateResult.Default;
    //    }
    //    #endregion
    //}
}
