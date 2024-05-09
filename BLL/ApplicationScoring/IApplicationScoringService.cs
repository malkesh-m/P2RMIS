using System.Collections.Generic;
using Sra.P2rmis.Bll.Mail;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.SummaryStatement;
using Sra.P2rmis.Bll.ModelBuilders.ApplicationScoring;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// ApplicationScoringService provides services to perform business related functions for
    /// the ApplicationScoring Application.
    /// </summary>
    public interface IApplicationScoringService: IServerBase
    {
        /// <summary>
        /// Retrieve the information to populate the Reviewer "Ready to Review" application scoring grid.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <returns>Container of IReviewerApplicationScoring from the identified SessionPanel</returns>
        Container<IReviewerApplicationScoring> ListReviewerReadyForReview(int sessionPanelId, int userId);
        /// <summary>
        /// Gets active or scoring application
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        KeyValuePair<int, string> GetActiveOrScoringApplication(int sessionPanelId);
        /// <summary>
        /// Get application status
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>The status in string format</returns>
        string GetApplicationStatus(int panelApplicationId);
        /// <summary>
        /// Save or update a reviewer's critique.
        /// </summary>
        /// <param name="applicationWorkflowStepElemenContentId">ApplicationWorkflowStepElementContent entity identifier</param>
        /// <param name="contentText">Critique text</param>
        /// <param name="userId">User entity identifier</param>
        void SaveReviewersCritique(int applicationWorkflowStepElemenContentId, string contentText, int userId);
        /// <summary>
        /// List the panel application's critiques.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">Current user entity identifier</param>
        /// <returns>Container of IReviewerCritiques</returns>
        Container<ReviewerScores> ListApplicationScores(int panelApplicationId, int userId);
        /// <summary>
        /// Save or update a reviewer's critique.
        /// </summary>
        /// <param name="applicationId">Application entity identifier</param>
        /// <param name="comment">Application comment</param>
        /// <param name="userId">User entity identifier</param>
        void AddComment(int applicationId, string comment, int userId);
        /// <summary>
        /// Retrieves user application comments for a specified application.
        /// </summary>
        /// <param name="panelApplicationId">Identifier for a Panel Application</param>
        /// <returns>Zero or more comments related to a panel application</returns>
        Container<ISummaryCommentModel> GetApplicationComments(int panelApplicationId);
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
        bool EditComment(int commentId, string comment, int userId);
        /// <summary>
        /// Delete a reviewers comment
        /// </summary>
        /// <param name="commentId">Comment entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        void DeleteComment(int commentId, int userId);
        /// <summary>
        /// Save a reviewer's score
        /// </summary>
        /// <param name="reviewerScores">Collection of reviewer scores</param>
        /// <param name="overallScores">Collection of reviewers overall scores (there will be only one)</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier for the application being scored</param>
        /// <param name="userId">User entity identifier</param>
        /// <param name="canUserEditAnyScoreCard">Whether the user can edit another user's final score card</param>
        void SaveScore(List<ReviewerScores> reviewerScores, List<ReviewerScores> overallScores, int panelApplicationId, int userId, bool canUserEditAnyScoreCard);
        /// <summary>
        /// Retrieves any scores that have changed since the last polling request.
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        List<ScoreCacheEntry> PollScore(int applicationId);
        /// <summary>
        /// Deactivate an application
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        void Deactivate(int panelApplicationId, int userId);
        /// <summary>
        /// Determines if any application is actively scoring for the session of the supplied panel.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <returns>If an application is scoring the log number is returned; null otherwise</returns>
        ActiveApplicationModel IsAnyApplicationBeingScored(int panelApplicationId);
        /// <summary>
        /// Determines if any application is actively scoring for the session of the supplied panel.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <returns>If an application is scoring the log number is returned; null otherwise</returns>
        ActiveApplicationModel IsAnyApplicationBeingScoredByPanel(int panelId);
        /// <summary>
        /// Change the applications status to triage
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        void Triage(int panelApplicationId, int userId);
        /// <summary>
        /// Change the applications status to Disapproved
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        void Disapproved(int panelApplicationId, int userId);
        /// <summary>
        /// Change the applications status to ready to score
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        void ReadyToScore(int panelApplicationId, int userId);
        /// <summary>
        /// Change the applications status to active
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        void Active(int panelApplicationId, int userId);
        /// <summary>
        /// Change the applications status to Scoring
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        void Scoring(int panelApplicationId, int userId);
        /// <summary>
        /// Abstain the identified reviewer.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="reviewerUserId">User entity identifier of the reviewer being abstained.</param>
        /// <param name="userId">User entity identifier</param>
        void MarkReviewierScoresAsAbstain(int panelApplicationId, int reviewerUserId, int userId);
        /// <summary>
        /// Return a list of COIs for the PanelApplication
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <returns><PanelApplication entity identifier/returns>
        Container<CoiModel> ListApplicationCoi(int panelApplicationId);
        /// <summary>
        /// Retrieves the pre-meeting scores for the indicated applicication, session panel and reviewer.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <param name="sessionPanelId">Identifier for an application's instance of a workflow</param>
        /// <param name="reviewerId">Identifier for an application's instance of a workflow</param>
        /// <returns>The eviewerApplicationPremeetingScoresModel object</returns>
        ReviewerApplicationPremeetingScoresModel GetApplicationPreMtgScoresForReviewer(int panelApplicationId);
        /// <summary>
        /// Retrieves the critique phase information for the critique edit header
        /// </summary>
        /// <param name="panelApplicationId">The panel aplication identifier</param>
        /// <returns></returns>
        EditCritiquePhaseModel GetCritiqueStageStep(int panelApplicationId);
        /// <summary>
        /// Gets the critique review order
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <returns>List of critique review ordered model objects</returns>
        List<ICritiqueReviewerOrderedModel> GetCritiqueReviewerOrder(int panelApplicationId);
        /// <summary>
        /// Checks whether the user can access their panel assignment due to having incomplete assignments
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sessionPanelId"></param>
        /// <returns>True if access is permitted; otherwise false;</returns>
        bool CanUserAccessPanelAssignment(int userId, int sessionPanelId);
        /// <summary>
        /// Creates and populates a web model containing data for PreAssignmentApplications Current Assignment grid
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of PreAssignmentModel objects</returns>
        Container<IPreAssignmentModel> RetrievePreAssignmentApplications(int sessionPanelId, int userId);
        /// <summary>
        /// Creates and populates a web model containing data for PostAssignmentApplications Current Assignment grid
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of PreAssignmentModel objects</returns>
        Container<IPostAssignmentModel> RetrievePostAssignmentApplications(int sessionPanelId, int userId);
        /// <summary>
        /// Retrieves the chair applications.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Container<IChairAssignmentModel> RetrieveChairApplications(int sessionPanelId, int userId);
        /// <summary>
        /// Determines whether [is chair person] [the specified session panel identifier].
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is chair person] [the specified session panel identifier]; otherwise, <c>false</c>.
        /// </returns>
        bool IsCpritChairPerson(int sessionPanelId, int userId);
        /// <summary>
        /// Calculates the model to control the visibility of the icons on the Critique edit page
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        CritiqueIconControlModel ShowCritiqueIcons(int panelApplicationId, int sessionPanelId, int userId);
        /// <summary>
        /// List the panel application's critiques.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">Current user entity identifier</param>
        /// <param name="isAdmin">Current user is an admin</param>
        /// <returns>Container of IReviewerCritiques</returns>
        Container<IReviewerCritiques> ListApplicationCritiquesForApplicationEvaluation(int panelApplicationId, int userId, bool isAdmin);
        /// <summary>
        /// Save or update a reviewer's critique during the post assignment phase.
        /// </summary>
        /// <param name="applicationWorkflowStepElement">ApplicationWorkflowStepElement entity identifier</param>
        /// <param name="applicationWorkflowStepElemenContentId">ApplicationWorkflowStepElementContent entity identifier</param>
        /// <param name="critiqueText">Critique text</param>
        /// <param name="score">Reviewer's score</param>
        /// <param name="abstain">Indicates if the user abstained from the critique</param>
        /// <param name="userId">User entity identifier</param>
        /// <param name="isPanelStarted">Indicates if the panel has been started</param>
        ISaveReviewersCritiquePostAssignmentResultsModel SaveReviewersCritiquePostAssignment(int applicationWorkflowStepElement, int applicationWorkflowStepElemenContentId, string critiqueText, decimal? score, bool abstain, bool isPanelStarted, int userId, string errorMessage);
        /// <summary>
        /// Determines if the critique (containing the supplied ApplicationWorkflowStepElemnt) is submittal.
        /// Submittal is defined as containing an entry for the ContextText for each criteria or an abstention.
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">ApplicationWorkflowStepElemnt entity identifier</param>
        /// <returns>Container of IIncompleteCriteriaNameModel objects listing any incomplete criteria.  Empty container list signifies all criteria have been saved.</returns>
        Container<IIncompleteCriteriaNameModel> CanSubmit(int applicationWorkflowStepElementId);
        /// <summary>
        /// Determines if the critique (containing the supplied ApplicationWorkflowStepElemnt) requires an overall rating
        /// If it does it checks if one has been supplied.
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">ApplicationWorkflowStepElemnt entity identifier</param>
        /// <returns></returns>
        bool HasOverall(int applicationWorkflowStepElementId);
        /// <summary>
        /// Save all incomplete review criteria as abstain.
        /// </summary>
        /// <param name="applicationWorkflowStepElementIdCollection">Enumerable list of ApplicationWorkflowStepElement entity identifiers</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of ISaveReviewersCritiquePostAssignmentResultsModel representing the created ApplicationWorkflowStepElementContent entities</returns>
        Container<ISaveReviewersCritiquePostAssignmentResultsModel> SaveReviewersIncompleteCritiquePostAssignmentAsAbstains(ICollection<int> applicationWorkflowStepElementIdCollection, int userId);
        /// <summary>
        /// Retrieves the assigned reviewers scores.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IReviewerCritiqueSummary RetrieveAssignedReviewersScores(int panelApplicationId, int userId);
        /// <summary>
        /// Validates the value entered into the Rating control.
        /// </summary>
        /// <param name="applicationWorkflowStepElementId">The Critique's ApplicationWorkflowStepElement entity identifier</param>
        /// <param name="rating">Rating value</param>
        /// <param name="isPanelStarted">Indicates if the panel has been started</param>
        /// <returns>True if the rating value is valid; false otherwise.</returns>
        RatingValidationModel IsRatingValid(int applicationWorkflowStepElementId, decimal? rating, bool isPanelStarted);
        /// <summary>
        /// Retrieve the critique from the specified workflow.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <param name="applicationWorkflowStepId">ApplicationWorkflowStep entity identifier</param>
        /// <param name="isCurrentUser">Whether the current critique page belongs to the logged in user</param>
        /// <param name="hasManageCritiquesPermission">Whether the user has permission to manage other user's critiques</param>
        /// <param name="hasViewCritiquesPermission">Whether the user has permission to view other user's critiques</param>
        /// <returns>Container of the workflow's critique</returns>
        Container<IReviewerCritiques> ListApplicationCritiqueForPanelManagement(int panelApplicationId, int userId, int applicationWorkflowStepId, bool isCurrentUser, bool hasManageCritiquesPermission, bool hasViewCritiquesPermission);
        /// <summary>
        /// Determines the MOD states of PanelApplication.
        /// </summary>
        /// <param name="panelApplicationEntityId">PanelApplication entity identifier</param>
        /// <returns>IPanelApplicaitonModeStatus object containing MOD status of the PanelApplication</returns>
        IPanelApplicaitonModeStatus PanelApplicationMODStatus(int panelApplicationEntityId);
        /// <summary>
        /// Retrieves the discussion information.
        /// </summary>
        /// <param name="applicationStageStepId">The application stage step identifier.</param>
        /// <returns>DiscussionBoardModel representing a discussion with comments and participants</returns>
        IDiscussionBoardModel RetreiveDiscussionInfo(int applicationStageStepId);
        /// <summary>
        /// Can user participate in discussion
        /// </summary>
        /// <param name="applicationStageStepId">The application step identifier.</param>
        /// <param name="userId">The user identifier</param>
        /// <returns>true if the user can participate in the panel discussion; false otherwise</returns>
        bool IsUserDiscussionParticipant(int applicationStageStepId, int userId);
        /// <summary>
        /// Sends the discussion notification.
        /// </summary>
        /// <param name="theMailService">The mail service.</param>
        /// <param name="applicationStageStepDiscussionCommentId">The application stage step discussion comment identifier.</param>
        /// <returns>Mailstatus regarding success or failure</returns>
        MailService.MailStatus SendDiscussionNotification(IMailService theMailService, int applicationStageStepDiscussionCommentId, bool isNew);
        /// <summary>
        /// Calculates the model to control the visibility of the icons on the Critique edit page
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        CritiqueIconControlModel ShowCritiqueIcons(int panelApplicationId, int userId);
        /// <summary>
        /// Retrieves the scoring status for the application.
        /// </summary>
        /// <param name="sessionPanelId"></param>
        /// <returns>Scoring status for the identified application; null if none exists</returns>
        Container<IApplicationStatusModel> RetrieveSessionApplicationScoringStatuses(int sessionPanelId);
        /// <summary>
        /// Determines if the meeting is active
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <returns>True if now is outside of the meeting dates; false otherwise</returns>
        bool IsSessionPanelOver(int sessionPanelId);
        /// <summary>
        /// determine if critique has been submitted
        /// </summary>
        /// <param name="applicationWorkflowStepElementId"></param>
        /// <returns></returns>
        bool IsResolved(int applicationWorkflowStepElementId);
    }
}
