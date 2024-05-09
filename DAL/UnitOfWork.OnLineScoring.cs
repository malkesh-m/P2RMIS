using Sra.P2rmis.Dal.Repository;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Definition of repository classes for ApplicationScoringRepository.
    /// </summary>    
    public partial class UnitOfWork
    {
        #region ApplicationScoring Repository
        /// <summary>
        /// Provides database access for the OnLineScoring repository functions.
        /// </summary>
        private IApplicationScoringRepository _ApplicationScoringRepository;
        public IApplicationScoringRepository ApplicationScoringRepository { get { return LazyLoadApplicationScoringRepository(); } }
        /// <summary>
        /// Lazy load the PanelManagementRepository.
        /// </summary>
        /// <returns>PanelManagementRepository</returns>
        private IApplicationScoringRepository LazyLoadApplicationScoringRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ApplicationScoringRepository == null)
            {
                this._ApplicationScoringRepository = new ApplicationScoringRepository(_context);
            }
            return _ApplicationScoringRepository;
        }
        #endregion
        #region ApplicationReviewStatu Repository
        /// <summary>
        /// Provides database access for the ApplicationReviewStatu repository functions.
        /// </summary>
        private IGenericRepository<ApplicationReviewStatu> _genericApplicationReviewStatuRepository;
        public IGenericRepository<ApplicationReviewStatu> ApplicationReviewStatuGenericRepository { get { return LazyLoadApplicationReviewStatuGenericRepository(); } }
        /// <summary>
        /// Lazy load the ApplicationReviewStatuRepository.
        /// </summary>
        /// <returns>ApplicationReviewStatuRepository</returns>
        private IGenericRepository<ApplicationReviewStatu> LazyLoadApplicationReviewStatuGenericRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._genericApplicationReviewStatuRepository == null)
            {
                this._genericApplicationReviewStatuRepository = new GenericRepository<ApplicationReviewStatu>(_context);
            }
            return _genericApplicationReviewStatuRepository;
        }
        #endregion
        #region ClientScoringScaleLegendItem Repository
        /// <summary>
        /// Provides database access for the ClientScoringScaleElement repository functions.
        /// </summary>
        private IClientScoringScaleLegendItemRepository _ClientScoringScaleLegendItemRepository;
        public IClientScoringScaleLegendItemRepository ClientScoringScaleLegendItemRepository { get { return LazyLoadClientScoringScaleLegendItemRepository(); } }
        /// <summary>
        /// Lazy load the ClientScoringScaleElementItemRepository.
        /// </summary>
        /// <returns>ClientScoringScaleElemenItemtRepository</returns>
        private IClientScoringScaleLegendItemRepository LazyLoadClientScoringScaleLegendItemRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ClientScoringScaleLegendItemRepository == null)
            {
                this._ClientScoringScaleLegendItemRepository = new ClientScoringScaleLegendItemRepository(_context);
            }
            return _ClientScoringScaleLegendItemRepository;
        }
        #endregion
        #region ClientScoringScaleAdjectival Repository
        /// <summary>
        /// Provides database access for the ClientScoringScaleAdjectival repository functions.
        /// </summary>
        private IGenericRepository<ClientScoringScaleAdjectival> _ClientScoringScaleAdjectivalRepository;
        public IGenericRepository<ClientScoringScaleAdjectival> ClientScoringScaleAdjectivalRepository { get { return LazyLoadClientScoringScaleAdjectivalRepository(); } }
        /// <summary>
        /// Lazy load the ClientScoringScaleAdjectivalRepository.
        /// </summary>
        /// <returns>ClientScoringScaleAdjectivalRepository</returns>
        private IGenericRepository<ClientScoringScaleAdjectival> LazyLoadClientScoringScaleAdjectivalRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ClientScoringScaleAdjectivalRepository == null)
            {
                this._ClientScoringScaleAdjectivalRepository = new GenericRepository<ClientScoringScaleAdjectival>(_context);
            }
            return _ClientScoringScaleAdjectivalRepository;
        }
        #endregion
        #region ApplicationStageStep Repository

        private IGenericRepository<ApplicationStageStep> _applicationStageStepRepository;
        /// <summary>
        /// Provides database access for the application stage step repository.
        /// </summary>
        /// <value>
        /// The application stage step repository.
        /// </value>
        public IGenericRepository<ApplicationStageStep> ApplicationStageStepRepository { get
        {
            return LazyLoadApplicationStageStepRepository();
        } }

        /// <summary>
        /// Lazy loads the application stage step repository.
        /// </summary>
        /// <returns></returns>
        private IGenericRepository<ApplicationStageStep> LazyLoadApplicationStageStepRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._applicationStageStepRepository == null)
            {
                this._applicationStageStepRepository = new GenericRepository<ApplicationStageStep>(_context);
            }
            return _applicationStageStepRepository;
        }
        #endregion
        #region ApplicationStage Repository

        private IGenericRepository<ApplicationStage> _applicationStageRepository;
        /// <summary>
        /// Provides database access for the application stage step repository.
        /// </summary>
        /// <value>
        /// The application stage step repository.
        /// </value>
        public IGenericRepository<ApplicationStage> ApplicationStageRepository
        {
            get
            {
                return LazyLoadApplicationStageRepository();
            }
        }

        /// <summary>
        /// Lazy loads the application stage step repository.
        /// </summary>
        /// <returns></returns>
        private IGenericRepository<ApplicationStage> LazyLoadApplicationStageRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._applicationStageRepository == null)
            {
                this._applicationStageRepository = new GenericRepository<ApplicationStage>(_context);
            }
            return _applicationStageRepository;
        }
        #endregion
        #region ClientScoringScale Repository
        /// <summary>
        /// Provides database access for the ClientScoringScale repository functions.
        /// </summary>
        private IGenericRepository<ClientScoringScale> _ClientScoringScaleRepository;
        public IGenericRepository<ClientScoringScale> ClientScoringScaleRepository { get { return LazyLoadClientScoringScaleRepository(); } }
        /// <summary>
        /// Lazy load the ClientScoringScaleRepository.
        /// </summary>
        /// <returns>ClientScoringScaleRepository</returns>
        private IGenericRepository<ClientScoringScale> LazyLoadClientScoringScaleRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ClientScoringScaleRepository == null)
            {
                this._ClientScoringScaleRepository = new GenericRepository<ClientScoringScale>(_context);
            }
            return _ClientScoringScaleRepository;
        }
        #endregion

        #region ApplicationStageStepDiscussion Repository
        /// <summary>
        /// Provides database access for the ApplicationStageStepDiscussion repository functions.
        /// </summary>
        private IGenericRepository<ApplicationStageStepDiscussion> _applicationStageStepDiscussionRepository;
        public IGenericRepository<ApplicationStageStepDiscussion> ApplicationStageStepDiscussionRepository { get { return LazyLoadApplicationStageStepDiscussionRepository(); } }
        /// <summary>
        /// Lazy load the ApplicationStageStepDiscussionRepository.
        /// </summary>
        /// <returns>ApplicationStageStepDiscussionRepository</returns>
        private IGenericRepository<ApplicationStageStepDiscussion> LazyLoadApplicationStageStepDiscussionRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._applicationStageStepDiscussionRepository == null)
            {
                this._applicationStageStepDiscussionRepository = new GenericRepository<ApplicationStageStepDiscussion>(_context);
            }
            return _applicationStageStepDiscussionRepository;
        }
        #endregion
        #region ApplicationStageStepDiscussionComment Repository
        /// <summary>
        /// Provides database access for the ApplicationStageStepDiscussionComment repository functions.
        /// </summary>
        private IGenericRepository<ApplicationStageStepDiscussionComment> _applicationStageStepDiscussionCommentRepository;
        public IGenericRepository<ApplicationStageStepDiscussionComment> ApplicationStageStepDiscussionCommentRepository { get { return LazyLoadApplicationStageStepDiscussionCommentRepository(); } }
        /// <summary>
        /// Lazy load the ApplicationStageStepDiscussionCommentRepository.
        /// </summary>
        /// <returns>ApplicationStageStepDiscussionCommentRepository</returns>
        private IGenericRepository<ApplicationStageStepDiscussionComment> LazyLoadApplicationStageStepDiscussionCommentRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._applicationStageStepDiscussionCommentRepository == null)
            {
                this._applicationStageStepDiscussionCommentRepository = new GenericRepository<ApplicationStageStepDiscussionComment>(_context);
            }
            return _applicationStageStepDiscussionCommentRepository;
        }
        #endregion
    }
}
