namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Base interface for the Assignment models
    /// </summary>
    public interface IBaseAssignmentModel
    {
        #region Entity identifiers
        /// <summary>
        /// PanelApplication entity identifier
        /// </summary>
        int PanelApplicationId { get; set; }
        /// <summary>
        /// Application entity identifier
        /// </summary>
        int ApplicationId { get; set; }
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        int SessionPanelId { get; set; }
        /// <summary>
        /// Gets or sets the name of the session panel.
        /// </summary>
        /// <value>
        /// The abbreviation of the session panel.
        /// </value>
        string SessionPanelAbbreviation { get; set; }
        /// <summary>
        /// PanelUserAsssignment entity identifier
        /// </summary>
        int? PanelUserAssignmentId { get; set; }
        #endregion
        #region Attributes
        /// <summary>
        /// Gets or sets the name of the session panel.
        /// </summary>
        /// <value>
        /// The name of the session panel.
        /// </value>
        string SessionPanelName { get; set; }
        /// <summary>
        /// Application log number
        /// </summary>
        string ApplicationLogNumber { get; set; }
        /// <summary>
        /// Application title
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// Principal investigator first name
        /// </summary>
        string PIFirstName { get; set; }
        /// <summary>
        /// Principal investigator last name
        /// </summary>
        string PILastName { get; set; }
        /// <summary>
        /// Mechanism
        /// </summary>
        string Mechanism { get; set; } 
        #endregion
    }
}
