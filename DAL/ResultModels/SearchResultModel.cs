namespace Sra.P2rmis.Dal.ResultModels
{
    /// <summary>
    /// Model representing the results of an application search
    /// </summary>
    public class SearchResultModel : ISearchResultModel
    {
        #region Properties
        /// <summary>
        /// the applications panel id 
        /// </summary>
        public int PanelId { get; set; }
        /// <summary>
        /// the panel name where the application resides 
        /// </summary>
        public string PanelName { get; set; }
        /// <summary>
        /// the application unique identifier
        /// </summary>
        public string ApplicationID { get; set; }
        /// <summary>
        /// program id for the application
        /// </summary>
        public int ProgramId { get; set; }
        #endregion
    }
}
