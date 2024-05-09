using System;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for Program/Year in PanelManagement
    /// </summary>
    public interface IProgramYearModel
    {
        /// <summary>
        /// The Program/Year identifier
        /// </summary>
        int ProgramYearId { get; set; }
        /// <summary>
        /// The panel's fiscal year
        /// </summary>
        string FY { get; set; }
        /// <summary>
        /// The program's description
        /// </summary>
        string ProgramDescription { get; set; }
        /// <summary>
        /// The application's program abbreviation
        /// </summary>
        string ProgramAbbreviation { get; set; }
        /// <summary>
        /// The date the user was assigned to the panel
        /// </summary>
        DateTime? AssignedDate { get; set; }
        /// <summary>
        /// The text to be displayed in the Program/Year dropdown list
        /// </summary>
        string DisplayText { get; set; }
        /// <summary>
        /// DateTime program was closed.  Null indicates the program was open
        /// </summary>
        DateTime? DateClosed { get; set; }
    }
}
