using System.Linq;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    /// <summary>
    /// Provides database access to ProgramMechanism entities.
    /// </summary>
    public interface IProgramMechanismRepository: IGenericRepository<ProgramMechanism>
    {
        /// <summary>
        /// Retrieve the ProgramMechanisms for the specified ProgramYear.  Merely
        /// a wrapper to call the RepositoryHelper which does all the heavy lifting
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifiers</param>
        /// <returns>IQueryable container of ProgramMechanisms</returns>
        IQueryable<ProgramMechanism> RetrieveProgramYearProgramMechanisms(int programYearId);
    }
    /// <summary>
    /// Provides database access to ProgramMechanism entities.
    /// </summary>
    public class ProgramMechanismRepository: GenericRepository<ProgramMechanism>, IProgramMechanismRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ProgramMechanismRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion
        #region Services        
        /// <summary>
        /// Retrieve the ProgramMechanisms for the specified ProgramYear.  Merely
        /// a wrapper to call the RepositoryHelper which does all the heavy lifting
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifiers</param>
        /// <returns>IQueryable container of ProgramMechanisms</returns>
        public IQueryable<ProgramMechanism> RetrieveProgramYearProgramMechanisms(int programYearId)
        {
            IQueryable<ProgramMechanism> result = RepositoryHelpers.RetrieveProgramYearProgramMechanisms(context, programYearId);
            return result;
        }
        #endregion
    }
}
