using Sra.P2rmis.Dal.Repository.ConsumerManagement;

namespace Sra.P2rmis.Dal
{
    public partial interface IUnitOfWork
    {
        /// <summary>
        /// Provides database access for NomineeType functions. 
        /// </summary>
        IGenericRepository<NomineeType> NomineeTypeRepository { get; }
        /// <summary>
        /// Provides database access for Nominee functions. 
        /// </summary>
        INomineeRepository NomineeRepository { get; }
        /// <summary>
        /// Provides database access for NomineeAffected functions. 
        /// </summary>
        INomineeAffectedRepository NomineeAffectedRepository { get; }
        /// <summary>
        /// Provides database access for NomineeSponsor functions. 
        /// </summary>
        INomineeSponsorRepository NomineeSponsorRepository { get; }
        /// <summary>
        /// Provides database access for NomineeProgram functions. 
        /// </summary>
        INomineeProgramRepository NomineeProgramRepository { get; }
    }
}
