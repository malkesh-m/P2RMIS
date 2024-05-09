namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Web model for search for reviewers
    /// </summary>
    public class SearchForReviewersModel : ISearchForReviewersModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchForReviewersModel"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="username">The username.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="programId">The program identifier.</param>
        /// <param name="yearId">The year identifier.</param>
        /// <param name="panelName">Name of the panel.</param>
        /// <param name="resume">The resume.</param>
        /// <param name="expertise">The expertise.</param>
        /// <param name="participantTypeId">The participant type identifier.</param>
        /// <param name="participantRoleId">The participant role identifier.</param>
        /// <param name="academicRankId">The academic rank identifier.</param>
        /// <param name="isPotentialChair">if set to <c>true</c> [is potential chair].</param>
        /// <param name="rating">The rating.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="isStateExcluded">if set to <c>true</c> [is state excluded].</param>
        /// <param name="genderId">The gender identifier.</param>
        /// <param name="ethinicityId">The ethinicity identifier.</param>
        public SearchForReviewersModel(string firstName, string lastName, int? userId, 
                string username, string organization, int? programId, 
                int? yearId, string panelName, string resume,
                string expertise, int? participantTypeId, int? participantRoleId,
                int? academicRankId, bool isPotentialChair, string rating, 
                int? stateId, bool isStateExcluded, int? genderId,
                int? ethinicityId, int? programYearId, string sessionPanelAbbreviation)
        {
            EthinicityId = ethinicityId;
            StateId = stateId;
            IsStateExcluded = isStateExcluded;
            GenderId = genderId;
            AcademicRankId = academicRankId;
            ParticipantTypeId = participantTypeId;
            ParticipantRoleId = participantRoleId;
            FirstName = firstName;
            LastName = lastName;
            UserId = userId;
            Username = username;
            Organization = organization;
            ProgramId = programId;
            YearId = yearId;
            PanelName = panelName;
            Resume = resume;
            Expertise = expertise;
            IsPotentialChair = isPotentialChair;
            Rating = rating;
            ProgramYearId = programYearId;
            SessionPanelAbbreviation = sessionPanelAbbreviation;
        }
        /// <summary>
        /// Ethnicity selection identifier
        /// </summary>
        public int? EthinicityId { get; set; }
        /// <summary>
        /// Ethnicity selection identifier
        /// </summary>
        public int? StateId { get; set; }
        /// <summary>
        /// Ethnicity selection identifier
        /// </summary>
        public int? GenderId { get; set; }
        /// <summary>
        /// Gets or sets the academic rank identifier.
        /// </summary>
        /// <value>
        /// The academic rank identifier.
        /// </value>
        public int? AcademicRankId { get; set; }
        /// <summary>
        /// Gets or sets the participant type identifier.
        /// </summary>
        /// <value>
        /// The participant type identifier.
        /// </value>
        public int? ParticipantTypeId { get; set; }
        /// <summary>
        /// Gets or sets the participant role identifier.
        /// </summary>
        /// <value>
        /// The participant role identifier.
        /// </value>
        public int? ParticipantRoleId { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int? UserId { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        public string Organization { get; set; }
        /// <summary>
        /// Gets or sets the resume.
        /// </summary>
        /// <value>
        /// The resume.
        /// </value>
        public string Resume { get; set; }
        /// <summary>
        /// Gets or sets the expertise.
        /// </summary>
        /// <value>
        /// The expertise.
        /// </value>
        public string Expertise { get; set; }
        /// <summary>
        /// Gets or sets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        public string PanelName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is potential chair.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is potential chair; otherwise, <c>false</c>.
        /// </value>
        public bool IsPotentialChair { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is state excluded.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is state excluded; otherwise, <c>false</c>.
        /// </value>
        public bool IsStateExcluded { get; set; }
        /// <summary>
        /// Selected Program entity identifier
        /// </summary>
        public int? ProgramId { get; set; }
        /// <summary>
        /// Selected Program Year entity identifier
        /// </summary>
        public int? YearId { get; set; }
        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public string Rating { get; set; }       
        /// <summary>
        /// Gets or sets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        public int? ProgramYearId { get; set; }
        /// <summary>
        /// Gets or sets the session panel abbreviation.
        /// </summary>
        /// <value>
        /// The session panel abbreviation.
        /// </value>
        public string SessionPanelAbbreviation { get; set; }
        public string FiscalYear { get; set; }
    }
}
