using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PanelApplicationReviewerExpertise object. 
    /// </summary>	
    public partial class PanelApplicationReviewerExpertise : IStandardDateFields
    {
        /// <summary>
        /// Modifies the current record with the supplied information.
        /// </summary>
        /// <param name="clientExpertiseRatingId">The expertise rating identifier</param>
        /// <param name="comment">The expertise comment</param>
        /// <param name="userId">User identifier</param>
        /// <returns>PanelApplicationReviewerExpertise models</returns>
        public PanelApplicationReviewerExpertise Modify(int? clientExpertiseRatingId, string comment, int userId)
        {
            this.ClientExpertiseRatingId = clientExpertiseRatingId;
            this.ExpertiseComments = comment;

            Helper.UpdateModifiedFields(this, userId);
            return this;
        }
        /// <summary>
        /// Fills in the empty current record with the specified record with the supplied information.
        /// </summary>
        /// <param name="panelApplicationId">The panel application identifier</param>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier</param>
        /// <param name="clientExpertiseRatingId">The expertise rating identifier</param>
        /// <param name="comment">The expertise comment</param>
        /// <param name="userId">User identifier</param>
        /// <param name="comments">The comments</param>
        /// <returns>PanelApplicationReviewerExpertise models</returns>
        public PanelApplicationReviewerExpertise Populate(int panelApplicationId, int panelUserAssignmentId, int? clientExpertiseRatingId, int userId, string comments)
        {
            PanelApplicationReviewerExpertise expertise = new PanelApplicationReviewerExpertise();

            expertise.Modify(clientExpertiseRatingId, comments, userId);
            expertise.PanelApplicationId = panelApplicationId;
            expertise.PanelUserAssignmentId = panelUserAssignmentId;
            Helper.UpdateCreatedFields(expertise, userId);

            return expertise;
        }
        /// <summary>
        /// Retrieves the Experience rating if one exists
        /// </summary>
        /// <returns><Rating abbreviation if one exists; null otherwise/returns>
        public string Rating()
        {
            return (this.ClientExpertiseRatingId.HasValue) ? this.ClientExpertiseRating.RatingAbbreviation : null;
        }
        /// <summary>
        /// Retrieves the Experience rating if one exists
        /// </summary>
        /// <returns><Rating abbreviation if one exists; null otherwise/returns>
        public bool? Conflict()
        {
            return (this.ClientExpertiseRatingId.HasValue) ? this.ClientExpertiseRating.ConflictFlag : false;
        }
    }
}
