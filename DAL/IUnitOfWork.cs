using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.Repository.Setup;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Identifies the access points to the database entities.
    /// </summary>
    public partial interface IUnitOfWork
    {
        /// <summary>
        /// Provides database access for the ProgramFY repository functions.
        /// </summary>
        ProgramFYRepository ProgramRepository { get; }
        /// <summary>
        /// Provides database access for the SessionDetail repository functions.
        /// </summary>
        ISessionDetailRepository SessionDetailRepository { get; }
        /// <summary>
        /// Provides database access for the SystemTemplate  repository functions.
        /// </summary>
        SystemTemplateRepository UofwSysTemplateRepository { get; }
        /// <summary>
        /// Provides database access for the User repository functions.
        /// </summary>
        UserRepository UofwUserRepository { get; }
        /// <summary>
        /// Report repository providing access to the database for the report functions.
        /// </summary>
        IReportRepository ReportRepository { get; }
        /// <summary>
        /// Report viewer repository providing access to the database for the report functions.
        /// </summary>
        IReportViewerRepository ReportViewerRepository { get; }

        /// <summary>
        /// Provides database access for the UserClient repository functions.
        /// </summary>
        IUserClientRepository UserClientRepository { get; }
        /// <summary>
        /// Provides database access for the WorkflowTemplate repository functions.
        /// </summary>
        IWorkflowTemplateRepository WorkflowTemplateRepository { get; }
        /// <summary>
        /// TODO:  I believe this can be deleted
        /// </summary>
        IWorkflowInstanceRepository WorkflowInstanceRepository { get; }
        /// <summary>
        /// Provides database access for the Summary Management repository functions.
        /// </summary>
        ISummaryManagementRepository SummaryManagementRepository { get; }
        /// <summary>
        /// Provides database access for the Application repository functions.
        /// </summary>
        IApplicationRepository ApplicationRepository { get; }
        /// <summary>
        /// Repository for ApplicationReviewStatus objects.  Provides CRUD methods and 
        /// associated database services.
        /// </summary>
        IApplicationReviewStatusRepository ApplicationReviewStatusRepository { get; }
        /// <summary>
        /// Provides database access for the ApplicationWorkflow repository functions.
        /// </summary>
        IApplicationWorkflowRepository ApplicationWorkflowRepository { get; }
        /// <summary>
        /// Provides database access for the ApplicationWorkflow repository functions.
        /// </summary>
        IApplicationWorkflowStepRepository ApplicationWorkflowStepRepository { get; }
        /// <summary>
        /// Provides database access for the ApplicationWorkflow repository functions.
        /// </summary>
        IApplicationWorkflowStepWorkLogRepository ApplicationWorkflowStepWorkLogRepository { get; }
        /// <summary>
        /// Provides database access for the ApplicationWorkflowStepElementContent repository functions.
        /// </summary>
        IApplicationWorkflowStepElementContentRepository ApplicationWorkflowStepElementContentRepository { get; }
        /// <summary>
        /// Provides database access for the ApplicationWorkflowStepElementContentHistory repository functions.
        /// </summary>
        IApplicationWorkflowStepElementContentHistoryRepository ApplicationWorkflowStepElementContentHistoryRepository { get; }
        /// <summary>
        /// Provides database access for the User Application Comment repository functions.
        /// </summary>
        IUserApplicationCommentRepository UserApplicationCommentRepository { get; }
        /// <summary>
        /// Provides database access for the ApplicationDefaultWorkflow repository functions.
        /// </summary>
        IApplicationDefaultWorkflowRepository ApplicationDefaultWorkflowRepository { get; }
        /// <summary>
        /// Provides database access for the ApplicationWorkflowStepAssignment repository functions.
        /// </summary>
        IApplicationWorkflowStepAssignmentRepository ApplicationWorkflowStepAssignmentRepository { get; }
        /// <summary>
        /// Provides database access for the WorkflowMechanismR repository functions.
        /// </summary>
        IWorkflowMechanismRepository WorkflowMechanismRepository { get; }
        /// <summary>
        /// Provides database access for the ApplicationSummaryLog repository functions.
        /// </summary>
        IApplicationSummaryLogRepository ApplicationSummaryLogRepository { get; }
        /// <summary>
        /// Provides database access for the ApplicationWorkflowStepElement repository functions.
        /// </summary>
        IApplicationWorkflowStepElementRepository ApplicationWorkflowStepElementRepository { get; }
        /// <summary>
        /// Provides database access for ViewPanelDetailsRepository functions.
        /// </summary>
        ViewPanelDetailsRepository ViewPanelDetailsRepository { get; }
        /// <summary>
        /// Gets the notification repository.
        /// </summary>
        /// <value>
        /// The notification repository.
        /// </value>
        INotificationRepository NotificationRepository { get; }
        /// <summary>
        /// Provides database access for the ProgramYear repository functions.
        /// </summary>
        IProgramYearRepository ProgramYearRepository { get; }
        /// <summary>
        /// Provides database access for the UserTrainingDocument repository functions.
        /// </summary>
        IApplicationBudgetRepository ApplicationBudgetRepository { get; }
        /// <summary>
        /// Gets the application workflow summary statement repository.
        /// </summary>
        /// <value>
        /// The application workflow summary statement repository.
        /// </value>
        IGenericRepository<ApplicationWorkflowSummaryStatement> ApplicationWorkflowSummaryStatementRepository { get; }

        // CommentRepository
        /// <summary>
        /// CRUD method to save any changes.
        /// </summary>
        void Save();
        /// <summary>
        /// Detach all entities.
        /// </summary>
        void DetachAllEntities();
        /// <summary>
        /// Checks if the entity has been modified.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">The specific entity under test</param>
        /// <returns>True if entity has been modified; false otherwise</returns>
        bool HasEntityBeenModified<T>(T entity) where T : class;
        /// <summary>
        /// Provides database access to ClientProgram entities.
        /// </summary>
        IClientProgramRepository ClientProgramRepository { get; }
        /// <summary>
        /// Provides database access for the ProgramMechanism repository functions.
        /// </summary
        IProgramMechanismRepository ProgramMechanismRepository { get; }
        /// <summary>
        /// Gets the client registration document repository.
        /// </summary>
        /// <value>
        /// The client registration document repository.
        /// </value>
        IClientRegistrationDocumentRepository ClientRegistrationDocumentRepository { get; }
        /// <summary>
        /// Gets the referral mapping repository.
        /// </summary>
        /// <value>
        /// The referral mapping repository.
        /// </value>
        IReferralMappingRepository ReferralMappingRepository { get; }
        /// <summary>
        /// Gets the referral mapping data repository.
        /// </summary>
        /// <value>
        /// The referral mapping data repository.
        /// </value>
        IReferralMappingDataRepository ReferralMappingDataRepository { get; }
        /// <summary>
        /// Gets the workflow repository.
        /// </summary>
        IWorkflowRepository WorkflowRepository { get; }
        /// <summary>
        /// Sets the entity deleted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId">The user identifier.</param>
        /// <param name="entity">The entity.</param>
        void SetEntityDeleted<T>(int userId, T entity) where T : class;
    }
}
