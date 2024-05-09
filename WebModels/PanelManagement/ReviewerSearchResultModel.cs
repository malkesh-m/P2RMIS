

using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Interface representing results returned from a reviewer search
    /// </summary>
    public interface IReviewerSearchResultModel : IUserSearchResultModel
    {
        /// <summary>
        /// Gets a value indicating whether this instance is program user.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is program user; otherwise, <c>false</c>.
        /// </value>
        bool IsProgramUser { get; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is blocked.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is blocked; otherwise, <c>false</c>.
        /// </value>
        bool IsBlocked { get; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        /// <remarks>
        /// We must use double here which is the SQL Server type of the average function to support LINQ query
        /// </remarks>
        double? Rating { get; }

        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        string Organization { get; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        string Position { get; }

        /// <summary>
        /// Gets or sets the expertise.
        /// </summary>
        /// <value>
        /// The expertise.
        /// </value>
        string Expertise { get; }

        /// <summary>
        /// Gets or sets the academic rank.
        /// </summary>
        /// <value>
        /// The academic rank.
        /// </value>
        string AcademicRank { get; }

        /// <summary>
        /// Gets or sets the military branch.
        /// </summary>
        /// <value>
        /// The military branch.
        /// </value>
        string MilitaryBranch { get; }

        /// <summary>
        /// Gets or sets the military rank.
        /// </summary>
        /// <value>
        /// The military rank.
        /// </value>
        string MilitaryRank { get; }

        /// <summary>
        /// Gets or sets the military status.
        /// </summary>
        /// <value>
        /// The military status.
        /// </value>
        string MilitaryStatus { get; }

        /// <summary>
        /// Gets or sets the ethnicity.
        /// </summary>
        /// <value>
        /// The ethnicity.
        /// </value>
        string Ethnicity { get; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        string Gender { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is potential chair.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is potential chair; otherwise, <c>false</c>.
        /// </value>
        bool IsPotentialChair { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is previously participated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is previously participated; otherwise, <c>false</c>.
        /// </value>
        bool IsPreviouslyParticipated { get; }

        /// <summary>
        /// Gets or sets the user resume identifier.
        /// </summary>
        /// <value>
        /// The user resume identifier.
        /// </value>
        int? UserResumeId { get; }

        /// <summary>
        /// Gets the name of the user resume file.
        /// </summary>
        /// <value>
        /// The name of the user resume file.
        /// </value>
        string UserResumeFileName { get; }

        /// <summary>
        /// Gets or sets the preferred website address.
        /// </summary>
        /// <value>
        /// The preferred website address.
        /// </value>
        string PreferredWebsiteAddress { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has communication log.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has communication log; otherwise, <c>false</c>.
        /// </value>
        bool HasCommunicationLog { get; }

        /// <summary>
        /// Gets a value indicating whether this user is on panel.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is on the panel Potential or Assigned; otherwise, <c>false</c>.
        /// </value>
        bool IsOnPanel { get; }

        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        int? StateId { get; set; }
        /// <summary>
        /// Gets or sets the ethnicity identifier.
        /// </summary>
        /// <value>
        /// The ethnicity identifier.
        /// </value>
        int? EthnicityId { get; set; }
        /// <summary>
        /// Gets or sets the gender identifier.
        /// </summary>
        /// <value>
        /// The gender identifier.
        /// </value>
        int? GenderId { get; set; }
        /// <summary>
        /// Gets or sets the text of the resume.
        /// </summary>
        /// <value>
        /// The resume text.
        /// </value>
        string ResumeText { get; set; }
        /// <summary>
        /// Gets or sets the reviewer's participation list.
        /// </summary>
        /// <value>
        /// The participation details.
        /// </value>
        IEnumerable<ParticipationModel> Participation { get; set; }

        /// <summary>
        /// Gets or sets the academic rank identifier.
        /// </summary>
        /// <value>
        /// The academic rank identifier.
        /// </value>
        int? AcademicRankId { get; set; }
        /// <summary>
        /// Gets or sets the name suffix.
        /// </summary>
        /// <value>
        /// The name suffix
        /// </value>
        string Suffix { get; }
    }

    /// <summary>
    /// Class representing results returned from a reviewer search
    /// </summary>
    /// <seealso cref="Sra.P2rmis.WebModels.PanelManagement.IReviewerSearchResultModel" />
    public class ReviewerSearchResultModel : IReviewerSearchResultModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the user information identifier.
        /// </summary>
        /// <value>
        /// The user information identifier.
        /// </value>
        public int UserInfoId { get; set; }
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
        /// Gets or sets the name suffix.
        /// </summary>
        /// <value>
        /// The name suffix
        /// </value>
        public string Suffix { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is blocked.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is blocked; otherwise, <c>false</c>.
        /// </value>
        public bool IsBlocked { get; set; }
        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public double? Rating { get; set; }
        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        public string Organization { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public string Position { get; set; }
        /// <summary>
        /// Gets or sets the degrees.
        /// </summary>
        /// <value>
        /// The degrees.
        /// </value>
        public List<string> Degrees { get; set; }
        /// <summary>
        /// Gets or sets the expertise.
        /// </summary>
        /// <value>
        /// The expertise.
        /// </value>
        public string Expertise { get; set; }
        /// <summary>
        /// Gets or sets the academic rank.
        /// </summary>
        /// <value>
        /// The academic rank.
        /// </value>
        public string AcademicRank { get; set; }
        /// <summary>
        /// Gets or sets the military branch.
        /// </summary>
        /// <value>
        /// The military branch.
        /// </value>
        public string MilitaryBranch { get; set; }
        /// <summary>
        /// Gets or sets the military rank.
        /// </summary>
        /// <value>
        /// The military rank.
        /// </value>
        public string MilitaryRank { get; set; }
        /// <summary>
        /// Gets or sets the military status.
        /// </summary>
        /// <value>
        /// The military status.
        /// </value>
        public string MilitaryStatus { get; set; }
        /// <summary>
        /// Gets or sets the ethnicity.
        /// </summary>
        /// <value>
        /// The ethnicity.
        /// </value>
        public string Ethnicity { get; set; }
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is potential chair.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is potential chair; otherwise, <c>false</c>.
        /// </value>
        public bool IsPotentialChair { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is previously participated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is previously participated; otherwise, <c>false</c>.
        /// </value>
        public bool IsPreviouslyParticipated { get; set; }
        /// <summary>
        /// Gets or sets the user resume identifier.
        /// </summary>
        /// <value>
        /// The user resume identifier.
        /// </value>
        public int? UserResumeId { get; set; }
        /// <summary>
        /// Gets the name of the user resume file.
        /// </summary>
        /// <value>
        /// The name of the user resume file.
        /// </value>
        public string UserResumeFileName { get; set; }
        /// <summary>
        /// Gets or sets the preferred website address.
        /// </summary>
        /// <value>
        /// The preferred website address.
        /// </value>
        public string PreferredWebsiteAddress { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has communication log.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has communication log; otherwise, <c>false</c>.
        /// </value>
        public bool HasCommunicationLog { get; set; }
        /// <summary>
        /// Gets a value indicating whether this user is on panel.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is on the panel Potential or Assigned; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnPanel { get; set; }
        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>
        /// The state identifier.
        /// </value>
        public int? StateId { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the ethnicity identifier.
        /// </summary>
        /// <value>
        /// The ethnicity identifier.
        /// </value>
        public int? EthnicityId { get; set; }
        /// <summary>
        /// Gets or sets the gender identifier.
        /// </summary>
        /// <value>
        /// The gender identifier.
        /// </value>
        public int? GenderId { get; set; }
        /// <summary>
        /// Gets or sets the resume text.
        /// </summary>
        /// <value>
        /// The resume text.
        /// </value>
        public string ResumeText { get; set; }
        /// <summary>
        /// Gets or sets the participation.
        /// </summary>
        /// <value>
        /// The participation.
        /// </value>
        public IEnumerable<ParticipationModel> Participation { get; set; }
        /// <summary>
        /// Gets or sets the academic rank identifier.
        /// </summary>
        /// <value>
        /// The academic rank identifier.
        /// </value>
        public int? AcademicRankId { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance is program user.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is program user; otherwise, <c>false</c>.
        /// </value>
        public bool IsProgramUser { get; set; }
    }
    #region Nested Types
    /// <summary>
    /// Class representing a users participation history
    /// </summary>
    public class ParticipationModel
    {
        /// <summary>
        /// Gets or sets the name of the panel.
        /// </summary>
        /// <value>
        /// The name of the panel.
        /// </value>
        public string PanelName { get; set; }

        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the client program identifier.
        /// </summary>
        /// <value>
        /// The client program identifier.
        /// </value>
        public int ClientProgramId { get; set; }
        /// <summary>
        /// Gets or sets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        public int ProgramYearId { get; set; }

        /// <summary>
        /// Gets or sets the client role identifier.
        /// </summary>
        /// <value>
        /// The client role identifier.
        /// </value>
        public int? ClientRoleId { get; set; }

        /// <summary>
        /// Gets or sets the client participant type identifier.
        /// </summary>
        /// <value>
        /// The client participant type identifier.
        /// </value>
        public int ClientParticipantTypeId { get; set; }
    }
    #endregion
}

