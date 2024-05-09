
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Definition of repository classes for Mail entity objects.
    /// 
    /// In general entity objects that are specifically used by individual subsystems are
    /// located in source files named UnitOfWork."subsystem name".
    /// </summary>    
    public partial class UnitOfWork
    {
        #region CommunicationLog Repository
        /// <summary>
        /// Provides database access for the CommunicationLog repository functions.
        /// </summary>
        private IGenericRepository<CommunicationLog> _CommunicationLogRepository;
        public IGenericRepository<CommunicationLog> CommunicationLogRepository { get { return LazyLoadCommunicationLogRepository(); } }
        /// <summary>
        /// Lazy load the CommunicationLogRepository.
        /// </summary>
        /// <returns>CommunicationLogRepository</returns>
        private IGenericRepository<CommunicationLog> LazyLoadCommunicationLogRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._CommunicationLogRepository == null)
            {
                this._CommunicationLogRepository = new GenericRepository<CommunicationLog>(_context);
            }
            return _CommunicationLogRepository;
        }
        #endregion
        #region CommunicationLogRecipient Repository
        /// <summary>
        /// Provides database access for the CommunicationLogRecipient repository functions.
        /// </summary>
        private IGenericRepository<CommunicationLogRecipient> _CommunicationLogRecipientRepository;
        public IGenericRepository<CommunicationLogRecipient> CommunicationLogRecipientRepository { get { return LazyLoadCommunicationLogRecipientRepository(); } }
        /// <summary>
        /// Lazy load the CommunicationLogRecipientRepository.
        /// </summary>
        /// <returns>CommunicationLogRecipientRepository</returns>
        private IGenericRepository<CommunicationLogRecipient> LazyLoadCommunicationLogRecipientRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._CommunicationLogRecipientRepository == null)
            {
                this._CommunicationLogRecipientRepository = new GenericRepository<CommunicationLogRecipient>(_context);
            }
            return _CommunicationLogRecipientRepository;
        }
        #endregion		
        #region CommunicationLogAttachment Repository
        /// <summary>
        /// Provides database access for the CommunicationLogAttachment repository functions.
        /// </summary>
        private IGenericRepository<CommunicationLogAttachment> _CommunicationLogAttachmentRepository;
        public IGenericRepository<CommunicationLogAttachment> CommunicationLogAttachmentRepository { get { return LazyLoadCommunicationLogAttachmentRepository(); } }
        /// <summary>
        /// Lazy load the CommunicationLogAttachmentRepository.
        /// </summary>
        /// <returns>CommunicationLogAttachmentRepository</returns>
        private IGenericRepository<CommunicationLogAttachment> LazyLoadCommunicationLogAttachmentRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._CommunicationLogAttachmentRepository == null)
            {
                this._CommunicationLogAttachmentRepository = new GenericRepository<CommunicationLogAttachment>(_context);
            }
            return _CommunicationLogAttachmentRepository;
        }
        #endregion
    }
}
