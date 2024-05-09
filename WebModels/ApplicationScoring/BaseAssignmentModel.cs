namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Base model class for the assignment view models
    /// </summary>
    public class BaseAssignmentModel : IBaseAssignmentModel
    {
        #region Construction & Setup
        /// <summary>
        /// Base model constructor
        /// </summary>
        public BaseAssignmentModel()
        {

        }
        /// <summary>
        /// Populate the general portion of the web model
        /// </summary>
        /// <param name="applicationLogNumber">Application log number</param>
        /// <param name="title">Application title</param>
        /// <param name="pIFirstName">Principal investigator first name</param>
        /// <param name="pILastName">Principal investigator last name</param>
        /// <param name="mechanism">Mechanism</param>
        public void Populate(string applicationLogNumber, string title, string pIFirstName, string pILastName, string mechanism)
        {
            this.ApplicationLogNumber = applicationLogNumber;
            this.Title = title;
            this.PIFirstName = pIFirstName;
            this.PILastName = pILastName;
            this.Mechanism = mechanism;
        }
        /// <summary>
        /// Populates the entity portions of the web model
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="applicationId">Application entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="sessionPanelAbbreviation">SessionPanel abbreviation</param>
        public void PopulateEntityIdentifiers(int panelApplicationId, int applicationId, int sessionPanelId, string sessionPanelAbbreviation, string sessionPanelName)
        {
            this.PanelApplicationId = panelApplicationId;
            this.ApplicationId = applicationId;
            this.SessionPanelId = sessionPanelId;
            this.SessionPanelAbbreviation = sessionPanelAbbreviation;
            this.SessionPanelName = sessionPanelName;
        }
        #endregion
        #region Entity Identifiers Attributes
        /// <summary>
        /// PanelApplication entity identifier
        /// </summary>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// Application entity identifier
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        public int SessionPanelId { get; set; }
        /// <summary>
        /// Gets or sets the abbreviation of the session panel.
        /// </summary>
        /// <value>
        /// The abbreviation of the session panel.
        /// </value>
        public string SessionPanelAbbreviation { get; set; }
        /// <summary>
        /// PanelUserAssignment entity identifier
        /// </summary>
        public int? PanelUserAssignmentId { get; set; }
        #endregion
        #region Attributes
        /// Gets or sets the name of the session panel.
        /// </summary>
        /// <value>
        /// The name of the session panel.
        /// </value>
        public string SessionPanelName { get; set; }
        /// <summary>
        /// Application log number
        /// </summary>
        public string ApplicationLogNumber { get; set; }
        /// <summary>
        /// Application title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Principal investigator first name
        /// </summary>
        public string PIFirstName { get; set; }
        /// <summary>
        /// Principal investigator last name
        /// </summary>
        public string PILastName { get; set; }
        /// <summary>
        /// Mechanism
        /// </summary>
        public string Mechanism { get; set; }
        #endregion
    }
}
