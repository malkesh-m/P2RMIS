
namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Class containing the website address information
    /// </summary>
    public class WebsiteModel : Editable, IWebsiteModel
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userWebsiteId">The user website identifier</param>
        /// <param name="websiteTypeId">The website type identifier</param>
        /// <param name="websiteAddress">The website address</param>
        /// <param name="primary">The primary flag</param>
        public WebsiteModel(int userWebsiteId, int websiteTypeId, string websiteAddress, bool primary)
        {
            this.UserWebsiteId = userWebsiteId;
            this.WebsiteTypeId = websiteTypeId;
            this.WebsiteAddress = websiteAddress;
            this.Primary = primary;
        }
        /// <summary>
        /// Default constructor.  The default constructor is required to provide an empty container for display.
        /// </summary>
        public WebsiteModel() {}
        #endregion
        /// <summary>
        /// The user website identifier
        /// </summary>
        public int UserWebsiteId { get; set; }
        /// <summary>
        /// The website type identifier
        /// </summary>
        public int WebsiteTypeId { get; set; }
        /// <summary>
        /// The website address
        /// </summary>
        public string WebsiteAddress { get; set; }
        /// <summary>
        /// Primary Website
        /// </summary>
        public bool Primary { get; set; }
        /// <summary>
        /// Does the model have data?  A model has data if the web site address is supplied.
        /// </summary>
        /// <returns>True if the model has data; false otherwise</returns>
        public override bool HasData()
        {
            return !(string.IsNullOrWhiteSpace(this.WebsiteAddress));
        }
    }
}
