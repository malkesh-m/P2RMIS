
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Definition of repository classes for panel registration entity objects.
    /// 
    /// In general entity objects that are specifically used by individual subsystems are
    /// located in source files named UnitOfWork."subsystem name".
    /// </summary> 
    public partial class UnitOfWork
    {
        #region PanelUserRegistration Repository
        /// <summary>
        /// Provides database access for PanelUserRegistrationRepository functions. 
        /// </summary>
        private IGenericRepository<PanelUserRegistration> _panelUserRegistrationRepository;
        public IGenericRepository<PanelUserRegistration> PanelUserRegistrationRepository { get { return LazyLoadPanelUserRegistrationRepository(); } }
        /// <summary>
        /// Lazy load the PanelUserRegistrationRepository
        /// </summary>
        /// <returns>PanelUserRegistrationRepository</returns>
        private IGenericRepository<PanelUserRegistration> LazyLoadPanelUserRegistrationRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._panelUserRegistrationRepository == null)
            {
                this._panelUserRegistrationRepository = new GenericRepository<PanelUserRegistration>(_context);
            }
            return _panelUserRegistrationRepository;
        }
        #endregion
        #region PanelUserRegistrationDocument Repository
        /// <summary>
        /// Provides database access for PanelUserRegistrationDocument functions. 
        /// </summary>
        private IGenericRepository<PanelUserRegistrationDocument> _panelUserRegistrationDocumentRepository;
        public IGenericRepository<PanelUserRegistrationDocument> PanelUserRegistrationDocumentRepository { get { return LazyLoadPanelUserRegistrationDocumentRepository(); } }
        /// <summary>
        /// Lazy load the PanelUserRegistrationDocument
        /// </summary>
        /// <returns>PanelUserRegistrationDocument</returns>
        private IGenericRepository<PanelUserRegistrationDocument> LazyLoadPanelUserRegistrationDocumentRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._panelUserRegistrationDocumentRepository == null)
            {
                this._panelUserRegistrationDocumentRepository = new GenericRepository<PanelUserRegistrationDocument>(_context);
            }
            return _panelUserRegistrationDocumentRepository;
        }
        #endregion
        #region PanelUserRegistrationDocumentItem Repository
        /// <summary>
        /// Provides database access for PanelUserRegistrationDocumentItem functions. 
        /// </summary>
        private IGenericRepository<PanelUserRegistrationDocumentItem> _panelUserRegistrationDocumentItemRepository;
        public IGenericRepository<PanelUserRegistrationDocumentItem> PanelUserRegistrationDocumentItemRepository { get { return LazyLoadPanelUserRegistrationDocumentItemRepository(); } }
        /// <summary>
        /// Lazy load the PanelUserRegistrationDocumentItem repository
        /// </summary>
        /// <returns>PanelUserRegistrationDocumentItem Repository</returns>
        private IGenericRepository<PanelUserRegistrationDocumentItem> LazyLoadPanelUserRegistrationDocumentItemRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._panelUserRegistrationDocumentItemRepository == null)
            {
                this._panelUserRegistrationDocumentItemRepository = new GenericRepository<PanelUserRegistrationDocumentItem>(_context);
            }
            return _panelUserRegistrationDocumentItemRepository;
        }
        #endregion
        #region PanelUserRegistrationDocumentContract Repository
        /// <summary>
        /// Provides database access for PanelUserRegistrationDocumentContract functions. 
        /// </summary>
        private IGenericRepository<PanelUserRegistrationDocumentContract> _panelUserRegistrationDocumentContractRepository;
        public IGenericRepository<PanelUserRegistrationDocumentContract> PanelUserRegistrationDocumentContractRepository { get { return LazyLoadPanelUserRegistrationDocumentContractRepository(); } }
        /// <summary>
        /// Lazy load the PanelUserRegistrationDocumentContract repository
        /// </summary>
        /// <returns>PanelUserRegistrationDocumentContract Repository</returns>
        private IGenericRepository<PanelUserRegistrationDocumentContract> LazyLoadPanelUserRegistrationDocumentContractRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._panelUserRegistrationDocumentContractRepository == null)
            {
                this._panelUserRegistrationDocumentContractRepository = new GenericRepository<PanelUserRegistrationDocumentContract>(_context);
            }
            return _panelUserRegistrationDocumentContractRepository;
        }
        #endregion
    }
}
