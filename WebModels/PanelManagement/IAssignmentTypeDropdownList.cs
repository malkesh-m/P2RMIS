

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for AssignmentType DropdownList in panel management
    /// </summary>
    public interface IAssignmentTypeDropdownList
    {
        /// <summary>
        /// The Assignment Type identifier
        /// </summary>
        int ClientAssignmentTypeId { get; set; }
        /// <summary>
        /// The Assignment Type Abbreviation 
        /// </summary>
        string ClientAssignmentTypeAbbreviation { get; set; }
    }
}
