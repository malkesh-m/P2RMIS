
namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for Client Coi DropdownList in panel amanagement
    /// </summary>
    public interface IClientCoiDropdownList
    {
        /// <summary>
        /// Client Coi Type identifier
        /// </summary>
        int ClientCoiTypeId { get; set; }
        /// <summary>
        /// Coi type description
        /// </summary>
        string CoiTypeDescription { get; set; }
    }
}
