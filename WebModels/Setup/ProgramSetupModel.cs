using System;

namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Data model returned to populate the Program Setup grid
    /// </summary>
    public interface IProgramSetupModel
    {
        #region The Data
        /// <summary>
        /// Client abbreviation
        /// </summary>
        string ClientAbrv { get; }
        /// <summary>
        /// Client name
        /// </summary>
        string ClientDesc { get; }
        /// <summary>
        /// Fiscal year
        /// </summary>
        string Year { get; }
        /// <summary>
        /// Program name
        /// </summary>
        string ProgramDescription { get; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        string ProgramAbbreviation { get; }
        /// <summary>
        /// Is the program active?
        /// </summary>
        bool Active { get; }
        /// <summary>
        /// Number of cycles in the program
        /// </summary>
        int CycleCount { get; }
        /// <summary>
        /// Number of awards in the program
        /// </summary>
        int ProgramMechanismCount { get; }
        /// <summary>
        /// Number of meeting for the program
        /// </summary>
        int ClientMeetingCount { get; }
        /// <summary>
        /// DateTime ProgramYear entity was modified
        /// </summary>
        Nullable<DateTime> ModifiedDate { get; }
        bool IsApplicationsReleased { get; set; }
        #endregion
        #region The Indexes
        /// <summary>
        /// Client entity identifier
        /// </summary>
        int ClientId { get; }
        /// <summary>
        /// ClientProgram entity identifier
        /// </summary>
        int ClientProgramId { get; }
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        int ProgramYearId { get; }
        #endregion
    }
    /// <summary>
    /// Data model returned to populate the Program Setup grid
    /// </summary>
    public class ProgramSetupModel : IProgramSetupModel
    {
        #region Construction & Setup
        /// <summary>
        /// Default Model Constructor
        /// </summary>
        public ProgramSetupModel() { }
        #endregion
        #region The Data
        /// <summary>
        /// Client abbreviation
        /// </summary>
        public string ClientAbrv { get; set; } = string.Empty;
        /// <summary>
        /// Client name
        /// </summary>
        public string ClientDesc { get; set; } = string.Empty;
        /// <summary>
        /// Fiscal year
        /// </summary>
        public string Year { get; set; } = string.Empty;
        /// <summary>
        /// Program name
        /// </summary>
        public string ProgramDescription { get; set; } = string.Empty;
        /// <summary>
        /// Program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; set; } = string.Empty;
        /// <summary>
        /// Is the program active?
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Number of cycles in the program
        /// </summary>
        public int CycleCount { get; set; }
        /// <summary>
        /// Number of awards in the program
        /// </summary>
        public int ProgramMechanismCount { get; set; } 
        /// <summary>
        /// Number of meeting for the program
        /// </summary>
        public int ClientMeetingCount { get; set; }
        #endregion
        #region The Indexes
        /// <summary>
        /// Client entity identifier
        /// </summary>
        public int ClientId { get; set; } 
        /// <summary>
        /// ClientProgram entity identifier
        /// </summary>
        public int ClientProgramId { get; set; }
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        public int ProgramYearId { get; set; }
        /// <summary>
        /// DateTime ProgramYear entity was modified
        /// </summary>
        public Nullable<DateTime> ModifiedDate { get; set; }
        /// <summary>
        /// Indicates if applications have been released.
        /// </summary>
        public bool IsApplicationsReleased { get; set; }
        #endregion
    }
}

