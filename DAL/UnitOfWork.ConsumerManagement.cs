using Sra.P2rmis.Dal.Repository.ConsumerManagement;

namespace Sra.P2rmis.Dal
{
    public partial class UnitOfWork
    {
        #region Nominee Repository
        /// <summary>
        /// Provides database access for the Nominee repository functions.
        /// </summary>
        private INomineeRepository _NomineeRepository;
        public INomineeRepository NomineeRepository { get { return LazyLoadNomineeRepository(); } }
        /// <summary>
        /// Lazy load the NomineeRepository.
        /// </summary>
        /// <returns>NomineeRepository</returns>
        private INomineeRepository LazyLoadNomineeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._NomineeRepository == null)
            {
                this._NomineeRepository = new NomineeRepository(_context);
            }
            return _NomineeRepository;
        }
        #endregion

        #region NomineeType Repository
        /// <summary>
        /// Provides database access for the NomineeType repository functions.
        /// </summary>
        private IGenericRepository<NomineeType> _NomineeTypeRepository;
        public IGenericRepository<NomineeType> NomineeTypeRepository { get { return LazyLoadNomineeTypeRepository(); } }
        /// <summary>
        /// Lazy load the NomineeTypeRepository.
        /// </summary>
        /// <returns>NomineeTypeRepository</returns>
        private IGenericRepository<NomineeType> LazyLoadNomineeTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._NomineeTypeRepository == null)
            {
                this._NomineeTypeRepository = new GenericRepository<NomineeType>(_context);
            }
            return _NomineeTypeRepository;
        }
        #endregion

        #region NomineeAffected Repository
        /// <summary>
        /// Provides database access for the NomineeAffected repository functions.
        /// </summary>
        private INomineeAffectedRepository _NomineeAffectedRepository;
        public INomineeAffectedRepository NomineeAffectedRepository { get { return LazyLoadNomineeAffectedRepository(); } }
        /// <summary>
        /// Lazy load the NomineeAffectedRepository.
        /// </summary>
        /// <returns>NomineeAffectedRepository</returns>
        private INomineeAffectedRepository LazyLoadNomineeAffectedRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._NomineeAffectedRepository == null)
            {
                this._NomineeAffectedRepository = new NomineeAffectedRepository(_context);
            }
            return _NomineeAffectedRepository;
        }
        #endregion

        #region NomineeProgram Repository
        /// <summary>
        /// Provides database access for the NomineeProgram repository functions.
        /// </summary>
        private INomineeProgramRepository _NomineeProgramRepository;
        public INomineeProgramRepository NomineeProgramRepository { get { return LazyLoadNomineeProgramRepository(); } }
        /// <summary>
        /// Lazy load the NomineeProgramRepository.
        /// </summary>
        /// <returns>NomineeProgramRepository</returns>
        private INomineeProgramRepository LazyLoadNomineeProgramRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._NomineeProgramRepository == null)
            {
                this._NomineeProgramRepository = new NomineeProgramRepository(_context);
            }
            return _NomineeProgramRepository;
        }
        #endregion

        #region Nominee Repository
        /// <summary>
        /// Provides database access for the Nominee repository functions.
        /// </summary>
        private INomineeSponsorRepository _NomineeSponsorRepository;
        public INomineeSponsorRepository NomineeSponsorRepository { get { return LazyLoadNomineeSponsorRepository(); } }
        /// <summary>
        /// Lazy load the NomineeSponsorRepository.
        /// </summary>
        /// <returns>NomineeSponsorRepository</returns>
        private INomineeSponsorRepository LazyLoadNomineeSponsorRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._NomineeSponsorRepository == null)
            {
                this._NomineeSponsorRepository = new NomineeSponsorRepository(_context);
            }
            return _NomineeSponsorRepository;
        }
        #endregion
    }
}
