
namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for AssignmentType DropdownList in panel management
    /// </summary>
    public class AssignmentTypeDropdownList : IAssignmentTypeDropdownList
    {
        /// <summary>
        /// The Assignment Type identifier
        /// </summary>
        public int ClientAssignmentTypeId { get; set; }
        /// <summary>
        /// The Assignment Type Abbreviation 
        /// </summary>
        public string ClientAssignmentTypeAbbreviation { get; set; }
    }

}
