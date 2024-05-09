using System;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    public interface IUserParticipationHistoryModel
    {
        /// <summary>
        /// Fiscal year of program
        /// </summary>
        string FiscalYear { get; set; }

        /// <summary>
        /// Client abbreviated name
        /// </summary>
        string ClientAbrv { get; set; }

        /// <summary>
        /// Participation type name
        /// </summary>
        string ParticipantType { get; set; }

        /// <summary>
        /// Participation role name
        /// </summary>
        string ParticipantRole { get; set; }

        /// <summary>
        /// Abbreviated program name
        /// </summary>
        string ProgramAbrv { get; set; }

        /// <summary>
        /// Abbreviated panel name
        /// </summary>
        string PanelAbrv { get; set; }

        /// <summary>
        /// Date panel ends
        /// </summary>
        DateTime? PanelEndDate { get; set; }

        /// <summary>
        /// Scope of the participation (program or panel)
        /// </summary>
        string Scope { get; set; }

        /// <summary>
        /// Identifer for a participation
        /// </summary>
        int ParticipationId { get; set; }

        /// <summary>
        /// Date notification was sent
        /// </summary>
        DateTime? NotificationSent { get; set; }
        /// <summary>
        /// Date participation was last modified
        /// </summary>
        DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Program end date
        /// </summary>
        DateTime? ProgramEndDate { get; set; }
        /// <summary>
        /// Registration start date
        /// </summary>
        DateTime? RegistrationStartDate { get; set; }
        /// <summary>
        /// Registration Completion date
        /// </summary>
        DateTime? RegistrationCompletedDate { get; set; }
        /// <summary>
        /// Date the contract was signed for a participation
        /// </summary>
        DateTime? ContractSignedDate { get; set; }
        /// <summary>
        /// Session panel identifier
        /// </summary>
        int? SessionPanelId { get; set; }
        /// <summary>
        /// Whether the program is active
        /// </summary>
        bool IsProgramActive { get; }

        /// <summary>
        /// Gets or sets the registration identifier.
        /// </summary>
        /// <value>
        /// The registration identifier.
        /// </value>
        int RegistrationId { get; set; }
    }
}