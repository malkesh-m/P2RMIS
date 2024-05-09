using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.Repository;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Definition of repository classes for panel management entity objects.
    /// 
    /// In general entity objects that are specifically used by individual subsystems are
    /// located in source files named UnitOfWork."subsystem name".
    /// </summary>    
    public partial class UnitOfWork
    {
        #region PanelManagement Repository
        /// <summary>
        /// Provides database access for the PanelManagement repository functions.
        /// </summary>
        private IPanelManagementRepository _PanelManagementRepository;
        public IPanelManagementRepository PanelManagementRepository { get { return LazyLoadPanelManagementRepository(); } }
        /// <summary>
        /// Lazy load the PanelManagementRepository.
        /// </summary>
        /// <returns>PanelManagementRepository</returns>
        private IPanelManagementRepository LazyLoadPanelManagementRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PanelManagementRepository == null)
            {
                this._PanelManagementRepository = new PanelManagementRepository(_context);
            }
            return _PanelManagementRepository;
        }
        #endregion
        #region ActionLog Repository
        /// <summary>
        /// Provides database access for the ActionLog repository functions.
        /// </summary>
        private IActionLogRepository _actionLogRepository;
        public IActionLogRepository ActionLogRepository { get { return LazyLoadActionLogRepository(); } }
        /// <summary>
        /// Lazy load the ActionLogRepository.
        /// </summary>
        /// <returns>ActionLogRepository</returns>
        private IActionLogRepository LazyLoadActionLogRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._actionLogRepository == null)
            {
                this._actionLogRepository = new ActionLogRepository(_context);
            }
            return _actionLogRepository;
        }
        #endregion
        #region PanelApplication Repository
        /// <summary>
        /// Provides database access for the PanelApplication repository functions.
        /// </summary>
        private IPanelApplicationRepository _PanelApplicationRepository;
        public IPanelApplicationRepository PanelApplicationRepository { get { return LazyLoadPanelApplicationRepository(); } }
        /// <summary>
        /// Lazy load the PanelApplicationRepository.
        /// </summary>
        /// <returns>PanelApplicationRepository</returns>
        private IPanelApplicationRepository LazyLoadPanelApplicationRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PanelApplicationRepository == null)
            {
                this._PanelApplicationRepository = new PanelApplicationRepository(_context);
            }
            return _PanelApplicationRepository;
        }
        #endregion

        #region SessionPanel Repository
        /// <summary>
        /// Provides database access for the SessionPanel repository functions.
        /// </summary>
        private ISessionPanelRepository _SessionPanelRepository;
        public ISessionPanelRepository SessionPanelRepository { get { return LazyLoadSessionPanelRepository(); } }
        /// <summary>
        /// Lazy load the SessionPanelRepository.
        /// </summary>
        /// <returns>SessionPanelRepository</returns>
        private ISessionPanelRepository LazyLoadSessionPanelRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._SessionPanelRepository == null)
            {
                this._SessionPanelRepository = new SessionPanelRepository(_context);
            }
            return _SessionPanelRepository;
        }
        #endregion		
        #region ReviewerEvaluation Repository
        /// <summary>
        /// Provides database access for the Reviewer Evaluation repository functions.
        /// </summary>
        private IReviewerEvaluationRepository _reviewerEvaluationRepository;
        public IReviewerEvaluationRepository ReviewerEvaluationRepository { get { return LazyLoadReviewerEvaluationRepository(); } }
        /// <summary>
        /// Lazy load the ReviewerEvaluationRepository.
        /// </summary>
        /// <returns>ReviewerEvaluationRepository</returns>
        private IReviewerEvaluationRepository LazyLoadReviewerEvaluationRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._reviewerEvaluationRepository == null)
            {
                this._reviewerEvaluationRepository = new ReviewerEvaluationRepository(_context);
            }
            return _reviewerEvaluationRepository;
        }
        #endregion
        #region PanelApplicationReviewerExpertise Repository
        /// <summary>
        /// Provides database access for the PanelApplicationReviewerExpertise repository functions.
        /// </summary>
        private IPanelApplicationReviewerExpertiseRepository _panelApplicationReviewerExpertiseRepository;
        public IPanelApplicationReviewerExpertiseRepository PanelApplicationReviewerExpertiseRepository { get { return LazyLoadPanelApplicationReviewerExpertiseRepository(); } }
        /// <summary>
        /// Lazy load the PanelApplicationReviewerExpertiseRepository.
        /// </summary>
        /// <returns>PanelApplicationReviewerExpertiseRepository</returns>
        private IPanelApplicationReviewerExpertiseRepository LazyLoadPanelApplicationReviewerExpertiseRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._panelApplicationReviewerExpertiseRepository == null)
            {
                this._panelApplicationReviewerExpertiseRepository = new PanelApplicationReviewerExpertiseRepository(_context);
            }
            return _panelApplicationReviewerExpertiseRepository;
        }
        #endregion
        #region PanelApplicationReviewerAssignment Repository
        /// <summary>
        /// Provides database access for the PanelApplicationReviewerAssignment repository functions.
        /// </summary>
        private PanelApplicationReviewerAssignmentRepository _PanelApplicationReviewerAssignmentRepository;
        public IPanelApplicationReviewerAssignmentRepository PanelApplicationReviewerAssignmentRepository { get { return LazyLoadPanelApplicationReviewerAssignmentRepository(); } }
        /// <summary>
        /// Lazy load the PanelApplicationReviewerAssignmentRepository.
        /// </summary>
        /// <returns>PanelApplicationReviewerAssignmentRepository</returns>
        private IPanelApplicationReviewerAssignmentRepository LazyLoadPanelApplicationReviewerAssignmentRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PanelApplicationReviewerAssignmentRepository == null)
            {
                this._PanelApplicationReviewerAssignmentRepository = new PanelApplicationReviewerAssignmentRepository(_context);
            }
            return _PanelApplicationReviewerAssignmentRepository;
        }
        #endregion
        #region PanelUserAssignment Repository
        /// <summary>
        /// Provides database access for the PanelUserAssignment repository functions.
        /// </summary>
        private IPanelUserAssignmentRepository _PanelUserAssignmentRepository;
        public IPanelUserAssignmentRepository PanelUserAssignmentRepository { get { return LazyLoadPanelUserAssignmentRepository(); } }
        /// <summary>
        /// Lazy load the PanelUserAssignmentRepository.
        /// </summary>
        /// <returns>PanelUserAssignmentRepository</returns>
        private IPanelUserAssignmentRepository LazyLoadPanelUserAssignmentRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PanelUserAssignmentRepository == null)
            {
                this._PanelUserAssignmentRepository = new PanelUserAssignmentRepository(_context);
            }
            return _PanelUserAssignmentRepository;
        }
        #endregion
        #region ClientAssignmentType Repository
        /// <summary>
        /// Provides database access for the ClientAssignmentType repository functions.
        /// </summary>
        private IClientAssignmentTypeRepository _ClientAssignmentTypeRepository;
        public IClientAssignmentTypeRepository ClientAssignmentTypeRepository { get { return LazyLoadClientAssignmentTypeRepository(); } }
        /// <summary>
        /// Lazy load the ClientAssignmentTypeRepository.
        /// </summary>
        /// <returns>ClientAssignmentTypeRepository</returns>
        private IClientAssignmentTypeRepository LazyLoadClientAssignmentTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ClientAssignmentTypeRepository == null)
            {
                this._ClientAssignmentTypeRepository = new ClientAssignmentTypeRepository(_context);
            }
            return _ClientAssignmentTypeRepository;
        }
        #endregion		
        #region PanelApplicationReviewerCoiDetail Repository
        /// <summary>
        /// Provides database access for the ClientAssignmentType repository functions.
        /// </summary>
        private IPanelApplicationReviewerCoiDetailRepository _PanelApplicationReviewerCoiDetailRepository;
        public IPanelApplicationReviewerCoiDetailRepository PanelApplicationReviewerCoiDetailRepository { get { return LazyLoadPanelApplicationReviewerCoiDetailRepository(); } }
        /// <summary>
        /// Lazy load the ClientAssignmentTypeRepository.
        /// </summary>
        /// <returns>ClientAssignmentTypeRepository</returns>
        private IPanelApplicationReviewerCoiDetailRepository LazyLoadPanelApplicationReviewerCoiDetailRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PanelApplicationReviewerCoiDetailRepository == null)
            {
                this._PanelApplicationReviewerCoiDetailRepository = new PanelApplicationReviewerCoiDetailRepository(_context);
            }
            return _PanelApplicationReviewerCoiDetailRepository;
        }

        #endregion
        #region Email Repository
        /// <summary>
        /// Provides database access for the Email repository functions.
        /// </summary>
        private IEmailRepository _emailRepository;
        public IEmailRepository EmailRepository { get { return LazyLoadEmailRepository(); } }
        /// <summary>
        /// Lazy load the EmailRepository.
        /// </summary>
        /// <returns>EmailRepository</returns>
        private IEmailRepository LazyLoadEmailRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._emailRepository == null)
            {
                this._emailRepository = new EmailRepository(_context);
            }
            return _emailRepository;
        }
        #endregion
        #region MeetingSession Repository
        /// <summary>
        /// Provides database access for the MeetingSession repository functions.
        /// </summary>
        private IGenericRepository<MeetingSession> _MeetingSessionRepository;
        public IGenericRepository<MeetingSession> MeetingSessionRepository { get { return LazyLoadMeetingSessionRepository(); } }
        /// <summary>
        /// Lazy load the MeetingSessionRepository.
        /// </summary>
        /// <returns>MeetingSessionRepository</returns>
        private IGenericRepository<MeetingSession> LazyLoadMeetingSessionRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._MeetingSessionRepository == null)
            {
                this._MeetingSessionRepository = new GenericRepository<MeetingSession>(_context);
            }
            return _MeetingSessionRepository;
        }
        #endregion
        #region PanelStageStep Repository
        /// <summary>
        /// Provides database access for the PanelStageStep repository functions.
        /// </summary>
        private IGenericRepository<PanelStageStep> _PanelStageStepRepository;
        public IGenericRepository<PanelStageStep> PanelStageStepRepository { get { return LazyLoadPanelStageStepRepository(); } }
        /// <summary>
        /// Lazy load the PanelStageStepRepository.
        /// </summary>
        /// <returns>PanelStageStepRepository</returns>
        private IGenericRepository<PanelStageStep> LazyLoadPanelStageStepRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PanelStageStepRepository == null)
            {
                this._PanelStageStepRepository = new GenericRepository<PanelStageStep>(_context);
            }
            return _PanelStageStepRepository;
        }
        #endregion			
        #region PanelApplicationSummary Repository
        /// <summary>
        /// Provides database access for the PanelApplicationSummary repository functions.
        /// </summary>
        private IGenericRepository<PanelApplicationSummary> _PanelApplicationSummaryRepository;
        public IGenericRepository<PanelApplicationSummary> PanelApplicationSummaryRepository { get { return LazyLoadPanelApplicationSummaryRepository(); } }
        /// <summary>
        /// Lazy load the PanelApplicationSummaryRepository.
        /// </summary>
        /// <returns>PanelApplicationSummaryRepository</returns>
        private IGenericRepository<PanelApplicationSummary> LazyLoadPanelApplicationSummaryRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PanelApplicationSummaryRepository == null)
            {
                this._PanelApplicationSummaryRepository = new GenericRepository<PanelApplicationSummary>(_context);
            }
            return _PanelApplicationSummaryRepository;
        }
        #endregion	
        #region ClientExpertiseRating Repository
        /// <summary>
        /// Provides database access for the ClientExpertiseRating repository functions.
        /// </summary>
        private IGenericRepository<ClientExpertiseRating> _ClientExpertiseRatingRepository;
        public IGenericRepository<ClientExpertiseRating> ClientExpertiseRatingRepository { get { return LazyLoadClientExpertiseRatingRepository(); } }
        /// <summary>
        /// Lazy load the ClientExpertiseRatingRepository.
        /// </summary>
        /// <returns>ClientExpertiseRatingRepository</returns>
        private IGenericRepository<ClientExpertiseRating> LazyLoadClientExpertiseRatingRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ClientExpertiseRatingRepository == null)
            {
                this._ClientExpertiseRatingRepository = new GenericRepository<ClientExpertiseRating>(_context);
            }
            return _ClientExpertiseRatingRepository;
        }
        #endregion
        #region ClientParticipantType Repository
        /// <summary>
        /// Provides database access for the ClientParticipantType repository functions.
        /// </summary>
        private IGenericRepository<ClientParticipantType> _clientParticipantTypeRepository;
        public IGenericRepository<ClientParticipantType> ClientParticipantTypeRepository { get { return LazyLoadClientParticipantTypeRepository(); } }
        /// <summary>
        /// Lazy load the ClientParticipantTypeRepository.
        /// </summary>
        /// <returns>ClientParticipantTypeRepository</returns>
        private IGenericRepository<ClientParticipantType> LazyLoadClientParticipantTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._clientParticipantTypeRepository == null)
            {
                this._clientParticipantTypeRepository = new GenericRepository<ClientParticipantType>(_context);
            }
            return _clientParticipantTypeRepository;
        }
        #endregion
        #region ClientRole Repository
        /// <summary>
        /// Provides database access for the ClientRole repository functions.
        /// </summary>
        private IGenericRepository<ClientRole> _clientRoleRepository;
        public IGenericRepository<ClientRole> ClientRoleRepository { get { return LazyLoadClientRoleRepository(); } }
        /// <summary>
        /// Lazy load the ClientRoleRepository.
        /// </summary>
        /// <returns>ClientRoleRepository</returns>
        private IGenericRepository<ClientRole> LazyLoadClientRoleRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._clientRoleRepository == null)
            {
                this._clientRoleRepository = new GenericRepository<ClientRole>(_context);
            }
            return _clientRoleRepository;
        }
        #endregion
        #region UserCommunicationLog Repository
        /// <summary>
        /// Provides database access for the UserCommunicationLog repository functions.
        /// </summary>
        private IGenericRepository<UserCommunicationLog> _userCommunicationLogRepository;
        public IGenericRepository<UserCommunicationLog> UserCommunicationLogRepository { get { return LazyLoadUserCommunicationRepository(); } }
        /// <summary>
        /// Lazy load the UserCommunicationLogRepository.
        /// </summary>
        /// <returns>UserCommunicationLogRepository</returns>
        private IGenericRepository<UserCommunicationLog> LazyLoadUserCommunicationRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._userCommunicationLogRepository == null)
            {
                this._userCommunicationLogRepository = new GenericRepository<UserCommunicationLog>(_context);
            }
            return _userCommunicationLogRepository;
        }
        #endregion
        #region CommunicationMethod Repository
        /// <summary>
        /// Provides database access for the CommunicationMethod repository functions.
        /// </summary>
        private IGenericRepository<CommunicationMethod> _communicationMethodRepository;
        public IGenericRepository<CommunicationMethod> CommunicationMethodRepository { get { return LazyLoadCommunicationMethodRepository(); } }
        /// <summary>
        /// Lazy load the CommunicationMethodRepository.
        /// </summary>
        /// <returns>CommunicationMethodRepository</returns>
        private IGenericRepository<CommunicationMethod> LazyLoadCommunicationMethodRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._communicationMethodRepository == null)
            {
                this._communicationMethodRepository = new GenericRepository<CommunicationMethod>(_context);
            }
            return _communicationMethodRepository;
        }
        #endregion
        #region ParticipationMethod Repository
        /// <summary>
        /// Provides database access for the ParticipationMethod repository functions.
        /// </summary>
        private IGenericRepository<ParticipationMethod> _participationMethodRepository;
        public IGenericRepository<ParticipationMethod> ParticipationMethodRepository { get { return LazyLoadParticipationMethodRepository(); } }
        /// <summary>
        /// Lazy load the ParticipationMethodRepository.
        /// </summary>
        /// <returns>ParticipationMethodRepository</returns>
        private IGenericRepository<ParticipationMethod> LazyLoadParticipationMethodRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._participationMethodRepository == null)
            {
                this._participationMethodRepository = new GenericRepository<ParticipationMethod>(_context);
            }
            return _participationMethodRepository;
        }
        #endregion
        #region PanelUserPotentialAssignment Repository
        /// <summary>
        /// Provides database access for the PanelUserPotentialAssignment repository functions.
        /// </summary>
        private IGenericRepository<PanelUserPotentialAssignment> _panelUserPotentialAssignmentRepository;
        public IGenericRepository<PanelUserPotentialAssignment> PanelUserPotentialAssignmentRepository { get { return LazyLoadPanelUserPotentialAssignmentRepository(); } }
        /// <summary>
        /// Lazy load the PanelUserPotentialAssignmentRepository.
        /// </summary>
        /// <returns>PanelUserPotentialAssignmentRepository</returns>
        private IGenericRepository<PanelUserPotentialAssignment> LazyLoadPanelUserPotentialAssignmentRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._panelUserPotentialAssignmentRepository == null)
            {
                this._panelUserPotentialAssignmentRepository = new GenericRepository<PanelUserPotentialAssignment>(_context);
            }
            return _panelUserPotentialAssignmentRepository;
        }
        #endregion
        #region AssignmentTypeThreshold Repository
        /// <summary>
        /// Provides database access for the AssignmentTypeThreshold repository functions.
        /// </summary>
        private IAssignmentTypeThresholdRepository _assignmentTypeThresholdRepository;
        public IAssignmentTypeThresholdRepository AssignmentTypeThresholdRepository { get { return LazyLoadAssignmentTypeThresholdRepository(); } }
        /// <summary>
        /// Lazy load the AssignmentTypeThreshold.
        /// </summary>
        /// <returns>AssignmentTypeThresholdRepository</returns>
        private IAssignmentTypeThresholdRepository LazyLoadAssignmentTypeThresholdRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._assignmentTypeThresholdRepository == null)
            {
                this._assignmentTypeThresholdRepository = new AssignmentTypeThresholdRepository(_context);
            }
            return _assignmentTypeThresholdRepository;
        }
        #endregion

        #region ProgramUserAssignment Repository

        /// <summary>
        /// The program user assignment repository
        /// </summary>
        private IGenericRepository<ProgramUserAssignment> _ProgramUserAssignmentRepository;
        public IGenericRepository<ProgramUserAssignment> ProgramUserAssignmentRepository { get { return LazyLoadProgramUserAssignmentRepository(); } }

        /// <summary>
        /// Lazies the load program user assignment repository.
        /// </summary>
        /// <returns></returns>
        private IGenericRepository<ProgramUserAssignment> LazyLoadProgramUserAssignmentRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ProgramUserAssignmentRepository == null)
            {
                this._ProgramUserAssignmentRepository = new GenericRepository<ProgramUserAssignment>(_context);
            }
            return _ProgramUserAssignmentRepository;
        }
        #endregion

        #region SessionUserAssignment Repository

        /// <summary>
        /// The program user assignment repository
        /// </summary>
        private IGenericRepository<SessionUserAssignment> _SessionUserAssignmentRepository;
        public IGenericRepository<SessionUserAssignment> SessionUserAssignmentRepository { get { return LazyLoadSessionUserAssignmentRepository(); } }

        /// <summary>
        /// Lazies the load program user assignment repository.
        /// </summary>
        /// <returns></returns>
        private IGenericRepository<SessionUserAssignment> LazyLoadSessionUserAssignmentRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._SessionUserAssignmentRepository == null)
            {
                this._SessionUserAssignmentRepository = new GenericRepository<SessionUserAssignment>(_context);
            }
            return _SessionUserAssignmentRepository;
        }
        #endregion
    }
}
