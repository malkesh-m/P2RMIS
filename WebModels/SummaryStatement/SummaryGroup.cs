namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// class representing a high level summary for a panel and it's applications
    /// </summary>
    public class SummaryGroup : ISummaryGroup
    {
        /// <summary>
        /// The program abbreviation for the applications
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// The year of the program 
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// the application's cycle
        /// </summary>
        public int Cycle { get; set; }
        /// <summary>
        /// The panel abbreviation that evaluated the application
        /// </summary>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// The unique identifier for a panel 
        /// </summary>
        public int PanelId { get; set; }
        /// <summary>
        /// Number of applications assigned to the panel
        /// </summary>
        public int NumberPanelApplications { get; set; }
        /// <summary>
        /// The award for the applications contained in the summary.  This is
        /// only set if an award was used as a search filter.
        /// </summary>
        public string Award { get; set; }
        /// <summary>
        /// The user's first name for the applications contained in the summary.  This is
        /// only set if an user name was used as a search filter.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The user's last name for the applications contained in the summary.  This is
        /// only set if an user last was used as a search filter.
        /// </summary>
        public string LastName { get; set; }
    }
}
