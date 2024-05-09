using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.HelperClasses;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Linq implementation of criteria search methods
    /// </summary>
    internal partial class RepositoryHelpers
    {
        
       
        /// <summary>
        /// Retrieve the open programs for all clients
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        /// <returns>Enumerable list of ProgramModel of all open programs</returns>
        internal static IEnumerable<ProgramModel> GetOpenProgramList(P2RMISNETEntities context)
        {
            var result = from programFY in context.ProgramYears
                         join clientProgram in context.ClientPrograms on programFY.ClientProgramId equals clientProgram.ClientProgramId
                         join client in context.Clients on clientProgram.ClientId equals client.ClientID
                         orderby programFY.Year descending, clientProgram.ProgramDescription
                         where programFY.DateClosed == null
                         select new ProgramModel { ProgramAbbrv = clientProgram.ProgramAbbreviation , ProgramName = clientProgram.ProgramDescription };
            return result;
        }
        
    }
}
