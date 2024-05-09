using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.PanelManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the Critique page
    /// </summary>
    public class CritiqueViewModel
    {
        /// <summary>
        /// Initializes a new instance of the CritiqueViewModel class.
        /// </summary>
        public CritiqueViewModel() 
        {
            //
            // By default the link is enabled.
            //
            this.IsCurrentAssignmentEnabled = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CritiqueViewModel"/> class.
        /// </summary>
        /// <param name="reviewerCritiquesList">The reviewer critiques list.</param>
        /// <param name="applicationInformation">The application information.</param>
        /// <param name="critiquePhase">The critique phase.</param>
        /// <param name="critiqueReviewerOrderedList">The critique reviewer ordered list.</param>
        /// <param name="showCritiqueIcons">The show critique icons.</param>
        /// <param name="isCurrentAssignmentEnabled">Indicates if the CurrentAssignments link is enabled.</param>
        /// <param name="modStatus">Online Discussion Board status</param>
        /// <param name="clientScoringScaleLegend">ClientScoringScaleLegendModel for this critique</param>
        /// <param name="panelApplicationId">Panel application identifier.</param>
        /// <param name="sessionPanelId">Session panel identifier.</param>
        public CritiqueViewModel(List<IReviewerCritiques> reviewerCritiquesList, IApplicationInformationModel applicationInformation, IEditCritiquePhaseModel critiquePhase,
        List<ICritiqueReviewerOrderedModel> critiqueReviewerOrderedList, CritiqueIconControlModel showCritiqueIcons, bool isCurrentAssignmentEnabled, 
            IPanelApplicaitonModeStatus modStatus, ClientScoringScaleLegendModel clientScoringScaleLegend, int panelApplicationId, int sessionPanelId)
        {
            ReviewerCritiquesList = reviewerCritiquesList.ConvertAll(x => new ReviewerCritiquesViewModel(x, clientScoringScaleLegend, panelApplicationId, sessionPanelId));
            ApplicationInformation = new ApplicationInformationViewModel(applicationInformation, critiquePhase, critiqueReviewerOrderedList);
            IsScoreCardEnabled = showCritiqueIcons.IsScoreCardEnabled;
            IsCommentsEnabled = showCritiqueIcons.CanAddEditComments || showCritiqueIcons.CanAViewComments;
            IsReviewer = showCritiqueIcons.IsReviewer;
            IsClient = showCritiqueIcons.IsClient;
            CritiqueLastUpdateDate =
                reviewerCritiquesList.Any(x => x.IsCurrentUser) ?
                reviewerCritiquesList.First(x => x.IsCurrentUser).Critiques.Max(x => x.LastUpdateDate) : null;
            ShowSubmitMessage =
                reviewerCritiquesList.Any(x => x.IsCurrentUser) ?
                reviewerCritiquesList.First(x => x.IsCurrentUser).ShowCritiqueSumbitMessage : false;
            NoScoresAvailable = !reviewerCritiquesList.SelectMany(x => x.Critiques)
                    .SelectMany(x => x.PhaseScores)
                    .Any(x => x.Score != string.Empty && x.Score != ViewHelpers.Constants.ScoreNotApplicableMessage && x.IsSubmitted);
            CanSubmit =
                reviewerCritiquesList.Any(x => x.CanEdit && (!x.IsPanelStarted || (x.IsCurrentReviewStageAsync && !x.IsComplete)));
            this.IsCurrentAssignmentEnabled = isCurrentAssignmentEnabled;
            ApplicationStageStepId = modStatus.ApplicationStageStepId;
        }      

        /// <summary>
        /// List of reviewer critique information
        /// </summary>
        public List<ReviewerCritiquesViewModel> ReviewerCritiquesList { get; private set; }

        /// <summary>
        /// Application information
        /// </summary>
        public ApplicationInformationViewModel ApplicationInformation { get; private set; }

        /// <summary>
        /// Application status
        /// </summary>
        public string ApplicationStatus { get; set; }

        /// <summary>
        /// Whether the Comments feature is enabled
        /// </summary>
        public bool IsCommentsEnabled { get; private set; }
        
        /// <summary>
        /// Whether the Scoreboard feature is enabled
        /// </summary>
        public bool IsScoreboardEnabled
        {
            get
            {
                return ((ApplicationStatus == Routing.MyWorkspace.Status.Scoring) || (ApplicationStatus == Routing.MyWorkspace.Status.Active));
            } 
        }

        /// <summary>
        /// Whether the Score Card feature is enabled
        /// </summary>
        public bool IsScoreCardEnabled { get; private set; }

        /// <summary>
        /// Panel application identifier
        /// </summary>
        public int PanelApplicationId { get; set; }

        /// <summary>
        /// Session panel identifier
        /// </summary>
        public int SessionPanelId { get; set; }

        /// <summary>
        /// User identifier
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Last page's URL
        /// </summary>
        public string LastPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the critique un-submitted message.
        /// </summary>
        /// <value>
        /// The critique un-submitted message.
        /// </value>
        public string CritiqueUnsubmittedMessage
        {
            get
            {
                string workflowStepName = ReviewerCritiquesList.Any(x => x.IsCurrentUser) ?
                    ReviewerCritiquesList.First(x => x.IsCurrentUser).WorkflowStepName : string.Empty;
                return
                    !string.IsNullOrEmpty(workflowStepName) ?
                        string.Format(MessageService.Constants.UnsubmittedCritiqueNotification, workflowStepName) +
                        (CritiqueLastUpdateDate != null ? 
                            string.Format(MessageService.Constants.LastUpdateDateMessage, CritiqueLastUpdateDate) : string.Empty) : string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the critique last update date.
        /// </summary>
        /// <value>
        /// The critique last update date.
        /// </value>
        public DateTime? CritiqueLastUpdateDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is reviewer critique submitted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is reviewer critique submitted; otherwise, <c>false</c>.
        /// </value>
        public bool ShowSubmitMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether no scores are available.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [no scores available]; otherwise, <c>false</c>.
        /// </value>
        public bool NoScoresAvailable { get; set; }

        /// <summary>
        /// Get reviewers and their critiques for the UI display
        /// Gets or sets a value indicating whether this instance can submit.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can submit; otherwise, <c>false</c>.
        /// </value>
        public bool CanSubmit { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can access discussion board.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can access discussion board; otherwise, <c>false</c>.
        /// </value>
        public bool CanAccessDiscussionBoard { get; set; }
        /// <summary>
        /// Gets the application stage step identifier.
        /// </summary>
        /// <value>
        /// The application stage step identifier.
        /// </value>
        public int ApplicationStageStepId { get; private set; }
        //
        // Indicates if the CurrentAssignemt link is enabled.  
        //
        public bool IsCurrentAssignmentEnabled { get; private set; }
        /// <summary>
        /// Indicates if the user is a reviewer 
        /// </summary>
        public bool IsReviewer { get; private set; }
        /// <summary>
        /// Indicates if the user is a client 
        /// </summary>
        public bool IsClient { get; private set; }
        /// <summary>
        /// Replaces the HTML paragraph markings that were removed when the critique text was saved.
        /// </summary>
        public void InjectHtmlParagraphMarkers()
        {
            //
            // Need to replace all of the paragraph markers in each Critique content.  First
            // we select all of the critiques.
            //
            this.ReviewerCritiquesList.SelectMany(x => x.Critiques).
                //
                // Then we convert the IEnumerable to a list because ForEach() is only on a List
                //
                ToList().
                //
                // Then we just convert each content.
                //
                ForEach(y => y.ReplaceParagraphMarkers());
        }
        /// <summary>
        /// Reset the IsReviewer flag.
        /// </summary>
        public void SetIsReviewerForPanelmanagement()
        {
            IsReviewer = false;
        }
        /// <summary>
        /// Sets the appropriate ReviewerCritiqueViewModel to the current reviewer
        /// matching the parameter
        /// </summary>
        /// <param name="userId">User entity identifier to set</param>
        public void SelectThisReviewer(int userId)
        {
            var a = this.ReviewerCritiquesList.FirstOrDefault(x => x.ReviewerId == userId);
            if (a != null)
            {
                a.SetIsCurrentReviewer();
            }
        }
    }
}