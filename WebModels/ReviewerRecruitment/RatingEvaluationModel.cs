using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ReviewerRecruitment
{
    /// <summary>
    /// Web model for the Reviewer Recruitment Rating/Evaluation modal
    /// </summary>
    public interface IRatingEvaluationModel
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        string FirstName { get; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        string LastName { get; }
        /// <summary>
        /// Reviewer average rating
        /// </summary>
        decimal? AverageRating { get; }
        /// <summary>
        /// Number of individual ratings.  This is the number of 
        /// evaluations that were used in the average rating calculation.
        /// </summary>
        int NumberOfRatings { get; }
        /// <summary>
        /// Number of potential chair recommendations.
        /// </summary>
        int NumberOfPotentialChairRecomendations { get; }
        /// <summary>
        /// Collection of individual reviewer rating.
        /// </summary>
        IEnumerable<IRatingModel> Ratings { get; }
    }
    /// <summary>
    /// Web model for the Reviewer Recruitment Rating/Evaluation modal
    /// </summary>
    public class RatingEvaluationModel: IRatingEvaluationModel
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstName">Reviewer's first name</param>
        /// <param name="lastName">Reviewer's last name</param>
        public RatingEvaluationModel(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        public void SetRatings(List<IRatingModel> list, decimal? averageRating, int numberOfRatings, int numberOfChairRecomendations)
        {
            this.Ratings = list;
            this.AverageRating = averageRating;
            this.NumberOfRatings = numberOfRatings;
            this.NumberOfPotentialChairRecomendations = numberOfChairRecomendations;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; private set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; private set; }
        /// <summary>
        /// Reviewer average rating
        /// </summary>
        public decimal? AverageRating { get; private set; }
        /// <summary>
        /// Number of individual ratings.  This is the number of 
        /// evaluations that were used in the average rating calculation.
        /// </summary>
        public int NumberOfRatings { get; private set; }
        /// <summary>
        /// Number of potential chair recommendations.
        /// </summary>
        public int NumberOfPotentialChairRecomendations { get; private set; }
        /// <summary>
        /// Collection of individual reviewer rating.
        /// </summary>
        public IEnumerable<IRatingModel> Ratings { get; private set; }
        #endregion
    }
}
