namespace Sra.P2rmis.WebModels.PanelManagement
{
    public interface IPanelSignificationsModel
    {
        /// <summary>
        /// the application's panel id 
        /// </summary>
        int PanelId { get; set; }
        /// <summary>
        /// the panel name where the application resides 
        /// </summary>
        string PanelName { get; set; }
        /// <summary>
        /// the panel abbreviation where the application resides
        /// </summary>
        string PanelAbbreviation { get; set; }
        /// <summary>
        /// The panel's fiscal year
        /// </summary>
        string FY { get; set; }
        /// <summary>
        /// The application's program abbreviation
        /// </summary>
        string ProgramAbbreviation { get; set; }
        /// <summary>
        /// The user's role
        /// </summary>
        string Role { get; set; }
        /// <summary>
        /// The text to be displayed in the panel dropdown list
        /// </summary>
        string DisplayText { get; set; }
    }
}
