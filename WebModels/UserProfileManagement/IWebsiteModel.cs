
namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Class containing the awebsite address information
    /// </summary>
    public interface IWebsiteModel: IEditable
    {
        /// <summary>
        /// The user website identifier
        /// </summary>
        int UserWebsiteId { get; set; }
        /// <summary>
        /// The website type identifier
        /// </summary>
        int WebsiteTypeId { get; set; }
        /// <summary>
        /// The website address
        /// </summary>
        string WebsiteAddress { get; set; }
        /// <summary>
        /// Primary Website
        /// </summary>
        bool Primary { get; set; }
    }
}
