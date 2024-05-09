using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;
using System.Web;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.Repository;
using Sra.P2rmis.Dal.Repository.Setup;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal
{

    public class UnitOfWorkReqScope : IDisposable
    {
        private const string ItemsKey = "__P2RMISNETContext__";

        public UnitOfWorkReqScope()
        {
        }

        public P2RMISNETEntities Current
        {
            get
            {
                var current = (P2RMISNETEntities)HttpContext.Current.Items[ItemsKey];

                if (current == null)
                {
                    HttpContext.Current.Items[ItemsKey] = current = new P2RMISNETEntities();
                }

                return current;
            }
        }

        public bool HasCurrent
        {
            get { return HttpContext.Current.Items.Contains(ItemsKey); }
        }

        public void Dispose()
        {
            if (HasCurrent)
            {
                ((P2RMISNETEntities)HttpContext.Current.Items[ItemsKey]).Dispose();
            }
        }
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public partial class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Attributes
        /// <summary>
        /// >P2RMIS database context
        /// </summary>
        private P2RMISNETEntities _context;
        /// <summary>
        /// Indicates if this object has been disposed but not garbage collected.
        /// </summary>
        private bool _disposed = false;
        #endregion
        #region Constructor; Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        public void UnitofWork() 
        {
            // TODO:: re-factor for DI
            _context = new P2RMISNETEntities(); 
        }
        public void UnitofWork(DbContext context)
        {
            // TODO:: re-factor for DI
            _context = (P2RMISNETEntities) context;
        }
    /// <summary>
    /// TODO:: document me
    /// </summary>
    public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// TODO:: document me
        /// </summary>
        /// <param name="disposing">-----</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing && _context != null)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
        #endregion
        #region Repository Access
        #endregion
        #region User Repository
        /// <summary>
        /// Provides database access for UserRepository functions.  Placed in method; Properties actually
        /// replace property call with code in the property.
        /// </summary>
        private UserRepository _userRepository;
            public UserRepository UofwUserRepository { get { return LazyLoadUofwUserRepository(); } }
            /// <summary>
            /// Lazy load the UserRepository
            /// </summary>
            /// <returns></returns>
            private UserRepository LazyLoadUofwUserRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._userRepository == null)
                {
                    this._userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
            #endregion
            #region System Template Repository
            /// <summary>
            /// Provides database access for SystemTemplateRepository functions.
            /// </summary>
            private SystemTemplateRepository _sysTemplateRepository;
            public SystemTemplateRepository UofwSysTemplateRepository { get { return LazyLoadSystemTemplateRepository(); } }
            /// <summary>
            /// Lazy load the SystemTemplateRepository.
            /// </summary>
            /// <returns>SystemTemplateRepository</returns>
            private SystemTemplateRepository LazyLoadSystemTemplateRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._sysTemplateRepository == null)
                {
                    this._sysTemplateRepository = new SystemTemplateRepository(_context);
                }
                return _sysTemplateRepository;
            }
        #endregion
        #region ProgramFYRepository
        /// <summary>
        /// Provides database access for ProgramFYRepository functions.
        /// </summary>
        private ProgramFYRepository _programRepository;
        public ProgramFYRepository ProgramRepository { get { return LazyLoadProgramFYRepository(); } }
        /// <summary>
        /// Lazy load the ProgramFYRepository.
        /// </summary>
        /// <returns>ProgramFYRepository</returns>
        private ProgramFYRepository LazyLoadProgramFYRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._programRepository == null)
            {
                this._programRepository = new ProgramFYRepository(_context);
            }
            return _programRepository;
        }
        #endregion
        #region SessionDetailRepository
        /// <summary>
        /// Provides database access for SessionDetailRepository functions.
        /// </summary>
        private SessionDetailRepository _sessionDetailRepository;
        public ISessionDetailRepository SessionDetailRepository { get { return LazyLoadSessionDetailRepository(); } }
        /// <summary>
        /// Lazy load the SessionDetailRepository.
        /// </summary>
        /// <returns>ScoreboardRepository</returns>
        private SessionDetailRepository LazyLoadSessionDetailRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._sessionDetailRepository == null)
            {
                this._sessionDetailRepository = new SessionDetailRepository(_context);
            }
            return _sessionDetailRepository;
        }
        #endregion
            #region ViewPanelDetailsRepository
            /// <summary>
            /// Provides database access for ViewPanelDetailsRepository functions.
            /// </summary>
            private ViewPanelDetailsRepository _viewPanelDetailsRepository;
            public ViewPanelDetailsRepository ViewPanelDetailsRepository {get { return LazyLoadViewPanelDetailRepository(); } }
            /// <summary>
            /// Lazy load the ViewPanelDetailRepository.
            /// </summary>
            /// <returns>ViewPanelDetailsRepository</returns>
            private ViewPanelDetailsRepository LazyLoadViewPanelDetailRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._viewPanelDetailsRepository == null)
                {
                    this._viewPanelDetailsRepository = new ViewPanelDetailsRepository(_context);
                }
                return _viewPanelDetailsRepository;
            }
            #endregion
            #region User Application Comment Repository
            /// <summary>
            /// Provides database access for User Application Comment repository functions.
            /// </summary>
            private IUserApplicationCommentRepository _userApplicationCommentRepository;
            public IUserApplicationCommentRepository UserApplicationCommentRepository { get { return LazyLoadCommentRepository(); } }
            /// <summary>
            /// Lazy load the CommentRepository.
            /// </summary>
            /// <returns>CommentRepository</returns>
            private IUserApplicationCommentRepository LazyLoadCommentRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._userApplicationCommentRepository == null)
                {
                    this._userApplicationCommentRepository = new UserApplicationCommentRepository(_context);
                }
                return _userApplicationCommentRepository;
            }
            #endregion
            #region Search Repository
            
            #endregion
            #region Report Repository
            /// <summary>
            /// Provides database access for Search repository functions.
            /// </summary>
            private IReportRepository _reportRepository;
            public IReportRepository ReportRepository { get { return LazyLoadReportRepository(); } }
            /// <summary>
            /// Lazy load the SearchRepository.
            /// </summary>
            /// <returns>SearchRepository</returns>
            private IReportRepository LazyLoadReportRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._reportRepository == null)
                {
                    this._reportRepository = new ReportRepository(_context);
                }
                return _reportRepository;
            }
            #endregion
            #region Report Viewer Repository
            /// <summary>
            /// Provides database access for Report Viewer repository functions.
            /// </summary>
            private IReportViewerRepository _reportViewerRepository;
            public IReportViewerRepository ReportViewerRepository { get { return LazyLoadReportViewerRepository(); } }
            /// <summary>
            /// Lazy load the ReportViewerRepository.
            /// </summary>
            /// <returns>ReportViewerRepository</returns>
            private IReportViewerRepository LazyLoadReportViewerRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._reportViewerRepository == null)
                {
                    this._reportViewerRepository = new ReportViewerRepository(_context);
                }
                return _reportViewerRepository;
            }
            #endregion
            #region WorkflowTemplate Repository
            /// <summary>
            /// Provides database access for the WorkflowTemplate repository functions.
            /// </summary>
            private IWorkflowTemplateRepository _WorkflowTemplateRepository;
            public IWorkflowTemplateRepository WorkflowTemplateRepository { get { return LazyLoadWorkflowTemplateRepository(); } }
            /// <summary>
            /// Lazy load the WorkflowTemplateRepository.
            /// </summary>
            /// <returns>WorkflowTemplateRepository</returns>
            private IWorkflowTemplateRepository LazyLoadWorkflowTemplateRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._WorkflowTemplateRepository == null)
                {
                    this._WorkflowTemplateRepository = new WorkflowTemplateRepository(_context);
                }
                return _WorkflowTemplateRepository;
            }
            #endregion
            #region WorkflowInstance Repository
            /// <summary>
            /// Provides database access for the WorkflowInstance repository functions.
            /// </summary>
            private IWorkflowInstanceRepository _WorkflowInstanceRepository;
            public IWorkflowInstanceRepository WorkflowInstanceRepository { get { return LazyLoadWorkflowInstanceRepository(); } }
            /// <summary>
            /// Lazy load the WorkflowInstanceRepository.
            /// </summary>
            /// <returns>WorkflowInstanceRepository</returns>
            private IWorkflowInstanceRepository LazyLoadWorkflowInstanceRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._WorkflowInstanceRepository == null)
                {
                    this._WorkflowInstanceRepository = new WorkflowInstanceRepository(_context);
                }
                return _WorkflowInstanceRepository;
            }
            #endregion
            #region UserClient Repository
            /// <summary>
            /// Provides database access for the UserClient repository functions.
            /// </summary>
            private IUserClientRepository _userClientRepository;
            public IUserClientRepository UserClientRepository { get { return LazyLoadUserClientRepository(); } }
            /// <summary>
            /// Lazy load the UserClientRepository.
            /// </summary>
            /// <returns>UserClientRepository</returns>
            private IUserClientRepository LazyLoadUserClientRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._userClientRepository == null)
                {
                    this._userClientRepository = new UserClientRepository(_context);
                }
                return _userClientRepository;
            }
            #endregion
            #region Summary Management Repository
            /// <summary>
            /// Provides database access for the summary management repository functions.
            /// </summary>
            private ISummaryManagementRepository _summaryManagementRepository;
            public ISummaryManagementRepository SummaryManagementRepository { get { return LazyLoadSummaryManagementRepository(); } }
            /// <summary>
            /// Lazy load the SummaryManagementRepository.
            /// </summary>
            /// <returns>SummaryManagementRepository</returns>
            private ISummaryManagementRepository LazyLoadSummaryManagementRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._summaryManagementRepository == null)
                {
                    this._summaryManagementRepository = new SummaryManagementRepository(_context);
                }
                return _summaryManagementRepository;
            }
            #endregion
            #region Application Repository
            /// <summary>
            /// Provides database access for the Application repository functions.
            /// </summary>
            private IApplicationRepository _ApplicationRepository;
            public IApplicationRepository ApplicationRepository { get { return LazyLoadApplicationRepository(); } }
            /// <summary>
            /// Lazy load the ApplicationRepository.
            /// </summary>
            /// <returns>ApplicationRepository</returns>
            private IApplicationRepository LazyLoadApplicationRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._ApplicationRepository == null)
                {
                    this._ApplicationRepository = new ApplicationRepository(_context);
                }
                return _ApplicationRepository;
            }
        #endregion
        #region ApplicationBudgetRepository
        /// <summary>
        /// Provides database access for the application budget repository functions.
        /// </summary>
        private IApplicationBudgetRepository _ApplicationBudgetRepository;
        public IApplicationBudgetRepository ApplicationBudgetRepository { get { return LazyLoadApplicationBudgetRepository(); } }
        /// <summary>
        /// Lazy load the ApplicationBudgetRepository.
        /// </summary>
        /// <returns>ApplicationBudgetRepository</returns>
        private IApplicationBudgetRepository LazyLoadApplicationBudgetRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ApplicationBudgetRepository == null)
            {
                this._ApplicationBudgetRepository = new ApplicationBudgetRepository(_context);
            }
            return _ApplicationBudgetRepository;
        }

        #endregion
        #region ApplicationReviewStatu Repository
        /// <summary>
        /// Provides database access for the ApplicationReviewStatu repository functions.
        /// </summary>
        private IApplicationReviewStatusRepository _ApplicationReviewStatu;
            public IApplicationReviewStatusRepository ApplicationReviewStatusRepository { get { return LazyLoadApplicationReviewStatu(); } }
            /// <summary>
            /// Lazy load the ApplicationReviewStatu.
            /// </summary>
            /// <returns>ApplicationReviewStatu repository</returns>
            private IApplicationReviewStatusRepository LazyLoadApplicationReviewStatu()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._ApplicationReviewStatu == null)
                {
                    this._ApplicationReviewStatu = new ApplicationReviewStatusRepository(_context);
                }
                return _ApplicationReviewStatu;
            }
            #endregion
            #region ApplicationWorkflow Repository
            /// <summary>
            /// Provides database access for the ApplicationWorkflow repository functions.
            /// </summary>
            private IApplicationWorkflowRepository _ApplicationWorkflowRepository;
            public IApplicationWorkflowRepository ApplicationWorkflowRepository { get { return LazyLoadApplicationWorkflowRepository(); } }
            /// <summary>
            /// Lazy load the ApplicationWorkflowRepository.
            /// </summary>
            /// <returns>ApplicationWorkflowRepository</returns>
            private IApplicationWorkflowRepository LazyLoadApplicationWorkflowRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._ApplicationWorkflowRepository == null)
                {
                    this._ApplicationWorkflowRepository = new ApplicationWorkflowRepository(_context);
                }
                return _ApplicationWorkflowRepository;
            }
            #endregion
            #region ApplicationWorkflowStep Repository
            /// <summary>
            /// Provides database access for the ApplicationWorkflowStep repository functions.
            /// </summary>
            private IApplicationWorkflowStepRepository _ApplicationWorkflowStepRepository;
            public IApplicationWorkflowStepRepository ApplicationWorkflowStepRepository { get { return LazyLoadApplicationWorkflowStepRepository(); } }
            /// <summary>
            /// Lazy load the ApplicationWorkflowStep.
            /// </summary>
            /// <returns>ApplicationWorkflowStep</returns>
            private IApplicationWorkflowStepRepository LazyLoadApplicationWorkflowStepRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._ApplicationWorkflowStepRepository == null)
                {
                    this._ApplicationWorkflowStepRepository = new ApplicationWorkflowStepRepository(_context);
                }
                return _ApplicationWorkflowStepRepository;
            }
            #endregion
            #region ApplicationWorkflowStepElement Repository
            /// <summary>
            /// Provides database access for the ApplicationWorkflowStepElement repository functions.
            /// </summary>
            private IApplicationWorkflowStepElementRepository _ApplicationWorkflowStepElementRepository;
            public IApplicationWorkflowStepElementRepository ApplicationWorkflowStepElementRepository { get { return LazyLoadApplicationWorkflowStepElementRepository(); } }
            /// <summary>
            /// Lazy load the ApplicationWorkflowStepElement.
            /// </summary>
            /// <returns>ApplicationWorkflowStepElement</returns>
            private IApplicationWorkflowStepElementRepository LazyLoadApplicationWorkflowStepElementRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._ApplicationWorkflowStepElementRepository == null)
                {
                    this._ApplicationWorkflowStepElementRepository = new ApplicationWorkflowStepElementRepository(_context);
                }
                return _ApplicationWorkflowStepElementRepository;
            }
            #endregion
            #region ApplicationWorkflowStepWorkLog Repository
            /// <summary>
            /// Provides database access for the ApplicationWorkflowStepWorkLog repository functions.
            /// </summary>
            private IApplicationWorkflowStepWorkLogRepository _ApplicationWorkflowStepWorkLogRepository;
            public IApplicationWorkflowStepWorkLogRepository ApplicationWorkflowStepWorkLogRepository { get { return LazyLoadApplicationWorkflowStepWorkLogRepository(); } }
            /// <summary>
            /// Lazy load the ApplicationWorkflowStepWorkLog.
            /// </summary>
            /// <returns>ApplicationWorkflowStepWorkLogRepository</returns>
            private IApplicationWorkflowStepWorkLogRepository LazyLoadApplicationWorkflowStepWorkLogRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._ApplicationWorkflowStepWorkLogRepository == null)
                {
                    this._ApplicationWorkflowStepWorkLogRepository = new ApplicationWorkflowStepWorkLogRepository(_context);
                }
                return _ApplicationWorkflowStepWorkLogRepository;
            }
            #endregion
            #region ApplicationWorkflowStepElementContent Repository
            /// <summary>
            /// Provides database access for the ApplicationWorkflowStepElementContent repository functions.
            /// </summary>
            private IApplicationWorkflowStepElementContentRepository _ApplicationWorkflowStepElementContentRepository;
            public IApplicationWorkflowStepElementContentRepository ApplicationWorkflowStepElementContentRepository { get { return LazyLoadApplicationWorkflowStepElementContentRepository(); } }
            /// <summary>
            /// Lazy load the ApplicationWorkflowStepElementContentRepository.
            /// </summary>
            /// <returns>ApplicationWorkflowStepElementContentRepository</returns>
            private IApplicationWorkflowStepElementContentRepository LazyLoadApplicationWorkflowStepElementContentRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._ApplicationWorkflowStepElementContentRepository == null)
                {
                    this._ApplicationWorkflowStepElementContentRepository = new ApplicationWorkflowStepElementContentRepository(_context);
                }
                return _ApplicationWorkflowStepElementContentRepository;
            }
            #endregion
            #region ApplicationWorkflowStepElementContentHistoryRepository Repository
            /// <summary>
            /// Provides database access for the ApplicationWorkflowStepElementContentHistory repository functions.
            /// </summary>
            private IApplicationWorkflowStepElementContentHistoryRepository _ApplicationWorkflowStepElementContentHistoryRepository;
            public IApplicationWorkflowStepElementContentHistoryRepository ApplicationWorkflowStepElementContentHistoryRepository { get { return LazyLoadApplicationWorkflowStepElementContentHistoryRepository(); } }
            /// <summary>
            /// Lazy load the ApplicationWorkflowStepElementContentRepository.
            /// </summary>
            /// <returns>ApplicationWorkflowStepElementContentHistoryRepository</returns>
            private IApplicationWorkflowStepElementContentHistoryRepository LazyLoadApplicationWorkflowStepElementContentHistoryRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._ApplicationWorkflowStepElementContentHistoryRepository == null)
                {
                    this._ApplicationWorkflowStepElementContentHistoryRepository = new ApplicationWorkflowStepElementContentHistoryRepository(_context);
                }
                return _ApplicationWorkflowStepElementContentHistoryRepository;
            }
            #endregion
            #region ApplicationDefaultWorkflow Repository
            /// <summary>
            /// Provides database access for the ApplicationDefaultWorkflow repository functions.
            /// </summary>
            private IApplicationDefaultWorkflowRepository _ApplicationDefaultWorkflowRepository;
            public IApplicationDefaultWorkflowRepository ApplicationDefaultWorkflowRepository { get { return LazyLoadApplicationDefaultWorkflowRepository(); } }
            /// <summary>
            /// Lazy load the ApplicationDefaultWorkflowRepository.
            /// </summary>
            /// <returns>ApplicationDefaultWorkflowRepository</returns>
            private IApplicationDefaultWorkflowRepository LazyLoadApplicationDefaultWorkflowRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._ApplicationDefaultWorkflowRepository == null)
                {
                    this._ApplicationDefaultWorkflowRepository = new ApplicationDefaultWorkflowRepository(_context);
                }
                return _ApplicationDefaultWorkflowRepository;
            }
            #endregion
            #region ApplicationWorkflowStepAssignment Repository
            /// <summary>
            /// Provides database access for the ApplicationWorkflowStepAssignment repository functions.
            /// </summary>
            private IApplicationWorkflowStepAssignmentRepository _ApplicationWorkflowStepAssignmentRepository;
            public IApplicationWorkflowStepAssignmentRepository ApplicationWorkflowStepAssignmentRepository { get { return LazyLoadApplicationWorkflowStepAssignmentRepository(); } }
            /// <summary>
            /// Lazy load the ApplicationWorkflowStepAssignmentRepository.
            /// </summary>
            /// <returns>ApplicationWorkflowStepAssignmentRepository</returns>
            private IApplicationWorkflowStepAssignmentRepository LazyLoadApplicationWorkflowStepAssignmentRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._ApplicationWorkflowStepAssignmentRepository == null)
                {
                    this._ApplicationWorkflowStepAssignmentRepository = new ApplicationWorkflowStepAssignmentRepository(_context);
                }
                return _ApplicationWorkflowStepAssignmentRepository;
            }
            #endregion
            #region ClientScoringScaleLegend Repository
            /// <summary>
            /// Provides database access for the ClientScoringScaleElement repository functions.
            /// </summary>
            private IClientScoringScaleLegendRepository _ClientScoringScaleLegendRepository;
            public IClientScoringScaleLegendRepository ClientScoringScaleLegendRepository { get { return LazyLoadClientScoringScaleLegendRepository(); } }
            /// <summary>
            /// Lazy load the ClientScoringScaleElementRepository.
            /// </summary>
            /// <returns>ClientScoringScaleElementRepository</returns>
            private IClientScoringScaleLegendRepository LazyLoadClientScoringScaleLegendRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._ClientScoringScaleLegendRepository == null)
                {
                    this._ClientScoringScaleLegendRepository = new ClientScoringScaleLegendRepository(_context);
                }
                return _ClientScoringScaleLegendRepository;
            }
            #endregion
        #region ClientRegistrationDocument Repository
        /// <summary>
        /// Provides database access for the ClientRegistrationDocument repository functions.
        /// </summary>
        private IClientRegistrationDocumentRepository _ClientRegistrationDocumentRepository;
        public IClientRegistrationDocumentRepository ClientRegistrationDocumentRepository { get { return LazyLoadClientRegistrationDocumentRepository(); } }
        /// <summary>
        /// Lazy load the ClientRegistrationDocumentRepository.
        /// </summary>
        /// <returns>ClientRegistrationDocumentRepository</returns>
        private IClientRegistrationDocumentRepository LazyLoadClientRegistrationDocumentRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ClientRegistrationDocumentRepository == null)
            {
                this._ClientRegistrationDocumentRepository = new ClientRegistrationDocumentRepository(_context);
            }
            return _ClientRegistrationDocumentRepository;
        }
        #endregion

        #region Sandbox for ClientProgramRepository2
        /// <summary>
        /// Provides database access for the PanelApplication repository functions.
        /// </summary>
        private IClientProgramRepository _ClientProgramRepository2;
        public IClientProgramRepository ClientProgramRepository { get { return LazyLoadPClientProgramRepository2(); } }
        /// <summary>
        /// Lazy load the PanelApplicationRepository.
        /// </summary>
        /// <returns>PanelApplicationRepository</returns>
        private IClientProgramRepository LazyLoadPClientProgramRepository2()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ClientProgramRepository2 == null)
            {
                this._ClientProgramRepository2 = new ClientProgramRepository(_context);
            }
            return _ClientProgramRepository2;
        }
        #endregion
        #region ProgramMechanismRepository
        /// <summary>
        /// Provides database access for the ProgramMechanism repository functions.
        /// </summary>
        private IProgramMechanismRepository _programMechanismRepository;
        public IProgramMechanismRepository ProgramMechanismRepository { get { return LazyLoadProgramMechanismRepository(); } }
        /// <summary>
        /// Lazy load the ProgramMechanism.
        /// </summary>
        /// <returns>IProgramMechanism object</returns>
        private IProgramMechanismRepository LazyLoadProgramMechanismRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._programMechanismRepository == null)
            {
                this._programMechanismRepository = new ProgramMechanismRepository(_context);
            }
            return _programMechanismRepository;
        }
        #endregion
        #region ProgramYear Repository
        /// <summary>
        /// Provides database access for the ProgramYear repository functions.
        /// </summary>
        private IProgramYearRepository _programYearRepository;
            public IProgramYearRepository ProgramYearRepository { get { return LazyLoadProgramYearRepository(); } }
            /// <summary>
            /// Lazy load the ProgramYearRepository.
            /// </summary>
            /// <returns>ProgramYearRepository</returns>
            private IProgramYearRepository LazyLoadProgramYearRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._programYearRepository == null)
                {
                    this._programYearRepository = new ProgramYearRepository(_context);
                }
                return _programYearRepository;
            }
        #endregion
        #region ApplicationWorkflowSummaryStatement Repository        
        /// <summary>
        /// The application workflow summary statement repository
        /// </summary>
        private IGenericRepository<ApplicationWorkflowSummaryStatement> _applicationWorkflowSummaryStatementRepository;
        /// <summary>
        /// Gets the application workflow summary statement repository.
        /// </summary>
        /// <value>
        /// The application workflow summary statement repository.
        /// </value>
        public IGenericRepository<ApplicationWorkflowSummaryStatement> ApplicationWorkflowSummaryStatementRepository { get { return LazyLoadApplicationWorkflowSummaryStatementRepository(); } }
        /// <summary>
        /// Lazies the load application workflow summary statement repository.
        /// </summary>
        /// <returns></returns>
        private IGenericRepository<ApplicationWorkflowSummaryStatement> LazyLoadApplicationWorkflowSummaryStatementRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._applicationWorkflowSummaryStatementRepository == null)
            {
                this._applicationWorkflowSummaryStatementRepository = new GenericRepository<ApplicationWorkflowSummaryStatement>(_context);
            }
            return _applicationWorkflowSummaryStatementRepository;
        }
        #endregion
        #region Notification Repository                
        /// <summary>
        /// The notification repository
        /// </summary>
        private INotificationRepository _NotificationRepository;
        /// <summary>
        /// Gets the notification repository.
        /// </summary>
        /// <value>
        /// The notification repository.
        /// </value>
        public INotificationRepository NotificationRepository { get { return LazyLoadNotificationRepository(); } }
        /// <summary>
        /// Lazies the load notification repository.
        /// </summary>
        /// <returns></returns>
        private INotificationRepository LazyLoadNotificationRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._NotificationRepository == null)
            {
                this._NotificationRepository = new NotificationRepository(_context);
            }
            return _NotificationRepository;
        }
        #endregion
        #region ReferralMapping Repository                
        /// <summary>
        /// The referralMapping repository
        /// </summary>
        private IReferralMappingRepository _ReferralMappingRepository;
        /// <summary>
        /// Gets the referralMapping repository.
        /// </summary>
        /// <value>
        /// The referralMapping repository.
        /// </value>
        public IReferralMappingRepository ReferralMappingRepository { get { return LazyLoadReferralMappingRepository(); } }
        /// <summary>
        /// Lazies the load referralMapping repository.
        /// </summary>
        /// <returns></returns>
        private IReferralMappingRepository LazyLoadReferralMappingRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ReferralMappingRepository == null)
            {
                this._ReferralMappingRepository = new ReferralMappingRepository(_context);
            }
            return _ReferralMappingRepository;
        }
        #endregion 
        #region ReferralMappingData Repository                
        /// <summary>
        /// The referralMappingData repository
        /// </summary>
        private IReferralMappingDataRepository _ReferralMappingDataRepository;
        /// <summary>
        /// Gets the referralMappingData repository.
        /// </summary>
        /// <value>
        /// The referralMappingData repository.
        /// </value>
        public IReferralMappingDataRepository ReferralMappingDataRepository { get { return LazyLoadReferralMappingDataRepository(); } }
        /// <summary>
        /// Lazies the load referralMappingData repository.
        /// </summary>
        /// <returns></returns>
        private IReferralMappingDataRepository LazyLoadReferralMappingDataRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ReferralMappingDataRepository == null)
            {
                this._ReferralMappingDataRepository = new ReferralMappingDataRepository(_context);
            }
            return _ReferralMappingDataRepository;
        }
        #endregion
        #region Workflow Repository
        /// <summary>
        /// Provides database access for thetWorkflow repository functions.
        /// </summary>
        private IWorkflowRepository _WorkflowRepository;
        public IWorkflowRepository WorkflowRepository { get { return LazyLoadWorkflowRepository(); } }
        /// <summary>
        /// Lazy load the WorkflowRepository.
        /// </summary>
        /// <returns>WorkflowRepository</returns>
        private IWorkflowRepository LazyLoadWorkflowRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._WorkflowRepository == null)
            {
                this._WorkflowRepository = new WorkflowRepository(_context);
            }
            return _WorkflowRepository;
        }
        private UnitOfWorkReqScope unitReqScope = new UnitOfWorkReqScope();
        #endregion

        #region Save
        /// <summary>
        /// TODO:: document me
        /// </summary>
        public void Save()
        {
            SaveChanges(_context);
        }
        /// <summary>
        /// Detach all entities.
        /// </summary>
        public void DetachAllEntities()
        {
            var changedEntriesCopy = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
        /// <summary>
        /// Wrapper for SaveChanges adding the Validation Messages to the generated exception
        /// </summary>
        /// <param name="context">The context.</param>
        private void SaveChanges(P2RMISNETEntities context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());

                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb.ToString());
            }
        }
        /// <summary>
        /// Checks if the entity has been modified.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">The specific entity under test</param>
        /// <returns>True if entity has been modified; false otherwise</returns>
        public bool HasEntityBeenModified<T>(T entity) where T : class
        {
            return this._context.Entry(entity).State == EntityState.Modified;
        }
        /// <summary>
        /// Sets the entity deleted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId">The user identifier.</param>
        /// <param name="entity">The entity.</param>
        public void SetEntityDeleted<T>(int userId, T entity) where T : class
        {
            const string deletedByName = "DeletedBy";
            const string deletedDateName = "DeletedDate";
            this._context.Entry(entity).CurrentValues[deletedByName] = userId;
            this._context.Entry(entity).CurrentValues[deletedDateName] = GlobalProperties.P2rmisDateTimeNow;
            this._context.Entry(entity).State = EntityState.Deleted;
        }
        #endregion
    }
}