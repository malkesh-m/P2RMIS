
namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Returns from method IsAnyApplicationBeingScored()
    /// </summary>
    public class ActiveApplicationModel
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="applicationTitle">Application title</param>
        /// <param name="logNumer">Application log number</param>
        public ActiveApplicationModel(int? panelApplicationId, string applicationTitle, string logNumer)
        {
            this.PanelApplicationId = panelApplicationId;
            this.ApplicationTitle = applicationTitle;
            this.LogNumber = logNumer;
        }        
        #endregion
        /// <summary>
        /// Panel application entity identifier
        /// </summary>
        public int? PanelApplicationId { get; private set; }
        /// <summary>
        /// The application title
        /// </summary>
        public string ApplicationTitle { get; private set; }
        /// <summary>
        /// Application log number
        /// </summary>
        public string LogNumber { get; private set; }
    }
}
