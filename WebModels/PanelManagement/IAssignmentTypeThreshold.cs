using System;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Assignment Type Threshold
    /// </summary>
    public interface IAssignmentTypeThreshold
    {
        /// <summary>
        /// Gets or sets the assignment type threshold identifier.
        /// </summary>
        /// <value>
        /// The assignment type threshold identifier.
        /// </value>
        int AssignmentTypeThresholdId { get; set; }
        /// <summary>
        /// Gets or sets the session panel identifier.
        /// </summary>
        /// <value>
        /// The session panel identifier.
        /// </value>
        int SessionPanelId { get; set; }
        /// <summary>
        /// Gets or sets the scientist reviewer sort order.
        /// </summary>
        /// <value>
        /// The scientist reviewer sort order.
        /// </value>
        int? ScientistReviewerSortOrder { get; set; }
        /// <summary>
        /// Gets or sets the advocate consumer sort order.
        /// </summary>
        /// <value>
        /// The advocate consumer sort order.
        /// </value>
        int? AdvocateConsumerSortOrder { get; set; }
        /// <summary>
        /// Gets or sets the specialist reviewer sort order.
        /// </summary>
        /// <value>
        /// The specialist reviewer sort order.
        /// </value>
        int? SpecialistReviewerSortOrder { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can support specialist reviewer.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can support specialist reviewer; otherwise, <c>false</c>.
        /// </value>
        bool CanSupportSpecialistReviewer { get; set; }
    }
}