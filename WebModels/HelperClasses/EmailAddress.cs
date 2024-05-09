namespace Sra.P2rmis.WebModels.HelperClasses
{
    /// <summary>
    /// Web model for user emails
    /// </summary>
    public class EmailAddress: IEmailAddress
    {
        #region Constructor
        /// <summary>
        /// Constructor for EmailAddress
        /// </summary>
        public EmailAddress() { }
        /// <summary>
        /// Constructor for EmailAddress
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="userEmailAddress">Email Address</param>
        /// <param name="participantTypeAbbreviation">Participant type Abreviation</param>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        public EmailAddress(string firstName, string lastName, string userEmailAddress, string participantTypeAbbreviation, int sessionPanelId, int? panelUserAssignmentId)
        {
            FirstName = firstName;
            LastName = lastName;
            UserEmailAddress = userEmailAddress;
            ParticipantTypeAbbreviation = participantTypeAbbreviation;
            SessionPanelId = sessionPanelId;
            PanelUserAssignmentId = panelUserAssignmentId;
        }

        #endregion
        /// <summary>
        /// Session panel identifier
        /// </summary>
        public int SessionPanelId { get; set; }
        /// <summary>
        /// User's email address
        /// </summary>
        public string UserEmailAddress { get; set; }
        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// PanelUserAssignment identifier
        /// </summary>
        public int? PanelUserAssignmentId { get; set; }
        /// <summary>
        /// the client participant type abbreviation.  Primarily used for filtering the list.
        /// </summary>
        public string ParticipantTypeAbbreviation { get; set; }
    }
}
