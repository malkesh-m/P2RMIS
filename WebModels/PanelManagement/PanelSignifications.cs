namespace Sra.P2rmis.WebModels.PanelManagement
{
    public class PanelSignificationsModel : IPanelSignificationsModel
    {
        /// <summary>
        /// the application's panel id 
        /// </summary>
        public int PanelId { get; set; }
        /// <summary>
        /// the panel name where the application resides 
        /// </summary>
        public string PanelName { get; set; }
        /// <summary>
        /// the panel abbreviation where the application resides
        /// </summary>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// The panel's fiscal year
        /// </summary>
        public string FY { get; set; }
        /// <summary>
        /// The application's program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// The user's role
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// The text to be displayed in the panel dropdown list
        /// </summary>
        public string DisplayText { get; set; }
    }
}
