namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Model representing a Program drop down entry that is filterable by Active.
    /// </summary>
    public interface IFilterableProgramModel
    {
        #region Attributes
        /// <summary>
        /// Program abbreviation to display
        /// </summary>
        string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Program description 
        /// </summary>
        string ProgramDescription { get; set; }
        /// <summary>
        /// Indicates if the program is active (not closed)
        /// </summary>
        bool IsActive { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// ClientProgram entity identifier
        /// </summary>
        int ClientProgramId { get; set; }
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        int ProgramYearId { get; set; }
        #endregion
    }
    /// <summary>
    /// Model representing a Program drop down entry that is filterable by Active.
    /// </summary>
    public class FilterableProgramModel: IFilterableProgramModel
    {
        #region Attributes
        /// <summary>
        /// Program abbreviation to display
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Program description 
        /// </summary>
        public string ProgramDescription { get; set; }
        /// <summary>
        /// Indicates if the program is active (not closed)
        /// </summary>
        public bool IsActive { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// ClientProgram entity identifier
        /// </summary>
        public int ClientProgramId { get; set; }
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        public int ProgramYearId { get; set; }
        #endregion
    }
}
