using Sra.P2rmis.Dal.Repository.LibraryManagement;

namespace Sra.P2rmis.Dal
{
   /// <summary>
    /// Definition of repository classes for Training document management entity objects.
    /// 
    /// In general entity objects that are specifically used by individual subsystems are
    /// located in source files named UnitOfWork."subsystem name".
    /// </summary>
    public partial class UnitOfWork
    {
        #region TrainingCategory Repository
        /// <summary>
        /// Provides database access for the TrainingCategory repository functions.
        /// </summary>
        private IGenericRepository<TrainingCategory> _TrainingCategoryRepository;
        public IGenericRepository<TrainingCategory> TrainingCategoryRepository { get { return LazyLoadTrainingCategoryRepository(); } }
        /// <summary>
        /// Lazy load the TrainingCategoryRepository.
        /// </summary>
        /// <returns>TrainingCategoryRepository</returns>
        private IGenericRepository<TrainingCategory> LazyLoadTrainingCategoryRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._TrainingCategoryRepository == null)
            {
                this._TrainingCategoryRepository = new GenericRepository<TrainingCategory>(_context);
            }
            return _TrainingCategoryRepository;
        }
        #endregion

        #region PeerReviewDocument Repository
        /// <summary>
        /// Provides database access for the PeerReviewDocument repository functions.
        /// </summary>
        private IPeerReviewDocumentRepository _PeerReviewDocumentRepository;
        public IPeerReviewDocumentRepository PeerReviewDocumentRepository { get { return LazyLoadPeerReviewDocumentRepository(); } }
        /// <summary>
        /// Lazy load the PeerReviewDocumentRepository.
        /// </summary>
        /// <returns>PeerReviewDocumentRepository</returns>
        private IPeerReviewDocumentRepository LazyLoadPeerReviewDocumentRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PeerReviewDocumentRepository == null)
            {
                this._PeerReviewDocumentRepository = new PeerReviewDocumentRepository(_context);
            }
            return _PeerReviewDocumentRepository;
        }
        /// <summary>
        /// Provides database access for the PeerReviewDocument repository functions.
        /// </summary>
        private IUserPeerReviewDocumentRepository _UserPeerReviewDocumentRepository;
        public IUserPeerReviewDocumentRepository UserPeerReviewDocumentRepository { get { return LazyLoadUserPeerReviewDocumentRepository(); } }
        /// <summary>
        /// Lazy load the PeerReviewDocumentRepository.
        /// </summary>
        /// <returns>PeerReviewDocumentRepository</returns>
        private IUserPeerReviewDocumentRepository LazyLoadUserPeerReviewDocumentRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserPeerReviewDocumentRepository == null)
            {
                this._UserPeerReviewDocumentRepository = new UserPeerReviewDocumentRepository(_context);
            }
            return _UserPeerReviewDocumentRepository;
        }
        #endregion

    }
}
