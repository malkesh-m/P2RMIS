namespace Sra.P2rmis.Web.UI.Models
{
    public class AlreadyActiveAppViewModel
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public AlreadyActiveAppViewModel()
        {
        }
        #endregion

        #region Properties

        public int? CurrentActiveAppId { get; set; }
        public string CurrentActiveAppLogNumber { get; set; }
        public int CurrentActiveAppStatusId { get; set; }
        public string CurrentActiveAppStatusDescription { get; set; }
        public int NewAppId { get; set; }
        public string NewAppLogNumber { get; set; }

        #endregion
    }
}