using Sra.P2rmis.Dal.Repository.Setup;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Identifies the access points to the database entities for Setup objects.
    /// </summary>
    public partial interface IUnitOfWork
    {
        /// <summary>
        /// Provides database access for the MechanismTemplateElement repository functions.
        /// </summary>
        IMechanismTemplateElementRepository MechanismTemplateElementRepository { get; }
        /// <summary>
        /// Gets the mechanism scoring template repository.
        /// </summary>
        /// <value>
        /// The mechanism scoring template repository.
        /// </value>
        IMechanismScoringTemplateRepository MechanismScoringTemplateRepository { get; }
        /// <summary>
        /// Provides database access for the ClientElement repository functions.
        /// </summary>
        IGenericRepository<ClientElement> ClientElementRepository { get; }
        /// <summary>
        /// Provides database access for the MechanismTemplate repository functions.
        /// </summary>
        IGenericRepository<MechanismTemplate> MechanismTemplateRepository { get; }
        /// <summary>
        /// Provides database access for the ClientMeeting repository functions.
        /// </summary>
        IGenericRepository<ClientMeeting> ClientMeetingRepository { get; }
        /// <summary>
        /// Provides database access for the StepType repository functions.
        /// </summary>
        IGenericRepository<StepType> StepTypeRepository { get; }
        /// <summary>
        /// Gets the program meeting repository.
        /// </summary>
        /// <value>
        /// The program meeting repository.
        /// </value>
        IGenericRepository<ProgramMeeting> ProgramMeetingRepository { get; }
        /// <summary>
        /// Provides database access for the ProgramSessionPayRate repository functions.
        /// </summary>
        IProgramSessionPayRateRepository ProgramSessionPayRateRepository { get; }
        /// <summary>
        /// Gets the client award type repository.
        /// </summary>
        /// <value>
        /// The client award type repository.
        /// </value>
        IGenericRepository<ClientAwardType> ClientAwardTypeRepository { get; }
        /// <summary>
        /// Gets the employment category repository.
        /// </summary>
        /// <value>
        /// The employment category repository.
        /// </value>
        IGenericRepository<EmploymentCategory> EmploymentCategoryRepository { get; }
        /// <summary>
        /// Gets the mechanism template element scoring repository.
        /// </summary>
        /// <value>
        /// The mechanism template element scoring repository.
        /// </value>
        IMechanismTemplateElementScoringRepository MechanismTemplateElementScoringRepository { get; }

        /// <summary>
        /// Gets the program cycle deliverable repository.
        /// </summary>
        /// <value>
        /// The program cycle deliverable repository.
        /// </value>
        IGenericRepository<ProgramCycleDeliverable> ProgramCycleDeliverableRepository { get; }

        /// <summary>
        /// Provides database access for client data deliverable functions.
        /// </summary>
        IGenericRepository<ClientDataDeliverable> ClientDataDeliverableRepository { get; }

        /// <summary>
        /// Provides database access for custom deliverable repository methods.
        /// </summary>
        IDeliverableRepository DeliverableRepository { get; }
        /// <summary>
        /// Gets the import log repository.
        /// </summary>
        /// <value>
        /// The import log repository.
        /// </value>
        IImportLogRepository ImportLogRepository { get; }
        /// <summary>
        /// Gets the import log item repository.
        /// </summary>
        /// <value>
        /// The import log item repository.
        /// </value>
        IImportLogItemRepository ImportLogItemRepository { get; }
        /// <summary>
        /// Gets the program mechanism import log repository.
        /// </summary>
        /// <value>
        /// The program mechanism import log repository.
        /// </value>
        IProgramMechanismImportLogRepository ProgramMechanismImportLogRepository { get; }
        /// <summary>
        /// Gets the application information repository.
        /// </summary>
        /// <value>
        /// The application information repository.
        /// </value>
        IApplicationInfoRepository ApplicationInfoRepository { get; }
        /// <summary>
        /// Gets the application compliance repository.
        /// </summary>
        /// <value>
        /// The application compliance repository.
        /// </value>
        IApplicationComplianceRepository ApplicationComplianceRepository { get; }
        /// <summary>
        /// Gets the client application information repostiory.
        /// </summary>
        /// <value>
        /// The client application information repostiory.
        /// </value>
        IClientApplicationInfoTypeRepository ClientApplicationInfoTypeRepository { get; }
        /// <summary>
        /// Gets the compliance status repository.
        /// </summary>
        /// <value>
        /// The compliance status repository.
        /// </value>
        IComplianceStatusRepository ComplianceStatusRepository { get; }
        /// <summary>
        /// Gets the client application personnel repository.
        /// </summary>
        /// <value>
        /// The client application personnel repository.
        /// </value>
        IClientApplicationPersonnelTypeRepository ClientApplicationPersonnelTypeRepository { get; }
        /// <summary>
        /// Gets the application personnel repository.
        /// </summary>
        /// <value>
        /// The application personnel repository.
        /// </value>
        IApplicationPersonnelRepository ApplicationPersonnelRepository { get; }
        /// <summary>
        /// Gets the airport repository.
        /// </summary>
        IAirportRepository AirportRepository { get; }
        /// <summary>
        /// Gets the carrier repository.
        /// </summary>
        ICarrierRepository CarrierRepository { get; }
        /// <summary>
        /// Provides database access for the ClientSummaryTemplate repository functions.
        /// </summary>
        IGenericRepository<ClientSummaryTemplate> ClientSummaryTemplateRepository { get; }
        /// <summary>
        /// Provides database access for the ProgramMechanismSummaryStatement repository functions.
        /// </summary>
        IGenericRepository<ProgramMechanismSummaryStatement> ProgramMechanismSummaryStatementRepository { get; }
        /// <summary>
        /// Provides database access for the SummaryReviewerDescription repository functions.
        /// </summary>
        IGenericRepository<SummaryReviewerDescription> SummaryReviewerDescriptionRepository { get; }
    }
}
