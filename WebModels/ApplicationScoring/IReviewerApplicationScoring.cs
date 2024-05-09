using System;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Interface object returned for results of ReviewerApplicationScoring
    /// </summary>
    public interface IReviewerApplicationScoring: IBaseAssignmentModel
    {
        #region Display Values
        /// <summary>
        /// Reviewer assignment
        /// </summary>
        string AppAssign { get; set; }
        /// <summary>
        /// Application Status
        /// </summary>
        string ApplicationStatus { get; set; }
        /// <summary>
        /// Order of review
        /// </summary>
        Nullable<int> ReviewOrder { get; set; }
        /// <summary>
        /// Whether the user has been assigned
        /// </summary>
        bool Assigned { get; set; }
        /// <summary>
        /// Whether the user has critiques over the application
        /// </summary>
        bool HasCritiques { get; set; }
        /// <summary>
        /// Whether the application has comments
        /// </summary>
        bool HasApplicationComments { get; set; }
        /// <summary>
        /// Whether the panel's end date has expired
        /// </summary>
        bool PanelEndDateHasExpired { get; set; }
        /// <summary>
        /// Whether the assignment type is COI
        /// </summary>
        bool IsCoi { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IReviewerApplicationScoring"/> is blinded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if blinded; otherwise, <c>false</c>.
        /// </value>
        bool Blinded { get; set; }
        /// <summary>
        /// Indicates if the phase is an MOD (moderated on-line discussion)
        /// </summary>
        bool IsModPhase { get; set; }
        /// <summary>
        /// Indicates if the MOD can be started.
        /// </summary>
        bool IsModReady { get; set; }
        /// <summary>
        /// Indicates if the MOD is active
        /// </summary>
        bool IsModActive { get; set; }
        /// <summary>
        /// Indicates if the MOD is closed
        /// </summary>
        bool IsModClosed { get; set; }
        #endregion
        #region Entity Identifiers
        /// <summary>
        /// Whether the application is currently in scoring or active status
        /// </summary>
        bool IsScoringOrActive { get; set; }
        /// <summary>
        /// Whether the application is currently in scoring status
        /// </summary>
        bool IsScoring { get; set; }
        /// <summary>
        /// ApplicationStageStep entity identifier
        /// </summary>
        int ApplicationStageStepId { get; set; }
        /// <summary>
        /// ApplicationStageStepDiscussion identifier.  Container which holds the conversation.
        /// </summary>
        int? ApplicationStageStepDiscussionId { get; set; }
        /// <summary>
        /// Indicates if a MOD exists
        /// </summary>
        bool IsThereDiscussion { get; }
        /// <summary>
        /// Indicates if requesting user is a chairperson
        /// </summary>
        bool IsChairperson { get; }
        #endregion
    }
}
