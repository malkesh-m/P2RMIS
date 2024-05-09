
namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the GetSessionPanelEmailAddress requests.
    /// </summary>
    public interface ISessionPanelEmailAddressModel
    {
        /// <summary>
        /// The First name of the email address's owner
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// The First name of the email's address owner
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// The participant type abreviation of the email's address owner
        /// </summary>
        string ParticipantTypeAbrv { get; set; }
        /// <summary>
        /// The email address
        /// </summary>
        string Address { get; set; }
    }
}
