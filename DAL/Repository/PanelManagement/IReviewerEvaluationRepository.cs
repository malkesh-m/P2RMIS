
namespace Sra.P2rmis.Dal.Interfaces
{
    /// <summary>
    /// Repository for ReviewerEvaluation objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public interface IReviewerEvaluationRepository : IGenericRepository<ReviewerEvaluation>
    {
        /// <summary>
        /// Modifies the specified record with the supplied information.
        /// <param name="reviewEvaluationId">reviewer evaluation identifier of the record to be updated</param>
        /// <param name="rating">Rating</param>
        /// <param name="potentialChairFlag">Potential chair</param>
        /// <param name="ratingComments">Rating comments</param>
        /// <param name="userId">User identifier</param>
        /// <returns>Container of IApplicationInformationModel models</returns>
        /// </summary>
        ReviewerEvaluation Modify(int reviewEvaluationId, int? rating, bool? potentialChairFlag, string ratingComments, int userId);
        /// <summary>
        /// Fills in an empty record with the specified record with the supplied information.
        /// <param name="panelUserAssignementId">reviewer evaluation identifier of the record to be updated</param>
        /// <param name="rating">Rating</param>
        /// <param name="potentialChairFlag">Potential chair</param>
        /// <param name="ratingComments">Rating comments</param>
        /// <param name="userId">User identifier</param>
        /// <returns>Container of IApplicationInformationModel models</returns>
        /// </summary>
        ReviewerEvaluation Populate(int panelUserAssignementId, int? rating, bool? potentialChairFlag, string ratingComments, int userId);
    }
}
