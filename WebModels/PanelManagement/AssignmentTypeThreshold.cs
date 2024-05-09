
using System;

namespace Sra.P2rmis.WebModels.PanelManagement

{
    /// <summary>
    /// Data model for AssignmentType DropdownList in panel management
    /// </summary>
    public class AssignmentTypeThreshold : IAssignmentTypeThreshold
    {

        public AssignmentTypeThreshold() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentTypeThreshold"/> class.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="scientistReviewerSortOrder">The scientist reviewer sort order.</param>
        /// <param name="advocateConsumerSortOrder">The advocate consumer sort order.</param>
        /// <param name="specialistReviewerSortOrder">The specialist reviewer sort order.</param>
        public AssignmentTypeThreshold(int sessionPanelId, int? scientistReviewerSortOrder, int? advocateConsumerSortOrder,
            int? specialistReviewerSortOrder)
        {
            SessionPanelId = sessionPanelId;
            ScientistReviewerSortOrder = scientistReviewerSortOrder;
            AdvocateConsumerSortOrder = advocateConsumerSortOrder;
            SpecialistReviewerSortOrder = specialistReviewerSortOrder;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentTypeThreshold"/> class.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="scientistReviewerSortOrder">The scientist reviewer sort order.</param>
        /// <param name="advocateConsumerSortOrder">The advocate consumer sort order.</param>
        /// <param name="specialistReviewerSortOrder">The specialist reviewer sort order.</param>
        /// <param name="canSupportSpecialistReviewer">if set to <c>true</c> [can support specialist reviewer].</param>
        public AssignmentTypeThreshold(int sessionPanelId, int? scientistReviewerSortOrder, int? advocateConsumerSortOrder,
            int? specialistReviewerSortOrder, bool canSupportSpecialistReviewer) 
            : this(sessionPanelId, scientistReviewerSortOrder, advocateConsumerSortOrder, specialistReviewerSortOrder)
        {
            CanSupportSpecialistReviewer = canSupportSpecialistReviewer;
        }
        /// <summary>
        /// Gets or sets the assignment type threshold identifier.
        /// </summary>
        /// <value>
        /// The assignment type threshold identifier.
        /// </value>
        public int AssignmentTypeThresholdId { get; set; }
        /// <summary>
        /// Gets or sets the session panel identifier.
        /// </summary>
        /// <value>
        /// The session panel identifier.
        /// </value>
        public int SessionPanelId { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the scientist reviewer sort order.
        /// </summary>
        /// <value>
        /// The scientist reviewer sort order.
        /// </value>
        public int? ScientistReviewerSortOrder { get; set; }
        /// <summary>
        /// Gets or sets the advocate consumer sort order.
        /// </summary>
        /// <value>
        /// The advocate consumer sort order.
        /// </value>
        public int? AdvocateConsumerSortOrder { get; set; }
        /// <summary>
        /// Gets or sets the specialist reviewer sort order.
        /// </summary>
        /// <value>
        /// The specialist reviewer sort order.
        /// </value>
        public int? SpecialistReviewerSortOrder { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can support specialist reviewer.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can support specialist reviewer; otherwise, <c>false</c>.
        /// </value>
        public bool CanSupportSpecialistReviewer { get; set; }
    }

}
