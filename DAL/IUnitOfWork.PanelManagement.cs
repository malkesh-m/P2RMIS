using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Identifies the access points to the database entities for panel management.
    /// </summary>
    public partial interface IUnitOfWork
    {
        /// <summary>
        /// Provides database access for the PanelManagement repository functions.
        /// </summary>
        IPanelManagementRepository PanelManagementRepository { get; }
        /// <summary>
        /// Provides database access for the ActionLog repository functions.
        /// </summary>
        IActionLogRepository ActionLogRepository { get; }
        /// <summary>
        /// Provides database access for the PanelApplication repository functions.
        /// </summary>
        IPanelApplicationRepository PanelApplicationRepository { get; }
        /// <summary>
        /// Provides database access for the SessionPanel repository functions.
        /// </summary>
        ISessionPanelRepository SessionPanelRepository { get; }
        /// <summary>
        /// Provides database access for the ReviewerEvaluation repository functions.
        /// </summary>
        IReviewerEvaluationRepository ReviewerEvaluationRepository { get; }

        /// <summary>
        /// Provides database access for the PanelApplicationReviewerExpertise repository functions.
        /// </summary>  
        IPanelApplicationReviewerExpertiseRepository PanelApplicationReviewerExpertiseRepository { get; }
        /// <summary>
        /// Provides database access for the PanelUserAssignment repository functions.
        /// </summary>
        IPanelUserAssignmentRepository PanelUserAssignmentRepository { get ; }
        /// <summary>
        /// Provides database access for the PanelApplicationReviewerAssignment repository functions.
        /// </summary>
        IPanelApplicationReviewerAssignmentRepository PanelApplicationReviewerAssignmentRepository { get; }
        /// <summary>
        /// Provides database access for the ClientAssignmentType repository functions.
        /// </summary>
        IClientAssignmentTypeRepository ClientAssignmentTypeRepository { get; }
        /// <summary>
        /// Provides database access for the PanelApplicationReviewerCoiDetail repository functions.
        /// </summary>
        IPanelApplicationReviewerCoiDetailRepository PanelApplicationReviewerCoiDetailRepository { get; }
        /// <summary>
        /// Provides database access for the Email repository functions.
        /// </summary>
        IEmailRepository EmailRepository { get; }
        /// <summary>
        /// Provides database access for the MeetingSession repository functions.
        /// </summary>
        IGenericRepository<MeetingSession> MeetingSessionRepository { get; }
        /// <summary>
        /// Provides database access for the PanelStageStep repository functions.
        /// </summary>
        IGenericRepository<PanelStageStep> PanelStageStepRepository { get; }
        /// <summary>
        /// Provides database access for the PanelApplicationSummary repository functions.
        /// </summary>
        IGenericRepository<PanelApplicationSummary> PanelApplicationSummaryRepository { get; }
        /// <summary>
        /// Provides database access for the ClientExpertiseRating repository functions.
        /// </summary>
        IGenericRepository<ClientExpertiseRating> ClientExpertiseRatingRepository  { get; }
        /// <summary>
        /// Provides database access for the ClientParticipantType repository functions.
        /// </summary>
        IGenericRepository<ClientParticipantType> ClientParticipantTypeRepository { get; }
        /// <summary>
        /// Provides database access for the ClientRole repository functions.
        /// </summary>
        IGenericRepository<ClientRole> ClientRoleRepository { get; }
        /// <summary>
        /// Provides database access for the UserCommunicationLog repository functions.
        /// </summary>
        IGenericRepository<UserCommunicationLog> UserCommunicationLogRepository { get; }
        /// <summary>
        /// Provides database access for the CommunicationMethod repository functions.
        /// </summary>
        IGenericRepository<CommunicationMethod> CommunicationMethodRepository { get; }
        /// <summary>
        /// Provides database access for the ParticipationMethod repository functions.
        /// </summary>
        IGenericRepository<ParticipationMethod> ParticipationMethodRepository { get; }
        /// <summary>
        /// Provides database access for the PanelUserPotentialAssignment repository functions.
        /// </summary>
        IGenericRepository<PanelUserPotentialAssignment> PanelUserPotentialAssignmentRepository { get; }
        /// <summary>
        /// Provides database access for the PanelUserPotentialAssignment repository functions.
        /// </summary>
        IAssignmentTypeThresholdRepository AssignmentTypeThresholdRepository { get; }
        /// <summary>
        /// Gets the program user assignment repository.
        /// </summary>
        /// <value>
        /// The program user assignment repository.
        /// </value>
        IGenericRepository<ProgramUserAssignment> ProgramUserAssignmentRepository { get; }
        /// <summary>
        /// Gets the session user assignment repository.
        /// </summary>
        /// <value>
        /// The session user assignment repository.
        /// </value>
        IGenericRepository<SessionUserAssignment> SessionUserAssignmentRepository { get; }
    }
}
