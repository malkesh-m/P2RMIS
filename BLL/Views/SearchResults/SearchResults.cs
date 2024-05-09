using DataLayer = Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll.Views
{
    /// <summary>
    /// Business Layer representation of search results
    /// </summary>
    public class SearchResults
    {
        #region Constructors
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private SearchResults() { }
        /// <summary>
        /// Constructor.  Populate from the Data Layers SearchResultModel
        /// </summary>
        /// <param name="searchResult">search results from data layer</param>
        public SearchResults(DataLayer.SearchResultModel searchResults)
        {
            if (searchResults != null)
            {
                this.PanelID = searchResults.PanelId;
                this.PanelName = searchResults.PanelName;
                this.ApplicationId = searchResults.ApplicationID;
                this.ProgramId = searchResults.ProgramId;
                this.isValid = true;
            }
            else
            {
                this.isValid = false;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Panel unique identifier
        /// </summary>
        public int PanelID { get; set; }
        /// <summary>
        /// Panel Name
        /// </summary>
        public string PanelName { get; set; }
        /// <summary>
        /// Application identifier
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// Program ID for application
        /// </summary>
        public int ProgramId { get; set; } 
        /// <summary>
        /// determines if it is a valid application id
        /// </summary>
        public bool isValid { get; set; }
        #endregion
    }
}
