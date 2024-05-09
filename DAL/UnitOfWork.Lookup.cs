using Sra.P2rmis.Dal.Repository.Setup;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Definition of repository classes for lookup entity objects.
    /// 
    /// In general entity objects that are specifically used by individual subsystems are
    /// located in source files named UnitOfWork."subsystem name".
    /// </summary> 
    public partial class UnitOfWork
    {
        #region AddressType Repository
        /// <summary>
        /// Provides database access for the AddressType repository functions.
        /// </summary>
        private IGenericRepository<AddressType> _AddressTypeRepository;
        public IGenericRepository<AddressType> AddressTypeRepository { get { return LazyLoadAddressTypeRepository(); } }
        /// <summary>
        /// Lazy load the AddressTypeRepository.
        /// </summary>
        /// <returns>AddressTypeRepository</returns>
        private IGenericRepository<AddressType> LazyLoadAddressTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._AddressTypeRepository == null)
            {
                this._AddressTypeRepository = new GenericRepository<AddressType>(_context);
            }
            return _AddressTypeRepository;
        }
        #endregion		      
        #region AssignmentType Repository
        /// <summary>
        /// Provides database access for the AssignmentType repository functions.
        /// </summary>
        private IGenericRepository<AssignmentType> _AssignmentTypeRepository;
        public IGenericRepository<AssignmentType> AssignmentTypeRepository { get { return LazyLoadAssignmentTypeRepository(); } }
        /// <summary>
        /// Lazy load the AssignmentTypeRepository.
        /// </summary>
        /// <returns>AssignmentTypeRepository</returns>
        private IGenericRepository<AssignmentType> LazyLoadAssignmentTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._AssignmentTypeRepository == null)
            {
                this._AssignmentTypeRepository = new GenericRepository<AssignmentType>(_context);
            }
            return _AssignmentTypeRepository;
        }
        #endregion		    
        #region Country Repository
        /// <summary>
        /// Provides database access for the Country repository functions.
        /// </summary>
        private IGenericRepository<Country> _CountryRepository;
        public IGenericRepository<Country> CountryRepository { get { return LazyLoadCountryRepository(); } }
        /// <summary>
        /// Lazy load the CountryRepository.
        /// </summary>
        /// <returns>CountryRepository</returns>
        private IGenericRepository<Country> LazyLoadCountryRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._CountryRepository == null)
            {
                this._CountryRepository = new GenericRepository<Country>(_context);
            }
            return _CountryRepository;
        }
        #endregion		
        #region Degree Repository
        /// <summary>
        /// Provides database access for the Degree repository functions.
        /// </summary>
        private IGenericRepository<Degree> _DegreeRepository;
        public IGenericRepository<Degree> DegreeRepository { get { return LazyLoadDegreeRepository(); } }
        /// <summary>
        /// Lazy load the DegreeRepository.
        /// </summary>
        /// <returns>DegreeRepository</returns>
        private IGenericRepository<Degree> LazyLoadDegreeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._DegreeRepository == null)
            {
                this._DegreeRepository = new GenericRepository<Degree>(_context);
            }
            return _DegreeRepository;
        }
        #endregion		   
        #region Ethnicity Repository
        /// <summary>
        /// Provides database access for the Ethnicity repository functions.
        /// </summary>
        private IGenericRepository<Ethnicity> _EthnicityRepository;
        public IGenericRepository<Ethnicity> EthnicityRepository { get { return LazyLoadEthnicityRepository(); } }
        /// <summary>
        /// Lazy load the EthnicityRepository.
        /// </summary>
        /// <returns>EthnicityRepository</returns>
        private IGenericRepository<Ethnicity> LazyLoadEthnicityRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._EthnicityRepository == null)
            {
                this._EthnicityRepository = new GenericRepository<Ethnicity>(_context);
            }
            return _EthnicityRepository;
        }
        #endregion		
        #region Gender Repository
        /// <summary>
        /// Provides database access for the Gender repository functions.
        /// </summary>
        private IGenericRepository<Gender> _GenderRepository;
        public IGenericRepository<Gender> GenderRepository { get { return LazyLoadGenderRepository(); } }
        /// <summary>
        /// Lazy load the GenderRepository.
        /// </summary>
        /// <returns>GenderRepository</returns>
        private IGenericRepository<Gender> LazyLoadGenderRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._GenderRepository == null)
            {
                this._GenderRepository = new GenericRepository<Gender>(_context);
            }
            return _GenderRepository;
        }
        #endregion		      
        #region Prefix Repository
        /// <summary>
        /// Provides database access for the Prefix repository functions.
        /// </summary>
        private IGenericRepository<Prefix> _PrefixRepository;
        public IGenericRepository<Prefix> PrefixRepository { get { return LazyLoadPrefixRepository(); } }
        /// <summary>
        /// Lazy load the PrefixRepository.
        /// </summary>
        /// <returns>PrefixRepository</returns>
        private IGenericRepository<Prefix> LazyLoadPrefixRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PrefixRepository == null)
            {
                this._PrefixRepository = new GenericRepository<Prefix>(_context);
            }
            return _PrefixRepository;
        }
        #endregion		  	
        #region State Repository
        /// <summary>
        /// Provides database access for the State repository functions.
        /// </summary>
        private IGenericRepository<State> _StateRepository;
        public IGenericRepository<State> StateRepository { get { return LazyLoadStateRepository(); } }
        /// <summary>
        /// Lazy load the StateRepository.
        /// </summary>
        /// <returns>StateRepository</returns>
        private IGenericRepository<State> LazyLoadStateRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._StateRepository == null)
            {
                this._StateRepository = new GenericRepository<State>(_context);
            }
            return _StateRepository;
        }
        #endregion		
        #region PhoneType Repository
        /// <summary>
        /// Provides database access for the PhoneType repository functions.
        /// </summary>
        private IGenericRepository<PhoneType> _PhoneTypeRepository;
        public IGenericRepository<PhoneType> PhoneTypeRepository { get { return LazyLoadPhoneTypeRepository(); } }
        /// <summary>
        /// Lazy load the PhoneTypeRepository.
        /// </summary>
        /// <returns>PhoneTypeRepository</returns>
        private IGenericRepository<PhoneType> LazyLoadPhoneTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PhoneTypeRepository == null)
            {
                this._PhoneTypeRepository = new GenericRepository<PhoneType>(_context);
            }
            return _PhoneTypeRepository;
        }
        #endregion
        #region MilitaryRank Repository
        /// <summary>
        /// Provides database access for the MilitaryRank repository functions.
        /// </summary>
        private IGenericRepository<MilitaryRank> _MilitaryRankRepository;
        public IGenericRepository<MilitaryRank> MilitaryRankRepository { get { return LazyLoadMilitaryRankRepository(); } }
        /// <summary>
        /// Lazy load the MilitaryRankRepository.
        /// </summary>
        /// <returns>MilitaryRankRepository</returns>
        private IGenericRepository<MilitaryRank> LazyLoadMilitaryRankRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._MilitaryRankRepository == null)
            {
                this._MilitaryRankRepository = new GenericRepository<MilitaryRank>(_context);
            }
            return _MilitaryRankRepository;
        }
        #endregion		
        #region ProfileType Repository
        /// <summary>
        /// Provides database access for the ProfileType repository functions.
        /// </summary>
        private IGenericRepository<ProfileType> _ProfileTypeRepository;
        public IGenericRepository<ProfileType> ProfileTypeRepository { get { return LazyLoadProfileTypeRepository(); } }
        /// <summary>
        /// Lazy load the ProfileTypeRepository.
        /// </summary>
        /// <returns>ProfileTypeRepository</returns>
        private IGenericRepository<ProfileType> LazyLoadProfileTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ProfileTypeRepository == null)
            {
                this._ProfileTypeRepository = new GenericRepository<ProfileType>(_context);
            }
            return _ProfileTypeRepository;
        }
        #endregion		
        #region WebsiteType Repository
        /// <summary>
        /// Provides database access for the WebsiteType repository functions.
        /// </summary>
        private IGenericRepository<WebsiteType> _WebsiteTypeRepository;
        public IGenericRepository<WebsiteType> WebsiteTypeRepository { get { return LazyLoadWebsiteTypeRepository(); } }
        /// <summary>
        /// Lazy load the WebsiteTypeRepository.
        /// </summary>
        /// <returns>WebsiteTypeRepository</returns>
        private IGenericRepository<WebsiteType> LazyLoadWebsiteTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._WebsiteTypeRepository == null)
            {
                this._WebsiteTypeRepository = new GenericRepository<WebsiteType>(_context);
            }
            return _WebsiteTypeRepository;
        }
        #endregion
        #region MilitaryStatusType Repository
        /// <summary>
        /// Provides database access for the MilitaryStatusType repository functions.
        /// </summary>
        private IGenericRepository<MilitaryStatusType> _MilitaryStatusTypeRepository;
        public IGenericRepository<MilitaryStatusType> MilitaryStatusTypeRepository { get { return LazyLoadMilitaryStatusTypeRepository(); } }
        /// <summary>
        /// Lazy load the MilitaryStatusTypeRepository.
        /// </summary>
        /// <returns>MilitaryStatusTypeRepository</returns>
        private IGenericRepository<MilitaryStatusType> LazyLoadMilitaryStatusTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._MilitaryStatusTypeRepository == null)
            {
                this._MilitaryStatusTypeRepository = new GenericRepository<MilitaryStatusType>(_context);
            }
            return _MilitaryStatusTypeRepository;
        }
        #endregion		
        #region EmailAddressType Repository
        /// <summary>
        /// Provides database access for the EmailAddressType repository functions.
        /// </summary>
        private IGenericRepository<EmailAddressType> _EmailAddressTypeRepository;
        public IGenericRepository<EmailAddressType> EmailAddressTypeRepository { get { return LazyLoadEmailAddressTypeRepository(); } }
        /// <summary>
        /// Lazy load the EmailAddressTypeRepository.
        /// </summary>
        /// <returns>EmailAddressTypeRepository</returns>
        private IGenericRepository<EmailAddressType> LazyLoadEmailAddressTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._EmailAddressTypeRepository == null)
            {
                this._EmailAddressTypeRepository = new GenericRepository<EmailAddressType>(_context);
            }
            return _EmailAddressTypeRepository;
        }
        #endregion		
        #region AlternateContactType Repository
        /// <summary>
        /// Provides database access for the AlternateContactType repository functions.
        /// </summary>
        private IGenericRepository<AlternateContactType> _AlternateContactTypeRepository;
        public IGenericRepository<AlternateContactType> AlternateContactTypeRepository { get { return LazyLoadAlternateContactTypeRepository(); } }
        /// <summary>
        /// Lazy load the AlternateContactTypeRepository.
        /// </summary>
        /// <returns>AlternateContactTypeRepository</returns>
        private IGenericRepository<AlternateContactType> LazyLoadAlternateContactTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._AlternateContactTypeRepository == null)
            {
                this._AlternateContactTypeRepository = new GenericRepository<AlternateContactType>(_context);
            }
            return _AlternateContactTypeRepository;
        }
        #endregion

        #region ContractStatus Repository
        /// <summary>
        /// Provides database access for the Ethnicity repository functions.
        /// </summary>
        private IGenericRepository<ContractStatus> _ContractStatusRepository;
        public IGenericRepository<ContractStatus> ContractStatusRepository { get { return LazyLoadContractStatusRepository(); } }
        /// <summary>
        /// Lazy load the EthnicityRepository.
        /// </summary>
        /// <returns>EthnicityRepository</returns>
        private IGenericRepository<ContractStatus> LazyLoadContractStatusRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ContractStatusRepository == null)
            {
                this._ContractStatusRepository = new GenericRepository<ContractStatus>(_context);
            }
            return _ContractStatusRepository;
        }
        #endregion		

        #region LookupCommentType Repository
        /// <summary>
        /// Provides database access for the LookupCommentType repository functions.
        /// </summary>
        private IGenericRepository<LookupCommentType> _LookupCommentTypeRepository;
        public IGenericRepository<LookupCommentType> LookupCommentTypeRepository { get { return LazyLoadLookupCommentTypeRepository(); } }
        /// <summary>
        /// Lazy load the LookupCommentTypeRepository.
        /// </summary>
        /// <returns>LookupCommentTypeRepository</returns>
        private IGenericRepository<LookupCommentType> LazyLoadLookupCommentTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._LookupCommentTypeRepository == null)
            {
                this._LookupCommentTypeRepository = new GenericRepository<LookupCommentType>(_context);
            }
            return _LookupCommentTypeRepository;
        }
        #endregion		   
        #region LookupTemplateCategory Repository
        /// <summary>
        /// Provides database access for the LookupTemplateCategory repository functions.
        /// </summary>
        private IGenericRepository<LookupTemplateCategory> _LookupTemplateCategoryRepository;
        public IGenericRepository<LookupTemplateCategory> LookupTemplateCategoryRepository { get { return LazyLoadLookupTemplateCategoryRepository(); } }
        /// <summary>
        /// Lazy load the LookupTemplateCategoryRepository.
        /// </summary>
        /// <returns>LookupTemplateCategoryRepository</returns>
        private IGenericRepository<LookupTemplateCategory> LazyLoadLookupTemplateCategoryRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._LookupTemplateCategoryRepository == null)
            {
                this._LookupTemplateCategoryRepository = new GenericRepository<LookupTemplateCategory>(_context);
            }
            return _LookupTemplateCategoryRepository;
        }
        #endregion		        
        #region LookupTemplateStage Repository
        /// <summary>
        /// Provides database access for the LookupTemplateStage repository functions.
        /// </summary>
        private IGenericRepository<LookupTemplateStage> _LookupTemplateStageRepository;
        public IGenericRepository<LookupTemplateStage> LookupTemplateStageRepository { get { return LazyLoadLookupTemplateStageRepository(); } }
        /// <summary>
        /// Lazy load the LookupTemplateStageRepository.
        /// </summary>
        /// <returns>LookupTemplateStageRepository</returns>
        private IGenericRepository<LookupTemplateStage> LazyLoadLookupTemplateStageRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._LookupTemplateStageRepository == null)
            {
                this._LookupTemplateStageRepository = new GenericRepository<LookupTemplateStage>(_context);
            }
            return _LookupTemplateStageRepository;
        }
        #endregion		
        #region LookupTemplateType Repository
        /// <summary>
        /// Provides database access for the LookupTemplateType repository functions.
        /// </summary>
        private IGenericRepository<LookupTemplateType> _LookupTemplateTypeRepository;
        public IGenericRepository<LookupTemplateType> LookupTemplateTypeRepository { get { return LazyLoadLookupTemplateTypeRepository(); } }
        /// <summary>
        /// Lazy load the LookupTemplateTypeRepository.
        /// </summary>
        /// <returns>LookupTemplateTypeRepository</returns>
        private IGenericRepository<LookupTemplateType> LazyLoadLookupTemplateTypeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._LookupTemplateTypeRepository == null)
            {
                this._LookupTemplateTypeRepository = new GenericRepository<LookupTemplateType>(_context);
            }
            return _LookupTemplateTypeRepository;
        }
        #endregion		
        #region RecoveryQuestionRepository Repository
        /// <summary>
        /// Provides database access for the RecoveryQuestionRepository repository functions.
        /// </summary>
        private IGenericRepository<RecoveryQuestion> _RecoveryQuestionRepository;
        public IGenericRepository<RecoveryQuestion> RecoveryQuestionRepository { get { return LazyLoadRecoveryQuestionRepository(); } }
        /// <summary>
        /// Lazy load the RecoveryQuestionRepository.
        /// </summary>
        /// <returns>RecoveryQuestionRepository</returns>
        private IGenericRepository<RecoveryQuestion> LazyLoadRecoveryQuestionRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._RecoveryQuestionRepository == null)
            {
                this._RecoveryQuestionRepository = new GenericRepository<RecoveryQuestion>(_context);
            }
            return _RecoveryQuestionRepository;
        }
        #endregion
        #region TravelMode Repository
        /// <summary>
        /// Provides database access for the TravelModeRepository repository functions.
        /// </summary>
        private IGenericRepository<TravelMode> _TravelModeRepository;
        public IGenericRepository<TravelMode> TravelModeRepository { get { return LazyLoadTravelModeRepository(); } }
        /// <summary>
        /// Lazy load the TravelModeRepository.
        /// </summary>
        /// <returns>TravelModeRepository</returns>
        private IGenericRepository<TravelMode> LazyLoadTravelModeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._TravelModeRepository == null)
            {
                this._TravelModeRepository = new GenericRepository<TravelMode>(_context);
            }
            return _TravelModeRepository;
        }
        #endregion
        #region ScoringTemplate
        /// <summary>
        /// Provides database access for the ScoringTemplate repository functions.
        /// </summary>
        private IGenericRepository<ScoringTemplate> _ScoringTemplate;
        public IGenericRepository<ScoringTemplate> ScoringTemplate { get { return LazyLoadScoringTemplate(); } }
        /// <summary>
        /// Lazy load the ScoringTemplate.
        /// </summary>
        /// <returns>ScoringTemplate</returns>
        private IGenericRepository<ScoringTemplate> LazyLoadScoringTemplate()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ScoringTemplate == null)
            {
                this._ScoringTemplate = new GenericRepository<ScoringTemplate>(_context);
            }
            return _ScoringTemplate;
        }
        #endregion
        #region ScoringTemplatePhase Repository
        /// <summary>
        /// Provides database access for the ScoringTemplatePhase repository functions.
        /// </summary>
        private IScoringTemplatePhaseRepository _ScoringTemplatePhase;
        public IScoringTemplatePhaseRepository ScoringTemplatePhaseRepository { get { return LazyLoadScoringTemplatePhase(); } }
        /// <summary>
        /// Lazy load the ScoringTemplatePhase.
        /// </summary>
        /// <returns>ScoringTemplatePhase</returns>
        private IScoringTemplatePhaseRepository LazyLoadScoringTemplatePhase()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ScoringTemplatePhase == null)
            {
                this._ScoringTemplatePhase = new ScoringTemplatePhaseRepository(_context);
            }
            return _ScoringTemplatePhase;
        }
        #endregion
        #region MeetingType
        /// <summary>
        /// Provides database access for the MeetingType repository functions.
        /// </summary>
        private IGenericRepository<MeetingType> _MeetingType;
        public IGenericRepository<MeetingType> MeetingTypeRepository { get { return LazyLoadMeetingType(); } }
        /// <summary>
        /// Lazy load the MeetingType.
        /// </summary>
        /// <returns>MeetingType</returns>
        private IGenericRepository<MeetingType> LazyLoadMeetingType()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._MeetingType == null)
            {
                this._MeetingType = new GenericRepository<MeetingType>(_context);
            }
            return _MeetingType;
        }
        #endregion
        #region Hotel
        /// <summary>
        /// Provides database access for the Hotel repository functions.
        /// </summary>
        private IGenericRepository<Hotel> _Hotel;
        public IGenericRepository<Hotel> HotelRepository { get { return LazyLoadHotel(); } }
        /// <summary>
        /// Lazy load the Hotel.
        /// </summary>
        /// <returns>Hotel</returns>
        private IGenericRepository<Hotel> LazyLoadHotel()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._Hotel == null)
            {
                this._Hotel = new GenericRepository<Hotel>(_context);
            }
            return _Hotel;
        }
        #endregion

        #region PolicyType
        /// <summary>
        /// Provides database access for the PolicyType repository functions.
        /// </summary>
        private IGenericRepository<PolicyType> _PolicyType;
        public IGenericRepository<PolicyType> PolicyTypeRepository { get { return LazyLoadPolicyType(); } }
        /// <summary>
        /// Lazy load the PolicyType.
        /// </summary>
        /// <returns>PolicyType</returns>
        private IGenericRepository<PolicyType> LazyLoadPolicyType()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PolicyType == null)
            {
                this._PolicyType = new GenericRepository<PolicyType>(_context);
            }
            return _PolicyType;
        }
        #endregion
        #region PolicyRestrictionType
        /// <summary>
        /// Provides database access for the PolicyRestrictionType repository functions.
        /// </summary>
        private IGenericRepository<PolicyRestrictionType> _PolicyRestrictionType;
        public IGenericRepository<PolicyRestrictionType> PolicyRestrictionTypeRepository { get { return LazyLoadPolicyRestrictionType(); } }
        /// <summary>
        /// Lazy load the PolicyRestrictionType.
        /// </summary>
        /// <returns>PolicyRestrictionType</returns>
        private IGenericRepository<PolicyRestrictionType> LazyLoadPolicyRestrictionType()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PolicyRestrictionType == null)
            {
                this._PolicyRestrictionType = new GenericRepository<PolicyRestrictionType>(_context);
            }
            return _PolicyRestrictionType;
        }
        #endregion
        #region WeekDayType
        /// <summary>
        /// Provides database access for the WeekDayType repository functions.
        /// </summary>
        private IGenericRepository<WeekDay> _WeekDay;
        public IGenericRepository<WeekDay> WeekDayRepository { get { return LazyLoadWeekDay(); } }
        /// <summary>
        /// Lazy load the WeekDayType.
        /// </summary>
        /// <returns>WeekDayType</returns>
        private IGenericRepository<WeekDay> LazyLoadWeekDay()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._WeekDay == null)
            {
                this._WeekDay = new GenericRepository<WeekDay>(_context);
            }
            return _WeekDay;
        }
        #endregion
    }
}
