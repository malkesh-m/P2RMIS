namespace Sra.P2rmis.WebModels.HelperClasses
{
    public class ProgramModel: IProgramModel
    {
        #region Properties
        public int ClientProgramId { get; set; }
        /// <summary>
        /// Unique identifier for the program
        /// </summary>
        public string ProgramAbbrv { get; set; }
        /// <summary>
        /// the programs abbreviation 
        /// </summary>
        public string ProgramName { get; set; }
        #endregion
    }
}
