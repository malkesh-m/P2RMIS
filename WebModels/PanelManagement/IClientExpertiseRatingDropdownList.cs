
namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for Client Expertise DropdownList in panel management
    /// </summary>
    public interface IClientExpertiseRatingDropdownList
    {
         /// <summary>
        /// The Client Expertise Rating
        /// </summary>
        int ClientExpertiseRatingId { get; set; }
        /// <summary>
        /// The Client Expertise Rating Abbreviation 
        /// </summary>
        string ClientExpertiseRatingAbbreviation { get; set; }
    }
}
