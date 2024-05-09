namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Model object returned for results of the GetSessionPanelEmailAddress requests.
    /// </summary>
    public class SessionPanelEmailAddressModel
    {
        /// <summary>
        /// The First name of the email address's owner
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The First name of the email's address owner
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The participant type abreviation of the email's address owner
        /// </summary>
        public string ParticipantTypeAbrv { get; set; }
        /// <summary>
        /// The email address
        /// </summary>
        public string Address { get; set; }
    }
}
