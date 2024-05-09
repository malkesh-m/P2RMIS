namespace Sra.P2rmis.WebModels.Criteria
{
    /// <summary>
    /// Data model for a program year representation
    /// </summary>
    public interface IProgramYearModel
    {
        /// <summary>
        /// Identifier for a ProgramYear
        /// </summary>
        int ProgramYearId { get; set; }

        /// <summary>
        /// Identifier for a ClientProgram
        /// </summary>
        int ClientProgramId { get; set; }

        /// <summary>
        /// Year of the program offering
        /// </summary>
        string Year { get; set; }
    }

    /// <summary>
    /// Data model for a program year representation
    /// </summary>
    public class ProgramYearModel : IProgramYearModel
    {

        /// <summary>
        /// Constructor for a program year model
        /// </summary>
        /// <param name="programYearId">Identifier for a ProgramYear</param>
        /// <param name="clientProgramId">Identifier for a ClientProgram</param>
        /// <param name="year">Year of the program offering</param>
        public ProgramYearModel(int programYearId, int clientProgramId, string year)
        {
            ClientProgramId = clientProgramId;
            ProgramYearId = programYearId;
            Year = year;
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public ProgramYearModel() { }
        /// <summary>
        /// Identifier for a ProgramYear
        /// </summary>
        public int ProgramYearId { get; set; }
        /// <summary>
        /// Identifier for a ClientProgram
        /// </summary>
        public int ClientProgramId { get; set; }
        /// <summary>
        /// Year of the program offering
        /// </summary>
        public string Year { get; set; }
    }
}
