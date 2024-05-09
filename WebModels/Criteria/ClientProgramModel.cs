using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.Criteria
{
    /// <summary>
    /// Data model for a client program representation
    /// </summary>
    public interface IClientProgramModel
    {
        /// <summary>
        /// Name of the ClientProgram
        /// </summary>
        string ProgramName { get; set; }

        /// <summary>
        /// Abbreviation of a ClientProgram
        /// </summary>
        string ProgramAbbreviation { get; set; }

        /// <summary>
        /// Identifier for a ClientProgram
        /// </summary>
        int ClientProgramId { get; set; }

        /// <summary>
        /// Gets or sets the fiscal years.
        /// </summary>
        /// <value>
        /// The fiscal years.
        /// </value>
        List<string> FiscalYears { get; set; }
    }

    /// <summary>
    /// Data model for a client program representation
    /// </summary>
    public class ClientProgramModel : IClientProgramModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientProgramModel"/> class.
        /// </summary>
        /// <param name="clientProgramId">The client program identifier.</param>
        /// <param name="programAbbreviation">The program abbreviation.</param>
        /// <param name="programName">Name of the program.</param>
        public ClientProgramModel(int clientProgramId, string programAbbreviation, string programName)
        {
            ClientProgramId = clientProgramId;
            ProgramAbbreviation = programAbbreviation;
            ProgramName = programName;
        }
        /// <summary>
        /// Constructor for a ClientProgramModel
        /// </summary>
        /// <param name="clientProgramId">Identifier for a ClientProgram</param>
        /// <param name="programAbbreviation">Abbreviation of a ClientProgram</param>
        /// <param name="programName">Name of the ClientProgram</param>
        public ClientProgramModel(int clientProgramId, string programAbbreviation, string programName, List<string> fiscalYears)
            : this(clientProgramId, programAbbreviation, programName)
        {
            FiscalYears = fiscalYears;
        }
        /// <summary>
        /// Name of the ClientProgram
        /// </summary>
        public string ProgramName { get; set; }
        /// <summary>
        /// Abbreviation of a ClientProgram
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Identifier for a ClientProgram
        /// </summary>
        public int ClientProgramId { get; set; }
        /// <summary>
        /// Gets or sets the fiscal years.
        /// </summary>
        /// <value>
        /// The fiscal years.
        /// </value>
        public List<string> FiscalYears { get; set; }
    }
}
