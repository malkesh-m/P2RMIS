using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.Web.ViewModels;
using Newtonsoft.Json;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// Scorable application view model
    /// </summary>
    public class ScorableApplicationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScorableApplicationViewModel"/> class.
        /// </summary>
        /// <param name="assignmentModel">The assignment model.</param>
        public ScorableApplicationViewModel(IReviewerApplicationScoring assignmentModel)
        {
            ReviewOrder = assignmentModel.ReviewOrder;
            AppAssign = assignmentModel.AppAssign;
            LogNumber = assignmentModel.ApplicationLogNumber;
            ApplicationId = assignmentModel.ApplicationId;
            Title = assignmentModel.Title;
            PI = assignmentModel.Blinded ? ViewHelpers.Constants.Blinded :
                ViewHelpers.ConstructName(assignmentModel.PILastName, assignmentModel.PIFirstName);
            Mechanism = assignmentModel.Mechanism;
            IsCoi = assignmentModel.IsCoi;
            ApplicationStatus = assignmentModel.ApplicationStatus;
            PanelApplicationId = assignmentModel.PanelApplicationId;
            SessionPanelId = assignmentModel.SessionPanelId;
            ApplicationStageStepId = assignmentModel.ApplicationStageStepId;
            CanViewApplication = !IsCoi;
            CanAccessScoring = !IsCoi && assignmentModel.IsScoring;
            CanAccessDiscussionBoard = OnLineDiscussions.AccessDiscussionBoard(assignmentModel.Assigned, assignmentModel.IsModActive, 
                assignmentModel.IsModClosed, assignmentModel.IsModPhase);
            HasAssigned = assignmentModel.Assigned;
            HasCritiques = assignmentModel.HasCritiques;
            IsSessionPanelActive = !assignmentModel.PanelEndDateHasExpired;
            NeedCritiqueAction = CanViewApplication && HasAssigned && !HasCritiques;
        }

        /// <summary>
        /// Reviewer assignment
        /// </summary>
        [JsonProperty("appAssign")]
        public string AppAssign { get; set; }
        /// <summary>
        /// Application log number
        /// </summary>
        [JsonProperty("logNumber")]
        public string LogNumber { get; set; }
        /// <summary>
        /// Application title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// Principal investigator's name
        /// </summary>
        [JsonProperty("pi")]
        public string PI { get; set; }
        /// <summary>
        /// Mechanism
        /// </summary>
        [JsonProperty("mechanism")]
        public string Mechanism { get; set; }
        /// <summary>
        /// Application Status
        /// </summary>
        [JsonProperty("applicationStatus")]
        public string ApplicationStatus { get; set; }
        /// <summary>
        /// Order of review
        /// </summary>
        [JsonProperty("order")]
        public int? ReviewOrder { get; set; }
        /// <summary>
        /// Whether the user has critiques over the application
        /// </summary>
        [JsonProperty("hasCritiques")]
        public bool HasCritiques { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is session panel active.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is session panel active; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("isSessionPanelActive")]
        public bool IsSessionPanelActive { get; set; }
        /// <summary>
        /// Whether the assignment type is COI
        /// </summary>
        [JsonProperty("isCoi")]
        public bool IsCoi { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can view application.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can view application; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("canViewApplication")]
        public bool CanViewApplication { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can access scoring.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can access scoring; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("canAccessScoring")]
        public bool CanAccessScoring { get; set; }
        /// <summary>
        /// PanelApplication entity identifier
        /// </summary>
        [JsonProperty("panelApplicationId")]
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// Application entity identifier
        /// </summary>
        [JsonProperty("applicationId")]
        public int ApplicationId { get; set; }
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        [JsonProperty("sessionPanelId")]
        public int SessionPanelId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has assigned.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has assigned; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("hasAssigned")]
        public bool HasAssigned { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance can access discussion board.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can access discussion board; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("canAccessDiscussionBoard")]
        public bool CanAccessDiscussionBoard { get; private set; }
        /// <summary>
        /// Gets the application stage step identifier.
        /// </summary>
        /// <value>
        /// The application stage step identifier.
        /// </value>
        [JsonProperty("applicationStageStepId")]
        public int ApplicationStageStepId { get; private set; }
        /// <summary>
        /// Gets a value indicating whether [need critique action].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [need critique action]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("needCritiqueAction")]
        public bool NeedCritiqueAction { get; private set; }
    }
}