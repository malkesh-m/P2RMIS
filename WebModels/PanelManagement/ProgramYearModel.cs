using System;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for Program/Year in PanelManagement
    /// </summary>
    public class ProgramYearModel : IProgramYearModel
    {
        /// <summary>
        /// The Program/Year identifier
        /// </summary>
        public int ProgramYearId { get; set; }
        /// <summary>
        /// The panel's fiscal year
        /// </summary>
        public string FY { get; set; }
        /// <summary>
        /// The program's description
        /// </summary>
        public string ProgramDescription { get; set; }
        /// <summary>
        /// The application's program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// The date the user was assigned to the panel
        /// </summary>
        public DateTime? AssignedDate { get; set; }
        /// <summary>
        /// The text to be displayed in the Program/Year dropdown list
        /// </summary>
        public string DisplayText { get; set; }
        /// <summary>
        /// DateTime program was closed.  Null indicates the program was open
        /// </summary>
        public DateTime? DateClosed { get; set; }
    }
}
