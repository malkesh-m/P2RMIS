using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.WebModels.Setup;
using System.Data.Entity;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    /// <summary>
    /// Provides database access to ProgramYear entities.
    /// </summary>
    public interface IProgramYearRepository : IGenericRepository<ProgramYear>
    {
        /// <summary>
        /// Gets the program year with program.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        IEnumerable<ProgramYear> GetProgramYearWithProgram(int programYearId);
    }
    /// <summary>
    /// Provides database access to ClientProgram entities.
    /// </summary>
    public class ProgramYearRepository : GenericRepository<ProgramYear>, IProgramYearRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ProgramYearRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion
        #region Services        
        /// <summary>
        /// Gets the program year with program.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        /// <remarks>Includes ClientProgram</remarks>
        public IEnumerable<ProgramYear> GetProgramYearWithProgram(int programYearId)
        {
            var models = context.ProgramYears.Include(z => z.ClientProgram)
                .Where(x => x.ProgramYearId == programYearId);
            return models;
        }
        #endregion
    }
}
