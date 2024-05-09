
namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for Client Expertise DropdownList in panel management
    /// </summary>
    public class ClientExpertiseRatingDropdownList : IClientExpertiseRatingDropdownList
    {
        /// <summary>
        /// The Client Expertise Rating
        /// </summary>
        public int ClientExpertiseRatingId { get; set; }
        /// <summary>
        /// The Client Expertise Rating Abbreviation 
        /// </summary>
        public string ClientExpertiseRatingAbbreviation { get; set; }
    }
}
