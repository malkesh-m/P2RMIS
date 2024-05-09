using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for panel reviewers
    /// </summary>
    /// <seealso cref="Sra.P2rmis.WebModels.PanelManagement.IPanelReviewerModel" />
    public class PanelReviewerModel : IPanelReviewerModel
    {
        #region Constructor & SetUp
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="userInfoId">UserInfo entity identifier</param>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="typeName">Type name of Reviewer entity.</param>
        /// <param name="participationTypeId">ParticipationType entity identifier of the meeting</param>
        /// <param name="inPersonParticipationId">OnSite Participation entity identifier</param>
        public PanelReviewerModel(int userId, int userInfoId, int panelUserAssignmentId, string typeName, int participationTypeId, int inPersonParticipationId)
        {
            this.UserId = userId;
            this.UserInfoId = userInfoId;
            this.PanelUserAssignmentId = panelUserAssignmentId;
            this.TypeName = typeName;
            //
            // these non-reviewer properties are necessary to support UI 
            // functionality in the modal\
            //
            this.ParticipationTypeId = participationTypeId;
            this.InPersonParticipationId = inPersonParticipationId;
        }
        /// <summary>
        /// Initialize the reviewer identity fields.
        /// </summary>
        /// <param name="firstName">Reviewer's first name</param>
        /// <param name="lastName">Reviewer's last name</param>
        /// <param name="suffix">Reviewer's suffix</param>
        /// <param name="gender">Reviewer's gender</param>
        /// <param name="ethnicity">Reviewer's ethnicity</param>
        /// <param name="preferredWebsiteAddress">Reviewer's preferred websit address</param>
        /// <param name="organization">Reviewer's organization</param>
        /// <param name="position">Reviewer's position</param>
        /// <param name="hasPreferredEmailAddress">Does the reviewer have a preferred email address</param>
        public void SetReviewerInformation(string firstName, string lastName, string suffix, string gender, string ethnicity, string preferredWebsiteAddress, string organization, string position, bool hasPreferredEmailAddress)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Suffix = suffix;
            this.Gender = gender;
            this.Ethnicity = ethnicity;
            this.PreferredWebsiteAddress = preferredWebsiteAddress;
            this.Organization = organization;
            this.Position = position;
            this.HasPreferredEmailAddress = hasPreferredEmailAddress;
        }
        /// <summary>
        /// Initialize the reviewer identity fields.
        /// </summary>
        /// <param name="branch">Military branch</param>
        /// <param name="rank">Military rank</param>
        /// <param name="status">Military status</param>
        public void SetMilitary(string branch, string rank, string status)
        {
            this.MilitaryBranch = branch;
            this.MilitaryRank = rank;
            this.MilitaryStatus = status;
        }
        /// <summary>
        /// Initializes the Participant fields.
        /// </summary>
        /// /// <param name="level"></param>
        /// <param name="method"></param>
        /// <param name="role"></param>
        /// <param name="type"></param>
        /// <param name="roleAbbreviation">Role abbreviation</param>
        /// <param name="isClientApproved">Flag if client has approved</param>
        public void SetParticant(string level, string method, string role, string roleAbbreviation, string type, bool? isClientApproved)
        {
            this.ParticipantLevel = level;
            this.ParticipantMethod = method;
            this.ParticipantRole = role;
            this.ParticipantRoleAbbreviation = roleAbbreviation;
            this.ParticipantType = type;
            this.IsClientApproved = isClientApproved;
        }
        /// <summary>
        /// Initializes the academic information
        /// </summary>
        /// <param name="userResumeId">UserResume entity identifier</param>
        /// <param name="academicRank">Academic rank</param>
        /// <param name="expertise">Expertise</param>
        /// <param name="rating">Reviewer rating</param>
        /// <param name="degrees">Enumeration of the Reviewers degrees.</param>
        public void setAcademicInformation(int? userResumeId, string academicRank, string expertise, decimal? rating, IEnumerable<string> degrees)
        {
            this.UserResumeId = userResumeId;
            this.AcademicRank = academicRank;
            this.Expertise = expertise;
            this.Rating = rating;
            this.Degrees = degrees.Distinct().ToList<string>();
        }
        /// <summary>
        /// Initializes the "Is" type properties.
        /// </summary>
        /// <param name="isBlocked">Indicates if the reviewer is "blocked"</param>
        /// <param name="isPotentialChair">Indicates if the reviewer is "a potential chair"</param>
        /// <param name="isPreviouslyParticipated">Indicates if the reviewer had "previously participated" on a panel</param>
        /// 
        public void SetIs(bool isBlocked, bool isPotentialChair, bool isPreviouslyParticipated)
        {
            this.IsBlocked = isBlocked;
            this.IsPotentialChair = isPotentialChair;
            this.IsPreviouslyParticipated = isPreviouslyParticipated;
        }
        #endregion
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; private set; }
        /// <summary>
        /// Gets or sets the user information identifier.
        /// </summary>
        /// <value>
        /// The user information identifier.
        /// </value>
        public int UserInfoId { get; private set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; private set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; private set; }
        /// <summary>
        /// Gets or sets the name suffix.
        /// </summary>
        /// <value>
        /// The name suffix.
        /// </value>
        public string Suffix { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is blocked.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is blocked; otherwise, <c>false</c>.
        /// </value>
        public bool IsBlocked { get; private set; }
        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public decimal? Rating { get; private set; }
        /// <summary>
        /// Gets or sets the type of the participant.
        /// </summary>
        /// <value>
        /// The type of the participant.
        /// </value>
        public string ParticipantType { get; private set; }
        /// <summary>
        /// Gets or sets the participant method.
        /// </summary>
        /// <value>
        /// The participant method.
        /// </value>
        public string ParticipantMethod { get; private set; }
        /// <summary>
        /// Gets or sets the participant level.
        /// </summary>
        /// <value>
        /// The participant level.
        /// </value>
        public string ParticipantLevel { get; private set; }
        /// <summary>
        /// Gets or sets the participant role.
        /// </summary>
        /// <value>
        /// The participant role.
        /// </value>
        public string ParticipantRole { get; private set; }
        /// <summary>
        /// Gets or sets the participant role abbreviation.
        /// </summary>
        /// <value>
        /// The participant role abbreviation.
        /// </value>
        public string ParticipantRoleAbbreviation { get; private set; }
        /// <summary>
        /// Gets or sets the panel user assignment status.
        /// </summary>
        /// <value>
        /// The panel user assignment status.
        /// </value>
        public string PanelUserAssignmentStatus { get; private set; }
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int PanelUserAssignmentId { get; private set; }
        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
 
        public string Organization { get; private set; }
        /// <summary>
        /// Get or set whether the user has a preferred email address
        /// </summary>
        /// <value>
        /// true if the user has one preferred email address; false otherwise
        /// </value>
        public bool HasPreferredEmailAddress { get; private set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public string Position { get; private set; }
        /// <summary>
        /// Gets or sets the degrees.
        /// </summary>
        /// <value>
        /// The degrees.
        /// </value>
        public List<string> Degrees { get; private set; }
        /// <summary>
        /// Gets or sets the expertise.
        /// </summary>
        /// <value>
        /// The expertise.
        /// </value>
        public string Expertise { get; private set; }
        /// <summary>
        /// Gets or sets the academic rank.
        /// </summary>
        /// <value>
        /// The academic rank.
        /// </value>
        public string AcademicRank { get; private set; }
        /// <summary>
        /// Gets or sets the military branch.
        /// </summary>
        /// <value>
        /// The military branch.
        /// </value>
        public string MilitaryBranch { get; private set; }
        /// <summary>
        /// Gets or sets the military rank.
        /// </summary>
        /// <value>
        /// The military rank.
        /// </value>
        public string MilitaryRank { get; private set; }
        /// <summary>
        /// Gets or sets the military status.
        /// </summary>
        /// <value>
        /// The military status.
        /// </value>
        public string MilitaryStatus { get; private set; }
        /// <summary>
        /// Gets or sets the ethnicity.
        /// </summary>
        /// <value>
        /// The ethnicity.
        /// </value>
        public string Ethnicity { get; private set; }
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is potential chair.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is potential chair; otherwise, <c>false</c>.
        /// </value>
        public bool IsPotentialChair { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is previously participated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is previously participated; otherwise, <c>false</c>.
        /// </value>
        public bool IsPreviouslyParticipated { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is client approved.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is client approved; otherwise, <c>false</c>.
        /// </value>
        public bool? IsClientApproved { get; private set; }
        /// <summary>
        /// Gets or sets the user resume identifier.
        /// </summary>
        /// <value>
        /// The user resume identifier.
        /// </value>
        public int? UserResumeId { get; private set; }
        /// <summary>
        /// Gets the name of the user resume file.
        /// </summary>
        /// <value>
        /// The name of the user resume file.
        /// </value>
        public string UserResumeFileName { get; private set; }
        /// <summary>
        /// Gets or sets the preferred website address.
        /// </summary>
        /// <value>
        /// The preferred website address.
        /// </value>
        public string PreferredWebsiteAddress { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has communication log.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has communication log; otherwise, <c>false</c>.
        /// </value>
        public bool HasCommunicationLog { get; private set; }
        /// <summary>
        /// TypeName of source entity object 
        /// </summary>
        public string TypeName { get; private set; }

        #region Meeting attribues
        /// <summary>
        /// MeetingType entity identifier
        /// </summary>
        public int ParticipationTypeId { get; private set; }
        /// <summary>
        /// OnSite MeetingType entity identifier
        /// </summary>
        public int InPersonParticipationId { get; private set; }
        #endregion
        /// <summary>
        /// Indicates if the reviewers registration is complete
        /// </summary>
        public bool IsRegistrationComplete { get; private set; }
    }
    /// <summary>
    /// Interface for panel reviewers
    /// </summary>
    public interface IPanelReviewerModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        int UserId { get; }
        /// <summary>
        /// Gets or sets the user information identifier.
        /// </summary>
        /// <value>
        /// The user information identifier.
        /// </value>
        int UserInfoId { get;  }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        string FirstName { get; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        string LastName { get; }
        /// <summary>
        /// Gets or sets the name suffix.
        /// </summary>
        /// <value>
        /// The name suffix.
        /// </value>
        string Suffix { get; }
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
        decimal? Rating { get; }
        /// <summary>
        /// Gets or sets the type of the participant.
        /// </summary>
        /// <value>
        /// The type of the participant.
        /// </value>
        string ParticipantType { get; }
        /// <summary>
        /// Gets or sets the participant method.
        /// </summary>
        /// <value>
        /// The participant method.
        /// </value>
        string ParticipantMethod { get; }
        /// <summary>
        /// Gets or sets the participant level.
        /// </summary>
        /// <value>
        /// The participant level.
        /// </value>
        string ParticipantLevel { get; }
        /// <summary>
        /// Gets or sets the participant role.
        /// </summary>
        /// <value>
        /// The participant role.
        /// </value>
        string ParticipantRole { get; }
        /// <summary>
        /// Gets or sets the participant role abbreviation.
        /// </summary>
        /// <value>
        /// The participant role abbreviation.
        /// </value>
        string ParticipantRoleAbbreviation { get; }
        /// <summary>
        /// Gets or sets the panel user assignment status.
        /// </summary>
        /// <value>
        /// The panel user assignment status.
        /// </value>
        string PanelUserAssignmentStatus { get; }
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        int PanelUserAssignmentId { get; }
        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        string Organization { get; }
        /// <summary>
        /// Get or set whether the user has a preferred email address
        /// </summary>
        /// <value>
        /// true if the user has one preferred email address; false otherwise
        /// </value>
        bool HasPreferredEmailAddress { get; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        string Position { get; }
        /// <summary>
        /// Gets or sets the degrees.
        /// </summary>
        /// <value>
        /// The degrees.
        /// </value>
        List<string> Degrees { get; }
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
        /// Gets or sets a value indicating whether this instance is client approved.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is client approved; otherwise, <c>false</c>.
        /// </value>
        bool? IsClientApproved { get; }
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
        /// TypeName of source entity object 
        /// </summary>
        string TypeName { get; }
        /// <summary>
        /// ParticipationType entity identifier of the meeting
        /// </summary>
        int ParticipationTypeId { get; }
        /// <summary>
        /// OnSite Participation entity identifier
        /// </summary>
        int InPersonParticipationId { get; }
        /// <summary>
        /// Indicates if the reviewers registration is complete
        /// </summary>
        bool IsRegistrationComplete { get; }
    }
}
