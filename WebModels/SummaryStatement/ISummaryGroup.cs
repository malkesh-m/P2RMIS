namespace Sra.P2rmis.WebModels.SummaryStatement
{
    /// <summary>
    /// Model fields used to group the models for display
    /// </summary>
    public interface ISummaryGroup
    {
        /// <summary>
        /// The program abbreviation for the applications
        /// </summary>
        string ProgramAbbreviation { get; set; }
        /// <summary>
        /// The year of the program 
        /// </summary>
        string Year { get; set; }
        /// <summary>
        /// the application's cycle
        /// </summary>
        int Cycle { get; set; }
        /// <summary>
        /// The panel abbreviation that evaluated the application
        /// </summary>
        string PanelAbbreviation { get; set; }
        /// <summary>
        /// The unique identifier for a panel 
        /// </summary>
        int PanelId { get; set; }
        /// <summary>
        /// Number of applications assigned to the panel
        /// </summary>
        int NumberPanelApplications { get; set; }
        /// <summary>
        /// The award for the applications contained in the summary.  This is
        /// only set if an award was used as a search filter.
        /// </summary>
        string Award { get; set; }
        /// <summary>
        /// The user's first name for the applications contained in the summary.  This is
        /// only set if an user name was used as a search filter.
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// The user's last name for the applications contained in the summary.  This is
        /// only set if an user last was used as a search filter.
        /// </summary>
        string LastName { get; set; }
    }
}
