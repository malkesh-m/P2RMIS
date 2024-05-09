

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for Client Coi DropdownList in panel amanagement
    /// </summary>
    public class ClientCoiDropdownList : IClientCoiDropdownList
    {
        /// <summary>
        /// Client Coi Type identifier
        /// </summary>
        public int ClientCoiTypeId { get; set; }
        /// <summary>
        /// Coi type description 
        /// </summary>
        public string CoiTypeDescription { get; set; }
    }
}
