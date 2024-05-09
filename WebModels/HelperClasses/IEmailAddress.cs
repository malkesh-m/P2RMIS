
namespace Sra.P2rmis.WebModels.HelperClasses
{
    /// <summary>
    /// Web model for user emails
    /// </summary>
    public interface IEmailAddress
    {
        /// <summary>
        /// Session panel identifier
        /// </summary>
        int SessionPanelId { get; set; }
        /// <summary>
        /// User's email address
        /// </summary>
        string UserEmailAddress { get; set; }
        /// <summary>
        /// User's first name
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// User's last name
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// PanelUserAssignment identifier
        /// </summary>
        int? PanelUserAssignmentId { get; set; }
        /// <summary>
        /// the client participant type abbreviation.  Primarilary used for filtering the list.
        /// </summary>
        string ParticipantTypeAbbreviation { get; set; }
    }
}
