using Sra.P2rmis.WebModels.Lists;
using System;
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Model object returned for results of a reviewer critiques.
    /// </summary>
    public interface IReviewerCritiques
    {
        /// <summary>
        /// First name of reviewer
        /// </summary>
        string ReviewerFirstName { get; set; }
        /// <summary>
        /// Last name of reviewer
        /// </summary>
        string ReviewerLastName { get; set; }
        /// <summary>
        /// Reviewers participant type (ie. scientist reviewer)
        /// </summary>
        string ParticipantType { get; set; }
        /// <summary>
        /// Participant's role
        /// </summary>
        string Role { get; set; }
        /// <summary>
        /// Reviewers order of review (1 of n reviewers)
        /// </summary>
        int? ReviewerSlot { get; set; }
        /// <summary>
        /// Reviewers critiques
        /// </summary>
        List<CritiqueContent> Critiques { get; set; }
        /// <summary>
        /// the current review stage of the critique
        /// </summary>
        int CurrentReviewStage { get; set; }
        /// <summary>
        /// Whether the current review stage is asynchronies
        /// </summary>
        bool IsCurrentReviewStageAsync { get; set; }
        /// <summary>
        /// Determines if critique is submitted or not
        /// </summary>
        bool IsComplete { get; set; }
        /// <summary>
        /// the phase the critique is in
        /// </summary>
        string WorkflowStepName { get; set; }
        /// <summary>
        /// Indicates if the user is the current signed-on user
        /// </summary>
        bool IsCurrentUser { get; set; }
        /// <summary>
        /// Reviewer user Id
        /// </summary>
        int ReviewerId { get; set; }
        /// <summary>
        /// Is the current user one of the assigned users
        /// </summary>
        bool IsAssignedUser { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this panel is started.
        /// </summary>
        /// <value>
        /// <c>true</c> if this panel is started; otherwise, <c>false</c>.
        /// </value>
        bool IsPanelStarted { get; }
        /// <summary>
        /// Gets or sets the previous application workflow steps.
        /// </summary>
        /// <value>
        /// The previous application workflow steps.
        /// </value>
        IList<ApplicationWorkflowStepModel> PrevApplicationWorkflowSteps { get; set; }
        #region PostAssignment specific Properties
        /// <summary>
        /// Indicates if the user can edit the critique
        /// </summary>
        bool CanEdit { get; }
        /// <summary>
        /// Date & Time the phases critique was last updated.
        /// </summary>
        DateTime? ModifiedDate { get; }
        /// <summary>
        /// Indicates if a message telling the user they need to submit their critique.
        /// </summary>
        bool ShowCritiqueSumbitMessage { get; }
        /// <summary>
        /// ApplicationWorkflow entity identifier
        /// </summary>
        int ApplicationWorkflowId { get; }
        /// <summary>
        /// Assignment abbreviation
        /// </summary>
        string AssignmentAbbreviation { get; }
        #endregion
    }
    /// <summary>
    /// Model object returned for results of a reviewer critiques.
    /// </summary>
    public class ReviewerCritiques : IReviewerCritiques
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        public ReviewerCritiques(int applicationWorkflowId, string reviewerFirstName, string reviewerLastName, string reviewerRole, int? reviewerSlot, string roleName, List<CritiqueContent> critiques, bool isComplete, string workflowStepName, int currentReviewStage, bool isCurrentReviewStageAsync, bool isCurrentUser, int reviewerId)
        {
            ReviewerFirstName = reviewerFirstName;
            ReviewerLastName = reviewerLastName;
            ParticipantType = reviewerRole;
            ReviewerSlot = reviewerSlot;
            Role = roleName;
            Critiques = critiques;
            WorkflowStepName = workflowStepName;
            CurrentReviewStage = currentReviewStage;
            IsCurrentReviewStageAsync = isCurrentReviewStageAsync;
            IsComplete = isComplete;
            IsCurrentUser = isCurrentUser;
            ReviewerId = reviewerId;
            ApplicationWorkflowId = applicationWorkflowId;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public ReviewerCritiques(int applicationWorkflowId, string reviewerFirstName, string reviewerLastName,
                string reviewerRole, int? reviewerSlot, string roleName,
                List<CritiqueContent> critiques, bool isComplete, string workflowStepName,
                int currentReviewStage, bool isCurrentReviewStageAsync, bool isCurrentUser,
                int reviewerId, bool canEdit, DateTime? modifiedDate,
                string assignmentAbbreviation, bool isPanelStarted) :
            this(applicationWorkflowId, reviewerFirstName, reviewerLastName, reviewerRole, reviewerSlot, roleName, critiques, isComplete, workflowStepName, currentReviewStage, isCurrentReviewStageAsync, isCurrentUser, reviewerId)
        {
            this.CanEdit = canEdit;
            this.ModifiedDate = modifiedDate;
            this.AssignmentAbbreviation = assignmentAbbreviation;
            this.IsPanelStarted = isPanelStarted;
        }
        #endregion
        /// <summary>
        /// First name of reviewer
        /// </summary>
        public string ReviewerFirstName { get; set; }
        /// <summary>
        /// Last name of reviewer
        /// </summary>
        public string ReviewerLastName { get; set; }
        /// <summary>
        /// Reviewers participant type
        /// </summary>
        public string ParticipantType { get; set; }
        /// <summary>
        /// Reviewers role name
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Reviewers order of review (1 of n reviewers)
        /// </summary>
        public int? ReviewerSlot { get; set; }
        /// <summary>
        /// Reviewers critiques
        /// </summary>
        public List<CritiqueContent> Critiques { get; set; }
        /// <summary>
        /// the current review stage of the critique
        /// </summary>
        public int CurrentReviewStage { get; set; }
        /// <summary>
        /// Whether the current review stage is asynchronies
        /// </summary>
        public bool IsCurrentReviewStageAsync { get; set; }
        /// <summary>
        /// determines if the critique has been submitted or not
        /// </summary>
        public bool IsComplete { get; set; }
        /// <summary>
        /// the phase the critique is in
        /// </summary>
        public string WorkflowStepName { get; set; }
        /// <summary>
        /// Indicates if the user is the current signed-on user
        /// </summary>
        public bool IsCurrentUser { get; set; }
        /// <summary>
        /// Reviewer user Id
        /// </summary>
        public int ReviewerId { get; set; }
        /// <summary>
        /// Is the current user one of the assigned users
        /// </summary>
        public bool IsAssignedUser { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this panel is started.
        /// </summary>
        /// <value>
        /// <c>true</c> if this panel is started; otherwise, <c>false</c>.
        /// </value>
        public bool IsPanelStarted { get; private set; }

        #region PostAssignment specific Properties
        /// <summary>
        /// Indicates if the user can edit the critique
        /// </summary>
        public bool CanEdit { get; private set; }
        /// <summary>
        /// Date & Time the phases critique was last updated.
        /// </summary>
        public DateTime? ModifiedDate { get; private set; }
        /// <summary>
        /// Indicates if a message telling the user they need to submit their critique.
        /// If they can edit it their critique has not been submitted.
        /// </summary>
        public bool ShowCritiqueSumbitMessage 
        { 
            get 
            {
                return this.CanEdit && !this.IsPanelStarted;
            }
        }
        /// <summary>
        /// ApplicationWorkflow entity identifier
        /// </summary>
        public int ApplicationWorkflowId { get; private set; }
        /// <summary>
        /// Assignment abbreviation
        /// </summary>
        public string AssignmentAbbreviation { get; private set; }
        /// <summary>
        /// Gets the previous application workflow steps.
        /// </summary>
        /// <value>
        /// The previous application workflow steps.
        /// </value>
        public IList<ApplicationWorkflowStepModel> PrevApplicationWorkflowSteps { get; set; }
        #endregion
    }
    /// <summary>
    /// Application workflow step model for critiques
    /// </summary>
    public class ApplicationWorkflowStepModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string StepName { get; set; }
        /// <summary>
        /// Gets or sets the application workflow step identifier.
        /// </summary>
        /// <value>
        /// The application workflow step identifier.
        /// </value>
        public int ApplicationWorkflowStepId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is completed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is completed; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompleted { get; set; }
    }

    /// <summary>
    /// Model object returned for results of individual reviewer critique.
    /// </summary>
    public interface ICritiqueContent
    {
        /// <summary>
        /// The Criterion
        /// </summary>
        string Criterion { get; }
        /// <summary>
        /// ClientElementId of criteria
        /// </summary>
        int ClientElementId { get; }
        /// <summary>
        /// Reviewer entered text.
        /// </summary>
        string Content { get; }
        /// <summary>
        /// Collection of all scores across all phases of review
        /// </summary>
        IEnumerable<CritiqueScore> PhaseScores { get; }
        /// <summary>
        /// Score for the current stage
        /// </summary>
        string Score { get; set; }
        /// <summary>
        /// Adjectival value of a score
        /// </summary>
        string AdjectivalScore { get; set; }
        /// <summary>
        /// Whether the criteria accepts a score
        /// </summary>
        bool IsCriteriaScored { get; }
        /// <summary>
        /// Whether the criteria accepts a critique
        /// </summary>
        bool IsCriteriaText { get; }
        /// <summary>
        /// Content entity identifier
        /// </summary>
        int ApplicationWorkflowStepElementContentId { get; }
        /// <summary>
        /// Whether the critique was revised during the current phase
        /// </summary>
        bool CritiqueRevised { get; }
        /// <summary>
        /// List of possible adjectiaval scores for this critique
        /// </summary>
        IEnumerable<AdjectivalScoreValue> ValidAdjectivalScores { get; }
        /// <summary>
        /// Adjectival score dropdown list for this critique
        /// </summary>
        List<IListEntry> AdjectivalScoreDropdown { get; }
        /// <summary>
        /// The currently selected adjectival value in the adjectival dropdown list
        /// </summary>
        int AdjectivalScoreDropdownId { get; set; }
        /// <summary>
        /// Instructions for evaluating this section of the critique
        /// </summary>
        string Instructions { get; }
        /// <summary>
        /// Reviewer abstained from scoring this section of the critique
        /// </summary>
        bool Abstain { get; set; }
        /// <summary>
        /// ApplicationWorkflowStepElement entity identifier
        /// </summary>
        int ApplicationWorkflowStepElementId { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is overall.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is overall; otherwise, <c>false</c>.
        /// </value>
        bool IsOverall { get; }
        /// <summary>
        /// Gets or sets the last update date for the content.
        /// </summary>
        /// <value>
        /// The last update date for the content.
        /// </value>
        DateTime? LastUpdateDate { get; set; }
        /// <summary>
        /// Score type
        /// </summary>
        string ScoreType { get; }
        /// <summary>
        /// Score high value
        /// </summary>
        decimal ScoreHighValue { get; }
        /// <summary>
        /// Score low value
        /// </summary>
        decimal ScoreLowValue { get; }
        /// <summary>
        /// Gets the recommended word limit.
        /// </summary>
        /// <value>
        /// The recommended word limit.
        /// </value>
        int? RecommendedWordLimit { get; }
    }
    /// <summary>
    /// Model object returned for results of individual reviewer critique.
    /// </summary>
    public class CritiqueContent : ICritiqueContent
    {
        #region Constructor & Setup

        /// <summary>
        /// Webmodel constructor
        /// </summary>
        /// <param name="criterion">Step criterion</param>
        /// <param name="clientElementId">ElementId for criterion</param>
        /// <param name="content">Step content</param>
        /// <param name="score">The critique score</param>
        /// <param name="isCriteriaScored">Whether criterion accepts scores</param>
        /// <param name="isCriteriaText">Whether criterion accepts text</param>
        /// <param name="applicationWorkflowStepElementContentId">ApplicationWorkflowStepElementContent entity identifier</param>
        /// <param name="critiqueRevised">The critique revised</param>
        /// <param name="adjectivalScore">The adjectival score</param>
        /// <param name="validAdjectivalScores">List of valid adjectival scores</param>
        /// <param name="instructions">Critique evaluation instructions</param>
        /// <param name="abstain">Reviewer abstains from scoring this section of the critique</param>
        /// <param name="applicationWorkflowStepElementId">ApplicationWorkflowStepElement entity identifier</param>
        /// <param name="recommendedWordLimit">Recommended word limit</param>
        /// <param name="sortOrder">Criteria sort order</param>

        public CritiqueContent(string criterion, int clientElementId, string content, string score, bool isCriteriaScored, bool isCriteriaText,
                                int applicationWorkflowStepElementContentId, bool critiqueRevised, string adjectivalScore, IEnumerable<AdjectivalScoreValue> validAdjectivalScores,
                                string instructions, bool abstain, int applicationWorkflowStepElementId, DateTime? lastUpdateDate, 
                                string scoreType, decimal scoreHighValue, decimal scoreLowValue, bool isOverall, int? recommendedWordLimit, int sortOrder)
        {
            this.Criterion = criterion;
            this.ClientElementId = clientElementId;
            this.Content = content;
            this.Score = score;
            this.IsCriteriaScored = isCriteriaScored;
            this.IsCriteriaText = isCriteriaText;
            this.ApplicationWorkflowStepElementContentId = applicationWorkflowStepElementContentId;
            this.CritiqueRevised = critiqueRevised;
            this.AdjectivalScore = adjectivalScore;
            this.ValidAdjectivalScores = validAdjectivalScores;
            this.BuildAdjectivalScoreDropdown();
            this.Instructions = instructions;
            this.Abstain = abstain;
            this.ApplicationWorkflowStepElementId = applicationWorkflowStepElementId;
            this.LastUpdateDate = lastUpdateDate;
            this.ScoreType = scoreType;
            this.ScoreHighValue = scoreHighValue;
            this.ScoreLowValue = scoreLowValue;
            this.IsOverall = isOverall;
            this.RecommendedWordLimit = recommendedWordLimit;
            this.SortOrder = sortOrder;
        }
        public void BuildAdjectivalScoreDropdown()
        {
            this.AdjectivalScoreDropdown = new List<IListEntry>();

            foreach (AdjectivalScoreValue item in ValidAdjectivalScores)
            {
                ListEntry row = new ListEntry(item.SortOrder, item.AdjectivalLabel);
                AdjectivalScoreDropdown.Add(row);
            }
         }
        #endregion
        #region Properties
        /// <summary>
        /// The Criterion
        /// </summary>
        public string Criterion { get; private set; }
        /// <summary>
        /// ClientElementId of criteria
        /// </summary>
        public int ClientElementId { get; private set; }
        /// <summary>
        /// Reviewer entered text.
        /// </summary>
        public string Content { get; private set; }
        /// <summary>
        /// Collection of all scores across all phases of review
        /// </summary>
        public IEnumerable<CritiqueScore> PhaseScores { get; set; }
        /// <summary>
        /// Score for the current stage
        /// </summary>
        public string Score { get; set; }
        /// <summary>
        /// Adjectival value of a score
        /// </summary>
        public string AdjectivalScore { get; set; }
        /// <summary>
        /// Whether the criteria accepts a score
        /// </summary>
        public bool IsCriteriaScored { get; private set; }
        /// <summary>
        /// Whether the criteria accepts a critique
        /// </summary>
        public bool IsCriteriaText { get; private set; }
        /// <summary>
        /// Content entity identifier
        /// </summary>
        public int ApplicationWorkflowStepElementContentId { get; private set; }
        /// <summary>
        /// Whether the critique was revised during the current phase
        /// </summary>
        public bool CritiqueRevised { get; private set; }
        /// <summary>
        /// List of possible adjectiaval scores for this critique
        /// </summary>
        public IEnumerable<AdjectivalScoreValue> ValidAdjectivalScores { get; set; }
        /// <summary>
        /// Adjectival score dropdown list for this critique
        /// </summary>
        public List<IListEntry> AdjectivalScoreDropdown { get; set; }
        /// <summary>
        /// The currently selected adjectival value in the adjectival dropdown list
        /// </summary>
        public int AdjectivalScoreDropdownId { get; set; }
        /// <summary>
        /// Instructions for evaluating this section of the critique
        /// </summary>
        public string Instructions { get; set; }
        /// <summary>
        /// Reviewer abstained from scoring this section of the critique
        /// </summary>
        public bool Abstain { get; set; }
        /// <summary>
        /// ApplicationWorkflowStepElement entity identifier
        /// </summary>
        public int ApplicationWorkflowStepElementId { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance is overall.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is overall; otherwise, <c>false</c>.
        /// </value>
        public bool IsOverall { get; private set; }

        /// <summary>
        /// Gets or sets the last update date for the content.
        /// </summary>
        /// <value>
        /// The last update date for the content.
        /// </value>
        public DateTime? LastUpdateDate { get; set; }
        /// <summary>
        /// Score type
        /// </summary>
        public string ScoreType { get; private set;}
        /// <summary>
        /// Score high value
        /// </summary>
        public decimal ScoreHighValue { get; private set; }
        /// <summary>
        /// Score low value
        /// </summary>
        public decimal ScoreLowValue { get; private set; }
        /// <summary>
        /// Gets the recommended word limit.
        /// </summary>
        /// <value>
        /// The recommended word limit.
        /// </value>
        public int? RecommendedWordLimit { get; private set; }
        /// <summary>
        /// Criteria sort order
        /// </summary>
        public int SortOrder { get; private set; }
        #endregion
    }
}
