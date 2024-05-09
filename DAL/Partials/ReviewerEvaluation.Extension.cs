using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ReviewerEvaluation object.
    /// </summary>
    public partial class ReviewerEvaluation : IAlternateStandardDateFields
    {
        #region Helpers
        /// <summary>
        /// Modifies the current record with the supplied information.

        /// <param name="rating">Rating</param>
        /// <param name="potentialChairFlag">Potential chair</param>
        /// <param name="ratingComments">Rating comments</param>
        /// <param name="userId">User identifier</param>
        /// <returns>ReviewerEvaluation models</returns>
        /// </summary>
        public ReviewerEvaluation Modify(int? rating, bool? potentialChairFlag, string ratingComments, int userId)
        {
            this.Rating = rating;
            this.RecommendChairFlag = (potentialChairFlag.HasValue) ? potentialChairFlag.Value : false;
            this.Comments = ratingComments;
            Helper.UpdateModifiedFields(this, userId);

            return this;
        }
        /// <summary>
        /// Fills in the empty current record with the specified record with the supplied information.
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
            eval.PanelUserAssignmentId = panelUserAssignmentId;
            eval.Rating = rating;
            eval.RecommendChairFlag = (potentialChairFlag.HasValue) ? potentialChairFlag.Value : false;
            eval.Comments = ratingComments;
            Helper.UpdateCreatedFields(eval, userId);

            return eval;
        }
        #endregion
    }
}

