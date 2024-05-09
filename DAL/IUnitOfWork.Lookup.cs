using Sra.P2rmis.Dal.Repository.Setup;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Identifies the access points to the database entities for lookup entity objects.
    /// </summary>
    public partial interface IUnitOfWork
    {
        /// <summary>
        /// Provides database access for the AddressType repository functions.
        /// </summary>
        IGenericRepository<AddressType> AddressTypeRepository { get; }
        /// <summary>
        /// Provides database access for the AssignmentType repository functions.
        /// </summary>
        IGenericRepository<AssignmentType> AssignmentTypeRepository { get; }
        /// <summary>
        /// Provides database access for the Country repository functions.
        /// </summary>
        IGenericRepository<Country> CountryRepository { get; }
        /// <summary>
        /// Provides database access for the Degree repository functions.
        /// </summary>
        IGenericRepository<Degree> DegreeRepository { get; }
        /// <summary>
        /// Provides database access for the Ethnicity repository functions.
        /// </summary>
        IGenericRepository<Ethnicity> EthnicityRepository { get; }
        /// <summary>
        /// Provides database access for the Contract Status repository functions.
        /// </summary>
        IGenericRepository<ContractStatus> ContractStatusRepository { get; }
        /// <summary>
        /// Provides database access for the Gender repository functions.
        /// </summary>
        IGenericRepository<Gender> GenderRepository { get; }
        /// <summary>
        /// Provides database access for the Prefix repository functions.
        /// </summary>
        IGenericRepository<Prefix> PrefixRepository { get; }
        /// <summary>
        /// Provides database access for the State repository functions.
        /// </summary>
        IGenericRepository<State> StateRepository { get; }
        /// <summary>
        /// Provides database access for the PhoneType repository functions.
        /// </summary>
        IGenericRepository<PhoneType> PhoneTypeRepository { get; }
        /// <summary>
        /// Provides database access for the MilitaryRank repository functions.
        /// </summary>
        IGenericRepository<MilitaryRank> MilitaryRankRepository { get; }
        /// <summary>
        /// Provides database access for the ProfileType repository functions.
        /// </summary>
        IGenericRepository<ProfileType> ProfileTypeRepository { get; }
        /// <summary>
        /// Provides database access for the WebsiteType repository functions.
        /// </summary>
        IGenericRepository<WebsiteType> WebsiteTypeRepository { get; }
        /// <summary>
        /// Provides database access for the MilitaryStatusType repository functions.
        /// </summary>
        IGenericRepository<MilitaryStatusType> MilitaryStatusTypeRepository { get; }
        /// <summary>
        /// Provides database access for the EmailAddressType repository functions.
        /// </summary>
        IGenericRepository<EmailAddressType> EmailAddressTypeRepository { get; }
        /// <summary>
        /// Provides database access for the AlternateContactType repository functions.
        /// </summary>
        IGenericRepository<AlternateContactType> AlternateContactTypeRepository { get; }
        /// <summary>
        /// Provides database access for the LookupCommentType repository functions.
        /// </summary>
        IGenericRepository<LookupCommentType> LookupCommentTypeRepository { get; }
        ///// <summary>
        ///// Provides database access for the LookupRole repository functions.
        ///// </summary>
        //IGenericRepository<LookupRole> LookupRoleRepository { get; }
        /// <summary>
        /// Provides database access for the LookupStage repository functions.
        /// </summary>
        //IGenericRepository<LookupStage> LookupStageRepository { get; }
        /// <summary>
        /// Provides database access for the LookupTemplateCategory repository functions.
        /// </summary>
        IGenericRepository<LookupTemplateCategory> LookupTemplateCategoryRepository { get; }
        /// <summary>
        /// Provides database access for the LookupTemplateStage repository functions.
        /// </summary>
        IGenericRepository<LookupTemplateStage> LookupTemplateStageRepository { get; }
        /// <summary>
        /// Provides database access for the LookupTemplateType repository functions.
        /// </summary>
        IGenericRepository<LookupTemplateType> LookupTemplateTypeRepository { get; }
        /// <summary>
        /// Provides database access for the RecoveryQuestion repository functions.
        /// </summary>
        IGenericRepository<RecoveryQuestion> RecoveryQuestionRepository { get; }
        /// <summary>
        /// Gets the travel mode repository.
        /// </summary>
        /// <value>
        /// The travel mode repository.
        /// </value>
        IGenericRepository<TravelMode> TravelModeRepository { get; }
        /// <summary>
        /// Provides database access for the ScoringTemplate repository functions.
        /// </summary>
        IGenericRepository<ScoringTemplate> ScoringTemplate { get; }
        /// <summary>
        /// Provides database access for the ScoringTemplatePhase repository functions.
        /// </summary>
        IScoringTemplatePhaseRepository ScoringTemplatePhaseRepository { get; }
        /// <summary>
        /// Provides database access for the MeetingType repository functions.
        /// </summary>
        IGenericRepository<MeetingType> MeetingTypeRepository { get; }
        /// <summary>
        /// Provides database access for the Hotel repository functions.
        /// </summary>
        IGenericRepository<Hotel> HotelRepository { get; }

        /// <summary>
        /// Provides database access for the Policy Type repository functions.
        /// </summary>
        IGenericRepository<PolicyType> PolicyTypeRepository { get; }

        /// <summary>
        /// Provides database access for the Policy Restriction Type repository functions.
        /// </summary>
        IGenericRepository<PolicyRestrictionType> PolicyRestrictionTypeRepository { get; }

        /// <summary>
        /// Provides database access for the WeekDay  repository functions.
        /// </summary>
        IGenericRepository<WeekDay> WeekDayRepository { get; }

    }
}
