
namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Web model returned for Critique rating validation
    /// </summary>
    public interface IRatingValidationModel
    {
        /// <summary>
        /// Error message
        /// </summary>
        string ErrorMessage { get; }
        /// <summary>
        /// Indicates if the rating valid?
        /// </summary>
        bool IsRatingValid { get;}
    }
    /// <summary>
    /// Web model returned for Critique rating validation
    /// </summary>
    public class RatingValidationModel : IRatingValidationModel
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor - for the unhappy path.
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        public RatingValidationModel(string errorMessage)
        {
            this.IsRatingValid = false;
            this.ErrorMessage = errorMessage;
        }
        /// <summary>
        /// Constructor - for the happy path.
        /// </summary>
        public RatingValidationModel()
        {
            this.IsRatingValid = true;
            this.ErrorMessage = string.Empty;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; private set; }
        /// <summary>
        /// Indicates if the rating valid?
        /// </summary>
        public bool IsRatingValid { get; private set; }
        #endregion
    }
}
