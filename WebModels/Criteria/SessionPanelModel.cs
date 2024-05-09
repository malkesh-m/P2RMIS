namespace Sra.P2rmis.WebModels.Criteria
{
    /// <summary>
    /// Data model for a session panel representation
    /// </summary>
    public interface ISessionPanelModel
    {
        /// <summary>
        /// Full Name of the Panel
        /// </summary>
        string PanelName { get; set; }

        /// <summary>
        /// Abbreviation of the Panel
        /// </summary>
        string PanelAbbreviation { get; set; }

        /// <summary>
        /// Identifier for a Panel
        /// </summary>
        int SessionPanelId { get; set; }

        /// <summary>
        /// Gets or sets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        string ProgramAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the year of the program.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        string Year { get; set; }
    }

    /// <summary>
    /// Data model for a session panel representation
    /// </summary>
    public class SessionPanelModel : ISessionPanelModel
    {
        public SessionPanelModel() { }
        #region Constructor
        public SessionPanelModel(int sessionPanelId, string panelAbbreviation, string panelName)
        {
            SessionPanelId = sessionPanelId;
            PanelAbbreviation = panelAbbreviation;
            PanelName = panelName;
            SessionPanelAbbreviation = panelAbbreviation;
        }

        public SessionPanelModel(int sessionPanelId, string panelAbbreviation, string panelName,
            string programAbbreviation, string year)
        {
            SessionPanelId = sessionPanelId;
            PanelAbbreviation = panelAbbreviation;
            PanelName = panelName;
            ProgramAbbreviation = programAbbreviation;
            Year = year;
            SessionPanelAbbreviation = panelAbbreviation;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Full Name of the Panel
        /// </summary>
        public string PanelName { get; set; }
        /// <summary>
        /// Abbreviation of the Panel
        /// </summary>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// Identifier for a Panel
        /// </summary>
        public int SessionPanelId { get; set; }

        /// <summary>
        /// Gets or sets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        public string ProgramAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the year of the program.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public string Year { get; set; }
        /// <summary>
        /// Gets or sets the session panel abbreviation.
        /// </summary>
        /// <value>
        /// The session panel abbreviation.
        /// </value>
        public string SessionPanelAbbreviation { get; set; }
        #endregion
    }
}
