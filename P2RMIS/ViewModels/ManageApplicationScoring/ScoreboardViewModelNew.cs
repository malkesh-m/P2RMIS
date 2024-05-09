using System.Collections.Generic;
using Sra.P2rmis.WebModels.ApplicationScoring;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    public class ScoreboardViewModelNew
    {
        #region Constants

        public class Constants
        {
            public const string NoActiveApp = "NoActiveApp";
            public const string ActiveApp = "ActiveApp";
            public const string ScoringApp = "ScoringApp";
            public const string DiscussingApp = "DiscussingApp";
        }
        
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ScoreboardViewModelNew()
        {
            this.CoiList = new List<CoiModel>();
        }
        #endregion

        #region Properties

        public int PanelId { get; set; }
        public int PanelAppId { get; set; }
        public string DisplayContent { get; set; }
        public string CurrentAppStatus { get; set; }
        public string AppLogNumber { get; set; }
        /// <summary>
        /// Gets or sets the name of the pi.
        /// </summary>
        /// <value>
        /// The name of the pi.
        /// </value>
        public string PiName { get; set; }
        /// <summary>
        /// List of COI models to display
        /// </summary>
        public List<CoiModel> CoiList { get; set; }
        /// <summary>
        /// Gets or sets the application information.
        /// </summary>
        /// <value>
        /// The application information.
        /// </value>
        /// <remarks>TODO: convert to a view model</remarks>
        public IApplicationInformationModel ApplicationInformation { get; set; }
        /// <summary>
        /// Gets or sets the scoreboard scores.
        /// </summary>
        /// <value>
        /// The scoreboard scores.
        /// </value>
        /// <remarks>TODO: convert to a view model</remarks>
        public IReviewerApplicationPremeetingScoresModel ScoreboardScores { get; set; }

        #endregion
    }
}