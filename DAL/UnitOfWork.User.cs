using Sra.P2rmis.Dal.Repository.UserProfileManagement;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Definition of repository classes for User profile management entity objects.
    /// 
    /// In general entity objects that are specifically used by individual subsystems are
    /// located in source files named UnitOfWork."subsystem name".
    /// </summary> 
    public partial class UnitOfWork
    {
        #region UserAddress Repository
        /// <summary>
        /// Provides database access for the UserAddress repository functions.
        /// </summary>
        private IGenericRepository<UserAddress> _UserAddressRepository;
        public IGenericRepository<UserAddress> UserAddressRepository { get { return LazyLoadUserAddressRepository(); } }
        /// <summary>
        /// Lazy load the UserAddressRepository.
        /// </summary>
        /// <returns>UserAddressRepository</returns>
        private IGenericRepository<UserAddress> LazyLoadUserAddressRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserAddressRepository == null)
            {
                this._UserAddressRepository = new GenericRepository<UserAddress>(_context);
            }
            return _UserAddressRepository;
        }
        #endregion		
        #region UserAlternateContactPhone Repository
        /// <summary>
        /// Provides database access for the UserAlternateContactPhone repository functions.
        /// </summary>
        private IGenericRepository<UserAlternateContactPhone> _UserAlternateContactPhoneRepository;
        public IGenericRepository<UserAlternateContactPhone> UserAlternateContactPhoneRepository { get { return LazyLoadUserAlternateContactPhoneRepository(); } }
        /// <summary>
        /// Lazy load the UserAlternateContactPhoneRepository.
        /// </summary>
        /// <returns>UserAlternateContactPhone</returns>
        private IGenericRepository<UserAlternateContactPhone> LazyLoadUserAlternateContactPhoneRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserAlternateContactPhoneRepository == null)
            {
                this._UserAlternateContactPhoneRepository = new GenericRepository<UserAlternateContactPhone>(_context);
            }
            return _UserAlternateContactPhoneRepository;
        }
        #endregion
        #region UserAlternateContact Repository
        /// <summary>
        /// Provides database access for the UserAlternateContact repository functions.
        /// </summary>
        private IGenericRepository<UserAlternateContact> _UserAlternateContactRepository;
        public IGenericRepository<UserAlternateContact> UserAlternateContactRepository { get { return LazyLoadUserAlternateContactRepository(); } }
        /// <summary>
        /// Lazy load the UserAlternateContactRepository.
        /// </summary>
        /// <returns>UserAlternateContac</returns>
        private IGenericRepository<UserAlternateContact> LazyLoadUserAlternateContactRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserAlternateContactRepository == null)
            {
                this._UserAlternateContactRepository = new GenericRepository<UserAlternateContact>(_context);
            }
            return _UserAlternateContactRepository;
        }
        #endregion
        #region UserDegree Repository
        /// <summary>
        /// Provides database access for the UserDegree repository functions.
        /// </summary>
        private IGenericRepository<UserDegree> _UserDegreeRepository;
        public IGenericRepository<UserDegree> UserDegreeRepository { get { return LazyLoadUserDegreeRepository(); } }
        /// <summary>
        /// Lazy load the UserDegreeRepository.
        /// </summary>
        /// <returns>UserDegreeRepository</returns>
        private IGenericRepository<UserDegree> LazyLoadUserDegreeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserDegreeRepository == null)
            {
                this._UserDegreeRepository = new GenericRepository<UserDegree>(_context);
            }
            return _UserDegreeRepository;
        }
        #endregion		
        #region UserPassword Repository
        /// <summary>
        /// Provides database access for the UserPassword repository functions.
        /// </summary>
        private IGenericRepository<UserPassword> _UserPasswordRepository;
        public IGenericRepository<UserPassword> UserPasswordRepository { get { return LazyLoadUserPasswordRepository(); } }
        /// <summary>
        /// Lazy load the UserPasswordRepository.
        /// </summary>
        /// <returns>UserPasswordRepository</returns>
        private IGenericRepository<UserPassword> LazyLoadUserPasswordRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserPasswordRepository == null)
            {
                this._UserPasswordRepository = new GenericRepository<UserPassword>(_context);
            }
            return _UserPasswordRepository;
        }
        #endregion	
        #region UserEmail Repository
        /// <summary>
        /// Provides database access for the UserEmail repository functions.
        /// </summary>
        private IGenericRepository<UserEmail> _UserEmailRepository;
        public IGenericRepository<UserEmail> UserEmailRepository { get { return LazyLoadUserEmailRepository(); } }
        /// <summary>
        /// Lazy load the UserEmailRepository.
        /// </summary>
        /// <returns>UserEmailRepository</returns>
        private IGenericRepository<UserEmail> LazyLoadUserEmailRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserEmailRepository == null)
            {
                this._UserEmailRepository = new GenericRepository<UserEmail>(_context);
            }
            return _UserEmailRepository;
        }
        #endregion		
        #region UserInfo Repository
        /// <summary>
        /// Provides database access for the UserInfo repository functions.
        /// </summary>
        private IGenericRepository<UserInfo> _UserInfoRepository;
        public IGenericRepository<UserInfo> UserInfoRepository { get { return LazyLoadUserInfoRepository(); } }
        /// <summary>
        /// Lazy load the UserInfoRepository.
        /// </summary>
        /// <returns>UserInfoRepository</returns>
        private IGenericRepository<UserInfo> LazyLoadUserInfoRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserInfoRepository == null)
            {
                this._UserInfoRepository = new GenericRepository<UserInfo>(_context);
            }
            return _UserInfoRepository;
        }
        #endregion		 
        #region UserPhone Repository
        /// <summary>
        /// Provides database access for the UserPhone repository functions.
        /// </summary>
        private IGenericRepository<UserPhone> _UserPhoneRepository;
        public IGenericRepository<UserPhone> UserPhoneRepository { get { return LazyLoadUserPhoneRepository(); } }
        /// <summary>
        /// Lazy load the UserPhoneRepository.
        /// </summary>
        /// <returns>UserPhoneRepository</returns>
        private IGenericRepository<UserPhone> LazyLoadUserPhoneRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserPhoneRepository == null)
            {
                this._UserPhoneRepository = new GenericRepository<UserPhone>(_context);
            }
            return _UserPhoneRepository;
        }
        #endregion
        #region UserResume Repository
        /// <summary>
        /// Provides database access for the UserResume repository functions.
        /// </summary>
        private IGenericRepository<UserResume> _UserResumeRepository;
        public IGenericRepository<UserResume> UserResumeRepository { get { return LazyLoadUserResumeRepository(); } }
        /// <summary>
        /// Lazy load the UserResumeRepository.
        /// </summary>
        /// <returns>UserResumeRepository</returns>
        private IGenericRepository<UserResume> LazyLoadUserResumeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserResumeRepository == null)
            {
                this._UserResumeRepository = new GenericRepository<UserResume>(_context);
            }
            return _UserResumeRepository;
        }
        #endregion			
        #region UserSystemRole Repository
        /// <summary>
        /// Provides database access for the UserSystemRole repository functions.
        /// </summary>
        private IGenericRepository<UserSystemRole> _UserSystemRoleRepository;
        public IGenericRepository<UserSystemRole> UserSystemRoleRepository { get { return LazyLoadUserSystemRoleRepository(); } }
        /// <summary>
        /// Lazy load the UserSystemRoleRepository.
        /// </summary>
        /// <returns>UserSystemRoleRepository</returns>
        private IGenericRepository<UserSystemRole> LazyLoadUserSystemRoleRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserSystemRoleRepository == null)
            {
                this._UserSystemRoleRepository = new GenericRepository<UserSystemRole>(_context);
            }
            return _UserSystemRoleRepository;
        }
        #endregion		
        #region UserWebsite Repository
        /// <summary>
        /// Provides database access for the UserWebsite repository functions.
        /// </summary>
        private IGenericRepository<UserWebsite> _UserWebsiteRepository;
        public IGenericRepository<UserWebsite> UserWebsiteRepository { get { return LazyLoadUserWebsiteRepository(); } }
        /// <summary>
        /// Lazy load the UserWebsiteRepository.
        /// </summary>
        /// <returns>UserWebsiteRepository</returns>
        private IGenericRepository<UserWebsite> LazyLoadUserWebsiteRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserWebsiteRepository == null)
            {
                this._UserWebsiteRepository = new GenericRepository<UserWebsite>(_context);
            }
            return _UserWebsiteRepository;
        }
        #endregion		
        #region UserProfile Repository
        /// <summary>
        /// Provides database access for the UserProfile repository functions.
        /// </summary>
        private IGenericRepository<UserProfile> _UserProfileRepository;
        public IGenericRepository<UserProfile> UserProfileRepository { get { return LazyLoadUserProfileRepository(); } }
        /// <summary>
        /// Lazy load the UserProfileRepository.
        /// </summary>
        /// <returns>UserProfileRepository</returns>
        private IGenericRepository<UserProfile> LazyLoadUserProfileRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserProfileRepository == null)
            {
                this._UserProfileRepository = new GenericRepository<UserProfile>(_context);
            }
            return _UserProfileRepository;
        }
        #endregion			
        #region Client Repository
        #region client Repository
        /// <summary>
        /// Provides database access for the UserAlternateContact repository functions.
        /// </summary>
        private IGenericRepository<Client> _ClientRepository;
        public IGenericRepository<Client> ClientRepository { get { return LazyLoadClientRepository(); } }
        /// <summary>
        /// Lazy load the ClientRepository.
        /// </summary>
        /// <returns>Client</returns>
        private IGenericRepository<Client> LazyLoadClientRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ClientRepository == null)
            {
                this._ClientRepository = new GenericRepository<Client>(_context);
            }
            return _ClientRepository;
        }
        #endregion

        #endregion
        #region UserRepository Repository
        /// <summary>
        /// Provides database access for the User repository functions.
        /// </summary>
        private IUserRepository _UserRepository;
        public IUserRepository UserRepository { get { return LazyLoadUserRepository(); } }
        /// <summary>
        /// Lazy load the UserRepository.
        /// </summary>
        /// <returns>UserRepository</returns>
        private IUserRepository LazyLoadUserRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserRepository == null)
            {
                this._UserRepository = new Repository.UserProfileManagement.UserRepository(_context);
            }
            return _UserRepository;
        }
        #endregion
        #region UserAccountRecoveryRepository Repository
        /// <summary>
        /// Provides database access for the UserAccountRecoveryRepository repository functions.
        /// </summary>
        private IGenericRepository<UserAccountRecovery> _UserAccountRecoveryRepository;
        public IGenericRepository<UserAccountRecovery> UserAccountRecoveryRepository { get { return LazyLoadUserAccountRecoveryRepository(); } }
        /// <summary>
        /// Lazy load the UserAccountRecoveryRepository.
        /// </summary>
        /// <returns>UserAccountRecoveryRepository</returns>
        private IGenericRepository<UserAccountRecovery> LazyLoadUserAccountRecoveryRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserAccountRecoveryRepository == null)
            {
                this._UserAccountRecoveryRepository = new GenericRepository<UserAccountRecovery>(_context);
            }
            return _UserAccountRecoveryRepository;
        }
        #endregion
        #region UserAccountStatusChangeLogRepository Repository
            /// <summary>
            /// Provides database access for the UserAccountStatusChangeLog repository functions.
            /// </summary>
            private IUserAccountStatusChangeLogRepository _UserAccountStatusChangeLogRepository;
            public IUserAccountStatusChangeLogRepository UserAccountStatusChangeLogRepository { get { return LazyLoadUserAccountStatusChangeLogRepository(); } }
            /// <summary>
            /// Lazy load the UserAccountStatusChangeLogRepository.
            /// </summary>
            /// <returns>UserAccountStatusChangeLogRepository</returns>
            private IUserAccountStatusChangeLogRepository LazyLoadUserAccountStatusChangeLogRepository()
            {
                if (this._context == null)
                {
                    _context = new P2RMISNETEntities();
                }

                if (this._UserAccountStatusChangeLogRepository == null)
                {
                    this._UserAccountStatusChangeLogRepository = new UserAccountStatusChangeLogRepository(_context);
                }
                return _UserAccountStatusChangeLogRepository;
            }
        #endregion
        #region AccountStatusReason Repository
        /// <summary>
        /// Provides database access for the AccountStatusReasonRepository repository functions.
        /// </summary>
        private IGenericRepository<AccountStatusReason> _AccountStatusReasonRepository;
        public IGenericRepository<AccountStatusReason> AccountStatusReasonRepository { get { return LazyLoadAccountStatusReasonRepository(); } }
        /// <summary>
        /// Lazy load the AccountStatusReasonRepository.
        /// </summary>
        /// <returns>AccountStatusReasonRepository</returns>
        private IGenericRepository<AccountStatusReason> LazyLoadAccountStatusReasonRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._AccountStatusReasonRepository == null)
            {
                this._AccountStatusReasonRepository = new GenericRepository<AccountStatusReason>(_context);
            }
            return _AccountStatusReasonRepository;
        }
        #endregion
        #region UserAccountStatus Repository
        /// <summary>
        /// Provides database access for UserAccountStatusRepository functions.  Placed in method; Properties actually
        /// replace property call with code in the property.
        /// </summary>
        private IGenericRepository<UserAccountStatu> _userAccountStatusRepository;
        public IGenericRepository<UserAccountStatu> UserAccountStatusRepository { get { return LazyLoadUofwUserAccountStatusRepository(); } }
        /// <summary>
        /// Lazy load the UserAccountStatusRepository
        /// </summary>
        /// <returns></returns>
        private IGenericRepository<UserAccountStatu> LazyLoadUofwUserAccountStatusRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._userAccountStatusRepository == null)
            {
                this._userAccountStatusRepository = new GenericRepository<UserAccountStatu>(_context);
            }
            return _userAccountStatusRepository;
        }
        #endregion
        #region ProfileTypeRoleRepository Repository
        /// <summary>
        /// Provides database access for ProfileTypeRoleRepository functions. 
        /// </summary>
        private IGenericRepository<ProfileTypeRole> _ProfileTypeRoleRepository;
        public IGenericRepository<ProfileTypeRole> ProfileTypeRoleRepository { get { return LazyLoadProfileTypeRoleRepository(); } }
        /// <summary>
        /// Lazy load the ProfileTypeRoleRepository
        /// </summary>
        /// <returns>ProfileTypeRolesRepository</returns>
        private IGenericRepository<ProfileTypeRole> LazyLoadProfileTypeRoleRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ProfileTypeRoleRepository == null)
            {
                this._ProfileTypeRoleRepository = new GenericRepository<ProfileTypeRole>(_context);
            }
            return _ProfileTypeRoleRepository;
        }
        #endregion
        #region SystemRoleRepository Repository
        /// <summary>
        /// Provides database access for SystemRoleRepository functions. 
        /// </summary>
        private IGenericRepository<SystemRole> _SystemRoleRepository;
        public IGenericRepository<SystemRole> SystemRoleRepository { get { return LazyLoadSystemRoleRepository(); } }
        /// <summary>
        /// Lazy load the SystemRoleRepository
        /// </summary>
        /// <returns>SystemRolesRepository</returns>
        private IGenericRepository<SystemRole> LazyLoadSystemRoleRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._SystemRoleRepository == null)
            {
                this._SystemRoleRepository = new GenericRepository<SystemRole>(_context);
            }
            return _SystemRoleRepository;
        }
        #endregion
        #region ProfessionalAffiliation Repository
        /// <summary>
        /// Provides database access for ProfessionalAffiliationRepository functions. 
        /// </summary>
        private IGenericRepository<ProfessionalAffiliation> _ProfessionalAffiliationRepository;
        public IGenericRepository<ProfessionalAffiliation> ProfessionalAffiliationRepository { get { return LazyLoadProfessionalAffiliationRepository(); } }
        /// <summary>
        /// Lazy load the ProfessionalAffiliationRepository
        /// </summary>
        /// <returns>ProfessionalAffiliationRepository</returns>
        private IGenericRepository<ProfessionalAffiliation> LazyLoadProfessionalAffiliationRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._ProfessionalAffiliationRepository == null)
            {
                this._ProfessionalAffiliationRepository = new GenericRepository<ProfessionalAffiliation>(_context);
            }
            return _ProfessionalAffiliationRepository;
        }
        #endregion
        #region AcademicRank Repository
        /// <summary>
        /// Provides database access for AcademicRankRepository functions. 
        /// </summary>
        private IGenericRepository<AcademicRank> _AcademicRankRepository;
        public IGenericRepository<AcademicRank> AcademicRankRepository { get { return LazyLoadAcademicRankRepository(); } }
        /// <summary>
        /// Lazy load the AcademicRankRepository
        /// </summary>
        /// <returns>AcademicRankRepository</returns>
        private IGenericRepository<AcademicRank> LazyLoadAcademicRankRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._AcademicRankRepository == null)
            {
                this._AcademicRankRepository = new GenericRepository<AcademicRank>(_context);
            }
            return _AcademicRankRepository;
        }
        #endregion
        #region UserInfoChangeLog Repository
        /// <summary>
        /// Provides database access for UserInfoChangeLogRepository functions. 
        /// </summary>
        private IGenericRepository<UserInfoChangeLog> _UserInfoChangeLogRepository;
        public IGenericRepository<UserInfoChangeLog> UserInfoChangeLogRepository { get { return LazyLoadUserInfoChangeLogRepository(); } }
        /// <summary>
        /// Lazy load the UserInfoChangeLogRepository
        /// </summary>
        /// <returns>UserInfoChangeLogRepository</returns>
        private IGenericRepository<UserInfoChangeLog> LazyLoadUserInfoChangeLogRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserInfoChangeLogRepository == null)
            {
                this._UserInfoChangeLogRepository = new GenericRepository<UserInfoChangeLog>(_context);
            }
            return _UserInfoChangeLogRepository;
        }
        #endregion
        #region UserClientBlock Repository 
        /// <summary>
        /// Provides database access for UserClientBlockRepository functions. 
        /// </summary>
        private IUserClientBlockRepository _UserClientBlockRepository;
        public IUserClientBlockRepository UserClientBlockRepository { get { return LazyLoadUserClientBlockRepository(); } }
        /// <summary>
        /// Lazy load the UserClientBlockRepository
        /// </summary>
        /// <returns>UserClientBlockRepository</returns>
        private IUserClientBlockRepository LazyLoadUserClientBlockRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._UserClientBlockRepository == null)
            {
                this._UserClientBlockRepository = new UserClientBlockRepository(_context);
            }
            return _UserClientBlockRepository;
        }
        #endregion
    }
}
