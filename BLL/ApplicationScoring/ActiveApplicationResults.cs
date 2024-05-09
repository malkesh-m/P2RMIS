
namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// Return results for the call IsAnyApplicationBeingScored()
    /// </summary>
    public class ActiveApplicationResults
    {
        #region Construction & set up
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="panelApplicationId">Panel application entity identifier</param>
        /// <param name="logNumber">Application log number</param>
        internal ActiveApplicationResults(int? panelApplicationId, string logNumber)
        {
            this.PanelApplicationId = panelApplicationId;
            this.LogNumberr = logNumber;
            HasActiveApplication = true;
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        internal ActiveApplicationResults()
        {
            HasActiveApplication = false;
        }
        #endregion

        /// <summary>
        /// Panel application entity identifier
        /// </summary>
        public int? PanelApplicationId { get; private set; }
        /// <summary>
        /// Application log number
        /// </summary>
        public string LogNumberr { get; private set; }
        /// <summary>
        /// Indicator that an active application exists
        /// </summary>
        public bool HasActiveApplication { get; private set; }
    }
}
