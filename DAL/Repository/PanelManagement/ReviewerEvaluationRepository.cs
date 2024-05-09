using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for ReviewerEvaluation objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public class ReviewerEvaluationRepository : GenericRepository<ReviewerEvaluation>, IReviewerEvaluationRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ReviewerEvaluationRepository(P2RMISNETEntities context)
            : base(context)
        {

        }

        /// <summary>
        /// Modifies the specified record with the supplied information.
        /// <param name="reviewEvaluationId">reviewer evaluation identifier of the record to be updated</param>
        /// <param name="rating">Rating</param>
        /// <param name="potentialChairFlag">Potential chair</param>
        /// <param name="ratingComments">Rating comments</param>
        /// <param name="userId">User identifier</param>
        /// <returns>ReviewerEvaluation models</returns>
        /// </summary>
        public ReviewerEvaluation Modify(int reviewEvaluationId, int? rating, bool? potentialChairFlag, string ratingComments, int userId)
        {
            ReviewerEvaluation eval = context.ReviewerEvaluations.Find(reviewEvaluationId);
            return eval.Modify(rating, potentialChairFlag, ratingComments, userId);
        }
        /// <summary>
        /// Fills in an empty record with the specified record with the supplied information.
        /// <param name="panelUserAssignementId">panel user assignment identifier </param>
        /// <param name="rating">Rating</param>
        /// <param name="potentialChairFlag">Potential chair</param>
        /// <param name="ratingComments">Rating comments</param>
        /// <param name="userId">User identifier</param>
        /// <returns>ReviewerEvaluation models</returns>
        /// </summary>
        public ReviewerEvaluation Populate(int panelUserAssignmentId, int? rating, bool? potentialChairFlag, string ratingComments, int userId)
        {
            ReviewerEvaluation eval = new ReviewerEvaluation();

            return eval.Populate(panelUserAssignmentId, rating, potentialChairFlag, ratingComments, userId);
        }
        #endregion
    }
}
