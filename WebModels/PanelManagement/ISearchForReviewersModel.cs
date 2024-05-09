namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Interface for search for reviewers
    /// </summary>
    public interface ISearchForReviewersModel
    {
        /// <summary>
        /// Ethnicity selection identifier
        /// </summary>
        int? EthinicityId { get; set; }
        /// <summary>
        /// Ethnicity selection identifier
        /// </summary>
        int? StateId { get; set; }
        /// <summary>
        /// Ethnicity selection identifier
        /// </summary>
        int? GenderId { get; set; }
        /// <summary>
        /// Gets or sets the academic rank identifier.
        /// </summary>
        /// <value>
        /// The academic rank identifier.
        /// </value>
        int? AcademicRankId { get; set; }
        /// <summary>
        /// Gets or sets the participant type identifier.
        /// </summary>
        /// <value>
        /// The participant type identifier.
        /// </value>
        int? ParticipantTypeId { get; set; }
        /// <summary>
        /// Gets or sets the participant role identifier.
        /// </summary>
        /// <value>
        /// The participant role identifier.
        /// </value>
        int? ParticipantRoleId { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        string LastName { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        int? UserId { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        string Username { get; set; }
        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        string Organization { get; set; }
        /// <summary>
        /// Gets or sets the resume.
        /// </summary>
        /// <value>
        /// The resume.
        /// </value>
        string Resume { get; set; }
        /// <summary>
        /// Gets or sets the expertise.
        /// </summary>
        /// <value>
        /// The expertise.
        /// </value>
        string Expertise { get; set; }
        /// <summary>
        /// Gets or sets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        string PanelName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is potential chair.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is potential chair; otherwise, <c>false</c>.
        /// </value>
        bool IsPotentialChair { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is state excluded.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is state excluded; otherwise, <c>false</c>.
        /// </value>
        bool IsStateExcluded { get; set; }
        /// <summary>
        /// Selected Program entity identifier
        /// </summary>
        int? ProgramId { get; set; }
        /// <summary>
        /// Selected Program Year entity identifier
        /// </summary>
        int? YearId { get; set; }
        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        string Rating { get; set; }
        /// <summary>
        /// Gets or sets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        int? ProgramYearId { get; set; }
        string SessionPanelAbbreviation { get; set; }
    }
}
