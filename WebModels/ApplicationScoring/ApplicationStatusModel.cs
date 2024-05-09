namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Model for Application Review status
    /// </summary>
    public interface IApplicationStatusModel
    {
        /// <summary>
        /// Application or is it PanelApplicationIdentifier identifier
        /// </summary>
        string applicationId { get; }
        /// <summary>
        /// Application review status
        /// </summary>
        string status { get; }
        /// <summary>
        /// Indicates if this application is active
        /// </summary>
        bool isActive { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is scoring.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is scoring; otherwise, <c>false</c>.
        /// </value>
        bool IsScoring { get; }
        /// <summary>
        /// PanelApplication entity identifier
        /// </summary>
        string PanelApplicationId { get; }
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        string SessionPanelId { get; }
        /// <summary>
        /// The scorecard URL
        /// </summary>
        string ScoreCardUrl { get; set; }
        /// <summary>
        /// Application's possible scores
        /// </summary>
        long? PossibleScores { get; }
        /// <summary>
        /// Application's actual scores.
        /// </summary>
        long? ActualScores { get; }
        /// <summary>
        /// Applications average overall score
        /// </summary>
        decimal? AverageOE { get; }
        /// <summary>
        /// Initialize the score values.
        /// </summary> 
        /// <param name="possibleScores"></param>
        /// <param name="actualScores"></param>
        /// <param name="averageOE"></param>
        void SetScores(int? possibleScores, int? actualScores, decimal? averageOE);
    }
    /// <summary>
    /// Model for Application Review status
    /// </summary>
    public class ApplicationStatusModel: IApplicationStatusModel
    {
        #region Constructor & setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theApplicationId">Application entity identifier</param>
        /// <param name="theStatus">Text representation of the Application status</param>
        /// <param name="isActive">Indicates if this Application is active</param>
        /// <param name="isScoring">Indicates if the application is scoring</param>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        public ApplicationStatusModel(int theApplicationId, string theStatus, bool isActive, bool isScoring, int panelApplicationId, int sessionPanelId)
        {
            this.applicationId = theApplicationId.ToString();
            this.status = theStatus;
            this.isActive = isActive;
            this.IsScoring = isScoring;
            this.PanelApplicationId = panelApplicationId.ToString();
            this.SessionPanelId = sessionPanelId.ToString();
        }
        /// <summary>
        /// Initialize the score values.
        /// </summary> 
        /// <param name="possibleScores">Number of possible scores</param>
        /// <param name="actualScores">Number of actual scores</param>
        /// <param name="averageOE">Average overall average</param>
        public void SetScores(int? possibleScores, int? actualScores, decimal? averageOE)
        {
            this.PossibleScores = possibleScores;
            this.ActualScores = actualScores;
            this.AverageOE = averageOE;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Application or is it PanelApplicationIdentifier identifier
        /// </summary>
        public string applicationId { get; private set; }
        /// <summary>
        /// Application review status
        /// </summary>
        public string status { get; private set; }
        /// <summary>
        /// Indicates if this application is active
        /// </summary>
        public bool isActive { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance is scoring.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is scoring; otherwise, <c>false</c>.
        /// </value>
        public bool IsScoring { get; private set; }
        /// <summary>
        /// PanelApplication entity identifier
        /// </summary>
        public string PanelApplicationId { get; private set; }
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        public string SessionPanelId { get; private set; }
        /// <summary>
        /// The scorecard URL
        /// </summary>
        public string ScoreCardUrl { get; set; }
        /// <summary>
        /// Application's possible scores
        /// </summary>
        public long? PossibleScores { get; private set; }
        /// <summary>
        /// Application's actual scores.
        /// </summary>
        public long? ActualScores { get; private set; }
        /// <summary>
        /// Applications average overall score
        /// </summary>
        public decimal? AverageOE { get; private set; }
        /// <summary>
        /// Format the Overall average
        /// </summary>
        public string FormattedAverage
        {
            get { return (!AverageOE.HasValue || AverageOE == 0) ? "N/A" : AverageOE.Value.ToString("0.0"); }
        }
        #endregion
    }
}
