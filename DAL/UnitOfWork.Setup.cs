using Sra.P2rmis.Dal.Repository.Setup;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Definition of repository classes for Setup application
    /// 
    /// In general entity objects that are specifically used by individual subsystems are
    /// located in source files named UnitOfWork."subsystem name".
    /// </summary> 
    public partial class UnitOfWork
    {
        #region MechanismTemplateElement Repository
        /// <summary>
        /// Provides database access for the MechanismTemplateElement repository functions.
        /// </summary>
        private MechanismTemplateElementRepository _MechanismTemplateElement;
        public IMechanismTemplateElementRepository MechanismTemplateElementRepository { get { return LazyLoadMechanismTemplateElement(); } }
        /// <summary>
        /// Lazy load the MechanismTemplateElement.
        /// </summary>
        /// <returns>MechanismTemplateElement</returns>
        private IMechanismTemplateElementRepository LazyLoadMechanismTemplateElement()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._MechanismTemplateElement == null)
            {
                this._MechanismTemplateElement = new MechanismTemplateElementRepository(_context);
            }
            return _MechanismTemplateElement;
        }
        #endregion
        #region ClientElement Repository
        /// <summary>
        /// Provides database access for the ClientElement repository functions.
        /// </summary>
        private IGenericRepository<ClientElement> _ClientElement;
        public IGenericRepository<ClientElement> ClientElementRepository { get { return LazyLoadClientElements(); } }
        /// <summary>
        /// Lazy load the ClientElement.
        /// </summary>
        /// <returns>ClientElement</returns>
        private IGenericRepository<ClientElement> LazyLoadClientElements()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ClientElement == null)
            {
                this._ClientElement = new GenericRepository<ClientElement>(_context);
            }
            return _ClientElement;
        }
        #endregion
        #region MechanismTemplate Repository
        /// <summary>
        /// Provides database access for the MechanismTemplate repository functions.
        /// </summary>
        private IGenericRepository<MechanismTemplate> _MechanismTemplate;
        public IGenericRepository<MechanismTemplate> MechanismTemplateRepository { get { return LazyLoadMechanismTemplates(); } }
        /// <summary>
        /// Lazy load the MechanismTemplate.
        /// </summary>
        /// <returns>MechanismTemplate</returns>
        private IGenericRepository<MechanismTemplate> LazyLoadMechanismTemplates()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._MechanismTemplate == null)
            {
                this._MechanismTemplate = new GenericRepository<MechanismTemplate>(_context);
            }
            return _MechanismTemplate;
        }
        #endregion
        #region MechanismScoringTemplate Repository
        /// <summary>
        /// Provides database access for the MechanismTemplate repository functions.
        /// </summary>
        private IMechanismScoringTemplateRepository _MechanismScoringTemplate;
        public IMechanismScoringTemplateRepository MechanismScoringTemplateRepository { get { return LazyLoadMechanismScoringTemplates(); } }
        /// <summary>
        /// Lazy load the MechanismScoringTemplate.
        /// </summary>
        /// <returns>MechanismScoringTemplate</returns>
        private IMechanismScoringTemplateRepository LazyLoadMechanismScoringTemplates()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._MechanismScoringTemplate == null)
            {
                this._MechanismScoringTemplate = new MechanismScoringTemplateRepository(_context);
            }
            return _MechanismScoringTemplate;
        }
        #endregion
        #region MechanismTemplateElementScoring Repository
        /// <summary>
        /// Provides database access for the MechanismTemplateElementScoring repository functions.
        /// </summary>
        private IMechanismTemplateElementScoringRepository _MechanismTemplateElementScoring;
        public IMechanismTemplateElementScoringRepository MechanismTemplateElementScoringRepository { get { return LazyLoadMechanismTemplateElementScoring(); } }
        /// <summary>
        /// Lazy load the MechanismTemplateElementScoring.
        /// </summary>
        /// <returns>MechanismTemplateElementScoring</returns>
        private IMechanismTemplateElementScoringRepository LazyLoadMechanismTemplateElementScoring()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._MechanismTemplateElementScoring == null)
            {
                this._MechanismTemplateElementScoring = new MechanismTemplateElementScoringRepository(_context);
            }
            return _MechanismTemplateElementScoring;
        }
        #endregion
        #region ClientMeeting Repository
        /// <summary>
        /// Provides database access for the ClientMeeting repository functions.
        /// </summary>
        private IGenericRepository<ClientMeeting> _ClientMeeting;
        public IGenericRepository<ClientMeeting> ClientMeetingRepository { get { return LazyLoadClientMeetings(); } }
        /// <summary>
        /// Lazy load the ClientMeeting.
        /// </summary>
        /// <returns>ClientMeeting</returns>
        private IGenericRepository<ClientMeeting> LazyLoadClientMeetings()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ClientMeeting == null)
            {
                this._ClientMeeting = new GenericRepository<ClientMeeting>(_context);
            }
            return _ClientMeeting;
        }
        #endregion
        #region StepType Repository
        /// <summary>
        /// Provides database access for the StepType repository functions.
        /// </summary>
        private IGenericRepository<StepType> _StepType;
        public IGenericRepository<StepType> StepTypeRepository { get { return LazyLoadStepTypesRepository(); } }
        /// <summary>
        /// Lazy load the StepType repository.
        /// </summary>
        /// <returns>StepTypeRepository</returns>
        private IGenericRepository<StepType> LazyLoadStepTypesRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._StepType == null)
            {
                this._StepType = new GenericRepository<StepType>(_context);
            }
            return _StepType;
        }
        #endregion
        #region ProgramMeeting Repository
        /// <summary>
        /// Provides database access for the ProgramMeeting repository functions.
        /// </summary>
        private IGenericRepository<ProgramMeeting> _ProgramMeeting;
        public IGenericRepository<ProgramMeeting> ProgramMeetingRepository { get { return LazyLoadProgramMeetings(); } }
        /// <summary>
        /// Lazy load the ProgramMeeting.
        /// </summary>
        /// <returns>ProgramMeeting</returns>
        private IGenericRepository<ProgramMeeting> LazyLoadProgramMeetings()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ProgramMeeting == null)
            {
                this._ProgramMeeting = new GenericRepository<ProgramMeeting>(_context);
            }
            return _ProgramMeeting;
        }
        #endregion

        #region ProgramSessionPayRate Repository
        /// <summary>
        /// Provides database access for the ProgramSessionPayRate repository functions.
        /// </summary>
        private IProgramSessionPayRateRepository _ProgramSessionPayRate;
        public IProgramSessionPayRateRepository ProgramSessionPayRateRepository { get { return LazyLoadProgramSessionPayRateRepository(); } }
        /// <summary>
        /// Lazy load the ProgramSessionPayRate repository.
        /// </summary>
        /// <returns>ProgramSessionPayRateRepository</returns>
        private IProgramSessionPayRateRepository LazyLoadProgramSessionPayRateRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ProgramSessionPayRate == null)
            {
                this._ProgramSessionPayRate = new ProgramSessionPayRateRepository(_context);
            }
            return _ProgramSessionPayRate;
        }
        #endregion


        #region 
        private IGenericRepository<ClientDataDeliverable> _clientDataDeliverable;
        /// <summary>
        /// Provides database access for client data deliverable functions.
        /// </summary>
        public IGenericRepository<ClientDataDeliverable> ClientDataDeliverableRepository { get { return lazyLoadClientDataDeliverableRepository(); } }
        /// <summary>
        /// Lazy load the ClientDataDeliverable repository.
        /// </summary>
        /// <returns>ClientDataDeliverableRepository</returns>
        private IGenericRepository<ClientDataDeliverable> lazyLoadClientDataDeliverableRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._clientDataDeliverable == null)
            {
                this._clientDataDeliverable = new GenericRepository<ClientDataDeliverable>(_context);
            }
            return _clientDataDeliverable;
        }
        #endregion
        #region ClientAwardType Repository
        /// <summary>
        /// Provides database access for the ClientAwardType repository functions.
        /// </summary>
        private IGenericRepository<ClientAwardType> _clientAwardType;
        public IGenericRepository<ClientAwardType> ClientAwardTypeRepository { get { return lazyLoadClientAwardTypeRepository(); } }
        /// <summary>
        /// Lazy load the ClientAwardType repository.
        /// </summary>
        /// <returns>ClientAwardTypeRepository</returns>
        private IGenericRepository<ClientAwardType> lazyLoadClientAwardTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._clientAwardType == null)
            {
                this._clientAwardType = new GenericRepository<ClientAwardType>(_context);
            }
            return _clientAwardType;
        }
        #endregion
        #region EmploymentCategory Repository
        /// <summary>
        /// Provides database access for the EmploymentCategory repository functions.
        /// </summary>
        private IGenericRepository<EmploymentCategory> _employmentCategory;
        public IGenericRepository<EmploymentCategory> EmploymentCategoryRepository { get { return lazyLoadEmploymentCategoryRepository(); } }
        /// <summary>
        /// Lazy load the EmploymentCategory repository.
        /// </summary>
        /// <returns>EmploymentCategoryRepository</returns>
        private IGenericRepository<EmploymentCategory> lazyLoadEmploymentCategoryRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._employmentCategory == null)
            {
                this._employmentCategory = new GenericRepository<EmploymentCategory>(_context);
            }
            return _employmentCategory;
        }
        #endregion
        #region ProgramCycleDeliverable Repository
        /// <summary>
        /// Provides database access for the ProgramCycleDeliverable repository functions.
        /// </summary>
        private IGenericRepository<ProgramCycleDeliverable> _programCycleDeliverable;
        public IGenericRepository<ProgramCycleDeliverable> ProgramCycleDeliverableRepository { get { return lazyLoadProgramCycleDeliverableRepository(); } }
        /// <summary>
        /// Lazy load the ProgramCycleDeliverable repository.
        /// </summary>
        /// <returns>ProgramCycleDeliverableRepository</returns>
        private IGenericRepository<ProgramCycleDeliverable> lazyLoadProgramCycleDeliverableRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }


            if (this._programCycleDeliverable == null)
            {
                this._programCycleDeliverable = new GenericRepository<ProgramCycleDeliverable>(_context);
            }
            return _programCycleDeliverable;
        }
        #endregion
        #region Deliverable Repository
        /// <summary>
        /// Provides database access for the Deliverable repository functions.
        /// </summary>
        private IDeliverableRepository _deliverableRepository;
        public IDeliverableRepository DeliverableRepository { get { return LazyLoadDeliverableRepository(); } }
        /// <summary>
        /// Lazy load the DeliverableRepository.
        /// </summary>
        /// <returns>DeliverableRepository</returns>
        private IDeliverableRepository LazyLoadDeliverableRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._deliverableRepository == null)
            {
                this._deliverableRepository = new DeliverableRepository(_context);
            }
            return _deliverableRepository;
        }
        #endregion
        #region ImportLog Repository
        /// <summary>
        /// Provides database access for the ImportLog repository functions.
        /// </summary>
        private IImportLogRepository _importLogRepository;
        public IImportLogRepository ImportLogRepository { get { return LazyLoadImportLogRepository(); } }
        /// <summary>
        /// Lazy load the ImportLogRepository.
        /// </summary>
        /// <returns>ImportLogRepository</returns>
        private IImportLogRepository LazyLoadImportLogRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._importLogRepository == null)
            {
                this._importLogRepository = new ImportLogRepository(_context);
            }
            return _importLogRepository;
        }
        #endregion
        #region ImportLogItem Repository
        /// <summary>
        /// Provides database access for the ImportLogItem repository functions.
        /// </summary>
        private IImportLogItemRepository _importLogItemRepository;
        public IImportLogItemRepository ImportLogItemRepository { get { return LazyLoadImportLogItemRepository(); } }
        /// <summary>
        /// Lazy load the ImportLogItemRepository.
        /// </summary>
        /// <returns>ImportLogItemRepository</returns>
        private IImportLogItemRepository LazyLoadImportLogItemRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._importLogItemRepository == null)
            {
                this._importLogItemRepository = new ImportLogItemRepository(_context);
            }
            return _importLogItemRepository;
        }
        #endregion
        #region ProgramMechanismImportLog Repository
        /// <summary>
        /// Provides database access for the ProgramMechanismImportLog repository functions.
        /// </summary>
        private IProgramMechanismImportLogRepository _programMechanismImportLogRepository;
        public IProgramMechanismImportLogRepository ProgramMechanismImportLogRepository { get { return LazyLoadProgramMechanismImportLogRepository(); } }
        /// <summary>
        /// Lazy load the ProgramMechanismImportLogRepository.
        /// </summary>
        /// <returns>ProgramMechanismImportLogRepository</returns>
        private IProgramMechanismImportLogRepository LazyLoadProgramMechanismImportLogRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._programMechanismImportLogRepository == null)
            {
                this._programMechanismImportLogRepository = new ProgramMechanismImportLogRepository(_context);
            }
            return _programMechanismImportLogRepository;
        }
        #endregion
        #region ApplicationInfo Repository
        /// <summary>
        /// Provides database access for the ApplicationInfo repository functions.
        /// </summary>
        private IApplicationInfoRepository _applicationInfoRepository;
        public IApplicationInfoRepository ApplicationInfoRepository { get { return LazyLoadApplicationInfoRepository(); } }
        /// <summary>
        /// Lazy load the ApplicationInfoRepository.
        /// </summary>
        /// <returns>ApplicationInfoRepository</returns>
        private IApplicationInfoRepository LazyLoadApplicationInfoRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._applicationInfoRepository == null)
            {
                this._applicationInfoRepository = new ApplicationInfoRepository(_context);
            }
            return _applicationInfoRepository;
        }
        #endregion
        #region ApplicationCompliance Repository
        /// <summary>
        /// Provides database access for the ApplicationCompliance repository functions.
        /// </summary>
        private IApplicationComplianceRepository _applicationComplianceRepository;
        public IApplicationComplianceRepository ApplicationComplianceRepository { get { return LazyLoadApplicationComplianceRepository(); } }
        /// <summary>
        /// Lazy load the ApplicationComplianceRepository.
        /// </summary>
        /// <returns>ApplicationComplianceRepository</returns>
        private IApplicationComplianceRepository LazyLoadApplicationComplianceRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._applicationComplianceRepository == null)
            {
                this._applicationComplianceRepository = new ApplicationComplianceRepository(_context);
            }
            return _applicationComplianceRepository;
        }
        #endregion
        #region ClientApplicationInfoType Repository
        /// <summary>
        /// Provides database access for the ClientApplicationInfoType repository functions.
        /// </summary>
        private IClientApplicationInfoTypeRepository _clientApplicationInfoTypeRepository;
        public IClientApplicationInfoTypeRepository ClientApplicationInfoTypeRepository { get { return LazyLoadClientApplicationInfoTypeRepository(); } }
        /// <summary>
        /// Lazy load the ClientApplicationInfoTypeRepository.
        /// </summary>
        /// <returns>ClientApplicationInfoTypeRepository</returns>
        private IClientApplicationInfoTypeRepository LazyLoadClientApplicationInfoTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._clientApplicationInfoTypeRepository == null)
            {
                this._clientApplicationInfoTypeRepository = new ClientApplicationInfoTypeRepository(_context);
            }
            return _clientApplicationInfoTypeRepository;
        }
        #endregion
        #region ComplianceStatus Repository
        /// <summary>
        /// Provides database access for the ComplianceStatus repository functions.
        /// </summary>
        private IComplianceStatusRepository _complianceStatusRepository;
        public IComplianceStatusRepository ComplianceStatusRepository { get { return LazyLoadComplianceStatusRepository(); } }
        /// <summary>
        /// Lazy load the ComplianceStatusRepository.
        /// </summary>
        /// <returns>ComplianceStatusRepository</returns>
        private IComplianceStatusRepository LazyLoadComplianceStatusRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._complianceStatusRepository == null)
            {
                this._complianceStatusRepository = new ComplianceStatusRepository(_context);
            }
            return _complianceStatusRepository;
        }
        #endregion
        #region ApplicationPersonnel Repository
        /// <summary>
        /// Provides database access for the ApplicationPersonnel repository functions.
        /// </summary>
        private IApplicationPersonnelRepository _applicationPersonnelRepository;
        public IApplicationPersonnelRepository ApplicationPersonnelRepository { get { return LazyLoadApplicationPersonnelRepository(); } }
        /// <summary>
        /// Lazy load the ApplicationPersonnelRepository.
        /// </summary>
        /// <returns>ApplicationPersonnelRepository</returns>
        private IApplicationPersonnelRepository LazyLoadApplicationPersonnelRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._applicationPersonnelRepository == null)
            {
                this._applicationPersonnelRepository = new ApplicationPersonnelRepository(_context);
            }
            return _applicationPersonnelRepository;
        }
        #endregion
        #region ClientApplicationPersonnelType Repository
        /// <summary>
        /// Provides database access for the ClientApplicationPersonnelType repository functions.
        /// </summary>
        private IClientApplicationPersonnelTypeRepository _clientApplicationPersonnelTypeRepository;
        public IClientApplicationPersonnelTypeRepository ClientApplicationPersonnelTypeRepository { get { return LazyLoadClientApplicationPersonnelTypeRepository(); } }
        /// <summary>
        /// Lazy load the ClientApplicationPersonnelTypeRepository.
        /// </summary>
        /// <returns>ClientApplicationPersonnelTypeRepository</returns>
        private IClientApplicationPersonnelTypeRepository LazyLoadClientApplicationPersonnelTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._clientApplicationPersonnelTypeRepository == null)
            {
                this._clientApplicationPersonnelTypeRepository = new ClientApplicationPersonnelTypeRepository(_context);
            }
            return _clientApplicationPersonnelTypeRepository;
        }
        #endregion
        #region Airport Repository
        /// <summary>
        /// Provides database access for the Airport repository functions.
        /// </summary>
        private IAirportRepository _airportRepository;
        public IAirportRepository AirportRepository { get { return LazyLoadAirportRepository(); } }
        /// <summary>
        /// Lazy load the AirportRepository.
        /// </summary>
        /// <returns>AirportRepository</returns>
        private IAirportRepository LazyLoadAirportRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._airportRepository == null)
            {
                this._airportRepository = new AirportRepository(_context);
            }
            return _airportRepository;
        }
        #endregion
        #region Carrier Repository
        /// <summary>
        /// Provides database access for the Carrier repository functions.
        /// </summary>
        private ICarrierRepository _carrierRepository;
        public ICarrierRepository CarrierRepository { get { return LazyLoadCarrierRepository(); } }
        /// <summary>
        /// Lazy load the CarrierRepository.
        /// </summary>
        /// <returns>CarrierRepository</returns>
        private ICarrierRepository LazyLoadCarrierRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._carrierRepository == null)
            {
                this._carrierRepository = new CarrierRepository(_context);
            }
            return _carrierRepository;
        }
        #endregion
        #region ClientSummaryTemplate Repository
        /// <summary>
        /// Provides database access for the ClientSummaryTemplate repository functions.
        /// </summary>
        private IGenericRepository<ClientSummaryTemplate> _ClientSummaryTemplate;
        public IGenericRepository<ClientSummaryTemplate> ClientSummaryTemplateRepository { get { return LazyLoadClientSummaryTemplates(); } }
        /// <summary>
        /// Lazy load the ClientSummaryTemplate.
        /// </summary>
        /// <returns>ClientSummaryTemplate</returns>
        private IGenericRepository<ClientSummaryTemplate> LazyLoadClientSummaryTemplates()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ClientSummaryTemplate == null)
            {
                this._ClientSummaryTemplate = new GenericRepository<ClientSummaryTemplate>(_context);
            }
            return _ClientSummaryTemplate;
        }
        #endregion
        #region ProgramMechanismSummaryStatement Repository
        /// <summary>
        /// Provides database access for the ProgramMechanismSummaryStatement repository functions.
        /// </summary>
        private IGenericRepository<ProgramMechanismSummaryStatement> _ProgramMechanismSummaryStatement;
        public IGenericRepository<ProgramMechanismSummaryStatement> ProgramMechanismSummaryStatementRepository { get { return LazyLoadProgramMechanismSummaryStatements(); } }
        /// <summary>
        /// Lazy load the ProgramMechanismSummaryStatement.
        /// </summary>
        /// <returns>ProgramMechanismSummaryStatement</returns>
        private IGenericRepository<ProgramMechanismSummaryStatement> LazyLoadProgramMechanismSummaryStatements()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ProgramMechanismSummaryStatement == null)
            {
                this._ProgramMechanismSummaryStatement = new GenericRepository<ProgramMechanismSummaryStatement>(_context);
            }
            return _ProgramMechanismSummaryStatement;
        }
        #endregion
        #region SummaryReviewerDescription Repository
        /// <summary>
        /// Provides database access for the SummaryReviewerDescription repository functions.
        /// </summary>
        private IGenericRepository<SummaryReviewerDescription> _SummaryReviewerDescription;
        public IGenericRepository<SummaryReviewerDescription> SummaryReviewerDescriptionRepository { get { return LazyLoadSummaryReviewerDescriptions(); } }
        /// <summary>
        /// Lazy load the SummaryReviewerDescription.
        /// </summary>
        /// <returns>SummaryReviewerDescription</returns>
        private IGenericRepository<SummaryReviewerDescription> LazyLoadSummaryReviewerDescriptions()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._SummaryReviewerDescription == null)
            {
                this._SummaryReviewerDescription = new GenericRepository<SummaryReviewerDescription>(_context);
            }
            return _SummaryReviewerDescription;
        }
        #endregion
    }
}
