using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.MeetingManagement
{
    public interface IMeetingAttendanceModel
    {
        /// <summary>
         /// Gets or sets the reviewer user identifier.
         /// </summary>
         /// <value>
         /// The reviewer user identifier.
         /// </value>
        int ReviewerUserId { get; set; }
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
        /// Gets or sets a value indicating whether [restricted assigned flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [restricted assigned flag]; otherwise, <c>false</c>.
        /// </value>
        bool RestrictedAssignedFlag { get; set; }
        /// <summary>
        /// Gets or sets the participation method.
        /// </summary>
        /// <value>
        /// The participation method.
        /// </value>
        string ParticipationMethod { get; set; }
        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        string Program { get; set; }
        /// <summary>
        /// Gets or sets the programs
        /// </summary>
        IEnumerable<string> Programs { get; set; }
        /// <summary>
        /// Gets or sets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        string FiscalYear { get; set; }
        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the meeting abbreviation.
        /// </summary>
        /// <value>
        /// The meeting abbreviation.
        /// </value>
        string MeetingAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the name of the session.
        /// </summary>
        /// <value>
        /// The name of the session.
        /// </value>
        string SessionName { get; set; }
        /// <summary>
        /// Gets or sets the hotel modified by user identifier.
        /// </summary>
        /// <value>
        /// The hotel modified by user identifier.
        /// </value>
        int? HotelModifiedByUserId { get; set; }
        /// <summary>
        /// Gets or sets the travel modified by user identifier.
        /// </summary>
        /// <value>
        /// The travel modified by user identifier.
        /// </value>
        int? TravelModifiedByUserId { get; set; }
        /// <summary>
        /// Gets or sets the internal comments.
        /// </summary>
        /// <value>
        /// The internal comments.
        /// </value>
        string InternalComments { get; set; }
        /// <summary>
        /// Gets or sets the meeting registration identifier.
        /// </summary>
        /// <value>
        /// The meeting registration identifier.
        /// </value>
        int? MeetingRegistrationId { get; set; }
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        int? PanelUserAssignmentId { get; set; }
        /// <summary>
        /// The participation type in which the reviewer is assigned to the panel
        /// </summary>
        string ParticipantType { get; set; }
    }

    public class MeetingAttendanceModel : IMeetingAttendanceModel
    {
        public MeetingAttendanceModel() { }

        /// <summary>
        /// Overload for Non-Reviewer roles.
        /// </summary>
        /// <param name="reviewerUserId">The reviewer user identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="program">The program.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="meetingAbbreviation">The meeting abbreviation.</param>
        /// <param name="sessionName">Name of the session.</param>
        /// <param name="meetingRegistrationId">The meeting registration identifier.</param>
        /// <param name="institution">The institution.</param>
        /// <param name="email">The email.</param>
        /// <param name="role">The role.</param>
        public MeetingAttendanceModel(int reviewerUserId, string firstName, string lastName, 
                 string program, string fiscalYear, string meetingAbbreviation, string sessionName,
                 string institution,
                 string email, string role)
        {
            ReviewerUserId = reviewerUserId;
            FirstName = firstName;
            LastName = lastName;
            Program = program;
            FiscalYear = fiscalYear;
            MeetingAbbreviation = meetingAbbreviation;
            SessionName = sessionName;
            Institution = institution;
            Email = email;
            Role = role;
        }

        /// <summary>
        /// Gets or sets the reviewer user identifier.
        /// </summary>
        /// <value>
        /// The reviewer user identifier.
        /// </value>
        public int ReviewerUserId { get; set; }
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
        /// Gets or sets a value indicating whether [restricted assigned flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [restricted assigned flag]; otherwise, <c>false</c>.
        /// </value>
        public bool RestrictedAssignedFlag { get; set; }
        /// <summary>
        /// Gets or sets the participation method.
        /// </summary>
        /// <value>
        /// The participation method.
        /// </value>
        public string ParticipationMethod { get; set; }
        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public string Program { get; set; }
        /// <summary>
        /// Gets or sets the programs
        /// </summary>
        public IEnumerable<string> Programs { get; set; }
        /// <summary>
        /// Gets or sets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        public string FiscalYear { get; set; }
        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the meeting abbreviation.
        /// </summary>
        /// <value>
        /// The meeting abbreviation.
        /// </value>
        public string MeetingAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the name of the session.
        /// </summary>
        /// <value>
        /// The name of the session.
        /// </value>
        public string SessionName { get; set; }
        /// <summary>
        /// Gets or sets the hotel modified by user identifier.
        /// </summary>
        /// <value>
        /// The hotel modified by user identifier.
        /// </value>
        public int? HotelModifiedByUserId { get; set; }
        /// <summary>
        /// Gets or sets the travel modified by user identifier.
        /// </summary>
        /// <value>
        /// The travel modified by user identifier.
        /// </value>
        public int? TravelModifiedByUserId { get; set; }
        /// <summary>
        /// Gets or sets the internal comments.
        /// </summary>
        /// <value>
        /// The internal comments.
        /// </value>
        public string InternalComments { get; set; }
        /// <summary>
        /// Gets or sets the meeting registration identifier.
        /// </summary>
        /// <value>
        /// The meeting registration identifier.
        /// </value>
        public int? MeetingRegistrationId { get; set; }
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int? PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the session user assignment identifier.
        /// </summary>
        /// <value>
        /// The session user assignment identifier.
        /// </value>
        public int? SessionUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        public string Institution { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role { get; set; }

        /// <summary>
        /// The participation type in which the reviewer is assigned to the panel
        /// </summary>
        public string ParticipantType { get; set; }
    }
}
