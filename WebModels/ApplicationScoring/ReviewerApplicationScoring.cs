using System;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Base model object returned for results of Reviewer ApplicationScoring
    /// </summary>
    public class ReviewerApplicationScoring : IReviewerApplicationScoring
    {
        #region 
        /// <summary>
        /// Populate a ReviewerApplicationScoring web model
        /// </summary>
        /// <param name="reviewOrder">Application review order</param>
        /// <param name="appAssign"></param>
        /// <param name="applicationLogNumber">Application log number</param>
        /// <param name="title">Application title</param>
        /// <param name="piFirstName">Principal investigator first name</param>
        /// <param name="piLastName">Principal investigator last name</param>
        /// <param name="mechanism"></param>
        /// <param name="appStatus">Application status</param>
        /// <param name="applicationId">Application entity identifier</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="isModPhase">MOD phase indicator (Indicates if the phase is a MOD phase)</param>
        /// <param name="isModReady">MOD phase ready indicator (Is MOD ready to be started)</param>
        /// <param name="isModActive">MOD phase is active (>1 comment)</param>
        /// <param name="isModClosed">MOD phase is closed (phase is closed</param>
        /// <param name="isThereDiscussion">Indicates if a discussion exists (>1 comment)</param>
        /// <param name="isChairperson">Indicates if the requesting user is a chairperson</param>
        public void Populate(int? reviewOrder, string appAssign, string applicationLogNumber, string title, string piFirstName, string piLastName, string mechanism, 
            string appStatus, int applicationId, int panelApplicationId, int sessionPanelId, bool blinded,
            bool isModPhase, bool isModReady, bool isModActive, bool isModClosed, int applicationStageStepId, int? applicationStageStepDiscussionId, 
            bool isThereDiscussion, bool isChairperson)
        {
            //
            // Populate the display values
            //
            ReviewOrder = reviewOrder;
            AppAssign = appAssign;
            ApplicationLogNumber = applicationLogNumber;
            Title = title;
            PIFirstName = piFirstName;
            PILastName = piLastName;
            Mechanism = mechanism;
            ApplicationStatus = appStatus;
            //
            // Set the indexes values
            //
            ApplicationId = applicationId;
            PanelApplicationId = panelApplicationId;
            SessionPanelId = sessionPanelId;
            Blinded = blinded;
            //
            // Set the MOD values
            //
            IsModPhase = isModPhase;
            IsModReady = isModReady;
            IsModActive = isModActive;
            IsModClosed = isModClosed;
            ApplicationStageStepId = applicationStageStepId;
            ApplicationStageStepDiscussionId = applicationStageStepDiscussionId;
            IsThereDiscussion = isThereDiscussion;
            IsChairperson = isChairperson;
        }

        /// <summary>
        /// Populate the properties that determine if the action icons are enabled or disabled
        /// </summary>
        /// <param name="isReviewer">Indicates if the user is a reviewer</param>
        /// <param name="isCoi">Indicates if the user has a conflict of interest (COI)</param>
        /// <param name="hasPanelEndDateExpired">Indicates if the panel has ended</param>
        /// <param name="hasApplicationComments">Indicates if the application has comments</param>
        /// <param name="hasCritiques">Indicates if the application has critiques</param>
        /// <param name="isScoring">Indicates if the application is currently in scoring status</param>
        /// <param name="isActive">Indicates if the application is currently in active status</param>
        public void PopulateActions(bool isReviewer, bool isCoi, bool hasPanelEndDateExpired, bool hasApplicationComments, bool hasCritiques, bool isScoring, bool isActive)
        {
            //
            // Populate the properties that control the icons
            //
            this.Assigned = isReviewer;
            this.IsCoi = isCoi;
            this.PanelEndDateHasExpired = hasPanelEndDateExpired;
            this.HasApplicationComments = hasApplicationComments;
            this.HasCritiques = hasCritiques;
            this.IsScoring = isScoring;
            this.IsScoringOrActive = isScoring || isActive;
        }
        #endregion
        #region Display Values
        /// <summary>
        /// Reviewer assignment
        /// </summary>
        public string AppAssign { get; set; }
        /// <summary>
        /// Application log number
        /// </summary>
        public string ApplicationLogNumber { get; set; }
        /// <summary>
        /// Application title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Principal investigator first name
        /// </summary>
        public string PIFirstName { get; set; }
        /// <summary>
        /// Principal investigator last name
        /// </summary>
        public string PILastName { get; set; }
        /// <summary>
        /// Mechanism
        /// </summary>
        public string Mechanism { get; set; }
        /// <summary>
        /// Application Status
        /// </summary>
        public string ApplicationStatus { get; set; }
        /// <summary>
        /// Order of review
        /// </summary>
        public Nullable<int> ReviewOrder { get; set; }        
        /// <summary>
        /// Whether the user has been assigned
        /// </summary>
        public bool Assigned { get; set; }
        /// <summary>
        /// Whether the user has critiques over the application
        /// </summary>
        public bool HasCritiques { get; set; }
        /// <summary>
        /// Whether the application has comments
        /// </summary>
        public bool HasApplicationComments { get; set; }
        /// <summary>
        /// Whether the panel's end date has expired
        /// </summary>
        public bool PanelEndDateHasExpired { get; set; }
        /// <summary>
        /// Whether the assignment type is COI
        /// </summary>
        public bool IsCoi { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IReviewerApplicationScoring" /> is blinded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if blinded; otherwise, <c>false</c>.
        /// </value>
        public bool Blinded { get; set; }
        /// <summary>
        /// Whether the application is currently in scoring or active status
        /// </summary>
        public bool IsScoringOrActive { get; set; }
        /// <summary>
        /// Whether the application is currently in scoring status
        /// </summary>
        public bool IsScoring { get; set; }
        /// <summary>
        /// Indicates if the phase is an MOD (moderated on-line discussion)
        /// </summary>
        public bool IsModPhase { get; set; }
        /// <summary>
        /// Indicates if the MOD can be started.
        /// </summary>
        public bool IsModReady { get; set; }
        /// <summary>
        /// Indicates if the MOD is active
        /// </summary>
        public bool IsModActive { get; set; }
        /// <summary>
        /// Indicates if the MOD is closed
        /// </summary>
        public bool IsModClosed { get; set; }
        /// <summary>
        /// Indicates if a MOD exists
        /// </summary>
        public bool IsThereDiscussion { get; private set; }
        /// <summary>
        /// Indicates if requesting user is a chairperson
        /// </summary>
        public bool IsChairperson { get; private set; }
        #endregion
        #region Entity Identifiers
        /// <summary>
        /// Gets or sets the name of the session panel.
        /// </summary>
        /// <value>
        /// The name of the session panel.
        /// </value>
        public string SessionPanelName { get; set; }
        /// <summary>
        /// PanelApplication entity identifier
        /// </summary>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// Application entity identifier
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        public int SessionPanelId { get; set; }
        /// <summary>
        /// Gets or sets the abbreviation of the session panel.
        /// </summary>
        /// <value>
        /// The abbreviation of the session panel.
        /// </value>
        public string SessionPanelAbbreviation { get; set; }
        /// <summary>
        /// Panel user assignment identifier
        /// </summary>
        public int? PanelUserAssignmentId { get; set; }
        /// <summary>
        /// ApplicationStageStep entity identifier
        /// </summary>
        public int ApplicationStageStepId { get; set; }
        /// <summary>
        /// ApplicationStageStepDiscussion identifier.  Container which holds the conversation.
        /// </summary>
        public int? ApplicationStageStepDiscussionId { get; set; }
        #endregion
    }
}
