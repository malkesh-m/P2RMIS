using System;

namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Model for a user's participation history
    /// </summary>
    public class UserParticipationHistoryModel : IUserParticipationHistoryModel
    {
        /// <summary>
        /// Fiscal year of program
        /// </summary>
        public string FiscalYear { get; set; }
        /// <summary>
        /// Client abbreviated name
        /// </summary>
        public string ClientAbrv { get; set; }
        /// <summary>
        /// Participation type name
        /// </summary>
        public string ParticipantType { get; set; }
        /// <summary>
        /// Participation role name
        /// </summary>
        public string ParticipantRole { get; set; }
        /// <summary>
        /// Abbreviated program name
        /// </summary>
        public string ProgramAbrv { get; set; }
        /// <summary>
        /// Abbreviated panel name
        /// </summary>
        public string PanelAbrv { get; set; }
        /// <summary>
        /// Date panel ends
        /// </summary>
        public DateTime? PanelEndDate { get; set; }
        /// <summary>
        /// Scope of the participation (program or panel)
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// Identifier for a participation
        /// </summary>
        public int ParticipationId { get; set; }
        /// <summary>
        /// Date notification was sent
        /// </summary>
        public DateTime? NotificationSent { get; set; }
        /// <summary>
        /// Date participation was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Program end date
        /// </summary>
        public DateTime? ProgramEndDate { get; set; }
        /// <summary>
        /// Registration start date
        /// </summary>
        public DateTime? RegistrationStartDate { get; set; }
        /// <summary>
        /// Registration Completion date
        /// </summary>
        public DateTime? RegistrationCompletedDate { get; set; }

        /// <summary>
        /// Date the contract was signed for a participation
        /// </summary>
        public DateTime? ContractSignedDate { get; set; }
        /// <summary>
        /// Session panel identifier
        /// </summary>
        public int? SessionPanelId { get; set; }
        /// <summary>
        /// Whether the program is active
        /// </summary>
        public bool IsProgramActive
        {
            get
            {
                return (ProgramEndDate == null) ? true : false;
            }
        }
        /// <summary>
        /// Gets or sets the registration identifier.
        /// </summary>
        /// <value>
        /// The registration identifier.
        /// </value>
        public int RegistrationId { get; set; }
    }
}
