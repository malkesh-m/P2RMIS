using Sra.P2rmis.Dal.Repository.UserProfileManagement;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Identifies the access points to the database entities for user panel management.
    /// </summary>
    public partial interface IUnitOfWork
    {
        /// <summary>
        /// Provides database access for the UserAddress repository functions.
        /// </summary>
        IGenericRepository<UserAddress> UserAddressRepository { get; }
        /// <summary>
        /// Provides database access for the UserAlernateContactPhone repository functions.
        /// </summary>
        IGenericRepository<UserAlternateContactPhone> UserAlternateContactPhoneRepository { get; }
        /// <summary>
        /// Provides database access for the UserAlternateContact repository functions.
        /// </summary>
        IGenericRepository<UserAlternateContact> UserAlternateContactRepository { get; }
        /// <summary>
        /// Provides database access for the UserDegree repository functions.
        /// </summary>
        IGenericRepository<UserDegree> UserDegreeRepository { get; }
        /// <summary>
        /// Provides database access for the UserPassword repository functions.
        /// </summary>
        IGenericRepository<UserPassword> UserPasswordRepository { get; }
        /// <summary>
        /// Provides database access for the UserEmail repository functions.
        /// </summary>
        IGenericRepository<UserEmail> UserEmailRepository { get; }
        /// <summary>
        /// Provides database access for the UserInfo repository functions.
        /// </summary>
        IGenericRepository<UserInfo> UserInfoRepository { get; }
        /// <summary>
        /// Provides database access for the UserPhone repository functions.
        /// </summary>
        IGenericRepository<UserPhone> UserPhoneRepository { get; }
        /// <summary>
        /// Provides database access for the UserResume repository functions.
        /// </summary>
        IGenericRepository<UserResume> UserResumeRepository { get; }
        /// <summary>
        /// Provides database access for the UserSystemRole repository functions.
        /// </summary>
        IGenericRepository<UserSystemRole> UserSystemRoleRepository { get; }
        /// <summary>
        /// Provides database access for the UserWebsite repository functions.
        /// </summary>
        IGenericRepository<UserWebsite> UserWebsiteRepository { get; }
        /// <summary>
        /// Provides database access for the UserProfile repository functions.
        /// </summary>
        IGenericRepository<UserProfile> UserProfileRepository { get; }
        /// <summary>
        /// Provides database access for the Client repository functions,
        /// </summary>
        IGenericRepository<Client> ClientRepository { get; }
        /// <summary>
        /// Provides database access for the UserProfileManagement repository functions.
        /// </summary>
        IUserRepository UserRepository { get; }
        /// <summary>
        /// Provides database access for the UserAccountStatu repository functions.
        /// </summary>
        IGenericRepository<UserAccountStatu> UserAccountStatusRepository { get; } 
        /// <summary>
        /// Provides database access for the UserAccountRecoveryRepository repository functions.
        /// </summary>
        IGenericRepository<UserAccountRecovery> UserAccountRecoveryRepository { get; }
        /// <summary>
        /// Provides database access for the UserAccountStatusChangeLog repository functions.
        /// </summary>
        IUserAccountStatusChangeLogRepository UserAccountStatusChangeLogRepository { get; }
        /// <summary>
        /// Provides database access for the AccountStatusReasonRepository repository functions.
        /// </summary>
        IGenericRepository<AccountStatusReason> AccountStatusReasonRepository { get; }
        /// <summary>
        /// Provides database access for ProfileTypeRoleRepository functions. 
        /// </summary>
        IGenericRepository<ProfileTypeRole> ProfileTypeRoleRepository { get; }
        /// <summary>
        /// Provides database access for SystemRoleRepository functions. 
        /// </summary>
        IGenericRepository<SystemRole> SystemRoleRepository { get; }
        /// <summary>
        /// Provides database access for ProfessionalAffiliationRepository functions. 
        /// </summary>
        IGenericRepository<ProfessionalAffiliation> ProfessionalAffiliationRepository { get; }
        /// <summary>
        /// Provides database access for AcademicRankRepository functions. 
        /// </summary>
        IGenericRepository<AcademicRank> AcademicRankRepository { get; }
        /// <summary>
        /// Provides database access for UserInfoChangeLogRepository functions. 
        /// </summary>
        IGenericRepository<UserInfoChangeLog> UserInfoChangeLogRepository { get; }
        /// <summary>
        /// Gets the user client block repository.
        /// </summary>
        /// <value>
        /// The user client block repository.
        /// </value>
        IUserClientBlockRepository UserClientBlockRepository { get; }
        /// <summary>
        /// Provides database access for PolicyHistory functions. 
        /// </summary>
    }
}
