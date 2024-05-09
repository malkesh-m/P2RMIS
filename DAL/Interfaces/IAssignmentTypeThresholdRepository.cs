namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for Application objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public interface IAssignmentTypeThresholdRepository : IGenericRepository<AssignmentTypeThreshold>
    {
        /// <summary>
        /// Gets the specified session panel identifier.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns></returns>
        AssignmentTypeThreshold Get(int sessionPanelId);
        /// <summary>
        /// Upserts the specified session panel identifier.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="scientistReviewerSortOrder">The scientist reviewer sort order.</param>
        /// <param name="advocateConsumerSortOrder">The advocate consumer sort order.</param>
        /// <param name="specialistReviewerSortOrder">The specialist reviewer sort order.</param>
        /// <returns></returns>
        AssignmentTypeThreshold Upsert(int sessionPanelId, int? scientistReviewerSortOrder, int? advocateConsumerSortOrder,
            int? specialistReviewerSortOrder);
    }
}
