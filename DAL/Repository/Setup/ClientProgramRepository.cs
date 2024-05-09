using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    /// <summary>
    /// Provides database access to ClientProgram entities.
    /// </summary>
    public interface IClientProgramRepository: IGenericRepository<ClientProgram>
    {
        /// <summary>
        /// Retrieve the ProgramYears for the list of Client entity identifiers.  Merely
        /// a wrapper to call the RepositoryHelper which does all the heavy lifting
        /// </summary>
        /// <param name="clientIds">List of Client entity identifiers</param>
        /// <returns>IQueryable container of ProgramYears</returns> 
        IQueryable<ProgramYear> ProgramSetup(IList<int> clientIds);
    }
    /// <summary>
    /// Provides database access to ClientProgram entities.
    /// </summary>
    public class ClientProgramRepository: GenericRepository<ClientProgram>, IClientProgramRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ClientProgramRepository(P2RMISNETEntities context)
            : base(context)
        {

        }
        #endregion
        #region Services
        /// <summary>
        /// Retrieve the ProgramYears for the list of Client entity identifiers.  Merely
        /// a wrapper to call the RepositoryHelper which does all the heavy lifting
        /// </summary>
        /// <param name="clientIds">List of Client entity identifiers</param>
        /// <returns>IQueryable container of ProgramYears</returns>
        public IQueryable<ProgramYear> ProgramSetup(IList<int> clientIds)
        {
            IQueryable<ProgramYear> result = RepositoryHelpers.RetrieveProgramSetup(context, clientIds);
            return result;
        }
        #endregion
    }
}
