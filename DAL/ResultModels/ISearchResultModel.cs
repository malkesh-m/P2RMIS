namespace Sra.P2rmis.Dal.ResultModels
{
    public interface ISearchResultModel
    {
        #region Properties
        /// <summary>
        /// the applications panel id 
        /// </summary>
        int PanelId { get; set; }
        /// <summary>
        /// the panel name where the application resides 
        /// </summary>
        string PanelName { get; set; }
        /// <summary>
        /// the application unique identifier
        /// </summary>
        string ApplicationID { get; set; }
        /// <summary>
        /// program id for the application
        /// </summary>
        int ProgramId { get; set; }
        #endregion
    }
}
