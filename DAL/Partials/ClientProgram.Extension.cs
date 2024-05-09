using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ClientProgram object. 
    /// </summary>
    public partial class ClientProgram
    {
        /// <summary>
        /// Populate a new ClientProgram
        /// </summary>
        /// <param name="clientId">ClientEntity identifier who own's program</param>
        /// <param name="programAbbreviation">Program abbreviation</param>
        /// <param name="programDescription">Program description</param>
        /// <param name="userId">User entity identifier making the change</param>
        public void Populate(int clientId, string programAbbreviation, string programDescription, int userId)
        {
            this.ClientId = clientId;
            this.ProgramAbbreviation = programAbbreviation;
            this.ProgramDescription = programDescription;
            this.CreatedBy = userId;
            this.CreatedDate = GlobalProperties.P2rmisDateTimeNow;
            this.ModifiedBy = userId;
            this.ModifiedDate = GlobalProperties.P2rmisDateTimeNow;
        }
        /// <summary>
        /// Itemize the ProgramYear entity identifiers for this client project
        /// </summary>
        /// <returns>List of ProgramYear entity identifiers</returns>
        public List<int> ItemizeProgramYearIds()
        {
            return this.ProgramYears.Select(x => x.ProgramYearId).ToList();
        }
        /// <summary>
        /// Determines if the ClientProgram has a single ProgramYear
        /// </summary>
        /// <returns>True if the ClientProgram has only a single ProgramYear; false otherwise</returns>
        public bool IsLastProgramYear()
        {
            return (this.ProgramYears.Count() <= 1);
        }
    }
}
