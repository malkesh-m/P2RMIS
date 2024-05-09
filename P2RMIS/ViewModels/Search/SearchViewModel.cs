using Sra.P2rmis.Bll.Views;

namespace Sra.P2rmis.Web.UI.Models
{
    public class SearchViewModel
    {
        #region Constructors
        public SearchViewModel() { }
        /// <summary>
        /// Search view model
        /// </summary>
        /// <param name="">-----</param>
        public SearchViewModel(SearchResults results)
        {
            this.PanelID = results.PanelID;
            this.PanelName = results.PanelName;
            this.ApplicationID = results.ApplicationId;
            this.ProgramId = results.ProgramId;
            this.isValid = results.isValid;
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
        /// Application unique identifier
        /// </summary>
        public string ApplicationID { get ; set; }
        /// <summary>
        /// program id for the application
        /// </summary>
        public int ProgramId { get; set; }
        /// <summary>
        /// determines if application is valid
        /// </summary>
        public bool isValid { get; set; }
        #endregion 
    }
}