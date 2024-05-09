namespace Sra.P2rmis.Bll.Views
{
    public interface ISearchResults
    {
        /// <summary>
        /// Panel unique identifier
        /// </summary>
        int PanelID { get; set; }
        /// <summary>
        /// Panel Name
        /// </summary>
        string PanelName { get; set; }
        /// <summary>
        /// Application identifier
        /// </summary>
        string ApplicationId { get; set; }
        /// <summary>
        /// Program ID for application
        /// </summary>
        int ProgramId { get; set; } 
        /// <summary>
        /// determines is application id is valid
        /// </summary>
        bool isValid { get; set; }
    }
}
